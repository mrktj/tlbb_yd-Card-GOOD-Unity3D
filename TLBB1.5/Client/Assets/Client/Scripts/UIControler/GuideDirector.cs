using UnityEngine;
using System.Collections;

public class GuideDirector : MonoBehaviour {
	
	//新手引导导演类：分别显示各个阶段所应执行流程，通过统一接口实现调度
	
	public GuidePlayer guidePlayer;
	
	public GameObject keyObject;//关键对象，指某步骤突出显示部分
	public MainUILogic mainLogic;
	
	public enum GUIDE_SHOW_MODE{
		GAMING	=	0,
		SUSPEND	=	1,
	}
	
	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool Init(){
		GameObject m;
		Transform g;
		m = GameObject.Find("MainUILogic");
		g = GameObject.FindWithTag("MainCamera").transform.FindChild("GuideAnchor/GuidePanel/GuidePlayer");
		if(m == null || g == null)
			return false;
		mainLogic = m.GetComponent<MainUILogic>();
		guidePlayer = g.GetComponent<GuidePlayer>();
		return true;
	}
	
	
	private static GuideDirector _instance;
	public static GuideDirector Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GuideDirector)) as GuideDirector;
				if(!_instance)
				{
					GameObject gm = new GameObject("GuideDirector");
					_instance = gm.AddComponent(typeof(GuideDirector)) as GuideDirector;
					GameObject anchor = GameObject.FindWithTag("MainCamera").transform.FindChild("GuideAnchor").gameObject;
					gm.transform.parent = anchor.transform;
					gm.transform.localScale = Vector3.one;
					
					GameObject window = ResourceManager.Instance.LoadWindow("GuidePlayer");
					window.name = "GuidePlayer";
					window.transform.parent = gm.transform;
					window.transform.localScale = Vector3.one;
					window.transform.localPosition = new Vector3(0,0,-200);
					
					Debug.LogWarning("GuideDirector's init finish");
				}
			}
			return _instance;			
		}

	}
	
	//供guidemanager调用接口
	public void ShowGuide(GuideManager.GUIDE_STEP currentStep , int showMode){
		
		guidePlayer = transform.FindChild("GuidePlayer").GetComponent<GuidePlayer>();
		if(currentStep != GuideManager.GUIDE_STEP.CARD_CHOOSE){
			mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		}
		
		switch(currentStep){
			
		case GuideManager.GUIDE_STEP.CARD_CHOOSE://主卡牌选择
			GuideChoose.Instance.Init(guidePlayer, mainLogic);
			GuideChoose.Instance.PlayGuide(showMode);
			break;
			
		case GuideManager.GUIDE_STEP.LEADER://设置队长
			GuideLeader.Instance.Init(guidePlayer, mainLogic);
			GuideLeader.Instance.PlayGuide(showMode);
			break;
			
		case GuideManager.GUIDE_STEP.TEAM_MEMBER://设置1号队员
			GuideMember.Instance.Init(guidePlayer, mainLogic);
			GuideMember.Instance.PlayGuide(showMode);
			break;
			
		case GuideManager.GUIDE_STEP.COPY1_1://副本1-1
			GuideCopy1_1.Instance.Init(guidePlayer, mainLogic);
			GuideCopy1_1.Instance.PlayGuide(showMode);
			break;
			
		case GuideManager.GUIDE_STEP.COPY1_1_END:
			GuideCopy1_1_End.Instance.Init(guidePlayer, mainLogic);
			GuideCopy1_1_End.Instance.PlayGuide(showMode);
			break;
			
		case GuideManager.GUIDE_STEP.COPY1_2://副本1-2
			GuideCopy1_2.Instance.Init(guidePlayer, mainLogic);
			GuideCopy1_2.Instance.PlayGuide(showMode);
			break;
			
		case GuideManager.GUIDE_STEP.COPY1_2_END:
			GuideCopy1_2_End.Instance.Init(guidePlayer, mainLogic);
			GuideCopy1_2_End.Instance.PlayGuide(showMode);
			break;
			
		case GuideManager.GUIDE_STEP.COPY1_3://副本1-3
			GuideCopy1_3.Instance.Init(guidePlayer, mainLogic);
			GuideCopy1_3.Instance.PlayGuide(showMode);
			break;	
			
		case GuideManager.GUIDE_STEP.LOTTERY://抽奖
			GuideLottery.Instance.Init(guidePlayer, mainLogic);
			GuideLottery.Instance.PlayGuide(showMode);
			break;
			
		case GuideManager.GUIDE_STEP.TEAM_MEMBER2://设置2号队员
			GuideMember2.Instance.Init(guidePlayer, mainLogic);
			GuideMember2.Instance.PlayGuide(showMode);
			break;	
			
		case GuideManager.GUIDE_STEP.COPY1_4://副本1-4	
			GuideCopy1_4.Instance.Init(guidePlayer, mainLogic);
			GuideCopy1_4.Instance.PlayGuide(showMode);
			break;	
			
		case GuideManager.GUIDE_STEP.UPDATE://升级引导
			GuideUpdate.Instance.Init(guidePlayer, mainLogic);
			GuideUpdate.Instance.PlayGuide();
			break;
			
		case GuideManager.GUIDE_STEP.COPY1_5://副本1-5	
			GuideCopy1_5.Instance.Init(guidePlayer, mainLogic);
			GuideCopy1_5.Instance.PlayGuide(showMode);
			break;
			
		case GuideManager.GUIDE_STEP.GIFT://奖赏引导	
			GuideGift.Instance.Init(guidePlayer, mainLogic);
			GuideGift.Instance.PlayGuide(showMode);
			break;	
			
		case GuideManager.GUIDE_STEP.COPY2_1://副本2-1
			GuideCopy2_1.Instance.Init(guidePlayer, mainLogic);
			GuideCopy2_1.Instance.PlayGuide(showMode);
			break;
			
		case GuideManager.GUIDE_STEP.END://结束新手引导
			break;
			
		}
	}
}
