using UnityEngine;
using System;
using System.Collections;

public class QuitApplication : MonoBehaviour {
#if UNITY_ANDROID
	private const string QUIT_IS_SHOW = "quit_is_show";
	private UIPanel QuitPanel;
	// Use this for initialization
	void Start () {
		QuitPanel = GetComponent<UIPanel>();
		int isQuitShow = PlayerPrefs.GetInt(QUIT_IS_SHOW);
		if(isQuitShow == 1){
			QuitPanel.alpha = 1;
		}else{
			QuitPanel.alpha = 0;
		}
	}
	
	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			if(AndroidConfig.is91Channel()){
				PaySystemInterface.doSdk("show91QuitPanel","");
			}else{
				//Time.timeScale = 0;
			    QuitPanel.alpha = 1;
			    //SoundManager.Instance.PauseAllSound();
			    showQuitPrefs();
			}
			
		}
	}
	
	void IsQuitApplication()
	{
		//StartCoroutine("doit");
		try
		{
#if UNITY_ANDROID
		if(AndroidConfig.isUCChannel())
		{
			//整合UC渠道代码 lihao_yd  2013-11-25
			// UC登录//
			//UCGameSdk.exitSDK();
				
		    PaySystemInterface.doSdk("exitSDK","");
		}
#endif
			GameManager.Instance.OnApplicationQuit();
		}catch(Exception e){
			Debug.LogError("QuitApplication error: "+e.ToString());
		}finally{
			hideQuitPrefs();
			Application.Quit();
			//Time.timeScale = 0;
		}
	}

	void CancelQuitApplication()
	{
		//Time.timeScale = 1;
		QuitPanel.alpha = 0;
		int isQuitShow = PlayerPrefs.GetInt(QUIT_IS_SHOW);
		hideQuitPrefs();
		//SoundManager.Instance.PlayAllSound();
	}
	
	private static void setQuitPrefs(bool hide){
		int quit = hide ? 1 : 0;
		int isQuitShow = PlayerPrefs.GetInt(QUIT_IS_SHOW);
		if(isQuitShow == quit){
			PlayerPrefs.SetInt(QUIT_IS_SHOW, 1 - quit);
			PlayerPrefs.Save();
		}
	}
	
	public static void hideQuitPrefs(){
		setQuitPrefs(true);
	}
	public static void showQuitPrefs(){
		setQuitPrefs(false);
	}
	//IEnumerator doit()
	//{
	//	yield return new WaitForSeconds(0.2f);
	//	try
	//	{
	//		GameManager.Instance.OnApplicationQuit();
	//	}catch(Exception e){
	//		Debug.LogError("QuitApplication error: "+e.ToString());
	//	}finally{
	//		Application.Quit();
	//	}
	//}
#endif
}
