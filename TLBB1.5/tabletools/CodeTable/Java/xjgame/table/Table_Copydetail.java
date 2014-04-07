//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Copydetail implements ITableOperate {
 public final static String TAB_FILE_DATA = "/copydetail.txt";
 public static final int ID_COPYNAME = 0 ;
public static final int ID_SCENE_NAME = 1 ;
public static final int ID_SCENE_LIB = 2 ;
public static final int ID_COST_VALUE = 3 ;
public static final int ID_DROPITEM1 = 4 ;
public static final int ID_DROPITEM2 = 5 ;
public static final int ID_DROPITEM3 = 6 ;
public static final int ID_FIGHT1_0 = 7 ;
public static final int ID_FIGHT1_1 = 8 ;
public static final int ID_FIGHT1_2 = 9 ;
public static final int ID_FIGHT1_3 = 10 ;
public static final int ID_FIGHT1_4 = 11 ;
public static final int ID_FIGHT1_5 = 12 ;
public static final int ID_FIGHT2_0 = 13 ;
public static final int ID_FIGHT2_1 = 14 ;
public static final int ID_FIGHT2_2 = 15 ;
public static final int ID_FIGHT2_3 = 16 ;
public static final int ID_FIGHT2_4 = 17 ;
public static final int ID_FIGHT2_5 = 18 ;
public static final int ID_FIGHT3_0 = 19 ;
public static final int ID_FIGHT3_1 = 20 ;
public static final int ID_FIGHT3_2 = 21 ;
public static final int ID_FIGHT3_3 = 22 ;
public static final int ID_FIGHT3_4 = 23 ;
public static final int ID_FIGHT3_5 = 24 ;
public static final int ID_EVENTID = 25 ;
public static final int ID_COPY_LIMIT = 26 ;
public static final int ID_COPYFATHER = 27 ;
public static final int ID_COPY_EXPRIENCE = 28 ;
public static final int ID_COPY_MUSIC = 29 ;
public static final int ID_COPY_GOLD = 30 ;
public static final int ID_HETI_NOTICE = 31 ;
public static final int ID_NOTICE_LEFT = 32 ;
public static final int ID_NOTICE_RIGHT = 33 ;
public static final int ID_NOTICE_TEXT = 34 ;
public static final int ID_FONT_TEXT = 35 ;
public static final int ID_BACK_TEXT = 36 ;
public static final int ID_INFORMATION_NOTICE = 37 ;
public static final int MAX_RECORD = 38 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_InformationNotice;
 public int GetInformationNotice() { return m_InformationNotice;}

private int m_BackText;
 public int GetBackText() { return m_BackText;}

private int m_CopyExprience;
 public int GetCopyExprience() { return m_CopyExprience;}

private int m_CopyGold;
 public int GetCopyGold() { return m_CopyGold;}

private int m_CopyLimit;
 public int GetCopyLimit() { return m_CopyLimit;}

private String m_CopyMusic;
 public String GetCopyMusic() { return m_CopyMusic;}

private int m_Copyfather;
 public int GetCopyfather() { return m_Copyfather;}

private int m_Copyname;
 public int GetCopyname() { return m_Copyname;}

private int m_CostValue;
 public int GetCostValue() { return m_CostValue;}

private int m_DropItem[] = new int[3];
 public int GetDropItembyIndex(int idx) {
 if(idx>=0 && idx<3) return m_DropItem[idx];
 return -1;
 }

private int m_EventID;
 public int GetEventID() { return m_EventID;}

private int m_Fight1[] = new int[6];
 public int GetFight1byIndex(int idx) {
 if(idx>=0 && idx<6) return m_Fight1[idx];
 return -1;
 }

private int m_Fight2[] = new int[6];
 public int GetFight2byIndex(int idx) {
 if(idx>=0 && idx<6) return m_Fight2[idx];
 return -1;
 }

private int m_Fight3[] = new int[6];
 public int GetFight3byIndex(int idx) {
 if(idx>=0 && idx<6) return m_Fight3[idx];
 return -1;
 }

private int m_FontText;
 public int GetFontText() { return m_FontText;}

private int m_HetiNotice;
 public int GetHetiNotice() { return m_HetiNotice;}

private int m_NoticeLeft;
 public int GetNoticeLeft() { return m_NoticeLeft;}

private int m_NoticeRight;
 public int GetNoticeRight() { return m_NoticeRight;}

private int m_NoticeText;
 public int GetNoticeText() { return m_NoticeText;}

private String m_SceneLib;
 public String GetSceneLib() { return m_SceneLib;}

private String m_SceneName;
 public String GetSceneName() { return m_SceneName;}

public boolean LoadTable(HashMap<Integer, Table_Copydetail> _tab) throws TableException
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
 HashMap<Integer, Table_Copydetail> _hash = (HashMap<Integer, Table_Copydetail> )obj;
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
 Table_Copydetail _values = new Table_Copydetail();
 _values.m_InformationNotice =  Integer.parseInt((String)valuesList.get(ID_INFORMATION_NOTICE));
_values.m_BackText =  Integer.parseInt((String)valuesList.get(ID_BACK_TEXT));
_values.m_CopyExprience =  Integer.parseInt((String)valuesList.get(ID_COPY_EXPRIENCE));
_values.m_CopyGold =  Integer.parseInt((String)valuesList.get(ID_COPY_GOLD));
_values.m_CopyLimit =  Integer.parseInt((String)valuesList.get(ID_COPY_LIMIT));
_values.m_CopyMusic =  (String)valuesList.get(ID_COPY_MUSIC);
_values.m_Copyfather =  Integer.parseInt((String)valuesList.get(ID_COPYFATHER));
_values.m_Copyname =  Integer.parseInt((String)valuesList.get(ID_COPYNAME));
_values.m_CostValue =  Integer.parseInt((String)valuesList.get(ID_COST_VALUE));
_values.m_DropItem [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_DROPITEM1));
_values.m_DropItem [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_DROPITEM2));
_values.m_DropItem [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_DROPITEM3));
_values.m_EventID =  Integer.parseInt((String)valuesList.get(ID_EVENTID));
_values.m_Fight1 [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT1_0));
_values.m_Fight1 [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT1_1));
_values.m_Fight1 [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT1_2));
_values.m_Fight1 [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT1_3));
_values.m_Fight1 [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT1_4));
_values.m_Fight1 [ 5 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT1_5));
_values.m_Fight2 [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT2_0));
_values.m_Fight2 [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT2_1));
_values.m_Fight2 [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT2_2));
_values.m_Fight2 [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT2_3));
_values.m_Fight2 [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT2_4));
_values.m_Fight2 [ 5 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT2_5));
_values.m_Fight3 [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT3_0));
_values.m_Fight3 [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT3_1));
_values.m_Fight3 [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT3_2));
_values.m_Fight3 [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT3_3));
_values.m_Fight3 [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT3_4));
_values.m_Fight3 [ 5 ] =  Integer.parseInt((String)valuesList.get(ID_FIGHT3_5));
_values.m_FontText =  Integer.parseInt((String)valuesList.get(ID_FONT_TEXT));
_values.m_HetiNotice =  Integer.parseInt((String)valuesList.get(ID_HETI_NOTICE));
_values.m_NoticeLeft =  Integer.parseInt((String)valuesList.get(ID_NOTICE_LEFT));
_values.m_NoticeRight =  Integer.parseInt((String)valuesList.get(ID_NOTICE_RIGHT));
_values.m_NoticeText =  Integer.parseInt((String)valuesList.get(ID_NOTICE_TEXT));
_values.m_SceneLib =  (String)valuesList.get(ID_SCENE_LIB);
_values.m_SceneName =  (String)valuesList.get(ID_SCENE_NAME);

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

