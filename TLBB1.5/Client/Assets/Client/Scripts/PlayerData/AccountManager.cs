using System;
using System.Collections.Generic;
using UnityEngine;
using card.net;

using Games.CharacterLogic;


public class AccountManager : MonoBehaviour {
	
	public enum UserType
	{
		NewUser,
		NotBinding,
		OldUser
	};
	public static long GuestAccountID = -1;
	private static AccountManager _instance;
	
	public static bool IsUseAccount = true;
	public static UserType userType = UserType.OldUser; //新用户 新用户未绑定 已绑定用户
	//public static string forgetPasswordURL = "http://member.changyou.com/common/codeAdmin.do";
	public static string forgetPasswordURL = ClientConfigure.getForgetPasswordURL();
	
	public static string userAgreementURL =  ClientConfigure.getUserAgreementURL();
	//public static string userAgreementURL =  "http://member.changyou.com/inc/useragreement.html";
#if UNITY_ANDROID
	public static string registerOther = "http://member.changyou.com/webphoregist/phone_regist.html?gametype=undefined&order=spe";
#endif
	private static string AccountIdKey = "AccountKey_Id_";
	private static string AccountEmailKey = "AccountKey_Email_";
	private static string AccountPasswordKey = "AccountKey_Password_";
	
	public delegate void BindCompleteDelegate();
	public BindCompleteDelegate completeDalegate;
	
	private AccountInfo mVisitorAccount;
	private AccountInfo mCurAccount;
	private List<AccountInfo> mAccountList;
	private bool mIsInGame = false;
	
	private GameObject loginUI;
	private GameObject manageUI;
	private GameObject registerUI;
	private GameObject accountsUI;
	private GameObject bindUI;
	private GameObject mRootPanel;
	
	private bool isAccountLogin=false;
	
	public enum WindowType{
		NONE,
		LOGIN,
		REGISTER,
		MANAGER,
		BIND,
	}
	public WindowType lastWindow = WindowType.NONE;
	
	public bool IsInGame { set { mIsInGame = value; } get { return mIsInGame; } }
	public AccountInfo VisitorAccount 
	{ 
		set 
		{
			mVisitorAccount = value;
		} 
		get
		{
			return mVisitorAccount;
		}
	}
	
	public AccountInfo CurAccount
	{
		set
		{
			mCurAccount = value;
			if(mCurAccount != null)
			{
				SaveCurAccount();
			}
			else
			{
				DeleteCurAccount();
			}
		}
		get
		{
			return mCurAccount;
		}
	}
#if UNITY_ANDROID
	public List<AccountInfo> AccountList { 
		get 
		{ 
			if(mAccountList == null)
			{
				mAccountList = new List<AccountInfo>();
			}
			return mAccountList;
		} 
	}
#else
	public List<AccountInfo> AccountList { get { return mAccountList;} }
#endif
	
	public static AccountManager Instance
	{
		get
		{
			if(!_instance)
			{
				GameObject go = GameObject.Find("AccountManager");//new GameObject("AccountManager");
				if(go != null)
				{
					_instance = go.GetComponent<AccountManager>();
				}
			}
			if(!_instance)
			{
				GameObject go = new GameObject("AccountManager");
				_instance = go.AddComponent(typeof(AccountManager)) as AccountManager;
			}
			return _instance;			
		}
	}
	
	void Awake(){
		
	}
	// Use this for initialization
	void Start () {
		mRootPanel = GameObject.Find("Camera/Anchor/Panel");
		if(mRootPanel == null)
		{
			Debug.LogError("Cant find Camera/Anchor/Panel");
		}
	}
	
	//点击进入按钮时进行的操作 
	public void initGuestAccount()
	{
		if (CurAccount != null) //如果有当前登录 绑定过的账号,不初始化游客信息
		{
			userType = UserType.OldUser;
			return;
		}
		string curServer = HTTPClientAPI.uri.ToString();

		if (!PlayerPrefs.HasKey(curServer + "_Guest"))
		{
			Debug.Log("sssr HasKey" + curServer + "_Guest NO");
			userType = UserType.NewUser;
			return;
		}
		Debug.Log("sssr HasKey" + curServer + "_Guest YES");
		GuestAccountID = long.Parse(PlayerPrefs.GetString(curServer + "_Guest"));
		if (GuestAccountID > 0)
			userType = UserType.NotBinding;
		else
			userType = UserType.NewUser;
	}
	
	public void initAccount()
	{
		if(!IsUseAccount)
		{
			return;
		}
		Debug.Log("AccountManager().initAccount()");
		mAccountList = new List<AccountInfo>();
		userType = UserType.NewUser;
		if(!string.IsNullOrEmpty(PlayerPrefs.GetString("ACCOUNT_ID"))) //ACCOUNT_ID: 本地缓存的快速登陆的AccountID
		{
			mVisitorAccount = new AccountInfo();
			mVisitorAccount.accountId = long.Parse(PlayerPrefs.GetString("ACCOUNT_ID"));
			Debug.Log("ACCOUNT_ID = " + mVisitorAccount.accountId);
			//第 二次及以上 登陆才会进入这段逻辑
			userType = UserType.NotBinding;
			if (PlayerPrefs.HasKey(mVisitorAccount.accountId.ToString()+ "_LoginBefore"))
				PlayerPrefs.SetInt(mVisitorAccount.accountId.ToString()+ "_LoginBefore",2);
			else
				PlayerPrefs.SetInt(mVisitorAccount.accountId.ToString()+ "_LoginBefore",1);
		}
		
		if(! string.IsNullOrEmpty(PlayerPrefs.GetString(AccountIdKey + "Cur"))) //Cur: 缓存已经绑定的登陆信息
		{
			mCurAccount = new AccountInfo();
			mCurAccount.accountId = long.Parse(PlayerPrefs.GetString(AccountIdKey + "Cur"));
			mCurAccount.email =  PlayerPrefs.GetString(AccountEmailKey + "Cur");
			mCurAccount.password =  PlayerPrefs.GetString(AccountPasswordKey + "Cur");
			Debug.Log(AccountIdKey + "Cur = " + mCurAccount.accountId);
			Debug.Log(AccountEmailKey + "Cur = " + mCurAccount.email);
			Debug.Log(AccountPasswordKey + "Cur = " + mCurAccount.password);
			userType = UserType.OldUser;
		}
		int i = 0;
		while(PlayerPrefs.HasKey(AccountEmailKey + i))
		{
			if(! string.IsNullOrEmpty(PlayerPrefs.GetString(AccountEmailKey + i)))
			{
				AccountInfo info = new AccountInfo();
				info.accountId = long.Parse(PlayerPrefs.GetString(AccountIdKey + i));
				info.email = PlayerPrefs.GetString(AccountEmailKey + i);
				info.password = PlayerPrefs.GetString(AccountPasswordKey + i);
				info.listIndex = i;
				mAccountList.Add(info);
				Debug.Log(AccountIdKey + i + " = " + info.accountId);
				Debug.Log(AccountEmailKey + i + " = " + info.email);
				Debug.Log(AccountPasswordKey + i + " = " + info.password);
			}
			else
			{
				PlayerPrefs.DeleteKey(AccountIdKey + i);
				PlayerPrefs.DeleteKey(AccountEmailKey + i);
				PlayerPrefs.DeleteKey(AccountPasswordKey + i);
			}
			i++;
			if(i > 3)
			{
				break;
			}
		}
		//DontDestroyOnLoad(transform.gameObject);
	}
	
	public void ShowAccount()
	{
		ShowCurAccountUI(true);
	}
	public void OnEnterGame()
	{
		mIsInGame = true;
		if(accountsUI != null)   
		{
			accountsUI.SetActive(false);
		}
		DestroyAllUI();
	}
	public void DestroyAllUI()
	{
		//test
//		if(accountsUI != null)
//		{
//			Texture tx = accountsUI.GetComponent<CyouAccounts>().accountBtn.transform.FindChild("Background").gameObject.GetComponent<UISprite>().mainTexture;
//			Destroy(tx);
//			tx = null;
//		}
		
		if(accountsUI != null)   
		{
			accountsUI.SetActive(false);
			Destroy(accountsUI);
			accountsUI = null;
		}
		if(loginUI != null)
		{
			loginUI.SetActive(false);
			Destroy(loginUI);
			loginUI = null;
		}
		if(registerUI != null)
		{
			registerUI.SetActive(false);
			Destroy(registerUI);
			registerUI = null;
		}		
		if(manageUI != null)
		{
			manageUI.SetActive(false);
			Destroy(manageUI);
			manageUI = null;
		}	
		if(bindUI != null)
		{
			bindUI.SetActive(false);
			Destroy(bindUI);
			bindUI = null;
		}

		Resources.UnloadUnusedAssets();
	}
	public void ShowLoginUI()
	{
#if UNITY_ANDROID
		if(AndroidConfig.channelLogin())
		{
			return;
		}
#endif
		if(loginUI == null)
		{
			GameObject go = Resources.Load("Widgets/CyouLogin") as GameObject;
			loginUI = Instantiate(go) as GameObject;
			loginUI.transform.parent = Camera.main.transform;
			loginUI.transform.localPosition = new Vector3(0, 0, -300f);
			loginUI.transform.localScale = Vector3.one;
			loginUI.SetActive(true);
			go = null;
			Resources.UnloadUnusedAssets();
#if UNITY_ANDROID
            HideRegisterUI();
#endif
		}
		else
		{
            if (!loginUI.gameObject.activeSelf)
            {
                loginUI.SetActive(true);
#if UNITY_ANDROID
                HideRegisterUI();
#endif
            }
		}
		isAccountLogin = true;
	}
	public void HideLoginUI(){//隐藏登录界面方法
		isAccountLogin = false;
		if(loginUI == null)
		{
			return;
		}
		else
		{
			if(loginUI.gameObject.activeSelf) loginUI.SetActive(false);
		}
	}
	public void ShowRegisterUI()
	{
		if(registerUI == null)
		{
			GameObject go = Resources.Load("Widgets/CyouRegister") as GameObject;
			registerUI = Instantiate(go) as GameObject;
			registerUI.transform.parent = Camera.main.transform;
			registerUI.transform.localPosition = new Vector3(0, 0, -300f);
			registerUI.transform.localScale = Vector3.one;
			registerUI.SetActive(true);
			go = null;
			Resources.UnloadUnusedAssets();
#if UNITY_ANDROID
            HideLoginUI();
#endif
		}
		else
		{
            if (!registerUI.gameObject.activeSelf)
            {
                registerUI.SetActive(true);
#if UNITY_ANDROID
                HideLoginUI();
#endif
            }
		}
	}
#if UNITY_ANDROID
    public void HideRegisterUI()
    {//隐藏登录界面方法
        if (registerUI == null)
        {
            return;
        }
        else
        {
            if (registerUI.gameObject.activeSelf) registerUI.SetActive(false);
        }
    }
#endif
	public void ShowCurAccountUI(bool bFirst)
	{
		if(!IsUseAccount)
		{
			return;
		}
		
		if(accountsUI == null)
		{
			GameObject go = Resources.Load("Widgets/CyouAccounts") as GameObject;
			accountsUI = Instantiate(go) as GameObject;
            accountsUI.transform.parent = Camera.main.transform;
			accountsUI.transform.localPosition = new Vector3(0, 0, -20f);
			accountsUI.transform.localScale = Vector3.one;
			accountsUI.SetActive(true);
#if UNITY_ANDROID
			if(bFirst == false)
			{
				AndroidConfig.switchAccountRefresh(accountsUI);
			}
#endif
			go = null;
			Resources.UnloadUnusedAssets();
		}
		else
		{
			if(! accountsUI.gameObject.activeSelf) accountsUI.SetActive(true);
			accountsUI.GetComponent<CyouAccounts>().SwitchRefresh();
		}
	}
	public void HideCurAccountUI(){
		if(accountsUI == null)
		{
			return;
		}
		else
		{
			if(accountsUI.gameObject.activeSelf) accountsUI.SetActive(false);
		}
	}
	public void ShowAccountManageUI()
	{
#if UNITY_ANDROID
		if(AndroidConfig.showAccountManageUI())
		{
			return;
		}
#endif
		if(manageUI == null)
		{
			GameObject go = Resources.Load("Widgets/CyouManager") as GameObject;
			manageUI = Instantiate(go) as GameObject;
            manageUI.transform.parent = Camera.main.transform;
			manageUI.transform.localPosition = new Vector3(0, 0, -300f);
			manageUI.transform.localScale = Vector3.one;
			manageUI.SetActive(true);
			go = null;
			Resources.UnloadUnusedAssets();
		}
		else
		{
			if(! manageUI.gameObject.activeSelf) manageUI.SetActive(true);
		}
	}
	public void ShowAccountBindUI()
	{
#if UNITY_ANDROID
		if(AndroidConfig.channelLogin())
		{
			return;
		}
#endif
		if(bindUI == null)
		{
			GameObject go = Resources.Load("Widgets/CyouBind") as GameObject;
			bindUI = Instantiate(go) as GameObject;
            bindUI.transform.parent = Camera.main.transform;
			bindUI.transform.localPosition = new Vector3(0, 0, -300f);
			bindUI.transform.localScale = Vector3.one;
			bindUI.SetActive(true);
			go = null;
			Resources.UnloadUnusedAssets();
		}
		else
		{
			if(! bindUI.gameObject.activeSelf) bindUI.SetActive(true);
		}
	}
	public string GetLoginAccountID()
	{
#if UNITY_ANDROID
		string accountID = AndroidConfig.getLoginAccountID();
		if(accountID != null)
		{
			return accountID;
		}
#endif
		string account_id = "";
		if(mCurAccount != null)
		{
			account_id = mCurAccount.accountId.ToString();
		}
		else
		{
			account_id = PlayerPrefs.GetString("ACCOUNT_ID");
		}
		return account_id;
	}
	
	void SaveCurAccount()
	{
		if(mCurAccount != null)
		{
			PlayerPrefs.SetString(AccountIdKey + "Cur", mCurAccount.accountId.ToString());
			PlayerPrefs.SetString(AccountEmailKey + "Cur", mCurAccount.email);
			PlayerPrefs.SetString(AccountPasswordKey + "Cur", mCurAccount.password);
			Debug.LogWarning("		|	AccountId	|	AccountEmail	|	AccountPasswordKey");
			Debug.LogWarning("Cur	|	"+mCurAccount.accountId.ToString()+"	|	"+mCurAccount.email+"	|	"+mCurAccount.password);
			AddAcount(mCurAccount);
		}
	}
	void DeleteCurAccount()
	{
		PlayerPrefs.DeleteKey(AccountIdKey + "Cur");
		PlayerPrefs.DeleteKey(AccountEmailKey + "Cur");
		PlayerPrefs.DeleteKey(AccountPasswordKey + "Cur");
		mCurAccount = null;
	}
	void AddAcount(AccountInfo account)
	{
#if UNITY_ANDROID
		if(AccountList.Count > 4)
#else
		if(mAccountList.Count > 4)
#endif
		{
			return;
		}//大于四，不可能，报错
		bool exist = false;
		int curAccount = -1;
		foreach(AccountInfo info in mAccountList)
		{
			if(info.email == account.email)
			{
				exist = true;
				curAccount = info.listIndex;
				info.accountId = account.accountId;
				info.email = account.email;
				break;
			}
		}
		
		if(exist)//当前账号在记录内
		{
			foreach(AccountInfo info in mAccountList)
			{
				if(info.listIndex < curAccount){
					info.listIndex++;
				}
				else if(info.listIndex == curAccount){
					info.listIndex = 0;
				}
			}//对已有队列重新排序
			mAccountList.Sort(LoginCompareTo);
		}
		else//当前账号不在记录内
		{
			foreach(AccountInfo info in mAccountList)
			{
				info.listIndex++;
				if(info.listIndex == 4){
					mAccountList.Remove(info);
					break;
				}
			}//
			account.listIndex = 0;
			mAccountList.Add(account);
			mAccountList.Sort(LoginCompareTo);
		}
		for(int i = 0; i < mAccountList.Count; i++)
		{
			PlayerPrefs.SetString(AccountIdKey + mAccountList[i].listIndex, mAccountList[i].accountId.ToString());
			PlayerPrefs.SetString(AccountEmailKey + mAccountList[i].listIndex, mAccountList[i].email);
			PlayerPrefs.SetString(AccountPasswordKey + mAccountList[i].listIndex, mAccountList[i].password);
			Debug.LogWarning(i+"	|	"+mAccountList[i].accountId.ToString()+"	|	"+mAccountList[i].email+"	|	"+mAccountList[i].password);
		}
	}
	//登录排序算法
    public int LoginCompareTo(AccountInfo actA, AccountInfo actB)
    {
		if(actA.listIndex < actB.listIndex)
			return -1;
		else 
			return 1;
    }
	
	
	public static void ClearAllAccount()
	{
		PlayerPrefs.DeleteKey("ACCOUNT_ID");
		PlayerPrefs.DeleteKey(AccountIdKey + "Cur");
		PlayerPrefs.DeleteKey(AccountEmailKey + "Cur");
		PlayerPrefs.DeleteKey(AccountPasswordKey + "Cur");
		
		int i = 0;
		while(PlayerPrefs.HasKey(AccountIdKey + i))
		{
			PlayerPrefs.DeleteKey(AccountIdKey + i);
			PlayerPrefs.DeleteKey(AccountEmailKey + i);
			PlayerPrefs.DeleteKey(AccountPasswordKey + i);
			i++;
		}
	}
	void OnApplicationQuit()
	{
        DestroyAllUI();
		_instance = null;
		Destroy(gameObject);
	}
	void OnDestroy ()
	{
		DestroyAllUI();
        _instance = null;
	}
	
	public enum AccountState{
		NONE,//正常
		NULLOREMPTY,//空
		TOOSHORT,//过短
		TOOLONG,//过长
		ERRORTYPE,//格式错误
		NOTEMAIL,//非邮箱
	}
	public static AccountState CheckUsername(string username){

		
		if(string.IsNullOrEmpty(username))
			return AccountState.NOTEMAIL;
		
		if(username.StartsWith("@") || username.EndsWith("@"))
			return AccountState.NOTEMAIL;
		
		string pattern = @"(([a-z0-9_])+@([a-zA-Z0-9_])+(\.[a-zA-Z0-9_]))";
		if(!System.Text.RegularExpressions.Regex.IsMatch(username,pattern)){
			return AccountState.NOTEMAIL;
		}
		
		string [] checkText = username.Split('@');
		if(checkText.Length != 2){
			return AccountState.NOTEMAIL;
		}else{
			string backStr = checkText[1];
			if(backStr.StartsWith(".") || backStr.EndsWith(".")
				|| backStr.StartsWith("_") || backStr.EndsWith("_")){
				return AccountState.NOTEMAIL;
			}
			
			
			string[] atback = backStr.Split('.');
			if(atback.Length <2 ){
				return AccountState.NOTEMAIL;
			}
			
			string temp = backStr.Replace(".","");
			if(string.IsNullOrEmpty(temp))
				return AccountState.NOTEMAIL;
			int templen = temp.Length;
			if(templen > 20)
				return AccountState.TOOLONG;
			
			
			for(int i=0 ; i<templen ; i++){
				char c_un = temp[i];
				if((c_un >= 'a' && c_un <= 'z')||(c_un >= '0' && c_un <= '9')||(c_un == '_')){
					//蟄礼ｬｦ隨ｦ蜷郁ｦ∵ｱ
					//Debug.Log("CheckUsername : "+c_un+" = (int)"+(int)c_un);
				}
				else
					return AccountState.ERRORTYPE;
			}
			
			
			if(string.IsNullOrEmpty(checkText[0]))
				return AccountState.NULLOREMPTY;
			int len = checkText[0].Length;
			if(len < 5)
				return AccountState.TOOSHORT;
			if(len > 16)
				return AccountState.TOOLONG;
			
			for(int i=0 ; i<len ; i++){
				char c_un = username[i];
				if((c_un >= 'a' && c_un <= 'z')||(c_un >= '0' && c_un <= '9')||(c_un == '_')){
					//包含无效字母
					//Debug.Log("CheckUsername : "+c_un+" = (int)"+(int)c_un);
				}
				else
					return AccountState.ERRORTYPE;
			}
				
		}
		return AccountState.NONE;

	}
	public static AccountState CheckPassword(string password){
		int len = password.Length;
		if(string.IsNullOrEmpty(password))
			return AccountState.NULLOREMPTY;
		if(len < 4)
			return AccountState.TOOSHORT;
		if(len > 16)
			return AccountState.TOOLONG;
		//密码仅不可使用空格、逗号、单双引号
		for(int i=0 ; i<len ; i++){
			char c_pw = password[i];
			//Debug.Log("CheckPassword : "+c_pw+" = (int)"+(int)c_pw);
			if(c_pw == ' ' || c_pw == ',' || c_pw == '\'' || c_pw == '\"')
				return AccountState.ERRORTYPE;
			if((c_pw < 0 && c_pw > 128)){
				return AccountState.ERRORTYPE;
			}
		}
		return AccountState.NONE;
	}

#if UNITY_ANDROID
	public string GetCurEmail()
	{
		string email = "";
		if(null != mCurAccount)
		{
			email = mCurAccount.email;
		}
		else
		{
			email = PlayerPrefs.GetString(AccountEmailKey + "Cur");
		}
		return AccountInfo.Base64Decode(email);	 
	}
	
	public GameObject GetAccountsUI()
	{
		return accountsUI;
	}
	
	// 回调从注册窗口返回的用户名
	public void OnCyouRegCallback(string jsonstr){
		JsonData json = JsonMapper.ToObject (jsonstr);
		JsonData data = json ["data"];
		// 将信息显示到输入框 //
		// 设置账号输入字符串 //
		Debug.LogWarning("AccountManager.Instance.lastWindow = "+AccountManager.Instance.lastWindow);
		if(AccountManager.Instance.lastWindow == AccountManager.WindowType.LOGIN){
			AccountManager.Instance.lastWindow = AccountManager.WindowType.REGISTER;
			AccountManager.Instance.ShowLoginUI();
		}else if(AccountManager.Instance.lastWindow == AccountManager.WindowType.MANAGER){
			AccountManager.Instance.lastWindow = AccountManager.WindowType.REGISTER;
			AccountManager.Instance.ShowAccountManageUI();
		}
		else
			AccountManager.Instance.lastWindow = AccountManager.WindowType.REGISTER;
		// 设置用户名 //
		loginUI.GetComponent<CyouLogin>().setUserName((string)data);
	}
#endif
	
	public void otherRegFinish(string username)
	{
		Debug.Log("otherRegFinish:"+username);
		KDTLWebView.removeWebView();
		//registerUI.GetComponent<CyouRegister>().setUserName(username);
		registerUI.SetActive(false);
		ShowLoginUI();
		loginUI.GetComponent<CyouLogin>().setUserName(username);
	}
//	// Hide the web view.
//       private void DeactivateWebView() 
//       {
//            WebMediator.Hide();
//            // Clear the state of the web view (by loading a blank page).
//            WebMediator.LoadUrl("about:blank");
//        }
	
	//登陆界面使用：判断是否登陆billing超时使用
	public bool IsAccountLogin(){
		if((loginUI != null && loginUI.gameObject.activeSelf) ||
				(manageUI != null && manageUI.gameObject.activeSelf) ||
				(registerUI != null && registerUI.gameObject.activeSelf))
			return true;
		else
			return false;
	}
}


