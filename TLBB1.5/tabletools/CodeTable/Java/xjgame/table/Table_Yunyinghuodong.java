//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Yunyinghuodong implements ITableOperate {
 public final static String TAB_FILE_DATA = "/yunyinghuodong.txt";
 public static final int ID_DECRIPTION = 0 ;
public static final int ID_TRIGGER_TYPE = 1 ;
public static final int ID_TRIGGER_VALUE = 2 ;
public static final int ID_END_TYPE = 3 ;
public static final int ID_END_VALUE = 4 ;
public static final int ID_REWARD_TYPE = 5 ;
public static final int ID_REWARD_VALUE = 6 ;
public static final int ID_REWARD_NUM = 7 ;
public static final int MAX_RECORD = 8 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_Decription;
 public String GetDecription() { return m_Decription;}

private int m_EndType;
 public int GetEndType() { return m_EndType;}

private String m_EndValue;
 public String GetEndValue() { return m_EndValue;}

private String m_RewardNum;
 public String GetRewardNum() { return m_RewardNum;}

private String m_RewardType;
 public String GetRewardType() { return m_RewardType;}

private String m_RewardValue;
 public String GetRewardValue() { return m_RewardValue;}

private int m_TriggerType;
 public int GetTriggerType() { return m_TriggerType;}

private int m_TriggerValue;
 public int GetTriggerValue() { return m_TriggerValue;}

public boolean LoadTable(HashMap<Integer, Table_Yunyinghuodong> _tab) throws TableException
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
 HashMap<Integer, Table_Yunyinghuodong> _hash = (HashMap<Integer, Table_Yunyinghuodong> )obj;
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
 Table_Yunyinghuodong _values = new Table_Yunyinghuodong();
 _values.m_Decription =  (String)valuesList.get(ID_DECRIPTION);
_values.m_EndType =  Integer.parseInt((String)valuesList.get(ID_END_TYPE));
_values.m_EndValue =  (String)valuesList.get(ID_END_VALUE);
_values.m_RewardNum =  (String)valuesList.get(ID_REWARD_NUM);
_values.m_RewardType =  (String)valuesList.get(ID_REWARD_TYPE);
_values.m_RewardValue =  (String)valuesList.get(ID_REWARD_VALUE);
_values.m_TriggerType =  Integer.parseInt((String)valuesList.get(ID_TRIGGER_TYPE));
_values.m_TriggerValue =  Integer.parseInt((String)valuesList.get(ID_TRIGGER_VALUE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

