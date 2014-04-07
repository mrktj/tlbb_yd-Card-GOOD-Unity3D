//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_GambleCost implements ITableOperate {
 public final static String TAB_FILE_DATA = "/gamble_cost.txt";
 public static final int ID_ONE_COST = 0 ;
public static final int ID_TEN_COST = 1 ;
public static final int MAX_RECORD = 2 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_OneCost;
 public int GetOneCost() { return m_OneCost;}

private int m_TenCost;
 public int GetTenCost() { return m_TenCost;}

public boolean LoadTable(HashMap<Integer, Table_GambleCost> _tab) throws TableException
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
 HashMap<Integer, Table_GambleCost> _hash = (HashMap<Integer, Table_GambleCost> )obj;
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
 Table_GambleCost _values = new Table_GambleCost();
 _values.m_OneCost =  Integer.parseInt((String)valuesList.get(ID_ONE_COST));
_values.m_TenCost =  Integer.parseInt((String)valuesList.get(ID_TEN_COST));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

