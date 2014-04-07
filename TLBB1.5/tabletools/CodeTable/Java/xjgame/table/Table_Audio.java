//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Audio implements ITableOperate {
 public final static String TAB_FILE_DATA = "/audio.txt";
 public static final int ID_NAME = 0 ;
public static final int ID_SUFFIX = 1 ;
public static final int ID_PLAYTIMES = 2 ;
public static final int ID_DELAYTIME = 3 ;
public static final int ID_TYPE = 4 ;
public static final int MAX_RECORD = 5 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private float m_Delaytime;
 public float GetDelaytime() { return m_Delaytime;}

private String m_Name;
 public String GetName() { return m_Name;}

private int m_Playtimes;
 public int GetPlaytimes() { return m_Playtimes;}

private String m_Suffix;
 public String GetSuffix() { return m_Suffix;}

private int m_Type;
 public int GetType() { return m_Type;}

public boolean LoadTable(HashMap<Integer, Table_Audio> _tab) throws TableException
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
 HashMap<Integer, Table_Audio> _hash = (HashMap<Integer, Table_Audio> )obj;
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
 Table_Audio _values = new Table_Audio();
 _values.m_Delaytime =  Float.parseFloat((String)valuesList.get(ID_DELAYTIME));
_values.m_Name =  (String)valuesList.get(ID_NAME);
_values.m_Playtimes =  Integer.parseInt((String)valuesList.get(ID_PLAYTIMES));
_values.m_Suffix =  (String)valuesList.get(ID_SUFFIX);
_values.m_Type =  Integer.parseInt((String)valuesList.get(ID_TYPE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

