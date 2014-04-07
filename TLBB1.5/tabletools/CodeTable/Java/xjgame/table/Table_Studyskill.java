//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Studyskill implements ITableOperate {
 public final static String TAB_FILE_DATA = "/studyskill.txt";
 public static final int ID_SKILLGROUP = 0 ;
public static final int ID_SKILLLEVEL = 1 ;
public static final int ID_SKILLNEXTLEVEL = 2 ;
public static final int ID_SKILLHIGHLEVEL = 3 ;
public static final int ID_SKILLQUALITY = 4 ;
public static final int ID_EXPERIENCE = 5 ;
public static final int ID_SKILLEXPERIENCE = 6 ;
public static final int ID_COSTMONEY = 7 ;
public static final int ID_SKILLNAME = 8 ;
public static final int ID_SKILLDES = 9 ;
public static final int ID_LEADERNUM = 10 ;
public static final int MAX_RECORD = 11 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_CostMoney;
 public int GetCostMoney() { return m_CostMoney;}

private int m_Experience;
 public int GetExperience() { return m_Experience;}

private int m_LeaderNum;
 public int GetLeaderNum() { return m_LeaderNum;}

private String m_SkillDes;
 public String GetSkillDes() { return m_SkillDes;}

private int m_SkillExperience;
 public int GetSkillExperience() { return m_SkillExperience;}

private int m_SkillHighLevel;
 public int GetSkillHighLevel() { return m_SkillHighLevel;}

private int m_SkillLevel;
 public int GetSkillLevel() { return m_SkillLevel;}

private String m_SkillName;
 public String GetSkillName() { return m_SkillName;}

private int m_SkillNextLevel;
 public int GetSkillNextLevel() { return m_SkillNextLevel;}

private int m_SkillQuality;
 public int GetSkillQuality() { return m_SkillQuality;}

private int m_Skillgroup;
 public int GetSkillgroup() { return m_Skillgroup;}

public boolean LoadTable(HashMap<Integer, Table_Studyskill> _tab) throws TableException
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
 HashMap<Integer, Table_Studyskill> _hash = (HashMap<Integer, Table_Studyskill> )obj;
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
 Table_Studyskill _values = new Table_Studyskill();
 _values.m_CostMoney =  Integer.parseInt((String)valuesList.get(ID_COSTMONEY));
_values.m_Experience =  Integer.parseInt((String)valuesList.get(ID_EXPERIENCE));
_values.m_LeaderNum =  Integer.parseInt((String)valuesList.get(ID_LEADERNUM));
_values.m_SkillDes =  (String)valuesList.get(ID_SKILLDES);
_values.m_SkillExperience =  Integer.parseInt((String)valuesList.get(ID_SKILLEXPERIENCE));
_values.m_SkillHighLevel =  Integer.parseInt((String)valuesList.get(ID_SKILLHIGHLEVEL));
_values.m_SkillLevel =  Integer.parseInt((String)valuesList.get(ID_SKILLLEVEL));
_values.m_SkillName =  (String)valuesList.get(ID_SKILLNAME);
_values.m_SkillNextLevel =  Integer.parseInt((String)valuesList.get(ID_SKILLNEXTLEVEL));
_values.m_SkillQuality =  Integer.parseInt((String)valuesList.get(ID_SKILLQUALITY));
_values.m_Skillgroup =  Integer.parseInt((String)valuesList.get(ID_SKILLGROUP));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

