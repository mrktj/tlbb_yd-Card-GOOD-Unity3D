//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Language : ITableOperate
{ private const string TAB_FILE_DATA = "language.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_CHINESE,
ID_ENGLISH,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private string m_Chinese;
 public string Chinese { get{ return m_Chinese;}}

private string m_English;
 public string English { get{ return m_English;}}

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
 Tab_Language _values = new Tab_Language();
 _values.m_Chinese =  valuesList[(int)_ID.ID_CHINESE] as string;
_values.m_English =  valuesList[(int)_ID.ID_ENGLISH] as string;

 _hash.Add(nKey,_values); }


}
}

