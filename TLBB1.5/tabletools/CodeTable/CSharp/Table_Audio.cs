//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Audio : ITableOperate
{ private const string TAB_FILE_DATA = "audio.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_NAME,
ID_SUFFIX,
ID_PLAYTIMES,
ID_DELAYTIME,
ID_TYPE,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private float m_Delaytime;
 public float Delaytime { get{ return m_Delaytime;}}

private string m_Name;
 public string Name { get{ return m_Name;}}

private int m_Playtimes;
 public int Playtimes { get{ return m_Playtimes;}}

private string m_Suffix;
 public string Suffix { get{ return m_Suffix;}}

private int m_Type;
 public int Type { get{ return m_Type;}}

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
 Tab_Audio _values = new Tab_Audio();
 _values.m_Delaytime =  Convert.ToSingle(valuesList[(int)_ID.ID_DELAYTIME] as string);
_values.m_Name =  valuesList[(int)_ID.ID_NAME] as string;
_values.m_Playtimes =  Convert.ToInt32(valuesList[(int)_ID.ID_PLAYTIMES] as string);
_values.m_Suffix =  valuesList[(int)_ID.ID_SUFFIX] as string;
_values.m_Type =  Convert.ToInt32(valuesList[(int)_ID.ID_TYPE] as string);

 _hash.Add(nKey,_values); }


}
}

