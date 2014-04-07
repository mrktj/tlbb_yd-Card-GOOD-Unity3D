//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Card : ITableOperate
{ private const string TAB_FILE_DATA = "card.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_NOTE,
ID_APPEARANCE,
ID_STAR,
ID_NEXT_CARD,
ID_LEVEL_BASE,
ID_MAX_LEVEL,
ID_EXP_BASE,
ID_PRICE_BASE,
ID_SELL_BASE,
ID_LEADER_BASE,
ID_ELEMENT,
ID_ATTACK_TYPE,
ID_ATTACK_BASE,
ID_ATTACK_GROW,
ID_HP_BASE,
ID_HP_GROW,
ID_STORM_BASE,
ID_TENACITY_BASE,
ID_PRECISE_BASE,
ID_DODGE_BASE,
ID_STORM_RATIO,
ID_ATTACK_STORM,
ID_PH_DF_BASE,
ID_PH_DF_GROW,
ID_MG_DF_BASE,
ID_MG_DF_GROW,
ID_PH_NT_BASE,
ID_MG_NT_BASE,
ID_SKILL_LEADER,
ID_SKILL_COMM,
ID_SKILL_VOL,
ID_PROBABILITY,
ID_SKILL_COMB,
ID_COMB_GROUP,
ID_IS_BOSS,
ID_DROP_ID,
ID_HIGHSTAR_DISPLAY,
ID_DROP_LEVEL,
ID_GAMBLE_LEVEL,
ID_CARD_TYPE,
ID_SKILL_STUDY,
ID_SKILL_STUDYLV,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Appearance;
 public int Appearance { get{ return m_Appearance;}}

private int m_AttackBase;
 public int AttackBase { get{ return m_AttackBase;}}

private int m_AttackGrow;
 public int AttackGrow { get{ return m_AttackGrow;}}

private int m_AttackStorm;
 public int AttackStorm { get{ return m_AttackStorm;}}

private int m_AttackType;
 public int AttackType { get{ return m_AttackType;}}

private int m_CardType;
 public int CardType { get{ return m_CardType;}}

private int m_CombGroup;
 public int CombGroup { get{ return m_CombGroup;}}

private int m_DodgeBase;
 public int DodgeBase { get{ return m_DodgeBase;}}

private int m_DropID;
 public int DropID { get{ return m_DropID;}}

private int m_DropLevel;
 public int DropLevel { get{ return m_DropLevel;}}

private int m_Element;
 public int Element { get{ return m_Element;}}

private int m_ExpBase;
 public int ExpBase { get{ return m_ExpBase;}}

private int m_GambleLevel;
 public int GambleLevel { get{ return m_GambleLevel;}}

private int m_HighStarDisplay;
 public int HighStarDisplay { get{ return m_HighStarDisplay;}}

private int m_HpBase;
 public int HpBase { get{ return m_HpBase;}}

private int m_HpGrow;
 public int HpGrow { get{ return m_HpGrow;}}

private int m_IsBoss;
 public int IsBoss { get{ return m_IsBoss;}}

private int m_LeaderBase;
 public int LeaderBase { get{ return m_LeaderBase;}}

private int m_LevelBase;
 public int LevelBase { get{ return m_LevelBase;}}

private int m_MaxLevel;
 public int MaxLevel { get{ return m_MaxLevel;}}

private int m_MgDfBase;
 public int MgDfBase { get{ return m_MgDfBase;}}

private int m_MgDfGrow;
 public int MgDfGrow { get{ return m_MgDfGrow;}}

private int m_MgNtBase;
 public int MgNtBase { get{ return m_MgNtBase;}}

private int m_NextCard;
 public int NextCard { get{ return m_NextCard;}}

private string m_Note;
 public string Note { get{ return m_Note;}}

private int m_PhDfBase;
 public int PhDfBase { get{ return m_PhDfBase;}}

private int m_PhDfGrow;
 public int PhDfGrow { get{ return m_PhDfGrow;}}

private int m_PhNtBase;
 public int PhNtBase { get{ return m_PhNtBase;}}

private int m_PreciseBase;
 public int PreciseBase { get{ return m_PreciseBase;}}

private int m_PriceBase;
 public int PriceBase { get{ return m_PriceBase;}}

private int m_Probability;
 public int Probability { get{ return m_Probability;}}

private int m_SellBase;
 public int SellBase { get{ return m_SellBase;}}

private int m_SkillComb;
 public int SkillComb { get{ return m_SkillComb;}}

private int m_SkillComm;
 public int SkillComm { get{ return m_SkillComm;}}

private int m_SkillLeader;
 public int SkillLeader { get{ return m_SkillLeader;}}

private int m_SkillStudy;
 public int SkillStudy { get{ return m_SkillStudy;}}

private int m_SkillStudylv;
 public int SkillStudylv { get{ return m_SkillStudylv;}}

private int m_SkillVol;
 public int SkillVol { get{ return m_SkillVol;}}

private int m_Star;
 public int Star { get{ return m_Star;}}

private int m_StormBase;
 public int StormBase { get{ return m_StormBase;}}

private int m_StormRatio;
 public int StormRatio { get{ return m_StormRatio;}}

private int m_TenacityBase;
 public int TenacityBase { get{ return m_TenacityBase;}}

public bool LoadTable(Hashtable _tab)
 {
 if(!TableManager.ReaderPList(GetInstanceFile(),SerializableTable,_tab))
 {
 throw TableException.ErrorReader("Load File{0} Fail!!!",GetInstanceFile());
 }
 return true;
 }
 public void SerializableTable(ArrayList valuesList,string skey,Hashtable _hash)
 {
 if (string.IsNullOrEmpty(skey))
 {
 throw TableException.ErrorReader("Read File{0} as key is Empty Fail!!!", GetInstanceFile());
 }

 if ((int)_ID.MAX_RECORD!=valuesList.Count)
 {
 throw TableException.ErrorReader("Load {0} error as CodeSize:{1} not Equal DataSize:{2}", GetInstanceFile(),_ID.MAX_RECORD,valuesList.Count);
 }
 Int32 nKey = Convert.ToInt32(skey);
 Tab_Card _values = new Tab_Card();
 _values.m_Appearance =  Convert.ToInt32(valuesList[(int)_ID.ID_APPEARANCE] as string);
_values.m_AttackBase =  Convert.ToInt32(valuesList[(int)_ID.ID_ATTACK_BASE] as string);
_values.m_AttackGrow =  Convert.ToInt32(valuesList[(int)_ID.ID_ATTACK_GROW] as string);
_values.m_AttackStorm =  Convert.ToInt32(valuesList[(int)_ID.ID_ATTACK_STORM] as string);
_values.m_AttackType =  Convert.ToInt32(valuesList[(int)_ID.ID_ATTACK_TYPE] as string);
_values.m_CardType =  Convert.ToInt32(valuesList[(int)_ID.ID_CARD_TYPE] as string);
_values.m_CombGroup =  Convert.ToInt32(valuesList[(int)_ID.ID_COMB_GROUP] as string);
_values.m_DodgeBase =  Convert.ToInt32(valuesList[(int)_ID.ID_DODGE_BASE] as string);
_values.m_DropID =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_ID] as string);
_values.m_DropLevel =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_LEVEL] as string);
_values.m_Element =  Convert.ToInt32(valuesList[(int)_ID.ID_ELEMENT] as string);
_values.m_ExpBase =  Convert.ToInt32(valuesList[(int)_ID.ID_EXP_BASE] as string);
_values.m_GambleLevel =  Convert.ToInt32(valuesList[(int)_ID.ID_GAMBLE_LEVEL] as string);
_values.m_HighStarDisplay =  Convert.ToInt32(valuesList[(int)_ID.ID_HIGHSTAR_DISPLAY] as string);
_values.m_HpBase =  Convert.ToInt32(valuesList[(int)_ID.ID_HP_BASE] as string);
_values.m_HpGrow =  Convert.ToInt32(valuesList[(int)_ID.ID_HP_GROW] as string);
_values.m_IsBoss =  Convert.ToInt32(valuesList[(int)_ID.ID_IS_BOSS] as string);
_values.m_LeaderBase =  Convert.ToInt32(valuesList[(int)_ID.ID_LEADER_BASE] as string);
_values.m_LevelBase =  Convert.ToInt32(valuesList[(int)_ID.ID_LEVEL_BASE] as string);
_values.m_MaxLevel =  Convert.ToInt32(valuesList[(int)_ID.ID_MAX_LEVEL] as string);
_values.m_MgDfBase =  Convert.ToInt32(valuesList[(int)_ID.ID_MG_DF_BASE] as string);
_values.m_MgDfGrow =  Convert.ToInt32(valuesList[(int)_ID.ID_MG_DF_GROW] as string);
_values.m_MgNtBase =  Convert.ToInt32(valuesList[(int)_ID.ID_MG_NT_BASE] as string);
_values.m_NextCard =  Convert.ToInt32(valuesList[(int)_ID.ID_NEXT_CARD] as string);
_values.m_Note =  valuesList[(int)_ID.ID_NOTE] as string;
_values.m_PhDfBase =  Convert.ToInt32(valuesList[(int)_ID.ID_PH_DF_BASE] as string);
_values.m_PhDfGrow =  Convert.ToInt32(valuesList[(int)_ID.ID_PH_DF_GROW] as string);
_values.m_PhNtBase =  Convert.ToInt32(valuesList[(int)_ID.ID_PH_NT_BASE] as string);
_values.m_PreciseBase =  Convert.ToInt32(valuesList[(int)_ID.ID_PRECISE_BASE] as string);
_values.m_PriceBase =  Convert.ToInt32(valuesList[(int)_ID.ID_PRICE_BASE] as string);
_values.m_Probability =  Convert.ToInt32(valuesList[(int)_ID.ID_PROBABILITY] as string);
_values.m_SellBase =  Convert.ToInt32(valuesList[(int)_ID.ID_SELL_BASE] as string);
_values.m_SkillComb =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_COMB] as string);
_values.m_SkillComm =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_COMM] as string);
_values.m_SkillLeader =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_LEADER] as string);
_values.m_SkillStudy =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_STUDY] as string);
_values.m_SkillStudylv =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_STUDYLV] as string);
_values.m_SkillVol =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_VOL] as string);
_values.m_Star =  Convert.ToInt32(valuesList[(int)_ID.ID_STAR] as string);
_values.m_StormBase =  Convert.ToInt32(valuesList[(int)_ID.ID_STORM_BASE] as string);
_values.m_StormRatio =  Convert.ToInt32(valuesList[(int)_ID.ID_STORM_RATIO] as string);
_values.m_TenacityBase =  Convert.ToInt32(valuesList[(int)_ID.ID_TENACITY_BASE] as string);

 _hash.Add(nKey,_values); }


}
}

