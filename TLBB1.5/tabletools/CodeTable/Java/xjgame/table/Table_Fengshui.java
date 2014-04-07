//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Fengshui implements ITableOperate {
 public final static String TAB_FILE_DATA = "/fengshui.txt";
 public static final int ID_NAME = 0 ;
public static final int ID_ICON = 1 ;
public static final int ID_EFFECT_DESC = 2 ;
public static final int ID_ACTIVE_CLAIM = 3 ;
public static final int ID_NEXTLEV_CLAIM = 4 ;
public static final int ID_TYPE = 5 ;
public static final int ID_NUMBER = 6 ;
public static final int ID_EFFECTAREA = 7 ;
public static final int ID_EFFECTTYPE_1 = 8 ;
public static final int ID_EFFECTDATA_1 = 9 ;
public static final int ID_EFFECTTYPE_2 = 10 ;
public static final int ID_EFFECTDATA_2 = 11 ;
public static final int ID_ACTIVATION_1 = 12 ;
public static final int ID_ACTIVATION_2 = 13 ;
public static final int ID_ACTIVATION_3 = 14 ;
public static final int ID_ACTIVATION_4 = 15 ;
public static final int ID_LEVELITEMID = 16 ;
public static final int ID_LEVELITEMCOUNT = 17 ;
public static final int MAX_RECORD = 18 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Activation[] = new int[4];
 public int GetActivationbyIndex(int idx) {
 if(idx>=0 && idx<4) return m_Activation[idx];
 return -1;
 }

private int m_ActiveClaim;
 public int GetActiveClaim() { return m_ActiveClaim;}

private int m_EffectArea;
 public int GetEffectArea() { return m_EffectArea;}

private int m_EffectData[] = new int[2];
 public int GetEffectDatabyIndex(int idx) {
 if(idx>=0 && idx<2) return m_EffectData[idx];
 return -1;
 }

private int m_EffectType[] = new int[2];
 public int GetEffectTypebyIndex(int idx) {
 if(idx>=0 && idx<2) return m_EffectType[idx];
 return -1;
 }

private int m_EffectDesc;
 public int GetEffectDesc() { return m_EffectDesc;}

private String m_Icon;
 public String GetIcon() { return m_Icon;}

private int m_LevelItemCount;
 public int GetLevelItemCount() { return m_LevelItemCount;}

private int m_LevelItemId;
 public int GetLevelItemId() { return m_LevelItemId;}

private String m_Name;
 public String GetName() { return m_Name;}

private int m_NextlevClaim;
 public int GetNextlevClaim() { return m_NextlevClaim;}

private int m_Number;
 public int GetNumber() { return m_Number;}

private int m_Type;
 public int GetType() { return m_Type;}

public boolean LoadTable(HashMap<Integer, Table_Fengshui> _tab) throws TableException
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
 HashMap<Integer, Table_Fengshui> _hash = (HashMap<Integer, Table_Fengshui> )obj;
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
 Table_Fengshui _values = new Table_Fengshui();
 _values.m_Activation [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_ACTIVATION_1));
_values.m_Activation [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_ACTIVATION_2));
_values.m_Activation [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_ACTIVATION_3));
_values.m_Activation [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_ACTIVATION_4));
_values.m_ActiveClaim =  Integer.parseInt((String)valuesList.get(ID_ACTIVE_CLAIM));
_values.m_EffectArea =  Integer.parseInt((String)valuesList.get(ID_EFFECTAREA));
_values.m_EffectData [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_EFFECTDATA_1));
_values.m_EffectData [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_EFFECTDATA_2));
_values.m_EffectType [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_EFFECTTYPE_1));
_values.m_EffectType [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_EFFECTTYPE_2));
_values.m_EffectDesc =  Integer.parseInt((String)valuesList.get(ID_EFFECT_DESC));
_values.m_Icon =  (String)valuesList.get(ID_ICON);
_values.m_LevelItemCount =  Integer.parseInt((String)valuesList.get(ID_LEVELITEMCOUNT));
_values.m_LevelItemId =  Integer.parseInt((String)valuesList.get(ID_LEVELITEMID));
_values.m_Name =  (String)valuesList.get(ID_NAME);
_values.m_NextlevClaim =  Integer.parseInt((String)valuesList.get(ID_NEXTLEV_CLAIM));
_values.m_Number =  Integer.parseInt((String)valuesList.get(ID_NUMBER));
_values.m_Type =  Integer.parseInt((String)valuesList.get(ID_TYPE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

