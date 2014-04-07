//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Eightsided : ITableOperate
{ private const string TAB_FILE_DATA = "eightsided.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_DAY_TIME,
ID_REWARD1_TYPE,
ID_VALUE_1,
ID_REWARD1_CHANCE,
ID_REWARD2_TYPE,
ID_VALUE_2,
ID_REWARD2_CHANCE,
ID_REWARD3_TYPE,
ID_VALUE_3,
ID_REWARD3_CHANCE,
ID_REWARD4_TYPE,
ID_VALUE_4,
ID_REWARD4_CHANCE,
ID_REWARD5_TYPE,
ID_VALUE_5,
ID_REWARD5_CHANCE,
ID_REWARD6_TYPE,
ID_VALUE_6,
ID_REWARD6_CHANCE,
ID_REWARD7_TYPE,
ID_VALUE_7,
ID_REWARD7_CHANCE,
ID_REWARD8_TYPE,
ID_VALUE_8,
ID_REWARD8_CHANCE,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_ReWard1Chance;
 public int ReWard1Chance { get{ return m_ReWard1Chance;}}

private int m_ReWard1Type;
 public int ReWard1Type { get{ return m_ReWard1Type;}}

private int m_ReWard2Chance;
 public int ReWard2Chance { get{ return m_ReWard2Chance;}}

private int m_ReWard2Type;
 public int ReWard2Type { get{ return m_ReWard2Type;}}

private int m_ReWard3Chance;
 public int ReWard3Chance { get{ return m_ReWard3Chance;}}

private int m_ReWard3Type;
 public int ReWard3Type { get{ return m_ReWard3Type;}}

private int m_ReWard4Chance;
 public int ReWard4Chance { get{ return m_ReWard4Chance;}}

private int m_ReWard4Type;
 public int ReWard4Type { get{ return m_ReWard4Type;}}

private int m_ReWard5Chance;
 public int ReWard5Chance { get{ return m_ReWard5Chance;}}

private int m_ReWard5Type;
 public int ReWard5Type { get{ return m_ReWard5Type;}}

private int m_ReWard6Chance;
 public int ReWard6Chance { get{ return m_ReWard6Chance;}}

private int m_ReWard6Type;
 public int ReWard6Type { get{ return m_ReWard6Type;}}

private int m_ReWard7Chance;
 public int ReWard7Chance { get{ return m_ReWard7Chance;}}

private int m_ReWard7Type;
 public int ReWard7Type { get{ return m_ReWard7Type;}}

private int m_ReWard8Chance;
 public int ReWard8Chance { get{ return m_ReWard8Chance;}}

private int m_ReWard8Type;
 public int ReWard8Type { get{ return m_ReWard8Type;}}

private int m_DayTime;
 public int DayTime { get{ return m_DayTime;}}

private int[] m_Value = new int[8];
 public int GetValuebyIndex(int idx) {
 if(idx>=0 && idx<8) return m_Value[idx];
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
 Tab_Eightsided _values = new Tab_Eightsided();
 _values.m_ReWard1Chance =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD1_CHANCE] as string);
_values.m_ReWard1Type =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD1_TYPE] as string);
_values.m_ReWard2Chance =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD2_CHANCE] as string);
_values.m_ReWard2Type =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD2_TYPE] as string);
_values.m_ReWard3Chance =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD3_CHANCE] as string);
_values.m_ReWard3Type =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD3_TYPE] as string);
_values.m_ReWard4Chance =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD4_CHANCE] as string);
_values.m_ReWard4Type =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD4_TYPE] as string);
_values.m_ReWard5Chance =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD5_CHANCE] as string);
_values.m_ReWard5Type =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD5_TYPE] as string);
_values.m_ReWard6Chance =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD6_CHANCE] as string);
_values.m_ReWard6Type =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD6_TYPE] as string);
_values.m_ReWard7Chance =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD7_CHANCE] as string);
_values.m_ReWard7Type =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD7_TYPE] as string);
_values.m_ReWard8Chance =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD8_CHANCE] as string);
_values.m_ReWard8Type =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD8_TYPE] as string);
_values.m_DayTime =  Convert.ToInt32(valuesList[(int)_ID.ID_DAY_TIME] as string);
_values.m_Value [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_VALUE_1] as string);
_values.m_Value [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_VALUE_2] as string);
_values.m_Value [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_VALUE_3] as string);
_values.m_Value [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_VALUE_4] as string);
_values.m_Value [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_VALUE_5] as string);
_values.m_Value [ 5 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_VALUE_6] as string);
_values.m_Value [ 6 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_VALUE_7] as string);
_values.m_Value [ 7 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_VALUE_8] as string);

 _hash.Add(nKey,_values); }


}
}

