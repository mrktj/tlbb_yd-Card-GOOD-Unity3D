//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Copydetail : ITableOperate
{ private const string TAB_FILE_DATA = "copydetail.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_COPYNAME,
ID_SCENE_NAME,
ID_SCENE_LIB,
ID_COST_VALUE,
ID_DROPITEM1,
ID_DROPITEM2,
ID_DROPITEM3,
ID_FIGHT1_0,
ID_FIGHT1_1,
ID_FIGHT1_2,
ID_FIGHT1_3,
ID_FIGHT1_4,
ID_FIGHT1_5,
ID_FIGHT2_0,
ID_FIGHT2_1,
ID_FIGHT2_2,
ID_FIGHT2_3,
ID_FIGHT2_4,
ID_FIGHT2_5,
ID_FIGHT3_0,
ID_FIGHT3_1,
ID_FIGHT3_2,
ID_FIGHT3_3,
ID_FIGHT3_4,
ID_FIGHT3_5,
ID_EVENTID,
ID_COPY_LIMIT,
ID_COPYFATHER,
ID_COPY_EXPRIENCE,
ID_COPY_MUSIC,
ID_COPY_GOLD,
ID_HETI_NOTICE,
ID_NOTICE_LEFT,
ID_NOTICE_RIGHT,
ID_NOTICE_TEXT,
ID_FONT_TEXT,
ID_BACK_TEXT,
ID_INFORMATION_NOTICE,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_InformationNotice;
 public int InformationNotice { get{ return m_InformationNotice;}}

private int m_BackText;
 public int BackText { get{ return m_BackText;}}

private int m_CopyExprience;
 public int CopyExprience { get{ return m_CopyExprience;}}

private int m_CopyGold;
 public int CopyGold { get{ return m_CopyGold;}}

private int m_CopyLimit;
 public int CopyLimit { get{ return m_CopyLimit;}}

private string m_CopyMusic;
 public string CopyMusic { get{ return m_CopyMusic;}}

private int m_Copyfather;
 public int Copyfather { get{ return m_Copyfather;}}

private int m_Copyname;
 public int Copyname { get{ return m_Copyname;}}

private int m_CostValue;
 public int CostValue { get{ return m_CostValue;}}

private int[] m_DropItem = new int[3];
 public int GetDropItembyIndex(int idx) {
 if(idx>=0 && idx<3) return m_DropItem[idx];
 return -1;
 }

private int m_EventID;
 public int EventID { get{ return m_EventID;}}

private int[] m_Fight1 = new int[6];
 public int GetFight1byIndex(int idx) {
 if(idx>=0 && idx<6) return m_Fight1[idx];
 return -1;
 }

private int[] m_Fight2 = new int[6];
 public int GetFight2byIndex(int idx) {
 if(idx>=0 && idx<6) return m_Fight2[idx];
 return -1;
 }

private int[] m_Fight3 = new int[6];
 public int GetFight3byIndex(int idx) {
 if(idx>=0 && idx<6) return m_Fight3[idx];
 return -1;
 }

private int m_FontText;
 public int FontText { get{ return m_FontText;}}

private int m_HetiNotice;
 public int HetiNotice { get{ return m_HetiNotice;}}

private int m_NoticeLeft;
 public int NoticeLeft { get{ return m_NoticeLeft;}}

private int m_NoticeRight;
 public int NoticeRight { get{ return m_NoticeRight;}}

private int m_NoticeText;
 public int NoticeText { get{ return m_NoticeText;}}

private string m_SceneLib;
 public string SceneLib { get{ return m_SceneLib;}}

private string m_SceneName;
 public string SceneName { get{ return m_SceneName;}}

public bool LoadTable(Hashtable _tab)
 {
 if(!TableManager.ReaderPList(GetInstanceFile(),SerializableTable,_tab))
 {
 throw TableException.ErrorReader("Load File{0} Fail!!!",GetInstanceFile());
 }
 return true;
 }
 public void SerializableTable(ArrayList valuesList,string skey,Hashtable _hash)
 {
 if (string.IsNullOrEmpty(skey))
 {
 throw TableException.ErrorReader("Read File{0} as key is Empty Fail!!!", GetInstanceFile());
 }

 if ((int)_ID.MAX_RECORD!=valuesList.Count)
 {
 throw TableException.ErrorReader("Load {0} error as CodeSize:{1} not Equal DataSize:{2}", GetInstanceFile(),_ID.MAX_RECORD,valuesList.Count);
 }
 Int32 nKey = Convert.ToInt32(skey);
 Tab_Copydetail _values = new Tab_Copydetail();
 _values.m_InformationNotice =  Convert.ToInt32(valuesList[(int)_ID.ID_INFORMATION_NOTICE] as string);
_values.m_BackText =  Convert.ToInt32(valuesList[(int)_ID.ID_BACK_TEXT] as string);
_values.m_CopyExprience =  Convert.ToInt32(valuesList[(int)_ID.ID_COPY_EXPRIENCE] as string);
_values.m_CopyGold =  Convert.ToInt32(valuesList[(int)_ID.ID_COPY_GOLD] as string);
_values.m_CopyLimit =  Convert.ToInt32(valuesList[(int)_ID.ID_COPY_LIMIT] as string);
_values.m_CopyMusic =  valuesList[(int)_ID.ID_COPY_MUSIC] as string;
_values.m_Copyfather =  Convert.ToInt32(valuesList[(int)_ID.ID_COPYFATHER] as string);
_values.m_Copyname =  Convert.ToInt32(valuesList[(int)_ID.ID_COPYNAME] as string);
_values.m_CostValue =  Convert.ToInt32(valuesList[(int)_ID.ID_COST_VALUE] as string);
_values.m_DropItem [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROPITEM1] as string);
_values.m_DropItem [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROPITEM2] as string);
_values.m_DropItem [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROPITEM3] as string);
_values.m_EventID =  Convert.ToInt32(valuesList[(int)_ID.ID_EVENTID] as string);
_values.m_Fight1 [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT1_0] as string);
_values.m_Fight1 [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT1_1] as string);
_values.m_Fight1 [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT1_2] as string);
_values.m_Fight1 [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT1_3] as string);
_values.m_Fight1 [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT1_4] as string);
_values.m_Fight1 [ 5 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT1_5] as string);
_values.m_Fight2 [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT2_0] as string);
_values.m_Fight2 [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT2_1] as string);
_values.m_Fight2 [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT2_2] as string);
_values.m_Fight2 [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT2_3] as string);
_values.m_Fight2 [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT2_4] as string);
_values.m_Fight2 [ 5 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT2_5] as string);
_values.m_Fight3 [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT3_0] as string);
_values.m_Fight3 [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT3_1] as string);
_values.m_Fight3 [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT3_2] as string);
_values.m_Fight3 [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT3_3] as string);
_values.m_Fight3 [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT3_4] as string);
_values.m_Fight3 [ 5 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT3_5] as string);
_values.m_FontText =  Convert.ToInt32(valuesList[(int)_ID.ID_FONT_TEXT] as string);
_values.m_HetiNotice =  Convert.ToInt32(valuesList[(int)_ID.ID_HETI_NOTICE] as string);
_values.m_NoticeLeft =  Convert.ToInt32(valuesList[(int)_ID.ID_NOTICE_LEFT] as string);
_values.m_NoticeRight =  Convert.ToInt32(valuesList[(int)_ID.ID_NOTICE_RIGHT] as string);
_values.m_NoticeText =  Convert.ToInt32(valuesList[(int)_ID.ID_NOTICE_TEXT] as string);
_values.m_SceneLib =  valuesList[(int)_ID.ID_SCENE_LIB] as string;
_values.m_SceneName =  valuesList[(int)_ID.ID_SCENE_NAME] as string;

 _hash.Add(nKey,_values); }


}
}

