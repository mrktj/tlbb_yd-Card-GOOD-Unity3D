//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Push : ITableOperate
{ private const string TAB_FILE_DATA = "push.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_WEEK,
ID_TIME,
ID_TEXT,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Text;
 public int Text { get{ return m_Text;}}

private int m_Time;
 public int Time { get{ return m_Time;}}

private int m_Week;
 public int Week { get{ return m_Week;}}

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
 Tab_Push _values = new Tab_Push();
 _values.m_Text =  Convert.ToInt32(valuesList[(int)_ID.ID_TEXT] as string);
_values.m_Time =  Convert.ToInt32(valuesList[(int)_ID.ID_TIME] as string);
_values.m_Week =  Convert.ToInt32(valuesList[(int)_ID.ID_WEEK] as string);

 _hash.Add(nKey,_values); }


}
}

