//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_GambleChancevalue : ITableOperate
{ private const string TAB_FILE_DATA = "gamble_chancevalue.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_PLAYER_MONEY,
ID_STAR1_CHANCEVALUE,
ID_STAR2_CHANCEVALUE,
ID_STAR3_CHANCEVALUE,
ID_STAR4_CHANCEVALUE,
ID_STAR5_CHANCEVALUE,
ID_STAR6_CHANCEVALUE,
ID_STAR7_CHANCEVALUE,
ID_IMONEY,
ID_CHANCEVALUE,
ID_MAXVALUE,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_ChanceValue;
 public int ChanceValue { get{ return m_ChanceValue;}}

private int m_Imoney;
 public int Imoney { get{ return m_Imoney;}}

private int m_MaxValue;
 public int MaxValue { get{ return m_MaxValue;}}

private int m_PlayerMoney;
 public int PlayerMoney { get{ return m_PlayerMoney;}}

private int m_Star1ChanceValue;
 public int Star1ChanceValue { get{ return m_Star1ChanceValue;}}

private int m_Star2ChanceValue;
 public int Star2ChanceValue { get{ return m_Star2ChanceValue;}}

private int m_Star3ChanceValue;
 public int Star3ChanceValue { get{ return m_Star3ChanceValue;}}

private int m_Star4ChanceValue;
 public int Star4ChanceValue { get{ return m_Star4ChanceValue;}}

private int m_Star5ChanceValue;
 public int Star5ChanceValue { get{ return m_Star5ChanceValue;}}

private int m_Star6ChanceValue;
 public int Star6ChanceValue { get{ return m_Star6ChanceValue;}}

private int m_Star7ChanceValue;
 public int Star7ChanceValue { get{ return m_Star7ChanceValue;}}

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
 Tab_GambleChancevalue _values = new Tab_GambleChancevalue();
 _values.m_ChanceValue =  Convert.ToInt32(valuesList[(int)_ID.ID_CHANCEVALUE] as string);
_values.m_Imoney =  Convert.ToInt32(valuesList[(int)_ID.ID_IMONEY] as string);
_values.m_MaxValue =  Convert.ToInt32(valuesList[(int)_ID.ID_MAXVALUE] as string);
_values.m_PlayerMoney =  Convert.ToInt32(valuesList[(int)_ID.ID_PLAYER_MONEY] as string);
_values.m_Star1ChanceValue =  Convert.ToInt32(valuesList[(int)_ID.ID_STAR1_CHANCEVALUE] as string);
_values.m_Star2ChanceValue =  Convert.ToInt32(valuesList[(int)_ID.ID_STAR2_CHANCEVALUE] as string);
_values.m_Star3ChanceValue =  Convert.ToInt32(valuesList[(int)_ID.ID_STAR3_CHANCEVALUE] as string);
_values.m_Star4ChanceValue =  Convert.ToInt32(valuesList[(int)_ID.ID_STAR4_CHANCEVALUE] as string);
_values.m_Star5ChanceValue =  Convert.ToInt32(valuesList[(int)_ID.ID_STAR5_CHANCEVALUE] as string);
_values.m_Star6ChanceValue =  Convert.ToInt32(valuesList[(int)_ID.ID_STAR6_CHANCEVALUE] as string);
_values.m_Star7ChanceValue =  Convert.ToInt32(valuesList[(int)_ID.ID_STAR7_CHANCEVALUE] as string);

 _hash.Add(nKey,_values); }


}
}

