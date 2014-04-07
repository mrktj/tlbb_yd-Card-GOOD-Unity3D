//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Effect implements ITableOperate {
 public final static String TAB_FILE_DATA = "/effect.txt";
 public static final int ID_NAME = 0 ;
public static final int ID_DESCRIBE = 1 ;
public static final int ID_ANIMATION = 2 ;
public static final int ID_MUSIC = 3 ;
public static final int ID_LOGICID = 4 ;
public static final int ID_ROUND = 5 ;
public static final int MAX_RECORD = 6 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_Animation;
 public String GetAnimation() { return m_Animation;}

private int m_Describe;
 public int GetDescribe() { return m_Describe;}

private int m_Logicid;
 public int GetLogicid() { return m_Logicid;}

private String m_Music;
 public String GetMusic() { return m_Music;}

private String m_Name;
 public String GetName() { return m_Name;}

private int m_Round;
 public int GetRound() { return m_Round;}

public boolean LoadTable(HashMap<Integer, Table_Effect> _tab) throws TableException
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
 HashMap<Integer, Table_Effect> _hash = (HashMap<Integer, Table_Effect> )obj;
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
 Table_Effect _values = new Table_Effect();
 _values.m_Animation =  (String)valuesList.get(ID_ANIMATION);
_values.m_Describe =  Integer.parseInt((String)valuesList.get(ID_DESCRIBE));
_values.m_Logicid =  Integer.parseInt((String)valuesList.get(ID_LOGICID));
_values.m_Music =  (String)valuesList.get(ID_MUSIC);
_values.m_Name =  (String)valuesList.get(ID_NAME);
_values.m_Round =  Integer.parseInt((String)valuesList.get(ID_ROUND));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

