//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Card implements ITableOperate {
 public final static String TAB_FILE_DATA = "/card.txt";
 public static final int ID_NOTE = 0 ;
public static final int ID_APPEARANCE = 1 ;
public static final int ID_STAR = 2 ;
public static final int ID_NEXT_CARD = 3 ;
public static final int ID_LEVEL_BASE = 4 ;
public static final int ID_MAX_LEVEL = 5 ;
public static final int ID_EXP_BASE = 6 ;
public static final int ID_PRICE_BASE = 7 ;
public static final int ID_SELL_BASE = 8 ;
public static final int ID_LEADER_BASE = 9 ;
public static final int ID_ELEMENT = 10 ;
public static final int ID_ATTACK_TYPE = 11 ;
public static final int ID_ATTACK_BASE = 12 ;
public static final int ID_ATTACK_GROW = 13 ;
public static final int ID_HP_BASE = 14 ;
public static final int ID_HP_GROW = 15 ;
public static final int ID_STORM_BASE = 16 ;
public static final int ID_TENACITY_BASE = 17 ;
public static final int ID_PRECISE_BASE = 18 ;
public static final int ID_DODGE_BASE = 19 ;
public static final int ID_STORM_RATIO = 20 ;
public static final int ID_ATTACK_STORM = 21 ;
public static final int ID_PH_DF_BASE = 22 ;
public static final int ID_PH_DF_GROW = 23 ;
public static final int ID_MG_DF_BASE = 24 ;
public static final int ID_MG_DF_GROW = 25 ;
public static final int ID_PH_NT_BASE = 26 ;
public static final int ID_MG_NT_BASE = 27 ;
public static final int ID_SKILL_LEADER = 28 ;
public static final int ID_SKILL_COMM = 29 ;
public static final int ID_SKILL_VOL = 30 ;
public static final int ID_PROBABILITY = 31 ;
public static final int ID_SKILL_COMB = 32 ;
public static final int ID_COMB_GROUP = 33 ;
public static final int ID_IS_BOSS = 34 ;
public static final int ID_DROP_ID = 35 ;
public static final int ID_HIGHSTAR_DISPLAY = 36 ;
public static final int ID_DROP_LEVEL = 37 ;
public static final int ID_GAMBLE_LEVEL = 38 ;
public static final int ID_CARD_TYPE = 39 ;
public static final int ID_SKILL_STUDY = 40 ;
public static final int ID_SKILL_STUDYLV = 41 ;
public static final int MAX_RECORD = 42 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Appearance;
 public int GetAppearance() { return m_Appearance;}

private int m_AttackBase;
 public int GetAttackBase() { return m_AttackBase;}

private int m_AttackGrow;
 public int GetAttackGrow() { return m_AttackGrow;}

private int m_AttackStorm;
 public int GetAttackStorm() { return m_AttackStorm;}

private int m_AttackType;
 public int GetAttackType() { return m_AttackType;}

private int m_CardType;
 public int GetCardType() { return m_CardType;}

private int m_CombGroup;
 public int GetCombGroup() { return m_CombGroup;}

private int m_DodgeBase;
 public int GetDodgeBase() { return m_DodgeBase;}

private int m_DropID;
 public int GetDropID() { return m_DropID;}

private int m_DropLevel;
 public int GetDropLevel() { return m_DropLevel;}

private int m_Element;
 public int GetElement() { return m_Element;}

private int m_ExpBase;
 public int GetExpBase() { return m_ExpBase;}

private int m_GambleLevel;
 public int GetGambleLevel() { return m_GambleLevel;}

private int m_HighStarDisplay;
 public int GetHighStarDisplay() { return m_HighStarDisplay;}

private int m_HpBase;
 public int GetHpBase() { return m_HpBase;}

private int m_HpGrow;
 public int GetHpGrow() { return m_HpGrow;}

private int m_IsBoss;
 public int GetIsBoss() { return m_IsBoss;}

private int m_LeaderBase;
 public int GetLeaderBase() { return m_LeaderBase;}

private int m_LevelBase;
 public int GetLevelBase() { return m_LevelBase;}

private int m_MaxLevel;
 public int GetMaxLevel() { return m_MaxLevel;}

private int m_MgDfBase;
 public int GetMgDfBase() { return m_MgDfBase;}

private int m_MgDfGrow;
 public int GetMgDfGrow() { return m_MgDfGrow;}

private int m_MgNtBase;
 public int GetMgNtBase() { return m_MgNtBase;}

private int m_NextCard;
 public int GetNextCard() { return m_NextCard;}

private String m_Note;
 public String GetNote() { return m_Note;}

private int m_PhDfBase;
 public int GetPhDfBase() { return m_PhDfBase;}

private int m_PhDfGrow;
 public int GetPhDfGrow() { return m_PhDfGrow;}

private int m_PhNtBase;
 public int GetPhNtBase() { return m_PhNtBase;}

private int m_PreciseBase;
 public int GetPreciseBase() { return m_PreciseBase;}

private int m_PriceBase;
 public int GetPriceBase() { return m_PriceBase;}

private int m_Probability;
 public int GetProbability() { return m_Probability;}

private int m_SellBase;
 public int GetSellBase() { return m_SellBase;}

private int m_SkillComb;
 public int GetSkillComb() { return m_SkillComb;}

private int m_SkillComm;
 public int GetSkillComm() { return m_SkillComm;}

private int m_SkillLeader;
 public int GetSkillLeader() { return m_SkillLeader;}

private int m_SkillStudy;
 public int GetSkillStudy() { return m_SkillStudy;}

private int m_SkillStudylv;
 public int GetSkillStudylv() { return m_SkillStudylv;}

private int m_SkillVol;
 public int GetSkillVol() { return m_SkillVol;}

private int m_Star;
 public int GetStar() { return m_Star;}

private int m_StormBase;
 public int GetStormBase() { return m_StormBase;}

private int m_StormRatio;
 public int GetStormRatio() { return m_StormRatio;}

private int m_TenacityBase;
 public int GetTenacityBase() { return m_TenacityBase;}

public boolean LoadTable(HashMap<Integer, Table_Card> _tab) throws TableException
 {
 if(!TableManager.ReaderPList(GetInstanceFile(),this,_tab))
 {
 throw TableException.ErrorReader("Load File{0} Fail!!!",GetInstanceFile());
 }
 return true;
 }

 @Override
 public void SerializableTable(ArrayList<String> valuesList,String skey,Object obj) throws TableException {
 @SuppressWarnings("unchecked")
 HashMap<Integer, Table_Card> _hash = (HashMap<Integer, Table_Card> )obj;
 if (skey.isEmpty())
 {
 throw TableException.ErrorReader("Read File"+GetInstanceFile()+" as key is Empty Fail!!!");
 }

 if (MAX_RECORD!=valuesList.size())
 {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as CodeSize:"+MAX_RECORD+" not Equal DataSize:"+valuesList.size());
 }
 try { 
 Integer nKey = Integer.parseInt(skey);
 Table_Card _values = new Table_Card();
 _values.m_Appearance =  Integer.parseInt((String)valuesList.get(ID_APPEARANCE));
_values.m_AttackBase =  Integer.parseInt((String)valuesList.get(ID_ATTACK_BASE));
_values.m_AttackGrow =  Integer.parseInt((String)valuesList.get(ID_ATTACK_GROW));
_values.m_AttackStorm =  Integer.parseInt((String)valuesList.get(ID_ATTACK_STORM));
_values.m_AttackType =  Integer.parseInt((String)valuesList.get(ID_ATTACK_TYPE));
_values.m_CardType =  Integer.parseInt((String)valuesList.get(ID_CARD_TYPE));
_values.m_CombGroup =  Integer.parseInt((String)valuesList.get(ID_COMB_GROUP));
_values.m_DodgeBase =  Integer.parseInt((String)valuesList.get(ID_DODGE_BASE));
_values.m_DropID =  Integer.parseInt((String)valuesList.get(ID_DROP_ID));
_values.m_DropLevel =  Integer.parseInt((String)valuesList.get(ID_DROP_LEVEL));
_values.m_Element =  Integer.parseInt((String)valuesList.get(ID_ELEMENT));
_values.m_ExpBase =  Integer.parseInt((String)valuesList.get(ID_EXP_BASE));
_values.m_GambleLevel =  Integer.parseInt((String)valuesList.get(ID_GAMBLE_LEVEL));
_values.m_HighStarDisplay =  Integer.parseInt((String)valuesList.get(ID_HIGHSTAR_DISPLAY));
_values.m_HpBase =  Integer.parseInt((String)valuesList.get(ID_HP_BASE));
_values.m_HpGrow =  Integer.parseInt((String)valuesList.get(ID_HP_GROW));
_values.m_IsBoss =  Integer.parseInt((String)valuesList.get(ID_IS_BOSS));
_values.m_LeaderBase =  Integer.parseInt((String)valuesList.get(ID_LEADER_BASE));
_values.m_LevelBase =  Integer.parseInt((String)valuesList.get(ID_LEVEL_BASE));
_values.m_MaxLevel =  Integer.parseInt((String)valuesList.get(ID_MAX_LEVEL));
_values.m_MgDfBase =  Integer.parseInt((String)valuesList.get(ID_MG_DF_BASE));
_values.m_MgDfGrow =  Integer.parseInt((String)valuesList.get(ID_MG_DF_GROW));
_values.m_MgNtBase =  Integer.parseInt((String)valuesList.get(ID_MG_NT_BASE));
_values.m_NextCard =  Integer.parseInt((String)valuesList.get(ID_NEXT_CARD));
_values.m_Note =  (String)valuesList.get(ID_NOTE);
_values.m_PhDfBase =  Integer.parseInt((String)valuesList.get(ID_PH_DF_BASE));
_values.m_PhDfGrow =  Integer.parseInt((String)valuesList.get(ID_PH_DF_GROW));
_values.m_PhNtBase =  Integer.parseInt((String)valuesList.get(ID_PH_NT_BASE));
_values.m_PreciseBase =  Integer.parseInt((String)valuesList.get(ID_PRECISE_BASE));
_values.m_PriceBase =  Integer.parseInt((String)valuesList.get(ID_PRICE_BASE));
_values.m_Probability =  Integer.parseInt((String)valuesList.get(ID_PROBABILITY));
_values.m_SellBase =  Integer.parseInt((String)valuesList.get(ID_SELL_BASE));
_values.m_SkillComb =  Integer.parseInt((String)valuesList.get(ID_SKILL_COMB));
_values.m_SkillComm =  Integer.parseInt((String)valuesList.get(ID_SKILL_COMM));
_values.m_SkillLeader =  Integer.parseInt((String)valuesList.get(ID_SKILL_LEADER));
_values.m_SkillStudy =  Integer.parseInt((String)valuesList.get(ID_SKILL_STUDY));
_values.m_SkillStudylv =  Integer.parseInt((String)valuesList.get(ID_SKILL_STUDYLV));
_values.m_SkillVol =  Integer.parseInt((String)valuesList.get(ID_SKILL_VOL));
_values.m_Star =  Integer.parseInt((String)valuesList.get(ID_STAR));
_values.m_StormBase =  Integer.parseInt((String)valuesList.get(ID_STORM_BASE));
_values.m_StormRatio =  Integer.parseInt((String)valuesList.get(ID_STORM_RATIO));
_values.m_TenacityBase =  Integer.parseInt((String)valuesList.get(ID_TENACITY_BASE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

