//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_FriendGamble implements ITableOperate {
 public final static String TAB_FILE_DATA = "/friend_gamble.txt";
 public static final int ID_STAR = 0 ;
public static final int ID_CARDID = 1 ;
public static final int ID_PRO = 2 ;
public static final int MAX_RECORD = 3 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_CardID;
 public int GetCardID() { return m_CardID;}

private int m_Pro;
 public int GetPro() { return m_Pro;}

private int m_Star;
 public int GetStar() { return m_Star;}

public boolean LoadTable(HashMap<Integer, Table_FriendGamble> _tab) throws TableException
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
 HashMap<Integer, Table_FriendGamble> _hash = (HashMap<Integer, Table_FriendGamble> )obj;
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
 Table_FriendGamble _values = new Table_FriendGamble();
 _values.m_CardID =  Integer.parseInt((String)valuesList.get(ID_CARDID));
_values.m_Pro =  Integer.parseInt((String)valuesList.get(ID_PRO));
_values.m_Star =  Integer.parseInt((String)valuesList.get(ID_STAR));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

