//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Worldbossaward implements ITableOperate {
 public final static String TAB_FILE_DATA = "/worldbossaward.txt";
 public static final int ID_TYPE = 0 ;
public static final int ID_PAR = 1 ;
public static final int ID_AWARD_GOLD = 2 ;
public static final int ID_AWARD_YUANBAO = 3 ;
public static final int ID_AWARD_LEADER = 4 ;
public static final int MAX_RECORD = 5 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_AwardGold;
 public int GetAwardGold() { return m_AwardGold;}

private int m_AwardLeader;
 public int GetAwardLeader() { return m_AwardLeader;}

private int m_AwardYuanbao;
 public int GetAwardYuanbao() { return m_AwardYuanbao;}

private int m_Par;
 public int GetPar() { return m_Par;}

private int m_Type;
 public int GetType() { return m_Type;}

public boolean LoadTable(HashMap<Integer, Table_Worldbossaward> _tab) throws TableException
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
 HashMap<Integer, Table_Worldbossaward> _hash = (HashMap<Integer, Table_Worldbossaward> )obj;
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
 Table_Worldbossaward _values = new Table_Worldbossaward();
 _values.m_AwardGold =  Integer.parseInt((String)valuesList.get(ID_AWARD_GOLD));
_values.m_AwardLeader =  Integer.parseInt((String)valuesList.get(ID_AWARD_LEADER));
_values.m_AwardYuanbao =  Integer.parseInt((String)valuesList.get(ID_AWARD_YUANBAO));
_values.m_Par =  Integer.parseInt((String)valuesList.get(ID_PAR));
_values.m_Type =  Integer.parseInt((String)valuesList.get(ID_TYPE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

