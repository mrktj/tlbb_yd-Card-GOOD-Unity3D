//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Worldboss implements ITableOperate {
 public final static String TAB_FILE_DATA = "/worldboss.txt";
 public static final int ID_BOSSID = 0 ;
public static final int ID_ZHANWEI = 1 ;
public static final int ID_SCENE_NAME = 2 ;
public static final int ID_SCENE_LIB = 3 ;
public static final int ID_DATE = 4 ;
public static final int ID_COPY_MUSIC = 5 ;
public static final int ID_STARTTIME = 6 ;
public static final int ID_ENDTIME = 7 ;
public static final int MAX_RECORD = 8 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_BossID;
 public int GetBossID() { return m_BossID;}

private String m_CopyMusic;
 public String GetCopyMusic() { return m_CopyMusic;}

private int m_Date;
 public int GetDate() { return m_Date;}

private int m_Endtime;
 public int GetEndtime() { return m_Endtime;}

private String m_SceneLib;
 public String GetSceneLib() { return m_SceneLib;}

private String m_SceneName;
 public String GetSceneName() { return m_SceneName;}

private int m_Starttime;
 public int GetStarttime() { return m_Starttime;}

private int m_Zhanwei;
 public int GetZhanwei() { return m_Zhanwei;}

public boolean LoadTable(HashMap<Integer, Table_Worldboss> _tab) throws TableException
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
 HashMap<Integer, Table_Worldboss> _hash = (HashMap<Integer, Table_Worldboss> )obj;
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
 Table_Worldboss _values = new Table_Worldboss();
 _values.m_BossID =  Integer.parseInt((String)valuesList.get(ID_BOSSID));
_values.m_CopyMusic =  (String)valuesList.get(ID_COPY_MUSIC);
_values.m_Date =  Integer.parseInt((String)valuesList.get(ID_DATE));
_values.m_Endtime =  Integer.parseInt((String)valuesList.get(ID_ENDTIME));
_values.m_SceneLib =  (String)valuesList.get(ID_SCENE_LIB);
_values.m_SceneName =  (String)valuesList.get(ID_SCENE_NAME);
_values.m_Starttime =  Integer.parseInt((String)valuesList.get(ID_STARTTIME));
_values.m_Zhanwei =  Integer.parseInt((String)valuesList.get(ID_ZHANWEI));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

