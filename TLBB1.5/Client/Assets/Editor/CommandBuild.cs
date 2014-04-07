using UnityEngine;
using UnityEditor;
//using UnityEditor.Callbacks;
//using UnityEditor.XCodeEditor;
using System.IO;

[AddComponentMenu("Build/Build")]
public class CommandBuild
{
	public static void BuildiOS()
	{
		BuildOptions opt = BuildOptions.UncompressedAssetBundle;
		CommandBuild.Build(BuildTarget.iPhone, opt);
	}
	
	public static void BuildWindows()
	{
		BuildOptions opt = BuildOptions.UncompressedAssetBundle | BuildOptions.SymlinkLibraries;
		CommandBuild.Build(BuildTarget.StandaloneWindows, opt);
	}
	
	private static void Build(BuildTarget _buildTarget, BuildOptions _opt)
	{
		string[] levels = {
			"Assets/Client/Scene/LogoScene.unity",
			"Assets/Client/Scene/LoginScene.unity",
			"Assets/Client/Scene/MainUI.unity",
			"Assets/Client/Scene/BattleUI.unity",
			"Assets/Client/Scene/loading.unity"};
		BuildPipeline.BuildPlayer(levels,"TLCard", _buildTarget, _opt);
		
//		// Create a new project object from build target
//		XCProject project = new XCProject( "TLCard" );
//
//		// Find and run through all projmods files to patch the project.
//		//Please pay attention that ALL projmods files in your project folder will be excuted!
//		string[] files = Directory.GetFiles( Application.dataPath, "*.projmods", SearchOption.AllDirectories );
//		foreach( string file in files ) {
//			project.ApplyMod( file );
//		}
//
//		// Finally save the xcode project
//		project.Save();
	}
	
}
