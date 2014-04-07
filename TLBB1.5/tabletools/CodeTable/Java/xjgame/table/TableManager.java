//This code create by CodeEngine mrd.cyou.com ,don't modify
package xjgame.table;

import java.util.ArrayList;
 import java.util.HashMap;
 import java.util.List;
 
 import org.slf4j.Logger;
 import org.slf4j.LoggerFactory;
 
 import cyou.mrd.Platform;
 import cyou.mrd.service.Service;
 import cyou.mrd.util.ConfigKeys;
 import cyou.mrd.util.Utils;

public class TableManager implements Service{
 private static final Logger log = LoggerFactory.getLogger( TableManager.class);
 public static boolean ReaderPList(String fileName,ITableOperate opt,Object data){
 try {
 List<String> valueList = Utils.getLinesFormTXTStarFrom0(Platform.getConfiguration().getString(ConfigKeys.SERVER_DATA_DIR) + fileName);
 if(valueList==null || valueList.size() ==0) {
 throw TableException.ErrorReader("Read %s Error as not any data", fileName);
 }
 int nLine=0;
 for (String string : valueList) {
 ArrayList<String> rLine = new ArrayList<String>();
 String strlist[] = string.split("\t");
 if(strlist == null || strlist.length ==0) {
 throw TableException.ErrorReader("Read %s Error at line:%d", fileName,nLine);
 }
 String sKeyString = strlist[0];
 for(int i=1;i<strlist.length;++i) {
 rLine.add(strlist[i]);
 }
 opt.SerializableTable(rLine,sKeyString, data);
 ++nLine;
 }
 } catch (Exception e) {
 e.printStackTrace();
 log.info("ERROR:"+e.getMessage());
 return false;
 }
 return true;
 }
 
 @Override
 public String getId() { 
 return "TableManager";
 }
 
 @Override
 public void startup() throws Exception {
 if(!TableManager.InitTable())
 {
 throw TableException.ErrorReader("init Table Error!!!!!!!!!!!!!"); 
 }
 }
 
 @Override
 public void shutdown() throws Exception {
 
 }

public static HashMap<Integer,Table_ChongLou> g_ChongLou = new HashMap<Integer,Table_ChongLou>();

public static HashMap<Integer,Table_GoldPrice> g_GoldPrice = new HashMap<Integer,Table_GoldPrice>();

public static HashMap<Integer,Table_PowerPrice> g_PowerPrice = new HashMap<Integer,Table_PowerPrice>();

public static HashMap<Integer,Table_TableModel> g_TableModel = new HashMap<Integer,Table_TableModel>();

public static HashMap<Integer,Table_YueKa> g_YueKa = new HashMap<Integer,Table_YueKa>();

public static HashMap<Integer,Table_Activities> g_Activities = new HashMap<Integer,Table_Activities>();

public static HashMap<Integer,Table_Activity> g_Activity = new HashMap<Integer,Table_Activity>();

public static HashMap<Integer,Table_Appearance> g_Appearance = new HashMap<Integer,Table_Appearance>();

public static HashMap<Integer,Table_Audio> g_Audio = new HashMap<Integer,Table_Audio>();

public static HashMap<Integer,Table_Bagua> g_Bagua = new HashMap<Integer,Table_Bagua>();

public static HashMap<Integer,Table_Btninfo> g_Btninfo = new HashMap<Integer,Table_Btninfo>();

public static HashMap<Integer,Table_Buff> g_Buff = new HashMap<Integer,Table_Buff>();

public static HashMap<Integer,Table_BuffEffect> g_BuffEffect = new HashMap<Integer,Table_BuffEffect>();

public static HashMap<Integer,Table_Card> g_Card = new HashMap<Integer,Table_Card>();

public static HashMap<Integer,Table_Cardexperience> g_Cardexperience = new HashMap<Integer,Table_Cardexperience>();

public static HashMap<Integer,Table_Cardgroup> g_Cardgroup = new HashMap<Integer,Table_Cardgroup>();

public static HashMap<Integer,Table_Copy> g_Copy = new HashMap<Integer,Table_Copy>();

public static HashMap<Integer,Table_Copydetail> g_Copydetail = new HashMap<Integer,Table_Copydetail>();

public static HashMap<Integer,Table_Copysurprise> g_Copysurprise = new HashMap<Integer,Table_Copysurprise>();

public static HashMap<Integer,Table_DollarGamble> g_DollarGamble = new HashMap<Integer,Table_DollarGamble>();

public static HashMap<Integer,Table_Drop> g_Drop = new HashMap<Integer,Table_Drop>();

public static HashMap<Integer,Table_Effect> g_Effect = new HashMap<Integer,Table_Effect>();

public static HashMap<Integer,Table_Eightsided> g_Eightsided = new HashMap<Integer,Table_Eightsided>();

public static HashMap<Integer,Table_Evolve> g_Evolve = new HashMap<Integer,Table_Evolve>();

public static HashMap<Integer,Table_Fengshui> g_Fengshui = new HashMap<Integer,Table_Fengshui>();

public static HashMap<Integer,Table_FriendGamble> g_FriendGamble = new HashMap<Integer,Table_FriendGamble>();

public static HashMap<Integer,Table_GambleChancevalue> g_GambleChancevalue = new HashMap<Integer,Table_GambleChancevalue>();

public static HashMap<Integer,Table_GambleCost> g_GambleCost = new HashMap<Integer,Table_GambleCost>();

public static HashMap<Integer,Table_Iap> g_Iap = new HashMap<Integer,Table_Iap>();

public static HashMap<Integer,Table_Idexperience> g_Idexperience = new HashMap<Integer,Table_Idexperience>();

public static HashMap<Integer,Table_Item> g_Item = new HashMap<Integer,Table_Item>();

public static HashMap<Integer,Table_Language> g_Language = new HashMap<Integer,Table_Language>();

public static HashMap<Integer,Table_Leaderskill> g_Leaderskill = new HashMap<Integer,Table_Leaderskill>();

public static HashMap<Integer,Table_Popup> g_Popup = new HashMap<Integer,Table_Popup>();

public static HashMap<Integer,Table_Push> g_Push = new HashMap<Integer,Table_Push>();

public static HashMap<Integer,Table_PvpBot> g_PvpBot = new HashMap<Integer,Table_PvpBot>();

public static HashMap<Integer,Table_PvpGenerate> g_PvpGenerate = new HashMap<Integer,Table_PvpGenerate>();

public static HashMap<Integer,Table_PvpScore> g_PvpScore = new HashMap<Integer,Table_PvpScore>();

public static HashMap<Integer,Table_PvpShop> g_PvpShop = new HashMap<Integer,Table_PvpShop>();

public static HashMap<Integer,Table_PvpnewReward> g_PvpnewReward = new HashMap<Integer,Table_PvpnewReward>();

public static HashMap<Integer,Table_Quest> g_Quest = new HashMap<Integer,Table_Quest>();

public static HashMap<Integer,Table_Randomname> g_Randomname = new HashMap<Integer,Table_Randomname>();

public static HashMap<Integer,Table_Rollingnotice> g_Rollingnotice = new HashMap<Integer,Table_Rollingnotice>();

public static HashMap<Integer,Table_Scratch> g_Scratch = new HashMap<Integer,Table_Scratch>();

public static HashMap<Integer,Table_ScratchCost> g_ScratchCost = new HashMap<Integer,Table_ScratchCost>();

public static HashMap<Integer,Table_ScratchPrize> g_ScratchPrize = new HashMap<Integer,Table_ScratchPrize>();

public static HashMap<Integer,Table_Servers> g_Servers = new HashMap<Integer,Table_Servers>();

public static HashMap<Integer,Table_ShopNotice> g_ShopNotice = new HashMap<Integer,Table_ShopNotice>();

public static HashMap<Integer,Table_Shopping> g_Shopping = new HashMap<Integer,Table_Shopping>();

public static HashMap<Integer,Table_Skill> g_Skill = new HashMap<Integer,Table_Skill>();

public static HashMap<Integer,Table_SkillDisplay> g_SkillDisplay = new HashMap<Integer,Table_SkillDisplay>();

public static HashMap<Integer,Table_Skillbasicchance> g_Skillbasicchance = new HashMap<Integer,Table_Skillbasicchance>();

public static HashMap<Integer,Table_Skillgroup> g_Skillgroup = new HashMap<Integer,Table_Skillgroup>();

public static HashMap<Integer,Table_Skillupdate> g_Skillupdate = new HashMap<Integer,Table_Skillupdate>();

public static HashMap<Integer,Table_StarGamble> g_StarGamble = new HashMap<Integer,Table_StarGamble>();

public static HashMap<Integer,Table_Studyskill> g_Studyskill = new HashMap<Integer,Table_Studyskill>();

public static HashMap<Integer,Table_Worldboss> g_Worldboss = new HashMap<Integer,Table_Worldboss>();

public static HashMap<Integer,Table_Worldbossaward> g_Worldbossaward = new HashMap<Integer,Table_Worldbossaward>();

public static HashMap<Integer,Table_Worldbosspar> g_Worldbosspar = new HashMap<Integer,Table_Worldbosspar>();

public static HashMap<Integer,Table_Yunyinghuodong> g_Yunyinghuodong = new HashMap<Integer,Table_Yunyinghuodong>();

public static boolean InitTable() throws TableException
 {
 Table_ChongLou s_ChongLou = new Table_ChongLou();
 if(!s_ChongLou.LoadTable(g_ChongLou))
 {
 log.error("Load Table:"+s_ChongLou.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_ChongLou.GetInstanceFile()+"OK!!!");
 }

Table_GoldPrice s_GoldPrice = new Table_GoldPrice();
 if(!s_GoldPrice.LoadTable(g_GoldPrice))
 {
 log.error("Load Table:"+s_GoldPrice.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_GoldPrice.GetInstanceFile()+"OK!!!");
 }

Table_PowerPrice s_PowerPrice = new Table_PowerPrice();
 if(!s_PowerPrice.LoadTable(g_PowerPrice))
 {
 log.error("Load Table:"+s_PowerPrice.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_PowerPrice.GetInstanceFile()+"OK!!!");
 }

Table_TableModel s_TableModel = new Table_TableModel();
 if(!s_TableModel.LoadTable(g_TableModel))
 {
 log.error("Load Table:"+s_TableModel.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_TableModel.GetInstanceFile()+"OK!!!");
 }

Table_YueKa s_YueKa = new Table_YueKa();
 if(!s_YueKa.LoadTable(g_YueKa))
 {
 log.error("Load Table:"+s_YueKa.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_YueKa.GetInstanceFile()+"OK!!!");
 }

Table_Activities s_Activities = new Table_Activities();
 if(!s_Activities.LoadTable(g_Activities))
 {
 log.error("Load Table:"+s_Activities.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Activities.GetInstanceFile()+"OK!!!");
 }

Table_Activity s_Activity = new Table_Activity();
 if(!s_Activity.LoadTable(g_Activity))
 {
 log.error("Load Table:"+s_Activity.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Activity.GetInstanceFile()+"OK!!!");
 }

Table_Appearance s_Appearance = new Table_Appearance();
 if(!s_Appearance.LoadTable(g_Appearance))
 {
 log.error("Load Table:"+s_Appearance.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Appearance.GetInstanceFile()+"OK!!!");
 }

Table_Audio s_Audio = new Table_Audio();
 if(!s_Audio.LoadTable(g_Audio))
 {
 log.error("Load Table:"+s_Audio.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Audio.GetInstanceFile()+"OK!!!");
 }

Table_Bagua s_Bagua = new Table_Bagua();
 if(!s_Bagua.LoadTable(g_Bagua))
 {
 log.error("Load Table:"+s_Bagua.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Bagua.GetInstanceFile()+"OK!!!");
 }

Table_Btninfo s_Btninfo = new Table_Btninfo();
 if(!s_Btninfo.LoadTable(g_Btninfo))
 {
 log.error("Load Table:"+s_Btninfo.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Btninfo.GetInstanceFile()+"OK!!!");
 }

Table_Buff s_Buff = new Table_Buff();
 if(!s_Buff.LoadTable(g_Buff))
 {
 log.error("Load Table:"+s_Buff.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Buff.GetInstanceFile()+"OK!!!");
 }

Table_BuffEffect s_BuffEffect = new Table_BuffEffect();
 if(!s_BuffEffect.LoadTable(g_BuffEffect))
 {
 log.error("Load Table:"+s_BuffEffect.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_BuffEffect.GetInstanceFile()+"OK!!!");
 }

Table_Card s_Card = new Table_Card();
 if(!s_Card.LoadTable(g_Card))
 {
 log.error("Load Table:"+s_Card.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Card.GetInstanceFile()+"OK!!!");
 }

Table_Cardexperience s_Cardexperience = new Table_Cardexperience();
 if(!s_Cardexperience.LoadTable(g_Cardexperience))
 {
 log.error("Load Table:"+s_Cardexperience.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Cardexperience.GetInstanceFile()+"OK!!!");
 }

Table_Cardgroup s_Cardgroup = new Table_Cardgroup();
 if(!s_Cardgroup.LoadTable(g_Cardgroup))
 {
 log.error("Load Table:"+s_Cardgroup.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Cardgroup.GetInstanceFile()+"OK!!!");
 }

Table_Copy s_Copy = new Table_Copy();
 if(!s_Copy.LoadTable(g_Copy))
 {
 log.error("Load Table:"+s_Copy.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Copy.GetInstanceFile()+"OK!!!");
 }

Table_Copydetail s_Copydetail = new Table_Copydetail();
 if(!s_Copydetail.LoadTable(g_Copydetail))
 {
 log.error("Load Table:"+s_Copydetail.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Copydetail.GetInstanceFile()+"OK!!!");
 }

Table_Copysurprise s_Copysurprise = new Table_Copysurprise();
 if(!s_Copysurprise.LoadTable(g_Copysurprise))
 {
 log.error("Load Table:"+s_Copysurprise.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Copysurprise.GetInstanceFile()+"OK!!!");
 }

Table_DollarGamble s_DollarGamble = new Table_DollarGamble();
 if(!s_DollarGamble.LoadTable(g_DollarGamble))
 {
 log.error("Load Table:"+s_DollarGamble.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_DollarGamble.GetInstanceFile()+"OK!!!");
 }

Table_Drop s_Drop = new Table_Drop();
 if(!s_Drop.LoadTable(g_Drop))
 {
 log.error("Load Table:"+s_Drop.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Drop.GetInstanceFile()+"OK!!!");
 }

Table_Effect s_Effect = new Table_Effect();
 if(!s_Effect.LoadTable(g_Effect))
 {
 log.error("Load Table:"+s_Effect.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Effect.GetInstanceFile()+"OK!!!");
 }

Table_Eightsided s_Eightsided = new Table_Eightsided();
 if(!s_Eightsided.LoadTable(g_Eightsided))
 {
 log.error("Load Table:"+s_Eightsided.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Eightsided.GetInstanceFile()+"OK!!!");
 }

Table_Evolve s_Evolve = new Table_Evolve();
 if(!s_Evolve.LoadTable(g_Evolve))
 {
 log.error("Load Table:"+s_Evolve.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Evolve.GetInstanceFile()+"OK!!!");
 }

Table_Fengshui s_Fengshui = new Table_Fengshui();
 if(!s_Fengshui.LoadTable(g_Fengshui))
 {
 log.error("Load Table:"+s_Fengshui.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Fengshui.GetInstanceFile()+"OK!!!");
 }

Table_FriendGamble s_FriendGamble = new Table_FriendGamble();
 if(!s_FriendGamble.LoadTable(g_FriendGamble))
 {
 log.error("Load Table:"+s_FriendGamble.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_FriendGamble.GetInstanceFile()+"OK!!!");
 }

Table_GambleChancevalue s_GambleChancevalue = new Table_GambleChancevalue();
 if(!s_GambleChancevalue.LoadTable(g_GambleChancevalue))
 {
 log.error("Load Table:"+s_GambleChancevalue.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_GambleChancevalue.GetInstanceFile()+"OK!!!");
 }

Table_GambleCost s_GambleCost = new Table_GambleCost();
 if(!s_GambleCost.LoadTable(g_GambleCost))
 {
 log.error("Load Table:"+s_GambleCost.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_GambleCost.GetInstanceFile()+"OK!!!");
 }

Table_Iap s_Iap = new Table_Iap();
 if(!s_Iap.LoadTable(g_Iap))
 {
 log.error("Load Table:"+s_Iap.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Iap.GetInstanceFile()+"OK!!!");
 }

Table_Idexperience s_Idexperience = new Table_Idexperience();
 if(!s_Idexperience.LoadTable(g_Idexperience))
 {
 log.error("Load Table:"+s_Idexperience.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Idexperience.GetInstanceFile()+"OK!!!");
 }

Table_Item s_Item = new Table_Item();
 if(!s_Item.LoadTable(g_Item))
 {
 log.error("Load Table:"+s_Item.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Item.GetInstanceFile()+"OK!!!");
 }

Table_Language s_Language = new Table_Language();
 if(!s_Language.LoadTable(g_Language))
 {
 log.error("Load Table:"+s_Language.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Language.GetInstanceFile()+"OK!!!");
 }

Table_Leaderskill s_Leaderskill = new Table_Leaderskill();
 if(!s_Leaderskill.LoadTable(g_Leaderskill))
 {
 log.error("Load Table:"+s_Leaderskill.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Leaderskill.GetInstanceFile()+"OK!!!");
 }

Table_Popup s_Popup = new Table_Popup();
 if(!s_Popup.LoadTable(g_Popup))
 {
 log.error("Load Table:"+s_Popup.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Popup.GetInstanceFile()+"OK!!!");
 }

Table_Push s_Push = new Table_Push();
 if(!s_Push.LoadTable(g_Push))
 {
 log.error("Load Table:"+s_Push.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Push.GetInstanceFile()+"OK!!!");
 }

Table_PvpBot s_PvpBot = new Table_PvpBot();
 if(!s_PvpBot.LoadTable(g_PvpBot))
 {
 log.error("Load Table:"+s_PvpBot.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_PvpBot.GetInstanceFile()+"OK!!!");
 }

Table_PvpGenerate s_PvpGenerate = new Table_PvpGenerate();
 if(!s_PvpGenerate.LoadTable(g_PvpGenerate))
 {
 log.error("Load Table:"+s_PvpGenerate.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_PvpGenerate.GetInstanceFile()+"OK!!!");
 }

Table_PvpScore s_PvpScore = new Table_PvpScore();
 if(!s_PvpScore.LoadTable(g_PvpScore))
 {
 log.error("Load Table:"+s_PvpScore.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_PvpScore.GetInstanceFile()+"OK!!!");
 }

Table_PvpShop s_PvpShop = new Table_PvpShop();
 if(!s_PvpShop.LoadTable(g_PvpShop))
 {
 log.error("Load Table:"+s_PvpShop.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_PvpShop.GetInstanceFile()+"OK!!!");
 }

Table_PvpnewReward s_PvpnewReward = new Table_PvpnewReward();
 if(!s_PvpnewReward.LoadTable(g_PvpnewReward))
 {
 log.error("Load Table:"+s_PvpnewReward.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_PvpnewReward.GetInstanceFile()+"OK!!!");
 }

Table_Quest s_Quest = new Table_Quest();
 if(!s_Quest.LoadTable(g_Quest))
 {
 log.error("Load Table:"+s_Quest.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Quest.GetInstanceFile()+"OK!!!");
 }

Table_Randomname s_Randomname = new Table_Randomname();
 if(!s_Randomname.LoadTable(g_Randomname))
 {
 log.error("Load Table:"+s_Randomname.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Randomname.GetInstanceFile()+"OK!!!");
 }

Table_Rollingnotice s_Rollingnotice = new Table_Rollingnotice();
 if(!s_Rollingnotice.LoadTable(g_Rollingnotice))
 {
 log.error("Load Table:"+s_Rollingnotice.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Rollingnotice.GetInstanceFile()+"OK!!!");
 }

Table_Scratch s_Scratch = new Table_Scratch();
 if(!s_Scratch.LoadTable(g_Scratch))
 {
 log.error("Load Table:"+s_Scratch.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Scratch.GetInstanceFile()+"OK!!!");
 }

Table_ScratchCost s_ScratchCost = new Table_ScratchCost();
 if(!s_ScratchCost.LoadTable(g_ScratchCost))
 {
 log.error("Load Table:"+s_ScratchCost.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_ScratchCost.GetInstanceFile()+"OK!!!");
 }

Table_ScratchPrize s_ScratchPrize = new Table_ScratchPrize();
 if(!s_ScratchPrize.LoadTable(g_ScratchPrize))
 {
 log.error("Load Table:"+s_ScratchPrize.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_ScratchPrize.GetInstanceFile()+"OK!!!");
 }

Table_Servers s_Servers = new Table_Servers();
 if(!s_Servers.LoadTable(g_Servers))
 {
 log.error("Load Table:"+s_Servers.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Servers.GetInstanceFile()+"OK!!!");
 }

Table_ShopNotice s_ShopNotice = new Table_ShopNotice();
 if(!s_ShopNotice.LoadTable(g_ShopNotice))
 {
 log.error("Load Table:"+s_ShopNotice.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_ShopNotice.GetInstanceFile()+"OK!!!");
 }

Table_Shopping s_Shopping = new Table_Shopping();
 if(!s_Shopping.LoadTable(g_Shopping))
 {
 log.error("Load Table:"+s_Shopping.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Shopping.GetInstanceFile()+"OK!!!");
 }

Table_Skill s_Skill = new Table_Skill();
 if(!s_Skill.LoadTable(g_Skill))
 {
 log.error("Load Table:"+s_Skill.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Skill.GetInstanceFile()+"OK!!!");
 }

Table_SkillDisplay s_SkillDisplay = new Table_SkillDisplay();
 if(!s_SkillDisplay.LoadTable(g_SkillDisplay))
 {
 log.error("Load Table:"+s_SkillDisplay.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_SkillDisplay.GetInstanceFile()+"OK!!!");
 }

Table_Skillbasicchance s_Skillbasicchance = new Table_Skillbasicchance();
 if(!s_Skillbasicchance.LoadTable(g_Skillbasicchance))
 {
 log.error("Load Table:"+s_Skillbasicchance.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Skillbasicchance.GetInstanceFile()+"OK!!!");
 }

Table_Skillgroup s_Skillgroup = new Table_Skillgroup();
 if(!s_Skillgroup.LoadTable(g_Skillgroup))
 {
 log.error("Load Table:"+s_Skillgroup.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Skillgroup.GetInstanceFile()+"OK!!!");
 }

Table_Skillupdate s_Skillupdate = new Table_Skillupdate();
 if(!s_Skillupdate.LoadTable(g_Skillupdate))
 {
 log.error("Load Table:"+s_Skillupdate.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Skillupdate.GetInstanceFile()+"OK!!!");
 }

Table_StarGamble s_StarGamble = new Table_StarGamble();
 if(!s_StarGamble.LoadTable(g_StarGamble))
 {
 log.error("Load Table:"+s_StarGamble.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_StarGamble.GetInstanceFile()+"OK!!!");
 }

Table_Studyskill s_Studyskill = new Table_Studyskill();
 if(!s_Studyskill.LoadTable(g_Studyskill))
 {
 log.error("Load Table:"+s_Studyskill.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Studyskill.GetInstanceFile()+"OK!!!");
 }

Table_Worldboss s_Worldboss = new Table_Worldboss();
 if(!s_Worldboss.LoadTable(g_Worldboss))
 {
 log.error("Load Table:"+s_Worldboss.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Worldboss.GetInstanceFile()+"OK!!!");
 }

Table_Worldbossaward s_Worldbossaward = new Table_Worldbossaward();
 if(!s_Worldbossaward.LoadTable(g_Worldbossaward))
 {
 log.error("Load Table:"+s_Worldbossaward.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Worldbossaward.GetInstanceFile()+"OK!!!");
 }

Table_Worldbosspar s_Worldbosspar = new Table_Worldbosspar();
 if(!s_Worldbosspar.LoadTable(g_Worldbosspar))
 {
 log.error("Load Table:"+s_Worldbosspar.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Worldbosspar.GetInstanceFile()+"OK!!!");
 }

Table_Yunyinghuodong s_Yunyinghuodong = new Table_Yunyinghuodong();
 if(!s_Yunyinghuodong.LoadTable(g_Yunyinghuodong))
 {
 log.error("Load Table:"+s_Yunyinghuodong.GetInstanceFile()+"ERROR!!!");
 return false; }
 else
 {
 log.debug("Load Table:"+s_Yunyinghuodong.GetInstanceFile()+"OK!!!");
 }


 return true;
 }

public static Table_ChongLou GetChongLouByID(int nIdex)
 {
 return g_ChongLou.get(nIdex);
 }

public static Table_GoldPrice GetGoldPriceByID(int nIdex)
 {
 return g_GoldPrice.get(nIdex);
 }

public static Table_PowerPrice GetPowerPriceByID(int nIdex)
 {
 return g_PowerPrice.get(nIdex);
 }

public static Table_TableModel GetTableModelByID(int nIdex)
 {
 return g_TableModel.get(nIdex);
 }

public static Table_YueKa GetYueKaByID(int nIdex)
 {
 return g_YueKa.get(nIdex);
 }

public static Table_Activities GetActivitiesByID(int nIdex)
 {
 return g_Activities.get(nIdex);
 }

public static Table_Activity GetActivityByID(int nIdex)
 {
 return g_Activity.get(nIdex);
 }

public static Table_Appearance GetAppearanceByID(int nIdex)
 {
 return g_Appearance.get(nIdex);
 }

public static Table_Audio GetAudioByID(int nIdex)
 {
 return g_Audio.get(nIdex);
 }

public static Table_Bagua GetBaguaByID(int nIdex)
 {
 return g_Bagua.get(nIdex);
 }

public static Table_Btninfo GetBtninfoByID(int nIdex)
 {
 return g_Btninfo.get(nIdex);
 }

public static Table_Buff GetBuffByID(int nIdex)
 {
 return g_Buff.get(nIdex);
 }

public static Table_BuffEffect GetBuffEffectByID(int nIdex)
 {
 return g_BuffEffect.get(nIdex);
 }

public static Table_Card GetCardByID(int nIdex)
 {
 return g_Card.get(nIdex);
 }

public static Table_Cardexperience GetCardexperienceByID(int nIdex)
 {
 return g_Cardexperience.get(nIdex);
 }

public static Table_Cardgroup GetCardgroupByID(int nIdex)
 {
 return g_Cardgroup.get(nIdex);
 }

public static Table_Copy GetCopyByID(int nIdex)
 {
 return g_Copy.get(nIdex);
 }

public static Table_Copydetail GetCopydetailByID(int nIdex)
 {
 return g_Copydetail.get(nIdex);
 }

public static Table_Copysurprise GetCopysurpriseByID(int nIdex)
 {
 return g_Copysurprise.get(nIdex);
 }

public static Table_DollarGamble GetDollarGambleByID(int nIdex)
 {
 return g_DollarGamble.get(nIdex);
 }

public static Table_Drop GetDropByID(int nIdex)
 {
 return g_Drop.get(nIdex);
 }

public static Table_Effect GetEffectByID(int nIdex)
 {
 return g_Effect.get(nIdex);
 }

public static Table_Eightsided GetEightsidedByID(int nIdex)
 {
 return g_Eightsided.get(nIdex);
 }

public static Table_Evolve GetEvolveByID(int nIdex)
 {
 return g_Evolve.get(nIdex);
 }

public static Table_Fengshui GetFengshuiByID(int nIdex)
 {
 return g_Fengshui.get(nIdex);
 }

public static Table_FriendGamble GetFriendGambleByID(int nIdex)
 {
 return g_FriendGamble.get(nIdex);
 }

public static Table_GambleChancevalue GetGambleChancevalueByID(int nIdex)
 {
 return g_GambleChancevalue.get(nIdex);
 }

public static Table_GambleCost GetGambleCostByID(int nIdex)
 {
 return g_GambleCost.get(nIdex);
 }

public static Table_Iap GetIapByID(int nIdex)
 {
 return g_Iap.get(nIdex);
 }

public static Table_Idexperience GetIdexperienceByID(int nIdex)
 {
 return g_Idexperience.get(nIdex);
 }

public static Table_Item GetItemByID(int nIdex)
 {
 return g_Item.get(nIdex);
 }

public static Table_Language GetLanguageByID(int nIdex)
 {
 return g_Language.get(nIdex);
 }

public static Table_Leaderskill GetLeaderskillByID(int nIdex)
 {
 return g_Leaderskill.get(nIdex);
 }

public static Table_Popup GetPopupByID(int nIdex)
 {
 return g_Popup.get(nIdex);
 }

public static Table_Push GetPushByID(int nIdex)
 {
 return g_Push.get(nIdex);
 }

public static Table_PvpBot GetPvpBotByID(int nIdex)
 {
 return g_PvpBot.get(nIdex);
 }

public static Table_PvpGenerate GetPvpGenerateByID(int nIdex)
 {
 return g_PvpGenerate.get(nIdex);
 }

public static Table_PvpScore GetPvpScoreByID(int nIdex)
 {
 return g_PvpScore.get(nIdex);
 }

public static Table_PvpShop GetPvpShopByID(int nIdex)
 {
 return g_PvpShop.get(nIdex);
 }

public static Table_PvpnewReward GetPvpnewRewardByID(int nIdex)
 {
 return g_PvpnewReward.get(nIdex);
 }

public static Table_Quest GetQuestByID(int nIdex)
 {
 return g_Quest.get(nIdex);
 }

public static Table_Randomname GetRandomnameByID(int nIdex)
 {
 return g_Randomname.get(nIdex);
 }

public static Table_Rollingnotice GetRollingnoticeByID(int nIdex)
 {
 return g_Rollingnotice.get(nIdex);
 }

public static Table_Scratch GetScratchByID(int nIdex)
 {
 return g_Scratch.get(nIdex);
 }

public static Table_ScratchCost GetScratchCostByID(int nIdex)
 {
 return g_ScratchCost.get(nIdex);
 }

public static Table_ScratchPrize GetScratchPrizeByID(int nIdex)
 {
 return g_ScratchPrize.get(nIdex);
 }

public static Table_Servers GetServersByID(int nIdex)
 {
 return g_Servers.get(nIdex);
 }

public static Table_ShopNotice GetShopNoticeByID(int nIdex)
 {
 return g_ShopNotice.get(nIdex);
 }

public static Table_Shopping GetShoppingByID(int nIdex)
 {
 return g_Shopping.get(nIdex);
 }

public static Table_Skill GetSkillByID(int nIdex)
 {
 return g_Skill.get(nIdex);
 }

public static Table_SkillDisplay GetSkillDisplayByID(int nIdex)
 {
 return g_SkillDisplay.get(nIdex);
 }

public static Table_Skillbasicchance GetSkillbasicchanceByID(int nIdex)
 {
 return g_Skillbasicchance.get(nIdex);
 }

public static Table_Skillgroup GetSkillgroupByID(int nIdex)
 {
 return g_Skillgroup.get(nIdex);
 }

public static Table_Skillupdate GetSkillupdateByID(int nIdex)
 {
 return g_Skillupdate.get(nIdex);
 }

public static Table_StarGamble GetStarGambleByID(int nIdex)
 {
 return g_StarGamble.get(nIdex);
 }

public static Table_Studyskill GetStudyskillByID(int nIdex)
 {
 return g_Studyskill.get(nIdex);
 }

public static Table_Worldboss GetWorldbossByID(int nIdex)
 {
 return g_Worldboss.get(nIdex);
 }

public static Table_Worldbossaward GetWorldbossawardByID(int nIdex)
 {
 return g_Worldbossaward.get(nIdex);
 }

public static Table_Worldbosspar GetWorldbossparByID(int nIdex)
 {
 return g_Worldbosspar.get(nIdex);
 }

public static Table_Yunyinghuodong GetYunyinghuodongByID(int nIdex)
 {
 return g_Yunyinghuodong.get(nIdex);
 }


}

