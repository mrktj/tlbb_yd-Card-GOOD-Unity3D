using UnityEngine;
using System.Collections;
using System;
using card.net;
using Games.CharacterLogic;

public class CYRegisterController : MonoBehaviour {
	
	public GameObject mainUILogic;
	
	public UIInput userNameInput;
	public UIInput passwordInput_1;
	public UIInput passwordInput_2;
	public UICheckbox agreeCheckBox;
	
	private string userName;
	private string password;
	
	void Awake(){
		mainUILogic = GameObject.Find("MainUILogic");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnEnable()
	{
		Refresh();
	}
	void Refresh()
	{
		userNameInput.text = "";
		passwordInput_1.text = "";
		passwordInput_2.text = "";
	}
	
	public void OnRegisterBtn()
	{
		
		AccountManager.AccountState usernameState;
		AccountManager.AccountState passwordState;
		usernameState = AccountManager.CheckUsername(userNameInput.text);
		passwordState = AccountManager.CheckPassword(passwordInput_1.text);
		switch(usernameState){
		case AccountManager.AccountState.NONE:
			break;
		case AccountManager.AccountState.NULLOREMPTY:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg22);
			return;
		case AccountManager.AccountState.TOOSHORT:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg29);
			return;
		case AccountManager.AccountState.TOOLONG:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg30);
			return;
		case AccountManager.AccountState.NOTEMAIL:
			BoxManager.showMessage("请使用正确的邮箱格式","账号格式错误");
			return ;
		default:
			BoxManager.showMessage("账号应为5-16位小写字母、数字、下划线组成","账号格式错误");//WML MARK
			return;
		}
		if(passwordInput_1.text != passwordInput_2.text)
		{
//			BoxManager.showMessage("密码不一致!重新输入！");  
			BoxManager.showMessageByID((int)MessageIdEnum.Msg31);
			return;
		}
		switch(passwordState){
		case AccountManager.AccountState.NONE:
			break;
		case AccountManager.AccountState.NULLOREMPTY:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg23);
			return;
		case AccountManager.AccountState.TOOSHORT:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg33);
			return;
		case AccountManager.AccountState.TOOLONG:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg34);
			return;
		default:
			BoxManager.showMessage("密码应为4-16位，且不包含空格、逗号、单双引号","密码格式错误");//WML MARK
			return;
		}
		/*
		if(! agreeCheckBox.isChecked)
		{
//			BoxManager.showMessage("请阅读并同意用户协议");  
			BoxManager.showMessageByID((int)MessageIdEnum.Msg32);
			return;
		}
		*/
		userName = userNameInput.text; //WML MARK
		Debug.LogWarning("UserName = " + userName);
		password = passwordInput_1.text;
		
		byte[]  byte_array = System.Text.Encoding.Default.GetBytes(userName);
		userName = Convert.ToBase64String(byte_array);
		byte_array = System.Text.Encoding.Default.GetBytes(userName);
		userName = Convert.ToBase64String(byte_array);
		
		byte_array = System.Text.Encoding.Default.GetBytes(password);
		password = Convert.ToBase64String(byte_array);
		byte_array = System.Text.Encoding.Default.GetBytes(password);
		password = Convert.ToBase64String(byte_array);
		
		FbHelper.CallFBLogout();
		
		NetworkSender.Instance().BindCyouAccount(OnRegisterRet, 2, userName, password, PlayerPrefs.GetString("ACCOUNT_ID"));
		//NetworkSender.Instance().CyouLogin(OnRegisterRet, 2, userName, password, PlayerPrefs.GetString("ACCOUNT_ID"));
	}
	
	void OnRegisterRet(bool bSuccess)
	{
		if(bSuccess)
		{
			switch(Obj_MyselfPlayer.GetMe().cyouCode)
			{
			case -1:
//				BoxManager.showMessage("Billing异常");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg24);
				break;
			case 0:
//				引起注册同名可绑定bug
//				Obj_MyselfPlayer.GetMe().accountID = Obj_MyselfPlayer.GetMe().cyouAccountId;
//				AccountInfo info = new AccountInfo();
//				info.accountId = Obj_MyselfPlayer.GetMe().cyouAccountId;
//				info.email = userName;
//				info.password = password;
//				AccountManager.Instance.CurAccount = info;
				//gameObject.SetActive(false);//TT10628
				//在这儿登陆--
				//NetworkSender.Instance().CyouLogin(OnLoginRet, 1, userName, password, PlayerPrefs.GetString("ACCOUNT_ID"));
				NetworkSender.Instance().BindCyouAccount(OnLoginRet, 1, userName, password, PlayerPrefs.GetString("ACCOUNT_ID"));
				break;
			case 1:
//				BoxManager.showMessage("参数不完整");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg26);
				break;
			case 2:
//				BoxManager.showMessage("用户名已存在");
				Debug.LogWarning("Uesr Exist");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg6);
				break;
			case 3:
//				BoxManager.showMessage("该ip地址禁止注册（黑名单）");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg35);
				break;
			case 4:
//				BoxManager.showMessage("同ip注册间隔必须大于3分钟，3分钟不得超过6个");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg36);
				break;
			case 5:
//				BoxManager.showMessage("该ip今天不能注册账号");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg37);
				break;
			case 6:
//				BoxManager.showMessage("用户名长度非5-16位");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg38);
				break;
			case 7:
//				BoxManager.showMessage("用户名首字母必须为字母或数字");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg39);
				break;
			case 8:
//				BoxManager.showMessage("用户名只能使用英文字母或数字以及 _ ");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg38);
				break;	 
			case 9:
//				BoxManager.showMessage("禁用词(密码不合法)");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg66);
				break;					
			case 99:
//				BoxManager.showMessage("账号系统异常");  
				BoxManager.showMessageByID((int)MessageIdEnum.Msg26);
				break;			
			default:
				break;
			}
		}
	}
	
	public void OnLoginRet(bool bSuccess)
	{
		Debug.LogWarning("OnLoginRet begin");
		if(bSuccess)
		{
			switch(Obj_MyselfPlayer.GetMe().cyouCode)
			{
			case -1:
//				BoxManager.showMessage("Billing异常");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg24);
				break;
			case 0:
				Obj_MyselfPlayer.GetMe().accountID = Obj_MyselfPlayer.GetMe().cyouAccountId;
				AccountInfo info = new AccountInfo();
				info.accountId = Obj_MyselfPlayer.GetMe().cyouAccountId;
				info.email = userName;
				info.password = password;
				AccountManager.Instance.CurAccount = info;
				//gameObject.SetActive(false);
				//这里是注册加登陆后的返回，游戏内注册
				PlayerPrefs.SetString("LastAccountId", Obj_MyselfPlayer.GetMe().accountID.ToString());
				PlayerPrefs.DeleteKey("ACCOUNT_ID");
				if(AccountManager.Instance.completeDalegate != null)
				{
					AccountManager.Instance.completeDalegate();
				}
				break;
			case 1:
//				BoxManager.showMessage("参数不完整");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg26);
				break;
			case 2:
//				BoxManager.showMessage("用户不存在");  
				BoxManager.showMessageByID((int)MessageIdEnum.Msg41);
				break;
			case 3:
//				BoxManager.showMessage("密码错误");  
				BoxManager.showMessageByID((int)MessageIdEnum.Msg152);
				break;
			case 99:
//				BoxManager.showMessage("账号系统异常");  
				BoxManager.showMessageByID((int)MessageIdEnum.Msg26);
				break;			
			default:
				break;
			}
		}
		Debug.LogWarning("OnLoginRet end");
	}

	public void OnAgreeCheckBox()
	{
		
	}
	
	public void OnAgreementBtn()
	{
		Debug.Log("OnAgreementBtn(), " + AccountManager.userAgreementURL);
		KDTLWebView.ShowWebView(AccountManager.userAgreementURL);
	}
	public void OnBackToLoginBtn()
	{
		gameObject.SetActive(false);
		if(AccountManager.Instance.IsInGame)
		{
			AccountManager.Instance.ShowAccountBindUI();
		}
		else
		{
			AccountManager.Instance.ShowLoginUI();
		}
	}
	public void Cancel(){
		//mainUILogic.SendMessage("OnHelpWindow");
		OnBind();
	}
	public void OnBind(){
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
}
