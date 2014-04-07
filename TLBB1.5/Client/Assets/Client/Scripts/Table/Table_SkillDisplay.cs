//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_SkillDisplay : ITableOperate
{ private const string TAB_FILE_DATA = "skill_display.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_NAME,
ID_DESCRIBE,
ID_ATTACK_ANIM,
ID_ATTACK_LIB,
ID_ATTACK_ANIM_TYPE,
ID_PROJECTILE_ANIM,
ID_PROJECTILE_LIB,
ID_PROJECTILE_ANIM_TYPE,
ID_FLY_TIME,
ID_BEHIT_ANIM,
ID_BEHIT_ANIM_TIME,
ID_BEHIT_LIB,
ID_BEHIT_ANIM_TYPE,
ID_BEHIT_ANIM_SELF,
ID_BEHIT_ANIM_FLIP,
ID_NUM_DELAY,
ID_MUSIC_DELAY,
ID_MUSIC,
ID_SKILL_NAME,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private string m_AttackAnim;
 public string AttackAnim { get{ return m_AttackAnim;}}

private int m_AttackAnimType;
 public int AttackAnimType { get{ return m_AttackAnimType;}}

private string m_AttackLib;
 public string AttackLib { get{ return m_AttackLib;}}

private string m_BehitAnim;
 public string BehitAnim { get{ return m_BehitAnim;}}

private int m_BehitAnimFlip;
 public int BehitAnimFlip { get{ return m_BehitAnimFlip;}}

private string m_BehitAnimSelf;
 public string BehitAnimSelf { get{ return m_BehitAnimSelf;}}

private int m_BehitAnimTime;
 public int BehitAnimTime { get{ return m_BehitAnimTime;}}

private int m_BehitAnimType;
 public int BehitAnimType { get{ return m_BehitAnimType;}}

private string m_BehitLib;
 public string BehitLib { get{ return m_BehitLib;}}

private int m_Describe;
 public int Describe { get{ return m_Describe;}}

private int m_FlyTime;
 public int FlyTime { get{ return m_FlyTime;}}

private string m_Music;
 public string Music { get{ return m_Music;}}

private int m_MusicDelay;
 public int MusicDelay { get{ return m_MusicDelay;}}

private int m_Name;
 public int Name { get{ return m_Name;}}

private int m_NumDelay;
 public int NumDelay { get{ return m_NumDelay;}}

private string m_ProjectileAnim;
 public string ProjectileAnim { get{ return m_ProjectileAnim;}}

private int m_ProjectileAnimType;
 public int ProjectileAnimType { get{ return m_ProjectileAnimType;}}

private string m_ProjectileLib;
 public string ProjectileLib { get{ return m_ProjectileLib;}}

private string m_SkillName;
 public string SkillName { get{ return m_SkillName;}}

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
 Tab_SkillDisplay _values = new Tab_SkillDisplay();
 _values.m_AttackAnim =  valuesList[(int)_ID.ID_ATTACK_ANIM] as string;
_values.m_AttackAnimType =  Convert.ToInt32(valuesList[(int)_ID.ID_ATTACK_ANIM_TYPE] as string);
_values.m_AttackLib =  valuesList[(int)_ID.ID_ATTACK_LIB] as string;
_values.m_BehitAnim =  valuesList[(int)_ID.ID_BEHIT_ANIM] as string;
_values.m_BehitAnimFlip =  Convert.ToInt32(valuesList[(int)_ID.ID_BEHIT_ANIM_FLIP] as string);
_values.m_BehitAnimSelf =  valuesList[(int)_ID.ID_BEHIT_ANIM_SELF] as string;
_values.m_BehitAnimTime =  Convert.ToInt32(valuesList[(int)_ID.ID_BEHIT_ANIM_TIME] as string);
_values.m_BehitAnimType =  Convert.ToInt32(valuesList[(int)_ID.ID_BEHIT_ANIM_TYPE] as string);
_values.m_BehitLib =  valuesList[(int)_ID.ID_BEHIT_LIB] as string;
_values.m_Describe =  Convert.ToInt32(valuesList[(int)_ID.ID_DESCRIBE] as string);
_values.m_FlyTime =  Convert.ToInt32(valuesList[(int)_ID.ID_FLY_TIME] as string);
_values.m_Music =  valuesList[(int)_ID.ID_MUSIC] as string;
_values.m_MusicDelay =  Convert.ToInt32(valuesList[(int)_ID.ID_MUSIC_DELAY] as string);
_values.m_Name =  Convert.ToInt32(valuesList[(int)_ID.ID_NAME] as string);
_values.m_NumDelay =  Convert.ToInt32(valuesList[(int)_ID.ID_NUM_DELAY] as string);
_values.m_ProjectileAnim =  valuesList[(int)_ID.ID_PROJECTILE_ANIM] as string;
_values.m_ProjectileAnimType =  Convert.ToInt32(valuesList[(int)_ID.ID_PROJECTILE_ANIM_TYPE] as string);
_values.m_ProjectileLib =  valuesList[(int)_ID.ID_PROJECTILE_LIB] as string;
_values.m_SkillName =  valuesList[(int)_ID.ID_SKILL_NAME] as string;

 _hash.Add(nKey,_values); }


}
}

