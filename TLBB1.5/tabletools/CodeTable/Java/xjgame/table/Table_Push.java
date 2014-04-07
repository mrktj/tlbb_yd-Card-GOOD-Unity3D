//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Push implements ITableOperate {
 public final static String TAB_FILE_DATA = "/push.txt";
 public static final int ID_WEEK = 0 ;
public static final int ID_TIME = 1 ;
public static final int ID_TEXT = 2 ;
public static final int MAX_RECORD = 3 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Text;
 public int GetText() { return m_Text;}

private int m_Time;
 public int GetTime() { return m_Time;}

private int m_Week;
 public int GetWeek() { return m_Week;}

public boolean LoadTable(HashMap<Integer, Table_Push> _tab) throws TableException
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
 HashMap<Integer, Table_Push> _hash = (HashMap<Integer, Table_Push> )obj;
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
 Table_Push _values = new Table_Push();
 _values.m_Text =  Integer.parseInt((String)valuesList.get(ID_TEXT));
_values.m_Time =  Integer.parseInt((String)valuesList.get(ID_TIME));
_values.m_Week =  Integer.parseInt((String)valuesList.get(ID_WEEK));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

