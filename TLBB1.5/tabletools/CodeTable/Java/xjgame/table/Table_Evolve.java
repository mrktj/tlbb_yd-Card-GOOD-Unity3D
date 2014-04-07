//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Evolve implements ITableOperate {
 public final static String TAB_FILE_DATA = "/evolve.txt";
 public static final int ID_CARDID1 = 0 ;
public static final int ID_CARDID2 = 1 ;
public static final int ID_CARDID3 = 2 ;
public static final int ID_CARDID4 = 3 ;
public static final int ID_CARDID5 = 4 ;
public static final int MAX_RECORD = 5 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_CardID[] = new int[5];
 public int GetCardIDbyIndex(int idx) {
 if(idx>=0 && idx<5) return m_CardID[idx];
 return -1;
 }

public boolean LoadTable(HashMap<Integer, Table_Evolve> _tab) throws TableException
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
 HashMap<Integer, Table_Evolve> _hash = (HashMap<Integer, Table_Evolve> )obj;
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
 Table_Evolve _values = new Table_Evolve();
 _values.m_CardID [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_CARDID1));
_values.m_CardID [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_CARDID2));
_values.m_CardID [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_CARDID3));
_values.m_CardID [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_CARDID4));
_values.m_CardID [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_CARDID5));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

