//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Popup implements ITableOperate {
 public final static String TAB_FILE_DATA = "/popup.txt";
 public static final int ID_TITLE = 0 ;
public static final int ID_CONTENT = 1 ;
public static final int ID_TYPE = 2 ;
public static final int MAX_RECORD = 3 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_Content;
 public String GetContent() { return m_Content;}

private String m_Title;
 public String GetTitle() { return m_Title;}

private String m_Type;
 public String GetType() { return m_Type;}

public boolean LoadTable(HashMap<Integer, Table_Popup> _tab) throws TableException
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
 HashMap<Integer, Table_Popup> _hash = (HashMap<Integer, Table_Popup> )obj;
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
 Table_Popup _values = new Table_Popup();
 _values.m_Content =  (String)valuesList.get(ID_CONTENT);
_values.m_Title =  (String)valuesList.get(ID_TITLE);
_values.m_Type =  (String)valuesList.get(ID_TYPE);

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

