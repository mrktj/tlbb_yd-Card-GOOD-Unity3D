using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using System.Security.Cryptography;

public class BundleCalculate : EditorWindow {
	
	

	[MenuItem("HaiwaiBundleRelease/统计资源信息(DownLoad使用)")]
	public static void SearchTextures(){

		string[] files =  GetAllFiles("Assets/StreamingAssets/","*.assetbundle");

		MD5 md5 = MD5.Create();
		for(int i = 0;i<files.Length ;i++){
			
			
			//DownloaderItem temp = new DownloaderItem(i,files[i],0,"0",files[i].Substring(0,files[i].IndexOf('/')));
			//files[i] = temp.ToString();
			FileStream str = File.OpenRead(files[i]);
			string Encode64 = HashString(str,md5);
			files[i] = files[i].Replace("Assets/StreamingAssets/","").Replace('\\','/')
						+ '\t'+Encode64;
			Debug.Log(files[i]);
		}
		
		Encoding utf8NoBom = new UTF8Encoding(false);
		
		File.WriteAllLines("Assets/StreamingAssets/"+ClientConfigure.resourceTxtName,files,utf8NoBom);
	}
	


	static string[] GetAllFiles(string rootPath,string paternan){
		List<string> files = new List<string>();
		RecureGetAllFiles(rootPath,files,paternan);
		return files.ToArray();
	}
	
	static void RecureGetAllFiles(string rootPath,List<string> files,string partenan){
		string[] subfiles;
		if(string.IsNullOrEmpty(partenan)){
			subfiles = Directory.GetFiles(rootPath);
		}else{
			subfiles = Directory.GetFiles(rootPath,partenan);
		}
		files.AddRange(subfiles);
		string[] subPaths = Directory.GetDirectories(rootPath);
		foreach(string sunPath in subPaths){
			RecureGetAllFiles(sunPath,files,partenan);
		}
	}

	public static string HashString(Stream source,MD5 md5){
		byte[] sourceBytes = md5.ComputeHash(source);
		StringBuilder sBuilder = new StringBuilder();
		for(int i = 0;i < sourceBytes.Length;i++){
			sBuilder.Append(sourceBytes[i].ToString("x"));
		}
		return sBuilder.ToString();
		
	}
}
