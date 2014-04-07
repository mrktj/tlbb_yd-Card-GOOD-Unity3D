//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Shopping implements ITableOperate {
 public final static String TAB_FILE_DATA = "/shopping.txt";
 public static final int ID_NOTE = 0 ;
public static final int ID_COST = 1 ;
public static final int ID_INCREASENUM = 2 ;
public static final int ID_LIMIT = 3 ;
public static final int MAX_RECORD = 4 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Cost;
 public int GetCost() { return m_Cost;}

private int m_IncreaseNum;
 public int GetIncreaseNum() { return m_IncreaseNum;}

private int m_Limit;
 public int GetLimit() { return m_Limit;}

private int m_Note;
 public int GetNote() { return m_Note;}

public boolean LoadTable(HashMap<Integer, Table_Shopping> _tab) throws TableException
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
 HashMap<Integer, Table_Shopping> _hash = (HashMap<Integer, Table_Shopping> )obj;
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
 Table_Shopping _values = new Table_Shopping();
 _values.m_Cost =  Integer.parseInt((String)valuesList.get(ID_COST));
_values.m_IncreaseNum =  Integer.parseInt((String)valuesList.get(ID_INCREASENUM));
_values.m_Limit =  Integer.parseInt((String)valuesList.get(ID_LIMIT));
_values.m_Note =  Integer.parseInt((String)valuesList.get(ID_NOTE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

