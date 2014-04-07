//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_ShopNotice implements ITableOperate {
 public final static String TAB_FILE_DATA = "/shop_notice.txt";
 public static final int ID_PIC_TYPE = 0 ;
public static final int ID_PIC_SPRITE = 1 ;
public static final int MAX_RECORD = 2 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_PicSprite;
 public String GetPicSprite() { return m_PicSprite;}

private String m_PicType;
 public String GetPicType() { return m_PicType;}

public boolean LoadTable(HashMap<Integer, Table_ShopNotice> _tab) throws TableException
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
 HashMap<Integer, Table_ShopNotice> _hash = (HashMap<Integer, Table_ShopNotice> )obj;
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
 Table_ShopNotice _values = new Table_ShopNotice();
 _values.m_PicSprite =  (String)valuesList.get(ID_PIC_SPRITE);
_values.m_PicType =  (String)valuesList.get(ID_PIC_TYPE);

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

