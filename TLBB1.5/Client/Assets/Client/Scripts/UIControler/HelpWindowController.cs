using UnityEngine;
using System.Collections;
using card;
using card.net;
using Games.CharacterLogic;
using Games.LogicObject;

/* Create at 7/31/2013 by Jack Wen
 * */

public class HelpWindowController : MonoBehaviour {
	
	public GameObject mainUILogic;
	public GameObject giftButton;
	public GameObject ppBtn;
	public GameObject logOutBtn;
	public GameObject sysBtn;
	public GameObject facebookBtn;
	public GameObject bindBtn;
	
	//const string raidrul = "http://tlyxz.changyou.com/help/lwjh_help.html";
	static string raidrul = ClientConfigure.getHelpURL();
	
	void Awake(){
		mainUILogic = GameObject.Find("MainUILogic");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	
	void OnEnable()
	{
		if(Obj_MyselfPlayer.GetMe().giftison == 1)
		{
			giftButton.SetActive(true);
			facebookBtn.SetActive(true);
			
		}
		else
		{
			giftButton.SetActive(false);
			facebookBtn.SetActive(false);
			
			sysBtn.transform.localPosition = new Vector3(-15,-145,-1);
			bindBtn.transform.localPosition = new Vector3(-15,-230,-1);
			logOutBtn.transform.localPosition = new Vector3(-15,-310,-1);			
		}
		if(DeviceHelper.GetChannelID() == DeviceHelper.CHANNEL_PP)
		{
			sysBtn.transform.localPosition = new Vector3(-15,-145,-1);
			ppBtn.transform.localPosition = new Vector3(-15,-60,-1);
			ppBtn.SetActive(true);
			logOutBtn.transform.localPosition = new Vector3(-15,-230,-1);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnSystemSet(){
		//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn57).ToString());
		mainUILogic.SendMessage("OnSystemSet");
	}
	//王明磊 返回登录
	public void OnReturnToLogin() {
//		GameManager.Instance.LoadLevel(Utils.UI_NAME_Login);
		//清空session id
		HTTPClientAPI.cleanSessionId();
		//reset login当前菜单为splahcontroller
		LoginLogic.needResetLogin = true;
		MainUILogic.needResetLogin = true;
		//清除临时切换数据
		//update
		//Obj_MyselfPlayer.GetMe().updateHeroItem = null;
		//Obj_MyselfPlayer.GetMe().updateMaterialItems = new UserCardItem[6];
		//evolution
		Obj_MyselfPlayer.GetMe().evolutionHeroItem = null;
		Obj_MyselfPlayer.GetMe().evolutionMaterialItems = new UserCardItem[5];
		//strengthen
		Obj_MyselfPlayer.GetMe().strengthenHeroItem = null;
		
		Obj_MyselfPlayer.ReleaseMe();
		//清空按钮闪烁状态
        MainController.needFlashLunJian = false;
        MainController.needFlashWulin = false;
		//清空新手引导状态
		GuideManager.Instance.guideTimeOut();
		  //NetworkSender.Instance().Login(LoginDone, AccountManager.Instance.GetLoginAccountID());
		//回到主菜单
		if (AccountManager.userType != AccountManager.UserType.OldUser)
			PlayerPrefs.SetInt("InGameBackLogin",1); //标记玩家是否在游戏中返回登录
		GameManager.Instance.LoadLevel(Utils.UI_NAME_Login);
#if UNITY_ANDROID
		//AndroidConfig.directShowLoginUI();
		//返回登陆之后注销账户
		AndroidConfig.SetThirdLoginInfo("");
#endif
	}
	
	public void OnRaider(){
		//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn55).ToString());
		
#if UNITY_IPHONE
		KDTLWebView.ShowWebView(raidrul, KDTLWebView.webViewMode.RAIDER);
#elif UNITY_ANDROID
		KDTLWebView.ShowWebView(raidrul, KDTLWebView.webViewMode.RAIDER);
#else
		mainUILogic.SendMessage("OnRaider");
#endif
	}
	public void OnChangeName(){
		mainUILogic.SendMessage("OnChangeName");
	}
	public void OnBind(){
#if UNITY_ANDROID	
		if(!AndroidConfig.GetChannelID().Equals("android_googleplay"))
		{
			BoxManager.showMessage("暂不支持此功能",ClientConfigure.title);
			return;
		}
#endif
		AccountManager.Instance.initAccount();
		Obj_MyselfPlayer.GetMe().isPurchase=false;
#if UNITY_ANDROID
		if(!AndroidConfig.isLogin())
#else
		if(AccountManager.Instance.CurAccount == null)
#endif
		{
			mainUILogic.SendMessage("OnBindAccount");
			//AccountManager.Instance.ShowAccountBindUI();
			AccountManager.Instance.completeDalegate += OnBindComplete;
		}
		else
			mainUILogic.SendMessage("OnBindSuccess");
	}
	void OnBindComplete()
	{
#if UNITY_ANDROID
		if(AndroidConfig.isLogin())
#else
		if(AccountManager.Instance.CurAccount != null)
#endif
			mainUILogic.SendMessage("OnBindSuccess");
	}
	public void OnCard () 
    {
        PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn56).ToString());
        KDTLWebView.ShowWebView(DeviceHelper.GetNoticeUrl(), KDTLWebView.webViewMode.NOTICE);
		//王明磊 : 统计模块代码 -> Statistics
        //BoxManager.showMessage("功能暂未开放");
		//BoxManager.showMessageByID((int)MessageIdEnum.Msg18);
	}
	
	public void ReturnMain(){
		mainUILogic.SendMessage("ReturnToMainUI");
	}

    void OnFeedBack()
    {
//        DeviceHelper.OpenUMFeed();
    }

   	void OnGetGift()
    {
        GiftWindow.OpenWindow(this.gameObject);
    }
	
	public void OnPPBinding()
	{
		DeviceHelper.PPCenter();
	}
	
	//facebook
	public void OnFacebookLoveButtonClick(GameObject obj){
		BoxManager.showMessageByID((int)MessageIdEnum.Msg801);
		UIEventListener.Get(BoxManager.buttonYes).onClick += RequestFacebookUrl;
	}
	
	public void RequestFacebookUrl(GameObject obj){
		//test
		//GotoFacebookUrl(true);
		if(!PlayerPrefs.HasKey(Obj_MyselfPlayer.GetMe().accountID.ToString()+"_huodong_facebookLove")){
			PlayerPrefs.SetInt(Obj_MyselfPlayer.GetMe().accountID.ToString()+"_huodong_facebookLove",1);
			PlayerPrefs.Save();
			NetworkSender.Instance().RequestTaskOver(GotoFacebookUrl,1);
		}else{
			KDTLWebView.ShowWebView(ClientConfigure.getFacebookUrl());
		}
	}
	
	public void GotoFacebookUrl(bool success){
		if(success){
			Debug.Log("GotoFacebookUrl Net success");
		}else{
			Debug.Log("GotoFacebookUrl Net error");
		}
		KDTLWebView.ShowWebView(ClientConfigure.getFacebookUrl());	
	}
	
	//end facebook		
}
