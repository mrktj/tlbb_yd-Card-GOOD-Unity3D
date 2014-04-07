//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_PvpScore implements ITableOperate {
 public final static String TAB_FILE_DATA = "/pvp_score.txt";
 public static final int ID_RANK_MIN = 0 ;
public static final int ID_RANK_MAX = 1 ;
public static final int ID_SCORE = 2 ;
public static final int MAX_RECORD = 3 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_RankMax;
 public int GetRankMax() { return m_RankMax;}

private int m_RankMin;
 public int GetRankMin() { return m_RankMin;}

private int m_Score;
 public int GetScore() { return m_Score;}

public boolean LoadTable(HashMap<Integer, Table_PvpScore> _tab) throws TableException
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
 HashMap<Integer, Table_PvpScore> _hash = (HashMap<Integer, Table_PvpScore> )obj;
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
 Table_PvpScore _values = new Table_PvpScore();
 _values.m_RankMax =  Integer.parseInt((String)valuesList.get(ID_RANK_MAX));
_values.m_RankMin =  Integer.parseInt((String)valuesList.get(ID_RANK_MIN));
_values.m_Score =  Integer.parseInt((String)valuesList.get(ID_SCORE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

