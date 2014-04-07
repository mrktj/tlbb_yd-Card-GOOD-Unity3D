//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Activities implements ITableOperate {
 public final static String TAB_FILE_DATA = "/activities.txt";
 public static final int ID_FLAG = 0 ;
public static final int ID_STARTDATE = 1 ;
public static final int ID_ENDDATE = 2 ;
public static final int ID_NAME = 3 ;
public static final int ID_CONTENT = 4 ;
public static final int ID_ACTIVITY_TYPE = 5 ;
public static final int ID_ACTIVITY_NUM = 6 ;
public static final int ID_REWARD_TYPE = 7 ;
public static final int ID_REWARD_ID = 8 ;
public static final int ID_REWARD_NUM = 9 ;
public static final int MAX_RECORD = 10 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_ActivityNum;
 public int GetActivityNum() { return m_ActivityNum;}

private int m_ActivityType;
 public int GetActivityType() { return m_ActivityType;}

private String m_Content;
 public String GetContent() { return m_Content;}

private String m_Enddate;
 public String GetEnddate() { return m_Enddate;}

private int m_Flag;
 public int GetFlag() { return m_Flag;}

private String m_Name;
 public String GetName() { return m_Name;}

private int m_RewardId;
 public int GetRewardId() { return m_RewardId;}

private int m_RewardNum;
 public int GetRewardNum() { return m_RewardNum;}

private int m_RewardType;
 public int GetRewardType() { return m_RewardType;}

private String m_Startdate;
 public String GetStartdate() { return m_Startdate;}

public boolean LoadTable(HashMap<Integer, Table_Activities> _tab) throws TableException
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
 HashMap<Integer, Table_Activities> _hash = (HashMap<Integer, Table_Activities> )obj;
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
 Table_Activities _values = new Table_Activities();
 _values.m_ActivityNum =  Integer.parseInt((String)valuesList.get(ID_ACTIVITY_NUM));
_values.m_ActivityType =  Integer.parseInt((String)valuesList.get(ID_ACTIVITY_TYPE));
_values.m_Content =  (String)valuesList.get(ID_CONTENT);
_values.m_Enddate =  (String)valuesList.get(ID_ENDDATE);
_values.m_Flag =  Integer.parseInt((String)valuesList.get(ID_FLAG));
_values.m_Name =  (String)valuesList.get(ID_NAME);
_values.m_RewardId =  Integer.parseInt((String)valuesList.get(ID_REWARD_ID));
_values.m_RewardNum =  Integer.parseInt((String)valuesList.get(ID_REWARD_NUM));
_values.m_RewardType =  Integer.parseInt((String)valuesList.get(ID_REWARD_TYPE));
_values.m_Startdate =  (String)valuesList.get(ID_STARTDATE);

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

