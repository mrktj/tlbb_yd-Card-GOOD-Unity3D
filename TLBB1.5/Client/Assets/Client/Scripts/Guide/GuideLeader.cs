using UnityEngine;
using System.Collections;
using Games.CharacterLogic;

public class GuideLeader : GuideBasic {
	
	public static int[] guide_leader_texts = 
	{
		//3002,//"这位少侠，我看你气质不凡，定是个中高手。不过无量山中龙蛇混杂，一不小心便会群战四起，你孤身一人恐怕多有不便。还是让我来帮你做些准备吧！",
		//3003,//"首先我们需要一位首领，这样遇到敌人我们就不会乱了阵脚。这位女侠善解人意，又通晓武学，不如就让她为少侠分忧吧！",
		3051,//	new新手描述3001阿朱	少侠青睐有加，阿朱必定全力相助！我来带领队伍能全面提升上阵侠士的[6BCFCF]闪避几率[F1ECCF]，以灵巧之术搏强劲之力！	null																																		
		3052,//	new新手描述3001王语嫣	少侠青睐有加，语嫣必定全力相助！我来带领队伍能全面提升上阵侠士的[6BCFCF]攻击力[F1ECCF]，通晓武学，无往不利！	null																																		
		3053,//	new新手描述3001梦姑	少侠青睐有加，梦姑必定全力相助！我来带领队伍能全面提升上阵侠士的[6BCFCF]攻击力[F1ECCF]，底蕴深厚，所向披靡！	null
	};
	public enum GUIDE_LEADER_STEP{
		NONE = -1,
		SELECT_1,//主页按钮队长
		SELECT_2,//选择队长
		LABEL_1,//选择队长弹出框
		END,
	}
	
	private static GuideLeader _instance;
	public static GuideLeader Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GuideLeader)) as GuideLeader;
				if(!_instance)
				{
					GameObject gm = new GameObject("GuideLeader");
					_instance = gm.AddComponent(typeof(GuideLeader)) as GuideLeader;

				}
			}
			return _instance;			
		}

	}
	
	public void Init(GuidePlayer gp, MainUILogic mul){
		InitGuide(gp, mul);
		curstep = (int)GUIDE_LEADER_STEP.SELECT_1;
		Debug.LogWarning("***GUIDE*** Init Guide Leader");
	}
	
	public void PlayGuide(int guideShowMode){
		//guideShowMode无用，此处游戏中断处理相同
		labelIndex = 0;
		player.SetPlayerActive(true);
		NextStep();
		player.nextStep = null;
		Debug.LogWarning("***GUIDE*** Init Play Guide Leader");
	}
	
	
	
	
	
	public void NextStep(){
		switch((GUIDE_LEADER_STEP)curstep){
			
		case GUIDE_LEADER_STEP.NONE:						break;//error
		default:
			StartCoroutine(ShowGuideLeader((GUIDE_LEADER_STEP)curstep));
			break;
			
		}
		
		curstep++;
	}
	public void ButtonNextStep(GameObject Button){
		NextStep();
	}
	IEnumerator ShowGuideLeader(GUIDE_LEADER_STEP currentstep){
		
		GameObject main1 = null;
		GameObject obj=null;
		
		switch(currentstep){
			
		case GUIDE_LEADER_STEP.SELECT_1://主页按钮队长
			
			player.nextStep = null;
			player.DisableRowZhao();
			//队长按钮
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.TeamController);
				yield return new WaitForSeconds(waitSecond);
			}
			TeamController teamController = main1.GetComponent<TeamController>();
			while(obj == null){
				obj = teamController.getGuideHeadButton().gameObject;
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);
			UpdateKeyObject();
			//下一步消息触发位于TeamController（186，13）
			//箭头位置设置
			player.SetRowPositionGPS(keyObject);
			
			break;
			
		case GUIDE_LEADER_STEP.SELECT_2://选择队长
			
			player.nextStep = null;
			player.DisableRowZhao();
			RecoverKeyObject();
			//寻找队长item
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.SelectTeamHeaderController);
				yield return new WaitForSeconds(waitSecond);
			}
			SelectTeamHeaderController hc = main1.GetComponent<SelectTeamHeaderController>();
			while(obj == null){
				obj = hc.getGuideItem();
				yield return new WaitForSeconds(waitSecond);
			}
			tempDraggableGO = hc.gameObject;
			SetKeyObj(obj);
			//设置队长item可点
			UpdateKeyObject();
			hc.SetPanelListEnable(false);
			//下一步消息触发位于TeamController（253，13）
			//箭头位置设置
			player.SetRowPositionGS(keyObject);
			
			break;
			
		case GUIDE_LEADER_STEP.LABEL_1://选择队长弹出框
			
			//player.ShowLabel(guide_leader_texts[labelIndex++]);
			if(tempDraggableGO != null)
				tempDraggableGO.GetComponent<SelectTeamHeaderController>().SetPanelListEnable(true);
			RecoverKeyObject();
			player.DisableRowZhao();
			int templateID = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(Obj_MyselfPlayer.GetMe().teamMemberArray[0]).templateID;
			switch(templateID){
			case 1008://王语嫣
				ShowLabel(0, guide_leader_texts[1]);	break;
			case 1012://阿朱
				ShowLabel(0, guide_leader_texts[0]);	break;
			case 1016://梦姑
				ShowLabel(0, guide_leader_texts[2]);	break;
			}
			//ShowLabel(0, "（队长技能）原用生平所学，踏上寻夫之旅！");
			player.nextStep = NextStep;
			
			break;
			
		case GUIDE_LEADER_STEP.END:
			
			GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.LEADER);
			//check下一步引导
			GuideManager.Instance.checkGuideState();
			//删除指引脚本
			Destroy(Instance.gameObject);
			
			break;
			
		}
		
	}
}
