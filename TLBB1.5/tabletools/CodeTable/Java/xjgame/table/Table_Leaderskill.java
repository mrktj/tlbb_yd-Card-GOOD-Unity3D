//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Leaderskill implements ITableOperate {
 public final static String TAB_FILE_DATA = "/leaderskill.txt";
 public static final int ID_LOGIC_ID = 0 ;
public static final int ID_ADD_PER = 1 ;
public static final int ID_ADD_VALUE = 2 ;
public static final int ID_NAME = 3 ;
public static final int ID_NOTE = 4 ;
public static final int MAX_RECORD = 5 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_AddPer;
 public int GetAddPer() { return m_AddPer;}

private int m_AddValue;
 public int GetAddValue() { return m_AddValue;}

private int m_LogicId;
 public int GetLogicId() { return m_LogicId;}

private int m_Name;
 public int GetName() { return m_Name;}

private int m_Note;
 public int GetNote() { return m_Note;}

public boolean LoadTable(HashMap<Integer, Table_Leaderskill> _tab) throws TableException
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
 HashMap<Integer, Table_Leaderskill> _hash = (HashMap<Integer, Table_Leaderskill> )obj;
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
 Table_Leaderskill _values = new Table_Leaderskill();
 _values.m_AddPer =  Integer.parseInt((String)valuesList.get(ID_ADD_PER));
_values.m_AddValue =  Integer.parseInt((String)valuesList.get(ID_ADD_VALUE));
_values.m_LogicId =  Integer.parseInt((String)valuesList.get(ID_LOGIC_ID));
_values.m_Name =  Integer.parseInt((String)valuesList.get(ID_NAME));
_values.m_Note =  Integer.parseInt((String)valuesList.get(ID_NOTE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

