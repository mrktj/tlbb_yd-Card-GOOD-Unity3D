using UnityEngine;
using System.Collections;

using card.net;

public class GuideGift : GuideBasic {

	public static int[] guide_wls_texts = 
	{
//		3023,//"这位少侠，我看你气质不凡，定是个中高手。不过无量山中龙蛇混杂，一不小心便会群战四起，你孤身一人恐怕多有不便。还是让我来帮你做些准备吧！",
		3066,//	new新手描述3014	少侠威武！你已战胜了无量山各路高手，快去领取[6BCFCF通关奖赏[1E201F]吧！每次通关都将获得惊喜奖赏哦！
		3067,//	new新手描述3015	大事不妙！听闻有人要陷害段公子一行人，事不宜迟，我们速速前往万劫谷营救吧！
	};
	public enum GUIDE_GIFT_STEP{
		NONE = -1,
		LABEL_1,//领赏出框
		SELECT_1,//主页奖赏按钮
		
		NONE_1,//隐藏箭头期
		
		SELECT_2,//领取奖赏按钮
		//NONE_1,//弹出框闲置期
		LABEL_2,//更多挑战弹出框
		END,
	}
	
	private static GuideGift _instance;
	public static GuideGift Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GuideGift)) as GuideGift;
				if(!_instance)
				{
					GameObject gm = new GameObject("GuideGift");
					_instance = gm.AddComponent(typeof(GuideGift)) as GuideGift;

				}
			}
			return _instance;			
		}

	}
	
	public void Init(GuidePlayer gp, MainUILogic mul){
		InitGuide(gp, mul);
		curstep = (int)GUIDE_GIFT_STEP.LABEL_1;
	}
	
	public void PlayGuide(int guideShowMode){
		labelIndex = 0;
		player.SetPlayerActive(true);
		NextStep();
		player.nextStep = NextStep;
	}
	
	
	
	
	public void NextStep(){
		switch((GUIDE_GIFT_STEP)curstep){
			
		case GUIDE_GIFT_STEP.NONE:						break;//error
		default:
			StartCoroutine(ShowGuideLeader((GUIDE_GIFT_STEP)curstep));
			break;
			
		}
		
		curstep++;
	}
	public void ButtonNextStep(GameObject Button){
		NextStep();
	}
	IEnumerator ShowGuideLeader(GUIDE_GIFT_STEP currentstep){
		
		GameObject main1 = null;
		GameObject obj=null;
		
		switch(currentstep){
			
		case GUIDE_GIFT_STEP.LABEL_1:
			
			//ShowLabel(0, "（领取奖赏）江湖不乏财大气粗却弱不禁风之人。虽然无量山未见到夫君，却也可领些赏钱，以备日后不时之需");
			ShowLabel(0, guide_wls_texts[labelIndex++]);
			
			break;
			
		case GUIDE_GIFT_STEP.SELECT_1://主页奖赏按钮
			
			player.DisableRowZhao();
			//寻找队长按钮
			player.nextStep = null;	
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.TeamController);
				yield return new WaitForSeconds(waitSecond);
			}
			TeamController teamController = main1.GetComponent<TeamController>();
			while(obj == null){
				obj = teamController.getGuideMissionButton();
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);
			//设置队长按钮可点
			UpdateKeyObject();
			//下一步消息触发位于TeamController（157，13）
			//箭头位置设置
			player.SetRowPositionPS(keyObject);
			
			break;
			
		case GUIDE_GIFT_STEP.NONE_1:
			
			RecoverKeyObject();
			player.DisableRowZhao();
			player.nextStep = null;	
			
			break;
			
		case GUIDE_GIFT_STEP.SELECT_2:
			
			RecoverKeyObject();
			player.nextStep = null;
			player.DisableRowZhao();
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.TaskController);
				yield return new WaitForSeconds(waitSecond);
			}
			TaskController main = main1.GetComponent<TaskController>();
			while(obj == null){
				obj = main.GetGuideItem();
				yield return new WaitForSeconds(waitSecond);
			}
			tempDraggableGO = main.gameObject;
			SetKeyObj(obj);
			UpdateKeyObject();
			main.SetPanelListEnable(false);
			//下一步消息触发位于TaskController（320，13）
			//箭头位置设置
			player.SetRowPositionGPS(keyObject);
			
			break;
			
		case GUIDE_GIFT_STEP.LABEL_2:
			
			//player.ShowLabel(guide_leader_texts[labelIndex++]);
			if(tempDraggableGO != null)
				tempDraggableGO.GetComponent<TaskController>().SetPanelListEnable(true);
			RecoverKeyObject();
			player.DisableRowZhao();
			//ShowLabel(0, "（更多挑战弹出框）打遍无量山，却没有他的踪影，只好继续找下去");
			ShowLabel(0, guide_wls_texts[labelIndex++]);
			player.nextStep = NextStep;
			
			break;
			
		case GUIDE_GIFT_STEP.END:
			
			GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.GIFT);
			player.DisableRowZhao();
			mainLogic.ReturnToMainUI();
			
			break;
			
		}
		
	}
}
