using UnityEngine;
using System.Collections;
using card.net;
using Games.CharacterLogic;
using Games.LogicObject;

public class FbHelper : MonoBehaviour {
	
	public static string lastResponse = "";
    #region FB.Init() example

    private static bool isInited = false;
	private static bool isIniting = false;
	
	public static bool UseFbLogin{
		get{
			int usefb = PlayerPrefs.GetInt("facebook");
			return usefb > 0?true:false;
		}
		set{
			int usefb = value?1:0;
			PlayerPrefs.SetInt("facebook",usefb);
			PlayerPrefs.Save();
		}
	}
	
	
    public static void CallFBInit()
    {
		if(isInited){
			Debug.Log("FaceBook 已经初始化一次");
		}else{
			Debug.Log("正在初始化Facebook");
			isIniting = true;
        	FB.Init(OnInitComplete, OnHideUnity);
		}
    }
	
    private static void OnInitComplete()
    {
        Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
		isInited = true;
		isIniting = false;
    }

    private static void OnHideUnity(bool isGameShown)
    {
        Debug.Log("Is game showing? " + isGameShown);
    }

    #endregion

    #region FB.Login() example
	
	public delegate void LoginSuccessCallback();
	private static LoginSuccessCallback loginSuccessCallback;
    public static  void CallFBLogin(LoginSuccessCallback callback)
    {
		if(!isInited){
			Debug.Log("Facebook再次进行初始化");
			CallFBInit();
			return ;
		}
		
		loginSuccessCallback = callback;
		Debug.Log("Facebook CallFBLogin");
		if( FB.IsLoggedIn && !string.IsNullOrEmpty(FB.UserId) &&  !string.IsNullOrEmpty(FB.AccessToken)){
			Debug.Log("this game had facebook info,don't needed logined again");
			Debug.Log("FB.UserId:"+FB.UserId
							+" FB.AccessToken"+FB.AccessToken);
			if(loginSuccessCallback != null){
				loginSuccessCallback();
				loginSuccessCallback = null;
			}
		}else{
			BoxManager.showProcessMessage("正在登陆Facebook");
			Debug.Log("正在登陆Facebook");
        	FB.Login("email,publish_actions", LoginCallback);
		}
		
    }

    private static void LoginCallback(FBResult result)
    {
		Debug.Log("facebook login callback");
        if (result.Error != null){
			BoxManager.showMessage("FaceBook 登陆出错","登陆信息");
            lastResponse = "Fb Error Response:\n" + result.Error;
			Debug.Log(lastResponse);
		}
        else if (!FB.IsLoggedIn){
			BoxManager.showMessage("FaceBook 登陆失败","登陆信息");
            lastResponse = "Fb Login cancelled by Player";
			Debug.Log(lastResponse);
        }else{
			BoxManager.removeMessage();
			Debug.Log("facebook login success");
			Debug.Log("FB.UserId:"+FB.UserId
							+" FB.AccessToken"+FB.AccessToken);			
			if(loginSuccessCallback != null){
				loginSuccessCallback();
				loginSuccessCallback = null;
			}
		}
		
		
    }
		
   	public static void CallFBLogout()
    {	
		if(FB.IsLoggedIn){
			Debug.Log("Fb is Logged,User Logout");
        	FB.Logout();
		}
		UseFbLogin = false;
    }
    #endregion
		
}
