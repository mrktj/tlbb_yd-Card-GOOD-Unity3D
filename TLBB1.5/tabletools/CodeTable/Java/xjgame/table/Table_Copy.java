//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Copy implements ITableOperate {
 public final static String TAB_FILE_DATA = "/copy.txt";
 public static final int ID_COPYNAME = 0 ;
public static final int ID_NOTE = 1 ;
public static final int ID_COPY_TYPE = 2 ;
public static final int ID_COPY_TYPE2 = 3 ;
public static final int ID_STARTWEEK = 4 ;
public static final int ID_STARTTIME = 5 ;
public static final int ID_TIMES = 6 ;
public static final int ID_COPY_ORDER = 7 ;
public static final int ID_COPY_NEXT = 8 ;
public static final int ID_PLAYER_LEVEL = 9 ;
public static final int ID_SUBCOPY1 = 10 ;
public static final int ID_SUBCOPY2 = 11 ;
public static final int ID_SUBCOPY3 = 12 ;
public static final int ID_SUBCOPY4 = 13 ;
public static final int ID_SUBCOPY5 = 14 ;
public static final int ID_SUBCOPY6 = 15 ;
public static final int ID_SUBCOPY7 = 16 ;
public static final int ID_SUBCOPY8 = 17 ;
public static final int ID_SUBCOPY9 = 18 ;
public static final int ID_SUBCOPY10 = 19 ;
public static final int ID_SUBCOPY11 = 20 ;
public static final int ID_SUBCOPY12 = 21 ;
public static final int ID_SUBCOPY13 = 22 ;
public static final int ID_SUBCOPY14 = 23 ;
public static final int ID_SUBCOPY15 = 24 ;
public static final int ID_DROP = 25 ;
public static final int ID_THUMB = 26 ;
public static final int MAX_RECORD = 27 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private int m_CopyNext;
 public int GetCopyNext() { return m_CopyNext;}

private int m_CopyOrder;
 public int GetCopyOrder() { return m_CopyOrder;}

private int m_CopyType[] = new int[2];
 public int GetCopyTypebyIndex(int idx) {
 if(idx>=0 && idx<2) return m_CopyType[idx];
 return -1;
 }

private int m_Copyname;
 public int GetCopyname() { return m_Copyname;}

private int m_Drop;
 public int GetDrop() { return m_Drop;}

private int m_Note;
 public int GetNote() { return m_Note;}

private int m_PlayerLevel;
 public int GetPlayerLevel() { return m_PlayerLevel;}

private int m_Starttime;
 public int GetStarttime() { return m_Starttime;}

private int m_Startweek;
 public int GetStartweek() { return m_Startweek;}

private int m_Subcopy[] = new int[15];
 public int GetSubcopybyIndex(int idx) {
 if(idx>=0 && idx<15) return m_Subcopy[idx];
 return -1;
 }

private String m_Thumb;
 public String GetThumb() { return m_Thumb;}

private int m_Times;
 public int GetTimes() { return m_Times;}

public boolean LoadTable(HashMap<Integer, Table_Copy> _tab) throws TableException
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
 HashMap<Integer, Table_Copy> _hash = (HashMap<Integer, Table_Copy> )obj;
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
 Table_Copy _values = new Table_Copy();
 _values.m_CopyNext =  Integer.parseInt((String)valuesList.get(ID_COPY_NEXT));
_values.m_CopyOrder =  Integer.parseInt((String)valuesList.get(ID_COPY_ORDER));
_values.m_CopyType [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_COPY_TYPE));
_values.m_CopyType [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_COPY_TYPE2));
_values.m_Copyname =  Integer.parseInt((String)valuesList.get(ID_COPYNAME));
_values.m_Drop =  Integer.parseInt((String)valuesList.get(ID_DROP));
_values.m_Note =  Integer.parseInt((String)valuesList.get(ID_NOTE));
_values.m_PlayerLevel =  Integer.parseInt((String)valuesList.get(ID_PLAYER_LEVEL));
_values.m_Starttime =  Integer.parseInt((String)valuesList.get(ID_STARTTIME));
_values.m_Startweek =  Integer.parseInt((String)valuesList.get(ID_STARTWEEK));
_values.m_Subcopy [ 0 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY1));
_values.m_Subcopy [ 1 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY2));
_values.m_Subcopy [ 2 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY3));
_values.m_Subcopy [ 3 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY4));
_values.m_Subcopy [ 4 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY5));
_values.m_Subcopy [ 5 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY6));
_values.m_Subcopy [ 6 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY7));
_values.m_Subcopy [ 7 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY8));
_values.m_Subcopy [ 8 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY9));
_values.m_Subcopy [ 9 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY10));
_values.m_Subcopy [ 10 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY11));
_values.m_Subcopy [ 11 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY12));
_values.m_Subcopy [ 12 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY13));
_values.m_Subcopy [ 13 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY14));
_values.m_Subcopy [ 14 ] =  Integer.parseInt((String)valuesList.get(ID_SUBCOPY15));
_values.m_Thumb =  (String)valuesList.get(ID_THUMB);
_values.m_Times =  Integer.parseInt((String)valuesList.get(ID_TIMES));

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

