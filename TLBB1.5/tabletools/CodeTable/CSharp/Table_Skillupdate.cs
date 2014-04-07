//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Skillupdate : ITableOperate
{ private const string TAB_FILE_DATA = "skillupdate.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_STAR,
ID_CHANCE,
ID_SKILL_CHANCE1,
ID_SKILL_CHANCE2,
ID_SKILL_CHANCE3,
ID_SKILL_CHANCE4,
ID_SKILL_CHANCE5,
ID_SKILL_CHANCE6,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Chance;
 public int Chance { get{ return m_Chance;}}

private int[] m_SkillChance = new int[6];
 public int GetSkillChancebyIndex(int idx) {
 if(idx>=0 && idx<6) return m_SkillChance[idx];
 return -1;
 }

private int m_Star;
 public int Star { get{ return m_Star;}}

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
 Tab_Skillupdate _values = new Tab_Skillupdate();
 _values.m_Chance =  Convert.ToInt32(valuesList[(int)_ID.ID_CHANCE] as string);
_values.m_SkillChance [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_CHANCE1] as string);
_values.m_SkillChance [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_CHANCE2] as string);
_values.m_SkillChance [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_CHANCE3] as string);
_values.m_SkillChance [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_CHANCE4] as string);
_values.m_SkillChance [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_CHANCE5] as string);
_values.m_SkillChance [ 5 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_CHANCE6] as string);
_values.m_Star =  Convert.ToInt32(valuesList[(int)_ID.ID_STAR] as string);

 _hash.Add(nKey,_values); }


}
}

