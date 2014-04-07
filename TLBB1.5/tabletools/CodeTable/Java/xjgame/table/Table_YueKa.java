//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_YueKa implements ITableOperate {
 public final static String TAB_FILE_DATA = "/YueKa.txt";
 public static final int ID_FLAG = 0 ;
public static final int ID_STARTDATE = 1 ;
public static final int ID_DAYS = 2 ;
public static final int ID_REWARD_DAYS = 3 ;
public static final int ID_RMB = 4 ;
public static final int ID_DROP_TYPE_1 = 5 ;
public static final int ID_DROP_ID_1 = 6 ;
public static final int ID_DROP_NUM_1 = 7 ;
public static final int ID_DROP_TYPE_2 = 8 ;
public static final int ID_DROP_ID_2 = 9 ;
public static final int ID_DROP_NUM_2 = 10 ;
public static final int ID_DROP_TYPE_3 = 11 ;
public static final int ID_DROP_ID_3 = 12 ;
public static final int ID_DROP_NUM_3 = 13 ;
public static final int ID_DROP_TYPE_4 = 14 ;
public static final int ID_DROP_ID_4 = 15 ;
public static final int ID_DROP_NUM_4 = 16 ;
public static final int ID_DROP_TYPE_5 = 17 ;
public static final int ID_DROP_ID_5 = 18 ;
public static final int ID_DROP_NUM_5 = 19 ;
public static final int MAX_RECORD = 20 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Days;
 public int GetDays() { return m_Days;}

private int m_DropID[] = new int[5];
 public int GetDropIDbyIndex(int idx) {
 if(idx>=0 && idx<5) return m_DropID[idx];
 return -1;
 }

private int m_DropNum[] = new int[5];
 public int GetDropNumbyIndex(int idx) {
 if(idx>=0 && idx<5) return m_DropNum[idx];
 return -1;
 }

private int m_DropType[] = new int[5];
 public int GetDropTypebyIndex(int idx) {
 if(idx>=0 && idx<5) return m_DropType[idx];
 return -1;
 }

private int m_Flag;
 public int GetFlag() { return m_Flag;}

private int m_RewardDays;
 public int GetRewardDays() { return m_RewardDays;}

private int m_Rmb;
 public int GetRmb() { return m_Rmb;}

private int m_Startdate;
 public int GetStartdate() { return m_Startdate;}

public boolean LoadTable(HashMap<Integer, Table_YueKa> _tab) throws TableException
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
 HashMap<Integer, Table_YueKa> _hash = (HashMap<Integer, Table_YueKa> )obj;
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
 Table_YueKa _values = new Table_YueKa();
 _values.m_Days =  Integer.parseInt((String)valuesList.get(ID_DAYS));
_values.m_DropID [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_ID_1));
_values.m_DropID [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_ID_2));
_values.m_DropID [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_ID_3));
_values.m_DropID [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_ID_4));
_values.m_DropID [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_ID_5));
_values.m_DropNum [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_NUM_1));
_values.m_DropNum [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_NUM_2));
_values.m_DropNum [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_NUM_3));
_values.m_DropNum [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_NUM_4));
_values.m_DropNum [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_NUM_5));
_values.m_DropType [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_TYPE_1));
_values.m_DropType [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_TYPE_2));
_values.m_DropType [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_TYPE_3));
_values.m_DropType [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_TYPE_4));
_values.m_DropType [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_TYPE_5));
_values.m_Flag =  Integer.parseInt((String)valuesList.get(ID_FLAG));
_values.m_RewardDays =  Integer.parseInt((String)valuesList.get(ID_REWARD_DAYS));
_values.m_Rmb =  Integer.parseInt((String)valuesList.get(ID_RMB));
_values.m_Startdate =  Integer.parseInt((String)valuesList.get(ID_STARTDATE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

