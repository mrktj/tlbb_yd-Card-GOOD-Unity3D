using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

/*

Editor
Editor log can be brought up through the Open Editor Log button in Unity's Console window.

Mac OS X	~/Library/Logs/Unity/Editor.log (or /Users/username/Library/Logs/Unity/Editor.log)
Windows XP *	C:\Documents and Settings\username\Local Settings\Application Data\Unity\Editor\Editor.log
Windows Vista/7 *	C:\Users\username\AppData\Local\Unity\Editor\Editor.log

(*) On Windows the Editor log file is stored in the local application data folder: %LOCALAPPDATA%\Unity\Editor\Editor.log, where LOCALAPPDATA is defined by CSIDL_LOCAL_APPDATA.





need to parse contents of editor log.
this part is what we're interested in:

[quote]
Textures      196.4 kb	 3.4% 
Meshes        0.0 kb	 0.0% 
Animations    0.0 kb	 0.0% 
Sounds        0.0 kb	 0.0% 
Shaders       0.0 kb	 0.0% 
Other Assets  37.4 kb	 0.6% 
Levels        8.5 kb	 0.1% 
Scripts       228.4 kb	 3.9% 
Included DLLs 5.2 mb	 91.7% 
File headers  12.5 kb	 0.2% 
Complete size 5.7 mb	 100.0% 

Used Assets, sorted by uncompressed size:
 39.1 kb	 0.7% Assets/BTX/GUI/Skin/Window.png
 21.0 kb	 0.4% Assets/BTX/GUI/BehaviourTree/Resources/BehaviourTreeGuiSkin.guiskin
 20.3 kb	 0.3% Assets/BTX/Fonts/DejaVuSans-SmallSize.ttf
 20.2 kb	 0.3% Assets/BTX/Fonts/DejaVuSans-Bold.ttf
 20.1 kb	 0.3% Assets/BTX/Fonts/DejaVuSansCondensed 1.ttf
 12.0 kb	 0.2% Assets/BTX/Fonts/DejaVuSansCondensed.ttf
 10.8 kb	 0.2% Assets/BTX/GUI/BehaviourTree/Nodes2/White.png
 8.1 kb	 0.1% Assets/BTX/GUI/BehaviourTree/Nodes/RoundedBox.png
 8.1 kb	 0.1% Assets/BTX/GUI/BehaviourTree/Nodes/Decorator.png
 4.9 kb	 0.1% Assets/BTX/GUI/Skin/Box.png
 4.6 kb	 0.1% Assets/BTX/GUI/BehaviourTree/GlovedHand.png
 4.5 kb	 0.1% Assets/BTX/GUI/Skin/TextField_Normal.png
 4.5 kb	 0.1% Assets/BTX/GUI/Skin/Button_Toggled.png
 4.5 kb	 0.1% Assets/BTX/GUI/Skin/Button_Normal.png
 4.5 kb	 0.1% Assets/BTX/GUI/Skin/Button_Active.png
 4.1 kb	 0.1% Assets/BTX/GUI/BehaviourTree/RunState/Visiting.png
 4.1 kb	 0.1% Assets/BTX/GUI/BehaviourTree/RunState/Success.png
 4.1 kb	 0.1% Assets/BTX/GUI/BehaviourTree/RunState/Running.png
 (etc. goes on and on until all files used are listed)
[/quote]


This part can also be helpful:

[quote]
Mono dependencies included in the build
Boo.Lang.dll
Mono.Security.dll
System.Core.dll
System.Xml.dll
System.dll
UnityScript.Lang.dll
mscorlib.dll
Assembly-CSharp.dll
Assembly-UnityScript.dll

[/quote]


so we're gonna flex our string parsing skills here.

just get this string since it seems to be constant enough:
"Used Assets, sorted by uncompressed size:"

then starting from that line going upwards, get the line that begins with "Textures"

we're relying on the assumption that this format won't get changed

in short, this is all complete hackery that won't be futureproof

hopefully UT would provide proper script access to this

*/

[System.Serializable]
public class BuildSizePart
{
	public string Name;
	public string Size;
	public double Percentage;

	public bool IsTotal { get{ return Name == "Complete size"; } }
}

public class BuildReport
{
	// doesn't work
	//[PostProcessBuild]
	//public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
	//{
	//	ShowBuildReport();
	//}

	static string GetTextFileContents(string file)
	{
		// thanks to http://answers.unity3d.com/questions/167518/reading-editorlog-in-the-editor.html
		FileStream fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
		StreamReader sr = new StreamReader(fs);

		if (fs != null && sr != null)
		{
			return sr.ReadToEnd();
		}
		return "";
	}

	public static string EditorLogContents
	{
		get
		{
			return GetTextFileContents(EditorLogPath);
		}
	}

	public static string EditorPrevLogContents
	{
		get
		{
			return GetTextFileContents(EditorPrevLogPath);
		}
	}

	public static string EditorDebugLogContents
	{
		get
		{
			return GetTextFileContents(Application.dataPath + "/BuildReportDebug/Editor.log");
		}
	}


	// from http://stackoverflow.com/questions/1143706/getting-the-path-of-the-home-directory-in-c
	public static string UserHomePath
	{
		get
		{
			string homePath = (Environment.OSVersion.Platform == PlatformID.Unix || 
				Environment.OSVersion.Platform == PlatformID.MacOSX)
				? Environment.GetEnvironmentVariable("HOME")
				: Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

			return homePath;
		}
	}

	// for windows only
	public static string LocalAppDataPath
	{
		get
		{
//			return Environment.GetEnvironmentVariable("LOCALAPPDATA");
			//cb 
			return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		}
	}

	public static string EditorLogPath
	{
		get
		{
			if (Environment.OSVersion.Platform == PlatformID.Unix || 
				Environment.OSVersion.Platform == PlatformID.MacOSX)
			{
				return UserHomePath + "/Library/Logs/Unity/Editor.log";
			}
			Debug.Log("LocalAppDataPath:"+LocalAppDataPath);
			return LocalAppDataPath + "\\Unity\\Editor\\Editor.log";
		}
	}

	public static string EditorPrevLogPath
	{
		get
		{
			if (Environment.OSVersion.Platform == PlatformID.Unix || 
				Environment.OSVersion.Platform == PlatformID.MacOSX)
			{
				return UserHomePath + "/Library/Logs/Unity/Editor-prev.log";
			}
			return LocalAppDataPath + "\\Unity\\Editor\\Editor-prev.log";
		}
	}

	// thanks to http://answers.unity3d.com/questions/16804/retrieving-project-name.html
	public static string ProjectName
	{
		get
		{
			var dp = Application.dataPath;
			string[] s = dp.Split("/"[0]);
			return s[s.Length - 2];
		}
	}


	static BuildSizePart[] ParseBuildSizePartsFromString(string inText)
	{
		// now parse the build parts to an array of `BuildSizePart`
		List<BuildSizePart> buildSizes = new List<BuildSizePart>();

		string[] buildPartsSplitted = inText.Split(new Char[] {'\n', '\r'});
		foreach (string b in buildPartsSplitted)
		{
			if (!string.IsNullOrEmpty(b))
			{
				//Debug.Log("got: " + b);

				string gotName = "";
				string gotSize = "";
				string gotPercent = "";

				Match match = Regex.Match(b, @"^[a-z \t]+[^0-9]", RegexOptions.IgnoreCase);
				if (match.Success)
				{
					gotName = match.Groups[0].Value;
					gotName = gotName.Trim();
					//Debug.Log("    name? " + gotName);
				}

				match = Regex.Match(b, @"[0-9.]+ (kb|mb|b|gb)", RegexOptions.IgnoreCase);
				if (match.Success)
				{
					gotSize = match.Groups[0].Value.ToUpper();
					//Debug.Log("    size? " + gotSize);
				}

				match = Regex.Match(b, @"[0-9.]+%", RegexOptions.IgnoreCase);
				if (match.Success)
				{
					gotPercent = match.Groups[0].Value;
					gotPercent = gotPercent.Substring(0, gotPercent.Length-1);
					//Debug.Log("    percent? " + gotPercent);
				}

				BuildSizePart inPart = new BuildSizePart();
				inPart.Name = gotName;
				inPart.Size = gotSize;
				inPart.Percentage = Double.Parse(gotPercent);
				buildSizes.Add(inPart);
			}
		}

		return buildSizes.ToArray();
	}

	static string GetBuildTypeFromEditorLog(string editorLog)
	{
		const string buildTypeKey = "*** Completed 'Build.Player.";

		int buildTypeIdx = editorLog.LastIndexOf(buildTypeKey);
		//Debug.Log("buildTypeIdx: " + buildTypeIdx);

		int buildTypeEndIdx = editorLog.IndexOf("' in ", buildTypeIdx);
		//Debug.Log("buildTypeEndIdx: " + buildTypeEndIdx);

		string buildType = editorLog.Substring(buildTypeIdx+buildTypeKey.Length, buildTypeEndIdx-buildTypeIdx-buildTypeKey.Length);
		//Debug.Log("buildType: " + buildType);
		return buildType;
	}

	static BuildSizePart[] ParseAssetSizesFromEditorLog(string editorLog, int offset)
	{
		List<BuildSizePart> assetSizes = new List<BuildSizePart>();
		int assetListStaIdx = editorLog.IndexOf("\n", offset);
		//Debug.Log("assetListStaIdx: " + assetListStaIdx);

		//Debug.Log(editorLog.Substring(assetListStaIdx, 500));

		int currentIdx = assetListStaIdx+1;
		while (true)
		{
			int lineEndIdx = editorLog.IndexOf("\n", currentIdx);
			string line = editorLog.Substring(currentIdx, lineEndIdx-currentIdx);
			//Debug.Log("line: " + line);
			
			Match match = Regex.Match(line, @"^ [0-9].*[a-z0-9 ]$", RegexOptions.IgnoreCase);
			if (match.Success)
			{
				// it's an asset entry. parse it
				//string b = match.Groups[0].Value;

				string gotName = "???";
				string gotSize = "?";
				string gotPercent = "?";

				match = Regex.Match(line, @"Assets/.+", RegexOptions.IgnoreCase);
				if (match.Success)
				{
					gotName = match.Groups[0].Value;
					gotName = gotName.Trim();
					//Debug.Log("    name? " + gotName);
				}

				match = Regex.Match(line, @"[0-9.]+ (kb|mb|b|gb)", RegexOptions.IgnoreCase);
				if (match.Success)
				{
					gotSize = match.Groups[0].Value.ToUpper();
					//Debug.Log("    size? " + gotSize);
				}
				else
				{
					Debug.Log("didn't find size for :" + line);
				}

				match = Regex.Match(line, @"[0-9.]+%", RegexOptions.IgnoreCase);
				if (match.Success)
				{
					gotPercent = match.Groups[0].Value;
					gotPercent = gotPercent.Substring(0, gotPercent.Length-1);
					//Debug.Log("    percent? " + gotPercent);
				}
				else
				{
					Debug.Log("didn't find percent for :" + line);
				}
				//Debug.Log("got: " + gotName + " size: " + gotSize);

				BuildSizePart inPart = new BuildSizePart();
				inPart.Name = gotName;
				inPart.Size = gotSize;
				inPart.Percentage = Double.Parse(gotPercent);
				assetSizes.Add(inPart);

			}
			else
			{
				break;
			}
			currentIdx = lineEndIdx+1;
		}
		return assetSizes.ToArray();
	}

	public static void GetValues(out string projectName, out string buildType, out BuildSizePart[] buildSizes, out string totalBuildSize, out string compressedBuildSize, out BuildSizePart[] assetSizes, out string DLLs)
	{
		//Debug.Log(EditorLogContents);

		projectName = ProjectName;
//		Debug.Log("projectName:"+projectName);
		string editorLog = EditorLogContents;
		editorLog = editorLog.Replace("\r\n", "\n");
//		Debug.Log("editorLog:"+editorLog);
		const string REPORT_START_KEY = "100.0% \n\nUsed Assets, sorted by uncompressed size:";

		int usedAssetsIdx = editorLog.LastIndexOf(REPORT_START_KEY);

		if (usedAssetsIdx == -1)
		{
			Debug.LogWarning("Build Report Window: No build info found in current session. Looking at data from previous session...");
			editorLog = EditorPrevLogContents;
			editorLog = editorLog.Replace("\r\n", "\n");
		}

		usedAssetsIdx = editorLog.LastIndexOf(REPORT_START_KEY);

		if (usedAssetsIdx == -1)
		{
			Debug.LogWarning("Build Report Window: No build info found. Build the project first.");
			buildType = "";
			buildSizes = new BuildSizePart[0];
			totalBuildSize = "";
			compressedBuildSize = "";
			assetSizes = new BuildSizePart[0];
			DLLs = "";
			return;
		}

//		Debug.Log("usedAssetsIdx: " + usedAssetsIdx);

		int texturesIdx = editorLog.LastIndexOf("Textures", usedAssetsIdx);
//		Debug.Log("texturesIdx: " + texturesIdx);

		int completeSizeIdx = editorLog.IndexOf("Complete size ", texturesIdx);
//		Debug.Log("completeSizeIdx: " + completeSizeIdx);

		completeSizeIdx = editorLog.IndexOf("\n", completeSizeIdx);

		string buildParts = editorLog.Substring(texturesIdx, completeSizeIdx-texturesIdx);
//		Debug.Log("count: " + (completeSizeIdx-texturesIdx));


//		Debug.Log("STA\n" + buildParts + "\nEND\n");

		buildSizes = ParseBuildSizePartsFromString(buildParts);

		totalBuildSize = "";

		foreach (BuildSizePart b in buildSizes)
		{
			if (b.IsTotal)
			{
				totalBuildSize = b.Size;
			}
		}

		compressedBuildSize = "";
		const string COMPRESSED_BUILD_SIZE_KEY = "Total compressed size ";
		int compressedBuildSizeIdx = editorLog.LastIndexOf(COMPRESSED_BUILD_SIZE_KEY, usedAssetsIdx, 800);
		if (compressedBuildSizeIdx != -1)
		{
			int compressedBuildSizeEndIdx = editorLog.IndexOf(". Total uncompressed size ", compressedBuildSizeIdx);
			compressedBuildSize = editorLog.Substring(compressedBuildSizeIdx+COMPRESSED_BUILD_SIZE_KEY.Length, compressedBuildSizeEndIdx - compressedBuildSizeIdx - COMPRESSED_BUILD_SIZE_KEY.Length);
			//Debug.Log("compressed: " + compressedBuildSize);
		}

		Array.Sort(buildSizes, delegate(BuildSizePart b1, BuildSizePart b2) {
			if (b1.Percentage > b2.Percentage) return -1;
			if (b1.Percentage < b2.Percentage) return 1;
			return 0;
		});

		buildType = GetBuildTypeFromEditorLog(editorLog);


		assetSizes = ParseAssetSizesFromEditorLog(editorLog, usedAssetsIdx+REPORT_START_KEY.Length);

		const string MONO_DLL_KEY = "Mono dependencies included in the build\n";
		int monoDllsStaIdx = editorLog.LastIndexOf(MONO_DLL_KEY, texturesIdx);
		int monoDllsEndIdx = editorLog.IndexOf("\n\n", monoDllsStaIdx);
		DLLs = editorLog.Substring(monoDllsStaIdx+MONO_DLL_KEY.Length, monoDllsEndIdx - monoDllsStaIdx-MONO_DLL_KEY.Length);
		//Debug.Log("STA\n" + monoDlls + "\nEND\n");

		
	}


	[MenuItem("Window/Show Build Report")]
	public static void sShowBuildReport()
	{
		string projectName;
		string buildType;
		BuildSizePart[] buildSizes;
		string totalBuildSize;
		string compressedBuildSize;
		BuildSizePart[] assetSizes;
		string DLLs;
		GetValues(out projectName, out buildType, out buildSizes, out totalBuildSize, out compressedBuildSize, out assetSizes, out DLLs);

		BuildReportWindow window = ScriptableObject.CreateInstance<BuildReportWindow>();
		window.Init(ProjectName, buildType, buildSizes, totalBuildSize, compressedBuildSize, assetSizes, DLLs);
		window.ShowUtility();
	}
}
