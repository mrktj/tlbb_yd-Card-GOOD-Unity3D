using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using System.IO;
using System;

public class ListWindow : EditorWindow
{
#if UNITY_ANDROID
	private const int WIDTH = 800;
	private const int HEIGHT = 450;
	private const int SPACE_WIDTH = 32;
	private const int SPACE_HEIGHT = 64;
	private static Collection<string> s_SearchUISpriteAnimation	= new Collection<string>();
	private static Vector2 scrollV = Vector2.zero;
	public static void Init (Collection<string>	s_SearchUISpriteAnimation)
	{  
		ListWindow.s_SearchUISpriteAnimation.Clear();
		foreach(string str in s_SearchUISpriteAnimation)
			ListWindow.s_SearchUISpriteAnimation.Add(str);
		int screenWidth = Screen.currentResolution.width;
		int screenHeight = Screen.currentResolution.height;
		ListWindow window = (ListWindow)EditorWindow.GetWindow (typeof(ListWindow));
		if(ListWindow.s_SearchUISpriteAnimation.Count == 0){
			window.position = new Rect ((screenWidth-(WIDTH>>1))>>1, (screenHeight-(HEIGHT>>1))>>1, WIDTH>>1, HEIGHT>>1);
		}else{
			window.position = new Rect ((screenWidth-WIDTH)>>1, (screenHeight-HEIGHT)>>1, WIDTH, HEIGHT);
		}
		window.ShowPopup ();
	} 
	public void OnGUI () {
		if(ListWindow.s_SearchUISpriteAnimation.Count == 0){
			GUILayout.Label("没有搜索到未设置尺寸的动画！", EditorStyles.boldLabel);
			return;
		}
		GUILayout.Label("搜索到未设置尺寸的动画如下：", EditorStyles.boldLabel);
		scrollV = GUILayout.BeginScrollView(scrollV, GUILayout.Width(WIDTH-SPACE_WIDTH),GUILayout.Height(HEIGHT-SPACE_HEIGHT));
		//string allStr = "";
		foreach(string str in s_SearchUISpriteAnimation){
			//allStr += (str + "\n");
			GUILayout.TextField(str, EditorStyles.textField);
		}
		// 放到一个文本区，便于选择
		//GUILayout.Label(allStr, EditorStyles.textField);
		GUILayout.EndScrollView();
	}
	
	public void OnDestroy(){
		s_SearchUISpriteAnimation.Clear();
	}
#endif
}