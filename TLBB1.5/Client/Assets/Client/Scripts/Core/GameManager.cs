using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using card.net;
////using Module.Log;
using GCGame.Table;
using System.Net.NetworkInformation;
using Games.CharacterLogic;
//using Statistics;
using Prime31;
using System;

/*
 * 游戏主管理类
 * 使用getInstance()保证此类和GameObject的唯一性
 * 创建后,场景切换不会销毁此类
 */
public class GameManager : MonoBehaviour {
	private static GameManager _instance;
	//注意：在逻辑类的Awake里需调用Instance.initGame()已保证可以单场景调试
	
	public static GameManager Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
				if(!_instance)
				{
					GameObject gm = new GameObject("GameManager");
					_instance = gm.AddComponent(typeof(GameManager)) as GameManager;
					#if UNITY_IPHONE
					//初始化友盟统计
//					gm.AddComponent(typeof(MobclickAgent));
					#endif
				}
			}
			return _instance;			
		}

	}
	
	private static bool isLogin = false;
	public string sceneName="";
    public string tempName = "";
	public static void userLoginDone ()
	{
		Debug.Log("User Login Done !");
		isLogin = true;
	}

	public double globalTimeCount = 0;
	//public double statisticsTimeCount = 0;
	bool inited = false;
	public void initGame()
	{
		if(inited)
		{
			return;
		}
		inited = true;
		#if UNITY_IPHONE
		//初始化Flurry统计
		//FlurryBinding.startSession("XWWYSPMJ453SP89WFCWN");
		//delete;
		
		#endif
		//------------尝试接入消息包压缩库开始-----------
//		if(MRDCompressLib.MRDCompressLib_Init()!=0)
//		{
//			Debug.Log("MRD Compress Lib Init Successed!");
//		}
//		else
//		{
//			Debug.Log("MRD Compress Lib Init Failed!");
//		}
		//------------尝试接入消息包压缩库结束-----------		

		//禁用多点触摸
		Input.multiTouchEnabled = false;
        DeviceHelper.AutoLockScreen(false);//关闭自动锁屏

		GuideManager.Instance.init();
		Application.targetFrameRate = -1;
		DontDestroyOnLoad(transform.gameObject);
		//
		StartCoroutine(responseTick());		
	}
	public bool isGuideMode ()
	{
		int step = (int)GuideManager.Instance.currentStep;
		if (step != (int)GuideManager.GUIDE_STEP.END && step != (int)GuideManager.GUIDE_STEP.SKIP_GUIDE)
			return true;
		return false;
	}
	public void LoadLevel(string levelName)
	{
		//转到每个logic destroy的时候调用
		ResourceManager.Instance.Clean();
//		AtlasManager.Instance.Clean();
        tempName = levelName;
		StartCoroutine(LoadAsyncLevel(levelName));
#if UNITY_ANDROID
		// UC的悬浮按钮 //
		if(levelName.Equals(Utils.UI_NAME_main)){
			AndroidConfig.showFloatButton ();
		}else{
			AndroidConfig.hideFloatButton();
		}
#endif
	}
	public AsyncOperation async;
	IEnumerator LoadAsyncLevel(string levelName)
	{
//		BoxManager.showProcessMessage("请稍候..."); 
        if (!levelName.Equals(Utils.UI_NAME_Battle))         //战斗界面改为load 不需要小人//
        {
            BoxManager.showMessageByID((int)MessageIdEnum.Msg103);
        }
		async = Application.LoadLevelAsync(levelName);
        yield return async;
	}
	void Awake(){

	}
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey("InGameBackLogin"))//标记玩家是否在游戏中返回登录 -- 影响快速登录显示绑定界面
			PlayerPrefs.DeleteKey("InGameBackLogin");
		
#if UNITY_EDITOR
#elif UNITY_ANDROID
#else
		WebMediator.Install();
#endif
		//Application.LoadLevel("mainUI");
	}
	public DateTime lastPowerTime=DateTime.Now;
//	public float time=300.0f;//赠送体力倒计时
	// Update is called once per frame
	void Update () {
		//全局计时器
		globalTimeCount += Time.deltaTime;
		
		if (Obj_MyselfPlayer.GetMe().freeCD > 0)
			Obj_MyselfPlayer.GetMe().freeCD -= Time.deltaTime;
		else
			Obj_MyselfPlayer.GetMe().freeCD = 0;
		
		if (Obj_MyselfPlayer.GetMe().changeCardTimer > 0)
			Obj_MyselfPlayer.GetMe().changeCardTimer -= Time.deltaTime;
		else
			Obj_MyselfPlayer.GetMe().changeCardTimer = 0;
        if (async != null)
        {
            if (async.isDone)
            {
                sceneName = tempName;
                async = null;
            }
        }
		//王明磊 : 统计模块代码 -> Statistics
//		if (globalTimeCount - statisticsTimeCount >= 300)
//		{
//			if (GameManager.isLogin)
//				StatisticsUpload.Instance().uploadData();
//			else
//				Debug.Log("Dont Send Data");
//			statisticsTimeCount = globalTimeCount;
//		}
		
//		if(time<0)
//		{
//			time=300.0f;
//			time-=Time.deltaTime;
//			if(Obj_MyselfPlayer.GetMe().level>0&&Obj_MyselfPlayer.GetMe().power<TableManager.GetIdexperienceByID(Obj_MyselfPlayer.GetMe().level).IDPhysicalValue)
//			{
//				Obj_MyselfPlayer.GetMe().power++;
//				if(GameObject.FindWithTag("main_controller")!=null)
//				{
//					GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
//				}
//			}
//		}
//		else
//		{
//			time-=Time.deltaTime;
//		}
	}
	
	public IEnumerator responseTick()
	{
		while(true)
		{
			NetManager.tick();
			yield return new WaitForSeconds(1);
		}
	}
	//------------save start------------
	public const int MAX_CARD_PROTECT_COUNT = 300;
	public const string KEY_CARD_PROTECT = "KEY_CARD_PROTECT";
	public static void saveProtectedCardsID(string[] cardIDs)
	{
		for(int i=0;i<cardIDs.Length;i++)
		{
			Debug.Log("KEY_CARD_PROTECT:"+cardIDs[i]);
		}
		PlayerPrefsX.SetStringArray(KEY_CARD_PROTECT  + Obj_MyselfPlayer.GetMe().accountID, cardIDs);
	}
	public static string[] getProtectedCardsID()
	{
		return PlayerPrefsX.GetStringArray(KEY_CARD_PROTECT + Obj_MyselfPlayer.GetMe().accountID);
	}
	
	//------------save end------------
	//------------Get Mac Address start------------
	public static string getMacAddress()
	{
#if UNITY_EDITOR
		return "00000000";
#elif UNITY_ANDROID
		string macAddress = WebMediator.GetMacAddress();
		return macAddress;//
#else
		string macAddress = "00000000";
		NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
		if(nics==null)
		{
			return "00000000";
		}
		foreach(NetworkInterface adapter in nics)
		{
			PhysicalAddress address = adapter.GetPhysicalAddress();
			if(address.ToString()!="")
			{
				macAddress = address.ToString();
				return macAddress;
			}
		}
		return macAddress;
#endif
    }
	//------------Get Mac Address end------------
	public static bool isQuiting = false;
	public void OnApplicationQuit ()
	{
		isQuiting = true;
	}
}
