//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_StarGamble : ITableOperate
{ private const string TAB_FILE_DATA = "star_gamble.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_STAR,
ID_FRIEND_WEIGHT,
ID_DOLLAR_WEIGHT,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_DollarWeight;
 public int DollarWeight { get{ return m_DollarWeight;}}

private int m_FriendWeight;
 public int FriendWeight { get{ return m_FriendWeight;}}

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
 Tab_StarGamble _values = new Tab_StarGamble();
 _values.m_DollarWeight =  Convert.ToInt32(valuesList[(int)_ID.ID_DOLLAR_WEIGHT] as string);
_values.m_FriendWeight =  Convert.ToInt32(valuesList[(int)_ID.ID_FRIEND_WEIGHT] as string);
_values.m_Star =  Convert.ToInt32(valuesList[(int)_ID.ID_STAR] as string);

 _hash.Add(nKey,_values); }


}
}

