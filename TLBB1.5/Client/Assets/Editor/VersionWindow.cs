using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using System.IO;
using System;

public class VersionWindow : EditorWindow
{
#if UNITY_ANDROID
	private const int WIDTH = 320;
	private const int HEIGHT = 180;
	private static string bundleVersion = "";
	private static string bundleVersionCode = "";
	
	public static void Init ()
	{  
		bundleVersion = PlayerSettings.bundleVersion;
		try{
			bundleVersionCode = PlayerSettings.Android.bundleVersionCode.ToString();
		}catch(Exception e){
			bundleVersionCode = "";
		}
		int screenWidth = Screen.currentResolution.width;
		int screenHeight = Screen.currentResolution.height;
		VersionWindow window = (VersionWindow)EditorWindow.GetWindow (typeof(VersionWindow));
		window.position = new Rect ((screenWidth-WIDTH)>>1, (screenHeight-HEIGHT)>>1, WIDTH, HEIGHT);
		window.ShowPopup ();

	} 
	public void OnGUI () {
		GUILayout.Label ("Bundle Version", EditorStyles.boldLabel);
		bundleVersion = GUILayout.TextField(bundleVersion, EditorStyles.textField);
		GUILayout.Label ("Bundle Version Code", EditorStyles.boldLabel);
		bundleVersionCode = GUILayout.TextField(bundleVersionCode, EditorStyles.textField);
		if(GUILayout.Button("OK", EditorStyles.miniButtonMid)){
			byte success = 0;
			if(string.IsNullOrEmpty(bundleVersion)){
				EditorUtility.DisplayDialog("Error", "Bundle Version is Null!", "OK");
			}else{
				PlayerSettings.bundleVersion = bundleVersion;
				success++;
			}
			if(string.IsNullOrEmpty(bundleVersionCode)){
				EditorUtility.DisplayDialog("Error", "Bundle Version Code is Null!", "OK");
			}else{
				try{
					PlayerSettings.Android.bundleVersionCode = int.Parse(bundleVersionCode);
					success++;
				}catch(Exception e){
					EditorUtility.DisplayDialog("Error", "Bundle Version Code is Error!", "OK");
				}
			}
			if(success >= 2){
				VersionWindow window = (VersionWindow)EditorWindow.GetWindow (typeof(VersionWindow));
				window.Close();
			}
		}
	}
#endif
}