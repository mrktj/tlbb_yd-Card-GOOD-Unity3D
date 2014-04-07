//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_PvpBot : ITableOperate
{ private const string TAB_FILE_DATA = "pvp_bot.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_STAR,
ID_MIN_LEVEL,
ID_MAX_LEVEL,
ID_QUANTITY_BOT,
ID_MIN_QUANTITY_CARD,
ID_MIN_QUANTITY_MAX,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_MaxLevel;
 public int MaxLevel { get{ return m_MaxLevel;}}

private int m_MinLevel;
 public int MinLevel { get{ return m_MinLevel;}}

private int m_MinQuantityCard;
 public int MinQuantityCard { get{ return m_MinQuantityCard;}}

private int m_MinQuantityMax;
 public int MinQuantityMax { get{ return m_MinQuantityMax;}}

private int m_QuantityBot;
 public int QuantityBot { get{ return m_QuantityBot;}}

private int m_Star;
 public int Star { get{ return m_Star;}}

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
 Tab_PvpBot _values = new Tab_PvpBot();
 _values.m_MaxLevel =  Convert.ToInt32(valuesList[(int)_ID.ID_MAX_LEVEL] as string);
_values.m_MinLevel =  Convert.ToInt32(valuesList[(int)_ID.ID_MIN_LEVEL] as string);
_values.m_MinQuantityCard =  Convert.ToInt32(valuesList[(int)_ID.ID_MIN_QUANTITY_CARD] as string);
_values.m_MinQuantityMax =  Convert.ToInt32(valuesList[(int)_ID.ID_MIN_QUANTITY_MAX] as string);
_values.m_QuantityBot =  Convert.ToInt32(valuesList[(int)_ID.ID_QUANTITY_BOT] as string);
_values.m_Star =  Convert.ToInt32(valuesList[(int)_ID.ID_STAR] as string);

 _hash.Add(nKey,_values); }


}
}

