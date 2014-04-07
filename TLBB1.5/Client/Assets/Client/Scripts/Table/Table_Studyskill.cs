//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Studyskill : ITableOperate
{ private const string TAB_FILE_DATA = "studyskill.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_SKILLGROUP,
ID_SKILLLEVEL,
ID_SKILLNEXTLEVEL,
ID_SKILLHIGHLEVEL,
ID_SKILLQUALITY,
ID_EXPERIENCE,
ID_SKILLEXPERIENCE,
ID_COSTMONEY,
ID_SKILLNAME,
ID_SKILLDES,
ID_LEADERNUM,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_CostMoney;
 public int CostMoney { get{ return m_CostMoney;}}

private int m_Experience;
 public int Experience { get{ return m_Experience;}}

private int m_LeaderNum;
 public int LeaderNum { get{ return m_LeaderNum;}}

private string m_SkillDes;
 public string SkillDes { get{ return m_SkillDes;}}

private int m_SkillExperience;
 public int SkillExperience { get{ return m_SkillExperience;}}

private int m_SkillHighLevel;
 public int SkillHighLevel { get{ return m_SkillHighLevel;}}

private int m_SkillLevel;
 public int SkillLevel { get{ return m_SkillLevel;}}

private string m_SkillName;
 public string SkillName { get{ return m_SkillName;}}

private int m_SkillNextLevel;
 public int SkillNextLevel { get{ return m_SkillNextLevel;}}

private int m_SkillQuality;
 public int SkillQuality { get{ return m_SkillQuality;}}

private int m_Skillgroup;
 public int Skillgroup { get{ return m_Skillgroup;}}

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
 Tab_Studyskill _values = new Tab_Studyskill();
 _values.m_CostMoney =  Convert.ToInt32(valuesList[(int)_ID.ID_COSTMONEY] as string);
_values.m_Experience =  Convert.ToInt32(valuesList[(int)_ID.ID_EXPERIENCE] as string);
_values.m_LeaderNum =  Convert.ToInt32(valuesList[(int)_ID.ID_LEADERNUM] as string);
_values.m_SkillDes =  valuesList[(int)_ID.ID_SKILLDES] as string;
_values.m_SkillExperience =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILLEXPERIENCE] as string);
_values.m_SkillHighLevel =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILLHIGHLEVEL] as string);
_values.m_SkillLevel =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILLLEVEL] as string);
_values.m_SkillName =  valuesList[(int)_ID.ID_SKILLNAME] as string;
_values.m_SkillNextLevel =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILLNEXTLEVEL] as string);
_values.m_SkillQuality =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILLQUALITY] as string);
_values.m_Skillgroup =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILLGROUP] as string);

 _hash.Add(nKey,_values); }


}
}

