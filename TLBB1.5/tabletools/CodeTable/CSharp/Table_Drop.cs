//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Drop : ITableOperate
{ private const string TAB_FILE_DATA = "drop.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_PROBABILITY,
ID_DROP_TYPE1,
ID_DROP_PRO1,
ID_DROP_VAL1,
ID_DROP_TYPE2,
ID_DROP_PRO2,
ID_DROP_VAL2,
ID_DROP_TYPE3,
ID_DROP_PRO3,
ID_DROP_VAL3,
ID_DROP_TYPE4,
ID_DROP_PRO4,
ID_DROP_VAL4,
ID_DROP_TYPE5,
ID_DROP_PRO5,
ID_DROP_VAL5,
ID_DROP_TYPE6,
ID_DROP_PRO6,
ID_DROP_VAL6,
ID_DROP_TYPE7,
ID_DROP_PRO7,
ID_DROP_VAL7,
ID_DROP_TYPE8,
ID_DROP_PRO8,
ID_DROP_VAL8,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int[] m_DropPro = new int[8];
 public int GetDropProbyIndex(int idx) {
 if(idx>=0 && idx<8) return m_DropPro[idx];
 return -1;
 }

private int[] m_DropType = new int[8];
 public int GetDropTypebyIndex(int idx) {
 if(idx>=0 && idx<8) return m_DropType[idx];
 return -1;
 }

private int[] m_DropVal = new int[8];
 public int GetDropValbyIndex(int idx) {
 if(idx>=0 && idx<8) return m_DropVal[idx];
 return -1;
 }

private int m_Probability;
 public int Probability { get{ return m_Probability;}}

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
 Tab_Drop _values = new Tab_Drop();
 _values.m_DropPro [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_PRO1] as string);
_values.m_DropPro [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_PRO2] as string);
_values.m_DropPro [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_PRO3] as string);
_values.m_DropPro [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_PRO4] as string);
_values.m_DropPro [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_PRO5] as string);
_values.m_DropPro [ 5 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_PRO6] as string);
_values.m_DropPro [ 6 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_PRO7] as string);
_values.m_DropPro [ 7 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_PRO8] as string);
_values.m_DropType [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_TYPE1] as string);
_values.m_DropType [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_TYPE2] as string);
_values.m_DropType [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_TYPE3] as string);
_values.m_DropType [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_TYPE4] as string);
_values.m_DropType [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_TYPE5] as string);
_values.m_DropType [ 5 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_TYPE6] as string);
_values.m_DropType [ 6 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_TYPE7] as string);
_values.m_DropType [ 7 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_TYPE8] as string);
_values.m_DropVal [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_VAL1] as string);
_values.m_DropVal [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_VAL2] as string);
_values.m_DropVal [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_VAL3] as string);
_values.m_DropVal [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_VAL4] as string);
_values.m_DropVal [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_VAL5] as string);
_values.m_DropVal [ 5 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_VAL6] as string);
_values.m_DropVal [ 6 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_VAL7] as string);
_values.m_DropVal [ 7 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_VAL8] as string);
_values.m_Probability =  Convert.ToInt32(valuesList[(int)_ID.ID_PROBABILITY] as string);

 _hash.Add(nKey,_values); }


}
}

