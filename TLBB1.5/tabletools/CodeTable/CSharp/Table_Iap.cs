//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Iap : ITableOperate
{ private const string TAB_FILE_DATA = "iap.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_GOODSID,
ID_GOODSTYPE,
ID_DESCRIBE,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private string m_Describe;
 public string Describe { get{ return m_Describe;}}

private int m_GoodsID;
 public int GoodsID { get{ return m_GoodsID;}}

private int m_Goodstype;
 public int Goodstype { get{ return m_Goodstype;}}

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
 Tab_Iap _values = new Tab_Iap();
 _values.m_Describe =  valuesList[(int)_ID.ID_DESCRIBE] as string;
_values.m_GoodsID =  Convert.ToInt32(valuesList[(int)_ID.ID_GOODSID] as string);
_values.m_Goodstype =  Convert.ToInt32(valuesList[(int)_ID.ID_GOODSTYPE] as string);

 _hash.Add(nKey,_values); }


}
}

