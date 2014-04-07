using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
using card.net;

public class CyouAccounts : MonoBehaviour {
	
	public UILabel userName;
	public UIButton accountBtn;
	public GameObject loginBtn;
	public GameObject logoutBtn;
	
	// Use this for initialization
	void Start () {
#if UNITY_ANDROID
		if(AndroidConfig.isDuokuChannel()){
			// 多酷渠道将注销按钮和登录按钮放大
			if(logoutBtn != null){
				Vector3 localScale = logoutBtn.transform.localScale;
				localScale.x*=1.3f;
				localScale.y*=1.3f;
				logoutBtn.transform.localScale = localScale;
				Vector3 localPosition = logoutBtn.transform.localPosition;
				localPosition.y=-32;
				logoutBtn.transform.localPosition = localPosition;
			}
			if(loginBtn != null){
				Vector3 localScale = loginBtn.transform.localScale;
				localScale.x*=1.3f;
				localScale.y*=1.3f;
				loginBtn.transform.localScale = localScale;
				Vector3 localPosition = loginBtn.transform.localPosition;
				localPosition.y=-32;
				loginBtn.transform.localPosition = localPosition;
			}
		}
		//小米渠道隐藏注销按钮  lihao_yd 2013-12-12
		//if(AndroidConfig.isXiaoMiChannel()){
			//Debug.Log("----XiaoMiChannel-hide-logoutBtn");
			//loginBtn.gameObject.SetActive(false);
			//logoutBtn.gameObject.SetActive(false);
		//}
		
		if( AndroidConfig.isUCChannel() == false && AndroidConfig.isThirdSDKPlatform() == false )
		{
			Debug.Log("Start-channelLogin");
			SwitchRefresh();
		}else{
			accountBtn.gameObject.SetActive(false);
			if(!AndroidConfig.isLogin()){
				loginBtn.gameObject.SetActive(true);
				logoutBtn.gameObject.SetActive(false);
			}
		}
#else
	SwitchRefresh();
#endif
	}
	
	// Update is called once per frame
	public float infoTime = 0.0f;
	void Update () {
#if UNITY_ANDROID
		//360的code60秒过期，清空登陆数据
		if(AndroidConfig.is360Channel()){
			infoTime += Time.deltaTime;
			//Debug.Log( "CYouAccountManager::Update() " + infoTime );
       	    if (infoTime > 60)
       	    { 
               infoTime = 0;
				AndroidConfig.SetThirdLoginInfo("");
            }
		}
		if(AndroidConfig.isSogouChannel()){
			PaySystemInterface.doSdk("doSogouListen","doSogouListen");
		}
#endif
	}
	
	void OnEnable()
	{
#if UNITY_ANDROID
#else
		SwitchRefresh();
#endif

	}
	
	public void Refresh()
	{
#if UNITY_ANDROID
		if(AndroidConfig.isLogin())
#else
		if(AccountManager.Instance.CurAccount != null && !string.IsNullOrEmpty(AccountManager.Instance.CurAccount.email))
#endif
		{
			loginBtn.gameObject.SetActive(false);
			//小米渠道隐藏注销按钮  lihao_yd  2013-12-12
#if UNITY_ANDROID
		    if(AndroidConfig.isXiaoMiChannel()||AndroidConfig.isSogouChannel()){
				Debug.Log("----Refresh-UI-xiaomi-hide-logoutBtn");
				logoutBtn.gameObject.SetActive(false);
			}else{
				logoutBtn.gameObject.SetActive(true);
			}
#else
		    logoutBtn.gameObject.SetActive(true);
#endif
			accountBtn.gameObject.SetActive(true);

#if UNITY_ANDROID
			if(hideAccountBtn()){
				return;
			}
#endif
			string email = AccountManager.Instance.CurAccount.email;
			email =	AccountInfo.Base64Decode(email);
			
			userName.text = email;
		}
		else
		{
			loginBtn.gameObject.SetActive(true);

#if UNITY_ANDROID
			AndroidConfig.channelLogin();
			showLoginBtn();
#endif
			logoutBtn.gameObject.SetActive(false);
			accountBtn.gameObject.SetActive(false);
		}
	}
	public void ShowCurAccount()
	{
		SwitchRefresh();
	}
	public void OnLoginBtn()
	{
		Debug.Log("----OnLoginBtn");
		if (PlayerPrefs.HasKey(HTTPClientAPI.uri.ToString() + "_Guest"))
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg804);
			UIEventListener.Get(BoxManager.buttonYes).onClick += ShowBindUI;
			UIEventListener.Get(BoxManager.buttonNo).onClick += ShowLoginAndRegUI;
		}else{
			ShowLoginAndRegUI(null);
		}
		
		
		
	}
	
	public void ShowLoginAndRegUI(GameObject go){
		Debug.Log("----ShowLoginAndRegUI");
		AccountManager.Instance.ShowLoginUI();
	}
	
	public void ShowBindUI(GameObject go){
		Debug.Log("----ShowLoginAndRegUI");
		AccountManager.Instance.ShowAccountBindUI();
	}
	
	
	
	public void OnLogoutBtn()
	{
		//清空session id
		HTTPClientAPI.cleanSessionId();		
		
		AccountManager.Instance.CurAccount = null;
		Obj_MyselfPlayer.GetMe().ClearBattleArraySet();
		MainController.needFlashWulin = false;
		
		//注销fb信息;
		FbHelper.CallFBLogout();
#if UNITY_ANDROID
		if(channelLogout())
		{
			return;
		}
#endif
		SwitchRefresh();
	}
	public void OnAccountMagagerBtn()
	{
		gameObject.SetActive(false);
		AccountManager.Instance.ShowAccountManageUI();
	}
	
	public void SwitchRefresh(){
		Refresh();
		//Refresh2();
	}
	//新版本：强制用户登录版使用函数
	public void Refresh2(){
		loginBtn.gameObject.SetActive(false);
		Debug.Log("***		Jack Wen		***");
#if UNITY_ANDROID
		if(AndroidConfig.isLogin())
#else
		if(AccountManager.Instance.CurAccount != null)
#endif
		{
			//loginBtn.gameObject.SetActive(false);
			AccountManager.Instance.HideLoginUI();
			
			//小米渠道隐藏注销按钮  lihao_yd  2013-12-12
#if UNITY_ANDROID
            if(AndroidConfig.isXiaoMiChannel()||AndroidConfig.isSogouChannel()){
				Debug.Log("----Refresh-UI-xiaomi-hide-logoutBtn");
				logoutBtn.gameObject.SetActive(false);
			}else{
				logoutBtn.gameObject.SetActive(true);
			}
#else
		    logoutBtn.gameObject.SetActive(true);
#endif
			accountBtn.gameObject.SetActive(true);
#if UNITY_ANDROID
			if(hideAccountBtn()){
				return;
			}
#endif
			string email = AccountManager.Instance.CurAccount.email;
			email =	AccountInfo.Base64Decode(email);

			userName.text = email;
			
			Debug.Log("userName: "+email);
		}
		else
		{
			//loginBtn.gameObject.SetActive(true);//必须登录版更新
			AccountManager.Instance.ShowLoginUI();//必须登录版更新
#if UNITY_ANDROID
			showLoginBtn();
#endif
			logoutBtn.gameObject.SetActive(false);
			accountBtn.gameObject.SetActive(false);
			
			Debug.Log("userName: NULL");
		}
		
		Debug.Log("***		End		***");
	}
	
#if UNITY_ANDROID
	// 只用于第三方
	public bool channelLogout(){
		//整合UC渠道代码 lihao_yd 2013-11-25
		/*
		if(AndroidConfig.isUCChannel()){
			UCGameSdk.logout();
			return true;
		}else 
		*/
		if(AndroidConfig.isUCChannel() || AndroidConfig.isThirdSDKPlatform()){
			AndroidConfig.SetThirdLoginInfo("");
			loginBtn.gameObject.SetActive(true);
			logoutBtn.gameObject.SetActive(false);
			PaySystemInterface.doSdk("doLogout",null);
			/*GameObject accountsUI = AccountManager.Instance.GetAccountsUI();
			if(accountsUI != null)
			{
				if(!accountsUI.gameObject.activeSelf)
					accountsUI.SetActive(true);
				accountsUI.GetComponent<CyouAccounts>().SwitchRefresh();
			}*/
			return true;
		}
		return false;
	}
	// 只有第三方用
	private bool hideAccountBtn(){
		if(AndroidConfig.isUCChannel()||AndroidConfig.isThirdSDKPlatform()){
			accountBtn.gameObject.SetActive(false);
			return true;
		}
		return false;
	}
	// 只有第三方用
	private void showLoginBtn(){
		if(AndroidConfig.isUCChannel()||AndroidConfig.isThirdSDKPlatform()){
			loginBtn.gameObject.SetActive(true);
		}
	}
#endif
}
