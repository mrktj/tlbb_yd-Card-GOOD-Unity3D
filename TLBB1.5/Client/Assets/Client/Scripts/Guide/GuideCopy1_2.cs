using UnityEngine;
using System.Collections;
using System.Threading;

public class GuideCopy1_2 : GuideBasic {

	public static int[] guide_copy_texts =
	{
		3005,//"不好，前面是无量剑西宗的人，来者不善啊！不如我们主动出击，打他们个措手不及！",
		3006,//"在家靠父母，出门靠朋友。选择一位出色的侠士并肩作战吧！",
		3007,//"现在是你运筹帷幄的时刻，好的布阵是胜利的先决条件。通常敌人都会优先攻击站在前排的侠士，一定要小心。布阵结束就让我们开始第一场战斗吧！",
	};
	public enum GUIDE_COPY_STEP
	{
		NONE = -1,
		
		SELECT_1,//副本选择
		SELECT_2,
		
		SELECT_3,
		
		NONE_1,//等待接收助战好友列表空白期
		
		SELECT_4,//选择助战好友
		
		NONE_2,//等待接收助战好友列表空白期
		
		SELECT_6,//开始战斗
		
		END,
	}
	
	private static GuideCopy1_2 _instance;
	public static GuideCopy1_2 Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GuideCopy1_2)) as GuideCopy1_2;
				if(!_instance)
				{
					GameObject gm = new GameObject("GuideCopy1_2");
					_instance = gm.AddComponent(typeof(GuideCopy1_2)) as GuideCopy1_2;

				}
			}
			return _instance;			
		}

	}
	
	public void Init(GuidePlayer gp, MainUILogic mul){
		InitGuide(gp, mul);
		curstep = (int)GUIDE_COPY_STEP.SELECT_1;
	}
	
	public void PlayGuide(int guideShowMode){
		switch(guideShowMode){
		case 0://GAMING
			labelIndex=0;
			curstep = (int)GUIDE_COPY_STEP.SELECT_3;//(待修改)
			NextStep();
			player.SetPlayerActive(true);
			player.nextStep = null;
			break;
		case 1://SUSPEND
			labelIndex=0;
			curstep = (int)GUIDE_COPY_STEP.SELECT_1;
			NextStep();
			player.SetPlayerActive(true);
			player.nextStep = null;
			break;
		}
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
			
		case GUIDE_COPY_STEP.SELECT_1://副本选择
			
			player.nextStep = null;
			player.DisableRowZhao();
			Debug.LogWarning("GUIDE_COPY_STEP : SELECT_1 ; begin to select pve button");
			//获取PVE按钮
			while(main1 == null){
				main1 = mainLogic.mainController;
				Debug.LogWarning("GUIDE_COPY_STEP : SELECT_1 ; finding mainController");
				yield return new WaitForSeconds(waitSecond);
			}
			Debug.LogWarning("GUIDE_COPY_STEP : SELECT_1 ; find mainController");
			MainController main = main1.GetComponent<MainController>();
			while(obj == null){
				obj = main.getPVEButton().gameObject;
				yield return new WaitForSeconds(waitSecond);
			}
			Debug.LogWarning("GUIDE_COPY_STEP : SELECT_1 ; find PVEButton");
			SetKeyObj(obj);
			UpdateKeyObject();
			//下一步消息触发位于MainController（222，13）
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
			//下一步消息触发位于PVEMainController（421，13）
			//箭头位置设置
			player.SetRowPositionGS(keyObject);
			
			break;
			
		case GUIDE_COPY_STEP.SELECT_3:
			
			player.nextStep = null;
			player.DisableRowZhao();
			RecoverKeyObject();
			//拖动列表恢复使用
			if(tempDraggableGO != null)
				tempDraggableGO.GetComponent<PVEMainController>().SetPanelListEnable(true);
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
			//下一步消息触发位于PVESubController（239，13）
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
			
		case GUIDE_COPY_STEP.SELECT_4://选择助战好友
			
			RecoverKeyObject();
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
			//下一步消息触发位于SelectAssistController（335，13）
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
			
		case GUIDE_COPY_STEP.SELECT_6://开始战斗
			
			player.nextStep = null;
			player.DisableRowZhao();
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
			//下一步消息触发位于BattleBeforeController（292，13)
			//箭头位置设置
			player.SetRowPositionS(keyObject);
			
			break;
			
		case GUIDE_COPY_STEP.END:
			
			RecoverKeyObject();
			GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.COPY1_2);
			
			break;
		}
	}
}
