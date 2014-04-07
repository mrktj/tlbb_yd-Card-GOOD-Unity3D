//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_SkillDisplay implements ITableOperate {
 public final static String TAB_FILE_DATA = "/skill_display.txt";
 public static final int ID_NAME = 0 ;
public static final int ID_DESCRIBE = 1 ;
public static final int ID_ATTACK_ANIM = 2 ;
public static final int ID_ATTACK_LIB = 3 ;
public static final int ID_ATTACK_ANIM_TYPE = 4 ;
public static final int ID_PROJECTILE_ANIM = 5 ;
public static final int ID_PROJECTILE_LIB = 6 ;
public static final int ID_PROJECTILE_ANIM_TYPE = 7 ;
public static final int ID_FLY_TIME = 8 ;
public static final int ID_BEHIT_ANIM = 9 ;
public static final int ID_BEHIT_ANIM_TIME = 10 ;
public static final int ID_BEHIT_LIB = 11 ;
public static final int ID_BEHIT_ANIM_TYPE = 12 ;
public static final int ID_BEHIT_ANIM_SELF = 13 ;
public static final int ID_BEHIT_ANIM_FLIP = 14 ;
public static final int ID_NUM_DELAY = 15 ;
public static final int ID_MUSIC_DELAY = 16 ;
public static final int ID_MUSIC = 17 ;
public static final int ID_SKILL_NAME = 18 ;
public static final int MAX_RECORD = 19 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_AttackAnim;
 public String GetAttackAnim() { return m_AttackAnim;}

private int m_AttackAnimType;
 public int GetAttackAnimType() { return m_AttackAnimType;}

private String m_AttackLib;
 public String GetAttackLib() { return m_AttackLib;}

private String m_BehitAnim;
 public String GetBehitAnim() { return m_BehitAnim;}

private int m_BehitAnimFlip;
 public int GetBehitAnimFlip() { return m_BehitAnimFlip;}

private String m_BehitAnimSelf;
 public String GetBehitAnimSelf() { return m_BehitAnimSelf;}

private int m_BehitAnimTime;
 public int GetBehitAnimTime() { return m_BehitAnimTime;}

private int m_BehitAnimType;
 public int GetBehitAnimType() { return m_BehitAnimType;}

private String m_BehitLib;
 public String GetBehitLib() { return m_BehitLib;}

private int m_Describe;
 public int GetDescribe() { return m_Describe;}

private int m_FlyTime;
 public int GetFlyTime() { return m_FlyTime;}

private String m_Music;
 public String GetMusic() { return m_Music;}

private int m_MusicDelay;
 public int GetMusicDelay() { return m_MusicDelay;}

private int m_Name;
 public int GetName() { return m_Name;}

private int m_NumDelay;
 public int GetNumDelay() { return m_NumDelay;}

private String m_ProjectileAnim;
 public String GetProjectileAnim() { return m_ProjectileAnim;}

private int m_ProjectileAnimType;
 public int GetProjectileAnimType() { return m_ProjectileAnimType;}

private String m_ProjectileLib;
 public String GetProjectileLib() { return m_ProjectileLib;}

private String m_SkillName;
 public String GetSkillName() { return m_SkillName;}

public boolean LoadTable(HashMap<Integer, Table_SkillDisplay> _tab) throws TableException
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
 HashMap<Integer, Table_SkillDisplay> _hash = (HashMap<Integer, Table_SkillDisplay> )obj;
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
 Table_SkillDisplay _values = new Table_SkillDisplay();
 _values.m_AttackAnim =  (String)valuesList.get(ID_ATTACK_ANIM);
_values.m_AttackAnimType =  Integer.parseInt((String)valuesList.get(ID_ATTACK_ANIM_TYPE));
_values.m_AttackLib =  (String)valuesList.get(ID_ATTACK_LIB);
_values.m_BehitAnim =  (String)valuesList.get(ID_BEHIT_ANIM);
_values.m_BehitAnimFlip =  Integer.parseInt((String)valuesList.get(ID_BEHIT_ANIM_FLIP));
_values.m_BehitAnimSelf =  (String)valuesList.get(ID_BEHIT_ANIM_SELF);
_values.m_BehitAnimTime =  Integer.parseInt((String)valuesList.get(ID_BEHIT_ANIM_TIME));
_values.m_BehitAnimType =  Integer.parseInt((String)valuesList.get(ID_BEHIT_ANIM_TYPE));
_values.m_BehitLib =  (String)valuesList.get(ID_BEHIT_LIB);
_values.m_Describe =  Integer.parseInt((String)valuesList.get(ID_DESCRIBE));
_values.m_FlyTime =  Integer.parseInt((String)valuesList.get(ID_FLY_TIME));
_values.m_Music =  (String)valuesList.get(ID_MUSIC);
_values.m_MusicDelay =  Integer.parseInt((String)valuesList.get(ID_MUSIC_DELAY));
_values.m_Name =  Integer.parseInt((String)valuesList.get(ID_NAME));
_values.m_NumDelay =  Integer.parseInt((String)valuesList.get(ID_NUM_DELAY));
_values.m_ProjectileAnim =  (String)valuesList.get(ID_PROJECTILE_ANIM);
_values.m_ProjectileAnimType =  Integer.parseInt((String)valuesList.get(ID_PROJECTILE_ANIM_TYPE));
_values.m_ProjectileLib =  (String)valuesList.get(ID_PROJECTILE_LIB);
_values.m_SkillName =  (String)valuesList.get(ID_SKILL_NAME);

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

