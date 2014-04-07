//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_PvpnewReward implements ITableOperate {
 public final static String TAB_FILE_DATA = "/pvpnew_reward.txt";
 public static final int ID_RANK_MAX = 0 ;
public static final int ID_RANK_MIN = 1 ;
public static final int ID_REWARD_1 = 2 ;
public static final int ID_REWARD_1_NUM = 3 ;
public static final int ID_REWARD_2 = 4 ;
public static final int ID_REWARD_2_NUM = 5 ;
public static final int ID_REWARD_3 = 6 ;
public static final int ID_REWARD_3_NUM = 7 ;
public static final int ID_REWARD_4 = 8 ;
public static final int ID_REWARD_4_NUM = 9 ;
public static final int ID_REWARD_5 = 10 ;
public static final int ID_REWARD_5_NUM = 11 ;
public static final int MAX_RECORD = 12 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_RankMax;
 public int GetRankMax() { return m_RankMax;}

private int m_RankMin;
 public int GetRankMin() { return m_RankMin;}

private int m_Reward[] = new int[5];
 public int GetRewardbyIndex(int idx) {
 if(idx>=0 && idx<5) return m_Reward[idx];
 return -1;
 }

private int m_Reward1Num;
 public int GetReward1Num() { return m_Reward1Num;}

private int m_Reward2Num;
 public int GetReward2Num() { return m_Reward2Num;}

private int m_Reward3Num;
 public int GetReward3Num() { return m_Reward3Num;}

private int m_Reward4Num;
 public int GetReward4Num() { return m_Reward4Num;}

private int m_Reward5Num;
 public int GetReward5Num() { return m_Reward5Num;}

public boolean LoadTable(HashMap<Integer, Table_PvpnewReward> _tab) throws TableException
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
 HashMap<Integer, Table_PvpnewReward> _hash = (HashMap<Integer, Table_PvpnewReward> )obj;
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
 Table_PvpnewReward _values = new Table_PvpnewReward();
 _values.m_RankMax =  Integer.parseInt((String)valuesList.get(ID_RANK_MAX));
_values.m_RankMin =  Integer.parseInt((String)valuesList.get(ID_RANK_MIN));
_values.m_Reward [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_REWARD_1));
_values.m_Reward [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_REWARD_2));
_values.m_Reward [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_REWARD_3));
_values.m_Reward [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_REWARD_4));
_values.m_Reward [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_REWARD_5));
_values.m_Reward1Num =  Integer.parseInt((String)valuesList.get(ID_REWARD_1_NUM));
_values.m_Reward2Num =  Integer.parseInt((String)valuesList.get(ID_REWARD_2_NUM));
_values.m_Reward3Num =  Integer.parseInt((String)valuesList.get(ID_REWARD_3_NUM));
_values.m_Reward4Num =  Integer.parseInt((String)valuesList.get(ID_REWARD_4_NUM));
_values.m_Reward5Num =  Integer.parseInt((String)valuesList.get(ID_REWARD_5_NUM));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

