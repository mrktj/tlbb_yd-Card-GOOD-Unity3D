//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Skillbasicchance : ITableOperate
{ private const string TAB_FILE_DATA = "skillbasicchance.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_BASIC_CHANCE1,
ID_BASIC_CHANCE2,
ID_BASIC_CHANCE3,
ID_BASIC_CHANCE4,
ID_BASIC_CHANCE5,
ID_BASIC_CHANCE6,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int[] m_BasicChance = new int[6];
 public int GetBasicChancebyIndex(int idx) {
 if(idx>=0 && idx<6) return m_BasicChance[idx];
 return -1;
 }

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
 Tab_Skillbasicchance _values = new Tab_Skillbasicchance();
 _values.m_BasicChance [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_BASIC_CHANCE1] as string);
_values.m_BasicChance [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_BASIC_CHANCE2] as string);
_values.m_BasicChance [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_BASIC_CHANCE3] as string);
_values.m_BasicChance [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_BASIC_CHANCE4] as string);
_values.m_BasicChance [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_BASIC_CHANCE5] as string);
_values.m_BasicChance [ 5 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_BASIC_CHANCE6] as string);

 _hash.Add(nKey,_values); }


}
}

