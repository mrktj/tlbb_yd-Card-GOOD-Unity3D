using UnityEngine;
using System.Collections;

public class SoundResource {

	public enum SoundRes
	{
		background,		
		//游戏刚进去的初始画面
		enter,
		
		/******战斗相关************/
		battle_start,   //战斗开?		
        battle_win,     //战斗胜利
		battle_lose,    //战斗失败
		battle_go,      //战斗中，卡牌移动
		battle_phase,   //战斗阶段音效 (1/3,  2/3 , 3/3)
		battle_boss,	//BOSS出场
		battle_carddie,	//己方卡牌阵亡
		battle_enemydie,//敌方卡牌阵亡
		battle_revive,	//卡牌复活
		battle_itemdrop,//道具掉落
		battle_moneydrop,//金钱掉落--
	}
}
