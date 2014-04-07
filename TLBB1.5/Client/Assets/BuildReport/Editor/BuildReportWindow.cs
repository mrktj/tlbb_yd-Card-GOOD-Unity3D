#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Text;


public class BuildReportWindow : EditorWindow
{
	string _projectName;
	string _buildType;
	BuildSizePart[] _buildSizes;
	string _totalBuildSize;
	string _compressedBuildSize;
	BuildSizePart[] _assetSizes;
	string _monoDLLs;

	GUISkin _usedSkin;

	Vector2 _assetListScrollPos;

	string GetLastFolder(string inFolder)
	{
		inFolder = inFolder.Replace('\\', '/');

		//Debug.Log("folder: " + inFolder);
		//string folderName = Path.GetDirectoryName(folderEntries[n]);

		int lastSlashIdx = inFolder.LastIndexOf('/');
		if (lastSlashIdx == -1)
		{
			return "";
		}
		return inFolder.Substring(lastSlashIdx+1, inFolder.Length-lastSlashIdx-1);

	}

	string FindAssetFolder(string folderToStart, string desiredFolderName)
	{
		string[] folderEntries = Directory.GetDirectories(folderToStart);

		for (int n = 0, len = folderEntries.Length; n < len; ++n)
		{
			string folderName = GetLastFolder(folderEntries[n]);
			//Debug.Log("folderName: " + folderName);

			if (folderName == desiredFolderName)
			{
				return folderEntries[n];
			}
			else
			{
				string recursed = FindAssetFolder(folderEntries[n], desiredFolderName);
				string recursedFolderName = GetLastFolder(recursed);
				if (recursedFolderName == desiredFolderName)
				{
					return recursed;
				}
			}
		}
		return "";
	}

	public void Init(string inProjectName, string inBuildType, BuildSizePart[] inBuildSizes, string inTotalSize, string inCompressedSize, BuildSizePart[] inAssetSizes, string inMonoDLLs)
	{
		_projectName = inProjectName;
		_buildType = inBuildType;
		_buildSizes = inBuildSizes;
		_totalBuildSize = inTotalSize;
		_compressedBuildSize = inCompressedSize;
		_assetSizes = inAssetSizes;
		_monoDLLs = inMonoDLLs;

		string guiSkinToUse = "BuildReportWindow.guiskin";
		if (EditorGUIUtility.isProSkin)
		{
			guiSkinToUse = "BuildReportWindowDark.guiskin";
		}

		// try default path
		_usedSkin = AssetDatabase.LoadAssetAtPath("Assets/BuildReport/GUI/" + guiSkinToUse, typeof(GUISkin)) as GUISkin;

		if (_usedSkin == null)
		{
			Debug.LogWarning("BuildReport package seems to have been moved. Finding...");

			string folderPath = FindAssetFolder(Application.dataPath, "BuildReport");
			folderPath = folderPath.Replace('\\', '/');
			int assetsIdx = folderPath.IndexOf("/Assets/");
			if (assetsIdx != -1)
			{
				folderPath = folderPath.Substring(assetsIdx+8, folderPath.Length-assetsIdx-8);
			}
			//Debug.Log(folderPath);

			_usedSkin = AssetDatabase.LoadAssetAtPath("Assets/" + folderPath + "/GUI/" + guiSkinToUse, typeof(GUISkin)) as GUISkin;
			//Debug.Log("_usedSkin " + (_usedSkin != null));
		}
	}

	void Refresh()
	{
		string projectName;
		string buildType;
		BuildSizePart[] buildSizes;
		string totalBuildSize;
		string compressedBuildSize;
		BuildSizePart[] assetSizes;
		string DLLs;
		BuildReport.GetValues(out projectName, out buildType, out buildSizes, out totalBuildSize, out compressedBuildSize, out assetSizes, out DLLs);

		Init(projectName, buildType, buildSizes, totalBuildSize, compressedBuildSize, assetSizes, DLLs);
	}

	void OnGUI()
	{
		if (_usedSkin == null)
		{
			return;
		}
		GUI.skin = _usedSkin;

		GUILayout.BeginHorizontal();

		GUILayout.BeginVertical();
		GUILayout.Label(_projectName+" By Waffles - Do not reupload!!! Read the readme!", "Title");
		GUILayout.Label("For " + _buildType, "Subtitle");
		GUILayout.EndVertical();

		//GUILayout.FlexibleSpace();
		if (GUILayout.Button("Refresh"))
		{
			Refresh();
		}
		GUILayout.EndHorizontal();

		GUILayout.Space(30);

		// for padding
		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		GUILayout.BeginVertical();




		// two column layout
		GUILayout.BeginHorizontal();

		// first column
		GUILayout.BeginVertical(GUILayout.MaxWidth(500));
		if (!string.IsNullOrEmpty(_compressedBuildSize))
		{
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			GUILayout.Label("Total Uncompressed\nBuild Size:", "Big1");
			GUILayout.Label(_totalBuildSize, "Big2");
			GUILayout.EndVertical();
			GUILayout.BeginVertical();
			GUILayout.Label("Total Compressed\nBuild Size:", "Big1");
			GUILayout.Label(_compressedBuildSize, "Big2");
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}
		else
		{
			GUILayout.Label("Total Build Size:", "Big1");
			GUILayout.Label(_totalBuildSize, "Big2");
		}
		GUILayout.Space(30);


		if (!string.IsNullOrEmpty(_compressedBuildSize))
		{
			GUILayout.BeginHorizontal();
		}
		GUILayout.Label("Size Breakdown:", "Big1");
		if (!string.IsNullOrEmpty(_compressedBuildSize))
		{
			GUILayout.Space(10);
			GUILayout.Label("Based on uncompressed build size", "Header2");
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}

		if (_buildSizes != null)
		{
//			try
//			{
//				FileStream aFile = new FileStream(@"E:\TLCard_Report.txt", FileMode.OpenOrCreate);
//				StreamWriter sw = new StreamWriter(aFile);
//				sw.WriteLine("BuildSizes");
//				foreach (BuildSizePart b in _buildSizes)
//				{
//					if (!b.IsTotal)
//					{
//						
//						sw.WriteLine(b.Name+"---"+b.Size+"---"+b.Percentage + "%");
//					}
//				}
//				sw.Close();
//			}catch(IOException ex)
//			{
//				Debug.Log(ex.Message);
//			}
			GUILayout.Space(10);
			bool useAlt = false;
			GUILayout.BeginHorizontal(GUILayout.MaxWidth(500));
			GUILayout.BeginVertical();
				foreach (BuildSizePart b in _buildSizes)
				{
					string styleToUse = useAlt ? "ListAltNormal" : "ListNormal";
					if (!b.IsTotal) GUILayout.Label(b.Name, styleToUse);
					useAlt = !useAlt;
				}
			GUILayout.EndVertical();

			GUILayout.BeginVertical();
				useAlt = false;
				foreach (BuildSizePart b in _buildSizes)
				{
					string styleToUse = useAlt ? "ListAltNormal" : "ListNormal";
					if (!b.IsTotal) GUILayout.Label(b.Size, styleToUse);
					useAlt = !useAlt;
				}
			GUILayout.EndVertical();

			GUILayout.BeginVertical();
				useAlt = false;
				foreach (BuildSizePart b in _buildSizes)
				{
					string styleToUse = useAlt ? "ListAltNormal" : "ListNormal";
					if (!b.IsTotal) GUILayout.Label(b.Percentage + "%", styleToUse);
					useAlt = !useAlt;
				}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}
		GUILayout.Space(30);

		GUILayout.Label("DLLs included:", "Big1");
		GUILayout.Label(_monoDLLs);

		GUILayout.EndVertical();

		GUILayout.Space(30);

		// second column
		GUILayout.BeginVertical();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Asset Breakdown:", "Big1");
		GUILayout.Space(10);

		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Sorted by", "Header2");
		GUILayout.Space(4);
		GUILayout.Label("uncompressed", "Header2Bold");
		GUILayout.Space(4);
		GUILayout.Label("size", "Header2");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();

		GUILayout.EndHorizontal();

		GUILayout.Space(10);

		_assetListScrollPos = GUILayout.BeginScrollView(
			_assetListScrollPos);
			if (_buildSizes != null)
			{
				const int LIST_HEIGHT = 25;
				const int ICON_DISPLAY_SIZE = 15;
				EditorGUIUtility.SetIconSize(Vector2.one * ICON_DISPLAY_SIZE);
				bool useAlt = true;

				GUILayout.BeginHorizontal();
				GUILayout.BeginVertical();
					foreach (BuildSizePart b in _assetSizes)
					{
						string styleToUse = useAlt ? "ListAlt" : "List";
						Texture icon = AssetDatabase.GetCachedIcon(b.Name);
						if (GUILayout.Button(new GUIContent(b.Name, icon), styleToUse, GUILayout.Height(LIST_HEIGHT)))
						{
							// thanks to http://answers.unity3d.com/questions/37180/how-to-highlight-or-select-an-asset-in-project-win.html
							GUI.skin = null;
							EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(b.Name, typeof(Object)));
							GUI.skin = _usedSkin;
						}
						useAlt = !useAlt;
					}
				GUILayout.EndVertical();

				GUILayout.BeginVertical();
					useAlt = true;
					foreach (BuildSizePart b in _assetSizes)
					{
						string styleToUse = useAlt ? "ListAlt" : "List";
						GUILayout.Label(b.Size, styleToUse, GUILayout.MinWidth(50), GUILayout.Height(LIST_HEIGHT));
						useAlt = !useAlt;
					}
				GUILayout.EndVertical();

				GUILayout.BeginVertical();
					useAlt = true;
					foreach (BuildSizePart b in _assetSizes)
					{
						string styleToUse = useAlt ? "ListAlt" : "List";
						GUILayout.Label(b.Percentage + "%", styleToUse, GUILayout.MinWidth(30), GUILayout.Height(LIST_HEIGHT));
						useAlt = !useAlt;
					}
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			}
		GUILayout.EndScrollView();

		GUILayout.EndVertical();

		GUILayout.EndHorizontal();


		GUILayout.Space(10);

		GUILayout.EndVertical();
		GUILayout.Space(10);
		GUILayout.EndHorizontal();
	}
}
#endif
