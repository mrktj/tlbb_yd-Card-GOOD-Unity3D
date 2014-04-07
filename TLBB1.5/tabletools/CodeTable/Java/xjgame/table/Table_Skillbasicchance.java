//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Skillbasicchance implements ITableOperate {
 public final static String TAB_FILE_DATA = "/skillbasicchance.txt";
 public static final int ID_BASIC_CHANCE1 = 0 ;
public static final int ID_BASIC_CHANCE2 = 1 ;
public static final int ID_BASIC_CHANCE3 = 2 ;
public static final int ID_BASIC_CHANCE4 = 3 ;
public static final int ID_BASIC_CHANCE5 = 4 ;
public static final int ID_BASIC_CHANCE6 = 5 ;
public static final int MAX_RECORD = 6 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_BasicChance[] = new int[6];
 public int GetBasicChancebyIndex(int idx) {
 if(idx>=0 && idx<6) return m_BasicChance[idx];
 return -1;
 }

public boolean LoadTable(HashMap<Integer, Table_Skillbasicchance> _tab) throws TableException
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
 HashMap<Integer, Table_Skillbasicchance> _hash = (HashMap<Integer, Table_Skillbasicchance> )obj;
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
 Table_Skillbasicchance _values = new Table_Skillbasicchance();
 _values.m_BasicChance [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_BASIC_CHANCE1));
_values.m_BasicChance [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_BASIC_CHANCE2));
_values.m_BasicChance [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_BASIC_CHANCE3));
_values.m_BasicChance [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_BASIC_CHANCE4));
_values.m_BasicChance [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_BASIC_CHANCE5));
_values.m_BasicChance [ 5 ] =  Integer.parseInt((String)valuesList.get(ID_BASIC_CHANCE6));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

