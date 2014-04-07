//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Worldbosspar implements ITableOperate {
 public final static String TAB_FILE_DATA = "/worldbosspar.txt";
 public static final int ID_HP_PAR = 0 ;
public static final int ID_ATT_PAR = 1 ;
public static final int ID_GOLD_PAR = 2 ;
public static final int MAX_RECORD = 3 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_AttPar;
 public int GetAttPar() { return m_AttPar;}

private int m_GoldPar;
 public int GetGoldPar() { return m_GoldPar;}

private int m_HpPar;
 public int GetHpPar() { return m_HpPar;}

public boolean LoadTable(HashMap<Integer, Table_Worldbosspar> _tab) throws TableException
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
 HashMap<Integer, Table_Worldbosspar> _hash = (HashMap<Integer, Table_Worldbosspar> )obj;
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
 Table_Worldbosspar _values = new Table_Worldbosspar();
 _values.m_AttPar =  Integer.parseInt((String)valuesList.get(ID_ATT_PAR));
_values.m_GoldPar =  Integer.parseInt((String)valuesList.get(ID_GOLD_PAR));
_values.m_HpPar =  Integer.parseInt((String)valuesList.get(ID_HP_PAR));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

