//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Btninfo implements ITableOperate {
 public final static String TAB_FILE_DATA = "/btninfo.txt";
 public static final int ID_BTNNAME = 0 ;
public static final int MAX_RECORD = 1 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_Btnname;
 public String GetBtnname() { return m_Btnname;}

public boolean LoadTable(HashMap<Integer, Table_Btninfo> _tab) throws TableException
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
 HashMap<Integer, Table_Btninfo> _hash = (HashMap<Integer, Table_Btninfo> )obj;
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
 Table_Btninfo _values = new Table_Btninfo();
 _values.m_Btnname =  (String)valuesList.get(ID_BTNNAME);

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

