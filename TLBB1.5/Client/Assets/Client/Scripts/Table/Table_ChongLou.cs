//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_ChongLou : ITableOperate
{ private const string TAB_FILE_DATA = "ChongLou.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_FIGHT1_0,
ID_FIGHT1_1,
ID_FIGHT1_2,
ID_FIGHT1_3,
ID_FIGHT1_4,
ID_FIGHT1_5,
ID_COPY_DROP_1,
ID_COPY_NUM_1,
ID_COPY_DROP_2,
ID_COPY_NUM_2,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int[] m_CopyDrop = new int[2];
 public int GetCopyDropbyIndex(int idx) {
 if(idx>=0 && idx<2) return m_CopyDrop[idx];
 return -1;
 }

private int[] m_CopyNum = new int[2];
 public int GetCopyNumbyIndex(int idx) {
 if(idx>=0 && idx<2) return m_CopyNum[idx];
 return -1;
 }

private int[] m_Fight1 = new int[6];
 public int GetFight1byIndex(int idx) {
 if(idx>=0 && idx<6) return m_Fight1[idx];
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
 Tab_ChongLou _values = new Tab_ChongLou();
 _values.m_CopyDrop [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_COPY_DROP_1] as string);
_values.m_CopyDrop [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_COPY_DROP_2] as string);
_values.m_CopyNum [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_COPY_NUM_1] as string);
_values.m_CopyNum [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_COPY_NUM_2] as string);
_values.m_Fight1 [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT1_0] as string);
_values.m_Fight1 [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT1_1] as string);
_values.m_Fight1 [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT1_2] as string);
_values.m_Fight1 [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT1_3] as string);
_values.m_Fight1 [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT1_4] as string);
_values.m_Fight1 [ 5 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_FIGHT1_5] as string);

 _hash.Add(nKey,_values); }


}
}

