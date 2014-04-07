using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
////using Module.Log;

public class UIDragController : MonoBehaviour {
	
	public GameObject battleBefore;
	
	private int sitNum;
	private float cx;
	private float cy;
	
	private float screenHeight;
	private GameObject mainLogic;
	
	void Awake()
	{
		sitNum = int.Parse(transform.name.Substring(9,1));//使用名称获取坐标位置CardLarge0
		cx = transform.localPosition.x;
		cy = transform.localPosition.y;
	}
	// Use this for initialization
	void Start () {
		mainLogic = GameObject.Find("MainUILogic");
		if(mainLogic == null)
		{
			Debug.LogError("can not find MainUILogic !");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDrag(Vector2 delta)
	{
		if(battleBefore == null)
		{
			Debug.LogError("Cant not find BattleBeforeController !");
		}
		
		if(mainLogic == null || battleBefore == null)
		{
			return;
		}
		Debug.Log("OnDrag(), delta = " + delta.x + ", " + delta.y);
		
		delta *= screenHeight/Screen.height;
		transform.localPosition += (Vector3)delta;
	}
	
	void OnPress(bool isPress)
	{
		//daixiugai
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_1)
			GuideOnPress(isPress);
		else
			NormalOnPress(isPress);
	}
	
	void GuideOnPress(bool isPress){
		if(battleBefore == null)
		{
			Debug.LogError("Cant not find BattleBeforeController !");
		}
		if(mainLogic == null || battleBefore == null)
		{
			return;
		}
		Debug.Log("OnPress(), isPress = " + isPress);
		
		float[] cardx = {-231, -133, -51, 45, 122, 228};
		float[] cardy = {206, 73, 42, -90};
		if(isPress)
		{
			screenHeight = GameObject.FindWithTag("UIRoot").GetComponent<UIRoot>().manualHeight;
			transform.localPosition += new Vector3(0, 0, -20);
			//daixiugai
			GuideCopy1_1.Instance.player.StopCopyGuide();//按下时，移动卡牌，隐藏指引动画
		}
		else
		{
			transform.localPosition += new Vector3(0, 0, 20);
			float fx = transform.localPosition.x;
			float fy = transform.localPosition.y;
			int num = -1;
			if(fx < cardx[3] && fx > cardx[2])
			{
				if(fy < cardy[2] && fy > cardy[3])
					num = 4;
			}
			Debug.Log("num="+num+"; sitNum="+sitNum);
			transform.localPosition = new Vector3(cx,cy,transform.localPosition.z);
			if(num <0){
				//daixiugai
				GuideCopy1_1.Instance.player.PlayCopyGuide();//位置问题，继续播放指引动画
				return;//退出，不改变
			}
			long temp;
			temp = Obj_MyselfPlayer.GetMe().battleArray[num];
			Obj_MyselfPlayer.GetMe().battleArray[num] = Obj_MyselfPlayer.GetMe().battleArray[sitNum];
			Obj_MyselfPlayer.GetMe().battleArray[sitNum] = temp;

            //如果当前移动的地方有助战好友，保存助战好友新位置
            if (num == Obj_MyselfPlayer.GetMe().nfightFriendPos)
            {
                Obj_MyselfPlayer.GetMe().nfightFriendPos = sitNum;
            }
            else if (sitNum == Obj_MyselfPlayer.GetMe().nfightFriendPos)
            {
                Obj_MyselfPlayer.GetMe().nfightFriendPos = num;
            }
			
			Debug.Log("wo yun 6:");
            Obj_MyselfPlayer.GetMe().SavebattleArray();
			
			if (battleBefore.name == "BattleBeforeController(Clone)" || battleBefore.name == "BattleBeforeController")
				battleBefore.transform.GetComponent<BattleBeforeController>().FreshUI();
			else if (battleBefore.name == "PVPBattleBeforeController(Clone)" || battleBefore.name == "PVPBattleBeforeController")
				battleBefore.transform.GetComponent<PVPBattleBeforeController>().FreshUI();
			GuideCopy1_1.Instance.NextStep();
		}
	}
	
	void NormalOnPress(bool isPress){
		
		bool isNewPVP = this.transform.parent.parent.name == "PVPBattleBeforeController(Clone)" || this.transform.parent.parent.name == "PVPBattleBeforeController";
		
		
		if(battleBefore == null)
		{
			Debug.LogError("Cant not find BattleBeforeController !");
		}
		if(mainLogic == null || battleBefore == null)
		{
			return;
		}
		Debug.Log("OnPress(), isPress = " + isPress);
		
		float[] cardx = {-231, -133, -51, 45, 122, 228};
		float[] cardy = {206, 73, 42, -90};
		if(isPress)
		{
			screenHeight = GameObject.FindWithTag("UIRoot").GetComponent<UIRoot>().manualHeight;
#if UNITY_ANDROID
			transform.localPosition += new Vector3(0, 0, -40);
#else
			transform.localPosition += new Vector3(0, 0, -20);
#endif
		}
		else
		{
#if UNITY_ANDROID
			transform.localPosition += new Vector3(0, 0, 40);
#else
			transform.localPosition += new Vector3(0, 0, 20);
#endif
			float fx = transform.localPosition.x;
			float fy = transform.localPosition.y;
			int num = -1;
			if(fx < cardx[1] && fx > cardx[0])
			{
				if(fy < cardy[0] && fy > cardy[1])
					num = 0;
				else if(fy < cardy[2] && fy > cardy[3])
					num = 3;
			}
			else if(fx < cardx[3] && fx > cardx[2])
			{
				if(fy < cardy[0] && fy > cardy[1])
					num = 1;
				else if(fy < cardy[2] && fy > cardy[3])
					num = 4;
			}
			else if(fx < cardx[5] && fx > cardx[4])
			{
				if(fy < cardy[0] && fy > cardy[1])
					num = 2;
				else if(fy < cardy[2] && fy > cardy[3])
					num = 5;
			}
			Debug.Log("num="+num+"; sitNum="+sitNum);
			transform.localPosition = new Vector3(cx,cy,transform.localPosition.z);
			if(num <0)
				return;//退出，不改变
			
			long temp;
			if (isNewPVP)
			{
				temp = Obj_MyselfPlayer.GetMe().PvPBattleArray[num];
				Obj_MyselfPlayer.GetMe().PvPBattleArray[num] = Obj_MyselfPlayer.GetMe().PvPBattleArray[sitNum];
				Obj_MyselfPlayer.GetMe().PvPBattleArray[sitNum] = temp;
			}else{
				temp = Obj_MyselfPlayer.GetMe().battleArray[num];
				Obj_MyselfPlayer.GetMe().battleArray[num] = Obj_MyselfPlayer.GetMe().battleArray[sitNum];
				Obj_MyselfPlayer.GetMe().battleArray[sitNum] = temp;
			}
			
			if (!isNewPVP)
			{
				//如果当前移动的地方有助战好友，保存助战好友新位置
	            if (num == Obj_MyselfPlayer.GetMe().nfightFriendPos)
	            {
	                Obj_MyselfPlayer.GetMe().nfightFriendPos = sitNum;
	            }
	            else if (sitNum == Obj_MyselfPlayer.GetMe().nfightFriendPos)
	            {
	                Obj_MyselfPlayer.GetMe().nfightFriendPos = num;
	            }
	            Obj_MyselfPlayer.GetMe().SavebattleArray();
			}
            
			if (!isNewPVP)
				battleBefore.transform.GetComponent<BattleBeforeController>().FreshUI();
			else
				battleBefore.transform.GetComponent<PVPBattleBeforeController>().FreshUI();
		}
	}
}
