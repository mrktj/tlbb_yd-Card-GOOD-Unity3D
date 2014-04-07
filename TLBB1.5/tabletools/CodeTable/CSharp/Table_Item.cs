//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Item : ITableOperate
{ private const string TAB_FILE_DATA = "item.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_NAME,
ID_INTRODUCE,
ID_ICON,
ID_TYPE,
ID_VALUE,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private string m_Icon;
 public string Icon { get{ return m_Icon;}}

private int m_Introduce;
 public int Introduce { get{ return m_Introduce;}}

private int m_Name;
 public int Name { get{ return m_Name;}}

private int m_Type;
 public int Type { get{ return m_Type;}}

private int m_Value;
 public int Value { get{ return m_Value;}}

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
 Tab_Item _values = new Tab_Item();
 _values.m_Icon =  valuesList[(int)_ID.ID_ICON] as string;
_values.m_Introduce =  Convert.ToInt32(valuesList[(int)_ID.ID_INTRODUCE] as string);
_values.m_Name =  Convert.ToInt32(valuesList[(int)_ID.ID_NAME] as string);
_values.m_Type =  Convert.ToInt32(valuesList[(int)_ID.ID_TYPE] as string);
_values.m_Value =  Convert.ToInt32(valuesList[(int)_ID.ID_VALUE] as string);

 _hash.Add(nKey,_values); }


}
}

