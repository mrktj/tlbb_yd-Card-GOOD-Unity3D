using UnityEngine;
using System.Collections;

public class GuideMember2 : GuideBasic {

	public static int[] guide_member_texts = 
	{
//		3004,//"南海鳄神似乎跃跃欲试想要和那些无量剑的人比试一番，那就让他试试看吧，看看他到底有何能耐。",
		3063,//	new新手描述3011	[6BCFCF]上阵更多高品质侠士[1E201F]会让我们的团队实力大大提升！
	};
	public enum GUIDE_MEMBER_STEP
	{
		NONE = -1,
		SELECT_1,//主页队员按钮
		SELECT_2,//选择队员
		SELECT_3,
		LABEL_1,//选择队长提示框
		END,
	}
	
	private static GuideMember2 _instance;
	public static GuideMember2 Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GuideMember2)) as GuideMember2;
				if(!_instance)
				{
					GameObject gm = new GameObject("GuideMember2");
					_instance = gm.AddComponent(typeof(GuideMember2)) as GuideMember2;

				}
			}
			return _instance;			
		}

	}
	
	public void Init(GuidePlayer gp, MainUILogic mul){
		InitGuide(gp, mul);
		curstep = (int)GUIDE_MEMBER_STEP.SELECT_1;
	}
	
	public void PlayGuide(int guideShowMode){
		//guideShowMode无用，此处游戏中断处理相同
		labelIndex = 0;
		player.SetPlayerActive(true);
		NextStep();
		player.nextStep = null;
	}
	
	
	public void NextStep(){
		switch((GUIDE_MEMBER_STEP)curstep){
			
		case GUIDE_MEMBER_STEP.NONE:						break;//error
		default:
			StartCoroutine(ShowGuideLeader((GUIDE_MEMBER_STEP)curstep));
			break;
			
		}
		
		curstep++;
	}
	public void ButtonNextStep(GameObject Button){
		NextStep();
	}
	IEnumerator ShowGuideLeader(GUIDE_MEMBER_STEP currentstep){
		
		GameObject main1 = null;
		GameObject obj=null;
		
		switch(currentstep){
			
		case GUIDE_MEMBER_STEP.SELECT_1://主页队员按钮
			
			player.nextStep = null;
			player.SetZhaoPosition1();//赵天师位置正常
			player.DisableRowZhao();
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.TeamController);
				yield return new WaitForSeconds(waitSecond);
			}
			TeamController teamController = main1.GetComponent<TeamController>();
			while(obj == null){
				obj = teamController.getGuideMember1Button().gameObject;
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);			
			UpdateKeyObject();
			//下一步消息触发位于TeamController（198，13）
			//箭头位置设置
			player.SetRowPositionGPS(keyObject);
			player.SetRowActive(true);
			
			break;
			
		case GUIDE_MEMBER_STEP.SELECT_2://选择队员
			
			player.nextStep = null;
			player.DisableRowZhao();
			RecoverKeyObject();
			//侠士列表某item
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.SelectTeamMemberController);
				yield return new WaitForSeconds(waitSecond);
			}
			SelectTeamMemberController stmc = main1.GetComponent<SelectTeamMemberController>();
			while(obj == null){
				obj = stmc.getGuideItem2();
				yield return new WaitForSeconds(waitSecond);
			}
			tempDraggableGO = stmc.gameObject;
			SetKeyObj(obj);		
			UpdateKeyObject();
			stmc.SetPanelListEnable(false);
			//下一步消息触发位于SelectTeamMemberController（588，13）
			//箭头位置设置
			player.SetRowPositionGS(keyObject);
			player.SetRowActive(true);
			
			break;
			
		case GUIDE_MEMBER_STEP.SELECT_3:
			
			player.nextStep = null;
			player.DisableRowZhao();
			if(tempDraggableGO != null)
				tempDraggableGO.GetComponent<SelectTeamMemberController>().SetPanelListEnable(true);
			RecoverKeyObject();
			//队员确认按钮
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.SelectTeamMemberController);
				yield return new WaitForSeconds(waitSecond);
			}
			SelectTeamMemberController memberController1 = main1.GetComponent<SelectTeamMemberController>();
			while(obj == null){
				obj = memberController1.getGuideConfirmButton();
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);			
			UpdateKeyObject();
			//下一步消息触发位于SelectTeamMemberController（628，13）（计划更改至TeamController）
			//箭头位置设置
			player.SetRowPositionPS(keyObject);
			player.SetRowActive(true);
			
			break;
			
		case GUIDE_MEMBER_STEP.LABEL_1://选择队长提示框
			
			RecoverKeyObject();
			//player.ShowLabel(guide_member_texts[labelIndex++]);
			ShowLabel(1, "（新卡上阵）新伙伴加入，当真为我们助力不少，指示需要看看，是否只是一个光说不练的家伙");
			ShowLabel(0, guide_member_texts[labelIndex++]);
			player.nextStep = NextStep;
			
			break;
			
		case GUIDE_MEMBER_STEP.END:
			
			player.DisableRowZhao();
			GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.TEAM_MEMBER2);
			//已在主页，需单独触发下一步
			GuideManager.Instance.checkGuideState();
			//删除指引脚本
			Destroy(Instance.gameObject);
			
			break;
			
		}
	}
}
