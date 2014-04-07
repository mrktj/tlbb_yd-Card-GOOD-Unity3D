//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Servers : ITableOperate
{ private const string TAB_FILE_DATA = "servers.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_NAME,
ID_DESCRIBE,
ID_STATE,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private string m_Describe;
 public string Describe { get{ return m_Describe;}}

private string m_Name;
 public string Name { get{ return m_Name;}}

private int m_State;
 public int State { get{ return m_State;}}

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
 Tab_Servers _values = new Tab_Servers();
 _values.m_Describe =  valuesList[(int)_ID.ID_DESCRIBE] as string;
_values.m_Name =  valuesList[(int)_ID.ID_NAME] as string;
_values.m_State =  Convert.ToInt32(valuesList[(int)_ID.ID_STATE] as string);

 _hash.Add(nKey,_values); }


}
}

