//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Idexperience : ITableOperate
{ private const string TAB_FILE_DATA = "idexperience.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_ID_EXPERIENCE,
ID_ID_PHYSICALVALUE,
ID_ID_COST,
ID_BAG_LIMIT,
ID_FRIEND_LIMIT,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_BagLimit;
 public int BagLimit { get{ return m_BagLimit;}}

private int m_FriendLimit;
 public int FriendLimit { get{ return m_FriendLimit;}}

private int m_IDCost;
 public int IDCost { get{ return m_IDCost;}}

private int m_IDExperience;
 public int IDExperience { get{ return m_IDExperience;}}

private int m_IDPhysicalValue;
 public int IDPhysicalValue { get{ return m_IDPhysicalValue;}}

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
 Tab_Idexperience _values = new Tab_Idexperience();
 _values.m_BagLimit =  Convert.ToInt32(valuesList[(int)_ID.ID_BAG_LIMIT] as string);
_values.m_FriendLimit =  Convert.ToInt32(valuesList[(int)_ID.ID_FRIEND_LIMIT] as string);
_values.m_IDCost =  Convert.ToInt32(valuesList[(int)_ID.ID_ID_COST] as string);
_values.m_IDExperience =  Convert.ToInt32(valuesList[(int)_ID.ID_ID_EXPERIENCE] as string);
_values.m_IDPhysicalValue =  Convert.ToInt32(valuesList[(int)_ID.ID_ID_PHYSICALVALUE] as string);

 _hash.Add(nKey,_values); }


}
}

