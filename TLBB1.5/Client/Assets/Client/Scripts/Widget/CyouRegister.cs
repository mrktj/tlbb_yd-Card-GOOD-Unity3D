using UnityEngine;
using System.Collections;
using System;
using card.net;
using Games.CharacterLogic;


public class CyouRegister : MonoBehaviour {
	
	public UIInput userNameInput;
	public UIInput passwordInput_1;
	public UIInput passwordInput_2;
	public UICheckbox agreeCheckBox;
	
	public GameObject otherRegBtn;
	
	private string userName;
	private string password;
	
	// Use this for initialization
	void Start () {
		if(DeviceHelper.GetChannelID() == "pp")
		{
			otherRegBtn.SetActive(false);
		}
#if UNITY_ANDROID
		//注册框太大，缩小//
		Vector3 scale = transform.localScale;
		scale.x = 0.8f;
		scale.y = 0.8f;
		transform.localScale = scale;
#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
    void OnEnable()
    {
        userNameInput.text = "";
        passwordInput_1.text = "";
        passwordInput_2.text = "";
        agreeCheckBox.isChecked = true;
    }
	public void OnRegisterOtherBtn()
	{
		KDTLWebView.ShowWebView("http://member.changyou.com/webphoregist/phone_regist.html?gametype=undefined&order=spe");
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
		userName = userNameInput.text; //zhouwei
		password = passwordInput_1.text;
		
		byte[]  byte_array = System.Text.Encoding.Default.GetBytes(userName);
		userName = Convert.ToBase64String(byte_array);
		byte_array = System.Text.Encoding.Default.GetBytes(userName);
		userName = Convert.ToBase64String(byte_array);
		
		byte_array = System.Text.Encoding.Default.GetBytes(password);
		password = Convert.ToBase64String(byte_array);
		byte_array = System.Text.Encoding.Default.GetBytes(password);
		password = Convert.ToBase64String(byte_array);
		
		//注销fb信息;
		FbHelper.CallFBLogout();
		
		if (AccountManager.Instance.lastWindow == AccountManager.WindowType.BIND)
			NetworkSender.Instance().Login(OnGuestLoginDone, PlayerPrefs.GetString("ACCOUNT_ID"),1);// 回调 AccountID 登陆类型(正常登陆)
//			NetworkSender.Instance().BindCyouAccount(OnBindRet,2,userName,password,PlayerPrefs.GetString("ACCOUNT_ID"));
		else
			NetworkSender.Instance().CyouLogin(OnRegisterRet, 2, userName, password, PlayerPrefs.GetString("ACCOUNT_ID"));

	}
	
	void OnGuestLoginDone(bool bSuccess)
	{
		if (bSuccess)
		{
			if (Obj_MyselfPlayer.GetMe().cyouCode == 0)
			{
				userName = userNameInput.text; //zhouwei
				password = passwordInput_1.text;
				
				byte[]  byte_array = System.Text.Encoding.Default.GetBytes(userName);
				userName = Convert.ToBase64String(byte_array);
				byte_array = System.Text.Encoding.Default.GetBytes(userName);
				userName = Convert.ToBase64String(byte_array);
				
				byte_array = System.Text.Encoding.Default.GetBytes(password);
				password = Convert.ToBase64String(byte_array);
				byte_array = System.Text.Encoding.Default.GetBytes(password);
				password = Convert.ToBase64String(byte_array);
				NetworkSender.Instance().BindCyouAccount(OnBindRet,2,userName,password,PlayerPrefs.GetString("ACCOUNT_ID"));
			}
		}
	}
	
	
	void OnBindRet(bool bSuccess)
	{
		if (bSuccess)
		{
			Debug.Log("Account = " + PlayerPrefs.GetString("ACCOUNT_ID"));
			switch(Obj_MyselfPlayer.GetMe().cyouCode)
			{
			case -1:
//				BoxManager.showMessage("Billing异常");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg24);
				break;
			case 0:
				NetworkSender.Instance().BindCyouAccount(OnBindLoginRet,1,userName,password,PlayerPrefs.GetString("ACCOUNT_ID"));
				break;
			case 1:
//				BoxManager.showMessage("参数不完整");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg26);
				break;
			case 2:
//				BoxManager.showMessage("用户已存在");
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
	
	void OnBindLoginRet(bool bSuccess)
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
				AccountInfo info = new AccountInfo();
				info.accountId = Obj_MyselfPlayer.GetMe().accountID;
				info.email = userName;
				info.password = password;
				AccountManager.Instance.CurAccount = info;
				//清除快速登录的账号--
				PlayerPrefs.DeleteKey("ACCOUNT_ID");
				if (PlayerPrefs.HasKey(HTTPClientAPI.uri.ToString() + "_Guest"))
					PlayerPrefs.DeleteKey(HTTPClientAPI.uri.ToString() + "_Guest");
				gameObject.SetActive(false);
				if(AccountManager.Instance.completeDalegate != null)
				{
					AccountManager.Instance.completeDalegate();
				}
//				BoxManager.showMessage("绑定成功！");
				
				AccountManager.Instance.ShowCurAccountUI(false);
				BoxManager.showMessageByID((int)MessageIdEnum.Msg25);
				break;
			case 1:
//				BoxManager.showMessage("参数不完整");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg26);
				break;
			case 2:
//				BoxManager.showMessage("用户不存在");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg41); //不可能出现
				break;
			case 3:
//				BoxManager.showMessage("密码错误"); 
				BoxManager.showMessageByID((int)MessageIdEnum.Msg152); //不可能出现
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
	
	//如果是账号登陆 调用此方法
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
				Obj_MyselfPlayer.GetMe().accountID = Obj_MyselfPlayer.GetMe().cyouAccountId;
				
				/*AccountInfo info = new AccountInfo();
				info.accountId = Obj_MyselfPlayer.GetMe().cyouAccountId;
				info.email = userName;
				info.password = password;
				AccountManager.Instance.CurAccount = info;*/
#if UNITY_ANDROID
				// gameObject.SetActive(false); // 这里注释掉，在登录成功后再隐藏
#else
				gameObject.SetActive(false);
#endif
				/*if(! AccountManager.Instance.IsInGame)
				{*/
					//在这儿登陆--
					NetworkSender.Instance().CyouLogin(OnLoginRet, 1, userName, password, PlayerPrefs.GetString("ACCOUNT_ID"));
				/*}
				else
				{
					NetworkSender.Instance().BindCyouAccount(OnLoginRet, 1, userName, password, PlayerPrefs.GetString("ACCOUNT_ID"));
				}*/
				break;
			case 1:
//				BoxManager.showMessage("参数不完整");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg26);
				break;
			case 2:
//				BoxManager.showMessage("用户名已存在"); 
				Debug.LogWarning("Cyou Register User Exist");
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
		if(bSuccess)
		{
			switch(Obj_MyselfPlayer.GetMe().cyouCode)
			{
			case -2:
				BoxManager.showMessage("请到《天龙八部（移动版）》官网\nhttp://tlyd.changyou.com/\n进行激活","账号未激活"); //WML MARK
				AccountManager.Instance.ShowLoginUI();
				break;
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
				gameObject.SetActive(false);
				BoxManager.removeMessage();
				//这里是注册加登陆后的返回，有2种情况：1,游戏外注册;2,游戏内注册				
				if(! AccountManager.Instance.IsInGame)
				{
					AccountManager.Instance.ShowCurAccountUI(false);
				}
				else if(AccountManager.Instance.completeDalegate != null)
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
		Debug.LogWarning("AccountManager.Instance.lastWindow = "+AccountManager.Instance.lastWindow);
		if(AccountManager.Instance.lastWindow == AccountManager.WindowType.LOGIN){
			AccountManager.Instance.lastWindow = AccountManager.WindowType.REGISTER;
			AccountManager.Instance.ShowLoginUI();
		}
		else if(AccountManager.Instance.lastWindow == AccountManager.WindowType.MANAGER){
			AccountManager.Instance.lastWindow = AccountManager.WindowType.REGISTER;
			AccountManager.Instance.ShowAccountManageUI();
		}
		else if (AccountManager.Instance.lastWindow == AccountManager.WindowType.BIND)
		{
			AccountManager.Instance.ShowAccountBindUI();
		}
		else
			AccountManager.Instance.lastWindow = AccountManager.WindowType.REGISTER;
	}
//	public void setUserName(string name)
//	{
//		Debug.Log("setUserName:"+name);
//		userNameInput.text = name;
//	}
#if UNITY_ANDROID
	void OnRegisterOther()
	{
		KDTLWebView.ShowWebView(AccountManager.registerOther);
	}
#endif
}
