//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_PvpShop : ITableOperate
{ private const string TAB_FILE_DATA = "pvp_shop.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_TITLE,
ID_DESCRIPTION,
ID_SCORE,
ID_TYPE,
ID_NUM,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private string m_Description;
 public string Description { get{ return m_Description;}}

private int m_Num;
 public int Num { get{ return m_Num;}}

private int m_Score;
 public int Score { get{ return m_Score;}}

private string m_Title;
 public string Title { get{ return m_Title;}}

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
 Tab_PvpShop _values = new Tab_PvpShop();
 _values.m_Description =  valuesList[(int)_ID.ID_DESCRIPTION] as string;
_values.m_Num =  Convert.ToInt32(valuesList[(int)_ID.ID_NUM] as string);
_values.m_Score =  Convert.ToInt32(valuesList[(int)_ID.ID_SCORE] as string);
_values.m_Title =  valuesList[(int)_ID.ID_TITLE] as string;
_values.m_Type =  Convert.ToInt32(valuesList[(int)_ID.ID_TYPE] as string);

 _hash.Add(nKey,_values); }


}
}

