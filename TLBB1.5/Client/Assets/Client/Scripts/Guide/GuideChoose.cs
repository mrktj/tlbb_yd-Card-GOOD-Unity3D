using UnityEngine;
using System.Collections;

using Games.CharacterLogic;

public class GuideChoose : GuideBasic {

	public static int[] guide_choose_texts = 
	{
		3050,
	};
	public enum GUIDE_CHOOSE_STEP
	{
		NONE = -1,
		LABEL_1 ,//【角色选择】弹出框
		END,
	}
	
	private static GuideChoose _instance;
	public static GuideChoose Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(GuideChoose)) as GuideChoose;
				if(!_instance)
				{
					GameObject gm = new GameObject("GuideChoose");
					_instance = gm.AddComponent(typeof(GuideChoose)) as GuideChoose;
				}
			}
			return _instance;			
		}
	}
	
	public void Init(GuidePlayer gp, MainUILogic mul){
		InitGuide(gp, mul);
		curstep = (int)GUIDE_CHOOSE_STEP.LABEL_1;
	}
	
	public void PlayGuide(int guideShowMode){
		//guideShowMode无用，此处游戏中断处理相同
		labelIndex=0;
		player.SetPlayerActive(true);
		NextStep();
		player.nextStep = NextStep;
	}
	
	
	
	
	public void NextStep(){
		
		switch((GUIDE_CHOOSE_STEP)curstep){
			
		case GUIDE_CHOOSE_STEP.NONE:						break;//error
		default:
			ShowGuideChoose((GUIDE_CHOOSE_STEP)curstep);	break;
		}
		
		curstep++;
	}
	public void ButtonNextStep(GameObject Button){
		NextStep();
	}
	public void ShowGuideChoose(GUIDE_CHOOSE_STEP currentstep){
		
		switch(currentstep){
			
		case GUIDE_CHOOSE_STEP.LABEL_1://【角色选择】弹出框
			Obj_MyselfPlayer.GetMe().ClearBattleArraySet();
			
			player.SetZhaoPlayer();
			//player.ShowLabel_temp("少室山一战，妞儿和爷们儿失散了，你有勇气带着妞儿去找爷们儿吗？");
			player.ShowLabel(guide_choose_texts[labelIndex++]);
			player.SetZhaoActive(true);
			player.nextStep = NextStep;
				
			break;
			
		case GUIDE_CHOOSE_STEP.END://返回主界面
			
			player.GuideStepEnd();			
			Destroy(Instance.gameObject);
			
			break;
			
		}
	}
}
