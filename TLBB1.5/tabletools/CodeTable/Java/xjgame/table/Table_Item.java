//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Item implements ITableOperate {
 public final static String TAB_FILE_DATA = "/item.txt";
 public static final int ID_NAME = 0 ;
public static final int ID_INTRODUCE = 1 ;
public static final int ID_ICON = 2 ;
public static final int ID_TYPE = 3 ;
public static final int ID_VALUE = 4 ;
public static final int MAX_RECORD = 5 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_Icon;
 public String GetIcon() { return m_Icon;}

private int m_Introduce;
 public int GetIntroduce() { return m_Introduce;}

private int m_Name;
 public int GetName() { return m_Name;}

private int m_Type;
 public int GetType() { return m_Type;}

private int m_Value;
 public int GetValue() { return m_Value;}

public boolean LoadTable(HashMap<Integer, Table_Item> _tab) throws TableException
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
 HashMap<Integer, Table_Item> _hash = (HashMap<Integer, Table_Item> )obj;
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
 Table_Item _values = new Table_Item();
 _values.m_Icon =  (String)valuesList.get(ID_ICON);
_values.m_Introduce =  Integer.parseInt((String)valuesList.get(ID_INTRODUCE));
_values.m_Name =  Integer.parseInt((String)valuesList.get(ID_NAME));
_values.m_Type =  Integer.parseInt((String)valuesList.get(ID_TYPE));
_values.m_Value =  Integer.parseInt((String)valuesList.get(ID_VALUE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

