//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Iap implements ITableOperate {
 public final static String TAB_FILE_DATA = "/iap.txt";
 public static final int ID_GOODSID = 0 ;
public static final int ID_GOODSTYPE = 1 ;
public static final int ID_DESCRIBE = 2 ;
public static final int MAX_RECORD = 3 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_Describe;
 public String GetDescribe() { return m_Describe;}

private int m_GoodsID;
 public int GetGoodsID() { return m_GoodsID;}

private int m_Goodstype;
 public int GetGoodstype() { return m_Goodstype;}

public boolean LoadTable(HashMap<Integer, Table_Iap> _tab) throws TableException
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
 HashMap<Integer, Table_Iap> _hash = (HashMap<Integer, Table_Iap> )obj;
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
 Table_Iap _values = new Table_Iap();
 _values.m_Describe =  (String)valuesList.get(ID_DESCRIBE);
_values.m_GoodsID =  Integer.parseInt((String)valuesList.get(ID_GOODSID));
_values.m_Goodstype =  Integer.parseInt((String)valuesList.get(ID_GOODSTYPE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

