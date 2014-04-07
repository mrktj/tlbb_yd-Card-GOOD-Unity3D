//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Eightsided implements ITableOperate {
 public final static String TAB_FILE_DATA = "/eightsided.txt";
 public static final int ID_DAY_TIME = 0 ;
public static final int ID_REWARD1_TYPE = 1 ;
public static final int ID_VALUE_1 = 2 ;
public static final int ID_REWARD1_CHANCE = 3 ;
public static final int ID_REWARD2_TYPE = 4 ;
public static final int ID_VALUE_2 = 5 ;
public static final int ID_REWARD2_CHANCE = 6 ;
public static final int ID_REWARD3_TYPE = 7 ;
public static final int ID_VALUE_3 = 8 ;
public static final int ID_REWARD3_CHANCE = 9 ;
public static final int ID_REWARD4_TYPE = 10 ;
public static final int ID_VALUE_4 = 11 ;
public static final int ID_REWARD4_CHANCE = 12 ;
public static final int ID_REWARD5_TYPE = 13 ;
public static final int ID_VALUE_5 = 14 ;
public static final int ID_REWARD5_CHANCE = 15 ;
public static final int ID_REWARD6_TYPE = 16 ;
public static final int ID_VALUE_6 = 17 ;
public static final int ID_REWARD6_CHANCE = 18 ;
public static final int ID_REWARD7_TYPE = 19 ;
public static final int ID_VALUE_7 = 20 ;
public static final int ID_REWARD7_CHANCE = 21 ;
public static final int ID_REWARD8_TYPE = 22 ;
public static final int ID_VALUE_8 = 23 ;
public static final int ID_REWARD8_CHANCE = 24 ;
public static final int MAX_RECORD = 25 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_ReWard1Chance;
 public int GetReWard1Chance() { return m_ReWard1Chance;}

private int m_ReWard1Type;
 public int GetReWard1Type() { return m_ReWard1Type;}

private int m_ReWard2Chance;
 public int GetReWard2Chance() { return m_ReWard2Chance;}

private int m_ReWard2Type;
 public int GetReWard2Type() { return m_ReWard2Type;}

private int m_ReWard3Chance;
 public int GetReWard3Chance() { return m_ReWard3Chance;}

private int m_ReWard3Type;
 public int GetReWard3Type() { return m_ReWard3Type;}

private int m_ReWard4Chance;
 public int GetReWard4Chance() { return m_ReWard4Chance;}

private int m_ReWard4Type;
 public int GetReWard4Type() { return m_ReWard4Type;}

private int m_ReWard5Chance;
 public int GetReWard5Chance() { return m_ReWard5Chance;}

private int m_ReWard5Type;
 public int GetReWard5Type() { return m_ReWard5Type;}

private int m_ReWard6Chance;
 public int GetReWard6Chance() { return m_ReWard6Chance;}

private int m_ReWard6Type;
 public int GetReWard6Type() { return m_ReWard6Type;}

private int m_ReWard7Chance;
 public int GetReWard7Chance() { return m_ReWard7Chance;}

private int m_ReWard7Type;
 public int GetReWard7Type() { return m_ReWard7Type;}

private int m_ReWard8Chance;
 public int GetReWard8Chance() { return m_ReWard8Chance;}

private int m_ReWard8Type;
 public int GetReWard8Type() { return m_ReWard8Type;}

private int m_DayTime;
 public int GetDayTime() { return m_DayTime;}

private int m_Value[] = new int[8];
 public int GetValuebyIndex(int idx) {
 if(idx>=0 && idx<8) return m_Value[idx];
 return -1;
 }

public boolean LoadTable(HashMap<Integer, Table_Eightsided> _tab) throws TableException
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
 HashMap<Integer, Table_Eightsided> _hash = (HashMap<Integer, Table_Eightsided> )obj;
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
 Table_Eightsided _values = new Table_Eightsided();
 _values.m_ReWard1Chance =  Integer.parseInt((String)valuesList.get(ID_REWARD1_CHANCE));
_values.m_ReWard1Type =  Integer.parseInt((String)valuesList.get(ID_REWARD1_TYPE));
_values.m_ReWard2Chance =  Integer.parseInt((String)valuesList.get(ID_REWARD2_CHANCE));
_values.m_ReWard2Type =  Integer.parseInt((String)valuesList.get(ID_REWARD2_TYPE));
_values.m_ReWard3Chance =  Integer.parseInt((String)valuesList.get(ID_REWARD3_CHANCE));
_values.m_ReWard3Type =  Integer.parseInt((String)valuesList.get(ID_REWARD3_TYPE));
_values.m_ReWard4Chance =  Integer.parseInt((String)valuesList.get(ID_REWARD4_CHANCE));
_values.m_ReWard4Type =  Integer.parseInt((String)valuesList.get(ID_REWARD4_TYPE));
_values.m_ReWard5Chance =  Integer.parseInt((String)valuesList.get(ID_REWARD5_CHANCE));
_values.m_ReWard5Type =  Integer.parseInt((String)valuesList.get(ID_REWARD5_TYPE));
_values.m_ReWard6Chance =  Integer.parseInt((String)valuesList.get(ID_REWARD6_CHANCE));
_values.m_ReWard6Type =  Integer.parseInt((String)valuesList.get(ID_REWARD6_TYPE));
_values.m_ReWard7Chance =  Integer.parseInt((String)valuesList.get(ID_REWARD7_CHANCE));
_values.m_ReWard7Type =  Integer.parseInt((String)valuesList.get(ID_REWARD7_TYPE));
_values.m_ReWard8Chance =  Integer.parseInt((String)valuesList.get(ID_REWARD8_CHANCE));
_values.m_ReWard8Type =  Integer.parseInt((String)valuesList.get(ID_REWARD8_TYPE));
_values.m_DayTime =  Integer.parseInt((String)valuesList.get(ID_DAY_TIME));
_values.m_Value [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_VALUE_1));
_values.m_Value [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_VALUE_2));
_values.m_Value [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_VALUE_3));
_values.m_Value [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_VALUE_4));
_values.m_Value [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_VALUE_5));
_values.m_Value [ 5 ] =  Integer.parseInt((String)valuesList.get(ID_VALUE_6));
_values.m_Value [ 6 ] =  Integer.parseInt((String)valuesList.get(ID_VALUE_7));
_values.m_Value [ 7 ] =  Integer.parseInt((String)valuesList.get(ID_VALUE_8));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

