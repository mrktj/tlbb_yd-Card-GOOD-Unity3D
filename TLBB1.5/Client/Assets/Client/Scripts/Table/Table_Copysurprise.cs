//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Copysurprise : ITableOperate
{ private const string TAB_FILE_DATA = "copysurprise.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_NPCID,
ID_NPC_WEIGHT,
ID_RATE,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private string m_NpcID;
 public string NpcID { get{ return m_NpcID;}}

private string m_NpcWeight;
 public string NpcWeight { get{ return m_NpcWeight;}}

private int m_Rate;
 public int Rate { get{ return m_Rate;}}

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
 Tab_Copysurprise _values = new Tab_Copysurprise();
 _values.m_NpcID =  valuesList[(int)_ID.ID_NPCID] as string;
_values.m_NpcWeight =  valuesList[(int)_ID.ID_NPC_WEIGHT] as string;
_values.m_Rate =  Convert.ToInt32(valuesList[(int)_ID.ID_RATE] as string);

 _hash.Add(nKey,_values); }


}
}

