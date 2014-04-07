using UnityEngine;
using System.Collections;

public class KDTLWebView: MonoBehaviour{
	
	public static GameObject topFrame;
	const int box_z = -70;
	
	
	private string url_str = "about:blank";
	

	void Start () 
	{
		
	}
	
	void OnEnable(){
		
	}
	void OnDisable(){
		
	}
	public enum webViewMode{
		MESSAGEBOX = 0,
		RAIDER,
        NOTICE,
	}
	
	public static void removeWebView()
	{
		if(topFrame!=null)
		{
			GameObject.Destroy(topFrame);
			topFrame = null;
		}
	}	
	
	public static void ShowWebView(string url)
	{
		ShowWebView(url, webViewMode.MESSAGEBOX);
	}
	
	public static void ShowWebView(string url, webViewMode mode){
#if UNITY_EDITOR
#else
		WebMediator.LoadUrl("about:blank");
		Debug.Log("Enter ShowWebView!");
		removeWebView();
		
		topFrame = ResourceManager.Instance.LoadPopUp("WebViewBox");//(GameObject)GameObject.Instantiate(messageBox);
		topFrame.transform.parent =  Camera.mainCamera.transform;
		topFrame.transform.localScale = Vector3.one;
		topFrame.transform.localPosition = new Vector3(0,0,box_z);
		
		WebViewBox wvb = topFrame.GetComponent<WebViewBox>();
		Debug.Log("ShowWebView url = "+url);

        if (wvb)
        {
            switch (mode)
            {
                case webViewMode.MESSAGEBOX:
                    wvb.OpenWebWinow(WebViewBox.WebViewType.DEFAULT, url);
                    break;
                case webViewMode.RAIDER:
                    wvb.OpenWebWinow(WebViewBox.WebViewType.RAIDER, url);
                    break;
                case webViewMode.NOTICE:
                    wvb.OpenWebWinow(WebViewBox.WebViewType.NOTICE, url);
                    break;
            }
        }
        else
			Debug.Log("WebViewBox is NULL!!!");
#endif
	}
	
	public static void ShowPreWebView() {
		#if UNITY_EDITOR
#else
		removeWebView();
		
		topFrame = ResourceManager.Instance.LoadPopUp("WebViewBox");
		topFrame.transform.parent =  Camera.mainCamera.transform;
		topFrame.transform.localScale = Vector3.one;
		topFrame.transform.localPosition = new Vector3(0,0,box_z);
		
		WebViewBox wvb = topFrame.GetComponent<WebViewBox>();
        if (wvb)
        {
            wvb.HideWebView();
        }
        else
			Debug.Log("WebViewBox is NULL!!!");

#endif
	}
	
	public static void PreWebView(string url){
#if UNITY_EDITOR
#else
		WebMediator.LoadUrl(url);
		WebMediator.Hide();

#endif
	}
	

	
#if UNITY_ANDROID
	public static void ShowWebView(string url,WebMediator.CallBackCloseWebview OnCloseWebView){
		ShowWebView(url, webViewMode.MESSAGEBOX,OnCloseWebView);
	}
	
	public static void ShowWebView(string url, webViewMode mode,WebMediator.CallBackCloseWebview OnCloseWebView){
#if UNITY_EDITOR
#else
		Debug.Log("Enter ShowWebView!");
		removeWebView();
		
		topFrame = ResourceManager.Instance.LoadPopUp("WebViewBox");//(GameObject)GameObject.Instantiate(messageBox);
		topFrame.transform.parent =  Camera.mainCamera.transform;
		topFrame.transform.localScale = Vector3.one;
		topFrame.transform.localPosition = new Vector3(0,0,box_z);
		
		WebViewBox wvb = topFrame.GetComponent<WebViewBox>();
		Debug.Log("ShowWebView url = "+url);

        if (wvb)
        {
			WebMediator.SetOnCallBackCloseWebView(OnCloseWebView);
            switch (mode)
            {
                case webViewMode.MESSAGEBOX:
                    wvb.OpenWebWinow(WebViewBox.WebViewType.DEFAULT, url);
                    break;
                case webViewMode.RAIDER:
                    wvb.OpenWebWinow(WebViewBox.WebViewType.RAIDER, url);
                    break;
                case webViewMode.NOTICE:
                    wvb.OpenWebWinow(WebViewBox.WebViewType.NOTICE, url);
                    break;
            }
        }
        else
			Debug.Log("WebViewBox is NULL!!!");
#endif
	}
#endif	
}