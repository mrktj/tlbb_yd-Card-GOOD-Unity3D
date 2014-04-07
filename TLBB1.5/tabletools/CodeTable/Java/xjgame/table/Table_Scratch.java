//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Scratch implements ITableOperate {
 public final static String TAB_FILE_DATA = "/scratch.txt";
 public static final int ID_PRIZE_TYPE = 0 ;
public static final int ID_VALUE = 1 ;
public static final int ID_WEIGHT = 2 ;
public static final int ID_PRIZE = 3 ;
public static final int MAX_RECORD = 4 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Prize;
 public int GetPrize() { return m_Prize;}

private int m_PrizeType;
 public int GetPrizeType() { return m_PrizeType;}

private int m_Value;
 public int GetValue() { return m_Value;}

private int m_Weight;
 public int GetWeight() { return m_Weight;}

public boolean LoadTable(HashMap<Integer, Table_Scratch> _tab) throws TableException
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
 HashMap<Integer, Table_Scratch> _hash = (HashMap<Integer, Table_Scratch> )obj;
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
 Table_Scratch _values = new Table_Scratch();
 _values.m_Prize =  Integer.parseInt((String)valuesList.get(ID_PRIZE));
_values.m_PrizeType =  Integer.parseInt((String)valuesList.get(ID_PRIZE_TYPE));
_values.m_Value =  Integer.parseInt((String)valuesList.get(ID_VALUE));
_values.m_Weight =  Integer.parseInt((String)valuesList.get(ID_WEIGHT));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

