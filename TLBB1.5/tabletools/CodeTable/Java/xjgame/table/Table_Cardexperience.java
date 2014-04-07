//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Cardexperience implements ITableOperate {
 public final static String TAB_FILE_DATA = "/cardexperience.txt";
 public static final int ID_CARD_EXP = 0 ;
public static final int ID_TOTAL_EXP = 1 ;
public static final int MAX_RECORD = 2 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_CardExp;
 public int GetCardExp() { return m_CardExp;}

private int m_TotalExp;
 public int GetTotalExp() { return m_TotalExp;}

public boolean LoadTable(HashMap<Integer, Table_Cardexperience> _tab) throws TableException
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
 HashMap<Integer, Table_Cardexperience> _hash = (HashMap<Integer, Table_Cardexperience> )obj;
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
 Table_Cardexperience _values = new Table_Cardexperience();
 _values.m_CardExp =  Integer.parseInt((String)valuesList.get(ID_CARD_EXP));
_values.m_TotalExp =  Integer.parseInt((String)valuesList.get(ID_TOTAL_EXP));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

