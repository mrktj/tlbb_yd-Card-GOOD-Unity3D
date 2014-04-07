//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Servers implements ITableOperate {
 public final static String TAB_FILE_DATA = "/servers.txt";
 public static final int ID_NAME = 0 ;
public static final int ID_DESCRIBE = 1 ;
public static final int ID_STATE = 2 ;
public static final int MAX_RECORD = 3 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_Describe;
 public String GetDescribe() { return m_Describe;}

private String m_Name;
 public String GetName() { return m_Name;}

private int m_State;
 public int GetState() { return m_State;}

public boolean LoadTable(HashMap<Integer, Table_Servers> _tab) throws TableException
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
 HashMap<Integer, Table_Servers> _hash = (HashMap<Integer, Table_Servers> )obj;
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
 Table_Servers _values = new Table_Servers();
 _values.m_Describe =  (String)valuesList.get(ID_DESCRIBE);
_values.m_Name =  (String)valuesList.get(ID_NAME);
_values.m_State =  Integer.parseInt((String)valuesList.get(ID_STATE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

