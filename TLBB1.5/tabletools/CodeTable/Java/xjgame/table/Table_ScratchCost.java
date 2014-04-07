//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_ScratchCost implements ITableOperate {
 public final static String TAB_FILE_DATA = "/scratch_cost.txt";
 public static final int ID_COST = 0 ;
public static final int MAX_RECORD = 1 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Cost;
 public int GetCost() { return m_Cost;}

public boolean LoadTable(HashMap<Integer, Table_ScratchCost> _tab) throws TableException
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
 HashMap<Integer, Table_ScratchCost> _hash = (HashMap<Integer, Table_ScratchCost> )obj;
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
 Table_ScratchCost _values = new Table_ScratchCost();
 _values.m_Cost =  Integer.parseInt((String)valuesList.get(ID_COST));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

