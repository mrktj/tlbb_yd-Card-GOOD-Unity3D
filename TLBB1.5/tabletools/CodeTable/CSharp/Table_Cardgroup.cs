//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Cardgroup : ITableOperate
{ private const string TAB_FILE_DATA = "cardgroup.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_CARDID1,
ID_CARDID2,
ID_CARDID3,
ID_CARDID4,
ID_CARDID5,
ID_CARDID6,
ID_CARDID7,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int[] m_CardID = new int[7];
 public int GetCardIDbyIndex(int idx) {
 if(idx>=0 && idx<7) return m_CardID[idx];
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
 Tab_Cardgroup _values = new Tab_Cardgroup();
 _values.m_CardID [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_CARDID1] as string);
_values.m_CardID [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_CARDID2] as string);
_values.m_CardID [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_CARDID3] as string);
_values.m_CardID [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_CARDID4] as string);
_values.m_CardID [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_CARDID5] as string);
_values.m_CardID [ 5 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_CARDID6] as string);
_values.m_CardID [ 6 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_CARDID7] as string);

 _hash.Add(nKey,_values); }


}
}

