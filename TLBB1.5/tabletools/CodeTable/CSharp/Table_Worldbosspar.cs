//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Worldbosspar : ITableOperate
{ private const string TAB_FILE_DATA = "worldbosspar.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_HP_PAR,
ID_ATT_PAR,
ID_GOLD_PAR,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_AttPar;
 public int AttPar { get{ return m_AttPar;}}

private int m_GoldPar;
 public int GoldPar { get{ return m_GoldPar;}}

private int m_HpPar;
 public int HpPar { get{ return m_HpPar;}}

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
 Tab_Worldbosspar _values = new Tab_Worldbosspar();
 _values.m_AttPar =  Convert.ToInt32(valuesList[(int)_ID.ID_ATT_PAR] as string);
_values.m_GoldPar =  Convert.ToInt32(valuesList[(int)_ID.ID_GOLD_PAR] as string);
_values.m_HpPar =  Convert.ToInt32(valuesList[(int)_ID.ID_HP_PAR] as string);

 _hash.Add(nKey,_values); }


}
}

