//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Worldbossaward : ITableOperate
{ private const string TAB_FILE_DATA = "worldbossaward.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_TYPE,
ID_PAR,
ID_AWARD_GOLD,
ID_AWARD_YUANBAO,
ID_AWARD_LEADER,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_AwardGold;
 public int AwardGold { get{ return m_AwardGold;}}

private int m_AwardLeader;
 public int AwardLeader { get{ return m_AwardLeader;}}

private int m_AwardYuanbao;
 public int AwardYuanbao { get{ return m_AwardYuanbao;}}

private int m_Par;
 public int Par { get{ return m_Par;}}

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
 Tab_Worldbossaward _values = new Tab_Worldbossaward();
 _values.m_AwardGold =  Convert.ToInt32(valuesList[(int)_ID.ID_AWARD_GOLD] as string);
_values.m_AwardLeader =  Convert.ToInt32(valuesList[(int)_ID.ID_AWARD_LEADER] as string);
_values.m_AwardYuanbao =  Convert.ToInt32(valuesList[(int)_ID.ID_AWARD_YUANBAO] as string);
_values.m_Par =  Convert.ToInt32(valuesList[(int)_ID.ID_PAR] as string);
_values.m_Type =  Convert.ToInt32(valuesList[(int)_ID.ID_TYPE] as string);

 _hash.Add(nKey,_values); }


}
}

