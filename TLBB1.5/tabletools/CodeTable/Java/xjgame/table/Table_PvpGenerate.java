//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_PvpGenerate implements ITableOperate {
 public final static String TAB_FILE_DATA = "/pvp_generate.txt";
 public static final int ID_RANK_MIN = 0 ;
public static final int ID_RANK_MAX = 1 ;
public static final int ID_X_MIN = 2 ;
public static final int ID_X_MAX = 3 ;
public static final int ID_Y_MIN = 4 ;
public static final int ID_Y_MAX = 5 ;
public static final int MAX_RECORD = 6 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_RankMax;
 public int GetRankMax() { return m_RankMax;}

private int m_RankMin;
 public int GetRankMin() { return m_RankMin;}

private int m_XMax;
 public int GetXMax() { return m_XMax;}

private int m_XMin;
 public int GetXMin() { return m_XMin;}

private int m_YMax;
 public int GetYMax() { return m_YMax;}

private int m_YMin;
 public int GetYMin() { return m_YMin;}

public boolean LoadTable(HashMap<Integer, Table_PvpGenerate> _tab) throws TableException
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
 HashMap<Integer, Table_PvpGenerate> _hash = (HashMap<Integer, Table_PvpGenerate> )obj;
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
 Table_PvpGenerate _values = new Table_PvpGenerate();
 _values.m_RankMax =  Integer.parseInt((String)valuesList.get(ID_RANK_MAX));
_values.m_RankMin =  Integer.parseInt((String)valuesList.get(ID_RANK_MIN));
_values.m_XMax =  Integer.parseInt((String)valuesList.get(ID_X_MAX));
_values.m_XMin =  Integer.parseInt((String)valuesList.get(ID_X_MIN));
_values.m_YMax =  Integer.parseInt((String)valuesList.get(ID_Y_MAX));
_values.m_YMin =  Integer.parseInt((String)valuesList.get(ID_Y_MIN));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

