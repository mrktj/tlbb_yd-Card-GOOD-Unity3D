//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Rollingnotice implements ITableOperate {
 public final static String TAB_FILE_DATA = "/rollingnotice.txt";
 public static final int ID_CONTENT = 0 ;
public static final int ID_TYPE = 1 ;
public static final int MAX_RECORD = 2 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_Content;
 public String GetContent() { return m_Content;}

private int m_Type;
 public int GetType() { return m_Type;}

public boolean LoadTable(HashMap<Integer, Table_Rollingnotice> _tab) throws TableException
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
 HashMap<Integer, Table_Rollingnotice> _hash = (HashMap<Integer, Table_Rollingnotice> )obj;
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
 Table_Rollingnotice _values = new Table_Rollingnotice();
 _values.m_Content =  (String)valuesList.get(ID_CONTENT);
_values.m_Type =  Integer.parseInt((String)valuesList.get(ID_TYPE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

