//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_PvpShop implements ITableOperate {
 public final static String TAB_FILE_DATA = "/pvp_shop.txt";
 public static final int ID_TITLE = 0 ;
public static final int ID_DESCRIPTION = 1 ;
public static final int ID_SCORE = 2 ;
public static final int ID_TYPE = 3 ;
public static final int ID_NUM = 4 ;
public static final int MAX_RECORD = 5 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_Description;
 public String GetDescription() { return m_Description;}

private int m_Num;
 public int GetNum() { return m_Num;}

private int m_Score;
 public int GetScore() { return m_Score;}

private String m_Title;
 public String GetTitle() { return m_Title;}

private int m_Type;
 public int GetType() { return m_Type;}

public boolean LoadTable(HashMap<Integer, Table_PvpShop> _tab) throws TableException
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
 HashMap<Integer, Table_PvpShop> _hash = (HashMap<Integer, Table_PvpShop> )obj;
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
 Table_PvpShop _values = new Table_PvpShop();
 _values.m_Description =  (String)valuesList.get(ID_DESCRIPTION);
_values.m_Num =  Integer.parseInt((String)valuesList.get(ID_NUM));
_values.m_Score =  Integer.parseInt((String)valuesList.get(ID_SCORE));
_values.m_Title =  (String)valuesList.get(ID_TITLE);
_values.m_Type =  Integer.parseInt((String)valuesList.get(ID_TYPE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

