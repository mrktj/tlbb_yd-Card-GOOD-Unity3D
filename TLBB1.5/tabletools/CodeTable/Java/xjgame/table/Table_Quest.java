//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Quest implements ITableOperate {
 public final static String TAB_FILE_DATA = "/quest.txt";
 public static final int ID_NAME = 0 ;
public static final int ID_IS_LOOP = 1 ;
public static final int ID_QUEST_TYPE = 2 ;
public static final int ID_QUEST_VALUE = 3 ;
public static final int ID_BACK_ID = 4 ;
public static final int ID_REWARD_GOLD = 5 ;
public static final int ID_REWARD_DOLLAR = 6 ;
public static final int ID_REWARD_POWER = 7 ;
public static final int ID_REWARD_CHANCE = 8 ;
public static final int ID_REWARD_ID = 9 ;
public static final int ID_ATTITEM_NUM = 10 ;
public static final int ID_HPITEM_NUM = 11 ;
public static final int ID_REWARD_ONE = 12 ;
public static final int ID_REWARD_TWO = 13 ;
public static final int MAX_RECORD = 14 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_AttitemNUM;
 public int GetAttitemNUM() { return m_AttitemNUM;}

private int m_BackID;
 public int GetBackID() { return m_BackID;}

private int m_HpitemNUM;
 public int GetHpitemNUM() { return m_HpitemNUM;}

private int m_IsLoop;
 public int GetIsLoop() { return m_IsLoop;}

private String m_Name;
 public String GetName() { return m_Name;}

private int m_QuestType;
 public int GetQuestType() { return m_QuestType;}

private int m_QuestValue;
 public int GetQuestValue() { return m_QuestValue;}

private int m_RewardID;
 public int GetRewardID() { return m_RewardID;}

private int m_RewardChance;
 public int GetRewardChance() { return m_RewardChance;}

private int m_RewardDollar;
 public int GetRewardDollar() { return m_RewardDollar;}

private int m_RewardGold;
 public int GetRewardGold() { return m_RewardGold;}

private String m_RewardOne;
 public String GetRewardOne() { return m_RewardOne;}

private int m_RewardPower;
 public int GetRewardPower() { return m_RewardPower;}

private String m_RewardTwo;
 public String GetRewardTwo() { return m_RewardTwo;}

public boolean LoadTable(HashMap<Integer, Table_Quest> _tab) throws TableException
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
 HashMap<Integer, Table_Quest> _hash = (HashMap<Integer, Table_Quest> )obj;
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
 Table_Quest _values = new Table_Quest();
 _values.m_AttitemNUM =  Integer.parseInt((String)valuesList.get(ID_ATTITEM_NUM));
_values.m_BackID =  Integer.parseInt((String)valuesList.get(ID_BACK_ID));
_values.m_HpitemNUM =  Integer.parseInt((String)valuesList.get(ID_HPITEM_NUM));
_values.m_IsLoop =  Integer.parseInt((String)valuesList.get(ID_IS_LOOP));
_values.m_Name =  (String)valuesList.get(ID_NAME);
_values.m_QuestType =  Integer.parseInt((String)valuesList.get(ID_QUEST_TYPE));
_values.m_QuestValue =  Integer.parseInt((String)valuesList.get(ID_QUEST_VALUE));
_values.m_RewardID =  Integer.parseInt((String)valuesList.get(ID_REWARD_ID));
_values.m_RewardChance =  Integer.parseInt((String)valuesList.get(ID_REWARD_CHANCE));
_values.m_RewardDollar =  Integer.parseInt((String)valuesList.get(ID_REWARD_DOLLAR));
_values.m_RewardGold =  Integer.parseInt((String)valuesList.get(ID_REWARD_GOLD));
_values.m_RewardOne =  (String)valuesList.get(ID_REWARD_ONE);
_values.m_RewardPower =  Integer.parseInt((String)valuesList.get(ID_REWARD_POWER));
_values.m_RewardTwo =  (String)valuesList.get(ID_REWARD_TWO);

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

