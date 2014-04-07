//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;
 using System.Xml;
 using Module.Log;
 using UnityEngine;
 using System.IO;

namespace GCGame.Table{

public interface ITableOperate
 {
 bool LoadTable(Hashtable _tab);
 string GetInstanceFile();
 }

 public delegate void SerializableTable(ArrayList valuesList, string skey, Hashtable _hash);
 
[Serializable]
 public class TableManager
{
 public static bool IsLoadFromLocal = false;
 public static bool ReaderPList(String xmlFile, SerializableTable _fun, Hashtable _hash)
 {
 string[] list= xmlFile.Split('.');
 string tableFilePath = Application.persistentDataPath + "/TableData/" + list[0] + ".txt";
 string[] alldataRow ;
 if (File.Exists(tableFilePath))
 {
 Debug.LogWarning("load from txt");
 StreamReader sr = null;
 sr = File.OpenText(tableFilePath);
 string tableData = sr.ReadToEnd();
 alldataRow = tableData.Split('\n');
 }
 else
 {
 Debug.LogWarning("load from localdata");
 TextAsset testAsset = Resources.Load(list[0], typeof(TextAsset)) as TextAsset;
 alldataRow = testAsset.text.Split('\n');
 }
 foreach(string line in alldataRow)
 {
 if(String.IsNullOrEmpty(line))continue;
 string[] strCol = line.Split('\t');
 if (strCol.Length == 0) continue;
 string skey = strCol[0];
 if (string.IsNullOrEmpty(skey)) return false;
 ArrayList valuesList = new ArrayList();
 for (int i = 1; i < strCol.Length;++i )
 {
 valuesList.Add(strCol[i]);
 }
 _fun(valuesList, skey, _hash);
 }
 return true;
 }


private static Hashtable g_ChongLou = new Hashtable();
 public static Hashtable GetChongLou()
 {
 if (g_ChongLou.Count == 0)
 {
 Tab_ChongLou s_Tab_ChongLou = new Tab_ChongLou();
 s_Tab_ChongLou.LoadTable(g_ChongLou);
 }
 return g_ChongLou;
 }

private static Hashtable g_GoldPrice = new Hashtable();
 public static Hashtable GetGoldPrice()
 {
 if (g_GoldPrice.Count == 0)
 {
 Tab_GoldPrice s_Tab_GoldPrice = new Tab_GoldPrice();
 s_Tab_GoldPrice.LoadTable(g_GoldPrice);
 }
 return g_GoldPrice;
 }

private static Hashtable g_PowerPrice = new Hashtable();
 public static Hashtable GetPowerPrice()
 {
 if (g_PowerPrice.Count == 0)
 {
 Tab_PowerPrice s_Tab_PowerPrice = new Tab_PowerPrice();
 s_Tab_PowerPrice.LoadTable(g_PowerPrice);
 }
 return g_PowerPrice;
 }

private static Hashtable g_TableModel = new Hashtable();
 public static Hashtable GetTableModel()
 {
 if (g_TableModel.Count == 0)
 {
 Tab_TableModel s_Tab_TableModel = new Tab_TableModel();
 s_Tab_TableModel.LoadTable(g_TableModel);
 }
 return g_TableModel;
 }

private static Hashtable g_YueKa = new Hashtable();
 public static Hashtable GetYueKa()
 {
 if (g_YueKa.Count == 0)
 {
 Tab_YueKa s_Tab_YueKa = new Tab_YueKa();
 s_Tab_YueKa.LoadTable(g_YueKa);
 }
 return g_YueKa;
 }

private static Hashtable g_Activities = new Hashtable();
 public static Hashtable GetActivities()
 {
 if (g_Activities.Count == 0)
 {
 Tab_Activities s_Tab_Activities = new Tab_Activities();
 s_Tab_Activities.LoadTable(g_Activities);
 }
 return g_Activities;
 }

private static Hashtable g_Activity = new Hashtable();
 public static Hashtable GetActivity()
 {
 if (g_Activity.Count == 0)
 {
 Tab_Activity s_Tab_Activity = new Tab_Activity();
 s_Tab_Activity.LoadTable(g_Activity);
 }
 return g_Activity;
 }

private static Hashtable g_Appearance = new Hashtable();
 public static Hashtable GetAppearance()
 {
 if (g_Appearance.Count == 0)
 {
 Tab_Appearance s_Tab_Appearance = new Tab_Appearance();
 s_Tab_Appearance.LoadTable(g_Appearance);
 }
 return g_Appearance;
 }

private static Hashtable g_Audio = new Hashtable();
 public static Hashtable GetAudio()
 {
 if (g_Audio.Count == 0)
 {
 Tab_Audio s_Tab_Audio = new Tab_Audio();
 s_Tab_Audio.LoadTable(g_Audio);
 }
 return g_Audio;
 }

private static Hashtable g_Bagua = new Hashtable();
 public static Hashtable GetBagua()
 {
 if (g_Bagua.Count == 0)
 {
 Tab_Bagua s_Tab_Bagua = new Tab_Bagua();
 s_Tab_Bagua.LoadTable(g_Bagua);
 }
 return g_Bagua;
 }

private static Hashtable g_Btninfo = new Hashtable();
 public static Hashtable GetBtninfo()
 {
 if (g_Btninfo.Count == 0)
 {
 Tab_Btninfo s_Tab_Btninfo = new Tab_Btninfo();
 s_Tab_Btninfo.LoadTable(g_Btninfo);
 }
 return g_Btninfo;
 }

private static Hashtable g_Buff = new Hashtable();
 public static Hashtable GetBuff()
 {
 if (g_Buff.Count == 0)
 {
 Tab_Buff s_Tab_Buff = new Tab_Buff();
 s_Tab_Buff.LoadTable(g_Buff);
 }
 return g_Buff;
 }

private static Hashtable g_BuffEffect = new Hashtable();
 public static Hashtable GetBuffEffect()
 {
 if (g_BuffEffect.Count == 0)
 {
 Tab_BuffEffect s_Tab_BuffEffect = new Tab_BuffEffect();
 s_Tab_BuffEffect.LoadTable(g_BuffEffect);
 }
 return g_BuffEffect;
 }

private static Hashtable g_Card = new Hashtable();
 public static Hashtable GetCard()
 {
 if (g_Card.Count == 0)
 {
 Tab_Card s_Tab_Card = new Tab_Card();
 s_Tab_Card.LoadTable(g_Card);
 }
 return g_Card;
 }

private static Hashtable g_Cardexperience = new Hashtable();
 public static Hashtable GetCardexperience()
 {
 if (g_Cardexperience.Count == 0)
 {
 Tab_Cardexperience s_Tab_Cardexperience = new Tab_Cardexperience();
 s_Tab_Cardexperience.LoadTable(g_Cardexperience);
 }
 return g_Cardexperience;
 }

private static Hashtable g_Cardgroup = new Hashtable();
 public static Hashtable GetCardgroup()
 {
 if (g_Cardgroup.Count == 0)
 {
 Tab_Cardgroup s_Tab_Cardgroup = new Tab_Cardgroup();
 s_Tab_Cardgroup.LoadTable(g_Cardgroup);
 }
 return g_Cardgroup;
 }

private static Hashtable g_Copy = new Hashtable();
 public static Hashtable GetCopy()
 {
 if (g_Copy.Count == 0)
 {
 Tab_Copy s_Tab_Copy = new Tab_Copy();
 s_Tab_Copy.LoadTable(g_Copy);
 }
 return g_Copy;
 }

private static Hashtable g_Copydetail = new Hashtable();
 public static Hashtable GetCopydetail()
 {
 if (g_Copydetail.Count == 0)
 {
 Tab_Copydetail s_Tab_Copydetail = new Tab_Copydetail();
 s_Tab_Copydetail.LoadTable(g_Copydetail);
 }
 return g_Copydetail;
 }

private static Hashtable g_Copysurprise = new Hashtable();
 public static Hashtable GetCopysurprise()
 {
 if (g_Copysurprise.Count == 0)
 {
 Tab_Copysurprise s_Tab_Copysurprise = new Tab_Copysurprise();
 s_Tab_Copysurprise.LoadTable(g_Copysurprise);
 }
 return g_Copysurprise;
 }

private static Hashtable g_DollarGamble = new Hashtable();
 public static Hashtable GetDollarGamble()
 {
 if (g_DollarGamble.Count == 0)
 {
 Tab_DollarGamble s_Tab_DollarGamble = new Tab_DollarGamble();
 s_Tab_DollarGamble.LoadTable(g_DollarGamble);
 }
 return g_DollarGamble;
 }

private static Hashtable g_Drop = new Hashtable();
 public static Hashtable GetDrop()
 {
 if (g_Drop.Count == 0)
 {
 Tab_Drop s_Tab_Drop = new Tab_Drop();
 s_Tab_Drop.LoadTable(g_Drop);
 }
 return g_Drop;
 }

private static Hashtable g_Effect = new Hashtable();
 public static Hashtable GetEffect()
 {
 if (g_Effect.Count == 0)
 {
 Tab_Effect s_Tab_Effect = new Tab_Effect();
 s_Tab_Effect.LoadTable(g_Effect);
 }
 return g_Effect;
 }

private static Hashtable g_Eightsided = new Hashtable();
 public static Hashtable GetEightsided()
 {
 if (g_Eightsided.Count == 0)
 {
 Tab_Eightsided s_Tab_Eightsided = new Tab_Eightsided();
 s_Tab_Eightsided.LoadTable(g_Eightsided);
 }
 return g_Eightsided;
 }

private static Hashtable g_Evolve = new Hashtable();
 public static Hashtable GetEvolve()
 {
 if (g_Evolve.Count == 0)
 {
 Tab_Evolve s_Tab_Evolve = new Tab_Evolve();
 s_Tab_Evolve.LoadTable(g_Evolve);
 }
 return g_Evolve;
 }

private static Hashtable g_Fengshui = new Hashtable();
 public static Hashtable GetFengshui()
 {
 if (g_Fengshui.Count == 0)
 {
 Tab_Fengshui s_Tab_Fengshui = new Tab_Fengshui();
 s_Tab_Fengshui.LoadTable(g_Fengshui);
 }
 return g_Fengshui;
 }

private static Hashtable g_FriendGamble = new Hashtable();
 public static Hashtable GetFriendGamble()
 {
 if (g_FriendGamble.Count == 0)
 {
 Tab_FriendGamble s_Tab_FriendGamble = new Tab_FriendGamble();
 s_Tab_FriendGamble.LoadTable(g_FriendGamble);
 }
 return g_FriendGamble;
 }

private static Hashtable g_GambleChancevalue = new Hashtable();
 public static Hashtable GetGambleChancevalue()
 {
 if (g_GambleChancevalue.Count == 0)
 {
 Tab_GambleChancevalue s_Tab_GambleChancevalue = new Tab_GambleChancevalue();
 s_Tab_GambleChancevalue.LoadTable(g_GambleChancevalue);
 }
 return g_GambleChancevalue;
 }

private static Hashtable g_GambleCost = new Hashtable();
 public static Hashtable GetGambleCost()
 {
 if (g_GambleCost.Count == 0)
 {
 Tab_GambleCost s_Tab_GambleCost = new Tab_GambleCost();
 s_Tab_GambleCost.LoadTable(g_GambleCost);
 }
 return g_GambleCost;
 }

private static Hashtable g_Iap = new Hashtable();
 public static Hashtable GetIap()
 {
 if (g_Iap.Count == 0)
 {
 Tab_Iap s_Tab_Iap = new Tab_Iap();
 s_Tab_Iap.LoadTable(g_Iap);
 }
 return g_Iap;
 }

private static Hashtable g_Idexperience = new Hashtable();
 public static Hashtable GetIdexperience()
 {
 if (g_Idexperience.Count == 0)
 {
 Tab_Idexperience s_Tab_Idexperience = new Tab_Idexperience();
 s_Tab_Idexperience.LoadTable(g_Idexperience);
 }
 return g_Idexperience;
 }

private static Hashtable g_Item = new Hashtable();
 public static Hashtable GetItem()
 {
 if (g_Item.Count == 0)
 {
 Tab_Item s_Tab_Item = new Tab_Item();
 s_Tab_Item.LoadTable(g_Item);
 }
 return g_Item;
 }

private static Hashtable g_Language = new Hashtable();
 public static Hashtable GetLanguage()
 {
 if (g_Language.Count == 0)
 {
 Tab_Language s_Tab_Language = new Tab_Language();
 s_Tab_Language.LoadTable(g_Language);
 }
 return g_Language;
 }

private static Hashtable g_Leaderskill = new Hashtable();
 public static Hashtable GetLeaderskill()
 {
 if (g_Leaderskill.Count == 0)
 {
 Tab_Leaderskill s_Tab_Leaderskill = new Tab_Leaderskill();
 s_Tab_Leaderskill.LoadTable(g_Leaderskill);
 }
 return g_Leaderskill;
 }

private static Hashtable g_Popup = new Hashtable();
 public static Hashtable GetPopup()
 {
 if (g_Popup.Count == 0)
 {
 Tab_Popup s_Tab_Popup = new Tab_Popup();
 s_Tab_Popup.LoadTable(g_Popup);
 }
 return g_Popup;
 }

private static Hashtable g_Push = new Hashtable();
 public static Hashtable GetPush()
 {
 if (g_Push.Count == 0)
 {
 Tab_Push s_Tab_Push = new Tab_Push();
 s_Tab_Push.LoadTable(g_Push);
 }
 return g_Push;
 }

private static Hashtable g_PvpBot = new Hashtable();
 public static Hashtable GetPvpBot()
 {
 if (g_PvpBot.Count == 0)
 {
 Tab_PvpBot s_Tab_PvpBot = new Tab_PvpBot();
 s_Tab_PvpBot.LoadTable(g_PvpBot);
 }
 return g_PvpBot;
 }

private static Hashtable g_PvpGenerate = new Hashtable();
 public static Hashtable GetPvpGenerate()
 {
 if (g_PvpGenerate.Count == 0)
 {
 Tab_PvpGenerate s_Tab_PvpGenerate = new Tab_PvpGenerate();
 s_Tab_PvpGenerate.LoadTable(g_PvpGenerate);
 }
 return g_PvpGenerate;
 }

private static Hashtable g_PvpScore = new Hashtable();
 public static Hashtable GetPvpScore()
 {
 if (g_PvpScore.Count == 0)
 {
 Tab_PvpScore s_Tab_PvpScore = new Tab_PvpScore();
 s_Tab_PvpScore.LoadTable(g_PvpScore);
 }
 return g_PvpScore;
 }

private static Hashtable g_PvpShop = new Hashtable();
 public static Hashtable GetPvpShop()
 {
 if (g_PvpShop.Count == 0)
 {
 Tab_PvpShop s_Tab_PvpShop = new Tab_PvpShop();
 s_Tab_PvpShop.LoadTable(g_PvpShop);
 }
 return g_PvpShop;
 }

private static Hashtable g_PvpnewReward = new Hashtable();
 public static Hashtable GetPvpnewReward()
 {
 if (g_PvpnewReward.Count == 0)
 {
 Tab_PvpnewReward s_Tab_PvpnewReward = new Tab_PvpnewReward();
 s_Tab_PvpnewReward.LoadTable(g_PvpnewReward);
 }
 return g_PvpnewReward;
 }

private static Hashtable g_Quest = new Hashtable();
 public static Hashtable GetQuest()
 {
 if (g_Quest.Count == 0)
 {
 Tab_Quest s_Tab_Quest = new Tab_Quest();
 s_Tab_Quest.LoadTable(g_Quest);
 }
 return g_Quest;
 }

private static Hashtable g_Randomname = new Hashtable();
 public static Hashtable GetRandomname()
 {
 if (g_Randomname.Count == 0)
 {
 Tab_Randomname s_Tab_Randomname = new Tab_Randomname();
 s_Tab_Randomname.LoadTable(g_Randomname);
 }
 return g_Randomname;
 }

private static Hashtable g_Rollingnotice = new Hashtable();
 public static Hashtable GetRollingnotice()
 {
 if (g_Rollingnotice.Count == 0)
 {
 Tab_Rollingnotice s_Tab_Rollingnotice = new Tab_Rollingnotice();
 s_Tab_Rollingnotice.LoadTable(g_Rollingnotice);
 }
 return g_Rollingnotice;
 }

private static Hashtable g_Scratch = new Hashtable();
 public static Hashtable GetScratch()
 {
 if (g_Scratch.Count == 0)
 {
 Tab_Scratch s_Tab_Scratch = new Tab_Scratch();
 s_Tab_Scratch.LoadTable(g_Scratch);
 }
 return g_Scratch;
 }

private static Hashtable g_ScratchCost = new Hashtable();
 public static Hashtable GetScratchCost()
 {
 if (g_ScratchCost.Count == 0)
 {
 Tab_ScratchCost s_Tab_ScratchCost = new Tab_ScratchCost();
 s_Tab_ScratchCost.LoadTable(g_ScratchCost);
 }
 return g_ScratchCost;
 }

private static Hashtable g_ScratchPrize = new Hashtable();
 public static Hashtable GetScratchPrize()
 {
 if (g_ScratchPrize.Count == 0)
 {
 Tab_ScratchPrize s_Tab_ScratchPrize = new Tab_ScratchPrize();
 s_Tab_ScratchPrize.LoadTable(g_ScratchPrize);
 }
 return g_ScratchPrize;
 }

private static Hashtable g_Servers = new Hashtable();
 public static Hashtable GetServers()
 {
 if (g_Servers.Count == 0)
 {
 Tab_Servers s_Tab_Servers = new Tab_Servers();
 s_Tab_Servers.LoadTable(g_Servers);
 }
 return g_Servers;
 }

private static Hashtable g_ShopNotice = new Hashtable();
 public static Hashtable GetShopNotice()
 {
 if (g_ShopNotice.Count == 0)
 {
 Tab_ShopNotice s_Tab_ShopNotice = new Tab_ShopNotice();
 s_Tab_ShopNotice.LoadTable(g_ShopNotice);
 }
 return g_ShopNotice;
 }

private static Hashtable g_Shopping = new Hashtable();
 public static Hashtable GetShopping()
 {
 if (g_Shopping.Count == 0)
 {
 Tab_Shopping s_Tab_Shopping = new Tab_Shopping();
 s_Tab_Shopping.LoadTable(g_Shopping);
 }
 return g_Shopping;
 }

private static Hashtable g_Skill = new Hashtable();
 public static Hashtable GetSkill()
 {
 if (g_Skill.Count == 0)
 {
 Tab_Skill s_Tab_Skill = new Tab_Skill();
 s_Tab_Skill.LoadTable(g_Skill);
 }
 return g_Skill;
 }

private static Hashtable g_SkillDisplay = new Hashtable();
 public static Hashtable GetSkillDisplay()
 {
 if (g_SkillDisplay.Count == 0)
 {
 Tab_SkillDisplay s_Tab_SkillDisplay = new Tab_SkillDisplay();
 s_Tab_SkillDisplay.LoadTable(g_SkillDisplay);
 }
 return g_SkillDisplay;
 }

private static Hashtable g_Skillbasicchance = new Hashtable();
 public static Hashtable GetSkillbasicchance()
 {
 if (g_Skillbasicchance.Count == 0)
 {
 Tab_Skillbasicchance s_Tab_Skillbasicchance = new Tab_Skillbasicchance();
 s_Tab_Skillbasicchance.LoadTable(g_Skillbasicchance);
 }
 return g_Skillbasicchance;
 }

private static Hashtable g_Skillgroup = new Hashtable();
 public static Hashtable GetSkillgroup()
 {
 if (g_Skillgroup.Count == 0)
 {
 Tab_Skillgroup s_Tab_Skillgroup = new Tab_Skillgroup();
 s_Tab_Skillgroup.LoadTable(g_Skillgroup);
 }
 return g_Skillgroup;
 }

private static Hashtable g_Skillupdate = new Hashtable();
 public static Hashtable GetSkillupdate()
 {
 if (g_Skillupdate.Count == 0)
 {
 Tab_Skillupdate s_Tab_Skillupdate = new Tab_Skillupdate();
 s_Tab_Skillupdate.LoadTable(g_Skillupdate);
 }
 return g_Skillupdate;
 }

private static Hashtable g_StarGamble = new Hashtable();
 public static Hashtable GetStarGamble()
 {
 if (g_StarGamble.Count == 0)
 {
 Tab_StarGamble s_Tab_StarGamble = new Tab_StarGamble();
 s_Tab_StarGamble.LoadTable(g_StarGamble);
 }
 return g_StarGamble;
 }

private static Hashtable g_Studyskill = new Hashtable();
 public static Hashtable GetStudyskill()
 {
 if (g_Studyskill.Count == 0)
 {
 Tab_Studyskill s_Tab_Studyskill = new Tab_Studyskill();
 s_Tab_Studyskill.LoadTable(g_Studyskill);
 }
 return g_Studyskill;
 }

private static Hashtable g_Worldboss = new Hashtable();
 public static Hashtable GetWorldboss()
 {
 if (g_Worldboss.Count == 0)
 {
 Tab_Worldboss s_Tab_Worldboss = new Tab_Worldboss();
 s_Tab_Worldboss.LoadTable(g_Worldboss);
 }
 return g_Worldboss;
 }

private static Hashtable g_Worldbossaward = new Hashtable();
 public static Hashtable GetWorldbossaward()
 {
 if (g_Worldbossaward.Count == 0)
 {
 Tab_Worldbossaward s_Tab_Worldbossaward = new Tab_Worldbossaward();
 s_Tab_Worldbossaward.LoadTable(g_Worldbossaward);
 }
 return g_Worldbossaward;
 }

private static Hashtable g_Worldbosspar = new Hashtable();
 public static Hashtable GetWorldbosspar()
 {
 if (g_Worldbosspar.Count == 0)
 {
 Tab_Worldbosspar s_Tab_Worldbosspar = new Tab_Worldbosspar();
 s_Tab_Worldbosspar.LoadTable(g_Worldbosspar);
 }
 return g_Worldbosspar;
 }

private static Hashtable g_Yunyinghuodong = new Hashtable();
 public static Hashtable GetYunyinghuodong()
 {
 if (g_Yunyinghuodong.Count == 0)
 {
 Tab_Yunyinghuodong s_Tab_Yunyinghuodong = new Tab_Yunyinghuodong();
 s_Tab_Yunyinghuodong.LoadTable(g_Yunyinghuodong);
 }
 return g_Yunyinghuodong;
 }

public IEnumerator InitTable()
 {
 Tab_ChongLou s_Tab_ChongLou = new Tab_ChongLou();
 if(s_Tab_ChongLou.LoadTable(g_ChongLou))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_ChongLou.GetInstanceFile(),g_ChongLou.Count);
 }
 yield return null;

Tab_GoldPrice s_Tab_GoldPrice = new Tab_GoldPrice();
 if(s_Tab_GoldPrice.LoadTable(g_GoldPrice))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_GoldPrice.GetInstanceFile(),g_GoldPrice.Count);
 }
 yield return null;

Tab_PowerPrice s_Tab_PowerPrice = new Tab_PowerPrice();
 if(s_Tab_PowerPrice.LoadTable(g_PowerPrice))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_PowerPrice.GetInstanceFile(),g_PowerPrice.Count);
 }
 yield return null;

Tab_TableModel s_Tab_TableModel = new Tab_TableModel();
 if(s_Tab_TableModel.LoadTable(g_TableModel))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_TableModel.GetInstanceFile(),g_TableModel.Count);
 }
 yield return null;

Tab_YueKa s_Tab_YueKa = new Tab_YueKa();
 if(s_Tab_YueKa.LoadTable(g_YueKa))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_YueKa.GetInstanceFile(),g_YueKa.Count);
 }
 yield return null;

Tab_Activities s_Tab_Activities = new Tab_Activities();
 if(s_Tab_Activities.LoadTable(g_Activities))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Activities.GetInstanceFile(),g_Activities.Count);
 }
 yield return null;

Tab_Activity s_Tab_Activity = new Tab_Activity();
 if(s_Tab_Activity.LoadTable(g_Activity))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Activity.GetInstanceFile(),g_Activity.Count);
 }
 yield return null;

Tab_Appearance s_Tab_Appearance = new Tab_Appearance();
 if(s_Tab_Appearance.LoadTable(g_Appearance))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Appearance.GetInstanceFile(),g_Appearance.Count);
 }
 yield return null;

Tab_Audio s_Tab_Audio = new Tab_Audio();
 if(s_Tab_Audio.LoadTable(g_Audio))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Audio.GetInstanceFile(),g_Audio.Count);
 }
 yield return null;

Tab_Bagua s_Tab_Bagua = new Tab_Bagua();
 if(s_Tab_Bagua.LoadTable(g_Bagua))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Bagua.GetInstanceFile(),g_Bagua.Count);
 }
 yield return null;

Tab_Btninfo s_Tab_Btninfo = new Tab_Btninfo();
 if(s_Tab_Btninfo.LoadTable(g_Btninfo))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Btninfo.GetInstanceFile(),g_Btninfo.Count);
 }
 yield return null;

Tab_Buff s_Tab_Buff = new Tab_Buff();
 if(s_Tab_Buff.LoadTable(g_Buff))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Buff.GetInstanceFile(),g_Buff.Count);
 }
 yield return null;

Tab_BuffEffect s_Tab_BuffEffect = new Tab_BuffEffect();
 if(s_Tab_BuffEffect.LoadTable(g_BuffEffect))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_BuffEffect.GetInstanceFile(),g_BuffEffect.Count);
 }
 yield return null;

Tab_Card s_Tab_Card = new Tab_Card();
 if(s_Tab_Card.LoadTable(g_Card))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Card.GetInstanceFile(),g_Card.Count);
 }
 yield return null;

Tab_Cardexperience s_Tab_Cardexperience = new Tab_Cardexperience();
 if(s_Tab_Cardexperience.LoadTable(g_Cardexperience))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Cardexperience.GetInstanceFile(),g_Cardexperience.Count);
 }
 yield return null;

Tab_Cardgroup s_Tab_Cardgroup = new Tab_Cardgroup();
 if(s_Tab_Cardgroup.LoadTable(g_Cardgroup))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Cardgroup.GetInstanceFile(),g_Cardgroup.Count);
 }
 yield return null;

Tab_Copy s_Tab_Copy = new Tab_Copy();
 if(s_Tab_Copy.LoadTable(g_Copy))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Copy.GetInstanceFile(),g_Copy.Count);
 }
 yield return null;

Tab_Copydetail s_Tab_Copydetail = new Tab_Copydetail();
 if(s_Tab_Copydetail.LoadTable(g_Copydetail))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Copydetail.GetInstanceFile(),g_Copydetail.Count);
 }
 yield return null;

Tab_Copysurprise s_Tab_Copysurprise = new Tab_Copysurprise();
 if(s_Tab_Copysurprise.LoadTable(g_Copysurprise))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Copysurprise.GetInstanceFile(),g_Copysurprise.Count);
 }
 yield return null;

Tab_DollarGamble s_Tab_DollarGamble = new Tab_DollarGamble();
 if(s_Tab_DollarGamble.LoadTable(g_DollarGamble))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_DollarGamble.GetInstanceFile(),g_DollarGamble.Count);
 }
 yield return null;

Tab_Drop s_Tab_Drop = new Tab_Drop();
 if(s_Tab_Drop.LoadTable(g_Drop))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Drop.GetInstanceFile(),g_Drop.Count);
 }
 yield return null;

Tab_Effect s_Tab_Effect = new Tab_Effect();
 if(s_Tab_Effect.LoadTable(g_Effect))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Effect.GetInstanceFile(),g_Effect.Count);
 }
 yield return null;

Tab_Eightsided s_Tab_Eightsided = new Tab_Eightsided();
 if(s_Tab_Eightsided.LoadTable(g_Eightsided))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Eightsided.GetInstanceFile(),g_Eightsided.Count);
 }
 yield return null;

Tab_Evolve s_Tab_Evolve = new Tab_Evolve();
 if(s_Tab_Evolve.LoadTable(g_Evolve))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Evolve.GetInstanceFile(),g_Evolve.Count);
 }
 yield return null;

Tab_Fengshui s_Tab_Fengshui = new Tab_Fengshui();
 if(s_Tab_Fengshui.LoadTable(g_Fengshui))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Fengshui.GetInstanceFile(),g_Fengshui.Count);
 }
 yield return null;

Tab_FriendGamble s_Tab_FriendGamble = new Tab_FriendGamble();
 if(s_Tab_FriendGamble.LoadTable(g_FriendGamble))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_FriendGamble.GetInstanceFile(),g_FriendGamble.Count);
 }
 yield return null;

Tab_GambleChancevalue s_Tab_GambleChancevalue = new Tab_GambleChancevalue();
 if(s_Tab_GambleChancevalue.LoadTable(g_GambleChancevalue))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_GambleChancevalue.GetInstanceFile(),g_GambleChancevalue.Count);
 }
 yield return null;

Tab_GambleCost s_Tab_GambleCost = new Tab_GambleCost();
 if(s_Tab_GambleCost.LoadTable(g_GambleCost))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_GambleCost.GetInstanceFile(),g_GambleCost.Count);
 }
 yield return null;

Tab_Iap s_Tab_Iap = new Tab_Iap();
 if(s_Tab_Iap.LoadTable(g_Iap))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Iap.GetInstanceFile(),g_Iap.Count);
 }
 yield return null;

Tab_Idexperience s_Tab_Idexperience = new Tab_Idexperience();
 if(s_Tab_Idexperience.LoadTable(g_Idexperience))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Idexperience.GetInstanceFile(),g_Idexperience.Count);
 }
 yield return null;

Tab_Item s_Tab_Item = new Tab_Item();
 if(s_Tab_Item.LoadTable(g_Item))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Item.GetInstanceFile(),g_Item.Count);
 }
 yield return null;

Tab_Language s_Tab_Language = new Tab_Language();
 if(s_Tab_Language.LoadTable(g_Language))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Language.GetInstanceFile(),g_Language.Count);
 }
 yield return null;

Tab_Leaderskill s_Tab_Leaderskill = new Tab_Leaderskill();
 if(s_Tab_Leaderskill.LoadTable(g_Leaderskill))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Leaderskill.GetInstanceFile(),g_Leaderskill.Count);
 }
 yield return null;

Tab_Popup s_Tab_Popup = new Tab_Popup();
 if(s_Tab_Popup.LoadTable(g_Popup))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Popup.GetInstanceFile(),g_Popup.Count);
 }
 yield return null;

Tab_Push s_Tab_Push = new Tab_Push();
 if(s_Tab_Push.LoadTable(g_Push))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Push.GetInstanceFile(),g_Push.Count);
 }
 yield return null;

Tab_PvpBot s_Tab_PvpBot = new Tab_PvpBot();
 if(s_Tab_PvpBot.LoadTable(g_PvpBot))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_PvpBot.GetInstanceFile(),g_PvpBot.Count);
 }
 yield return null;

Tab_PvpGenerate s_Tab_PvpGenerate = new Tab_PvpGenerate();
 if(s_Tab_PvpGenerate.LoadTable(g_PvpGenerate))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_PvpGenerate.GetInstanceFile(),g_PvpGenerate.Count);
 }
 yield return null;

Tab_PvpScore s_Tab_PvpScore = new Tab_PvpScore();
 if(s_Tab_PvpScore.LoadTable(g_PvpScore))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_PvpScore.GetInstanceFile(),g_PvpScore.Count);
 }
 yield return null;

Tab_PvpShop s_Tab_PvpShop = new Tab_PvpShop();
 if(s_Tab_PvpShop.LoadTable(g_PvpShop))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_PvpShop.GetInstanceFile(),g_PvpShop.Count);
 }
 yield return null;

Tab_PvpnewReward s_Tab_PvpnewReward = new Tab_PvpnewReward();
 if(s_Tab_PvpnewReward.LoadTable(g_PvpnewReward))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_PvpnewReward.GetInstanceFile(),g_PvpnewReward.Count);
 }
 yield return null;

Tab_Quest s_Tab_Quest = new Tab_Quest();
 if(s_Tab_Quest.LoadTable(g_Quest))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Quest.GetInstanceFile(),g_Quest.Count);
 }
 yield return null;

Tab_Randomname s_Tab_Randomname = new Tab_Randomname();
 if(s_Tab_Randomname.LoadTable(g_Randomname))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Randomname.GetInstanceFile(),g_Randomname.Count);
 }
 yield return null;

Tab_Rollingnotice s_Tab_Rollingnotice = new Tab_Rollingnotice();
 if(s_Tab_Rollingnotice.LoadTable(g_Rollingnotice))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Rollingnotice.GetInstanceFile(),g_Rollingnotice.Count);
 }
 yield return null;

Tab_Scratch s_Tab_Scratch = new Tab_Scratch();
 if(s_Tab_Scratch.LoadTable(g_Scratch))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Scratch.GetInstanceFile(),g_Scratch.Count);
 }
 yield return null;

Tab_ScratchCost s_Tab_ScratchCost = new Tab_ScratchCost();
 if(s_Tab_ScratchCost.LoadTable(g_ScratchCost))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_ScratchCost.GetInstanceFile(),g_ScratchCost.Count);
 }
 yield return null;

Tab_ScratchPrize s_Tab_ScratchPrize = new Tab_ScratchPrize();
 if(s_Tab_ScratchPrize.LoadTable(g_ScratchPrize))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_ScratchPrize.GetInstanceFile(),g_ScratchPrize.Count);
 }
 yield return null;

Tab_Servers s_Tab_Servers = new Tab_Servers();
 if(s_Tab_Servers.LoadTable(g_Servers))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Servers.GetInstanceFile(),g_Servers.Count);
 }
 yield return null;

Tab_ShopNotice s_Tab_ShopNotice = new Tab_ShopNotice();
 if(s_Tab_ShopNotice.LoadTable(g_ShopNotice))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_ShopNotice.GetInstanceFile(),g_ShopNotice.Count);
 }
 yield return null;

Tab_Shopping s_Tab_Shopping = new Tab_Shopping();
 if(s_Tab_Shopping.LoadTable(g_Shopping))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Shopping.GetInstanceFile(),g_Shopping.Count);
 }
 yield return null;

Tab_Skill s_Tab_Skill = new Tab_Skill();
 if(s_Tab_Skill.LoadTable(g_Skill))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Skill.GetInstanceFile(),g_Skill.Count);
 }
 yield return null;

Tab_SkillDisplay s_Tab_SkillDisplay = new Tab_SkillDisplay();
 if(s_Tab_SkillDisplay.LoadTable(g_SkillDisplay))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_SkillDisplay.GetInstanceFile(),g_SkillDisplay.Count);
 }
 yield return null;

Tab_Skillbasicchance s_Tab_Skillbasicchance = new Tab_Skillbasicchance();
 if(s_Tab_Skillbasicchance.LoadTable(g_Skillbasicchance))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Skillbasicchance.GetInstanceFile(),g_Skillbasicchance.Count);
 }
 yield return null;

Tab_Skillgroup s_Tab_Skillgroup = new Tab_Skillgroup();
 if(s_Tab_Skillgroup.LoadTable(g_Skillgroup))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Skillgroup.GetInstanceFile(),g_Skillgroup.Count);
 }
 yield return null;

Tab_Skillupdate s_Tab_Skillupdate = new Tab_Skillupdate();
 if(s_Tab_Skillupdate.LoadTable(g_Skillupdate))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Skillupdate.GetInstanceFile(),g_Skillupdate.Count);
 }
 yield return null;

Tab_StarGamble s_Tab_StarGamble = new Tab_StarGamble();
 if(s_Tab_StarGamble.LoadTable(g_StarGamble))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_StarGamble.GetInstanceFile(),g_StarGamble.Count);
 }
 yield return null;

Tab_Studyskill s_Tab_Studyskill = new Tab_Studyskill();
 if(s_Tab_Studyskill.LoadTable(g_Studyskill))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Studyskill.GetInstanceFile(),g_Studyskill.Count);
 }
 yield return null;

Tab_Worldboss s_Tab_Worldboss = new Tab_Worldboss();
 if(s_Tab_Worldboss.LoadTable(g_Worldboss))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Worldboss.GetInstanceFile(),g_Worldboss.Count);
 }
 yield return null;

Tab_Worldbossaward s_Tab_Worldbossaward = new Tab_Worldbossaward();
 if(s_Tab_Worldbossaward.LoadTable(g_Worldbossaward))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Worldbossaward.GetInstanceFile(),g_Worldbossaward.Count);
 }
 yield return null;

Tab_Worldbosspar s_Tab_Worldbosspar = new Tab_Worldbosspar();
 if(s_Tab_Worldbosspar.LoadTable(g_Worldbosspar))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Worldbosspar.GetInstanceFile(),g_Worldbosspar.Count);
 }
 yield return null;

Tab_Yunyinghuodong s_Tab_Yunyinghuodong = new Tab_Yunyinghuodong();
 if(s_Tab_Yunyinghuodong.LoadTable(g_Yunyinghuodong))
 {
 LogModule.DebugLog("Load Table:{0} OK! Record({1})",s_Tab_Yunyinghuodong.GetInstanceFile(),g_Yunyinghuodong.Count);
 }
 yield return null;


 }

public static Tab_ChongLou GetChongLouByID(int nIdex)
 {
 if( GetChongLou().ContainsKey(nIdex))
 {
 return g_ChongLou[nIdex] as Tab_ChongLou;
 }
 return null;
 }

public static Tab_GoldPrice GetGoldPriceByID(int nIdex)
 {
 if( GetGoldPrice().ContainsKey(nIdex))
 {
 return g_GoldPrice[nIdex] as Tab_GoldPrice;
 }
 return null;
 }

public static Tab_PowerPrice GetPowerPriceByID(int nIdex)
 {
 if( GetPowerPrice().ContainsKey(nIdex))
 {
 return g_PowerPrice[nIdex] as Tab_PowerPrice;
 }
 return null;
 }

public static Tab_TableModel GetTableModelByID(int nIdex)
 {
 if( GetTableModel().ContainsKey(nIdex))
 {
 return g_TableModel[nIdex] as Tab_TableModel;
 }
 return null;
 }

public static Tab_YueKa GetYueKaByID(int nIdex)
 {
 if( GetYueKa().ContainsKey(nIdex))
 {
 return g_YueKa[nIdex] as Tab_YueKa;
 }
 return null;
 }

public static Tab_Activities GetActivitiesByID(int nIdex)
 {
 if( GetActivities().ContainsKey(nIdex))
 {
 return g_Activities[nIdex] as Tab_Activities;
 }
 return null;
 }

public static Tab_Activity GetActivityByID(int nIdex)
 {
 if( GetActivity().ContainsKey(nIdex))
 {
 return g_Activity[nIdex] as Tab_Activity;
 }
 return null;
 }

public static Tab_Appearance GetAppearanceByID(int nIdex)
 {
 if( GetAppearance().ContainsKey(nIdex))
 {
 return g_Appearance[nIdex] as Tab_Appearance;
 }
 return null;
 }

public static Tab_Audio GetAudioByID(int nIdex)
 {
 if( GetAudio().ContainsKey(nIdex))
 {
 return g_Audio[nIdex] as Tab_Audio;
 }
 return null;
 }

public static Tab_Bagua GetBaguaByID(int nIdex)
 {
 if( GetBagua().ContainsKey(nIdex))
 {
 return g_Bagua[nIdex] as Tab_Bagua;
 }
 return null;
 }

public static Tab_Btninfo GetBtninfoByID(int nIdex)
 {
 if( GetBtninfo().ContainsKey(nIdex))
 {
 return g_Btninfo[nIdex] as Tab_Btninfo;
 }
 return null;
 }

public static Tab_Buff GetBuffByID(int nIdex)
 {
 if( GetBuff().ContainsKey(nIdex))
 {
 return g_Buff[nIdex] as Tab_Buff;
 }
 return null;
 }

public static Tab_BuffEffect GetBuffEffectByID(int nIdex)
 {
 if( GetBuffEffect().ContainsKey(nIdex))
 {
 return g_BuffEffect[nIdex] as Tab_BuffEffect;
 }
 return null;
 }

public static Tab_Card GetCardByID(int nIdex)
 {
 if( GetCard().ContainsKey(nIdex))
 {
 return g_Card[nIdex] as Tab_Card;
 }
 return null;
 }

public static Tab_Cardexperience GetCardexperienceByID(int nIdex)
 {
 if( GetCardexperience().ContainsKey(nIdex))
 {
 return g_Cardexperience[nIdex] as Tab_Cardexperience;
 }
 return null;
 }

public static Tab_Cardgroup GetCardgroupByID(int nIdex)
 {
 if( GetCardgroup().ContainsKey(nIdex))
 {
 return g_Cardgroup[nIdex] as Tab_Cardgroup;
 }
 return null;
 }

public static Tab_Copy GetCopyByID(int nIdex)
 {
 if( GetCopy().ContainsKey(nIdex))
 {
 return g_Copy[nIdex] as Tab_Copy;
 }
 return null;
 }

public static Tab_Copydetail GetCopydetailByID(int nIdex)
 {
 if( GetCopydetail().ContainsKey(nIdex))
 {
 return g_Copydetail[nIdex] as Tab_Copydetail;
 }
 return null;
 }

public static Tab_Copysurprise GetCopysurpriseByID(int nIdex)
 {
 if( GetCopysurprise().ContainsKey(nIdex))
 {
 return g_Copysurprise[nIdex] as Tab_Copysurprise;
 }
 return null;
 }

public static Tab_DollarGamble GetDollarGambleByID(int nIdex)
 {
 if( GetDollarGamble().ContainsKey(nIdex))
 {
 return g_DollarGamble[nIdex] as Tab_DollarGamble;
 }
 return null;
 }

public static Tab_Drop GetDropByID(int nIdex)
 {
 if( GetDrop().ContainsKey(nIdex))
 {
 return g_Drop[nIdex] as Tab_Drop;
 }
 return null;
 }

public static Tab_Effect GetEffectByID(int nIdex)
 {
 if( GetEffect().ContainsKey(nIdex))
 {
 return g_Effect[nIdex] as Tab_Effect;
 }
 return null;
 }

public static Tab_Eightsided GetEightsidedByID(int nIdex)
 {
 if( GetEightsided().ContainsKey(nIdex))
 {
 return g_Eightsided[nIdex] as Tab_Eightsided;
 }
 return null;
 }

public static Tab_Evolve GetEvolveByID(int nIdex)
 {
 if( GetEvolve().ContainsKey(nIdex))
 {
 return g_Evolve[nIdex] as Tab_Evolve;
 }
 return null;
 }

public static Tab_Fengshui GetFengshuiByID(int nIdex)
 {
 if( GetFengshui().ContainsKey(nIdex))
 {
 return g_Fengshui[nIdex] as Tab_Fengshui;
 }
 return null;
 }

public static Tab_FriendGamble GetFriendGambleByID(int nIdex)
 {
 if( GetFriendGamble().ContainsKey(nIdex))
 {
 return g_FriendGamble[nIdex] as Tab_FriendGamble;
 }
 return null;
 }

public static Tab_GambleChancevalue GetGambleChancevalueByID(int nIdex)
 {
 if( GetGambleChancevalue().ContainsKey(nIdex))
 {
 return g_GambleChancevalue[nIdex] as Tab_GambleChancevalue;
 }
 return null;
 }

public static Tab_GambleCost GetGambleCostByID(int nIdex)
 {
 if( GetGambleCost().ContainsKey(nIdex))
 {
 return g_GambleCost[nIdex] as Tab_GambleCost;
 }
 return null;
 }

public static Tab_Iap GetIapByID(int nIdex)
 {
 if( GetIap().ContainsKey(nIdex))
 {
 return g_Iap[nIdex] as Tab_Iap;
 }
 return null;
 }

public static Tab_Idexperience GetIdexperienceByID(int nIdex)
 {
 if( GetIdexperience().ContainsKey(nIdex))
 {
 return g_Idexperience[nIdex] as Tab_Idexperience;
 }
 return null;
 }

public static Tab_Item GetItemByID(int nIdex)
 {
 if( GetItem().ContainsKey(nIdex))
 {
 return g_Item[nIdex] as Tab_Item;
 }
 return null;
 }

public static Tab_Language GetLanguageByID(int nIdex)
 {
 if( GetLanguage().ContainsKey(nIdex))
 {
 return g_Language[nIdex] as Tab_Language;
 }
 return null;
 }

public static Tab_Leaderskill GetLeaderskillByID(int nIdex)
 {
 if( GetLeaderskill().ContainsKey(nIdex))
 {
 return g_Leaderskill[nIdex] as Tab_Leaderskill;
 }
 return null;
 }

public static Tab_Popup GetPopupByID(int nIdex)
 {
 if( GetPopup().ContainsKey(nIdex))
 {
 return g_Popup[nIdex] as Tab_Popup;
 }
 return null;
 }

public static Tab_Push GetPushByID(int nIdex)
 {
 if( GetPush().ContainsKey(nIdex))
 {
 return g_Push[nIdex] as Tab_Push;
 }
 return null;
 }

public static Tab_PvpBot GetPvpBotByID(int nIdex)
 {
 if( GetPvpBot().ContainsKey(nIdex))
 {
 return g_PvpBot[nIdex] as Tab_PvpBot;
 }
 return null;
 }

public static Tab_PvpGenerate GetPvpGenerateByID(int nIdex)
 {
 if( GetPvpGenerate().ContainsKey(nIdex))
 {
 return g_PvpGenerate[nIdex] as Tab_PvpGenerate;
 }
 return null;
 }

public static Tab_PvpScore GetPvpScoreByID(int nIdex)
 {
 if( GetPvpScore().ContainsKey(nIdex))
 {
 return g_PvpScore[nIdex] as Tab_PvpScore;
 }
 return null;
 }

public static Tab_PvpShop GetPvpShopByID(int nIdex)
 {
 if( GetPvpShop().ContainsKey(nIdex))
 {
 return g_PvpShop[nIdex] as Tab_PvpShop;
 }
 return null;
 }

public static Tab_PvpnewReward GetPvpnewRewardByID(int nIdex)
 {
 if( GetPvpnewReward().ContainsKey(nIdex))
 {
 return g_PvpnewReward[nIdex] as Tab_PvpnewReward;
 }
 return null;
 }

public static Tab_Quest GetQuestByID(int nIdex)
 {
 if( GetQuest().ContainsKey(nIdex))
 {
 return g_Quest[nIdex] as Tab_Quest;
 }
 return null;
 }

public static Tab_Randomname GetRandomnameByID(int nIdex)
 {
 if( GetRandomname().ContainsKey(nIdex))
 {
 return g_Randomname[nIdex] as Tab_Randomname;
 }
 return null;
 }

public static Tab_Rollingnotice GetRollingnoticeByID(int nIdex)
 {
 if( GetRollingnotice().ContainsKey(nIdex))
 {
 return g_Rollingnotice[nIdex] as Tab_Rollingnotice;
 }
 return null;
 }

public static Tab_Scratch GetScratchByID(int nIdex)
 {
 if( GetScratch().ContainsKey(nIdex))
 {
 return g_Scratch[nIdex] as Tab_Scratch;
 }
 return null;
 }

public static Tab_ScratchCost GetScratchCostByID(int nIdex)
 {
 if( GetScratchCost().ContainsKey(nIdex))
 {
 return g_ScratchCost[nIdex] as Tab_ScratchCost;
 }
 return null;
 }

public static Tab_ScratchPrize GetScratchPrizeByID(int nIdex)
 {
 if( GetScratchPrize().ContainsKey(nIdex))
 {
 return g_ScratchPrize[nIdex] as Tab_ScratchPrize;
 }
 return null;
 }

public static Tab_Servers GetServersByID(int nIdex)
 {
 if( GetServers().ContainsKey(nIdex))
 {
 return g_Servers[nIdex] as Tab_Servers;
 }
 return null;
 }

public static Tab_ShopNotice GetShopNoticeByID(int nIdex)
 {
 if( GetShopNotice().ContainsKey(nIdex))
 {
 return g_ShopNotice[nIdex] as Tab_ShopNotice;
 }
 return null;
 }

public static Tab_Shopping GetShoppingByID(int nIdex)
 {
 if( GetShopping().ContainsKey(nIdex))
 {
 return g_Shopping[nIdex] as Tab_Shopping;
 }
 return null;
 }

public static Tab_Skill GetSkillByID(int nIdex)
 {
 if( GetSkill().ContainsKey(nIdex))
 {
 return g_Skill[nIdex] as Tab_Skill;
 }
 return null;
 }

public static Tab_SkillDisplay GetSkillDisplayByID(int nIdex)
 {
 if( GetSkillDisplay().ContainsKey(nIdex))
 {
 return g_SkillDisplay[nIdex] as Tab_SkillDisplay;
 }
 return null;
 }

public static Tab_Skillbasicchance GetSkillbasicchanceByID(int nIdex)
 {
 if( GetSkillbasicchance().ContainsKey(nIdex))
 {
 return g_Skillbasicchance[nIdex] as Tab_Skillbasicchance;
 }
 return null;
 }

public static Tab_Skillgroup GetSkillgroupByID(int nIdex)
 {
 if( GetSkillgroup().ContainsKey(nIdex))
 {
 return g_Skillgroup[nIdex] as Tab_Skillgroup;
 }
 return null;
 }

public static Tab_Skillupdate GetSkillupdateByID(int nIdex)
 {
 if( GetSkillupdate().ContainsKey(nIdex))
 {
 return g_Skillupdate[nIdex] as Tab_Skillupdate;
 }
 return null;
 }

public static Tab_StarGamble GetStarGambleByID(int nIdex)
 {
 if( GetStarGamble().ContainsKey(nIdex))
 {
 return g_StarGamble[nIdex] as Tab_StarGamble;
 }
 return null;
 }

public static Tab_Studyskill GetStudyskillByID(int nIdex)
 {
 if( GetStudyskill().ContainsKey(nIdex))
 {
 return g_Studyskill[nIdex] as Tab_Studyskill;
 }
 return null;
 }

public static Tab_Worldboss GetWorldbossByID(int nIdex)
 {
 if( GetWorldboss().ContainsKey(nIdex))
 {
 return g_Worldboss[nIdex] as Tab_Worldboss;
 }
 return null;
 }

public static Tab_Worldbossaward GetWorldbossawardByID(int nIdex)
 {
 if( GetWorldbossaward().ContainsKey(nIdex))
 {
 return g_Worldbossaward[nIdex] as Tab_Worldbossaward;
 }
 return null;
 }

public static Tab_Worldbosspar GetWorldbossparByID(int nIdex)
 {
 if( GetWorldbosspar().ContainsKey(nIdex))
 {
 return g_Worldbosspar[nIdex] as Tab_Worldbosspar;
 }
 return null;
 }

public static Tab_Yunyinghuodong GetYunyinghuodongByID(int nIdex)
 {
 if( GetYunyinghuodong().ContainsKey(nIdex))
 {
 return g_Yunyinghuodong[nIdex] as Tab_Yunyinghuodong;
 }
 return null;
 }


}
}

