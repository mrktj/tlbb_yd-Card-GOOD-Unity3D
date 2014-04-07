using UnityEngine;
using System.Collections;
using System.Collections.Generic;
////using Module.Log;
using GCGame.Table;  
//using PBMessage;
using card;
using card.net;
using System;
using Games.CharacterLogic;

using System.IO;
//using ProtoBuf;
using xjgame.message;

public class LoginLogic : MonoBehaviour {
	public GameObject windowPanel;
	private Dictionary<ChildIndex,GameObject> controllers;// = new Dictionary<ChildIndex,GameObject>();
	// ChildIndex名称需要跟PreFab名称一致
	enum ChildIndex
	{
		NONE = -1,
		SplashController,
		CardChooseController,
		CardShowController,
		GuideController,
	}
	private ChildIndex lastPreviousWindowIndex= ChildIndex.NONE;
	private ChildIndex currentWindowIndex = ChildIndex.NONE;
	
	//reset 在session过期的时候需要调用
	public static bool needResetLogin = false;
    private static bool IsNeedInitPlatform = true;
	void resetLoginWindow()
	{
		needResetLogin = false;
		currentWindowIndex = ChildIndex.NONE;
		lastPreviousWindowIndex= ChildIndex.NONE;
		GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.NONE;		
	}
	void Awake()
	{
        ///*
       
		

       //*/
		Debug.Log("login awake");
		AccountManager.IsUseAccount = (DeviceHelper.CHANNEL_PP != DeviceHelper.GetChannelID());
		
		controllers = new Dictionary<ChildIndex,GameObject>();
		GuideManager.Instance.resetRootPanel();
		if(needResetLogin)
		{
			resetLoginWindow();
		}

        if (IsNeedInitPlatform)
        {
            DeviceHelper.InitState();
            IsNeedInitPlatform = false;
        }
        
        if (GlobalSave.IsFirstSetup())
        {
            DeviceHelper.SetupAnalytics();
            GlobalSave.SetFirstSetup();
        }
		
		//fb初始化,如果这里初始化失败,会在登陆时再次初始化;
		FbHelper.CallFBInit();
	}
	void Start()
	{
		Debug.Log("login Start");
		
		//王明磊 统计 - 初始化 进入游戏和服务器列表按钮的点击次数 为了和上次的进入游戏和服务器列表按钮点击相区别
		//用Btn-1 Btn-2代表本次登录点击次数
		//用Btn1 Btn2代表上次登录点击次数
		
		PlayerPrefs.SetInt("Btn-1",0);
		PlayerPrefs.SetInt("Btn-2",0);
		//从战斗返回，这个时候已经是cardchoose了
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.CARD_CHOOSE)
		{
			ActiveWindow(ChildIndex.CardChooseController);
		}else
		{
			GameManager.Instance.initGame();
			AccountManager.Instance.initAccount();
            if (DeviceHelper.CHANNEL_PP == DeviceHelper.GetChannelID())
            {
                DeviceHelper.PPLogin();
            }
			
			//if(PlayerPrefs.HasKey("FlashPVEBtn"))	PlayerPrefs.DeleteKey("FlashPVEBtn");//Jack Wen 20130926
			ActiveWindow(ChildIndex.SplashController);
		}
		
		//播放刚进游戏的初始音乐
		AudioManager.Instance.PlayBackgroundMusic(SoundResource.SoundRes.enter.ToString());
//		AudioController.Play(SoundResource.SoundRes.enter.ToString());

	}
//	public String[] prefabs = new String(){"CardChooseController","CardShowController","SplashController","CardItem"};
	
	void OnSplashWindow()
	{
		ActiveWindow(ChildIndex.SplashController);
	}
	public void OnCardChooseWindow()
	{
		ActiveWindow(ChildIndex.CardChooseController);
	}
	void OnCardInfoWindow()
	{
		ActiveWindow(ChildIndex.CardShowController);
	}
	void OnGuideWindow()
	{
		ActiveWindow(ChildIndex.GuideController);
	}

	private void ActiveWindow(ChildIndex index)
    {
		if(!controllers.ContainsKey(index))
		{
			GameObject window = ResourceManager.Instance.LoadWindow(index.ToString());
			controllers.Add(index,window);
			window.transform.parent = windowPanel.transform;
			window.transform.localScale = Vector3.one;
			window.SetActive(true);
		}
	    foreach (ChildIndex key in controllers.Keys)
	    {
	        if (index == key)
	        {
				controllers[key].transform.position = Vector3.zero;
				controllers[key].SetActive(true);
				lastPreviousWindowIndex = currentWindowIndex;
				currentWindowIndex = index;
	        }
	        else
	        {
	            controllers[key].SetActive(false);
	        }
	    }		
    }

	public void backToPreviousWindow()
	{
		if(lastPreviousWindowIndex!=ChildIndex.NONE)
		{
			ActiveWindow(lastPreviousWindowIndex);
		}
	}
	

	void OnDestroy()
	{
		AccountManager.Instance.DestroyAllUI();
	}
	//test PB support
/* 	void Start()
	{
		testPBSupport();
	}
	public static CSLogin newMsg = null;
	void testPBSupport()
	{
		CSLogin msg = new CSLogin();
        msg.mid = GameManager.getMacAddress(); 
		msg.version = ClientConfigure.GetClientVersion();
		msg.country = "china";
		msg.device = "iPad";
		msg.prisonBreak = "false";
		MemoryStream msData = new MemoryStream();
		Serializer.Serialize(msData,msg);
		msData.Position = 0;
		
		newMsg = Serializer.Deserialize<CSLogin>(msData);
		
	}
	
	void OnGUI()
	{
		if(newMsg==null)
		{
			return;
		}
		GUI.Label(new Rect(10,100,100,30),newMsg.mid);
		GUI.Label(new Rect(10,130,100,30),newMsg.version);
		GUI.Label(new Rect(10,160,100,30),newMsg.country);
		GUI.Label(new Rect(10,190,100,30),newMsg.device);
		GUI.Label(new Rect(10,220,100,30),newMsg.prisonBreak);
	} */
	
	void OnDisable()
	{
//		ResourceManager.Instance.Clean();
//		AtlasManager.Instance.Clean();
	}
}
