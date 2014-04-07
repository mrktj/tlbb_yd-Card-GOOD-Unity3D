//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_BuffEffect implements ITableOperate {
 public final static String TAB_FILE_DATA = "/buff_effect.txt";
 public static final int ID_BUFF_TYPE = 0 ;
public static final int ID_EFFECT_TYPE = 1 ;
public static final int ID_EFFECT_POS = 2 ;
public static final int ID_BUFF_ANIM = 3 ;
public static final int ID_BUFF_LIB = 4 ;
public static final int MAX_RECORD = 5 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_BuffAnim;
 public String GetBuffAnim() { return m_BuffAnim;}

private String m_BuffLib;
 public String GetBuffLib() { return m_BuffLib;}

private int m_BuffType;
 public int GetBuffType() { return m_BuffType;}

private int m_EffectPos;
 public int GetEffectPos() { return m_EffectPos;}

private int m_EffectType;
 public int GetEffectType() { return m_EffectType;}

public boolean LoadTable(HashMap<Integer, Table_BuffEffect> _tab) throws TableException
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
 HashMap<Integer, Table_BuffEffect> _hash = (HashMap<Integer, Table_BuffEffect> )obj;
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
 Table_BuffEffect _values = new Table_BuffEffect();
 _values.m_BuffAnim =  (String)valuesList.get(ID_BUFF_ANIM);
_values.m_BuffLib =  (String)valuesList.get(ID_BUFF_LIB);
_values.m_BuffType =  Integer.parseInt((String)valuesList.get(ID_BUFF_TYPE));
_values.m_EffectPos =  Integer.parseInt((String)valuesList.get(ID_EFFECT_POS));
_values.m_EffectType =  Integer.parseInt((String)valuesList.get(ID_EFFECT_TYPE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

