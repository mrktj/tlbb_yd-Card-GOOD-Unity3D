//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_GoldPrice implements ITableOperate {
 public final static String TAB_FILE_DATA = "/GoldPrice.txt";
 public static final int ID_POWER_PRICE = 0 ;
public static final int ID_GOLD_VALUE = 1 ;
public static final int MAX_RECORD = 2 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_GoldValue;
 public int GetGoldValue() { return m_GoldValue;}

private int m_PowerPrice;
 public int GetPowerPrice() { return m_PowerPrice;}

public boolean LoadTable(HashMap<Integer, Table_GoldPrice> _tab) throws TableException
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
 HashMap<Integer, Table_GoldPrice> _hash = (HashMap<Integer, Table_GoldPrice> )obj;
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
 Table_GoldPrice _values = new Table_GoldPrice();
 _values.m_GoldValue =  Integer.parseInt((String)valuesList.get(ID_GOLD_VALUE));
_values.m_PowerPrice =  Integer.parseInt((String)valuesList.get(ID_POWER_PRICE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

