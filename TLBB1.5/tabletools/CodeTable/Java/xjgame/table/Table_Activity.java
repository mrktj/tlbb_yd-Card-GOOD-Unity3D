//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Activity implements ITableOperate {
 public final static String TAB_FILE_DATA = "/activity.txt";
 public static final int ID_FLAG = 0 ;
public static final int ID_DESCRIPTION = 1 ;
public static final int ID_DROP_TYPE_1 = 2 ;
public static final int ID_DROP_ID_1 = 3 ;
public static final int ID_DROP_NUM_1 = 4 ;
public static final int ID_DROP_TYPE_2 = 5 ;
public static final int ID_DROP_ID_2 = 6 ;
public static final int ID_DROP_NUM_2 = 7 ;
public static final int ID_DROP_TYPE_3 = 8 ;
public static final int ID_DROP_ID_3 = 9 ;
public static final int ID_DROP_NUM_3 = 10 ;
public static final int ID_DROP_TYPE_4 = 11 ;
public static final int ID_DROP_ID_4 = 12 ;
public static final int ID_DROP_NUM_4 = 13 ;
public static final int ID_DROP_TYPE_5 = 14 ;
public static final int ID_DROP_ID_5 = 15 ;
public static final int ID_DROP_NUM_5 = 16 ;
public static final int MAX_RECORD = 17 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_Description;
 public String GetDescription() { return m_Description;}

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

public boolean LoadTable(HashMap<Integer, Table_Activity> _tab) throws TableException
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
 HashMap<Integer, Table_Activity> _hash = (HashMap<Integer, Table_Activity> )obj;
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
 Table_Activity _values = new Table_Activity();
 _values.m_Description =  (String)valuesList.get(ID_DESCRIPTION);
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

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

