//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Drop implements ITableOperate {
 public final static String TAB_FILE_DATA = "/drop.txt";
 public static final int ID_PROBABILITY = 0 ;
public static final int ID_DROP_TYPE1 = 1 ;
public static final int ID_DROP_PRO1 = 2 ;
public static final int ID_DROP_VAL1 = 3 ;
public static final int ID_DROP_TYPE2 = 4 ;
public static final int ID_DROP_PRO2 = 5 ;
public static final int ID_DROP_VAL2 = 6 ;
public static final int ID_DROP_TYPE3 = 7 ;
public static final int ID_DROP_PRO3 = 8 ;
public static final int ID_DROP_VAL3 = 9 ;
public static final int ID_DROP_TYPE4 = 10 ;
public static final int ID_DROP_PRO4 = 11 ;
public static final int ID_DROP_VAL4 = 12 ;
public static final int ID_DROP_TYPE5 = 13 ;
public static final int ID_DROP_PRO5 = 14 ;
public static final int ID_DROP_VAL5 = 15 ;
public static final int ID_DROP_TYPE6 = 16 ;
public static final int ID_DROP_PRO6 = 17 ;
public static final int ID_DROP_VAL6 = 18 ;
public static final int ID_DROP_TYPE7 = 19 ;
public static final int ID_DROP_PRO7 = 20 ;
public static final int ID_DROP_VAL7 = 21 ;
public static final int ID_DROP_TYPE8 = 22 ;
public static final int ID_DROP_PRO8 = 23 ;
public static final int ID_DROP_VAL8 = 24 ;
public static final int MAX_RECORD = 25 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_DropPro[] = new int[8];
 public int GetDropProbyIndex(int idx) {
 if(idx>=0 && idx<8) return m_DropPro[idx];
 return -1;
 }

private int m_DropType[] = new int[8];
 public int GetDropTypebyIndex(int idx) {
 if(idx>=0 && idx<8) return m_DropType[idx];
 return -1;
 }

private int m_DropVal[] = new int[8];
 public int GetDropValbyIndex(int idx) {
 if(idx>=0 && idx<8) return m_DropVal[idx];
 return -1;
 }

private int m_Probability;
 public int GetProbability() { return m_Probability;}

public boolean LoadTable(HashMap<Integer, Table_Drop> _tab) throws TableException
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
 HashMap<Integer, Table_Drop> _hash = (HashMap<Integer, Table_Drop> )obj;
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
 Table_Drop _values = new Table_Drop();
 _values.m_DropPro [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_PRO1));
_values.m_DropPro [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_PRO2));
_values.m_DropPro [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_PRO3));
_values.m_DropPro [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_PRO4));
_values.m_DropPro [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_PRO5));
_values.m_DropPro [ 5 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_PRO6));
_values.m_DropPro [ 6 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_PRO7));
_values.m_DropPro [ 7 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_PRO8));
_values.m_DropType [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_TYPE1));
_values.m_DropType [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_TYPE2));
_values.m_DropType [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_TYPE3));
_values.m_DropType [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_TYPE4));
_values.m_DropType [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_TYPE5));
_values.m_DropType [ 5 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_TYPE6));
_values.m_DropType [ 6 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_TYPE7));
_values.m_DropType [ 7 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_TYPE8));
_values.m_DropVal [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_VAL1));
_values.m_DropVal [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_VAL2));
_values.m_DropVal [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_VAL3));
_values.m_DropVal [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_VAL4));
_values.m_DropVal [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_VAL5));
_values.m_DropVal [ 5 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_VAL6));
_values.m_DropVal [ 6 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_VAL7));
_values.m_DropVal [ 7 ] =  Integer.parseInt((String)valuesList.get(ID_DROP_VAL8));
_values.m_Probability =  Integer.parseInt((String)valuesList.get(ID_PROBABILITY));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

