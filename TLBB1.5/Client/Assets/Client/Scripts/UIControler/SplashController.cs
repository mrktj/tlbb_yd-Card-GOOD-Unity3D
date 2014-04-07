using UnityEngine;
using System.Collections;
using System.Collections.Generic;
////using Module.Log;
using GCGame.Table;
//using PBMessage;
using card;
using card.net;
using System;
using Games.CharacterLogic;


public class SplashController : MonoBehaviour {
	public string testIPstring="10.6.96.28";
	public List<ServerDetail> servers = new List<ServerDetail> ();
	public GameObject mainLogic;
	private bool isCancleBinding = false;
	private bool isUnopened = false;
	
	public UILabel labelVersion;
	
	public GameObject BeginBtn;

    public GameObject LoadingProgress;

    public GameObject LabelLoading;

    public GameObject SpriteLoading;

    public GameObject ButtonBack;

	public GameObject ServerListWindow;
	public GameObject ServerListGrid;
	public GameObject ChangeServerBtn;
	public UILabel SelectServerName;
	
	private string _randomServerName = null;
	private string _defaultServerName = null;
	private string _lastServerName = null;
	private string _selectServerAddress = null;
	private int serverType = 0;//0-default,1-last,2-error
	
#if UNITY_ANDROID
	public static string serverName = "";
#endif
	
	public void OnDisable()
	{
	}

    //|| UNITY_STANDALONE_WIN || UNITY_IPHONE
	/*
     void OnGUI()
 	{
//#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
// #if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
 		testIPstring =GUI.TextField(new Rect(10,10,110,30),testIPstring,15);
 		if(GUI.Button(new Rect(130,10,60,30),"Confirm"))
 		{
 			if(testIPstring!="")
 			{
 				serverType = -1;
				SetHttpUri("http://"+testIPstring+":8080/XJGameServer/s", 0);
 			}
 		}
 		if(GUI.Button(new Rect(10,110,200,60),"GM体验服"))
 		{
             //HTTPClientAPI.uri = new Uri("http://124.248.36.253:8081/XJGameServer/s");
			SetHttpUri("http://10.6.193.106:8080/XJGameServer/s", 0);
 			//HTTPClientAPI.uri = new Uri("http://124.248.42.21:8888/XJGameServer/s");
 			serverType = -1;
            //HTTPClientAPI.uri = new Uri("http://219.232.251.92:8080/XJGameServer/s");
 		}
		if(GUI.Button(new Rect(10,180,200,60),"QA_Server"))
 		{
 			//zhangwei's server
			SetHttpUri("http://10.6.153.3:8080/XJGameServer/s", 0);
 			serverType = -1;
 		}
		//
		if(GUI.Button(new Rect(10,250,200,60),"219.232.251.92_Server"))
 		{
			SetHttpUri("http://219.232.251.92:8081/XJGameServer/s", 0);
 			serverType = -1;
 		}
 		if(GUI.Button(new Rect(10,70,120,30),"Clean Account ID")){
 			Debug.Log("Cleaned Account ID");
 			PlayerPrefs.DeleteKey("ACCOUNT_ID");
			foreach (ServerDetail sd in servers)
			{
				if (PlayerPrefs.HasKey(sd.address + "_Guest"))
				{
					string accountID = PlayerPrefs.GetString(sd.address + "_Guest");
					if (PlayerPrefs.HasKey(accountID + "_LoginTimes"))
						PlayerPrefs.DeleteKey(accountID + "_LoginTimes");
					Debug.Log("sssr Clean Account ID DeleteKey" + sd.address + "_Guest");
					PlayerPrefs.DeleteKey(sd.address + "_Guest");
				}
			}
			if (PlayerPrefs.HasKey("http://10.6.193.106:8080/XJGameServer/s_Guest"))
			{
				string accountID = PlayerPrefs.GetString("http://10.6.193.106:8080/XJGameServer/s_Guest");
				if (PlayerPrefs.HasKey(accountID + "_LoginTimes"))
					PlayerPrefs.DeleteKey(accountID + "_LoginTimes");
				PlayerPrefs.DeleteKey("http://10.6.193.106:8080/XJGameServer/s_Guest");
			}
			if (PlayerPrefs.HasKey("http://10.6.153.3:8080/XJGameServer/s_Guest"))
			{
				string accountID = PlayerPrefs.GetString("http://10.6.153.3:8080/XJGameServer/s_Guest");
				if (PlayerPrefs.HasKey(accountID + "_LoginTimes"))
					PlayerPrefs.DeleteKey(accountID + "_LoginTimes");
				PlayerPrefs.DeleteKey("http://10.6.153.3:8080/XJGameServer/s_Guest");
			}
			
 			for (int btnNo = 1; btnNo <= 58; btnNo++)
 			{
 				string key = "Btn" + btnNo.ToString();
 				if (PlayerPrefs.HasKey(key)){
 					PlayerPrefs.DeleteKey(key);
 				}
 			}
 			if (PlayerPrefs.HasKey("LastAccountId"))
 				PlayerPrefs.DeleteKey("LastAccountId");
 
 			GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.NONE;
 			AccountManager.ClearAllAccount();
 		}
 		GUI.color = Color.black;
 		GUI.Label(new Rect(10,50,150,30),"MacAdd:"+GameManager.getMacAddress());
 //#endif
     }
     */
//--------------------------------选择服务器模块--------------------------------
	void serverListClicked(){
		//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn-2");
		if(ServerListWindow.activeSelf)
			ServerListWindow.SetActive(false);
		else
			ServerListWindow.SetActive(true);
	}
	
	void OnChangeServer (GameObject go){
		OnChangeServer(go.name);
	}
	void OnChangeServer (string serverName){
		serverType = -1;
		foreach(ServerDetail sd in servers){
			if (sd.name.Equals(serverName)){
				_selectServerAddress = sd.address;
				if (sd.state == ServerDetail.ServerState.Unopened)
					isUnopened = true;
				else
					isUnopened = false;
				SetHttpUri(sd.address, 0);
				break;
			}
		}
		SelectServerName.text = serverName;
	#if UNITY_ANDROID
		SplashController.serverName = serverName;
	#endif
		ServerListWindow.SetActive(false);
		Debug.Log("ServerName : "+serverName);
		Debug.Log("OnServerChange : "+HTTPClientAPI.uri.ToString());
	}
//------------------------------------------------------------------------------	
	void Start()
	{
		
		mainLogic = GameObject.Find("LoginLogic");
		AccountManager.Instance.ShowAccount();//在登录界面上方显示已登录的畅游账号--
		GameManager.Instance.sceneName = Utils.UI_NAME_Login;
		
		labelVersion.text = DeviceHelper.GetVersionNumber();//ClientConfigure.GetClientVersion();
		
		ServerListWindow.SetActive(false);
		
	}
	void OnEnable(){
#if UNITY_EDITOR
		GetComponent<ServerListDownloader>().enabled = true;
#elif UNITY_STANDALONE_WIN
		GetComponent<ServerListDownloader>().enabled = false;
#elif UNITY_IPHONE
		GetComponent<ServerListDownloader>().enabled = true;
#elif UNITY_ANDROID
		GetComponent<ServerListDownloader>().enabled = true;
#else
		ServerDetail sd = new ServerDetail();
    	sd.address = "http://127.0.0.1:8080/XJGameServer/s";
    	sd.name = "LocalHost";
   		sd.state = ServerDetail.ServerState.New;
    	servers.Add(sd);
#endif
	}

//--------------------------------登陆函数模块--------------------------------
	public void OnGameLogin()
	{
		//初始化userType
		if (!Obj_MyselfPlayer.ShowedPublicNotice){
			KDTLWebView.PreWebView(DeviceHelper.GetNoticeUrl());
		}
		switch(serverType){
		case 0://default
			OnChangeServer(_defaultServerName);
			break;
		case 1://last
			OnChangeServer(_lastServerName);
			break;
		default:
			//不进行操作
			break;
		}
		AccountManager.Instance.initGuestAccount();
		switch (AccountManager.userType)
		{
		case AccountManager.UserType.NewUser:
			Debug.Log("Is New User");
			break;
		case AccountManager.UserType.NotBinding:
			Debug.Log("Not Binding User");
			break;
		case AccountManager.UserType.OldUser:
			Debug.Log("Old User");
			break;
		default:
			Debug.Log("Unknown User Type");
			break;
		}
		
		if(FbHelper.UseFbLogin){
			if(FB.IsLoggedIn && !string.IsNullOrEmpty(FB.UserId) &&!string.IsNullOrEmpty(FB.AccessToken)){
				FacebookLoginCallBack();
				return ;
			}else{
				FbHelper.CallFBLogin(FacebookLoginCallBack);
				return ;
			}
		}
		Debug.Log("normal login");
		
#if UNITY_ANDROID	
		if(AndroidConfig.isUCChannel()||AndroidConfig.isThirdSDKPlatform())
		{
			//安卓非畅游官网渠道
			CYouLogin();
		}
		else
		{
			//安卓官网渠道
			if (AccountManager.userType != AccountManager.UserType.OldUser)
			{
				QuickLogin();
			}
			else
			{
				CYouLogin();
			}
		}
#else
		if(DeviceHelper.GetChannelID() == DeviceHelper.CHANNEL_PP){
			PPLogin();
		}
		else{
			if (AccountManager.userType != AccountManager.UserType.OldUser)
				QuickLogin();
			else
				CYouLogin();
		}
#endif
	}
	
	void FacebookLoginCallBack(){
		Debug.Log("fb OnGameLogin");
		string email = AccountInfo.Base64Encode(FB.UserId);
		NetworkSender.Instance().CyouLogin(OnGameLoginDone,
											-1,email,FB.AccessToken,
											PlayerPrefs.GetString("ACCOUNT_ID"));
		
	}
	
	void QuickLogin()
	{
		Debug.Log("QuickLogin");
		//判断是第一次进入游戏的游客还是未绑定的游客
		//未绑定的游客
		string accountIdStr = "";
		//未绑定的玩家第二次登陆将提醒绑定账号
		int loginCounts = 0;
		long localSaveAccountID = AccountManager.GuestAccountID;
		if (AccountManager.userType == AccountManager.UserType.NotBinding)
		{
			if (PlayerPrefs.HasKey(localSaveAccountID.ToString() + "_LoginTimes")) //本地保存登陆次数 无:首次进入游戏 1:第二次进入游戏 2:第二次以上进入游戏
			{
				loginCounts = PlayerPrefs.GetInt(localSaveAccountID.ToString() + "_LoginTimes");
//				if (AccountManager.Instance.IsInGame)
//				{
//					Debug.LogError("IS In Game");
//					PlayerPrefs.SetInt(localSaveAccountID.ToString() + "_LoginTimes",-1);
//				}
				Debug.Log("LoginCounts = " + loginCounts);
				if (!PlayerPrefs.HasKey("InGameBackLogin"))
				{
					if (loginCounts == 1 && !isCancleBinding)
					{
						AccountManager.Instance.ShowAccountBindUI();
						isCancleBinding = true;
						return;
					}
				}
			}
		}
		if (AccountManager.userType != AccountManager.UserType.NewUser) //未绑定用户登陆
		{
			accountIdStr = localSaveAccountID.ToString();
		}
		if (isUnopened){
			BoxManager.showMessage("服务器即将开启",ClientConfigure.title); //WML MARK
			return;
		}
				//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn-1");
		//-----------------清理用户数据---------------------
		string los=PlayerPrefs.GetString("LastServer","error");
		if(los != _selectServerAddress || 
			los == "error"){
			//Obj_MyselfPlayer.GetMe().ClearBattleArraySet();
			MainController.needFlashWulin = false;
		}
		NetworkSender.Instance().Login(OnGameLoginDone, accountIdStr,1);
		
//		NetworkSender.Instance().CyouLogin(ProvingPasswordRet, 1, "", "", PlayerPrefs.GetString("ACCOUNT_ID"));
				
		//----------------添加服务器记录---------------------
		//处理存储的url丢失80端口//
		PlayerPrefs.SetString("LastServer", _selectServerAddress);
	}
	
	void ShowLoginUI(GameObject button)
	{
		AccountManager.Instance.ShowLoginUI();
	}
	
	void PPLogin()
	{
		Debug.Log("+++++++PP login");
		if(string.IsNullOrEmpty(PPHelper.LoginTokenKey))
		{
			Debug.Log("+++++++getting token key");
			DeviceHelper.PPLogin();
		}
		else
		{
		    Debug.Log("+++++++accountid none varify");
			NetworkSender.Instance().CyouLogin(Ret_PPAccountVarify, -1, "", PPHelper.LoginTokenKey, PlayerPrefs.GetString("ACCOUNT_ID"));
		}
	}
	
	void Ret_PPAccountVarify(bool bSuccess)
	{
		// is need login rightnow?
		if(bSuccess)
		{
			switch(Obj_MyselfPlayer.GetMe().cyouCode)
			{
			
			case 1:
               
				BoxManager.showMessageByID((int)MessageIdEnum.Msg171);
                UIEventListener.Get(BoxManager.buttonYes).onClick += ReLogPP;
				break;
			default:
                OnGameLoginDone(true);				
				break;
			}
			
		}
	}

    void ReLogPP(GameObject button){
        Debug.Log("re login");
        DeviceHelper.PPLogin();
    }
	
	void CYouLogin()
	{
		Debug.Log("CyouLogin");
		if (isUnopened){
			BoxManager.showMessage("服务器即将开启",ClientConfigure.title); //WML MARK
			return;
		}
		//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn-1");
		//-----------------清理用户数据---------------------
		string los=PlayerPrefs.GetString("LastServer","error");
		if(los != _selectServerAddress || 
			los == "error"){
			//Obj_MyselfPlayer.GetMe().ClearBattleArraySet();
			MainController.needFlashWulin = false;
		}
		//--------------------登录-------------------------
#if UNITY_ANDROID
		if(AndroidConfig.showLoginUI()){
			return;
		}
		string accountID = AccountManager.Instance.GetLoginAccountID();
		if(accountID == "")
			accountID = "154";
		NetworkSender.Instance().Login(OnGameLoginDone, accountID,1);
#else
		if(DeviceHelper.GetChannelID() == DeviceHelper.CHANNEL_PP){
			NetworkSender.Instance().Login(OnGameLoginDone, Obj_MyselfPlayer.GetMe().accountID.ToString(),1);
		}
		else{
			ProvingPassword();
//			NetworkSender.Instance().Login(OnGameLoginDone, AccountManager.Instance.GetLoginAccountID(),1);
		}
#endif
		//----------------添加服务器记录---------------------
		//处理存储的url丢失80端口//
		PlayerPrefs.SetString("LastServer", _selectServerAddress);
	}
	
	public void ProvingPassword(){
		
		if(AccountManager.Instance.CurAccount == null){
			return;
		}
		
		string userName;
		string password;
		
		userName = AccountManager.Instance.CurAccount.email;
		password = AccountManager.Instance.CurAccount.password;
		NetworkSender.Instance().CyouLogin(ProvingPasswordRet, 1, userName, password, PlayerPrefs.GetString("ACCOUNT_ID"));
		Debug.LogWarning("ProvingPassword username = " + userName);
		Debug.LogWarning("ProvingPassword password = " + password);
		
	}
	
	public void ProvingPasswordRet(bool bSuccess){
		
		if(bSuccess)
		{
			Debug.LogWarning("ProvingPasswordRet code = " + Obj_MyselfPlayer.GetMe().cyouCode);
			switch(Obj_MyselfPlayer.GetMe().cyouCode){
				
			case 0:
				//Login success
				NetworkSender.Instance().Login(OnGameLoginDone, AccountManager.Instance.GetLoginAccountID(),1);
				break;		
				
			default:
				//Login error
				AccountManager.Instance.HideCurAccountUI();
				BoxManager.showMessage("您的账号信息已过期，请重新登录","登录失败");
				AccountManager.Instance.ShowLoginUI();
				break;
				
			}
		}
		
	}
	
	public void OnGameLoginDone(bool bSuccess){
		
		if (PlayerPrefs.HasKey("InGameBackLogin"))
			PlayerPrefs.DeleteKey("InGameBackLogin");
		Debug.Log("Login step - GuideManager.Instance.currentStep:"+GuideManager.Instance.currentStep);
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.NONE){
            
			//DeviceHelper.SendTrackLogin();
			
			//海外的登陆追踪;
			SDKManager.trackEvent("enterGame","");
			AccountManager.Instance.OnEnterGame();
			mainLogic.SendMessage("OnGuideWindow");
		}else{
			OnGetUserInfo();
		}
		
	}

//    public void OnPPCenter()
//    {
//        Debug.Log("On PP Center");
//		DeviceHelper.PPCenter();
//    }
	public void OnGetUserInfo(){
		NetworkSender.Instance().GetUserInfo(OnGetUserInfoDone);
	}
	
	public void OnGetUserInfoDone(bool bSuccess)
	{
		AccountManager.Instance.OnEnterGame();
		//在这里检测上一次玩家的战斗是否完成？-
		if(Obj_MyselfPlayer.GetMe().battleData.isLastBattle){
            switch (GuideManager.Instance.currentStep){
            case GuideManager.GUIDE_STEP.COPY2_1:
                GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.END; break;
			}		
			Obj_MyselfPlayer.GetMe().isLastBattleNotFinish = true;
			GameManager.Instance.LoadLevel(Utils.UI_NAME_Battle);
		}
		else{
			GameManager.Instance.LoadLevel(Utils.UI_NAME_main);
		}
	}
//------------------------------------------------------------------------------------------------------
	
	
//----------------------------------------服务器列表处理模块-----------------------------------
	//1-新服 2-流畅 3-爆满
	public void DownloadListFinish(){
		
		//return;
		
		Debug.Log("Set Server List <Count : " + servers.Count + " >");
		string lServer = PlayerPrefs.GetString("LastServer","null");//取得值为IP地址
		string lastServerName = "";
		string defaultServerName = ""; //存储最新的server name
		int maxIndex = 0;
		Obj_MyselfPlayer.GetMe().hadBigAddress = false;
		foreach (ServerDetail sd in servers){
			if(sd.index == 90001) //如果是苹果审核测试服,设置为true,并据此隐藏忘记密码按钮
				Obj_MyselfPlayer.GetMe().hadBigAddress = true;
			switch (sd.state){				
			case ServerDetail.ServerState.Closed:
				continue;
			case ServerDetail.ServerState.New:
				sd.name += "[029920][流畅][F1ECCF]";		break;
			case ServerDetail.ServerState.Busy:
				sd.name += "[F48826][爆满][F1ECCF]";		break;
			case ServerDetail.ServerState.Open:
				sd.name += "[A5E295][新服][F1ECCF]";		break;
			case ServerDetail.ServerState.Unopened:
				sd.name += "[F8DF44][维护][F1ECCF]";		break;
			default:
				break;
			}
			if (sd.index > maxIndex){
				maxIndex = sd.index;
				defaultServerName = sd.name;
			}
#if UNITY_ANDROID
			//解决更新进度条界面卡死，刷不出服务器列表问题//
			string resS = sd.address.Replace(":80/","/");
			string desS = lServer.Replace(":80/","/");
			if (!string.IsNullOrEmpty(lServer) && resS.CompareTo(desS) == 0)
#else
			if (!string.IsNullOrEmpty(lServer) && sd.address.CompareTo(lServer) == 0)
#endif
				lastServerName = sd.name;
		}
		if (!string.IsNullOrEmpty(lastServerName)){
			_lastServerName = lastServerName;
			OnChangeServer(lastServerName);
			//serverList.selection = lastServerName;
			serverType = 1;
		}
		else{			
			_defaultServerName = defaultServerName;
			OnChangeServer(defaultServerName);
			serverType = 0;
		}
		AddServerToServerListWindow();
		//RandomServer();//随机取CYou login服务器
		//SetHttpUri(_randomServerName , 0);
		//OnChangeServer(_randomServerName);
	}
	
	public void AddServerToServerListWindow(){
		foreach(ServerDetail sd in servers){
			if(sd.state != 0){
				GameObject newItem =  ResourceManager.Instance.loadWidget("ServerListItem");
				newItem.transform.parent = ServerListGrid.transform;
				newItem.transform.localPosition = new Vector3(0, 0, -1);
				newItem.transform.localScale = new Vector3(1f, 1f, 1f);			
				newItem.name = sd.name;
				
				//changeserver function
				UIEventListener.Get(newItem).onClick += OnChangeServer;
				
				UILabel nameLabel = newItem.transform.FindChild("Label").GetComponent<UILabel>();
                nameLabel.transform.localScale = new Vector3(28, 28, 1);
		       	nameLabel.text = sd.name;
			}
		}
		ServerListGrid.GetComponent<UIGrid>().repositionNow = true;
	}
	
	public void UIForLoadServer(bool loadServer){

		if(loadServer){
			BeginBtn.SetActive(false);
			ChangeServerBtn.SetActive(false);
			AccountManager.Instance.HideCurAccountUI();
			AccountManager.Instance.DestroyAllUI();
            LoadingProgress.SetActive(true);
            LabelLoading.SetActive(true);
            SpriteLoading.SetActive(true);
            if(ButtonBack != null)
            {
	            ButtonBack.SetActive(false);
	        }
		}
		else{
			BeginBtn.SetActive(true);
			ChangeServerBtn.SetActive(true);
			AccountManager.Instance.ShowCurAccountUI(false);
			
            LoadingProgress.SetActive(false);
            LabelLoading.SetActive(false);
            SpriteLoading.SetActive(false);
            if(ButtonBack != null)
            {
	            ButtonBack.SetActive(true);
	        }
		}
	}
	
	public void SetHttpUri(string name ,int type)//type:0-address,1-name
	{
		switch(type){
		case 0:
			HTTPClientAPI.uri = new Uri(name);
			Debug.LogWarning("*** Set Http Uri : "+name);
			break;
		case 1:
			foreach(ServerDetail sd in servers){
				if (sd.name.Equals(name)){
					HTTPClientAPI.uri = new Uri(sd.address);
					break;
				}
			}
			break;
		}
	}
//----------------------------------------------------------------------------------------------------
	
//----------------------------------服务器随机选择模块---------------------------------------
	public string randomServerAddress{
		get{
			return _randomServerName;
		}
		set{
			_randomServerName = value;
		}
	}
	//服务器随机选择函数
	public void RandomServer(){
		int maxServerCount = servers.Count;
		int temp_server_count = 0;
		foreach(ServerDetail sd in servers){
			if(sd.state != ServerDetail.ServerState.Closed && 
				sd.state != ServerDetail.ServerState.Unopened)//未开启状态
				temp_server_count++;
		}
		if(maxServerCount == 1){
			_randomServerName = servers[0].address;
#if UNITY_ANDROID
		serverName = servers[0].name;
#endif
			return;
		}
		else if(temp_server_count == 1){
			foreach(ServerDetail sd in servers){
				if(sd.state != ServerDetail.ServerState.Closed && 
					sd.state != ServerDetail.ServerState.Unopened){//未开启状态
					_randomServerName = sd.address;
#if UNITY_ANDROID
					serverName = sd.name;
#endif
					return;
				}
			}
		}
		else if(maxServerCount < 1 ||
			temp_server_count < 1){
			_randomServerName = servers[0].address;
#if UNITY_ANDROID
					serverName = servers[0].name;
#endif
			//服务器列表无可用元素处理
			return;
		}
		
		System.Random ra = new System.Random();
		int randomServerIndex = ra.Next(1 ,temp_server_count);
		int tempId=0;
		for(int i=0 ; i<maxServerCount ; i++){
			if(servers[i].state != ServerDetail.ServerState.Closed && 
				servers[i].state != ServerDetail.ServerState.Unopened){
				tempId++;
			}
			if(tempId == randomServerIndex){
				_randomServerName = servers[i].address;
			#if UNITY_ANDROID
				serverName = servers[i].name;
			#endif
				return;
			}
		}
	}
	//服务器轮选函数
	public void SelectAnotherServer(){
		AnotherRandomServer();
		SetHttpUri(_randomServerName ,0);
	}
	public void AnotherRandomServer(){
		int maxServerCount = servers.Count;
		int temp_server_count = 0;
		foreach(ServerDetail sd in servers){
			if(sd.state != ServerDetail.ServerState.Closed && 
				sd.state != ServerDetail.ServerState.Unopened)//未开启状态
				temp_server_count++;
		}
		if(maxServerCount == 1){
			_randomServerName = servers[0].address;
		#if UNITY_ANDROID
			serverName = servers[0].name;
		#endif
			return;
		}
		else if(temp_server_count == 1){
			foreach(ServerDetail sd in servers){
				if(sd.state != ServerDetail.ServerState.Closed && 
					sd.state != ServerDetail.ServerState.Unopened){//未开启状态
					_randomServerName = sd.address;
					return;
				}
			}
		}
		else if(maxServerCount < 1 ||
			temp_server_count < 1){
			BoxManager.showMessage("服务器维护中，请稍后",ClientConfigure.title);
			//服务器列表无可用元素处理
			return;
		}
		
		int serverNum = 0;//[0]起始数组
		//第一次循环，定位默认服务器位置
		foreach(ServerDetail sd in servers){
			if(! _randomServerName.Equals(sd.address))
				serverNum++;
			else 
				break;
		}
		
		//重新定位服务器
		//默认服务器为列表最后一个,则从列表最开始进行进行轮询
		if(serverNum == maxServerCount-1){
			for(int i=0; i<maxServerCount; i++){
				if(servers[i].state != ServerDetail.ServerState.Closed && 
					servers[i].state != ServerDetail.ServerState.Unopened){
					_randomServerName = servers[i].address;
				#if UNITY_ANDROID
					serverName = servers[i].name;
				#endif
					return;
				}
			}
		}
		
		//1.从列表当前位置开始进行进行轮询
		for(int i=serverNum+1; i<maxServerCount; i++){
			if(servers[i].state != ServerDetail.ServerState.Closed && 
				servers[i].state != ServerDetail.ServerState.Unopened){
				_randomServerName = servers[i].address;
			#if UNITY_ANDROID
				serverName = servers[i].name;
			#endif
				return;
			}
		}
		//2.未搜到则从头开始未轮询段进行
		for(int i=0; i<=serverNum; i++){
			if(servers[i].state != ServerDetail.ServerState.Closed && 
				servers[i].state != ServerDetail.ServerState.Unopened){
				_randomServerName = servers[i].address;
			#if UNITY_ANDROID
				serverName = servers[i].name;
			#endif
				return;
			}
		}
	}
//---------------------------------------------------------------------------------------------------
	
}