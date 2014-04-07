//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Skill implements ITableOperate {
 public final static String TAB_FILE_DATA = "/skill.txt";
 public static final int ID_NAME = 0 ;
public static final int ID_EFFECT = 1 ;
public static final int ID_SKILL_LEVEL = 2 ;
public static final int ID_SKILL_MAXLEVEL = 3 ;
public static final int ID_SKILL_TYPE = 4 ;
public static final int ID_FIRST_RELEASE = 5 ;
public static final int ID_CD_ROUND = 6 ;
public static final int ID_LOGIC_ID = 7 ;
public static final int ID_SELECT_SCRIPT = 8 ;
public static final int ID_CAMP = 9 ;
public static final int ID_ILLUSTRATION = 10 ;
public static final int ID_DAMAGE_PER = 11 ;
public static final int ID_DAMAGE_VALUE = 12 ;
public static final int ID_UPDATE_RULE = 13 ;
public static final int ID_BUFF_RATE = 14 ;
public static final int ID_LV1_BF_ID1 = 15 ;
public static final int ID_LV1_BF_PROB1 = 16 ;
public static final int ID_LV1_BF_ID2 = 17 ;
public static final int ID_LV1_BF_PROB2 = 18 ;
public static final int ID_LV1_BF_ID3 = 19 ;
public static final int ID_LV1_BF_PROB3 = 20 ;
public static final int MAX_RECORD = 21 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_BuffRate;
 public int GetBuffRate() { return m_BuffRate;}

private int m_Camp;
 public int GetCamp() { return m_Camp;}

private int m_CdRound;
 public int GetCdRound() { return m_CdRound;}

private int m_DamagePer;
 public int GetDamagePer() { return m_DamagePer;}

private int m_DamageValue;
 public int GetDamageValue() { return m_DamageValue;}

private int m_Effect;
 public int GetEffect() { return m_Effect;}

private int m_FirstRelease;
 public int GetFirstRelease() { return m_FirstRelease;}

private int m_Illustration;
 public int GetIllustration() { return m_Illustration;}

private int m_LogicId;
 public int GetLogicId() { return m_LogicId;}

private int m_Lv1BfId[] = new int[3];
 public int GetLv1BfIdbyIndex(int idx) {
 if(idx>=0 && idx<3) return m_Lv1BfId[idx];
 return -1;
 }

private int m_Lv1BfProb[] = new int[3];
 public int GetLv1BfProbbyIndex(int idx) {
 if(idx>=0 && idx<3) return m_Lv1BfProb[idx];
 return -1;
 }

private String m_Name;
 public String GetName() { return m_Name;}

private int m_SelectScript;
 public int GetSelectScript() { return m_SelectScript;}

private int m_SkillLevel;
 public int GetSkillLevel() { return m_SkillLevel;}

private int m_SkillMaxlevel;
 public int GetSkillMaxlevel() { return m_SkillMaxlevel;}

private int m_SkillType;
 public int GetSkillType() { return m_SkillType;}

private int m_UpdateRule;
 public int GetUpdateRule() { return m_UpdateRule;}

public boolean LoadTable(HashMap<Integer, Table_Skill> _tab) throws TableException
 {
 if(!TableManager.ReaderPList(GetInstanceFile(),this,_tab))
 {
 throw TableException.ErrorReader("Load File{0} Fail!!!",GetInstanceFile());
 }
 return true;
 }

 @Override
 public void SerializableTable(ArrayList<String> valuesList,String skey,Object obj) throws TableException {
 @SuppressWarnings("unchecked")
 HashMap<Integer, Table_Skill> _hash = (HashMap<Integer, Table_Skill> )obj;
 if (skey.isEmpty())
 {
 throw TableException.ErrorReader("Read File"+GetInstanceFile()+" as key is Empty Fail!!!");
 }

 if (MAX_RECORD!=valuesList.size())
 {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as CodeSize:"+MAX_RECORD+" not Equal DataSize:"+valuesList.size());
 }
 try { 
 Integer nKey = Integer.parseInt(skey);
 Table_Skill _values = new Table_Skill();
 _values.m_BuffRate =  Integer.parseInt((String)valuesList.get(ID_BUFF_RATE));
_values.m_Camp =  Integer.parseInt((String)valuesList.get(ID_CAMP));
_values.m_CdRound =  Integer.parseInt((String)valuesList.get(ID_CD_ROUND));
_values.m_DamagePer =  Integer.parseInt((String)valuesList.get(ID_DAMAGE_PER));
_values.m_DamageValue =  Integer.parseInt((String)valuesList.get(ID_DAMAGE_VALUE));
_values.m_Effect =  Integer.parseInt((String)valuesList.get(ID_EFFECT));
_values.m_FirstRelease =  Integer.parseInt((String)valuesList.get(ID_FIRST_RELEASE));
_values.m_Illustration =  Integer.parseInt((String)valuesList.get(ID_ILLUSTRATION));
_values.m_LogicId =  Integer.parseInt((String)valuesList.get(ID_LOGIC_ID));
_values.m_Lv1BfId [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_LV1_BF_ID1));
_values.m_Lv1BfId [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_LV1_BF_ID2));
_values.m_Lv1BfId [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_LV1_BF_ID3));
_values.m_Lv1BfProb [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_LV1_BF_PROB1));
_values.m_Lv1BfProb [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_LV1_BF_PROB2));
_values.m_Lv1BfProb [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_LV1_BF_PROB3));
_values.m_Name =  (String)valuesList.get(ID_NAME);
_values.m_SelectScript =  Integer.parseInt((String)valuesList.get(ID_SELECT_SCRIPT));
_values.m_SkillLevel =  Integer.parseInt((String)valuesList.get(ID_SKILL_LEVEL));
_values.m_SkillMaxlevel =  Integer.parseInt((String)valuesList.get(ID_SKILL_MAXLEVEL));
_values.m_SkillType =  Integer.parseInt((String)valuesList.get(ID_SKILL_TYPE));
_values.m_UpdateRule =  Integer.parseInt((String)valuesList.get(ID_UPDATE_RULE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

