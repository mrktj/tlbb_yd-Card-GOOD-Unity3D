//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Appearance : ITableOperate
{ private const string TAB_FILE_DATA = "appearance.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_NOTE,
ID_NAME,
ID_HEAD_ICON,
ID_HEAD_ATLAS,
ID_BODY_ICON,
ID_BODY_ATLAS,
ID_HETI_ICON,
ID_XXX_ATLAS,
ID_STORY,
ID_COMB_DESCRIPE,
ID_DROP_DESCRIPE,
ID_IMG_NAME,
ID_IMG_NAME_ATLAS,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private string m_BodyAtlas;
 public string BodyAtlas { get{ return m_BodyAtlas;}}

private string m_BodyIcon;
 public string BodyIcon { get{ return m_BodyIcon;}}

private int m_CombDescripe;
 public int CombDescripe { get{ return m_CombDescripe;}}

private int m_DropDescripe;
 public int DropDescripe { get{ return m_DropDescripe;}}

private string m_HeadAtlas;
 public string HeadAtlas { get{ return m_HeadAtlas;}}

private string m_HeadIcon;
 public string HeadIcon { get{ return m_HeadIcon;}}

private string m_HetiIcon;
 public string HetiIcon { get{ return m_HetiIcon;}}

private string m_ImgName;
 public string ImgName { get{ return m_ImgName;}}

private string m_ImgNameAtlas;
 public string ImgNameAtlas { get{ return m_ImgNameAtlas;}}

private int m_Name;
 public int Name { get{ return m_Name;}}

private string m_Note;
 public string Note { get{ return m_Note;}}

private int m_Story;
 public int Story { get{ return m_Story;}}

private string m_XxxAtlas;
 public string XxxAtlas { get{ return m_XxxAtlas;}}

public bool LoadTable(Hashtable _tab)
 {
 if(!TableManager.ReaderPList(GetInstanceFile(),SerializableTable,_tab))
 {
 throw TableException.ErrorReader("Load File{0} Fail!!!",GetInstanceFile());
 }
 return true;
 }
 public void SerializableTable(ArrayList valuesList,string skey,Hashtable _hash)
 {
 if (string.IsNullOrEmpty(skey))
 {
 throw TableException.ErrorReader("Read File{0} as key is Empty Fail!!!", GetInstanceFile());
 }

 if ((int)_ID.MAX_RECORD!=valuesList.Count)
 {
 throw TableException.ErrorReader("Load {0} error as CodeSize:{1} not Equal DataSize:{2}", GetInstanceFile(),_ID.MAX_RECORD,valuesList.Count);
 }
 Int32 nKey = Convert.ToInt32(skey);
 Tab_Appearance _values = new Tab_Appearance();
 _values.m_BodyAtlas =  valuesList[(int)_ID.ID_BODY_ATLAS] as string;
_values.m_BodyIcon =  valuesList[(int)_ID.ID_BODY_ICON] as string;
_values.m_CombDescripe =  Convert.ToInt32(valuesList[(int)_ID.ID_COMB_DESCRIPE] as string);
_values.m_DropDescripe =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_DESCRIPE] as string);
_values.m_HeadAtlas =  valuesList[(int)_ID.ID_HEAD_ATLAS] as string;
_values.m_HeadIcon =  valuesList[(int)_ID.ID_HEAD_ICON] as string;
_values.m_HetiIcon =  valuesList[(int)_ID.ID_HETI_ICON] as string;
_values.m_ImgName =  valuesList[(int)_ID.ID_IMG_NAME] as string;
_values.m_ImgNameAtlas =  valuesList[(int)_ID.ID_IMG_NAME_ATLAS] as string;
_values.m_Name =  Convert.ToInt32(valuesList[(int)_ID.ID_NAME] as string);
_values.m_Note =  valuesList[(int)_ID.ID_NOTE] as string;
_values.m_Story =  Convert.ToInt32(valuesList[(int)_ID.ID_STORY] as string);
_values.m_XxxAtlas =  valuesList[(int)_ID.ID_XXX_ATLAS] as string;

 _hash.Add(nKey,_values); }


}
}

