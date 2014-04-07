//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Effect : ITableOperate
{ private const string TAB_FILE_DATA = "effect.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_NAME,
ID_DESCRIBE,
ID_ANIMATION,
ID_MUSIC,
ID_LOGICID,
ID_ROUND,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private string m_Animation;
 public string Animation { get{ return m_Animation;}}

private int m_Describe;
 public int Describe { get{ return m_Describe;}}

private int m_Logicid;
 public int Logicid { get{ return m_Logicid;}}

private string m_Music;
 public string Music { get{ return m_Music;}}

private string m_Name;
 public string Name { get{ return m_Name;}}

private int m_Round;
 public int Round { get{ return m_Round;}}

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
 Tab_Effect _values = new Tab_Effect();
 _values.m_Animation =  valuesList[(int)_ID.ID_ANIMATION] as string;
_values.m_Describe =  Convert.ToInt32(valuesList[(int)_ID.ID_DESCRIBE] as string);
_values.m_Logicid =  Convert.ToInt32(valuesList[(int)_ID.ID_LOGICID] as string);
_values.m_Music =  valuesList[(int)_ID.ID_MUSIC] as string;
_values.m_Name =  valuesList[(int)_ID.ID_NAME] as string;
_values.m_Round =  Convert.ToInt32(valuesList[(int)_ID.ID_ROUND] as string);

 _hash.Add(nKey,_values); }


}
}

