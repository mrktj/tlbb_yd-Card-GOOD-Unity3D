//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Skillgroup : ITableOperate
{ private const string TAB_FILE_DATA = "skillgroup.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_SKILL_LEVEL1,
ID_SKILL_LEVEL2,
ID_SKILL_LEVEL3,
ID_SKILL_LEVEL4,
ID_SKILL_LEVEL5,
ID_SKILL_LEVEL6,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int[] m_SkillLevel = new int[6];
 public int GetSkillLevelbyIndex(int idx) {
 if(idx>=0 && idx<6) return m_SkillLevel[idx];
 return -1;
 }

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
 Tab_Skillgroup _values = new Tab_Skillgroup();
 _values.m_SkillLevel [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_LEVEL1] as string);
_values.m_SkillLevel [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_LEVEL2] as string);
_values.m_SkillLevel [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_LEVEL3] as string);
_values.m_SkillLevel [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_LEVEL4] as string);
_values.m_SkillLevel [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_LEVEL5] as string);
_values.m_SkillLevel [ 5 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILL_LEVEL6] as string);

 _hash.Add(nKey,_values); }


}
}

