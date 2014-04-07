//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_BuffEffect : ITableOperate
{ private const string TAB_FILE_DATA = "buff_effect.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_BUFF_TYPE,
ID_EFFECT_TYPE,
ID_EFFECT_POS,
ID_BUFF_ANIM,
ID_BUFF_LIB,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private string m_BuffAnim;
 public string BuffAnim { get{ return m_BuffAnim;}}

private string m_BuffLib;
 public string BuffLib { get{ return m_BuffLib;}}

private int m_BuffType;
 public int BuffType { get{ return m_BuffType;}}

private int m_EffectPos;
 public int EffectPos { get{ return m_EffectPos;}}

private int m_EffectType;
 public int EffectType { get{ return m_EffectType;}}

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
 Tab_BuffEffect _values = new Tab_BuffEffect();
 _values.m_BuffAnim =  valuesList[(int)_ID.ID_BUFF_ANIM] as string;
_values.m_BuffLib =  valuesList[(int)_ID.ID_BUFF_LIB] as string;
_values.m_BuffType =  Convert.ToInt32(valuesList[(int)_ID.ID_BUFF_TYPE] as string);
_values.m_EffectPos =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECT_POS] as string);
_values.m_EffectType =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECT_TYPE] as string);

 _hash.Add(nKey,_values); }


}
}

