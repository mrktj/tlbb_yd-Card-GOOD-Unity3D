using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using card.net;
using xjgame.message;

public class GuideManager : MonoBehaviour {
//	List<GameObject>  guides = new List<GameObject>();
	
//	public const string GUIDE_SAVE_KEY = "GUIDE";
	public const int BOX_POSITON_TOP = 512-332;
	public const int BOX_POSITON_MIDDLE = 512-474;
	public const int BOX_POSITON_BOTTOM = 512-576;
	// !!! 这个枚举不要轻易改动,统计模块需要判断状态 -- WML
	public enum GUIDE_STEP
	{
        EMPTY           = -1,
		NONE 			= 0,
		FIRST_COPY		= 200,//演示战斗
		CARD_CHOOSE 	= 100,//主卡牌选择
		LEADER 			= 101,//设置队长
		TEAM_MEMBER 	= 102,//设置1号队员		
		COPY1_1 		= 201,//副本1-1						默认一个助战好友
		COPY1_1_END 	= 103,//副本1-1						结束不弹出好友申请界面
		COPY1_2 		= 202,//副本1-2
		COPY1_2_END		= 104,//副本1-2						弹出申请好友界面，进行申请解说
		COPY1_3 		= 203,//副本1-3
		COPY1_3_END		= 105,//副本1-3
		LOTTERY 		= 106,//抽奖
		TEAM_MEMBER2 	= 107,//设置2号队员			
		COPY1_4 		= 108,//副本1-4
		UPDATE 			= 109,//升级引导	
		COPY1_5 		= 110,//副本1-5		
		GIFT 			= 111,//奖赏引导		
		COPY2_1 		= 112,//副本2-1
//		COPY2_2 		= 113,//副本2-2
		END				= 900,//结束新手引导
		SKIP_GUIDE		= 901,//直接掉过，取得所有物品
	}
    public GUIDE_STEP currentStep = GUIDE_STEP.EMPTY;
	public bool isInSubStep = false;
	public GameObject rootPanel;
	
	public int selectTempletID = 0;
	public int selectedTempletID{
		
		get{
			if(selectTempletID == 0)
				selectTempletID = PlayerPrefs.GetInt(SAVE_GUIDE_TEMPLET_ID , 0);
			return selectTempletID;
		}
		set{
			selectTempletID = value;
			PlayerPrefs.SetInt(SAVE_GUIDE_TEMPLET_ID,value);
		}
		
	}
	public int lotteryTempletID = 0;
	private static GuideManager _instance;
	public static GuideManager Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GuideManager)) as GuideManager;
				if(!_instance)
				{
					GameObject gm = new GameObject("GuideManager");
					_instance = gm.AddComponent(typeof(GuideManager)) as GuideManager;
				}
			}
			return _instance;			
		}

	}
	
	public void init()
	{
		this.selectTempletID = PlayerPrefs.GetInt(SAVE_GUIDE_TEMPLET_ID);
	}
	//------------------------GAMING	=	0,		SUSPEND	=	1,---------------------
	public bool checkGuideState(int guideShowMode = 1)
	{
		Debug.Log("****** checkGuideState : "+currentStep);
		if(isInSubStep)
			return false;
		else
			isInSubStep = true;
		
		switch(currentStep)
		{
		case GUIDE_STEP.NONE:				break;
		case GUIDE_STEP.END:				break;
			
		default:
			GuideDirector.Instance.ShowGuide(currentStep , guideShowMode);
			return true;
		}
		return false;
	}



	public GuideDirector gd = null;
	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
		//GuideDirectorInit();
	}
	
	// Use this for initialization
	void Start () {

	}	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void resetRootPanel()
	{
		//rootPanel = Camera.mainCamera.transform.FindChild("GuideAnchor/GuidePanel").gameObject;
		//Debug.Log("rootPanel:"+rootPanel.name);
		
	}
	public bool needPrelude()
	{
		Debug.Log("check if need prelude");
		return currentStep == GUIDE_STEP.NONE;
	}
	public bool isEnd()
	{
		return currentStep == GUIDE_STEP.END;
	}
	//完成到某个Step 储存
	public void FinishedStep(GUIDE_STEP step)
	{
		Debug.Log("FinishedStep:"+step);
		isInSubStep = false;
		switch(step){
			
		case GUIDE_STEP.FIRST_COPY://演示战斗
			currentStep = GUIDE_STEP.CARD_CHOOSE;	break;
		case GUIDE_STEP.CARD_CHOOSE://主卡牌选择
			currentStep = GUIDE_STEP.LEADER;		break;
		case GUIDE_STEP.LEADER://设置队长
			currentStep = GUIDE_STEP.TEAM_MEMBER;	break;
		case GUIDE_STEP.TEAM_MEMBER://设置1号队员
			currentStep = GUIDE_STEP.COPY1_1;		break;
			
		case GUIDE_STEP.COPY1_1://副本1-1
			currentStep = GUIDE_STEP.COPY1_1_END;	break;
		case GUIDE_STEP.COPY1_1_END:
			currentStep = GUIDE_STEP.COPY1_2;		break;
			
		case GUIDE_STEP.COPY1_2://副本1-2
			currentStep = GUIDE_STEP.COPY1_2_END;	break;
		case GUIDE_STEP.COPY1_2_END:
			currentStep = GUIDE_STEP.COPY1_3;		break;
			
		case GUIDE_STEP.COPY1_3://副本1-3
			currentStep = GUIDE_STEP.COPY1_3_END;	break;
		case GUIDE_STEP.COPY1_3_END:
			currentStep = GUIDE_STEP.LOTTERY;		break;	
		case GUIDE_STEP.LOTTERY://抽奖
			currentStep = GUIDE_STEP.TEAM_MEMBER2;	break;
		case GUIDE_STEP.TEAM_MEMBER2://设置2号队员
			currentStep = GUIDE_STEP.COPY1_4;		break;			
		case GUIDE_STEP.COPY1_4://副本1-4	
			currentStep = GUIDE_STEP.UPDATE;		break;	
		case GUIDE_STEP.UPDATE://升级引导
			currentStep = GUIDE_STEP.COPY1_5;		break;
		case GUIDE_STEP.COPY1_5://副本1-5	
			currentStep = GUIDE_STEP.GIFT;			break;	
		case GUIDE_STEP.GIFT://奖赏引导	
			currentStep = GUIDE_STEP.COPY2_1;		break;	
		case GUIDE_STEP.COPY2_1://副本2-1
//			currentStep = GUIDE_STEP.COPY2_2;		break;
//		case GUIDE_STEP.COPY2_2://副本2-2
			currentStep = GUIDE_STEP.END;			break;
		case GUIDE_STEP.END://结束新手引导
			currentStep = GUIDE_STEP.END;			break;
			
		}
		Debug.Log("currentStep:"+currentStep);
//		SaveGuide(currentStep);
	}
	const string SAVE_GUIDE_TEMPLET_ID = "SAVE_GUIDE_TEMPLET_ID";

	public void SetTempletID(int idx)
	{
		this.selectedTempletID = idx;
		PlayerPrefs.SetInt(SAVE_GUIDE_TEMPLET_ID,idx);
	}
	
//--------------------NORMAL = 0--INTE = 1---------------------------
	public void guideTimeOut(int mode = 0)
	{
		if(mode == 0)
			isInSubStep = false;
		else if(currentStep != GUIDE_STEP.LEADER &&
			currentStep != GUIDE_STEP.TEAM_MEMBER &&
			currentStep != GUIDE_STEP.TEAM_MEMBER2)
		{
			isInSubStep = false;
		}
	//	gd.guidePlayer.SetPlayerActive(false);
	}
	
	
	
	public void CheckFlashBtn(GameObject go){
		TweenScale ts = go.GetComponent<TweenScale>();
		if(ts == null)
			ts = go.AddComponent<TweenScale>();
		ts.style = UITweener.Style.PingPong;
		ts.from = Vector3.one;
		ts.to = new Vector3(1.1f,1.1f,1.1f);
		ts.duration = 0.5f;
	}
	public void CheckStopFlashBtn(GameObject go){
		TweenScale ts = go.GetComponent<TweenScale>();
		if(ts == null)
			return;
		Destroy(ts);
		go.transform.localScale = Vector3.one;
	}
}
