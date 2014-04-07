using UnityEngine;
using System.Collections;
using System;
using Games.CharacterLogic;
using card.net;

public class CyouLogin : MonoBehaviour {
	
	public UIInput userNameInput;
	public UIInput passwordInput;
	
	private string userName;
	private string password;
	
	public GameObject forgetButton;
	
	// Use this for initialization
	void Start () {
		AccountManager.Instance.lastWindow = AccountManager.WindowType.LOGIN;
#if UNITY_ANDROID
		//登陆框太大，缩小//
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
		if(DeviceHelper.GetChannelID() == "App_Store" && Obj_MyselfPlayer.GetMe().hadBigAddress)
		{
			forgetButton.SetActive(false);
		}else{
			forgetButton.SetActive(true);
		}
		Refresh();
		AccountManager.Instance.lastWindow = AccountManager.WindowType.LOGIN;
	}
	void Refresh()
	{
		userNameInput.text = "";
		passwordInput.text = "";
	}
	public void OnLoginBtn()
	{
        Debug.Log("xlym:loginFlag:0");
		AccountManager.AccountState usernameState;
		AccountManager.AccountState passwordState;
		usernameState = AccountManager.CheckUsername(userNameInput.text);
		passwordState = AccountManager.CheckPassword(passwordInput.text);
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
		userName = userNameInput.text;
		password = passwordInput.text;
		
		userName = AccountInfo.Base64Encode(userName);
		password = AccountInfo.Base64Encode(password);
		
		//注销fb信息;
		FbHelper.CallFBLogout();
		
		Debug.Log("cyouLogin loginBtn *** username = "+userName+"; password = "+password);
		NetworkSender.Instance().CyouLogin(OnLoginRet, 1, userName, password, PlayerPrefs.GetString("ACCOUNT_ID"));
	}
	public void OnLoginRet(bool bSuccess)
	{
		if(bSuccess)
		{
			switch(Obj_MyselfPlayer.GetMe().cyouCode)
			{
			case -2:
				BoxManager.showMessage("请到《天龙八部（移动版）》官网\nhttp://tlyd.changyou.com/\n进行激活","账号未激活"); //WML MARK
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
				AccountManager.Instance.ShowCurAccountUI(false);
//				BoxManager.showMessage("登录成功！");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg65);
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
	public void OnRegisterBtn()
	{
		gameObject.SetActive(false);
		AccountManager.Instance.ShowRegisterUI();
	}
	public void OnDirectEnterGameBtn()
	{
		gameObject.SetActive(false);
		AccountManager.Instance.ShowCurAccountUI(false);
	}
	public void OnForgetPassowrdBtn()
	{
		Debug.Log("OnForgetPassowrdBtn(), " + AccountManager.forgetPasswordURL);
		KDTLWebView.ShowWebView(AccountManager.forgetPasswordURL);
	}
	public void setUserName(string userName){
		userNameInput.text=userName;
	}
	
	public void OnFaceBookLoginBtn(GameObject go){
		if(FB.IsLoggedIn && !string.IsNullOrEmpty(FB.UserId) &&!string.IsNullOrEmpty(FB.AccessToken)){
			FacebookLoginCallBack();
		}else{
			FbHelper.CallFBLogin(FacebookLoginCallBack);
		}
	}
	
	public void FacebookLoginCallBack(){
		Debug.Log("fb OnGameLogin");
		string email = AccountInfo.Base64Encode(FB.UserId);
		NetworkSender.Instance().CyouLogin(FacebookLoginDone,
											-1,email,FB.AccessToken,
											PlayerPrefs.GetString("ACCOUNT_ID"));
	}
	
	public void FacebookLoginDone( bool isSuccess)
	{
		Debug.Log("FacebookLoginDone");
		if(isSuccess)
		{
			switch(Obj_MyselfPlayer.GetMe().cyouCode)
			{
				case 0:
					FbHelper.UseFbLogin = true;
				
					gameObject.SetActive(false);
					AccountManager.Instance.ShowCurAccountUI(false);
				   	BoxManager.showMessageByID((int)MessageIdEnum.Msg65);
					break;
				default:
					FbHelper.CallFBLogout();
					BoxManager.showMessageByID((int)MessageIdEnum.Msg171);
					break;				
			}
		}
	}
	
}
