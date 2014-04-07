//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_ChongLou implements ITableOperate {
 public final static String TAB_FILE_DATA = "/ChongLou.txt";
 public static final int ID_FIGHT1_0 = 0 ;
public static final int ID_FIGHT1_1 = 1 ;
public static final int ID_FIGHT1_2 = 2 ;
public static final int ID_FIGHT1_3 = 3 ;
public static final int ID_FIGHT1_4 = 4 ;
public static final int ID_FIGHT1_5 = 5 ;
public static final int ID_COPY_DROP_1 = 6 ;
public static final int ID_COPY_NUM_1 = 7 ;
public static final int ID_COPY_DROP_2 = 8 ;
public static final int ID_COPY_NUM_2 = 9 ;
public static final int MAX_RECORD = 10 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_CopyDrop[] = new int[2];
 public int GetCopyDropbyIndex(int idx) {
 if(idx>=0 && idx<2) return m_CopyDrop[idx];
 return -1;
 }

private int m_CopyNum[] = new int[2];
 public int GetCopyNumbyIndex(int idx) {
 if(idx>=0 && idx<2) return m_CopyNum[idx];
 return -1;
 }

private int m_Fight1[] = new int[6];
 public int GetFight1byIndex(int idx) {
 if(idx>=0 && idx<6) return m_Fight1[idx];
 return -1;
 }

public boolean LoadTable(HashMap<Integer, Table_ChongLou> _tab) throws TableException
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
 HashMap<Integer, Table_ChongLou> _hash = (HashMap<Integer, Table_ChongLou> )obj;
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
 Table_ChongLou _values = new Table_ChongLou();
 _values.m_CopyDrop [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_COPY_DROP_1));
_values.m_CopyDrop [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_COPY_DROP_2));
_values.m_CopyNum [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_COPY_NUM_1));
_values.m_CopyNum [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_COPY_NUM_2));
_values.m_Fight1 [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT1_0));
_values.m_Fight1 [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT1_1));
_values.m_Fight1 [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT1_2));
_values.m_Fight1 [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT1_3));
_values.m_Fight1 [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT1_4));
_values.m_Fight1 [ 5 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT1_5));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

