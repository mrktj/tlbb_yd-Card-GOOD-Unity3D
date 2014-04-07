//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Skillgroup implements ITableOperate {
 public final static String TAB_FILE_DATA = "/skillgroup.txt";
 public static final int ID_SKILL_LEVEL1 = 0 ;
public static final int ID_SKILL_LEVEL2 = 1 ;
public static final int ID_SKILL_LEVEL3 = 2 ;
public static final int ID_SKILL_LEVEL4 = 3 ;
public static final int ID_SKILL_LEVEL5 = 4 ;
public static final int ID_SKILL_LEVEL6 = 5 ;
public static final int MAX_RECORD = 6 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_SkillLevel[] = new int[6];
 public int GetSkillLevelbyIndex(int idx) {
 if(idx>=0 && idx<6) return m_SkillLevel[idx];
 return -1;
 }

public boolean LoadTable(HashMap<Integer, Table_Skillgroup> _tab) throws TableException
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
 HashMap<Integer, Table_Skillgroup> _hash = (HashMap<Integer, Table_Skillgroup> )obj;
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
 Table_Skillgroup _values = new Table_Skillgroup();
 _values.m_SkillLevel [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_SKILL_LEVEL1));
_values.m_SkillLevel [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_SKILL_LEVEL2));
_values.m_SkillLevel [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_SKILL_LEVEL3));
_values.m_SkillLevel [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_SKILL_LEVEL4));
_values.m_SkillLevel [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_SKILL_LEVEL5));
_values.m_SkillLevel [ 5 ] =  Integer.parseInt((String)valuesList.get(ID_SKILL_LEVEL6));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

