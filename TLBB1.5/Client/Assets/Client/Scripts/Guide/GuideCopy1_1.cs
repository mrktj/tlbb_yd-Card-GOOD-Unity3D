using UnityEngine;
using System.Collections;
using System.Threading;

public class GuideCopy1_1 : GuideBasic {

	public static int[] guide_copy_texts =
	{
//		3005,//"不好，前面是无量剑西宗的人，来者不善啊！不如我们主动出击，打他们个措手不及！",
//		3006,//"在家靠父母，出门靠朋友。选择一位出色的侠士并肩作战吧！",
//		3007,//"现在是你运筹帷幄的时刻，好的布阵是胜利的先决条件。通常敌人都会优先攻击站在前排的侠士，一定要小心。布阵结束就让我们开始第一场战斗吧！",
		3055,//	new新手描述3003		少林寺一战后，乔峰、虚竹和段誉三兄弟趁胜追击，不明去向，我心中惴惴不安。听闻无量山比剑英雄云集，他们一定不会错过，我们快去打探一番！	null																																		
		3056,//	new新手描述3004删		初入江湖一定要多加小心，选择[6BCFCF]星级越高[1E201F]的助战好友其战斗力越强，我们的胜算也越大。选择一位出色的侠士并肩作战吧！	null																																		
		3057,//	new新手描述3005		我虽擅长治疗，但[6BCFCF]防御力较低[1E201F]。列阵之时若位于[6BCFCF]后排[1E201F]则能提高团队持续治疗能力，对我们更为有利！	null																																		
	};
	public enum GUIDE_COPY_STEP
	{
		NONE = -1,
		
		LABEL_1,//武林指导弹出框
		
		SELECT_1,//副本选择
		SELECT_2,
		SELECT_3,
		
		NONE_1,//等待接收助战好友列表空白期
		
		LABEL_5,//助战好友弹出框
		
		SELECT_4,//选择助战好友
		
		NONE_2,//等待接收助战好友列表空白期
		
		LABEL_6,//阵型调整弹出框
		
		SELECT_5,//调整阵型
		SELECT_6,//开始战斗
		
		END,
	}
	
	private static GuideCopy1_1 _instance;
	public static GuideCopy1_1 Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GuideCopy1_1)) as GuideCopy1_1;
				if(!_instance)
				{
					GameObject gm = new GameObject("GuideCopy1_1");
					_instance = gm.AddComponent(typeof(GuideCopy1_1)) as GuideCopy1_1;

				}
			}
			return _instance;			
		}

	}
	
	public void Init(GuidePlayer gp, MainUILogic mul){
		InitGuide(gp, mul);
		curstep = (int)GUIDE_COPY_STEP.LABEL_1;
	}
	
	public void PlayGuide(int guideShowMode){
		//guideShowMode无用，此处游戏中断处理相同
		labelIndex=0;
		player.SetPlayerActive(true);
		NextStep();
		player.nextStep = NextStep;
	}
	
	
	
	public void NextStep(){
		switch((GUIDE_COPY_STEP)curstep){
			
		case GUIDE_COPY_STEP.NONE:						break;//error
		default:
			StartCoroutine(ShowGuideLeader((GUIDE_COPY_STEP)curstep));
			break;
			
		}
		
		curstep++;
	}
	public void ButtonNextStep(GameObject Button){
		NextStep();
	}
	IEnumerator ShowGuideLeader(GUIDE_COPY_STEP currentstep){
		
		GameObject main1 = null;
		GameObject obj=null;
		
		switch(currentstep){
		
		case GUIDE_COPY_STEP.LABEL_1://武林指导弹出框
			
			//ShowLabel(0, "（战斗指引框）传闻他曾出现无量山，我们便去打听打听");
			ShowLabel(0, guide_copy_texts[labelIndex++]);
			Debug.Log("战斗指引框 阶段");
			
			break;
			
		case GUIDE_COPY_STEP.SELECT_1://副本选择
			
			player.nextStep = null;
			player.DisableRowZhao();
			//获取PVE按钮
			while(main1 == null){
				main1 = mainLogic.mainController;
				yield return new WaitForSeconds(waitSecond);
			}
			MainController main = main1.GetComponent<MainController>();
			while(obj == null){
				obj = main.getPVEButton().gameObject;
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);
			UpdateKeyObject();
			//下一步消息触发位于MainController（201，13）
			//箭头位置设置
			player.SetRowPositionPS(keyObject);
			
			break;
			
		case GUIDE_COPY_STEP.SELECT_2:
			
			player.nextStep = null;
			player.DisableRowZhao();
			RecoverKeyObject();
			//获取第一个副本item
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.PVEMainController);
				yield return new WaitForSeconds(waitSecond);
			}
			PVEMainController pvemain = main1.GetComponent<PVEMainController>();
			while(obj == null){
				obj = pvemain.getGuideItem();
				yield return new WaitForSeconds(waitSecond);
			}
			tempDraggableGO = pvemain.gameObject;
			SetKeyObj(obj);	
			UpdateKeyObject();
			//拖动列表暂停使用
			pvemain.SetPanelListEnable(false);
			//下一步消息触发位于PVEMainController（340，13）
			//箭头位置设置
			player.SetRowPositionGS(keyObject);
			
			break;
			
		case GUIDE_COPY_STEP.SELECT_3:
			
			player.nextStep = null;
			player.DisableRowZhao();
			//拖动列表恢复使用
			if(tempDraggableGO != null)
				tempDraggableGO.GetComponent<PVEMainController>().SetPanelListEnable(true);
			RecoverKeyObject();
			//获取第一个小副本item
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.PVESubController);
				yield return new WaitForSeconds(waitSecond);
			}
			PVESubController pvesub = main1.GetComponent<PVESubController>();
			while(obj == null){
				obj = pvesub.getGuideItem();
				yield return new WaitForSeconds(waitSecond);
			}
			tempDraggableGO = pvesub.gameObject;
			SetKeyObj(obj);
			UpdateKeyObject();	
			//拖动列表暂停使用
			pvesub.SetPanelListEnable(false);
			//下一步消息触发位于PVESubController（237，13）
			//箭头位置设置
			player.SetRowPositionGS(keyObject);
			
			break;
			
		case GUIDE_COPY_STEP.NONE_1:
			
			//拖动列表恢复使用
			if(tempDraggableGO != null)
				tempDraggableGO.GetComponent<PVESubController>().SetPanelListEnable(true);
			RecoverKeyObject();
			player.DisableRowZhao();
			player.nextStep = null;
			//下一步消息触发位于SelectAssistController（126，25）
			
			break;
			
		case GUIDE_COPY_STEP.LABEL_5://助战好友弹出框
			
			RecoverKeyObject();
			//ShowLabel(0, "（助战好友）有位无量山义士，我们便结伴同行");
			ShowLabel(0, guide_copy_texts[labelIndex++]);
			player.nextStep = NextStep;
			
			break;
			
		case GUIDE_COPY_STEP.SELECT_4://选择助战好友
			
			player.nextStep = null;
			player.DisableRowZhao();
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.SelectAssistController);
				yield return new WaitForSeconds(waitSecond);
			}
			SelectAssistController sac = main1.GetComponent<SelectAssistController>();
			while(obj == null){
				obj = sac.getGuideItem();
				yield return new WaitForSeconds(waitSecond);
			}
			tempDraggableGO = sac.gameObject;
			SetKeyObj(obj);
			UpdateKeyObject();
			//拖动列表暂停使用
			sac.SetPanelListEnable(false);
			//下一步消息触发位于SelectAssistController（336，13）
			//箭头位置设置
			player.SetRowPositionGS(keyObject);
			
			break;
			
		case GUIDE_COPY_STEP.NONE_2:
			
			//拖动列表恢复使用
			if(tempDraggableGO != null)
				tempDraggableGO.GetComponent<SelectAssistController>().SetPanelListEnable(true);
			RecoverKeyObject();
			player.DisableRowZhao();
			player.nextStep = null;
			//下一步消息触发位于BattleBeforeController（75，13）
			
			break;
			
		case GUIDE_COPY_STEP.LABEL_6://阵型调整弹出框
			
			//ShowLabel(1, "（阵型调整）非我贪生怕死，只为做闺蜜后盾");
			ShowLabel(1, guide_copy_texts[labelIndex++]);
			player.nextStep = NextStep;
			//加入阵型拖动演示动画
			
			break;
			
		case GUIDE_COPY_STEP.SELECT_5://调整阵型
			
			player.nextStep = null;
			player.DisableRowZhao();
			player.PlayCopyGuide();
			//队员选择
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.BattleBeforeController);
				yield return new WaitForSeconds(waitSecond);
			}
			BattleBeforeController bbc1 = main1.GetComponent<BattleBeforeController>();
			while(obj == null){
				obj = bbc1.getGuideCard();
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);		
			UpdateKeyObject();
			UpdateGameObject(bbc1.getTeamBG2Position());
			UpdateGameObject(bbc1.getTeamBG5Position());
			
			//下一步消息触发位于UIDragController（118，13）
			//player.SetRowPosition(bbc1.getTeamBG5Position().transform.localPosition);
			//player.SetRowPositionS(bbc1.getGuideRowPosition());
			
			break;
			
		case GUIDE_COPY_STEP.SELECT_6://开始战斗
			
			player.nextStep = null;
			RecoverKeyObject();
			player.DisableRowZhao();
			player.StopCopyGuide();
			//获取战斗开始按钮
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.BattleBeforeController);
				yield return new WaitForSeconds(waitSecond);
			}
			BattleBeforeController bbc = main1.GetComponent<BattleBeforeController>();
			while(obj == null){
				obj = bbc.getGuideItem();
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);
			UpdateKeyObject();
			RecoverGameObject(bbc.getTeamBG2Position());
			RecoverGameObject(bbc.getTeamBG5Position());	
			//下一步消息触发位于BattleBeforeController（307，13)
			//箭头位置设置
			player.SetRowPositionS(keyObject);
			
			break;
			
		case GUIDE_COPY_STEP.END:
			
			RecoverKeyObject();
			GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.COPY1_1);
			
			break;
		}
	}
}
