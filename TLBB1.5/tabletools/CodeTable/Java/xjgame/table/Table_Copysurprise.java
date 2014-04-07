//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Copysurprise implements ITableOperate {
 public final static String TAB_FILE_DATA = "/copysurprise.txt";
 public static final int ID_NPCID = 0 ;
public static final int ID_NPC_WEIGHT = 1 ;
public static final int ID_RATE = 2 ;
public static final int MAX_RECORD = 3 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_NpcID;
 public String GetNpcID() { return m_NpcID;}

private String m_NpcWeight;
 public String GetNpcWeight() { return m_NpcWeight;}

private int m_Rate;
 public int GetRate() { return m_Rate;}

public boolean LoadTable(HashMap<Integer, Table_Copysurprise> _tab) throws TableException
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
 HashMap<Integer, Table_Copysurprise> _hash = (HashMap<Integer, Table_Copysurprise> )obj;
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
 Table_Copysurprise _values = new Table_Copysurprise();
 _values.m_NpcID =  (String)valuesList.get(ID_NPCID);
_values.m_NpcWeight =  (String)valuesList.get(ID_NPC_WEIGHT);
_values.m_Rate =  Integer.parseInt((String)valuesList.get(ID_RATE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

