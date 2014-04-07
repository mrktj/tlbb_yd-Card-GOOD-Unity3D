using UnityEngine;
using System.Collections;
using System;
using Games.CharacterLogic;
using card.net;

public class BindAccountController : MonoBehaviour {
	
	public GameObject mainUILogic;

	public UIInput userNameInput;
	public UIInput passwordInput;
	
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
		passwordInput.text = "";
	}
	public void OnLoginBtn()
	{
		if(string.IsNullOrEmpty(userNameInput.text))
		{
			//BoxManager.showMessage("用户名不能为空"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg22);
			return;
		}
		if(string.IsNullOrEmpty(passwordInput.text))
		{
			//BoxManager.showMessage("密码不能为空"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg23);
			return;
		}
		userName = userNameInput.text; //WML MARK
		password = passwordInput.text;
		
		userName = AccountInfo.Base64Encode(userName);
		password = AccountInfo.Base64Encode(password);
		
		FbHelper.CallFBLogout();
		
		NetworkSender.Instance().BindCyouAccount(OnLoginRet, 1, userName, password, PlayerPrefs.GetString("ACCOUNT_ID"));
	}
	public void OnLoginRet(bool bSuccess)
	{
		if(bSuccess)
		{
			switch(Obj_MyselfPlayer.GetMe().cyouCode)
			{
			case -1:
				//BoxManager.showMessage("Billing异常");  
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
				
				//gameObject.SetActive(false);
				if(AccountManager.Instance.completeDalegate != null)
				{
					AccountManager.Instance.completeDalegate();
				}
				//BoxManager.showMessage("绑定成功！");   
				BoxManager.showMessageByID((int)MessageIdEnum.Msg25);
				break;
			case 1:
				//BoxManager.showMessage("参数不完整");   
				BoxManager.showMessageByID((int)MessageIdEnum.Msg26);
				break;
			case 2:
				//BoxManager.showMessage("用户不存在");   
				BoxManager.showMessageByID((int)MessageIdEnum.Msg27);
				break;
			case 3:
				//BoxManager.showMessage("密码错误");   
				BoxManager.showMessageByID((int)MessageIdEnum.Msg28);
				break;
			case 99:
				//BoxManager.showMessage("账号系统异常");   
				BoxManager.showMessageByID((int)MessageIdEnum.Msg26);
				break;			
			default:
				break;
			}
		}
	}
	public void Cancel(){
		if(Obj_MyselfPlayer.GetMe().isPurchase)
		{
			mainUILogic.SendMessage("OnPurchaseWindow");
		}
		else
		{
			mainUILogic.SendMessage("OnHelpWindow");
		}
	}
	public void OnRegisterBtn()
	{
		mainUILogic.SendMessage("OnRegisterController");
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
		string fbuserid = AccountInfo.Base64Encode( FB.UserId);
		string fbtoken = AccountInfo.Base64Encode( FB.AccessToken);
		NetworkSender.Instance().BindCyouAccount(FacebookBindDone, -1, fbuserid, fbtoken, PlayerPrefs.GetString("ACCOUNT_ID"));
	}
	
	public void FacebookBindDone( bool isSuccess)
	{
		Debug.Log("FacebookLoginDone");
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
				
					//gameObject.SetActive(false);
					if(AccountManager.Instance.completeDalegate != null)
					{
						AccountManager.Instance.completeDalegate();
					}
					//BoxManager.showMessage("绑定成功！");   
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
