//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_SkillEffect : ITableOperate
{ private const string TAB_FILE_DATA = "skill_effect.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_EFFECT_TYPE,
ID_ATTACK_ANIM,
ID_ATTACK_LIB,
ID_PROJECTILE_ANIM,
ID_PROJECTILE_LIB,
ID_FLY_TIME,
ID_BEHIT_ANIM,
ID_BEHIT_LIB,
ID_MUSIC,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private string m_AttackAnim;
 public string AttackAnim { get{ return m_AttackAnim;}}

private string m_AttackLib;
 public string AttackLib { get{ return m_AttackLib;}}

private string m_BehitAnim;
 public string BehitAnim { get{ return m_BehitAnim;}}

private string m_BehitLib;
 public string BehitLib { get{ return m_BehitLib;}}

private int m_EffectType;
 public int EffectType { get{ return m_EffectType;}}

private int m_FlyTime;
 public int FlyTime { get{ return m_FlyTime;}}

private string m_Music;
 public string Music { get{ return m_Music;}}

private string m_ProjectileAnim;
 public string ProjectileAnim { get{ return m_ProjectileAnim;}}

private string m_ProjectileLib;
 public string ProjectileLib { get{ return m_ProjectileLib;}}

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
 Tab_SkillEffect _values = new Tab_SkillEffect();
 _values.m_AttackAnim =  valuesList[(int)_ID.ID_ATTACK_ANIM] as string;
_values.m_AttackLib =  valuesList[(int)_ID.ID_ATTACK_LIB] as string;
_values.m_BehitAnim =  valuesList[(int)_ID.ID_BEHIT_ANIM] as string;
_values.m_BehitLib =  valuesList[(int)_ID.ID_BEHIT_LIB] as string;
_values.m_EffectType =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECT_TYPE] as string);
_values.m_FlyTime =  Convert.ToInt32(valuesList[(int)_ID.ID_FLY_TIME] as string);
_values.m_Music =  valuesList[(int)_ID.ID_MUSIC] as string;
_values.m_ProjectileAnim =  valuesList[(int)_ID.ID_PROJECTILE_ANIM] as string;
_values.m_ProjectileLib =  valuesList[(int)_ID.ID_PROJECTILE_LIB] as string;

 _hash.Add(nKey,_values); }


}
}

