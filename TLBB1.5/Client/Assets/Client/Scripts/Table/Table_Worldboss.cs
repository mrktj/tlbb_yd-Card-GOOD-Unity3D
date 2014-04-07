//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Worldboss : ITableOperate
{ private const string TAB_FILE_DATA = "worldboss.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_BOSSID,
ID_ZHANWEI,
ID_SCENE_NAME,
ID_SCENE_LIB,
ID_DATE,
ID_COPY_MUSIC,
ID_STARTTIME,
ID_ENDTIME,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_BossID;
 public int BossID { get{ return m_BossID;}}

private string m_CopyMusic;
 public string CopyMusic { get{ return m_CopyMusic;}}

private int m_Date;
 public int Date { get{ return m_Date;}}

private int m_Endtime;
 public int Endtime { get{ return m_Endtime;}}

private string m_SceneLib;
 public string SceneLib { get{ return m_SceneLib;}}

private string m_SceneName;
 public string SceneName { get{ return m_SceneName;}}

private int m_Starttime;
 public int Starttime { get{ return m_Starttime;}}

private int m_Zhanwei;
 public int Zhanwei { get{ return m_Zhanwei;}}

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
 Tab_Worldboss _values = new Tab_Worldboss();
 _values.m_BossID =  Convert.ToInt32(valuesList[(int)_ID.ID_BOSSID] as string);
_values.m_CopyMusic =  valuesList[(int)_ID.ID_COPY_MUSIC] as string;
_values.m_Date =  Convert.ToInt32(valuesList[(int)_ID.ID_DATE] as string);
_values.m_Endtime =  Convert.ToInt32(valuesList[(int)_ID.ID_ENDTIME] as string);
_values.m_SceneLib =  valuesList[(int)_ID.ID_SCENE_LIB] as string;
_values.m_SceneName =  valuesList[(int)_ID.ID_SCENE_NAME] as string;
_values.m_Starttime =  Convert.ToInt32(valuesList[(int)_ID.ID_STARTTIME] as string);
_values.m_Zhanwei =  Convert.ToInt32(valuesList[(int)_ID.ID_ZHANWEI] as string);

 _hash.Add(nKey,_values); }


}
}

