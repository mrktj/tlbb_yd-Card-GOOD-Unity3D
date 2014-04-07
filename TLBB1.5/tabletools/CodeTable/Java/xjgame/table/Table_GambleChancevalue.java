//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_GambleChancevalue implements ITableOperate {
 public final static String TAB_FILE_DATA = "/gamble_chancevalue.txt";
 public static final int ID_PLAYER_MONEY = 0 ;
public static final int ID_STAR1_CHANCEVALUE = 1 ;
public static final int ID_STAR2_CHANCEVALUE = 2 ;
public static final int ID_STAR3_CHANCEVALUE = 3 ;
public static final int ID_STAR4_CHANCEVALUE = 4 ;
public static final int ID_STAR5_CHANCEVALUE = 5 ;
public static final int ID_STAR6_CHANCEVALUE = 6 ;
public static final int ID_STAR7_CHANCEVALUE = 7 ;
public static final int ID_IMONEY = 8 ;
public static final int ID_CHANCEVALUE = 9 ;
public static final int ID_MAXVALUE = 10 ;
public static final int MAX_RECORD = 11 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_ChanceValue;
 public int GetChanceValue() { return m_ChanceValue;}

private int m_Imoney;
 public int GetImoney() { return m_Imoney;}

private int m_MaxValue;
 public int GetMaxValue() { return m_MaxValue;}

private int m_PlayerMoney;
 public int GetPlayerMoney() { return m_PlayerMoney;}

private int m_Star1ChanceValue;
 public int GetStar1ChanceValue() { return m_Star1ChanceValue;}

private int m_Star2ChanceValue;
 public int GetStar2ChanceValue() { return m_Star2ChanceValue;}

private int m_Star3ChanceValue;
 public int GetStar3ChanceValue() { return m_Star3ChanceValue;}

private int m_Star4ChanceValue;
 public int GetStar4ChanceValue() { return m_Star4ChanceValue;}

private int m_Star5ChanceValue;
 public int GetStar5ChanceValue() { return m_Star5ChanceValue;}

private int m_Star6ChanceValue;
 public int GetStar6ChanceValue() { return m_Star6ChanceValue;}

private int m_Star7ChanceValue;
 public int GetStar7ChanceValue() { return m_Star7ChanceValue;}

public boolean LoadTable(HashMap<Integer, Table_GambleChancevalue> _tab) throws TableException
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
 HashMap<Integer, Table_GambleChancevalue> _hash = (HashMap<Integer, Table_GambleChancevalue> )obj;
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
 Table_GambleChancevalue _values = new Table_GambleChancevalue();
 _values.m_ChanceValue =  Integer.parseInt((String)valuesList.get(ID_CHANCEVALUE));
_values.m_Imoney =  Integer.parseInt((String)valuesList.get(ID_IMONEY));
_values.m_MaxValue =  Integer.parseInt((String)valuesList.get(ID_MAXVALUE));
_values.m_PlayerMoney =  Integer.parseInt((String)valuesList.get(ID_PLAYER_MONEY));
_values.m_Star1ChanceValue =  Integer.parseInt((String)valuesList.get(ID_STAR1_CHANCEVALUE));
_values.m_Star2ChanceValue =  Integer.parseInt((String)valuesList.get(ID_STAR2_CHANCEVALUE));
_values.m_Star3ChanceValue =  Integer.parseInt((String)valuesList.get(ID_STAR3_CHANCEVALUE));
_values.m_Star4ChanceValue =  Integer.parseInt((String)valuesList.get(ID_STAR4_CHANCEVALUE));
_values.m_Star5ChanceValue =  Integer.parseInt((String)valuesList.get(ID_STAR5_CHANCEVALUE));
_values.m_Star6ChanceValue =  Integer.parseInt((String)valuesList.get(ID_STAR6_CHANCEVALUE));
_values.m_Star7ChanceValue =  Integer.parseInt((String)valuesList.get(ID_STAR7_CHANCEVALUE));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

