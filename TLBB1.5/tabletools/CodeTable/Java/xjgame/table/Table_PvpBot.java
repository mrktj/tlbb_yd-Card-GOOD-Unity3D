//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_PvpBot implements ITableOperate {
 public final static String TAB_FILE_DATA = "/pvp_bot.txt";
 public static final int ID_STAR = 0 ;
public static final int ID_MIN_LEVEL = 1 ;
public static final int ID_MAX_LEVEL = 2 ;
public static final int ID_QUANTITY_BOT = 3 ;
public static final int ID_MIN_QUANTITY_CARD = 4 ;
public static final int ID_MIN_QUANTITY_MAX = 5 ;
public static final int MAX_RECORD = 6 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_MaxLevel;
 public int GetMaxLevel() { return m_MaxLevel;}

private int m_MinLevel;
 public int GetMinLevel() { return m_MinLevel;}

private int m_MinQuantityCard;
 public int GetMinQuantityCard() { return m_MinQuantityCard;}

private int m_MinQuantityMax;
 public int GetMinQuantityMax() { return m_MinQuantityMax;}

private int m_QuantityBot;
 public int GetQuantityBot() { return m_QuantityBot;}

private int m_Star;
 public int GetStar() { return m_Star;}

public boolean LoadTable(HashMap<Integer, Table_PvpBot> _tab) throws TableException
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
 HashMap<Integer, Table_PvpBot> _hash = (HashMap<Integer, Table_PvpBot> )obj;
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
 Table_PvpBot _values = new Table_PvpBot();
 _values.m_MaxLevel =  Integer.parseInt((String)valuesList.get(ID_MAX_LEVEL));
_values.m_MinLevel =  Integer.parseInt((String)valuesList.get(ID_MIN_LEVEL));
_values.m_MinQuantityCard =  Integer.parseInt((String)valuesList.get(ID_MIN_QUANTITY_CARD));
_values.m_MinQuantityMax =  Integer.parseInt((String)valuesList.get(ID_MIN_QUANTITY_MAX));
_values.m_QuantityBot =  Integer.parseInt((String)valuesList.get(ID_QUANTITY_BOT));
_values.m_Star =  Integer.parseInt((String)valuesList.get(ID_STAR));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

