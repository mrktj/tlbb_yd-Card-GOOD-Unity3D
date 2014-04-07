//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Skillupdate implements ITableOperate {
 public final static String TAB_FILE_DATA = "/skillupdate.txt";
 public static final int ID_STAR = 0 ;
public static final int ID_CHANCE = 1 ;
public static final int ID_SKILL_CHANCE1 = 2 ;
public static final int ID_SKILL_CHANCE2 = 3 ;
public static final int ID_SKILL_CHANCE3 = 4 ;
public static final int ID_SKILL_CHANCE4 = 5 ;
public static final int ID_SKILL_CHANCE5 = 6 ;
public static final int ID_SKILL_CHANCE6 = 7 ;
public static final int MAX_RECORD = 8 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Chance;
 public int GetChance() { return m_Chance;}

private int m_SkillChance[] = new int[6];
 public int GetSkillChancebyIndex(int idx) {
 if(idx>=0 && idx<6) return m_SkillChance[idx];
 return -1;
 }

private int m_Star;
 public int GetStar() { return m_Star;}

public boolean LoadTable(HashMap<Integer, Table_Skillupdate> _tab) throws TableException
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
 HashMap<Integer, Table_Skillupdate> _hash = (HashMap<Integer, Table_Skillupdate> )obj;
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
 Table_Skillupdate _values = new Table_Skillupdate();
 _values.m_Chance =  Integer.parseInt((String)valuesList.get(ID_CHANCE));
_values.m_SkillChance [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_SKILL_CHANCE1));
_values.m_SkillChance [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_SKILL_CHANCE2));
_values.m_SkillChance [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_SKILL_CHANCE3));
_values.m_SkillChance [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_SKILL_CHANCE4));
_values.m_SkillChance [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_SKILL_CHANCE5));
_values.m_SkillChance [ 5 ] =  Integer.parseInt((String)valuesList.get(ID_SKILL_CHANCE6));
_values.m_Star =  Integer.parseInt((String)valuesList.get(ID_STAR));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

