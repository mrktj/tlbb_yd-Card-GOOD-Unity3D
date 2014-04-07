//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Copy : ITableOperate
{ private const string TAB_FILE_DATA = "copy.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_COPYNAME,
ID_NOTE,
ID_COPY_TYPE,
ID_COPY_TYPE2,
ID_STARTWEEK,
ID_STARTTIME,
ID_TIMES,
ID_COPY_ORDER,
ID_COPY_NEXT,
ID_PLAYER_LEVEL,
ID_SUBCOPY1,
ID_SUBCOPY2,
ID_SUBCOPY3,
ID_SUBCOPY4,
ID_SUBCOPY5,
ID_SUBCOPY6,
ID_SUBCOPY7,
ID_SUBCOPY8,
ID_SUBCOPY9,
ID_SUBCOPY10,
ID_SUBCOPY11,
ID_SUBCOPY12,
ID_SUBCOPY13,
ID_SUBCOPY14,
ID_SUBCOPY15,
ID_DROP,
ID_THUMB,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_CopyNext;
 public int CopyNext { get{ return m_CopyNext;}}

private int m_CopyOrder;
 public int CopyOrder { get{ return m_CopyOrder;}}

private int[] m_CopyType = new int[2];
 public int GetCopyTypebyIndex(int idx) {
 if(idx>=0 && idx<2) return m_CopyType[idx];
 return -1;
 }

private int m_Copyname;
 public int Copyname { get{ return m_Copyname;}}

private int m_Drop;
 public int Drop { get{ return m_Drop;}}

private int m_Note;
 public int Note { get{ return m_Note;}}

private int m_PlayerLevel;
 public int PlayerLevel { get{ return m_PlayerLevel;}}

private int m_Starttime;
 public int Starttime { get{ return m_Starttime;}}

private int m_Startweek;
 public int Startweek { get{ return m_Startweek;}}

private int[] m_Subcopy = new int[15];
 public int GetSubcopybyIndex(int idx) {
 if(idx>=0 && idx<15) return m_Subcopy[idx];
 return -1;
 }

private string m_Thumb;
 public string Thumb { get{ return m_Thumb;}}

private int m_Times;
 public int Times { get{ return m_Times;}}

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
 Tab_Copy _values = new Tab_Copy();
 _values.m_CopyNext =  Convert.ToInt32(valuesList[(int)_ID.ID_COPY_NEXT] as string);
_values.m_CopyOrder =  Convert.ToInt32(valuesList[(int)_ID.ID_COPY_ORDER] as string);
_values.m_CopyType [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_COPY_TYPE] as string);
_values.m_CopyType [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_COPY_TYPE2] as string);
_values.m_Copyname =  Convert.ToInt32(valuesList[(int)_ID.ID_COPYNAME] as string);
_values.m_Drop =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP] as string);
_values.m_Note =  Convert.ToInt32(valuesList[(int)_ID.ID_NOTE] as string);
_values.m_PlayerLevel =  Convert.ToInt32(valuesList[(int)_ID.ID_PLAYER_LEVEL] as string);
_values.m_Starttime =  Convert.ToInt32(valuesList[(int)_ID.ID_STARTTIME] as string);
_values.m_Startweek =  Convert.ToInt32(valuesList[(int)_ID.ID_STARTWEEK] as string);
_values.m_Subcopy [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY1] as string);
_values.m_Subcopy [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY2] as string);
_values.m_Subcopy [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY3] as string);
_values.m_Subcopy [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY4] as string);
_values.m_Subcopy [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY5] as string);
_values.m_Subcopy [ 5 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY6] as string);
_values.m_Subcopy [ 6 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY7] as string);
_values.m_Subcopy [ 7 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY8] as string);
_values.m_Subcopy [ 8 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY9] as string);
_values.m_Subcopy [ 9 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY10] as string);
_values.m_Subcopy [ 10 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY11] as string);
_values.m_Subcopy [ 11 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY12] as string);
_values.m_Subcopy [ 12 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY13] as string);
_values.m_Subcopy [ 13 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY14] as string);
_values.m_Subcopy [ 14 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SUBCOPY15] as string);
_values.m_Thumb =  valuesList[(int)_ID.ID_THUMB] as string;
_values.m_Times =  Convert.ToInt32(valuesList[(int)_ID.ID_TIMES] as string);

 _hash.Add(nKey,_values); }


}
}

