//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_ScratchPrize : ITableOperate
{ private const string TAB_FILE_DATA = "scratch_prize.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_HEAD_ICON,
ID_WEIGHT,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private string m_HeadIcon;
 public string HeadIcon { get{ return m_HeadIcon;}}

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
 Tab_ScratchPrize _values = new Tab_ScratchPrize();
 _values.m_HeadIcon =  valuesList[(int)_ID.ID_HEAD_ICON] as string;
_values.m_Weight =  Convert.ToInt32(valuesList[(int)_ID.ID_WEIGHT] as string);

 _hash.Add(nKey,_values); }


}
}

