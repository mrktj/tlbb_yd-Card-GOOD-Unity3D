//download and update server list
using UnityEngine;   
using System.Collections;   
using System.Collections.Generic;   
using System.IO;   
using System;
using GCGame.Table;

public class ServerListDownloader: MonoBehaviour
{   
	//TODO:read from config.h
	private string url = DeviceHelper.GetServerListUrl ();
	WWW www;
	private string serverList = "";
	private bool m_bLoadingTable = false;
	public static bool IsUpdateRes = true;   // 如果想在非编辑器下测试更新，将此置true
	private static float LoadDataTimer = 30.0f;     // 下载计时，如果时间过长，强制停止更新，加载本地资源
	private static float TIMEOUTTIMER = 10.0f;
	//private static float m_fakeBar = 0;
	private static bool IsBeginDownLoad = false;

	void Start ()
	{
		if (string.IsNullOrEmpty (url)) {
			Debug.Log ("Can't GetServerListUrl from DeviceHelper.mm [if Win32 or UnityEditor]");
			//"http://10.6.193.223:8080/deployTools/servers.txt";//"http://124.248.36.247:8080/servers.txt";//"http://files2.changyou.com/tlydios/servers.txt";
			url = "http://10.6.193.106:8080/servers.txt";
		}
		
        //调试
//		this.GetComponent<SplashController> ().UIForLoadServer (false);

        //发布
        this.GetComponent<SplashController>().UIForLoadServer(true);
		
        //LoadDataFromServer();
		StartCoroutine ("RequestServerList");
		
		string networkState = DeviceHelper.GetNetworkState();
		Debug.Log("GetNetworkState:************"+networkState);
		
		if(networkState.Equals("disconnect")
			||networkState.Equals("unknown")){
			
			DeviceHelper.ShowExitDialog();
		}
	}

	void Reconnect (GameObject obj)
	{
		StartCoroutine ("RequestServerList");
	}
	
	ResourceDownloader downloader;
	void LoadDataFromServer ()
	{
#if UNITY_ANDROID
		string resourceFilePath = Path.Combine(ResourceManager.PathURL.Replace("file://",""),ClientConfigure.resourceTxtName);
		if(File.Exists(resourceFilePath)){
			StartCoroutine ("RequestServerList");
		}else{
			downloader = gameObject.AddComponent<ResourceDownloader>();
			if(downloader){
				downloader.StartCoroutine(downloader.LoadAndSaveResources());
				m_bLoadingTable = true;
			}else{
				StartCoroutine ("RequestServerList");
			}
		}
#elif UNITY_IPHONE
		StartCoroutine ("RequestServerList");
#else
		StartCoroutine ("RequestServerList");
#endif
		
	}
	
	
	void SetFileNoBackupFlag (FileSystemInfo info)
	{
		if (!info.Exists)
			return;

		DirectoryInfo dir = info as DirectoryInfo;
		//不是目录 
		if (dir == null)
			return;
#if UNITY_IPHONE
        iPhone.SetNoBackupFlag(dir.FullName); //设置"无备份"标志。
#endif
		FileSystemInfo[] files = dir.GetFileSystemInfos ();
		for (int i = 0; i < files.Length; i++) {
			FileInfo file = files [i] as FileInfo;
			//是文件 
			if (file != null) {
#if UNITY_IPHONE
                iPhone.SetNoBackupFlag(file.FullName);
#endif
			}
            //对于子目录，进行递归调用 else
				SetFileNoBackupFlag (files [i]);

		} 
	}

	void SetSliderValue (float value)
	{
		UISlider processBar = this.GetComponent<SplashController> ().LoadingProgress.GetComponent<UISlider> ();
		UISprite sprite = this.GetComponent<SplashController> ().SpriteLoading.GetComponent<UISprite> ();
		Vector3 pos = sprite.transform.localPosition;
		float sizeX = processBar.transform.FindChild ("Foreground").gameObject.transform.localScale.x;
		pos.Set ((float)(-0.5 * sizeX + sizeX * value), pos.y, pos.z);
		sprite.gameObject.transform.localPosition = pos;
		processBar.sliderValue = value;
	}

	void SetLabelText (int msgID)
	{
		Tab_Popup msg = TableManager.GetPopupByID (msgID);
		if (msg == null) {
			Debug.LogError ("Msg is Null ID: " + msgID);
			return;
		}
		this.GetComponent<SplashController> ().LabelLoading.GetComponent<UILabel> ().text = msg.Content;
	}

	void Update ()
	{
		
		if(www != null){
			SetSliderValue (www.progress);
		}
		
		
		/*
		if(downloader != null){
			SetSliderValue (downloader.DownLoadPercent);
			if(downloader.DownLoadOver && m_bLoadingTable){
				m_bLoadingTable = false;
				StartCoroutine ("RequestServerList");
			}
		}else{
			if(www != null){
				SetSliderValue (www.progress);
			}
		}
		*/
		
	}

	void ForceFinishUpdate ()
	{
		ResUpdate.ExitUpdate ();
		m_bLoadingTable = false;
		TableManager.IsLoadFromLocal = true;
		SetSliderValue (0.95f);
		StartCoroutine ("RequestServerList");
	}
	
	IEnumerator RequestServerList ()
	{
		List<ServerDetail> servers = GetComponent<SplashController> ().servers;
		Debug.Log ("Updating Server List...");
		
		www = new WWW (url);   

		yield return www;   

		if (!string.IsNullOrEmpty (www.error)) {
			Debug.LogError (www.error);
		} else {
			serverList = www.text;
		}
		string[] alldataRow = serverList.Split ('\n');
		
		foreach (string line in alldataRow) {
			if (String.IsNullOrEmpty (line))
				continue;
			string[] strCol = line.Split ('\t');
			if (strCol.Length == 0)
				continue;
			string skey = strCol [0];
			if (string.IsNullOrEmpty (skey))
				yield return 0;
			ServerDetail sd = new ServerDetail (strCol);
			servers.Add (sd);
		}
		if (servers.Count > 0) {
			this.GetComponent<SplashController> ().LoadingProgress.GetComponent<UISlider> ().sliderValue = 1.0f;
			BoxManager.removeMessage ();
			this.GetComponent<SplashController> ().DownloadListFinish ();
			this.GetComponent<SplashController> ().UIForLoadServer (false);
			this.enabled = false;
			//use updated server
		} else {
//			BoxManager.showMessage("获取服务器列表失败,请重试",ClientConfigure.title);
			BoxManager.showMessageByID ((int)MessageIdEnum.Msg145);
			UIEventListener.Get (BoxManager.buttonYes).onClick += Reconnect;
			//generator server list failed; use local server list
		}
	}
#if UNITY_ANDROID
	private WWW wwwAssets;
	private string assetsPath;
    IEnumerator LoadAssetsWWW()
    {  
    	wwwAssets = new WWW(assetsPath);
    	yield return wwwAssets;
    }
#endif
}


/**
   // 当前事件
	_ResupdateExport	 int GetPushEventType();
	
	MSG_INVALID = -1,     // 目前没有事件
	MSG_NETERROR = 10001 ,			//!< 网络连接异常
	MSG_NEWAPP,				//!< 程序更新啦
	MSG_NOSDCARD,			//!< 没有SD卡
	MSG_SPACENOTENOUGH,		//!< 内存满了
	MSG_NOTDEFFINEERROR,	//!< 未定义错误
	MSG_TOTALNUM,			//!< 无用信息 表示有多少条提示
	MSG_UPDATE_APP,    // 需要更新程序
	MSG_UPDATE_RES,      // 需要更新必要资源
	MSG_UPDATING,          //  资源更新中
	MSG_UPDATE_FINNISH, // 资源更新完成
	MSG_VERSIONERROR,		//!< 版本号错误
	MSG_DOWN_MD5_ERR, // 下载Md5.ru错误
	MSG_READ_JSON_ERR, // 无法读取json对象
	MSG_JSON_FORMAT_ERR, // 读取json的key失败
	MSG_PARSE_HTTP_ERR, // 解析http头出错
	MSG_CHECK_MD5_ERR, // check下载的MD5出错
	MSG_COPY_UPDATE_ERR, // 拷贝update.ru失败
	MSG_NOT_UPDATE,  // 不需要更新
	MSG_CANNOT_CONNECT_SERVER, // 无法连接到更新服务器,放弃连接
	MSG_RECONNECT_SERVER, // 正在尝试重连更新服务器
	MSG_UPADTE_EXIST,     // 已经有一个update程序在运行
	MSG_DOWN_LIST_ERR, // 下载之前计算需要下载的文件总数出错
	MSG_UPDATE_UN_RES, // 需要更新非必要资源
	MSG_UPDATE_ALL_RES, // 需要更新必要资源和非必要资源

**/