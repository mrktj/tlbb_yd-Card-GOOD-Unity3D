//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Buff implements ITableOperate {
 public final static String TAB_FILE_DATA = "/buff.txt";
 public static final int ID_EFFECTID = 0 ;
public static final int ID_CD_ROUND = 1 ;
public static final int ID_LOGIC_ID = 2 ;
public static final int ID_ORDER = 3 ;
public static final int ID_PERCENT = 4 ;
public static final int ID_DATA = 5 ;
public static final int ID_CAMP = 6 ;
public static final int MAX_RECORD = 7 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Camp;
 public int GetCamp() { return m_Camp;}

private int m_CdRound;
 public int GetCdRound() { return m_CdRound;}

private int m_Data;
 public int GetData() { return m_Data;}

private int m_EffectID;
 public int GetEffectID() { return m_EffectID;}

private int m_LogicId;
 public int GetLogicId() { return m_LogicId;}

private int m_Order;
 public int GetOrder() { return m_Order;}

private int m_Percent;
 public int GetPercent() { return m_Percent;}

public boolean LoadTable(HashMap<Integer, Table_Buff> _tab) throws TableException
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
 HashMap<Integer, Table_Buff> _hash = (HashMap<Integer, Table_Buff> )obj;
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
 Table_Buff _values = new Table_Buff();
 _values.m_Camp =  Integer.parseInt((String)valuesList.get(ID_CAMP));
_values.m_CdRound =  Integer.parseInt((String)valuesList.get(ID_CD_ROUND));
_values.m_Data =  Integer.parseInt((String)valuesList.get(ID_DATA));
_values.m_EffectID =  Integer.parseInt((String)valuesList.get(ID_EFFECTID));
_values.m_LogicId =  Integer.parseInt((String)valuesList.get(ID_LOGIC_ID));
_values.m_Order =  Integer.parseInt((String)valuesList.get(ID_ORDER));
_values.m_Percent =  Integer.parseInt((String)valuesList.get(ID_PERCENT));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

