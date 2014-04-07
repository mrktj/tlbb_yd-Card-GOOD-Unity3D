//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Bagua implements ITableOperate {
 public final static String TAB_FILE_DATA = "/bagua.txt";
 public static final int ID_TYPE = 0 ;
public static final int ID_NUMBER = 1 ;
public static final int ID_TEXT = 2 ;
public static final int MAX_RECORD = 3 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Number;
 public int GetNumber() { return m_Number;}

private String m_Text;
 public String GetText() { return m_Text;}

private int m_Type;
 public int GetType() { return m_Type;}

public boolean LoadTable(HashMap<Integer, Table_Bagua> _tab) throws TableException
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
 HashMap<Integer, Table_Bagua> _hash = (HashMap<Integer, Table_Bagua> )obj;
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
 Table_Bagua _values = new Table_Bagua();
 _values.m_Number =  Integer.parseInt((String)valuesList.get(ID_NUMBER));
_values.m_Text =  (String)valuesList.get(ID_TEXT);
_values.m_Type =  Integer.parseInt((String)valuesList.get(ID_TYPE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

