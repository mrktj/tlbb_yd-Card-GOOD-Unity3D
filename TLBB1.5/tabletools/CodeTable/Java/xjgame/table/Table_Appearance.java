//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;
import java.util.ArrayList;
 import java.util.HashMap;

public class Table_Appearance implements ITableOperate {
 public final static String TAB_FILE_DATA = "/appearance.txt";
 public static final int ID_NOTE = 0 ;
public static final int ID_NAME = 1 ;
public static final int ID_HEAD_ICON = 2 ;
public static final int ID_HEAD_ATLAS = 3 ;
public static final int ID_BODY_ICON = 4 ;
public static final int ID_BODY_ATLAS = 5 ;
public static final int ID_HETI_ICON = 6 ;
public static final int ID_XXX_ATLAS = 7 ;
public static final int ID_STORY = 8 ;
public static final int ID_COMB_DESCRIPE = 9 ;
public static final int ID_DROP_DESCRIPE = 10 ;
public static final int ID_IMG_NAME = 11 ;
public static final int ID_IMG_NAME_ATLAS = 12 ;
public static final int MAX_RECORD = 13 ;

 public String GetInstanceFile(){return TAB_FILE_DATA; }

private String m_BodyAtlas;
 public String GetBodyAtlas() { return m_BodyAtlas;}

private String m_BodyIcon;
 public String GetBodyIcon() { return m_BodyIcon;}

private int m_CombDescripe;
 public int GetCombDescripe() { return m_CombDescripe;}

private int m_DropDescripe;
 public int GetDropDescripe() { return m_DropDescripe;}

private String m_HeadAtlas;
 public String GetHeadAtlas() { return m_HeadAtlas;}

private String m_HeadIcon;
 public String GetHeadIcon() { return m_HeadIcon;}

private String m_HetiIcon;
 public String GetHetiIcon() { return m_HetiIcon;}

private String m_ImgName;
 public String GetImgName() { return m_ImgName;}

private String m_ImgNameAtlas;
 public String GetImgNameAtlas() { return m_ImgNameAtlas;}

private int m_Name;
 public int GetName() { return m_Name;}

private String m_Note;
 public String GetNote() { return m_Note;}

private int m_Story;
 public int GetStory() { return m_Story;}

private String m_XxxAtlas;
 public String GetXxxAtlas() { return m_XxxAtlas;}

public boolean LoadTable(HashMap<Integer, Table_Appearance> _tab) throws TableException
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
 HashMap<Integer, Table_Appearance> _hash = (HashMap<Integer, Table_Appearance> )obj;
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
 Table_Appearance _values = new Table_Appearance();
 _values.m_BodyAtlas =  (String)valuesList.get(ID_BODY_ATLAS);
_values.m_BodyIcon =  (String)valuesList.get(ID_BODY_ICON);
_values.m_CombDescripe =  Integer.parseInt((String)valuesList.get(ID_COMB_DESCRIPE));
_values.m_DropDescripe =  Integer.parseInt((String)valuesList.get(ID_DROP_DESCRIPE));
_values.m_HeadAtlas =  (String)valuesList.get(ID_HEAD_ATLAS);
_values.m_HeadIcon =  (String)valuesList.get(ID_HEAD_ICON);
_values.m_HetiIcon =  (String)valuesList.get(ID_HETI_ICON);
_values.m_ImgName =  (String)valuesList.get(ID_IMG_NAME);
_values.m_ImgNameAtlas =  (String)valuesList.get(ID_IMG_NAME_ATLAS);
_values.m_Name =  Integer.parseInt((String)valuesList.get(ID_NAME));
_values.m_Note =  (String)valuesList.get(ID_NOTE);
_values.m_Story =  Integer.parseInt((String)valuesList.get(ID_STORY));
_values.m_XxxAtlas =  (String)valuesList.get(ID_XXX_ATLAS);

 _hash.put(nKey,_values);
 } catch (NumberFormatException e) {
 throw TableException.ErrorReader("Load "+GetInstanceFile()+" error as :" + e.getMessage());
 }
 }


}

