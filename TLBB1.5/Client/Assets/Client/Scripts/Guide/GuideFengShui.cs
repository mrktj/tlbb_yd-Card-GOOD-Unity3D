using UnityEngine;
using System.Collections;
using Games.CharacterLogic;

public class GuideFengShui : GuideBasic {

	public static int[] guide_fengshui_texts = 
	{
		//3002,//"这位少侠，我看你气质不凡，定是个中高手。不过无量山中龙蛇混杂，一不小心便会群战四起，你孤身一人恐怕多有不便。还是让我来帮你做些准备吧！",
		//3003,//"首先我们需要一位首领，这样遇到敌人我们就不会乱了阵脚。这位女侠善解人意，又通晓武学，不如就让她为少侠分忧吧！",
		3051,//	new新手描述3001阿朱	少侠青睐有加，阿朱必定全力相助！我来带领队伍能全面提升上阵侠士的[6BCFCF]闪避几率[F1ECCF]，以灵巧之术搏强劲之力！	null																																		
		3052,//	new新手描述3001王语嫣	少侠青睐有加，语嫣必定全力相助！我来带领队伍能全面提升上阵侠士的[6BCFCF]攻击力[F1ECCF]，通晓武学，无往不利！	null																																		
		3053,//	new新手描述3001梦姑	少侠青睐有加，梦姑必定全力相助！我来带领队伍能全面提升上阵侠士的[6BCFCF]攻击力[F1ECCF]，底蕴深厚，所向披靡！	null
	};
	public enum GUIDE_FENGSHUI_STEP{
		NONE = -1,
		END,
	}
	
	private static GuideFengShui _instance;
	public static GuideFengShui Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GuideFengShui)) as GuideFengShui;
				if(!_instance)
				{
					GameObject gm = new GameObject("GuideFengShui");
					_instance = gm.AddComponent(typeof(GuideFengShui)) as GuideFengShui;

				}
			}
			return _instance;			
		}

	}
	
	public void Init(GuidePlayer gp, MainUILogic mul){
		InitGuide(gp, mul);
		curstep = (int)GUIDE_FENGSHUI_STEP.NONE;
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
		switch((GUIDE_FENGSHUI_STEP)curstep){
			
		case GUIDE_FENGSHUI_STEP.NONE:						break;//error
		default:
			StartCoroutine(ShowGuideLeader((GUIDE_FENGSHUI_STEP)curstep));
			break;
			
		}
		
		curstep++;
	}
	public void ButtonNextStep(GameObject Button){
		NextStep();
	}
	IEnumerator ShowGuideLeader(GUIDE_FENGSHUI_STEP currentstep){
		
		GameObject main1 = null;
		GameObject obj=null;
		
		switch(currentstep){
			
		case GUIDE_FENGSHUI_STEP.END:
			
			GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.LEADER);
			//check下一步引导
			GuideManager.Instance.checkGuideState();
			//删除指引脚本
			Destroy(Instance.gameObject);
			
			break;
			
		}
		
		yield return new WaitForSeconds(waitSecond);
		
	}
}
