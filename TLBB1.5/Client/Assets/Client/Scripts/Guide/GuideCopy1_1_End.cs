using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using card.net;

public class GuideCopy1_1_End : GuideBasic {

	public static int[] guide_copyend_texts =
	{
//		3008,//"申请添加助战的侠士为好友，未来会有更高的收益。对方可以在书信界面中查看到你的申请",
		3058,//	new新手描述3006	恭喜少侠大胜！[6BCFCF]闯荡武林挑战副本[1E201F]是江湖中提升能力和获取道具的主要途径，持之以恒，必成大器！小女子思郎心切，心急如焚，咱们还是赶快继续搜寻吧！
	};
	public enum GUIDE_COPYEND_STEP
	{
		NONE = -1,
		LABEL_1 ,//江湖介绍
		END,
	}
	
	private static GuideCopy1_1_End _instance;
	public static GuideCopy1_1_End Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GuideCopy1_1_End)) as GuideCopy1_1_End;
				if(!_instance)
				{
					GameObject gm = new GameObject("GuideCopy1_1_End");
					_instance = gm.AddComponent(typeof(GuideCopy1_1_End)) as GuideCopy1_1_End;

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
			ShowGuideLeader((GUIDE_COPYEND_STEP)curstep);	break;
			
		}
		
		curstep++;
	}
	public void ButtonNextStep(GameObject Button){
		NextStep();
	}
	public void ShowGuideLeader(GUIDE_COPYEND_STEP currentstep){
		
		switch(currentstep){
		
		case GUIDE_COPYEND_STEP.LABEL_1://申请好友弹出框
			
			//ShowLabel(0, "（副本作用）武林向来多血雨，为他又何惧？待我重入无量山，宁可错杀一千，绝不放过一个");
			ShowLabel(0, guide_copyend_texts[labelIndex++]);
			player.nextStep = NextStep;
			//执行处位于BattleEndController(327,13)
			
			break;
			
		case GUIDE_COPYEND_STEP.END:
			
			RecoverKeyObject();
			GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.COPY1_1_END);
			//跳转至副本1-2(中断后返回应从主界面开始)
			//mainLogic.ReturnToMainUI();
			ReturnToPVEList();
			
			break;
			
		}
	}
	
	public void ReturnToPVEList(){
		
		GameObject.FindWithTag("main_controller").SendMessage("showBottomBar");
		
		if (Obj_MyselfPlayer.GetMe().isLastBattleNotFinish)
		{
			mainLogic.SendMessage("LoadPveSceneList");
			return ;
		}
		int nextSubCopy = Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID + 1;
		Debug.Log("NextCopyID : " + nextSubCopy);
		List<SubCopy> subCopys = Obj_MyselfPlayer.GetMe().normalCopys;
		Debug.Log("GetMe().normalCopys.Count = " + subCopys.Count);
		bool isMainCopyOpen = true;
		foreach (SubCopy sub in subCopys)
		{
			if (sub.subCopyID == nextSubCopy)
				isMainCopyOpen = false;
		}
		Tab_Copydetail nextSubDetail = TableManager.GetCopydetailByID(nextSubCopy);
		if (nextSubDetail == null) //如果获取下一副本不存在,要返回当前大副本小副本列表 可能发生在最后一个小副本
		{
			Debug.Log("No Next Copy [NextCopyDetail is Null]");
			mainLogic.SendMessage("LoadPveBossList");
			return;
		}
		Debug.Log("GetMe().curSubcopy.tblCopyDetail.Copyfather = " + Obj_MyselfPlayer.GetMe().curSubcopy.tblCopyDetail.Copyfather);
		if (nextSubDetail.Copyfather == Obj_MyselfPlayer.GetMe().curSubcopy.tblCopyDetail.Copyfather)
			isMainCopyOpen = false;
		if (!isMainCopyOpen) //没有大副本开启
		{
			Debug.Log("No Main Copy New Open");
			mainLogic.SendMessage("LoadPveBossList");
		}
		else
		{
			Tab_Copy nextMainCopy = TableManager.GetCopyByID(nextSubDetail.Copyfather);		
			if (Obj_MyselfPlayer.GetMe().level >= nextMainCopy.PlayerLevel) //等级足够进入新大副本
			{
				Debug.Log("Level is OK For New Main Copy -> UserLevel : " + Obj_MyselfPlayer.GetMe().level + " Need : " + nextMainCopy.PlayerLevel);
				Debug.Log("GetMe().normalMainCopys.Count = " + Obj_MyselfPlayer.GetMe().normalMainCopys.Count);
				foreach(MainCopy mainCopy in Obj_MyselfPlayer.GetMe().normalMainCopys)
				{
					if (mainCopy.copyId == nextSubDetail.Copyfather)
					{
						if (Obj_MyselfPlayer.GetMe().isNextMainOpened)
						{
							Debug.Log("NextMainOpened");
							mainLogic.SendMessage("LoadPveBossList");//非首次打开 返回当前大副本小副本列表
							return;
						}
						else if (mainCopy.copyId == 2) //如果是第二个大副本,并且是首次打开
						{
							Debug.Log("No.2 Main New Open");
							if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.GIFT){
								GuideManager.Instance.isInSubStep = false;
							}
							mainLogic.SendMessage("ReturnToMainUI"); //首次打开第二大副本 返回主界面
							return;
						}
						else
							Obj_MyselfPlayer.GetMe().curMainCopy = mainCopy; //首次打开其他大副本-跳转到新开启大副本的小副本列表
						break ;
					}
				}
			}
			Debug.Log("Level < Request || Other Main Copy New Open");
			mainLogic.SendMessage("LoadPveBossList");
		}
	}
}
