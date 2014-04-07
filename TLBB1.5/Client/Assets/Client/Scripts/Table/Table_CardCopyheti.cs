//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_CardCopyheti : ITableOperate
{ private const string TAB_FILE_DATA = "card_copyheti.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_CARD_ID_LEFT,
ID_CARD_ID_RIGHT,
ID_SPRITE_LEFT,
ID_SPRITE_RIGHT,
ID_NAME_CARD1,
ID_NAME_CARD2,
ID_HETI_NAME,
ID_HETI_DISCRIB,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_CardIdLeft;
 public int CardIdLeft { get{ return m_CardIdLeft;}}

private int m_CardIdRight;
 public int CardIdRight { get{ return m_CardIdRight;}}

private string m_HetiDiscrib;
 public string HetiDiscrib { get{ return m_HetiDiscrib;}}

private string m_HetiName;
 public string HetiName { get{ return m_HetiName;}}

private string[] m_NameCard = new string[2];
 public string GetNameCardbyIndex(int idx) {
 if(idx>=0 && idx<2) return m_NameCard[idx];
 return "";
 }

private string m_SpriteLeft;
 public string SpriteLeft { get{ return m_SpriteLeft;}}

private string m_SpriteRight;
 public string SpriteRight { get{ return m_SpriteRight;}}

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
 Tab_CardCopyheti _values = new Tab_CardCopyheti();
 _values.m_CardIdLeft =  Convert.ToInt32(valuesList[(int)_ID.ID_CARD_ID_LEFT] as string);
_values.m_CardIdRight =  Convert.ToInt32(valuesList[(int)_ID.ID_CARD_ID_RIGHT] as string);
_values.m_HetiDiscrib =  valuesList[(int)_ID.ID_HETI_DISCRIB] as string;
_values.m_HetiName =  valuesList[(int)_ID.ID_HETI_NAME] as string;
_values.m_NameCard [ 0 ] =  valuesList[(int)_ID.ID_NAME_CARD1] as string;
_values.m_NameCard [ 1 ] =  valuesList[(int)_ID.ID_NAME_CARD2] as string;
_values.m_SpriteLeft =  valuesList[(int)_ID.ID_SPRITE_LEFT] as string;
_values.m_SpriteRight =  valuesList[(int)_ID.ID_SPRITE_RIGHT] as string;

 _hash.Add(nKey,_values); }


}
}

