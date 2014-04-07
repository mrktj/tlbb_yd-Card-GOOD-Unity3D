using UnityEngine;
using System.Collections;

public class GuideCopy1_2_End : GuideBasic {

	public static int[] guide_copyend_texts =
	{
//		3008,//"申请添加助战的侠士为好友，未来会有更高的收益。对方可以在书信界面中查看到你的申请",
		3059,//	new新手描述3007	添加助战侠士为[6BCFCF]好友[1E201F]会在未来的战斗中给你带来大量侠义点数，进而获得优质卡牌哦！在[6BCFCF]书信[1E201F]的申请界面就可以查收好友申请啦！
		3060,//	new新手描述3008	在家靠父母，出门靠朋友。好友[6BCFCF]星级越高[1E201F]战斗力就越强哦！
	};
	public enum GUIDE_COPYEND_STEP
	{
		NONE = -1,
		LABEL_1 ,//申请好友弹出框
		LABEL_2 ,//好友能力弹出框
		SELECT_1 ,//申请好友引导
		END,
	}
	
	private static GuideCopy1_2_End _instance;
	public static GuideCopy1_2_End Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GuideCopy1_2_End)) as GuideCopy1_2_End;
				if(!_instance)
				{
					GameObject gm = new GameObject("GuideCopy1_2_End");
					_instance = gm.AddComponent(typeof(GuideCopy1_2_End)) as GuideCopy1_2_End;

				}
			}
			return _instance;			
		}

	}
	
	public void Init(GuidePlayer gp, MainUILogic mul){
		InitGuide(gp, mul);
		curstep = (int)GUIDE_COPYEND_STEP.LABEL_1;
	}
	
	public void PlayGuide(int guideShowMode){
		labelIndex=0;
		player.SetPlayerActive(true);
		NextStep();
		player.nextStep = NextStep;
	}
	
	
	
	
	
	public void NextStep(){
		switch((GUIDE_COPYEND_STEP)curstep){
			
		case GUIDE_COPYEND_STEP.NONE:						break;//error
		default:
			StartCoroutine(ShowGuideLeader((GUIDE_COPYEND_STEP)curstep));	
			break;
			
		}
		
		curstep++;
	}
	public void ButtonNextStep(GameObject Button){
		NextStep();
	}
	IEnumerator ShowGuideLeader(GUIDE_COPYEND_STEP currentstep){
		
		GameObject main1 = null;
		GameObject obj=null;
		
		switch(currentstep){
		
		case GUIDE_COPYEND_STEP.LABEL_1://申请好友弹出框
			
			//player.ShowLabel(guide_copyend_texts[labelIndex++]);
			//ShowLabel(0, "（申请好友）再入无量山，又认识新的义士，认为新闺蜜，以后一起开派对！");
			ShowLabel(0, guide_copyend_texts[labelIndex++]);
			
			break;
			
		case GUIDE_COPYEND_STEP.LABEL_2://好友能力弹出框
			
			//player.ShowLabel(guide_copyend_texts[labelIndex++]);
			//ShowLabel(0, "（好友能力）闺蜜一起上阵，还能够让我更快找到他");
			ShowLabel(0, guide_copyend_texts[labelIndex++]);
			
			break;
			
		case GUIDE_COPYEND_STEP.SELECT_1://申请好友引导
			
			player.nextStep = null;
			player.DisableRowZhao();
			//一号位队员按钮
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.FriendShipSettleController);
				yield return new WaitForSeconds(waitSecond);
			}
			FriendShipSettleController fssc = main1.GetComponent<FriendShipSettleController>();
			while(obj == null){
				obj = fssc.getGuideItem();
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);		
			UpdateKeyObject();
			//下一步消息触发位于FriendShipController（70，13）
			//箭头位置设置
			player.SetRowPositionS(keyObject);
			
			break;
			
		case GUIDE_COPYEND_STEP.END:
			
			RecoverKeyObject();
			GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.COPY1_2_END);
			
			break;
			
		}
	}
}
