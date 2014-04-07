//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Fengshui : ITableOperate
{ private const string TAB_FILE_DATA = "fengshui.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_NAME,
ID_ICON,
ID_EFFECT_DESC,
ID_ACTIVE_CLAIM,
ID_NEXTLEV_CLAIM,
ID_TYPE,
ID_NUMBER,
ID_EFFECTAREA,
ID_EFFECTTYPE_1,
ID_EFFECTDATA_1,
ID_EFFECTTYPE_2,
ID_EFFECTDATA_2,
ID_ACTIVATION_1,
ID_ACTIVATION_2,
ID_ACTIVATION_3,
ID_ACTIVATION_4,
ID_LEVELITEMID,
ID_LEVELITEMCOUNT,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int[] m_Activation = new int[4];
 public int GetActivationbyIndex(int idx) {
 if(idx>=0 && idx<4) return m_Activation[idx];
 return -1;
 }

private int m_ActiveClaim;
 public int ActiveClaim { get{ return m_ActiveClaim;}}

private int m_EffectArea;
 public int EffectArea { get{ return m_EffectArea;}}

private int[] m_EffectData = new int[2];
 public int GetEffectDatabyIndex(int idx) {
 if(idx>=0 && idx<2) return m_EffectData[idx];
 return -1;
 }

private int[] m_EffectType = new int[2];
 public int GetEffectTypebyIndex(int idx) {
 if(idx>=0 && idx<2) return m_EffectType[idx];
 return -1;
 }

private int m_EffectDesc;
 public int EffectDesc { get{ return m_EffectDesc;}}

private string m_Icon;
 public string Icon { get{ return m_Icon;}}

private int m_LevelItemCount;
 public int LevelItemCount { get{ return m_LevelItemCount;}}

private int m_LevelItemId;
 public int LevelItemId { get{ return m_LevelItemId;}}

private string m_Name;
 public string Name { get{ return m_Name;}}

private int m_NextlevClaim;
 public int NextlevClaim { get{ return m_NextlevClaim;}}

private int m_Number;
 public int Number { get{ return m_Number;}}

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
 Tab_Fengshui _values = new Tab_Fengshui();
 _values.m_Activation [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_ACTIVATION_1] as string);
_values.m_Activation [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_ACTIVATION_2] as string);
_values.m_Activation [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_ACTIVATION_3] as string);
_values.m_Activation [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_ACTIVATION_4] as string);
_values.m_ActiveClaim =  Convert.ToInt32(valuesList[(int)_ID.ID_ACTIVE_CLAIM] as string);
_values.m_EffectArea =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECTAREA] as string);
_values.m_EffectData [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECTDATA_1] as string);
_values.m_EffectData [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECTDATA_2] as string);
_values.m_EffectType [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECTTYPE_1] as string);
_values.m_EffectType [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECTTYPE_2] as string);
_values.m_EffectDesc =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECT_DESC] as string);
_values.m_Icon =  valuesList[(int)_ID.ID_ICON] as string;
_values.m_LevelItemCount =  Convert.ToInt32(valuesList[(int)_ID.ID_LEVELITEMCOUNT] as string);
_values.m_LevelItemId =  Convert.ToInt32(valuesList[(int)_ID.ID_LEVELITEMID] as string);
_values.m_Name =  valuesList[(int)_ID.ID_NAME] as string;
_values.m_NextlevClaim =  Convert.ToInt32(valuesList[(int)_ID.ID_NEXTLEV_CLAIM] as string);
_values.m_Number =  Convert.ToInt32(valuesList[(int)_ID.ID_NUMBER] as string);
_values.m_Type =  Convert.ToInt32(valuesList[(int)_ID.ID_TYPE] as string);

 _hash.Add(nKey,_values); }


}
}

