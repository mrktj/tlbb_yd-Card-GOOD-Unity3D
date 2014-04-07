using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using System.IO;
using System;

public class AutoEdit : Editor {
#if UNITY_ANDROID
	public	delegate	bool	Handle(string assetPath);
	static 	Dictionary<string, Handle> 	s_handlemap 	= 	new Dictionary<string, Handle>();
	static 	Collection<string>			s_PreffixPath	=	new Collection<string>();
	static 	Collection<string>			s_IgnorePath	=	new Collection<string>();

	static	bool						s_thisAssetNeedChange = false;
	static 	Collection<string>			s_SearchUISpriteAnimation	=	new Collection<string>();
	static string tempAnimationPath;//临时记录动画路径

	//清空所有处理规则
	static	void 	ClearMap()
	{
		s_handlemap.Clear();
		s_PreffixPath.Clear();
		s_IgnorePath.Clear();
		s_SearchUISpriteAnimation.Clear();
	}

	
	//注册忽略
	static	void 	Ignore(string ignorePath)
	{
		s_IgnorePath.Add (ignorePath);
	}

	//注册后缀
	static	void 	Register(string suffix, Handle hd)
	{
		s_handlemap.Add(suffix, hd);
	}
	
	//注册前缀
	static	void 	RegisterSearchPath(string path)
	{
		s_PreffixPath.Add (path);
	}
	
	//注册UISpriteAnimation
	static	void 	RegisterUISpriteAnimation(string animationPath)
	{
		s_SearchUISpriteAnimation.Add (animationPath);
	}
	//遍历所有资源，对指定路径资源进行相应处理
	static	void	ProcessPathHandles()
	{
		//获取在Project视图中选择的所有游戏对象
		int processedCount = 0;
		string[] assetPaths = AssetDatabase.GetAllAssetPaths();
		int totalNum = assetPaths.Length;
		int curIdx = 0;
		foreach(string assetPath in assetPaths)
		{
			curIdx++;
			if(curIdx < totalNum)
			{
				float process = (float)curIdx/(float)totalNum;
				EditorUtility.DisplayProgressBar(
						"Progress",
						assetPath,
						process);
			}
			else
			{
				EditorUtility.ClearProgressBar();
			}

			
			
			bool	bIgnore = false;
			foreach(string ignore in s_IgnorePath)
			{
				if(assetPath.Contains(ignore) == true)
				{
					bIgnore = true;
					break;
				}
			}
			
			if(bIgnore)
			{
				continue;
			}
			
			foreach(string preffix in s_PreffixPath)
			{
				if(assetPath.StartsWith(preffix))
				{
					foreach(string suf in s_handlemap.Keys)
					{
						if(assetPath.EndsWith(suf))
						{
							
							//返回为真代表这个文件有所处理
							//假代表没有任何改变
							s_thisAssetNeedChange = false;
							if(s_handlemap[suf](assetPath))
							{
								Debug.Log ("$$["+ assetPath +"] ");
							}
							else
							{
								//Debug.LogError ("$$[Error Change] " + assetPath);
							}
							processedCount++;
							if(processedCount == 50)
							{
								processedCount = 0;
								EditorUtility.UnloadUnusedAssetsIgnoreManagedReferences();
							}
							break;
						}
					}					
				}
			}
		}
		//刷新编辑器
		AssetDatabase.Refresh ();
	}
	
	
	/*****************
	 * 注册菜单事件
	 * ******************/
	
	/*
	 *  如果要全屏遮挡触摸，则BoxCollider的高度大于960的一律改成1280
		scaley>=1024 一律改成1280
  		所有UIPanel 中Depth pass去掉；--
  		NGUI材质shader尽量使用Unlit/Transparent Colored；--
  		Particle System->Renderer->Max Particle Size设置为100；--
  		不要使用完美像素适配--Pixel-Perfect，便于Android压缩纹理；程序中实时处理了
  		UIAtlas中Coordinates Pixels设置为Tex Coords；
	  * */
	[MenuItem("Android/1安卓基础设置")]
	static void AndroidBase()
	{
		Debug.Log ("$$ -----------------安卓基础设置-----------------");
		Caching.CleanCache ();
		
		ClearMap();
		
		//注册处理函数
		Register(".mat", HandleMat);
		Register(".prefab", HandlePrefab);
		Register("simhei.prefab", HandlesimheiFont);
		Ignore("SplashController");//忽略掉SplashController
		Ignore("MainUI_New.mat");//忽略掉
	
		RegisterSearchPath("Assets/Client/Resources/");
		RegisterSearchPath("Assets/Client/Prefabs/");
		RegisterSearchPath("Assets/Client/Atlas/");
		RegisterSearchPath("Assets/Client/Font/");
		RegisterSearchPath("Assets/Client/Logo/");
		RegisterSearchPath("Assets/Client/SpriteAnimations/");
		RegisterSearchPath("Assets/Client/SpritesCollections/");
		RegisterSearchPath("Assets/Client/Scene/");
		RegisterSearchPath("Assets/Client/jinyongtizi/");

		//对指定路径处理
		ProcessPathHandles();
		
		Caching.CleanCache ();
		// assetboundle banshenxiang

		

		ClearMap();
		Register(".prefab", HandleAssetbundleBackGround);
		RegisterSearchPath("Assets/SourceAssets/BattleBackground/");
		//对指定路径处理
		ProcessPathHandles();

		ClearMap();
		Register(".png", HandleAssetbundleBanshenxiang);
		RegisterSearchPath("Assets/SourceAssets/banshenxiang/");
		//对指定路径处理
		ProcessPathHandles();
		// assetboundle sounds
		ClearMap();
		Register(".prefab", HandleAssetbundleSounds);
		RegisterSearchPath("Assets/SourceAssets/Sounds/");
		//对指定路径处理
		ProcessPathHandles();
		
		//一帧的最长时间
		Debug.Log ("$$");
		Debug.Log ("$$  Time.maximumDeltaTime -> 0.05f");
		Time.maximumDeltaTime = 0.05f;
		
		//splash 自适应屏幕
		Debug.Log ("$$");
		Debug.Log ("$$  splashScreenScale -> ScaleToFill");
		PlayerSettings.Android.splashScreenScale = AndroidSplashScreenScale.ScaleToFill;
		//productName
		PlayerSettings.companyName = "GotoPlay";
		Debug.Log ("$$");
		Debug.Log ("$$  companyName -> "+PlayerSettings.companyName);
		PlayerSettings.productName = "天龍八部-真金庸 最武侠";
		Debug.Log ("$$");
		Debug.Log ("$$  productName -> "+PlayerSettings.productName);
		PlayerSettings.bundleIdentifier = "com.cyou.tlyd.android.tw";
		Debug.Log ("$$");
		Debug.Log ("$$  bundleIdentifier -> "+PlayerSettings.bundleIdentifier);
		//最低api
		PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel8;
		Debug.Log ("$$");
		Debug.Log ("$$  minSdkVersion -> "+PlayerSettings.Android.minSdkVersion.ToString());
		//OpenGLES
		PlayerSettings.targetGlesGraphics = TargetGlesGraphics.OpenGLES_2_0;
		Debug.Log ("$$");
		Debug.Log ("$$  minSdkVersion -> "+PlayerSettings.targetGlesGraphics.ToString());
		// ARM
		Debug.Log ("$$");
		Debug.Log ("$$  targetDevice -> ARMv7");
		PlayerSettings.Android.targetDevice = AndroidTargetDevice.ARMv7;
		
		//sd卡访问权限
		Debug.Log ("$$");
		Debug.Log ("$$  forceSDCardPermission -> true");
		PlayerSettings.Android.forceSDCardPermission = true;

		// google play 超过50M，使用扩展文件，扩展文件存放在 getExternalStorageDirectory()/Android/obb/<package-name>/ 路径下
		//PlayerSettings.Android.useAPKExpansionFiles = true;
		// keystore
		Debug.Log ("$$");
		Debug.Log ("$$  keystoreName -> Assets/Android/kdtl.keystore");
		PlayerSettings.Android.keystoreName = "AndroidPlugins/kdtl.keystore";
		PlayerSettings.Android.keystorePass = "123qweQWE";
		PlayerSettings.Android.keyaliasName = "kdtl";
		PlayerSettings.Android.keyaliasPass = "123qweQWE";

		//安卓设置完成
		EditorUtility.DisplayDialog("安卓设置", "安卓基础设置完成", "ok");
	}
	
	//生成高清版资源，修改png压缩格式就行了。
	[MenuItem("Android/2安卓图片资源修改/1高清版")]
	static void AndroidHighRes()
	{
		Debug.Log ("$$ -----------------安卓高清版-----------------");
		Caching.CleanCache ();
		
		ClearMap();
		
		//注册处理函数
		Register(".png", HandlePngHighRes);
		Ignore("Client/Font/");
    	Ignore("Resources/BattleBackground/");
    	Ignore("Atlas/Guide/");
		Ignore("LoginUI_1.png");
		

		RegisterSearchPath("Assets/Client/Resources/");
		RegisterSearchPath("Assets/Client/Prefabs/");
		RegisterSearchPath("Assets/Client/Atlas/");
		RegisterSearchPath("Assets/Client/Logo/");
		RegisterSearchPath("Assets/Client/SpriteAnimations/");
		RegisterSearchPath("Assets/Client/SpritesCollections/");
		RegisterSearchPath("Assets/Client/Scene/");
		RegisterSearchPath("Assets/Client/jinyongtizi/");

		//对指定路径处理
		ProcessPathHandles();
		
		//安卓设置完成
		EditorUtility.DisplayDialog("安卓设置", "安卓高清版设置完成", "ok");

	}
	
	//生成普通版资源，主要是对png资源的压缩格式，和图片最终大小的设置
	[MenuItem("Android/2安卓图片资源修改/2普通版")]
	static void AndroidNormalRes()
	{
		Debug.Log ("$$ -----------------安卓普通版-----------------");
		Caching.CleanCache ();
		
		ClearMap();
		
		//注册处理函数
		Register(".prefab", HandleNormalPrefab);
		Ignore("Client/Font/");
		
		RegisterSearchPath("Assets/Client/Resources/");
		RegisterSearchPath("Assets/Client/Prefabs/");
		RegisterSearchPath("Assets/Client/Atlas/");
		RegisterSearchPath("Assets/Client/Logo/");
		RegisterSearchPath("Assets/Client/SpriteAnimations/");
		RegisterSearchPath("Assets/Client/SpritesCollections/");
		RegisterSearchPath("Assets/Client/Scene/");
		RegisterSearchPath("Assets/Client/jinyongtizi/");

		
		//对指定路径处理
		ProcessPathHandles();
		
		Caching.CleanCache ();
		
		ClearMap();
		
		//注册处理函数
		Register(".png", HandlePngNormalRes);
		Ignore("Client/Font/");

		RegisterSearchPath("Assets/Client/Resources/");
		RegisterSearchPath("Assets/Client/Prefabs/");
		RegisterSearchPath("Assets/Client/Atlas/");
		RegisterSearchPath("Assets/Client/Logo/");
		RegisterSearchPath("Assets/Client/SpriteAnimations/");
		RegisterSearchPath("Assets/Client/SpritesCollections/");
		RegisterSearchPath("Assets/Client/Scene/");
		RegisterSearchPath("Assets/Client/jinyongtizi/");

		
		//对指定路径处理
		ProcessPathHandles();
		
		//安卓设置完成
		EditorUtility.DisplayDialog("安卓设置", "安卓普通版设置完成", "ok");

	}
	
	[MenuItem("Android/3Build/Build All")]
	static void Android_Build_ALL()
	{
		Debug.Log ("$$ -----------------3Build/CYOU渠道-----------------");
		buildAll();
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "1Build All完成", "ok");
	}
	
	[MenuItem("Android/3Build/Build LittleChannels")]
	static void Android_Build_LITTLE_CHANNEL()
	{
		Debug.Log ("$$ -----------------3Build/LittleChannels渠道-----------------");
		buildLittleChannel();
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "4Build LittleChannels完成", "ok");
	}

	[MenuItem("Android/3Build/Build TestLittleChannels")]
	static void Android_Build_TEST_LITTLE_CHANNEL()
	{
		Debug.Log ("$$ -----------------3Build/TestLittleChannels渠道-----------------");
		buildTestLittleChannel();
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "5Build TestLittleChannels完成", "ok");
	}

	[MenuItem("Android/3Build/Build TestSDKChannels")]
	static void Android_Build_TEST_ALL_SDK_CHANNEL()
	{
		Debug.Log ("$$ -----------------3Build/Build TestSDKChannels渠道-----------------");
		buildTestSDKChannel();
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "3Build/Build TestSDKChannels", "ok");
	}


	
	[MenuItem("Android/3Build/Build CYOU渠道")]
	static void Android_Build_CYOU()
	{
		Debug.Log ("$$ -----------------3Build/CYOU渠道-----------------");
		buildCyou(false,false);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Build CYOU渠道完成", "ok");
	}
	
	[MenuItem("Android/3Build/Build CYOU渠道 Obb模式")]
	static void Android_Build_CYOU_OBB()
	{
		Debug.Log ("$$ -----------------3Build/CYOU渠道Obb模式-----------------");
		buildCyou(false,true);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Build CYOU渠道Obb模式完成", "ok");
	}

	[MenuItem("Android/3Build/Build CYOU渠道(测试版)")]
	static void Android_Build_CYOU_test()
	{
		Debug.Log ("$$ -----------------3Build/CYOU渠道-----------------");
		buildCyou(true,false);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Build CYOU渠道完成", "ok");
	}
	
	[MenuItem("Android/3Build/Build UC渠道")]
	static void Android_Build_UC()
	{
		Debug.Log ("$$ -----------------Build/UC渠道-----------------");
		buildUC(false);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Build UC渠道完成", "ok");
	}

	[MenuItem("Android/3Build/Build UC渠道(测试版)")]
	static void Android_Build_UC_test()
	{
		Debug.Log ("$$ -----------------Build/UC渠道(测试版)-----------------");
		buildUC(true);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Build UC渠道(测试版)完成", "ok");
	}


	
	[MenuItem("Android/3Build/Build 360渠道")]
	static void Android_Build_360()
	{
		Debug.Log ("$$ -----------------3Build/6Build 360渠道-----------------");
		build360Channel(false);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "3Build/6Build 360渠道 完成", "ok");
	}

	[MenuItem("Android/3Build/Build 360渠道(测试版)")]
	static void Android_Build_360_Test()
	{
		Debug.Log ("$$ -----------------Android/3Build/6Build 360渠道(测试版)-----------------");
		build360Channel(true);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build 360渠道(测试版) 完成", "ok");
	}

	[MenuItem("Android/3Build/Build 豌豆荚SDK渠道")]
	static void Android_Build_wandoujia_sdk()
	{
		Debug.Log ("$$ -----------------3Build/豌豆荚SDK渠道-----------------");
		buildWanDouJiaChannel(false);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "3Build/豌豆荚SDK渠道 完成", "ok");
	}

	[MenuItem("Android/3Build/Build 豌豆荚SDK渠道(测试版)")]
	static void Android_Build_wandoujia_sdk_Test()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build 豌豆荚SDK渠道(测试版)-----------------");
		buildWanDouJiaChannel(true);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build 豌豆荚SDK渠道(测试版) 完成", "ok");
	}

	
	[MenuItem("Android/3Build/Build 91渠道")]
	static void Android_Build_91()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build 91渠道-----------------");
		build91Channel(false);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build 91渠道 完成", "ok");
	}

	[MenuItem("Android/3Build/Build 91渠道(测试版)")]
	static void Android_Build_91_Test()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build 91渠道(测试版)-----------------");
		build91Channel(true);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build 91渠道(测试版) 完成", "ok");
	}
	
	[MenuItem("Android/3Build/Build duoku渠道")]
	static void Android_Build_duoku()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build duoku渠道-----------------");
		buildduokuChannel(false);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build duoku渠道 完成", "ok");
	}

	[MenuItem("Android/3Build/Build duoku渠道(测试版)")]
	static void Android_Build_duoku_Test()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build duoku渠道(测试版)-----------------");
		buildduokuChannel(true);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build duoku渠道(测试版) 完成", "ok");
	}
	
	[MenuItem("Android/3Build/Build xiaomi渠道")]
	static void Android_Build_xiaomi()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build xiaomi渠道-----------------");
		buildxiaomiChannel(false);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build xiaomi渠道 完成", "ok");
	}

	[MenuItem("Android/3Build/Build xiaomi渠道(测试版)")]
	static void Android_Build_xiaomi_Test()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build xiaomi渠道(测试版)-----------------");
		buildxiaomiChannel(true);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build xiaomi渠道(测试版) 完成", "ok");
	}
	
	[MenuItem("Android/3Build/Build oppo渠道")]
	static void Android_Build_oppo()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build oppo渠道-----------------");
		buildoppoChannel(false);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build oppo渠道 完成", "ok");
	}

	[MenuItem("Android/3Build/Build oppo渠道(测试版)")]
	static void Android_Build_oppo_Test()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build xiaomi渠道(测试版)-----------------");
		buildoppoChannel(true);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build oppo渠道(测试版) 完成", "ok");
	}
	
	[MenuItem("Android/3Build/Build anzhi渠道")]
	static void Android_Build_anzhi()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build anzhi渠道-----------------");
		buildanzhiChannel(false);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build anzhi渠道 完成", "ok");
	}

	[MenuItem("Android/3Build/Build anzhi渠道(测试版)")]
	static void Android_Build_anzhi_Test()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build xiaomi渠道(测试版)-----------------");
		buildanzhiChannel(true);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build anzhi渠道(测试版) 完成", "ok");
	}



	[MenuItem("Android/3Build/Build 搜狗渠道")]
	static void Android_Build_sogou()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build 搜狗渠道-----------------");
		buildsogouChannel(false);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build 搜狗渠道 完成", "ok");
	}

	[MenuItem("Android/3Build/Build 搜狗渠道(测试版)")]
	static void Android_Build_sogou_Test()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build 搜狗渠道(测试版)-----------------");
		buildsogouChannel(true);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build 搜狗渠道(测试版) 完成", "ok");
	}

	[MenuItem("Android/3Build/Build 华为渠道")]
	static void Android_Build_huawei()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build 华为渠道-----------------");
		buildhuaweiChannel(false);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build 华为渠道 完成", "ok");
	}

	[MenuItem("Android/3Build/Build 华为渠道(测试版)")]
	static void Android_Build_huawei_Test()
	{
		Debug.Log ("$$ -----------------Android/3Build/Build 华为渠道(测试版)-----------------");
		buildhuaweiChannel(true);
		
		//安卓设置完成
		EditorUtility.DisplayDialog("Build", "Android/3Build/Build 华为渠道(测试版) 完成", "ok");
	}


	
	[MenuItem("Android/4搜索未设置尺寸的动画")]
	static void AndroidSearchUISpriteAnimation()
	{
		Debug.Log ("$$ -----------------搜索未设置大小的动画-----------------");
		Caching.CleanCache ();
		
		ClearMap();
		
		//注册处理函数
		Register(".prefab", HandleSearchAnimation);
	
		RegisterSearchPath("Assets/Client/Resources/");
		RegisterSearchPath("Assets/Client/Prefabs/");
		RegisterSearchPath("Assets/Client/SpriteAnimations/");
		RegisterSearchPath("Assets/Client/SpritesCollections/");
		RegisterSearchPath("Assets/Client/Scene/");

		//对指定路径处理
		ProcessPathHandles();
		// 显示列表
		ListWindow.Init(s_SearchUISpriteAnimation);
		s_SearchUISpriteAnimation.Clear();
	}
	
	[MenuItem("Android/5修改版本")]
	static void Android_Edit_Version(){
		VersionWindow.Init();
	}
	//======================build=========================
	// 需要编译的场景
	private static string[] LEVELS = {"Assets/Client/Scene/LogoScene.unity"
			, "Assets/Client/Scene/LoginScene.unity"
			, "Assets/Client/Scene/MainUI.unity"
			, "Assets/Client/Scene/BattleUI.unity"
	        , "Assets/Client/Scene/loading.unity"};
	private const string APK_NAME = "tlbbydb";
	private const string APK_FOLDER = "APK/";
	private static string MONTH = System.DateTime.Now.Month>9?""+System.DateTime.Now.Month:"0"+System.DateTime.Now.Month;
	private static string DAY = System.DateTime.Now.Day>9?""+System.DateTime.Now.Day:"0"+System.DateTime.Now.Day;
	private static string currDay = System.DateTime.Now.Year+"_"+MONTH+"_"+DAY;
	private static string currDay1 = System.DateTime.Now.Year+""+MONTH+""+DAY;
	
	private static void buildAll(){
		//buildxiaomiChannel(false);
		//build360Channel(false);
		//buildWanDouJiaChannel(false);
		buildanzhiChannel(false);
		build91Channel(false);
		buildsogouChannel(false);
		buildoppoChannel(false);
		buildUC(false);
		buildduokuChannel(false);
		buildhuaweiChannel(false);
	}
	
	private static void buildLittleChannel(){
		buildLittleChannel("sanxingapp");
		buildLittleChannel("sanxingyouxiquan");
		buildLittleChannel("nduoshichang");
		buildLittleChannel("sony");
		buildLittleChannel("zhongxing");
		buildLittleChannel("jinli");
		buildLittleChannel("jinshan");
		buildLittleChannel("shizimao");
		buildLittleChannel("muzhiwan");
		buildLittleChannel("wandoujia");
		buildLittleChannel("htc");
		buildLittleChannel("baoruan");
	}


	private static void buildTestSDKChannel(){
		Android_Build_360_Test();
		Android_Build_91_Test();
		Android_Build_duoku_Test();
		Android_Build_xiaomi_Test();
	}
	
	private static void buildTestLittleChannel(){
		buildTestLittleChannel("sanxingapp");
		buildTestLittleChannel("sanxingyouxiquan");
		buildTestLittleChannel("nduoshichang");
		buildTestLittleChannel("sony");
		buildTestLittleChannel("zhongxing");
		buildTestLittleChannel("jinli");
		buildTestLittleChannel("jinshan");
		buildTestLittleChannel("shizimao");
		buildTestLittleChannel("muzhiwan");
		buildTestLittleChannel("wandoujia");
	}
	
	
	private static void buildWanDouJiaChannel(bool bIsTest){
		setWanDouJia(bIsTest);
		string apkPath = APK_FOLDER;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath += currDay;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath = getApkPath("wandoujia_sdk");
		Debug.Log ("$$");
		Debug.Log ("$$  build android apk -> "+apkPath);
        BuildPipeline.BuildPlayer(LEVELS, apkPath, BuildTarget.Android, BuildOptions.None);
	}


	private static void build360Channel(bool bIsTest){
		set360(bIsTest);
		string apkPath = APK_FOLDER;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath += currDay;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath = getApkPath("360");
		Debug.Log ("$$");
		Debug.Log ("$$  build android apk -> "+apkPath);
        BuildPipeline.BuildPlayer(LEVELS, apkPath, BuildTarget.Android, BuildOptions.None);
	}

	private static void build91Channel(bool bIsTest){
		set91(bIsTest);
		string apkPath = APK_FOLDER;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath += currDay;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath = getApkPath("91");
		Debug.Log ("$$");
		Debug.Log ("$$  build android apk -> "+apkPath);
        BuildPipeline.BuildPlayer(LEVELS, apkPath, BuildTarget.Android, BuildOptions.None);
	}
	
	private static void buildduokuChannel(bool bIsTest){
		setduoku(bIsTest);
		string apkPath = APK_FOLDER;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath += currDay;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath = getApkPath("duoku");
		Debug.Log ("$$");
		Debug.Log ("$$  build android apk -> "+apkPath);
        BuildPipeline.BuildPlayer(LEVELS, apkPath, BuildTarget.Android, BuildOptions.None);
	}
	
	private static void buildxiaomiChannel(bool bIsTest){
		setxiaomi(bIsTest);
		string apkPath = APK_FOLDER;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath += currDay;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath = getApkPath("xiaomi");
		Debug.Log ("$$");
		Debug.Log ("$$  build android apk -> "+apkPath);
        BuildPipeline.BuildPlayer(LEVELS, apkPath, BuildTarget.Android, BuildOptions.None);
	}
	
	private static void buildoppoChannel(bool bIsTest){
		setoppo(bIsTest);
		string apkPath = APK_FOLDER;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath += currDay;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath = getApkPath("oppo");
		Debug.Log ("$$");
		Debug.Log ("$$  build android apk -> "+apkPath);
        BuildPipeline.BuildPlayer(LEVELS, apkPath, BuildTarget.Android, BuildOptions.None);
	}
	
	private static void buildsogouChannel(bool bIsTest){
		setsogou(bIsTest);
		string apkPath = APK_FOLDER;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath += currDay;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath = getApkPath("sogou");
		Debug.Log ("$$");
		Debug.Log ("$$  build android apk -> "+apkPath);
        BuildPipeline.BuildPlayer(LEVELS, apkPath, BuildTarget.Android, BuildOptions.None);
	}
	
	private static void buildhuaweiChannel(bool bIsTest){
		sethuawei(bIsTest);
		string apkPath = APK_FOLDER;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath += currDay;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath = getApkPath("huawei");
		Debug.Log ("$$");
		Debug.Log ("$$  build android apk -> "+apkPath);
        BuildPipeline.BuildPlayer(LEVELS, apkPath, BuildTarget.Android, BuildOptions.None);
	}


	private static void buildanzhiChannel(bool bIsTest){
		setanzhi(bIsTest);
		string apkPath = APK_FOLDER;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath += currDay;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath = getApkPath("anzhi");
		Debug.Log ("$$");
		Debug.Log ("$$  build android apk -> "+apkPath);
        BuildPipeline.BuildPlayer(LEVELS, apkPath, BuildTarget.Android, BuildOptions.None);
	}
	private static void buildLittleChannel(string path)
	{
		setLittleChannel(path);

		string apkPath = APK_FOLDER;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath += currDay;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath = getApkPath(path);
		Debug.Log ("$$");
		Debug.Log ("$$  build android apk -> "+apkPath);
        BuildPipeline.BuildPlayer(LEVELS, apkPath, BuildTarget.Android, BuildOptions.None);
	}
	
	private static void buildTestLittleChannel(string path)
	{
		setTestLittleChannel(path);

		string apkPath = APK_FOLDER;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath += currDay;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath = getApkPath(path);
		Debug.Log ("$$");
		Debug.Log ("$$  build android apk -> "+apkPath);
        BuildPipeline.BuildPlayer(LEVELS, apkPath, BuildTarget.Android, BuildOptions.None);
	}
	
	private static void buildCyou(bool bIsTest,bool bIsObbModel){
		setCyou(bIsTest);
		PlayerSettings.Android.useAPKExpansionFiles = bIsObbModel;
		
		string apkPath = APK_FOLDER;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath += currDay;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}

		apkPath = getApkPath("cyou");
		Debug.Log ("$$");
		Debug.Log ("$$  build android apk -> "+apkPath);
        BuildPipeline.BuildPlayer(LEVELS, apkPath, BuildTarget.Android, BuildOptions.None);
	}
	private static void buildUC(bool bIsTest){
		setUC(bIsTest);
		string apkPath = APK_FOLDER;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		apkPath += currDay;
		if(!Directory.Exists(apkPath)){
			Directory.CreateDirectory(apkPath);
		}
		if(bIsTest == false){
			apkPath = getApkPath("uc_release");
		}else{
			apkPath = getApkPath("uc_debug");
		}
		Debug.Log ("$$");
		Debug.Log ("$$  build android apk -> "+apkPath);
        BuildPipeline.BuildPlayer(LEVELS, apkPath, BuildTarget.Android, BuildOptions.None);
	}
	
	//=========================build end===============================
	private static void set360(bool isTest){
		Caching.CleanCache ();
		ClearMap();
	
		PlayerSettings.bundleIdentifier = "com.cyou.tlyd.android.tw.360";
		Debug.Log ("$$");
		Debug.Log ("$$  bundleIdentifier -> "+PlayerSettings.bundleIdentifier);
		
		Texture2D[] icons = new Texture2D[] {
		 AssetDatabase.LoadMainAssetAtPath("Assets/Client/Textures/Logo/114x114.png") as Texture2D,
		};
		Debug.Log ("$$");
		Debug.Log ("$$  change application icon");
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change splash image");
		FileUtil.ReplaceFile("Assets/Android/Splash/Normal/default.png", "Assets/Client/Logo/default.png");

		
		Debug.Log ("$$");
		Debug.Log ("$$  change Plugins files");
		FileUtil.DeleteFileOrDirectory("Assets/Plugins/Android");
		if(isTest == false)
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/Android_360","Assets/Plugins/Android");
		}
		else
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/test/Android_360","Assets/Plugins/Android");
		}

		AssetDatabase.Refresh();
		
		// keystore
		Debug.Log ("$$");
		Debug.Log ("$$  keystoreName -> Assets/Android/kdtl.keystore");
		PlayerSettings.Android.keystoreName = "AndroidPlugins/kdtl.keystore";
		PlayerSettings.Android.keystorePass = "123qweQWE";
		PlayerSettings.Android.keyaliasName = "kdtl";
		PlayerSettings.Android.keyaliasPass = "123qweQWE";
	}

	private static void setWanDouJia(bool isTest){
		Caching.CleanCache ();
		ClearMap();
	
		PlayerSettings.bundleIdentifier = "com.cyou.tlyd.android.tw.wdj";
		Debug.Log ("$$");
		Debug.Log ("$$  bundleIdentifier -> "+PlayerSettings.bundleIdentifier);
		
		Texture2D[] icons = new Texture2D[] {
		 AssetDatabase.LoadMainAssetAtPath("Assets/Client/Textures/Logo/114x114.png") as Texture2D,
		};
		Debug.Log ("$$");
		Debug.Log ("$$  change application icon");
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change splash image");
		FileUtil.ReplaceFile("Assets/Android/Splash/Normal/default.png", "Assets/Client/Logo/default.png");

		
		Debug.Log ("$$");
		Debug.Log ("$$  change Plugins files");
		FileUtil.DeleteFileOrDirectory("Assets/Plugins/Android");
		if(isTest == false)
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/Android_wandoujia_sdk","Assets/Plugins/Android");
		}
		else
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/test/Android_wandoujia_sdk","Assets/Plugins/Android");
		}

		AssetDatabase.Refresh();
		
		// keystore
		Debug.Log ("$$");
		Debug.Log ("$$  keystoreName -> Assets/Android/kdtl.keystore");
		PlayerSettings.Android.keystoreName = "AndroidPlugins/kdtl.keystore";
		PlayerSettings.Android.keystorePass = "123qweQWE";
		PlayerSettings.Android.keyaliasName = "kdtl";
		PlayerSettings.Android.keyaliasPass = "123qweQWE";
	}

	private static void set91(bool isTest){
		Caching.CleanCache ();
		ClearMap();
	
		PlayerSettings.bundleIdentifier = "com.cyou.tlyd.android.tw.91";
		Debug.Log ("$$");
		Debug.Log ("$$  bundleIdentifier -> "+PlayerSettings.bundleIdentifier);
		
		Texture2D[] icons = new Texture2D[] {
		 AssetDatabase.LoadMainAssetAtPath("Assets/Android/icons/114x114_91.png") as Texture2D,
		};

		Debug.Log ("$$");
		Debug.Log ("$$  change application icon");
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change splash image");
		FileUtil.ReplaceFile("Assets/Android/Splash/Normal/default.png", "Assets/Client/Logo/default.png");

		
		Debug.Log ("$$");
		Debug.Log ("$$  change Plugins files");
		FileUtil.DeleteFileOrDirectory("Assets/Plugins/Android");
		if(isTest == false)
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/Android_91","Assets/Plugins/Android");
		}
		else
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/test/Android_91","Assets/Plugins/Android");
		}

		AssetDatabase.Refresh();
		
		// keystore
		Debug.Log ("$$");
		Debug.Log ("$$  keystoreName -> Assets/Android/kdtl.keystore");
		PlayerSettings.Android.keystoreName = "AndroidPlugins/kdtl.keystore";
		PlayerSettings.Android.keystorePass = "123qweQWE";
		PlayerSettings.Android.keyaliasName = "kdtl";
		PlayerSettings.Android.keyaliasPass = "123qweQWE";
	}
	
	private static void setduoku(bool isTest){
		Caching.CleanCache ();
		ClearMap();
	
		PlayerSettings.bundleIdentifier = "com.cyou.tlyd.android.tw.duoku";
		Debug.Log ("$$");
		Debug.Log ("$$  bundleIdentifier -> "+PlayerSettings.bundleIdentifier);
		
		Texture2D[] icons = new Texture2D[] {
			AssetDatabase.LoadMainAssetAtPath("Assets/Android/icons/114x114_dk.png") as Texture2D,
		};
		Debug.Log ("$$");
		Debug.Log ("$$  change application icon");
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change splash image");
		FileUtil.ReplaceFile("Assets/Android/Splash/DK/default.png", "Assets/Client/Logo/default.png");

		
		Debug.Log ("$$");
		Debug.Log ("$$  change Plugins files");
		FileUtil.DeleteFileOrDirectory("Assets/Plugins/Android");
		if(isTest == false)
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/Android_duoku","Assets/Plugins/Android");
		}
		else
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/test/Android_duoku","Assets/Plugins/Android");
		}

		AssetDatabase.Refresh();
		
		// keystore
		Debug.Log ("$$");
		Debug.Log ("$$  keystoreName -> Assets/Android/kdtl.keystore");
		PlayerSettings.Android.keystoreName = "AndroidPlugins/kdtl.keystore";
		PlayerSettings.Android.keystorePass = "123qweQWE";
		PlayerSettings.Android.keyaliasName = "kdtl";
		PlayerSettings.Android.keyaliasPass = "123qweQWE";
	}

	private static void setxiaomi(bool isTest){
		Caching.CleanCache ();
		ClearMap();
	
		PlayerSettings.bundleIdentifier = "com.cyou.tlyd.android.tw.mi";
		Debug.Log ("$$");
		Debug.Log ("$$  bundleIdentifier -> "+PlayerSettings.bundleIdentifier);
		
		Texture2D[] icons = new Texture2D[] {
			AssetDatabase.LoadMainAssetAtPath("Assets/Android/icons/136x136_mi.png") as Texture2D,
		};
		Debug.Log ("$$");
		Debug.Log ("$$  change application icon");
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change splash image");
		FileUtil.ReplaceFile("Assets/Android/Splash/Normal/default.png", "Assets/Client/Logo/default.png");

		
		Debug.Log ("$$");
		Debug.Log ("$$  change Plugins files");
		FileUtil.DeleteFileOrDirectory("Assets/Plugins/Android");
		if(isTest == false)
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/Android_xiaomi","Assets/Plugins/Android");
		}
		else
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/test/Android_xiaomi","Assets/Plugins/Android");
		}

		AssetDatabase.Refresh();
		
		// keystore
		Debug.Log ("$$");
		Debug.Log ("$$  keystoreName -> Assets/Android/kdtl.keystore");
		PlayerSettings.Android.keystoreName = "AndroidPlugins/kdtl.keystore";
		PlayerSettings.Android.keystorePass = "123qweQWE";
		PlayerSettings.Android.keyaliasName = "kdtl";
		PlayerSettings.Android.keyaliasPass = "123qweQWE";
	}
	
	private static void setoppo(bool isTest){
		Caching.CleanCache ();
		ClearMap();
	
		PlayerSettings.bundleIdentifier = "com.cyou.tlyd.android.tw.oppo.nearme.gamecenter";
		Debug.Log ("$$");
		Debug.Log ("$$  bundleIdentifier -> "+PlayerSettings.bundleIdentifier);
		
		Texture2D[] icons = new Texture2D[] {
		 AssetDatabase.LoadMainAssetAtPath("Assets/Android/icons/114x114_oppo.png") as Texture2D,
		};
		Debug.Log ("$$");
		Debug.Log ("$$  change application icon");
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change splash image");
		FileUtil.ReplaceFile("Assets/Android/Splash/Normal/default.png", "Assets/Client/Logo/default.png");

		
		Debug.Log ("$$");
		Debug.Log ("$$  change Plugins files");
		FileUtil.DeleteFileOrDirectory("Assets/Plugins/Android");
		if(isTest == false)
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/Android_oppo","Assets/Plugins/Android");
		}
		else
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/test/Android_oppo","Assets/Plugins/Android");
		}

		AssetDatabase.Refresh();
		
		// keystore
		Debug.Log ("$$");
		Debug.Log ("$$  keystoreName -> Assets/Android/kdtl.keystore");
		PlayerSettings.Android.keystoreName = "AndroidPlugins/kdtl.keystore";
		PlayerSettings.Android.keystorePass = "123qweQWE";
		PlayerSettings.Android.keyaliasName = "kdtl";
		PlayerSettings.Android.keyaliasPass = "123qweQWE";
	}
	
	private static void setanzhi(bool isTest){
		Caching.CleanCache ();
		ClearMap();
	
		PlayerSettings.bundleIdentifier = "com.cyou.tlyd.android.tw.anzhi";
		Debug.Log ("$$");
		Debug.Log ("$$  bundleIdentifier -> "+PlayerSettings.bundleIdentifier);
		
		Texture2D[] icons = new Texture2D[] {
		 AssetDatabase.LoadMainAssetAtPath("Assets/Android/icons/144x144_anzhi.png") as Texture2D,
		};
		Debug.Log ("$$");
		Debug.Log ("$$  change application icon");
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change splash image");
		FileUtil.ReplaceFile("Assets/Android/Splash/anzhi/default.png", "Assets/Client/Logo/default.png");

		
		Debug.Log ("$$");
		Debug.Log ("$$  change Plugins files");
		FileUtil.DeleteFileOrDirectory("Assets/Plugins/Android");
		if(isTest == false)
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/Android_anzhi","Assets/Plugins/Android");
		}
		else
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/test/Android_anzhi","Assets/Plugins/Android");
		}

		AssetDatabase.Refresh();
		
		// keystore
		Debug.Log ("$$");
		Debug.Log ("$$  keystoreName -> Assets/Android/kdtl.keystore");
		PlayerSettings.Android.keystoreName = "AndroidPlugins/kdtl.keystore";
		PlayerSettings.Android.keystorePass = "123qweQWE";
		PlayerSettings.Android.keyaliasName = "kdtl";
		PlayerSettings.Android.keyaliasPass = "123qweQWE";
	}


	private static void sethuawei(bool isTest){
		Caching.CleanCache ();
		ClearMap();
	
		PlayerSettings.bundleIdentifier = "com.cyou.tlyd.android.tw.huawei";
		Debug.Log ("$$");
		Debug.Log ("$$  bundleIdentifier -> "+PlayerSettings.bundleIdentifier);
		
		Texture2D[] icons = new Texture2D[] {
		 AssetDatabase.LoadMainAssetAtPath("Assets/Client/Textures/Logo/114x114.png") as Texture2D,
		};
		Debug.Log ("$$");
		Debug.Log ("$$  change application icon");
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change splash image");
		FileUtil.ReplaceFile("Assets/Android/Splash/Normal/default.png", "Assets/Client/Logo/default.png");

		
		Debug.Log ("$$");
		Debug.Log ("$$  change Plugins files");
		FileUtil.DeleteFileOrDirectory("Assets/Plugins/Android");
		if(isTest == false)
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/Android_huawei","Assets/Plugins/Android");
		}
		else
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/test/Android_huawei","Assets/Plugins/Android");
		}

		AssetDatabase.Refresh();
		
		// keystore
		Debug.Log ("$$");
		Debug.Log ("$$  keystoreName -> Assets/Android/kdtl.keystore");
		PlayerSettings.Android.keystoreName = "AndroidPlugins/kdtl.keystore";
		PlayerSettings.Android.keystorePass = "123qweQWE";
		PlayerSettings.Android.keyaliasName = "kdtl";
		PlayerSettings.Android.keyaliasPass = "123qweQWE";
	}

	private static void setsogou(bool isTest){
		Caching.CleanCache ();
		ClearMap();
	
		PlayerSettings.bundleIdentifier = "com.cyou.tlyd.android.tw.sogou";
		Debug.Log ("$$");
		Debug.Log ("$$  bundleIdentifier -> "+PlayerSettings.bundleIdentifier);
		
		Texture2D[] icons = new Texture2D[] {
		 AssetDatabase.LoadMainAssetAtPath("Assets/Android/icons/144x144_sogou.png") as Texture2D,
		};
		Debug.Log ("$$");
		Debug.Log ("$$  change application icon");
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change splash image");
		FileUtil.ReplaceFile("Assets/Android/Splash/sogou/default.png", "Assets/Client/Logo/default.png");

		
		Debug.Log ("$$");
		Debug.Log ("$$  change Plugins files");
		FileUtil.DeleteFileOrDirectory("Assets/Plugins/Android");
		if(isTest == false)
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/Android_sogou_sdk","Assets/Plugins/Android");
		}
		else
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/test/Android_sogou_sdk","Assets/Plugins/Android");
		}

		AssetDatabase.Refresh();
		
		// keystore
		Debug.Log ("$$");
		Debug.Log ("$$  keystoreName -> Assets/Android/kdtl.keystore");
		PlayerSettings.Android.keystoreName = "AndroidPlugins/kdtl.keystore";
		PlayerSettings.Android.keystorePass = "123qweQWE";
		PlayerSettings.Android.keyaliasName = "kdtl";
		PlayerSettings.Android.keyaliasPass = "123qweQWE";
	}

	//=========================build end===============================
	private static void setCyou(bool isTest){
		Caching.CleanCache ();
		ClearMap();
		
		
		Texture2D[] icons = new Texture2D[] {
		 AssetDatabase.LoadMainAssetAtPath("Assets/Client/Textures/Logo/114x114.png") as Texture2D,
		};
		Debug.Log ("$$");
		Debug.Log ("$$  change application icon");
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change splash image");
		FileUtil.ReplaceFile("Assets/Android/Splash/Normal/default.png", "Assets/Client/Logo/default.png");
		
		PlayerSettings.productName = "天龍八部-真金庸 最武侠";
		PlayerSettings.bundleVersion = ClientConfigure.GetClientVersion();//"1.0.0.1";
		PlayerSettings.Android.bundleVersionCode = ClientConfigure.VersionNumber;//1001;
		
		PlayerSettings.bundleIdentifier = "com.cyou.tlyd.android.tw";
		Debug.Log ("$$");
		Debug.Log ("$$  bundleIdentifier -> "+PlayerSettings.bundleIdentifier);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change Plugins files");
		FileUtil.DeleteFileOrDirectory("Assets/Plugins/Android");
		if(isTest == true)
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/test/Android_cyou","Assets/Plugins/Android");
		}
		else
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/Android_cyou","Assets/Plugins/Android");
		}

		AssetDatabase.Refresh();
		
		// keystore
		Debug.Log ("$$");
		Debug.Log ("$$  keystoreName -> Assets/Android/kdtl.keystore");
		PlayerSettings.Android.keystoreName = "AndroidPlugins/kdtl.keystore";
		PlayerSettings.Android.keystorePass = "123qweQWE";
		PlayerSettings.Android.keyaliasName = "kdtl";
		PlayerSettings.Android.keyaliasPass = "123qweQWE";
	}
	
	private static void setLittleChannel(string path){
		Caching.CleanCache ();
		ClearMap();
		
		
		Texture2D[] icons = new Texture2D[] {
		 AssetDatabase.LoadMainAssetAtPath("Assets/Client/Textures/Logo/114x114.png") as Texture2D,
		};
		Debug.Log ("$$");
		Debug.Log ("$$  change application icon");
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change splash image");
		FileUtil.ReplaceFile("Assets/Android/Splash/Normal/default.png", "Assets/Client/Logo/default.png");

		PlayerSettings.bundleIdentifier = "com.cyou.tlyd.android.tw";
		Debug.Log ("$$");
		Debug.Log ("$$  bundleIdentifier -> "+PlayerSettings.bundleIdentifier);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change Plugins files");
		FileUtil.DeleteFileOrDirectory("Assets/Plugins/Android");
		FileUtil.CopyFileOrDirectory("AndroidPlugins/Android_"+path,"Assets/Plugins/Android");


		AssetDatabase.Refresh();
		
		// keystore
		Debug.Log ("$$");
		Debug.Log ("$$  keystoreName -> Assets/Android/kdtl.keystore");
		PlayerSettings.Android.keystoreName = "AndroidPlugins/kdtl.keystore";
		PlayerSettings.Android.keystorePass = "123qweQWE";
		PlayerSettings.Android.keyaliasName = "kdtl";
		PlayerSettings.Android.keyaliasPass = "123qweQWE";
	}
	
	private static void setTestLittleChannel(string path){
		Caching.CleanCache ();
		ClearMap();
		
		
		Texture2D[] icons = new Texture2D[] {
		 AssetDatabase.LoadMainAssetAtPath("Assets/Client/Textures/Logo/144x144.png") as Texture2D,
		};
		Debug.Log ("$$");
		Debug.Log ("$$  change application icon");
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change splash image");
		FileUtil.ReplaceFile("Assets/Android/Splash/Normal/default.png", "Assets/Client/Logo/default.png");

		PlayerSettings.bundleIdentifier = "com.cyou.tlyd.android.tw";
		Debug.Log ("$$");
		Debug.Log ("$$  bundleIdentifier -> "+PlayerSettings.bundleIdentifier);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change Plugins files");
		FileUtil.DeleteFileOrDirectory("Assets/Plugins/Android");
		FileUtil.CopyFileOrDirectory("AndroidPlugins/test/Android_"+path,"Assets/Plugins/Android");


		AssetDatabase.Refresh();
		
		// keystore
		Debug.Log ("$$");
		Debug.Log ("$$  keystoreName -> Assets/Android/kdtl.keystore");
		PlayerSettings.Android.keystoreName = "AndroidPlugins/kdtl.keystore";
		PlayerSettings.Android.keystorePass = "123qweQWE";
		PlayerSettings.Android.keyaliasName = "kdtl";
		PlayerSettings.Android.keyaliasPass = "123qweQWE";
	}
	
	private static void setUC(bool bIsTest){
		Caching.CleanCache ();
		ClearMap();
		
		
		Texture2D[] icons = new Texture2D[] {
		 AssetDatabase.LoadMainAssetAtPath("Assets/Android/icons/114x114_uc.png") as Texture2D,
		};
		Debug.Log ("$$");
		Debug.Log ("$$  change application icon");
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);

		Debug.Log ("$$");
		Debug.Log ("$$  change splash image");
		FileUtil.ReplaceFile("Assets/Android/Splash/UC/default.png", "Assets/Client/Logo/default.png");

		PlayerSettings.bundleIdentifier = "com.cyou.tlyd.android.tw.uc";
		Debug.Log ("$$");
		Debug.Log ("$$  bundleIdentifier -> "+PlayerSettings.bundleIdentifier);
		
		Debug.Log ("$$");
		Debug.Log ("$$  change Plugins files");
		FileUtil.DeleteFileOrDirectory("Assets/Plugins/Android");

		if(bIsTest == true)
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/test/Android_uc","Assets/Plugins/Android");
		}
		else
		{
			FileUtil.CopyFileOrDirectory("AndroidPlugins/Android_uc","Assets/Plugins/Android");
		}


		AssetDatabase.Refresh();
		
		// keystore
		Debug.Log ("$$");
		Debug.Log ("$$  keystoreName -> Assets/Android/kdtl.keystore");
		PlayerSettings.Android.keystoreName = "AndroidPlugins/kdtl.keystore";
		PlayerSettings.Android.keystorePass = "123qweQWE";
		PlayerSettings.Android.keyaliasName = "kdtl";
		PlayerSettings.Android.keyaliasPass = "123qweQWE";
	}
	
	private static string getApkPath(string channel){
		// APK/uc_tlbbydb_1.2.0.1_20131111_1013.apk
		string hour = System.DateTime.Now.Hour>9?""+System.DateTime.Now.Hour:"0"+System.DateTime.Now.Hour;
		string minute = System.DateTime.Now.Minute>9?""+System.DateTime.Now.Minute:"0"+System.DateTime.Now.Minute;
		return APK_FOLDER+currDay+"/"+channel+"_"+APK_NAME+"_"+AndroidConfig.GetVersionNumber()+"_"+currDay1+"_"+hour+""+minute+".apk";
	}
	
	/*
	 * 普通版png导入设置
	 * 取Png图片宽高的最大值，如果这个值是2次幂，证明这是一个atlas。只对atlas进行下面导入处理
	 * 在导入设置中把maxTextureSize设置为最大值的一半。
	 * textureFormat改成ATC_RGBA8格式。
	 * */
	public	static	bool HandlePngNormalRes(string assetPath)
	{
		UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Texture));
		if(obj != null)
		{
			UnityEngine.Texture tex = obj as UnityEngine.Texture;
			int OldMaxSize = 0;
			if(tex.height >= tex.width)
			{
				OldMaxSize = tex.height;
			}
			else
			{
				OldMaxSize = tex.width;
			}
			
			if(Mathf.IsPowerOfTwo(OldMaxSize))
			{
				//只有二次幂处理
				int	maxSize = (int)(OldMaxSize/2f);
				TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath( assetPath );
				if(textureImporter != null)
				{
					
					if(	textureImporter.textureFormat != TextureImporterFormat.ATC_RGBA8 ||
						textureImporter.maxTextureSize != maxSize
						)
					{
						textureImporter.maxTextureSize = maxSize;
						textureImporter.textureFormat = TextureImporterFormat.ATC_RGBA8;
						if(s_thisAssetNeedChange == false)
						{
							Debug.Log ("$$");
							s_thisAssetNeedChange = true;
						}
						Debug.Log ("$$  NormalRes PNG maxTextureSize from "+OldMaxSize+"=>"+maxSize+" textureFormat=ATC_RGBA8");
						AssetDatabase.ImportAsset( assetPath, ImportAssetOptions.ForceUpdate );
						EditorUtility.SetDirty(textureImporter);
						return true;
					}
				}
			}
		}
		return false;
	}
	
	/*
	 * 高清版图片导入规则
	 * 将textureFormat设置为ATC_RGBA8;
	 * */
	public	static	bool HandlePngHighRes(string assetPath)
	{
		UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Texture));
		if(obj != null)
		{
			UnityEngine.Texture tex = obj as UnityEngine.Texture;
			int OldMaxSize = 0;
			if(tex.height >= tex.width)
			{
				OldMaxSize = tex.height;
			}
			else
			{
				OldMaxSize = tex.width;
			}

			TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath( assetPath );
			if(textureImporter != null)
			{
				if(	textureImporter.textureFormat != TextureImporterFormat.ATC_RGBA8 ||
					textureImporter.maxTextureSize != OldMaxSize )
				{
					textureImporter.textureFormat = TextureImporterFormat.ATC_RGBA8;
					if(s_thisAssetNeedChange == false)
					{
						Debug.Log ("$$");
						s_thisAssetNeedChange = true;
					}
					Debug.Log ("$$  HighRes PNG textureFormat=>ATC_RGBA8");
					textureImporter.maxTextureSize = OldMaxSize;
					AssetDatabase.ImportAsset( assetPath, ImportAssetOptions.ForceUpdate );
					EditorUtility.SetDirty(textureImporter);
					return true;
				}
			}

		}
		return false;
	}

	
	/*
	 * 所有材质如果shader是Unlit/Premultiplied Colored，全改成Unlit/Transparent Colored
	 * */
	public	static	bool HandleMat(string assetPath)
	{
		bool ret = false;
		UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Material));
		if(obj != null)
		{
			UnityEngine.Material mat = (UnityEngine.Material)obj;
			if(mat.shader.name.Equals("Unlit/Premultiplied Colored"))
			{
				// 设置shader
				mat.shader = Shader.Find("Unlit/Transparent Colored");
				if(s_thisAssetNeedChange == false)
				{
					Debug.Log ("$$");
					s_thisAssetNeedChange = true;
				}
				Debug.Log ("$$  Unlit/Premultiplied Colored=>Unlit/Transparent Colored ");
				EditorUtility.SetDirty(obj);
				ret = true;
			}
		}
		return ret;
	}
	
	
	public	static	bool HandleNormalPrefab(string assetPath)
	{
		UnityEngine.GameObject obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.GameObject)) as UnityEngine.GameObject;
		if(obj != null)
		{
			bool needsave = false;
			
			if(HandleAtlas(obj))
			{
				needsave = true;
			}
					
			if( needsave )
			{
				EditorUtility.SetDirty(obj);
				return true;
			}
			else
			{
				return false;
			}
		}
		return false;
	}
	
	
	public	static	bool HandlesimheiFont(string assetPath)
	{
		UnityEngine.GameObject obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.GameObject)) as UnityEngine.GameObject;
		if(obj != null)
		{
			bool needsave = false;
			
			if(HandleFont(obj))
			{
				needsave = true;
			}
					
			if( needsave )
			{
				EditorUtility.SetDirty(obj);
				return true;
			}
			else
			{
				return false;
			}
		}
		return false;
	}
	
	public	static	bool HandlePrefab(string assetPath)
	{
		UnityEngine.GameObject obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.GameObject)) as UnityEngine.GameObject;

		if(obj != null)
		{
			bool needsave = false;
			if(HandlePanel(obj))
			{
				needsave = true;
			}
			
			if(HandleBoxCol(obj))
			{
				needsave = true;
			}
			
			if(HandleYScale(obj))
			{
				needsave = true;
			}
			
			if(HandleParticleSystem(obj))
			{
				needsave = true;
			}
			
			if( needsave )
			{
				EditorUtility.SetDirty(obj);
				return true;
			}
			else
			{
				return false;
			}
		}
		return false;
	}
	
	public	static	bool HandleSearchAnimation(string assetPath)
	{
		UnityEngine.GameObject obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.GameObject)) as UnityEngine.GameObject;

		if(obj != null)
		{
			bool needsave = false;
			tempAnimationPath = assetPath;
			if(HandleUISpriteAnimation(obj))
			{
				needsave = true;
			}
			if( needsave )
			{
				EditorUtility.SetDirty(obj);
				return true;
			}
			else
			{
				return false;
			}
		}
		return false;
	}
	
	/*
	 * 如果BoxCollid的Y大于960，一律调整到1280
	 * */
	public	static	bool HandleBoxCol(UnityEngine.GameObject obj)
	{
		bool ret = false;
		if(obj != null)
		{
			Component[] childcomponentls = obj.GetComponentsInChildren(typeof(BoxCollider), true);
			if(childcomponentls != null)
			{
				if(childcomponentls.Length>0)
				{
				    foreach (Component col in childcomponentls) 
					{
						BoxCollider box = col as BoxCollider;
						//高度大于960的一律改成1280
						if(box.size.y > 959f && box.size.y< 1280f)
						{
							if(s_thisAssetNeedChange == false)
							{	
								Debug.Log ("$$");
								s_thisAssetNeedChange = true;
							}
							Debug.Log ("$$  box.size.y="+box.size.y+" => y=1280f ");
							box.size = new Vector3(box.size.x, 1280f, box.size.z);
							ret = true;
						}
				    }
				}
			}
		}
		return ret;
	}
	
	/*
	 * 如果BoxCollid的Y大于960，一律调整到1280
	 * */
	public	static	bool HandlePanel(UnityEngine.GameObject obj)
	{
		bool ret = false;
		if(obj != null)
		{
			Component[] childcomponentls = obj.GetComponentsInChildren(typeof(UIPanel), true);
			if(childcomponentls != null)
			{
				if(childcomponentls.Length>0)
				{
				    foreach (Component co in childcomponentls) 
					{
						UIPanel panel = co as UIPanel;
						if(panel != null)
						{
							if(panel.depthPass == true)
							{
								if(s_thisAssetNeedChange == false)
								{
									Debug.Log ("$$");
									s_thisAssetNeedChange = true;
								}
								Debug.Log ("$$  panel.depthPass change to false ");
								panel.depthPass = false;
								ret = true;
							}
						}
				    }
				}
			}
		}
		return ret;
	}

	/*
	 * UIFont大小修改
	 * */
	public	static	bool HandleFont(UnityEngine.GameObject obj)
	{
		bool ret = false;
		if(obj != null)
		{
			Component[] childcomponentls = obj.GetComponentsInChildren(typeof(UIFont), true);
			if(childcomponentls != null)
			{
				if(childcomponentls.Length>0)
				{
				    foreach (Component co in childcomponentls) 
					{
						UIFont font = co as UIFont;
						if(font != null)
						{
							if(font.dynamicFontSize == 32)
							{
								if(s_thisAssetNeedChange == false)
								{
									Debug.Log ("$$");
									s_thisAssetNeedChange = true;
								}
								Debug.Log ("$$  font size change to 24 ");
								if(!font.isDynamic){
									Debug.LogError ("$$  font is not Dynamic ");
								}
								font.dynamicFontSize = 24;
								ret = true;
							}
						}
				    }
				}
			}
		}
		return ret;
	}

	/*
	 * 粒子中，renderer里面maxParticleSize 设为100
	 * */
	public	static	bool HandleParticleSystem(UnityEngine.GameObject obj)
	{
		bool ret = false;
		if(obj != null)
		{
			Component[] childcomponentls = obj.GetComponentsInChildren(typeof(ParticleSystem), true);
			if(childcomponentls != null)
			{
				if(childcomponentls.Length>0)
				{
				    foreach (Component co in childcomponentls) 
					{
						ParticleSystem  panel = co as ParticleSystem ;
						if(panel != null)
						{
							ParticleSystemRenderer[] renderers = panel.GetComponents<ParticleSystemRenderer>();
							foreach (ParticleSystemRenderer re in renderers) 
							{
								if(re.maxParticleSize < 100)
								{
									re.maxParticleSize = 100;
									ret = true;
									if(s_thisAssetNeedChange == false)
									{
										Debug.Log ("$$");
										s_thisAssetNeedChange = true;
									}
									Debug.Log ("$$  ParticleSystemRenderer maxParticleSize = 100 ");
								}
							}
						}
				    }
				}
			}
		}
		return ret;
	}
	
	/*
	 * Atlase坐标改成全部统一改成TexCoords
	 * */
	public	static	bool HandleAtlas(UnityEngine.GameObject obj)
	{
		bool ret = false;
		if(obj != null)
		{
			Component[] childcomponentls = obj.GetComponentsInChildren(typeof(UIAtlas), true);
			if(childcomponentls != null)
			{
				if(childcomponentls.Length>0)
				{
				    foreach (Component co in childcomponentls) 
					{
						UIAtlas  atlas = co as UIAtlas ;
						if(atlas != null)
						{
							if(atlas.coordinates == UIAtlas.Coordinates.Pixels)
							{
								if(s_thisAssetNeedChange == false)
								{
									Debug.Log ("$$");
									s_thisAssetNeedChange = true;
								}
								Debug.Log ("$$  atlas.mCoordinates == Coordinates.Pixels = >TexCoords ");
								atlas.coordinates = UIAtlas.Coordinates.TexCoords;
								ret = true;
							}
						}
				    }
				}
			}
		}
		return ret;
	}
	
	
	/*
	 * 如果Scaley>=1024 =>y=1280
	 * */
	public	static	bool HandleYScale(UnityEngine.GameObject obj)
	{
		string strCurPath = AssetDatabase.GetAssetPath(obj);

		bool ret = false;
		if(obj != null)
		{
			Component[] childcomponentls = obj.GetComponentsInChildren(typeof(Transform), true);
			if(childcomponentls != null)
			{
				if(childcomponentls.Length>0)
				{
				    foreach (Component co in childcomponentls) 
					{
						Transform tr = co as Transform;
						if(tr != null)
						{
							//y>=1024 =>y=1280
							if(tr.localScale.y> 1023f && tr.localScale.y<1280f)
							{
								if(s_thisAssetNeedChange == false)
								{
									Debug.Log ("$$");
									s_thisAssetNeedChange = true;
								}
								Debug.Log ("$$  localScale y="+tr.localScale.y+" y=1280f ");
								tr.localScale = new Vector3(tr.localScale.x, 1280f, tr.localScale.z);
								ret = true;
							}
						}
				    }
				}
			}
		}
		return ret;
	}


	public static bool HandleAssetbundleBackGround(string assetPath)
	{
		UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object)) as UnityEngine.Object;

		bool ret = false;
		if(obj != null)
		{
			string targetPath = Application.dataPath + "/StreamingAssets/BattleBackground/" + obj.name + ".assetbundle";
#if UNITY_ANDROID
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies,BuildTarget.Android)) {
#else
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies,BuildTarget.iPhone)) {
#endif
  				if(s_thisAssetNeedChange == false)
				{
					Debug.Log ("$$");
					s_thisAssetNeedChange = true;
				}
				Debug.Log ("$$  Assetbundle BackGround => "+obj.name);
				ret = true;
			} 
			else 
			{
 				Debug.Log ("$$  Assetbundle BackGround "+obj.name+" failed!");
			}
		}
		return ret;
	}

 	public static bool HandleAssetbundleBanshenxiang(string assetPath)
	{
		UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object)) as UnityEngine.Object;

		bool ret = false;
		if(obj != null)
		{
			string targetPath = Application.dataPath + "/StreamingAssets/banshenxiang/" + obj.name + ".assetbundle";
#if UNITY_ANDROID
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies,BuildTarget.Android)) {
#else
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies,BuildTarget.iPhone)) {
#endif
  				if(s_thisAssetNeedChange == false)
				{
					Debug.Log ("$$");
					s_thisAssetNeedChange = true;
				}
				Debug.Log ("$$  Assetbundle Banshenxiang => "+obj.name);
				ret = true;
			} 
			else 
			{
 				Debug.Log ("$$  Assetbundle Banshenxiang "+obj.name+" failed!");
			}
		}
		return ret;
	}

	public static bool HandleAssetbundleSounds(string assetPath)
	{
		UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object)) as UnityEngine.Object;

		bool ret = false;
		if(obj != null)
		{
			string targetPath = Application.dataPath + "/StreamingAssets/Sounds/" + obj.name + ".assetbundle";
#if UNITY_ANDROID
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies,BuildTarget.Android)) {
#else
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies,BuildTarget.iPhone)) {
#endif
  				if(s_thisAssetNeedChange == false)
				{
					Debug.Log ("$$");
					s_thisAssetNeedChange = true;
				}
				Debug.Log ("$$  Assetbundle Sounds => "+obj.name);
				ret = true;
			} 
			else 
			{
 				Debug.Log ("$$  Assetbundle Sounds "+obj.name+" failed!");
			}
		}
		return ret;
	}

	/*
	 * 查找Assets中的UISprite Animation，找到后，如果其绑定的GameObject的Scale为1和1，则显示出来
	 * */
	public	static	bool HandleUISpriteAnimation(UnityEngine.GameObject obj)
	{
		bool ret = false;
		if(obj != null)
		{
			Component[] childcomponentls = obj.GetComponentsInChildren(typeof(UISpriteAnimation), true);
			if(childcomponentls != null)
			{
				if(childcomponentls.Length>0)
				{
					string path = "";
				    foreach (Component col in childcomponentls) 
					{
						Vector3 localScale = col.transform.localScale;
						//缩放等于1
						if(localScale.x <= 1.1f && localScale.y <= 1.1f)
						{
							if(s_thisAssetNeedChange == false)
							{	
								Debug.Log ("$$");
								s_thisAssetNeedChange = true;
							}
							path = tempAnimationPath+"->"+col.gameObject.name;
							Debug.Log ("$$  UISprite Animation: "+path+" localScale do not set!");
							//path = tempAnimationPath+"->"+"[FFFF00]"+col.gameObject.name;
							RegisterUISpriteAnimation(path);
							ret = true;
						}
				    }
				}
			}
		}
		return ret;
	}

	/*
	
	[MenuItem("AutoEdit/Android Material")]
	static void AndroidMaterial()
	{
		Caching.CleanCache ();

        //获取在Project视图中选择的所有游戏对象
		string[] assetPaths = AssetDatabase.GetAllAssetPaths();
		
		foreach(string assetPath in assetPaths)
		{
			Debug.Log("-----assetPath: "+assetPath );

			if(!assetPath.EndsWith(".prefab"))// material的扩展名是.mat
				continue;
			//Debug.Log("-----assetPath: "+assetPath );
			UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.GameObject));
			if(obj != null)
			{
				
				 UnityEngine.Material mat = (UnityEngine.Material)obj;
				//Debug.Log("-----mat.shader.name: "+mat.shader.name);
				if(mat.shader.name.Equals("Unlit/Premultiplied Colored"))
				{
					// 设置shader
					mat.shader = Shader.Find("Unlit/Transparent Colored");
					//Debug.Log("-----to Transparent Colored: "+mat.name);
				}

				
			}
			new WaitForSeconds(0.1f);
		}
		//刷新编辑器
		AssetDatabase.Refresh ();	
	}
	*/
#endif
}
