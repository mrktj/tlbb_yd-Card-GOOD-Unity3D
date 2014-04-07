using System.Collections;
using System;

namespace Games.LogicObject
{
	//PVP 玩家数据
	public class PVPPlayerInfo 
	{
		public long nlGUID;  	//玩家的GUID(玩家的PlayerID，不是accountID)
		public string strName;  //玩家的名字	
		public int nTempleID;   //主卡牌TempleID
		public int nLev;        //主卡牌等级
        public int nFight;      //玩家战斗力	
        public int nRank;       //玩家排名
		public int skill_level; //主动技能等级
		public int add_quality_att;// 攻击强化次数
		public int add_quality_hp;//生命强化次数
        public int studySkillId;//学习技能id
        public int studySkillLev;//学习技能等级
	}

}
