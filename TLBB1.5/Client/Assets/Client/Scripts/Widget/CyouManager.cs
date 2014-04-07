using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using card.net;

public class CyouManager : MonoBehaviour {
	
	public UIPopupList userNameList;
	public UIInput passwordInput;
	
	private string userName;
	private string password;
	
	private string sUserName;
	// Use this for initialization
	
	public GameObject buttonForget;
	
	void Start () {
		buttonForget.SetActive(false);
		passwordInput.text = "";
		userNameList.items.Clear();
		foreach(AccountInfo info in AccountManager.Instance.AccountList)
		{
			if(string.IsNullOrEmpty(info.email))
			{
				continue;
			}
			string email = info.email;
			email =	AccountInfo.Base64Decode(email);

			userNameList.items.Add(email);
			if(info.listIndex == 0)
				ChangeUserName(email);
		}
		AccountManager.Instance.lastWindow = AccountManager.WindowType.MANAGER;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnEnable()
	{
		buttonForget.SetActive(false);
        passwordInput.text = "";
		userNameList.items.Clear();
		foreach(AccountInfo info in AccountManager.Instance.AccountList)
		{
			if(string.IsNullOrEmpty(info.email))
			{
				continue;
			}
			string email = info.email;
			email =	AccountInfo.Base64Decode(email);

			userNameList.items.Add(email);
			if(info.listIndex == 0)
				ChangeUserName(email);
		}
		AccountManager.Instance.lastWindow = AccountManager.WindowType.MANAGER;
	}
	public void OnSelectionChange()
	{
		
	}
	public void OnLoginBtn()
	{/*
		if(string.IsNullOrEmpty(userNameList.selection))
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
		usernameState = AccountManager.CheckUsername(userNameList.selection);
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

		userName = AccountInfo.Base64Encode(sUserName);
		password = AccountInfo.Base64Encode(passwordInput.text);
		
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
				//登录账号不同则清除本地阵型数据
				if(info.email != AccountManager.Instance.CurAccount.email){
					Debug.Log("4444444444444444444444444444444444444444444444444444444444444444444444444");
					Obj_MyselfPlayer.GetMe().ClearBattleArraySet();
					MainController.needFlashWulin = false;
				}
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
	
	public void OnCloseBtn()
	{
		gameObject.SetActive(false);
		AccountManager.Instance.ShowCurAccountUI(false);
	}
	public void OnForgetPassowrdBtn()
	{
		Debug.Log("OnForgetPassowrdBtn(), " + AccountManager.forgetPasswordURL);
		KDTLWebView.ShowWebView(AccountManager.forgetPasswordURL);
	}
	
	public void ChangeUserName(string item){
		sUserName = item;
		userNameList.selection = sUserName;
	}
}
