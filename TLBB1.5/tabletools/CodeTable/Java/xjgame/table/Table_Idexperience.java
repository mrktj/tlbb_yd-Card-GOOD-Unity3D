//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Idexperience implements ITableOperate {
 public final static String TAB_FILE_DATA = "/idexperience.txt";
 public static final int ID_ID_EXPERIENCE = 0 ;
public static final int ID_ID_PHYSICALVALUE = 1 ;
public static final int ID_ID_COST = 2 ;
public static final int ID_BAG_LIMIT = 3 ;
public static final int ID_FRIEND_LIMIT = 4 ;
public static final int MAX_RECORD = 5 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_BagLimit;
 public int GetBagLimit() { return m_BagLimit;}

private int m_FriendLimit;
 public int GetFriendLimit() { return m_FriendLimit;}

private int m_IDCost;
 public int GetIDCost() { return m_IDCost;}

private int m_IDExperience;
 public int GetIDExperience() { return m_IDExperience;}

private int m_IDPhysicalValue;
 public int GetIDPhysicalValue() { return m_IDPhysicalValue;}

public boolean LoadTable(HashMap<Integer, Table_Idexperience> _tab) throws TableException
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
 HashMap<Integer, Table_Idexperience> _hash = (HashMap<Integer, Table_Idexperience> )obj;
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
 Table_Idexperience _values = new Table_Idexperience();
 _values.m_BagLimit =  Integer.parseInt((String)valuesList.get(ID_BAG_LIMIT));
_values.m_FriendLimit =  Integer.parseInt((String)valuesList.get(ID_FRIEND_LIMIT));
_values.m_IDCost =  Integer.parseInt((String)valuesList.get(ID_ID_COST));
_values.m_IDExperience =  Integer.parseInt((String)valuesList.get(ID_ID_EXPERIENCE));
_values.m_IDPhysicalValue =  Integer.parseInt((String)valuesList.get(ID_ID_PHYSICALVALUE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

