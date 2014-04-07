using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WebViewBox : MonoBehaviour {
	
    public enum WebViewType
    {
        DEFAULT,               // 默认 
		RAIDER,                 // 指南
        NOTICE,                 // 公告
    }

    public enum WebViewHeightType 
    {
        HEIGHT_480,
        HEIGHT_960,
        HEIGHT_1136,
        HEIGHT_1024,
        HEIGHT_2048,
/*#if UNITY_ANDROID
		HEIGHT_1920,
		HEIGHT_1280,
		HEIGHT_800,
		HEIGHT_854,
		HEIGHT_640,
#endif*/
    }

#if UNITY_ANDROID
	private const bool isActivityWebview = false;
#endif
    public GameObject DefaultPanel;
    public GameObject NoticePanel;
	public GameObject RaiderPanel;
	public GameObject sprite;
	
	private string url = "about:blank";
	
	private int top = 320;
	private int bottom = 200;
    private int left = Screen.width / 10;
	private int right = Screen.width / 10;

    static Dictionary<WebViewType, Dictionary<WebViewHeightType, Rect>> WebWindowRects = null;

	void Start () {
#if UNITY_ANDROID
		gameObject.SetActive(false);
#endif
		Debug.Log("ScreenHeight : "+Screen.height);
		Debug.Log("ScreenWidth  : "+Screen.width);
	}
	
	void Update () {}
	
	void OnEnable(){}
	
	void OnDisable()
    {
#if UNITY_IPHONE		
	    WebMediator.Hide();
	    // Clear the state of the web view (by loading a blank page).
	    WebMediator.LoadUrl("about:blank");
#elif UNITY_ANDROID
		if(isActivityWebview)
		{
			WebMediator.Hide();
			WebMediator.LoadUrl("about:blank");
		}
		else
		{
			WebMediator.HideWebView();
		}
#endif		
	}
	
	public void init(string urlstr)
	{
		url = urlstr;
	}

    public void ClearWindow() 
    {
        DefaultPanel.SetActive(false);
        NoticePanel.SetActive(false);
    }

	public void setRect(int top, int bottom, int left, int right)
	{
		this.top = top;
		this.bottom = bottom;
		this.left = left;
		this.right = right;
	}
	
    public void OpenWebWinow(WebViewType type,string strURL) 
    {
        url = strURL;
        ClearWindow();
        switch (type)
        {
            case WebViewType.DEFAULT:
                OpenDefaultWeb();
                break;
            case WebViewType.NOTICE:
                OpenPublicNoticeWeb();
                break;
            case WebViewType.RAIDER:
                OpenRauderWeb();
                break;
        }
    }
#if UNITY_IPHONE
	void InitPosition(iPhoneGeneration generation){
		switch(generation){
		case iPhoneGeneration.iPhone:
		case iPhoneGeneration.iPhone3G:
		case iPhoneGeneration.iPhone3GS:
		case iPhoneGeneration.iPodTouch1Gen:
		case iPhoneGeneration.iPodTouch2Gen:
		case iPhoneGeneration.iPodTouch3Gen:
		//普通屏 320像素 x 480像素 iPhone 1、3G、3GS，iPod Touch 1、2、3
			top=114;
			bottom=141;
			left=23;
			right=23;
			break;
		case iPhoneGeneration.iPhone4:
		case iPhoneGeneration.iPhone4S:
		case iPhoneGeneration.iPodTouch4Gen:
		//3：2 Retina 屏 640像素 x 960像素 iPhone 4、4S，iPod Touch 4
			top=229;
			bottom=282;
			left=46;
			right=46;
			break;
		case iPhoneGeneration.iPhone5:
		case iPhoneGeneration.iPodTouch5Gen:
		//16：9 Retina 屏 640像素 x 1136像素 iPhone 5，iPod Touch 5
			top=317;
			bottom=370;
			left=46;
			right=46;
			break;
		case iPhoneGeneration.iPad1Gen:
		case iPhoneGeneration.iPad2Gen:
		case iPhoneGeneration.iPadMini1Gen:
		//普通屏 768像素 x 1024像素 iPad 1， iPad2，iPad mini
			top=261;
			bottom=314;
			left=110;
			right=110;
			break;
		case iPhoneGeneration.iPad3Gen:
		case iPhoneGeneration.iPad4Gen:
		//Retina屏 1536像素 x 2048像素 New iPad，iPad 4
			top=522;
			bottom=628;
			left=220;
			right=220;
			break;
		case iPhoneGeneration.iPhoneUnknown:
		//Retina屏  for 5s,5c //
			top=317;
			bottom=370;
			left=46;
			right=46;
			break;
		case iPhoneGeneration.iPadUnknown:
		//Retina屏  for ipad air(5) , mini2  //
			top=522;
			bottom=628;
			left=220;
			right=220;
			break;
		case iPhoneGeneration.iPodTouchUnknown:
			break;
		default:
			break;
		}
	}
	void InitPositionByScale(){
		int hei = Screen.height;
		switch(hei){
		case 480:
			top=114;
			bottom=141;
			left=23;
			right=23;
			break;
		case 960:
			top=229;
			bottom=282;
			left=46;
			right=46;
			break;
		case 1024:
			top=261;
			bottom=314;
			left=110;
			right=110;
			break;
		case 1136:
			top=317;
			bottom=370;
			left=46;
			right=46;
			break;
		case 2048:
			top=522;
			bottom=628;
			left=220;
			right=220;
			break;
		}
	}
#elif UNITY_ANDROID
	void InitPosition(){
		int h = Screen.height;
		int w = Screen.width;
		// 根据1136*768， 中间区域宽494高356 //
		float viewH = h * 0.31338f;
		float offH = h * 0.0493f;
		top = (int)(((h - viewH) - offH)*0.5f);
		bottom = (int)(top + offH);
		left = (int)((w - w * 0.643229f)*0.5f);
		right = left;
		/*
		top = ((h - 356) >> 1)-28;
		bottom = top + 56;
		left = (w - 494) >> 1;
		right = left;
		if(w > 320)
		{
			top = ((h - 356) >> 1)-28;
		    bottom = top + 56;
			left = (w - 494) >> 1;
		    right = left;
		}
		else
		{
			top=(int)(h*0.2f);
		    bottom=(int)(w*0.7f);
		    left=(int)(w*0.1f);
		    right=(int)(w*0.1f);
		}*/
	}
#endif
	public void OpenRauderWeb()
    {
#if UNITY_IPHONE
        //InitPosition(iPhoneSettings.generation);		
		InitPositionByScale();
        WebMediator.LoadUrl(url);
        WebMediator.SetMargin(left, top, right, bottom);
        WebMediator.Show();
#elif  UNITY_ANDROID
		if(isActivityWebview)
		{
			InitPosition();
			WebMediator.LoadUrl(url);
	    	WebMediator.SetMargin(left, top, right, bottom);
	    	WebMediator.Show();
		}
		else
		{
			WebMediator.ShowWebView(url);
		}
#endif
#if UNITY_IPHONE
        DefaultPanel.SetActive(false);
		NoticePanel.SetActive(false);
		RaiderPanel.SetActive(true);
#elif UNITY_ANDROID
        DefaultPanel.SetActive(false);
		NoticePanel.SetActive(false);
		RaiderPanel.SetActive(false);
#endif
        //Debug.Log("Enter OpenWeb!");
        //Debug.Log("In OpenWeb : url="+url);
        //Debug.Log("In OpenWeb : top="+top+" ; bottom="+bottom+" ; left="+left+" ; right="+right);
	}
	
	public void HideWebView()
	{
#if UNITY_IPHONE
        //InitPosition(iPhoneSettings.generation);	
		InitPositionByScale();
        WebMediator.SetMargin(left, top, right, bottom);
		WebMediator.Show();
#endif

#if UNITY_IPHONE
        NoticePanel.SetActive(true);
		DefaultPanel.SetActive(false);
		RaiderPanel.SetActive(false);
		sprite.SetActive(true);
#endif
	}
	
	public void OpenPublicNoticeWeb() 
    {
/*
        WebViewHeightType hType = WebViewHeightType.HEIGHT_1136;
#if UNITY_IPHONE
        hType = GetCurrentHeightTyp(iPhoneSettings.generation);
#elif  UNITY_ANDROID
		//hType = GetCurrentHeightTyp();
#endif 
        Rect rt = GetWebViewRect(hType, WebViewType.NOTICE);
#if UNITY_IPHONE
        WebMediator.LoadUrl(url);
        WebMediator.SetMargin((int)rt.xMin, (int)rt.yMin, (int)rt.width, (int)rt.height);
	    WebMediator.Show();
#elif  UNITY_ANDROID
		if(isActivityWebview)
		{
			WebMediator.LoadUrl(url);
            WebMediator.SetMargin((int)rt.xMin, (int)rt.yMin, (int)rt.width, (int)rt.height);
	        WebMediator.Show();
		}
		else
		{
			WebMediator.ShowWebView(url);
		}
#endif
*/
#if UNITY_IPHONE
        //InitPosition(iPhoneSettings.generation);	
		InitPositionByScale();
        WebMediator.LoadUrl(url);
        WebMediator.SetMargin(left, top, right, bottom);
        WebMediator.Show();
#elif  UNITY_ANDROID
		if(isActivityWebview)
		{
			InitPosition();
			WebMediator.LoadUrl(url);
            WebMediator.SetMargin(left, top, right, bottom);
	        WebMediator.Show();
		}
		else
		{
			WebMediator.ShowWebView(url);
		}
#endif

#if UNITY_IPHONE
        NoticePanel.SetActive(true);
		DefaultPanel.SetActive(false);
		RaiderPanel.SetActive(false);
#elif UNITY_ANDROID
        DefaultPanel.SetActive(false);
		NoticePanel.SetActive(false);
		RaiderPanel.SetActive(false);
#endif
    }
	
    public void OpenDefaultWeb()
    {
		left = 0;	right = 0;	bottom = 0;
#if UNITY_IPHONE
		if (Screen.height > 1136) {
			top = 132;
		} else {
			top = 66;
		}
		
		WebMediator.LoadUrl(url);
	    WebMediator.SetMargin(left, top, right, bottom);
	    WebMediator.Show();
#elif  UNITY_ANDROID
		if(isActivityWebview)
		{
			top = 66;
			WebMediator.LoadUrl(url);
	    	WebMediator.SetMargin(left, top, right, bottom);
	    	WebMediator.Show();
		}
		else
		{
			WebMediator.ShowWebView(url);
		}
#endif
#if UNITY_IPHONE
        DefaultPanel.SetActive(true);
		NoticePanel.SetActive(false);
		RaiderPanel.SetActive(false);
#elif UNITY_ANDROID
        DefaultPanel.SetActive(false);
		NoticePanel.SetActive(false);
		RaiderPanel.SetActive(false);
#endif
	}

	public void OnCancel()
    {
		KDTLWebView.removeWebView();
#if  UNITY_ANDROID
		WebMediator.HideWebView();
#endif
	}
#if UNITY_IPHONE
    WebViewHeightType GetCurrentHeightTyp(iPhoneGeneration generation)
    {
        switch (generation)
        {
            case iPhoneGeneration.iPhone:
            case iPhoneGeneration.iPhone3G:
            case iPhoneGeneration.iPhone3GS:
            case iPhoneGeneration.iPodTouch1Gen:
            case iPhoneGeneration.iPodTouch2Gen:
            case iPhoneGeneration.iPodTouch3Gen:
                //普通屏 320像素 x 480像素 iPhone 1、3G、3GS，iPod Touch 1、2、3
                return WebViewHeightType.HEIGHT_480;
            case iPhoneGeneration.iPhone4:
            case iPhoneGeneration.iPhone4S:
            case iPhoneGeneration.iPodTouch4Gen:
                //3：2 Retina 屏 640像素 x 960像素 iPhone 4、4S，iPod Touch 4
                return WebViewHeightType.HEIGHT_960;
            case iPhoneGeneration.iPhone5:
            case iPhoneGeneration.iPodTouch5Gen:
                //16：9 Retina 屏 640像素 x 1136像素 iPhone 5，iPod Touch 5
                return WebViewHeightType.HEIGHT_1136;
            case iPhoneGeneration.iPad1Gen:
            case iPhoneGeneration.iPad2Gen:
            case iPhoneGeneration.iPadMini1Gen:
                //普通屏 768像素 x 1024像素 iPad 1， iPad2，iPad mini
                return WebViewHeightType.HEIGHT_1024;
            case iPhoneGeneration.iPad3Gen:
            case iPhoneGeneration.iPad4Gen:
                //Retina屏 1536像素 x 2048像素 New iPad，iPad 4
                return WebViewHeightType.HEIGHT_2048;
            default:
                return WebViewHeightType.HEIGHT_1024;
        }
    }
#elif  UNITY_ANDROID
    WebViewHeightType GetCurrentHeightTyp()
    {
		return WebViewHeightType.HEIGHT_1136;
    }
#endif
    public Rect GetWebViewRect(WebViewHeightType hType,WebViewType wType) 
    {
        if (null == WebWindowRects)
            InitWebViewRect();

        if (WebWindowRects.ContainsKey(wType)) 
        {
            if (WebWindowRects[wType].ContainsKey(hType))
            {
                return WebWindowRects[wType][hType];
            }
        }

        return new Rect(0, 0, 0, 0);
    }

    public void InitWebViewRect() 
    {
        WebWindowRects = new Dictionary<WebViewType, Dictionary<WebViewHeightType, Rect>>();
        Dictionary<WebViewHeightType, Rect> heightRect = new Dictionary<WebViewHeightType, Rect>();
        //left top  right bottom 
        heightRect.Add(WebViewHeightType.HEIGHT_480, new Rect(0.0f, 66, 0.0f, 0.0f));
        heightRect.Add(WebViewHeightType.HEIGHT_960, new Rect(0, 66, 0, 0));
        heightRect.Add(WebViewHeightType.HEIGHT_1024, new Rect(0, 66, 0, 0));
        heightRect.Add(WebViewHeightType.HEIGHT_1136, new Rect(0, 66, 0, 0));
        heightRect.Add(WebViewHeightType.HEIGHT_2048, new Rect(0, 132, 0, 0));
#if  UNITY_ANDROID
		int h = Screen.height;
		int w = Screen.width;
		heightRect.Add(WebViewHeightType.HEIGHT_1136, new Rect(0, h*66/1136, 0, 0));
#endif
        WebWindowRects.Add(WebViewType.DEFAULT, heightRect);

        heightRect = new Dictionary<WebViewHeightType, Rect>();
        heightRect.Add(WebViewHeightType.HEIGHT_480, new Rect(36, 137, 36, 165));
        heightRect.Add(WebViewHeightType.HEIGHT_960, new Rect(73, 274, 73, 330));
        heightRect.Add(WebViewHeightType.HEIGHT_1024, new Rect(137, 306, 137, 362));
        heightRect.Add(WebViewHeightType.HEIGHT_1136, new Rect(73, 362, 73, 418));
        heightRect.Add(WebViewHeightType.HEIGHT_2048, new Rect(274, 612, 274, 724));
#if  UNITY_ANDROID
		heightRect.Add(WebViewHeightType.HEIGHT_1136, new Rect(w*0.095f, h*31866f, w*0.095f, h*0.368f));
#endif
        WebWindowRects.Add(WebViewType.RAIDER, heightRect);

        heightRect = new Dictionary<WebViewHeightType, Rect>();
        heightRect.Add(WebViewHeightType.HEIGHT_480, new Rect(GetNoticeReallyLeft(), GetNoticeReallyTop(), GetNoticeReallyRight(), GetNoticeReallyBottom()));
        heightRect.Add(WebViewHeightType.HEIGHT_960, new Rect(GetNoticeReallyLeft(), GetNoticeReallyTop(), GetNoticeReallyRight(), GetNoticeReallyBottom()));
        heightRect.Add(WebViewHeightType.HEIGHT_1024, new Rect(GetNoticeReallyLeft(), GetNoticeReallyTop(), GetNoticeReallyRight(), GetNoticeReallyBottom()));
        heightRect.Add(WebViewHeightType.HEIGHT_1136, new Rect(GetNoticeReallyLeft(), GetNoticeReallyTop(), GetNoticeReallyRight(), GetNoticeReallyBottom()));
        heightRect.Add(WebViewHeightType.HEIGHT_2048, new Rect(GetNoticeReallyLeft(), GetNoticeReallyTop(), GetNoticeReallyRight(), GetNoticeReallyBottom())); 
#if  UNITY_ANDROID
        heightRect.Add(WebViewHeightType.HEIGHT_1136, new Rect(GetNoticeReallyLeft(), GetNoticeReallyTop(), GetNoticeReallyRight(), GetNoticeReallyBottom()));
#endif

        WebWindowRects.Add(WebViewType.NOTICE, heightRect);
    }

    //left top  right bottom 
    public float GetNoticeReallyLeft() 
    {
        return Screen.width / 2 - 550.0f * GetReallyRate() / 2.0f;
    }

    public float GetNoticeReallyTop() 
    {
        return Screen.height / 2.0f - 473 * GetReallyRate() / 2.0f;
    }

    public float GetNoticeReallyRight() 
    {
        return Screen.width / 2 - 550.0f * GetReallyRate() / 2.0f;
    }

    public float GetNoticeReallyBottom() 
    {
        return Screen.height / 2 - 473.0f * GetReallyRate() / 2.0f;
    }

    public float GetReallyRate() 
    {
        WebViewHeightType hType = WebViewHeightType.HEIGHT_1136;
#if UNITY_IPHONE
        hType = GetCurrentHeightTyp(iPhone.generation);
#elif  UNITY_ANDROID
		hType = GetCurrentHeightTyp();
#endif
        switch (hType) 
        {
            case WebViewHeightType.HEIGHT_1024:
            case WebViewHeightType.HEIGHT_1136:
            case WebViewHeightType.HEIGHT_480:
            case WebViewHeightType.HEIGHT_960:
                return 1;
            case WebViewHeightType.HEIGHT_2048:
                return 2;
        }
        return 1;
    }
}

