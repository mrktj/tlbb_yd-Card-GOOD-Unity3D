//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_ScratchPrize implements ITableOperate {
 public final static String TAB_FILE_DATA = "/scratch_prize.txt";
 public static final int ID_HEAD_ICON = 0 ;
public static final int ID_WEIGHT = 1 ;
public static final int MAX_RECORD = 2 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_HeadIcon;
 public String GetHeadIcon() { return m_HeadIcon;}

private int m_Weight;
 public int GetWeight() { return m_Weight;}

public boolean LoadTable(HashMap<Integer, Table_ScratchPrize> _tab) throws TableException
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
 HashMap<Integer, Table_ScratchPrize> _hash = (HashMap<Integer, Table_ScratchPrize> )obj;
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
 Table_ScratchPrize _values = new Table_ScratchPrize();
 _values.m_HeadIcon =  (String)valuesList.get(ID_HEAD_ICON);
_values.m_Weight =  Integer.parseInt((String)valuesList.get(ID_WEIGHT));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

