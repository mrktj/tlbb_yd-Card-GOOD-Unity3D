using UnityEngine;
using System.Collections;

using Games.LogicObject;
using GCGame.Table;
using card.net;
using Games.CharacterLogic;

public class GuideLottery : GuideBasic {

	public static int[] guide_lottery_texts = 
	{
//		3021,//	UI描述	恭喜！你招募了[A65A04]{0}[F1ECCF]！
//		3022,//	UI描述	少侠悟性如此之高，未来的造诣可想而知。如果还有什么疑惑之处，可以点击[6BCFCF]帮助[F1ECCF]查看我留给你的[6BCFCF]江湖指南[F1ECCF]。我们如此有缘，就再赠予你[A65A04]5颗大力丸[F1ECCF]和[A65A04]5颗强身丹[F1ECCF]作为临别的礼物吧。!
		3061,//	new新手描述3009	这钱袋我一眼便识，乃是我郎君之物，一定是被龚光杰掠走的！小女子承蒙少侠照顾，余下的元宝就赠予少侠招募侠士吧！
		3062,//	new新手描述3010	恭喜少侠成功招募到[FDF035]{0}[1E201F]！有此等英雄加入我们真可谓如虎添翼！
	};
	public enum GUIDE_LOTTERY_STEP
	{
		NONE = -1,
		
		LABEL_1,//获得元宝
		
		SELECT_1,//主页_商铺按钮
		SELECT_2,//商铺_元宝抽奖
		SELECT_3,//元宝抽奖_抽一次
		
		HIDE_1,
		
		LABEL_2,//上阵引导
		
		END,
	}
	
	private static GuideLottery _instance;
	public static GuideLottery Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GuideLottery)) as GuideLottery;
				if(!_instance)
				{
					GameObject gm = new GameObject("GuideLottery");
					_instance = gm.AddComponent(typeof(GuideLottery)) as GuideLottery;

				}
			}
			return _instance;			
		}

	}
	
	public void Init(GuidePlayer gp, MainUILogic mul){
		InitGuide(gp, mul);
		curstep = (int)GUIDE_LOTTERY_STEP.LABEL_1;
		//设置相应关键字，为结束后武林闪烁准备
	}
	
	public void PlayGuide(int guideShowMode){
		labelIndex=0;
		player.SetPlayerActive(true);
		NextStep();
		player.nextStep = NextStep;
	}
	
	
	
	
	
	public void NextStep(){
		switch((GUIDE_LOTTERY_STEP)curstep){
			
		case GUIDE_LOTTERY_STEP.NONE:						break;//error
		default:
			StartCoroutine(ShowGuideLeader((GUIDE_LOTTERY_STEP)curstep));
			break;
		}
		
		curstep++;
	}
	public void ButtonNextStep(GameObject Button){
		NextStep();
	}
	IEnumerator ShowGuideLeader(GUIDE_LOTTERY_STEP currentstep){
		
		GameObject main1 = null;
		GameObject obj=null;
		
		switch(currentstep){
			
		case GUIDE_LOTTERY_STEP.LABEL_1://获得元宝
			
			//player.ShowLabel(guide_lottery_texts[labelIndex++]);
			//ShowLabel(0, "（获得元宝）出门带了些银钱，现在才想起可以找义士帮我一起去寻夫");
			Debug.LogWarning("*** GuideLottery Label1:");
			if(player == null)
				Debug.LogWarning("*** GuideLottery : player is null");
			if(mainLogic == null)
				Debug.LogWarning("*** GuideLottery : mainLogic is null");
			ShowLabel(0, guide_lottery_texts[labelIndex++]);
		
			break;
			
		case GUIDE_LOTTERY_STEP.SELECT_1://主页_商铺按钮
			
			player.nextStep = null;
			player.SetZhaoActive(false);
			player.SetRowActive(false);
			while(main1 == null){
				main1 = mainLogic.mainController;
				yield return new WaitForSeconds(waitSecond);
			}
			MainController main = main1.GetComponent<MainController>();
			while(obj == null){
				obj = main.getShopButton();
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);
			UpdateKeyObject();
			//下一步消息触发位于MainController（233，13）
			//箭头位置设置
			player.SetRowPositionPS(keyObject);
			player.SetRowActive(true);
				
			break;
		case GUIDE_LOTTERY_STEP.SELECT_2://商铺_元宝抽奖
			
			player.nextStep = null;
			player.SetZhaoActive(false);
			player.SetRowActive(false);
			RecoverKeyObject();
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.ShopController);
				yield return new WaitForSeconds(waitSecond);
			}
			ShopController sc = main1.GetComponent<ShopController>();
			while(obj == null){
				obj = sc.getDiamondLotteryButton();
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);
			UpdateKeyObject();
			//下一步消息触发位于ShopController（164，13）
			//箭头位置设置
			player.SetRowPositionS(keyObject);
			player.SetRowActive(true);
			
			break;
		case GUIDE_LOTTERY_STEP.SELECT_3://元宝抽奖_抽一次
			
			player.nextStep = null;
			player.SetZhaoActive(false);
			player.SetRowActive(false);
			RecoverKeyObject();
			while(main1 == null){
				main1 = mainLogic.getController(MainUILogic.ChildIndex.LotteryController);
				yield return new WaitForSeconds(waitSecond);
			}
			LotteryController lc = main1.GetComponent<LotteryController>();
			while(obj == null){
				obj = lc.getBtnOne();
				yield return new WaitForSeconds(waitSecond);
			}
			SetKeyObj(obj);
			UpdateKeyObject();
			//下一步消息触发位于LotteryController（257，13）
			//箭头位置设置
			player.SetRowPositionS(keyObject);
			player.SetRowActive(true);
				
			break;
			
		case GUIDE_LOTTERY_STEP.HIDE_1:
			
			RecoverKeyObject();
			player.DisableRowZhao();
			//下一步消息触发位于LotteryAnimationController（227，13）
			player.nextStep = null;
			Debug.LogWarning("*** Lottery step HIDE_1");
			
			break;
			
		case GUIDE_LOTTERY_STEP.LABEL_2://获得元宝
			
			player.SetPlayerActive(true);
			//player.ShowLabel(guide_lottery_texts[labelIndex++]);
			//ShowLabel(0, "（新卡上阵）找到义士，何不让他速速加入队伍，以提升实力");
			player.SetZhaoPosition2();//赵天师位置上移
			ShowLabel(0, guide_lottery_texts[labelIndex++]);
			player.nextStep = NextStep;
			Debug.LogWarning("*** Lottery step LABEL_2");
		
			break;
			
		case GUIDE_LOTTERY_STEP.END://返回主界面
			
			//PlayerPrefs.SetInt("FlashPVEBtn",0);//0-lottery end   1-flash   2-over//Jack Wen 20130926
			player.SetZhaoPosition1();//赵天师位置正常
			GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.LOTTERY);
			mainLogic.ReturnToMainUI();
            mainLogic.CheckPublicNoticeShow();
			//mainLogic.flashWulin();
			
			break;
			
		}
	}
}
