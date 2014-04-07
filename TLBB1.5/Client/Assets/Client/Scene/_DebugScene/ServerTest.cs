using UnityEngine;
using System.Collections;

using card;
using card.net;
using System;
using Games.CharacterLogic;
using xjgame.message;

public class ServerTest: MonoBehaviour {
	
	public UIInput[] uiInput;
	// Use this for initialization
	void Start () {
		MyServer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void GCServer () {
		SetHttpUri("http://10.6.193.106:8080/XJGameServer/s", 0);
	}
	public void MyServer () {
		SetHttpUri("http://127.0.0.1:8080/XJGameServer/s", 0);
	}
	public void SetHttpUri(string name ,int type){//type:0-address,1-name
		HTTPClientAPI.uri = new Uri(name);
		Debug.LogWarning("*** Set Http Uri : "+name);
	}
	public void Login () {
		string userName = AccountInfo.Base64Encode("servertest@changyou.com");
		string password = AccountInfo.Base64Encode("123456");
		NetworkSender.Instance().CyouLogin(ProvingPasswordRet, 1, userName, password, PlayerPrefs.GetString("ACCOUNT_ID"));
	}
	public void ProvingPasswordRet(bool bSuccess){
		if (bSuccess) {
			Debug.LogWarning("ProvingPasswordRet code = " + Obj_MyselfPlayer.GetMe().cyouCode);
			switch(Obj_MyselfPlayer.GetMe().cyouCode){				
			case 0:
				//Login success
				NetworkSender.Instance().Login(OnGameLoginDone, AccountManager.Instance.GetLoginAccountID(),1);
				break;
			default:
				//Login error
				AccountManager.Instance.HideCurAccountUI();
				BoxManager.showMessage("Login Error! Please login your CYOU-email again!","ERROR");
				break;
			}
		}
	}
	public void OnGameLoginDone(bool bSuccess){
		Debug.Log("Login step - GuideManager.Instance.currentStep:"+GuideManager.Instance.currentStep);
		OnGetUserInfo();
	}
	public void OnGetUserInfo(){
		NetworkSender.Instance().GetUserInfo(OnGetUserInfoDone);
	}
	public void OnGetUserInfoDone(bool bSuccess)
	{
		Debug.Log("Login success");
	}
	
	public void PBSend () {
		CSLogin msg = (CSLogin)PacketDistributed.CreatePacket(MessageID.CSLogin);
		NetworkSender.Instance().send(null, msg, false);
	}
}
