//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_StarGamble implements ITableOperate {
 public final static String TAB_FILE_DATA = "/star_gamble.txt";
 public static final int ID_STAR = 0 ;
public static final int ID_FRIEND_WEIGHT = 1 ;
public static final int ID_DOLLAR_WEIGHT = 2 ;
public static final int MAX_RECORD = 3 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_DollarWeight;
 public int GetDollarWeight() { return m_DollarWeight;}

private int m_FriendWeight;
 public int GetFriendWeight() { return m_FriendWeight;}

private int m_Star;
 public int GetStar() { return m_Star;}

public boolean LoadTable(HashMap<Integer, Table_StarGamble> _tab) throws TableException
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
 HashMap<Integer, Table_StarGamble> _hash = (HashMap<Integer, Table_StarGamble> )obj;
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
 Table_StarGamble _values = new Table_StarGamble();
 _values.m_DollarWeight =  Integer.parseInt((String)valuesList.get(ID_DOLLAR_WEIGHT));
_values.m_FriendWeight =  Integer.parseInt((String)valuesList.get(ID_FRIEND_WEIGHT));
_values.m_Star =  Integer.parseInt((String)valuesList.get(ID_STAR));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

