//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_FriendGamble : ITableOperate
{ private const string TAB_FILE_DATA = "friend_gamble.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_STAR,
ID_CARDID,
ID_PRO,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_CardID;
 public int CardID { get{ return m_CardID;}}

private int m_Pro;
 public int Pro { get{ return m_Pro;}}

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
 Tab_FriendGamble _values = new Tab_FriendGamble();
 _values.m_CardID =  Convert.ToInt32(valuesList[(int)_ID.ID_CARDID] as string);
_values.m_Pro =  Convert.ToInt32(valuesList[(int)_ID.ID_PRO] as string);
_values.m_Star =  Convert.ToInt32(valuesList[(int)_ID.ID_STAR] as string);

 _hash.Add(nKey,_values); }


}
}

