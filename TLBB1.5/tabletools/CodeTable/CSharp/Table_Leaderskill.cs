//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Leaderskill : ITableOperate
{ private const string TAB_FILE_DATA = "leaderskill.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_LOGIC_ID,
ID_ADD_PER,
ID_ADD_VALUE,
ID_NAME,
ID_NOTE,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_AddPer;
 public int AddPer { get{ return m_AddPer;}}

private int m_AddValue;
 public int AddValue { get{ return m_AddValue;}}

private int m_LogicId;
 public int LogicId { get{ return m_LogicId;}}

private int m_Name;
 public int Name { get{ return m_Name;}}

private int m_Note;
 public int Note { get{ return m_Note;}}

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
 Tab_Leaderskill _values = new Tab_Leaderskill();
 _values.m_AddPer =  Convert.ToInt32(valuesList[(int)_ID.ID_ADD_PER] as string);
_values.m_AddValue =  Convert.ToInt32(valuesList[(int)_ID.ID_ADD_VALUE] as string);
_values.m_LogicId =  Convert.ToInt32(valuesList[(int)_ID.ID_LOGIC_ID] as string);
_values.m_Name =  Convert.ToInt32(valuesList[(int)_ID.ID_NAME] as string);
_values.m_Note =  Convert.ToInt32(valuesList[(int)_ID.ID_NOTE] as string);

 _hash.Add(nKey,_values); }


}
}

