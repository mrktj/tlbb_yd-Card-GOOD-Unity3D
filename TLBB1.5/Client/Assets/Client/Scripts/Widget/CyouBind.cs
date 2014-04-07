using UnityEngine;
using System.Collections;
using card.net;
using Games.CharacterLogic;

public class CyouBind : MonoBehaviour {

	public UIInput userNameInput;
	public UIInput passwordInput;
	
	private string userName;
	private string password;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnEnable()
    {
        userNameInput.text = "";
        passwordInput.text = "";
		AccountManager.Instance.lastWindow = AccountManager.WindowType.BIND;
    }

	public void OnLoginBtn()
	{/*
		if(string.IsNullOrEmpty(userNameInput.text))
		{
//			BoxManager.showMessage("用户名不能为空"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg22);
			return;
		}
		if(string.IsNullOrEmpty(passwordInput.text))
		{
//			BoxManager.showMessage("密码不能为空"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg23);
			return;
		}*/
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
		
		FbHelper.CallFBLogout();
		
		NetworkSender.Instance().Login(OnGuestLoginDone, PlayerPrefs.GetString("ACCOUNT_ID"),1);// 回调 AccountID 登陆类型(正常登陆)
//		NetworkSender.Instance().BindCyouAccount(OnLoginRet, 1, userName, password, accountID);
//		NetworkSender.Instance().BindCyouAccount(OnLoginRet, 1, userName, password, PlayerPrefs.GetString("ACCOUNT_ID"));
	}
	
	public void OnGuestLoginDone(bool bSuccess)
	{
		if (bSuccess)
		{
			if (Obj_MyselfPlayer.GetMe().cyouCode == 0)
			{
				userName = userNameInput.text; //WML MARK
				password = passwordInput.text;
				userName = AccountInfo.Base64Encode(userName);
				password = AccountInfo.Base64Encode(password);
				string accountID = PlayerPrefs.GetString(HTTPClientAPI.uri.ToString() + "_Guest");
				NetworkSender.Instance().BindCyouAccount(OnLoginRet, 1, userName, password, accountID);
			}
		}
	}
	
	public void OnLoginRet(bool bSuccess)
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
				BoxManager.showMessageByID((int)MessageIdEnum.Msg27);
				break;
			case 3:
//				BoxManager.showMessage("密码错误"); 
				BoxManager.showMessageByID((int)MessageIdEnum.Msg28);
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
	public void OnCloseBtn()
	{
		gameObject.SetActive(false);
		AccountManager.Instance.DestroyAllUI();
		AccountManager.Instance.ShowCurAccountUI(false);
	}
	public void OnForgetPassowrdBtn()
	{
		Debug.Log("OnForgetPassowrdBtn(), " + AccountManager.forgetPasswordURL);
		KDTLWebView.ShowWebView(AccountManager.forgetPasswordURL);
	}
	
	public void OnFaceBookBindBtn(){

		FbHelper.CallFBLogin(FacebookLoginCallBack);
	}
	
	public void FacebookLoginCallBack(){
		Debug.Log("before bind,player should login game");
		NetworkSender.Instance().Login(GameLoginCallBack, PlayerPrefs.GetString("ACCOUNT_ID"),1);// 回调 AccountID 登陆类型(正常登陆)
	}	
	
	public void GameLoginCallBack(bool issuccess){
		string fbuserid = AccountInfo.Base64Encode( FB.UserId);
		string fbtoken = AccountInfo.Base64Encode( FB.AccessToken);
		string accountID = PlayerPrefs.GetString(HTTPClientAPI.uri.ToString() + "_Guest");
		NetworkSender.Instance().BindCyouAccount(FacebookBindDone, -1, fbuserid, fbtoken, accountID);	
	}
	
	public void FacebookBindDone( bool isSuccess)
	{
		Debug.Log("FacebookBindDone");
		if(isSuccess)
		{
			switch(Obj_MyselfPlayer.GetMe().cyouCode)
			{
				case 0:
					FbHelper.UseFbLogin = true;
				
					AccountInfo info = new AccountInfo();
					info.accountId = Obj_MyselfPlayer.GetMe().accountID;
					info.email = Obj_MyselfPlayer.GetMe().userName;
					info.password = Obj_MyselfPlayer.GetMe().passward;
					AccountManager.Instance.CurAccount = info;
				
					//清除快速登录的账号--
					PlayerPrefs.DeleteKey("ACCOUNT_ID");
					PlayerPrefs.Save();					
				
					gameObject.SetActive(false);
					if(AccountManager.Instance.completeDalegate != null)
					{
						AccountManager.Instance.completeDalegate();
					}
					//BoxManager.showMessage("绑定成功！");   
				
					AccountManager.Instance.ShowCurAccountUI(false);
					BoxManager.showMessageByID((int)MessageIdEnum.Msg25);
				
					break;
				default:
					FbHelper.CallFBLogout();
					BoxManager.showMessage("您的账号已经被绑定",ClientConfigure.title);
					break;				
			}
		}
	}	
	
}
