using UnityEngine;
using System.Collections;

public class GuideUpdate : GuideBasic {

	public static int[] guide_update_texts =
	{
//		3009,//	UI描述	恭喜！你已经取得了第一场胜利！
//		3010,//	UI描述	江湖凶险，你还要不断提升侠士的等级方能应对！
//		3011,//	UI描述	我这里有一颗[A65A04]白棋子[F1ECCF]，吸收它可以大大[6BCFCF]提升侠士等级[F1ECCF]！
//		3012,//	UI描述	升级能通过[6BCFCF]吸收其他侠士的功力[F1ECCF]重点提升一些有潜力的侠士，现在就让我们试试。
//		3013,//	UI描述	[6BCFCF]被吸收的侠士将会被消耗掉。[F1ECCF]棋子的属性差，却能提供很高的经验，是用来吸收的绝佳对象。
//		3014,//	UI描述	恭喜少侠！你的队长[A65A04]{0}[F1ECCF]升到了16级！更多的棋子可以在[6BCFCF]珍珑棋局[F1ECCF]活动中获得！
//		3015,//	UI描述	有时我们仅仅提高上阵侠士的等级还是不够的，有种更为直接的方式——[6BCFCF]修炼[F1ECCF]，能大幅提升侠士的生命值和攻击力！
//		3016,//	UI描述	现在我们返回主页，让你的队长进行修炼。
		3064,//	new新手描述3012	[FDF035]金棋子[1E201F]中蕴含了高手的浑厚功力，吸收之后将瞬间提升等级！
		3065,//	new新手描述3013	通过[6BCFCF]升级[1E201F]我的功力更胜从前，再遇强敌定能助少侠一臂之力！
	};
	public enum GUIDE_UPDATE_STEP
	{
		NONE = -1,
		
		LABEL_1,//【卡牌升级】弹出框
//		LABEL_2,
//		LABEL_3,
		
		SELECT_1,//主页_升级按钮
		
		NONE_1,
		
//		LABEL_4 ,//【选择升级卡牌】弹出框
		
		SELECT_2,//选择升级卡牌按钮
		SELECT_3,//升级卡牌选择
		
		NONE_2,
		
//		LABEL_5,//【材料卡牌选择】弹出框
		
		SELECT_4,//选择材料按钮
		SELECT_5,
		SELECT_6,
		SELECT_7,//卡牌升级按钮
		
		NONE_3,//
		
		LABEL_6,//【修炼引导】弹出框
//		LABEL_7,
//		LABEL_8,
		
		SELECT_8,//升级_主页按钮
		
		END,
	}
	
	private static GuideUpdate _instance;
	public static GuideUpdate Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GuideUpdate)) as GuideUpdate;
				if(!_instance)
				{
					GameObject gm = new GameObject("GuideUpdate");
					_instance = gm.AddComponent(typeof(GuideUpdate)) as GuideUpdate;

				}
			}
			return _instance;			
		}

	}
	
	public void Init(GuidePlayer gp, MainUILogic mul){
		InitGuide(gp, mul);
		curstep = (int)GUIDE_UPDATE_STEP.LABEL_1;
	}
	
	public void PlayGuide(){
		labelIndex=0;
		player.SetPlayerActive(true);
		NextStep();
		player.nextStep = NextStep;
	}
	
	
	
	
	public void NextStep(){
		switch((GUIDE_UPDATE_STEP)curstep){
			
		case GUIDE_UPDATE_STEP.NONE:						break;//error
		default:
			StartCoroutine(ShowGuideLeader((GUIDE_UPDATE_STEP)curstep));
			break;
		}
		
		curstep++;
	}
	public void ButtonNextStep(GameObject Button){
		NextStep();
	}
	IEnumerator ShowGuideLeader(GUIDE_UPDATE_STEP currentstep){
		
		GameObject main1 = null;
		GameObject obj=null;
		
		switch(currentstep){
		
		case GUIDE_UPDATE_STEP.LABEL_1://【卡牌升级】弹出框
			
			//player.ShowLabel(guide_update_texts[labelIndex++]);
			//ShowLabel(1, "（卡牌升级）亲爱滴，咱们已经有不少好卡了");
			ShowLabel(0, guide_update_texts[labelIndex++]);
			
			break;
			
//		case GUIDE_UPDATE_STEP.LABEL_2:
//			
//			//player.ShowLabel(guide_update_texts[labelIndex++]);
//			ShowLabel(0, "（卡牌升级）亲，你说的什么意思？");
//			
//			break;
//		case GUIDE_UPDATE_STEP.LABEL_3:
//			
//			//player.ShowLabel(guide_update_texts[labelIndex++]);
//			ShowLabel(1, "（卡牌升级）我们练武之人，自当时刻勤勉，以提升内功修为，这些卡自然是帮我们的！");
//			
//			break;
		
		case GUIDE_UPDATE_STEP.SELECT_1://主页_升级按钮
			
			player.SetZhaoActive(false);
			player.SetRowActive(false);
			//获取合成按钮
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.TeamController);
				yield return new WaitForSeconds(waitSecond);
			}
			TeamController tc = main1.GetComponent<TeamController>();
			while(obj == null){
				obj = tc.getGuideUpdateButton();
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);
			UpdateKeyObject();
			//下一步消息触发位于TeamController（180，13）
			player.nextStep = null;
			//箭头位置设置
			player.SetRowPositionPS(keyObject);
			player.SetRowActive(true);
			
			break;
			
		case GUIDE_UPDATE_STEP.NONE_1:
			
			RecoverKeyObject();
			player.SetZhaoActive(false);
			player.SetRowActive(false);
			player.nextStep = null;
			Debug.LogWarning("GuideUpdate NONE_1!");
			//下一步消息触发位于CardUpdateController（157，13）
			
			break;
		
//		case GUIDE_UPDATE_STEP.LABEL_4://【选择升级卡牌】弹出框
//			
//			ShowLabel(0, "（选择升级卡牌）提升修为这等好事，我当然要上");
//			player.nextStep = NextStep;
//			
//			break;
		
		case GUIDE_UPDATE_STEP.SELECT_2://选择升级卡牌按钮
			
			Debug.LogWarning("GuideUpdate SELECT_2!");
			player.SetZhaoActive(false);
			player.SetRowActive(false);
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.CardUpdateController);
				yield return new WaitForSeconds(waitSecond);
			}
			CardUpdateController cuc = main1.GetComponent<CardUpdateController>();
			while(obj == null){
				obj = cuc.getGuideCardItem();
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);
			UpdateKeyObject();
			//下一步消息触发位于CardUpdateController（657，17）
			player.nextStep = null;
			//箭头位置设置
			player.SetRowPositionPS(keyObject);
			player.SetRowActive(true);
			
			break;
		case GUIDE_UPDATE_STEP.SELECT_3://升级卡牌选择
			
			player.nextStep = null;
			RecoverKeyObject();
			player.SetRowActive(false);
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.SelectHeroController);
				yield return new WaitForSeconds(waitSecond);
			}
			SelectHeroController shc = main1.GetComponent<SelectHeroController>();
			while(obj == null){
				obj = shc.getGuideCardItem();
				yield return new WaitForSeconds(waitSecond);
			}
			tempDraggableGO = shc.gameObject;
			SetKeyObj(obj);
			UpdateKeyObject();
			shc.SetPanelListEnable(false);
			//下一步消息触发位于SelectHeroController（1257，13）
			//箭头位置设置
			player.SetRowPositionGS(keyObject);
			player.SetRowActive(true);
			
			break;
		
		case GUIDE_UPDATE_STEP.NONE_2:
			
			if(tempDraggableGO != null)
				tempDraggableGO.GetComponent<SelectHeroController>().SetPanelListEnable(true);
			RecoverKeyObject();
			player.SetZhaoActive(false);
			player.SetRowActive(false);
			player.nextStep = null;
			//下一步消息触发位于CardUpdateController（157，13）
			
			break;
		
//		case GUIDE_UPDATE_STEP.LABEL_5://【材料卡牌选择】弹出框
//			
//			ShowLabel(1, "（材料卡牌选择）这个好，助你提升修为真真是极好的");
//			player.nextStep = NextStep;
//			
//			break;
		
		case GUIDE_UPDATE_STEP.SELECT_4://选择材料按钮
			
			player.nextStep = null;
			player.SetZhaoActive(false);
			player.SetRowActive(false);
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.CardUpdateController);
				yield return new WaitForSeconds(waitSecond);
			}
			CardUpdateController cuc4 = main1.GetComponent<CardUpdateController>();
			while(obj == null){
				obj = cuc4.getGuideMaterialItem();
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);
			UpdateKeyObject();
			//下一步消息触发位于CardUpdateController（676，17）
			//箭头位置设置
			player.SetRowPositionPS(keyObject);
			player.SetRowActive(true);
			
			break;
		case GUIDE_UPDATE_STEP.SELECT_5:
			
			RecoverKeyObject();
			player.SetRowActive(false);
			player.nextStep = null;
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.SelectHeroController);
				yield return new WaitForSeconds(waitSecond);
			}
			SelectHeroController shc5 = main1.GetComponent<SelectHeroController>();
			while(obj == null){
				obj = shc5.getGuideMaterialItem();
				yield return new WaitForSeconds(waitSecond);
			}
			tempDraggableGO = shc5.gameObject;
			SetKeyObj(obj);
			UpdateKeyObject();
			shc5.SetPanelListEnable(false);
			//下一步消息触发位于SelectHeroController（1511，17）
			//箭头位置设置
			player.SetRowPositionGS(keyObject);
			player.SetRowActive(true);
			
			break;
		case GUIDE_UPDATE_STEP.SELECT_6:
			
			if(tempDraggableGO != null)
				tempDraggableGO.GetComponent<SelectHeroController>().SetPanelListEnable(true);
			RecoverKeyObject();
			player.SetRowActive(false);
			SelectHeroController shc6 = mainLogic.getController(MainUILogic.ChildIndex.SelectHeroController).GetComponent<SelectHeroController>();
			SetKeyObj(shc6.getGuideConfirmButton());
			UpdateKeyObject();
			//下一步消息触发位于CardUpdateController（154，13）
			player.nextStep = null;
			//箭头位置设置
			player.SetRowPositionPS(keyObject);
			player.SetRowActive(true);
			
			break;
		case GUIDE_UPDATE_STEP.SELECT_7://卡牌升级按钮
			
			RecoverKeyObject();
			player.SetRowActive(false);
			player.nextStep = null;
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.CardUpdateController);
				yield return new WaitForSeconds(waitSecond);
			}
			CardUpdateController cuc7 = main1.GetComponent<CardUpdateController>();
			while(obj == null){
				obj = cuc7.getGuideConfirmButton();
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);
			
			UpdateKeyObject();
			//下一步消息触发位于CardUpdateController（691，13）
			//箭头位置设置
			player.SetRowPositionS(keyObject);
			player.SetRowActive(true);
			
			break;
			
		case GUIDE_UPDATE_STEP.NONE_3:
			
			RecoverKeyObject();
			player.DisableRowZhao();
			player.nextStep = null;
			
			break;
			
		case GUIDE_UPDATE_STEP.LABEL_6://【修炼引导】弹出框
			
			RecoverKeyObject();
			//ShowLabel(0, "（修炼引导）修为果然大增");
			ShowLabel(0, guide_update_texts[labelIndex++]);
			player.nextStep = NextStep;
			
			break;
//		case GUIDE_UPDATE_STEP.LABEL_7:
//			
//			ShowLabel(1, "（修炼引导）我还诓你不成？");
//			
//			break;
//		case GUIDE_UPDATE_STEP.LABEL_8:
//			
//			ShowLabel(0, "（修炼引导）带我们继续去无量山找夫君");
//			
//			break;
			
		case GUIDE_UPDATE_STEP.SELECT_8://升级_主页按钮
			
			player.nextStep = null;
			player.DisableRowZhao();
			//获取PVE按钮
			while(main1 == null){
				main1 = mainLogic.mainController;
				yield return new WaitForSeconds(waitSecond);
			}
			MainController main = main1.GetComponent<MainController>();
			while(obj == null){
				obj = main.getHomeButton();
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);
			UpdateKeyObject();
			//下一步消息触发位于MainController（194，13）
			//箭头位置设置
			player.SetRowPositionPS(keyObject);
			player.SetRowActive(true);
			
			break;
			
		case GUIDE_UPDATE_STEP.END:
			
			RecoverKeyObject();
			GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.UPDATE);
			
			break;
			
		}
	}
}
