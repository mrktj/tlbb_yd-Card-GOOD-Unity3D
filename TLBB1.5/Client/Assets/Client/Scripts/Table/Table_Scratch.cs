//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Scratch : ITableOperate
{ private const string TAB_FILE_DATA = "scratch.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_PRIZE_TYPE,
ID_VALUE,
ID_WEIGHT,
ID_PRIZE,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Prize;
 public int Prize { get{ return m_Prize;}}

private int m_PrizeType;
 public int PrizeType { get{ return m_PrizeType;}}

private int m_Value;
 public int Value { get{ return m_Value;}}

private int m_Weight;
 public int Weight { get{ return m_Weight;}}

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
 Tab_Scratch _values = new Tab_Scratch();
 _values.m_Prize =  Convert.ToInt32(valuesList[(int)_ID.ID_PRIZE] as string);
_values.m_PrizeType =  Convert.ToInt32(valuesList[(int)_ID.ID_PRIZE_TYPE] as string);
_values.m_Value =  Convert.ToInt32(valuesList[(int)_ID.ID_VALUE] as string);
_values.m_Weight =  Convert.ToInt32(valuesList[(int)_ID.ID_WEIGHT] as string);

 _hash.Add(nKey,_values); }


}
}

