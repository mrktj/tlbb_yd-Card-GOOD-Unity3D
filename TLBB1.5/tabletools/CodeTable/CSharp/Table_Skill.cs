//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Skill : ITableOperate
{ private const string TAB_FILE_DATA = "skill.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_NAME,
ID_EFFECT,
ID_SKILL_LEVEL,
ID_SKILL_MAXLEVEL,
ID_SKILL_TYPE,
ID_FIRST_RELEASE,
ID_CD_ROUND,
ID_LOGIC_ID,
ID_SELECT_SCRIPT,
ID_CAMP,
ID_ILLUSTRATION,
ID_DAMAGE_PER,
ID_DAMAGE_VALUE,
ID_UPDATE_RULE,
ID_BUFF_RATE,
ID_LV1_BF_ID1,
ID_LV1_BF_PROB1,
ID_LV1_BF_ID2,
ID_LV1_BF_PROB2,
ID_LV1_BF_ID3,
ID_LV1_BF_PROB3,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_BuffRate;
 public int BuffRate { get{ return m_BuffRate;}}

private int m_Camp;
 public int Camp { get{ return m_Camp;}}

private int m_CdRound;
 public int CdRound { get{ return m_CdRound;}}

private int m_DamagePer;
 public int DamagePer { get{ return m_DamagePer;}}

private int m_DamageValue;
 public int DamageValue { get{ return m_DamageValue;}}

private int m_Effect;
 public int Effect { get{ return m_Effect;}}

private int m_FirstRelease;
 public int FirstRelease { get{ return m_FirstRelease;}}

private int m_Illustration;
 public int Illustration { get{ return m_Illustration;}}

private int m_LogicId;
 public int LogicId { get{ return m_LogicId;}}

private int[] m_Lv1BfId = new int[3];
 public int GetLv1BfIdbyIndex(int idx) {
 if(idx>=0 && idx<3) return m_Lv1BfId[idx];
 return -1;
 }

private int[] m_Lv1BfProb = new int[3];
 public int GetLv1BfProbbyIndex(int idx) {
 if(idx>=0 && idx<3) return m_Lv1BfProb[idx];
 return -1;
 }

private string m_Name;
 public string Name { get{ return m_Name;}}

private int m_SelectScript;
 public int SelectScript { get{ return m_SelectScript;}}

private int m_SkillLevel;
 public int SkillLevel { get{ return m_SkillLevel;}}

private int m_SkillMaxlevel;
 public int SkillMaxlevel { get{ return m_SkillMaxlevel;}}

private int m_SkillType;
 public int SkillType { get{ return m_SkillType;}}

private int m_UpdateRule;
 public int UpdateRule { get{ return m_UpdateRule;}}

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
 Tab_Skill _values = new Tab_Skill();
 _values.m_BuffRate =  Convert.ToInt32(valuesList[(int)_ID.ID_BUFF_RATE] as string);
_values.m_Camp =  Convert.ToInt32(valuesList[(int)_ID.ID_CAMP] as string);
_values.m_CdRound =  Convert.ToInt32(valuesList[(int)_ID.ID_CD_ROUND] as string);
_values.m_DamagePer =  Convert.ToInt32(valuesList[(int)_ID.ID_DAMAGE_PER] as string);
_values.m_DamageValue =  Convert.ToInt32(valuesList[(int)_ID.ID_DAMAGE_VALUE] as string);
_values.m_Effect =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECT] as string);
_values.m_FirstRelease =  Convert.ToInt32(valuesList[(int)_ID.ID_FIRST_RELEASE] as string);
_values.m_Illustration =  Convert.ToInt32(valuesList[(int)_ID.ID_ILLUSTRATION] as string);
_values.m_LogicId =  Convert.ToInt32(valuesList[(int)_ID.ID_LOGIC_ID] as string);
_values.m_Lv1BfId [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_LV1_BF_ID1] as string);
_values.m_Lv1BfId [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_LV1_BF_ID2] as string);
_values.m_Lv1BfId [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_LV1_BF_ID3] as string);
_values.m_Lv1BfProb [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_LV1_BF_PROB1] as string);
_values.m_Lv1BfProb [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_LV1_BF_PROB2] as string);
_values.m_Lv1BfProb [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_LV1_BF_PROB3] as string);
_values.m_Name =  valuesList[(int)_ID.ID_NAME] as string;
_values.m_SelectScript =  Convert.ToInt32(valuesList[(int)_ID.ID_SELECT_SCRIPT] as string);
_values.m_SkillLevel =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_LEVEL] as string);
_values.m_SkillMaxlevel =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_MAXLEVEL] as string);
_values.m_SkillType =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_TYPE] as string);
_values.m_UpdateRule =  Convert.ToInt32(valuesList[(int)_ID.ID_UPDATE_RULE] as string);

 _hash.Add(nKey,_values); }


}
}

