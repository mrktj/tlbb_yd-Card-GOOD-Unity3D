using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
////using Module.Log;
using Games.Battle;

public class DraggableCard : MonoBehaviour {
	
	private GameObject battleLogic;
	private int srcPos = -1;
	private int tarPos = -1;
	
	private float cx;
	private float cy;
	
	private float screenHeight;
	
	float[] cardx = {120f, 264f, 312f, 459f, 522f, 652f};
	float[] cardy = {428f, 270f, 225f, 58f};//这个坐标在战斗中用-
    //private bool isPressed = false;//在鼠标抬起前是否有一次有效的按下
    private Vector3 initPos;
	public bool isDraged = false;
	
	void Awake()
	{

	}
	// Use this for initialization
	void Start () {
		battleLogic = GameObject.Find("BattleLogic");
        initPos = transform.localPosition;

		string cardName = transform.name;
		if(! cardName.StartsWith("BattleCard-"))
		{
			return;
		}
		
		if(cardName.Replace("BattleCard-", "").Length > 0)
		{
			srcPos = int.Parse(cardName.Replace("BattleCard-",""));
			//srcPos = int.Parse(transform.name.Substring(4,1));//使用名称获取坐标位置,CardLarge-0
			cx = transform.localPosition.x;
			cy = transform.localPosition.y;
			BattleSlot[] slots = BattleCardManager.Instance.BattleSlotArray[BattleCardType.E_BATTLE_CARD_TYPE_SELF];
			
			for(int i = 0; i < 3; i++)
			{
				cardx[2 * i] = slots[i].SlotBorder.transform.localPosition.x - 80f;
                cardx[2 * i + 1] = slots[i].SlotBorder.transform.localPosition.x + 80f;
			}
            cardy[0] = slots[0].SlotBorder.transform.localPosition.y + 90f;
            cardy[1] = slots[0].SlotBorder.transform.localPosition.y - 90f;
            cardy[2] = slots[3].SlotBorder.transform.localPosition.y + 90f;
            cardy[3] = slots[3].SlotBorder.transform.localPosition.y - 90f;
		}
	}
	
	
	void OnDrag(Vector2 delta)
	{
		if(battleLogic == null)
		{
			return;
		}
		
		if(Obj_MyselfPlayer.GetMe().isAutoFowrard)
		{
			return;
		}

        //如果卡牌正在播放复活动画，就不要拖动
		if (gameObject.GetComponent<CardUI>().InReviveAnimation)
		{
            return;
		}

		BattleProcedureType state = battleLogic.GetComponent<BattleLogic>().GetBattleCore().GetBattlePlayer().GetBattleStateType();
		if(state != BattleProcedureType.E_BATTLE_PROCEDURE_WAITING)
		{
			return;
		}
		
		if(srcPos >= 0 && srcPos <= 5)
		{
			Debug.Log("OnDrag(), delta = " + delta.x + ", " + delta.y);
		
			delta *= screenHeight/Screen.height;
			transform.localPosition += (Vector3)delta;
		}
		isDraged = true;
	}
	
	void OnPress(bool isPress)
	{
		if(isPress)
		{
			isDraged = false;
		}
		if(battleLogic == null)
		{
			return;
		}

        BattleProcedureType state = battleLogic.GetComponent<BattleLogic>().GetBattleCore().GetBattlePlayer().GetBattleStateType();
        if (state != BattleProcedureType.E_BATTLE_PROCEDURE_WAITING)
        {
            return;
        }
		
		if(srcPos <0 || srcPos > 5)
		{
			return;
		}
				
		Debug.Log("OnPress(), isPress = " + isPress);

		if(isPress)
		{
			screenHeight = 1024f;//GameObject.FindWithTag("UIRoot").GetComponent<UIRoot>().manualHeight;
			transform.localPosition += new Vector3(0f, 0f, -50f);
		}
		else
		{
			transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, initPos.z);
			float fx = transform.localPosition.x;
			float fy = transform.localPosition.y;
			tarPos = -1;
			if(fx < cardx[1] && fx > cardx[0])
			{
				if(fy < cardy[0] && fy > cardy[1])
					tarPos = 0;
				else if(fy < cardy[2] && fy > cardy[3])
					tarPos = 3;
			}
			else if(fx < cardx[3] && fx > cardx[2])
			{
				if(fy < cardy[0] && fy > cardy[1])
					tarPos = 1;
				else if(fy < cardy[2] && fy > cardy[3]) 
					tarPos = 4;
			}
			else if(fx < cardx[5] && fx > cardx[4])
			{
				if(fy < cardy[0] && fy > cardy[1])
					tarPos = 2;
				else if(fy < cardy[2] && fy > cardy[3])
					tarPos = 5;
			}
			Debug.Log("srcPos = "+ srcPos + "; tgtPos = " + tarPos);
			transform.localPosition = new Vector3(cx,cy,transform.localPosition.z);
			if(tarPos < 0 || tarPos == srcPos)
				return;//--退出，不改变--
			
			long temp;
			temp = Obj_MyselfPlayer.GetMe().battleArray[tarPos];
			Obj_MyselfPlayer.GetMe().battleArray[tarPos] = Obj_MyselfPlayer.GetMe().battleArray[srcPos];
			Obj_MyselfPlayer.GetMe().battleArray[srcPos] = temp;


            //如果当前移动的地方有助战好友，保存助战好友新位置
            if (tarPos == Obj_MyselfPlayer.GetMe().nfightFriendPos)                 
            {
                Obj_MyselfPlayer.GetMe().nfightFriendPos = srcPos;
            }
            else if (srcPos == Obj_MyselfPlayer.GetMe().nfightFriendPos)
            {
                Obj_MyselfPlayer.GetMe().nfightFriendPos = tarPos;
            }
			
			Debug.Log("wo yun 1:");
            Obj_MyselfPlayer.GetMe().SavebattleArray();

			BattleCardManager.Instance.OnCardPosChanged(srcPos, tarPos);
		}
	}
	
	public void OnChangeSlot(int index)
	{
		srcPos = index;
		cx = transform.localPosition.x;
		cy = transform.localPosition.y;
	}
}
