using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System;

public class BundleBuilder : Editor {

	[MenuItem("AssetBundler Builder/Build BanShenXiang Assets")]
	static void BuildBanShenXiangAssets()
	{
		Caching.CleanCache ();
        //获取在Project视图中选择的所有游戏对象
		UnityEngine.Object[] SelectedAsset = Selection.GetFiltered (typeof(UnityEngine.Object), SelectionMode.DeepAssets);
 
        //遍历所有的游戏对象
		foreach (UnityEngine.Object obj in SelectedAsset) 
		{
			//本地测试：建议最后将Assetbundle放在StreamingAssets文件夹下，如果没有就创建一个，因为移动平台下只能读取这个路径
			//StreamingAssets是只读路径，不能写入
			//服务器下载：就不需要放在这里，服务器上客户端用www类进行下载。
			string targetPath = Application.dataPath + "/StreamingAssets/banshenxiang/" + obj.name + ".assetbundle";
#if UNITY_ANDROID
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies,BuildTarget.Android)) {
#else
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies,BuildTarget.iPhone)) {
#endif
  				Debug.Log("资源打包成功: " + obj.name );
			} 
			else 
			{
 				Debug.Log("资源打包失败: "+obj.name );
			}
		}
		//刷新编辑器
		AssetDatabase.Refresh ();	
		
	}

	[MenuItem("AssetBundler Builder/Build Sounds Assets")]
	static void BuildSoundsAssets()
	{
		Caching.CleanCache ();
        //获取在Project视图中选择的所有游戏对象
		UnityEngine.Object[] SelectedAsset = Selection.GetFiltered (typeof(UnityEngine.Object), SelectionMode.DeepAssets);
 
        //遍历所有的游戏对象
		foreach (UnityEngine.Object obj in SelectedAsset) 
		{
			string targetPath = Application.dataPath + "/StreamingAssets/Sounds/" + obj.name + ".assetbundle";
#if UNITY_ANDROID
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies,BuildTarget.Android)) {
#else
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies,BuildTarget.iPhone)) {
#endif
  				Debug.Log("资源打包成功: " + obj.name );
			} 
			else 
			{
 				Debug.Log("资源打包失败: "+obj.name );
            }
        }
        //刷新编辑器
        AssetDatabase.Refresh();

    }

    [MenuItem("AssetBundler Builder/Build BattleBackground Assets")]
    static void BuildBattleBackgroundAssets()
    {
        Caching.CleanCache();
        //获取在Project视图中选择的所有游戏对象
        UnityEngine.Object[] SelectedAsset = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);

        //遍历所有的游戏对象
        foreach (UnityEngine.Object obj in SelectedAsset)
        {
            string targetPath = Application.dataPath + "/StreamingAssets/BattleBackground/" + obj.name + ".assetbundle";
#if UNITY_ANDROID
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies,BuildTarget.Android)) {
#else
            if (BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.iPhone))
            {
#endif
                Debug.Log("资源打包成功: " + obj.name);
            }
            else
            {
                Debug.Log("资源打包失败: " + obj.name);
            }
        }
        //刷新编辑器
        AssetDatabase.Refresh();

    }

    [MenuItem("AssetBundler Builder/Build Txt Assets")]
    static void BuildTxtAssets()
    {

        string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + "Client/Resources/");
        string[] div_line = new string[] { "Assets/" };
        Debug.Log(Application.persistentDataPath);

        string TableDataPath = Application.persistentDataPath + "/TableData";
        if (!Directory.Exists(TableDataPath))
        {
            Directory.CreateDirectory(TableDataPath);
        }

        foreach (string fileName in fileEntries)
        {
            string[] sTemp = fileName.Split(div_line, StringSplitOptions.RemoveEmptyEntries);
            string filePath = sTemp[1];

            string[] sEnd = sTemp[1].Split('.');
            if (sEnd.Length > 1 && sEnd[1].ToUpper() == "TXT")
            {
                //int index = filePath.LastIndexOf("/");  
                filePath = "Assets/" + filePath;
                //Debug.Log(filePath);  
                string localPath = filePath;

                UnityEngine.Object t = AssetDatabase.LoadMainAssetAtPath(localPath);
                Debug.Log(localPath);
                if (t != null)
                {
                    Debug.Log(t.name);
                    string bundlePath = Application.persistentDataPath + "/TableData/" + t.name + ".unity3d";
                   
                    Debug.Log("Building bundle at: " + bundlePath);

                    //从激活的选择编译资源文件
                    BuildPipeline.BuildAssetBundle(t, null, bundlePath, BuildAssetBundleOptions.CompleteAssets);
                }
            }
        }
    }

    [MenuItem("AssetBundler Builder/Build iphone Txt Assets")]
    static void BuildTxtAssetsIphone()
    {

        string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + "Client/Resources/");
        string[] div_line = new string[] { "Assets/" };
        string TableDataPath = Application.persistentDataPath + "/TableData";
        if (!Directory.Exists(TableDataPath))
        {
            Directory.CreateDirectory(TableDataPath);
        }
        foreach (string fileName in fileEntries)
        {
            string[] sTemp = fileName.Split(div_line, StringSplitOptions.RemoveEmptyEntries);
            string filePath = sTemp[1];

            string[] sEnd = sTemp[1].Split('.');
            if (sEnd.Length > 1 && sEnd[1].ToUpper() == "TXT")
            {
                //int index = filePath.LastIndexOf("/");  
                filePath = "Assets/" + filePath;
                //Debug.Log(filePath);  
                string localPath = filePath;

                UnityEngine.Object t = AssetDatabase.LoadMainAssetAtPath(localPath);
                Debug.Log(localPath);
                if (t != null)
                {
                    Debug.Log(t.name);
                    string bundlePath = Application.persistentDataPath + "/TableData/" + t.name + ".unity3d";

                    Debug.Log("Building bundle at: " + bundlePath);

                    //从激活的选择编译资源文件
                    BuildPipeline.BuildAssetBundle(t, null, bundlePath, BuildAssetBundleOptions.CompleteAssets, BuildTarget.iPhone);
                }
            }
        }
    }

}
