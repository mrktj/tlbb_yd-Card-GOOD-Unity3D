using System;
using System.Collections.Generic;
using System.Collections;
using Games.LogicObject;
using GCGame.Table;
using xjgame.message;
using card;
using Module.Log;
using UnityEngine;
using card.net;
//using System.Linq;
namespace Games.CharacterLogic
{
    public partial class Obj_MyselfPlayer : Obj_OtherPlayer
    {
		//服务器列表里是否有90001这个ID的地址 -- 用于通过苹果审核 的审核测试服
		public bool hadBigAddress = false;
		//-------------------------------------------------
		
		//------------换卡相关数据------------
		public bool isChangeCardOpen = false;
		public double changeCardTimer = 0;
		public List<ChangeCardInfo> changeCardInfo = new List<ChangeCardInfo>();
		public UserCardItem materialChangeCard = null;
		
		//------------换卡相关数据------------
		
        private static Obj_MyselfPlayer m_Impl = null;
        public const int BATTLECARD_MAX = 5;

		//购买背包 好友上限 花费 常量
		public const int BagMaxLimit = 300;
		public const int FriendMaxLimit = 200;
		public int BuyCost = 50;
        public static bool ShowedPublicNotice = false;
		//temp notice url
		public const  string  NoticeURL = "http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_android.html";
		
//------------用户数据开始------------
		public bool isNewUser = false;
		public long accountID {  set; get; }
        public string uid { set; get; }
        public string accountName {  set; get; }
        public Int32 mail {  set; get; }
        public Int32 level {  set; get; }
        public Int32 exp {  set; get; }
        public Int32 money {  set; get; }
        public Int32 dollar {  set; get; }
		public Int32 fpoint {set; get;}
        public Int32 power {  set; get; }
        public List<UserItem> itemList { protected set; get; }
        public List<MainCopy> activityCopys { protected set; get; } //从服务器获取的活动大副本列表 - 都是曾经打过的
		public List<SubCopy> normalCopys {protected set; get;}
        public List<UserCardItem> cardBagList { protected set; get; }
		public List<MainCopy> activityMainCopys=new List<MainCopy>(); //程序维护的活动大副本列表,是从表读入的全部活动大副本信息
		public List<MainCopy> normalMainCopys=new List<MainCopy>();
		public long[] teamMemberArray = {-1, -1, -1, -1, -1};	//team, 0:team header, 1-4:team member
		public int leadership; //用户领导力
		public int purchaseDollar; //充值元宝数
		public int daysAfterFirstLogin; //距离首次登录的天数
		public int powerTime {  set; get; }//赠送体力倒计时
		public int bagMax {  set; get; } //背包上限
		public int mailListCount = 0; //未读邮件数
		public int taskListCount = 0; //未领奖励数
		public int lotteryLuckyNum = 0;
		public int giftison = 0; //礼包激活码开关   1=开启  0=关闭
		public int lastPower=0;
		public int lastLeaderShip=0;
		public int lastHero=0;
		public int lastFriend=0;
		public int lastExp=0;
		public int lastLevel=0;
        public int receive_power_time = 0;
        public int battel_sign = 0;     //0=没有收到战斗返回消息包 1=收到错误返回包 2=收到正确战斗返回包 3=场景加载完成
        public float loading_process = 0f;
		public Int32 pataTimes = 0;
		public Int32 pataNum = 0;
		public Int32 lastPataNum = 0;
		// 王明磊 购买金币 购买体力
		public Int32 buyMoneyTimes {set; get;}
		public Int32 buyPowerTimes {set; get;}

        //月卡 xlym
        public MonthCardInfoClass monthCardInfo = new MonthCardInfoClass();
        public int monthCardFlag = 0;
        //scroll记录 xlym
        public Dictionary<string, ScrollData> scrollRecord = new Dictionary<string, ScrollData>();
		
		//area服务器分区id
		public int areaId{set;get;}
//------------用户数据结束------------
		
//------------畅游账号数据开始------------
		public int cyouType;//畅游账号登录:1.畅游账号注册:2
		public int cyouCode;//返回码--
		public long cyouAccountId;//如果成功返回账号ID
//------------畅游账号数据结束------------
		
//------------好友数据开始------------
		public int friendNumMax {set;get;}
		//public int friendShipPoint{set;get;}
		public List<UserFriend> friendsList;
		public UserFriend friendSearchResult;
		public List<AssistFriend> AssistanceList;
		//current selected assist friend
		public AssistFriend currentAssistFriend;
		//current selected my friend
		public UserFriend currentFriend;
		
//		public PlayerFriendData friendData;//JackWen 13-11-30
//------------好友数据结束------------
//------------卡牌数据开始------------
		public long selectedCardID = -1;//selected card id,use for show card info
//------------卡牌数据结束------------
 		
//------------战斗数据开始------------
		public bool isLastBattleNotFinish = false; //王明磊 记录是否为战斗中断返回的战斗结束 true = 未结束 false = 结束
		public bool isGuideBattle = false;
		public long[] battleArray ={ -1, -1,-1, -1, -1, -1};	       //--记录阵形,每个槽对应的card guid,布阵时拖动改变这个值--(这个是对应的卡牌ID )
        public int nSetState;                                           //--记录设置状态(记录以前是否有保存过)
        public int nfightFriendPos = -1;                        //助战好友的位置。(助战好友只能记录位置，没法保存ID)
		public int nGetAccountStrCheck = -1;
		public int[] cardStates = {0, 0, 0, 0, 0, 0};			//--记录出战卡牌的状态,每个槽里面的卡牌是否死亡,0:正常,1:死亡--
		public MainCopy curMainCopy = null;
		public SubCopy curSubcopy = null;
		public BattleRoundData battleData = new BattleRoundData();
		public float acceleration = 1f;
		public CopyType copyType = CopyType.NORMAL; //副本类型
		public int reviveCount = 0;
		//public bool isPVPBattle = false;
		public Games.Battle.BattleType battleType = Games.Battle.BattleType.PVE;
		public long enemyGUID = -1;
		public string enemyName = "";
        public List<LogicObject.DropBag> lastBattleDrops = new List<LogicObject.DropBag>();//中断前的战斗掉落
		public bool isAutoFowrard = false;
        public int currentCopyStar = 0; //战斗结束当前副本星级
        public List<FragmentInfo> currentFragmentList = new List<FragmentInfo>();
		public int pataRoundRewardMoney;
		public int pataRoundRewardYuanbao;
		public int pataTotalRewardMoney;
		public int pataTotalRewardYuanbao;
		public int pataClearFlag = 1;		//结算标志位，1=结算 2 = 退出（不清次数）
		public bool isSuspendinpata = false;
		
//------------战斗数据结束------------

//------------任务数据开始------------
		public bool isNextMainOpened = false; //记录下一大副本是否已经开启 非首次开启则胜利后不跳转
		public List<UserTask> taskList=new List<UserTask>();
		public UserFinishTask finishTask=new UserFinishTask();
//------------任务数据结束------------
		
//------------商城数据开始------------
		public List<PurchaseInfo> purchaseInfoList=new List<PurchaseInfo>();
		public int ifFreeGamble = 2; //标志是否每日登录赠送抽奖一次 1:是 2:否
		public double freeCD = -1; //记录当前每日登录赠送抽奖的倒计时,正常情况>=0
		public bool isPurchase = false; //记录是否从支付界面转到绑定界面		
//------------商城数据结束------------
		
//------------邮件数据开始------------
		public List<MailInfo> mailList=new List<MailInfo>();
		public MailInfo selectMail=new MailInfo();
		public int mailIsFull; //0 未满 1 满
		public int friendMailIsFull; //0 未满 1 满
		//public int mailState=0;    //1成功 2失败
//------------邮件数据结束------------
		
//------------图鉴数据开始------------
		public int heroInfoTemplateId=-1;
		public List<HeroInfo> heroList=new List<HeroInfo>();
		public List<int> newTemplateID=new List<int>();
		public int heroWindowState=0;//0 图鉴中的人物信息 1 有新卡牌加入的人物信息
//------------图鉴数据开始------------
		
//------------养成数据开始------------
		public CultivateType curCultivateType = CultivateType.UNKNOWN;//当前的养成操作类型--
		public bool isSelectHero = true;//是否要摆选择英雄,选择英雄:true,选择材料:false.
		//update
		public UserCardItem updateHeroItem = null;
		public UserCardItem[] updateMaterialItems = new UserCardItem[6];
		//evolution
		public UserCardItem evolutionHeroItem = null;
		public UserCardItem[] evolutionMaterialItems = new UserCardItem[5];
		//strengthen
		public UserCardItem strengthenHeroItem = null;

        //判断是否现在是在显示卡牌信息界面
        public bool bShowCardInfo = false;
//------------养成数据结束------------	
		
//------------改名数据开始------------	
		public int changeNameType = 0;
//------------改名数据结束------------
		
//------------PVP数据------------
		//public bool bInPvP = false;                                             //记录玩家是否是在PVP中(主要在排布阵型界面用，区分是否有助战好友)
		public List<PVPPlayerInfo> pvpPlayerInfoList { protected set; get; }	//PVP对手玩家信息列表	
		public int nHeroRank;													//用户的PVP排行
		public int nPvPTimes;													//用户PvP剩余的次数
		public long nlTotalScore;												//用户的总积分
		public PVPPlayerInfo pvpChoosePlayer = null;
		
//------------积分商城数据------------		
		public int nPvPShopSocre = 0;													//用户积分数据
		
//------------刮刮乐------------	
		public int GGLTimes = 0;
		public int GGLRewardID = 0;
		
		
//--------------八卦阵-------------
		public int BGZTimes = 0;
		public int Flags = 0;
		public int BGZRewardID = 0;

//--------------技能学习/升级-------------
        public SkillHeroType skillHeroType = SkillHeroType.LEARN_MAIN; //选择技能英雄类型
        public UserCardItem learnMainHeroItem = null;
        public UserCardItem learnChildHeroItem = null;
        public UserCardItem updateMainHeroItem = null;
        public List<UserCardItem> updateChildHeroItems = new List<UserCardItem>();

//--------------世界boss-------------
        public WorldBossClass activeBoss = new WorldBossClass();//当前开启的boss
        public WorldBossKillInfoClass lastKillInfo = new WorldBossKillInfoClass();//上一轮击杀信息
        public WorldBossAttInfoClass currentKillInfo = new WorldBossAttInfoClass();//本轮boss击杀情况
        public List<WorldBossDamageRankInfoClass> worldBossWeekRankList = new List<WorldBossDamageRankInfoClass>();//周排行
        public WorldBossDamageRankInfoClass playerRank = new WorldBossDamageRankInfoClass();//玩家在排行榜中的排名
		public long worldBossCurrentDamage = 0;//本轮伤害
		public int worldBossRewardMoney = 0;//获得金币
		public long worldBossHpBeginBattle = 0;//boss血量
        public int hasWorldBossReward = 0;//是否领取boss奖励
        public int rewardLev = 0;//奖励等级
        public int worldBossActiveFlag = 0;//是否开启 0 是 1否
        public DateTime resurgenceCD = new DateTime(); //复活cd
        public DateTime activeBossCD = new DateTime();//开启剩余时间
//--------------群雄争霸-------------
		public List<PVPPlayerInfo[]> qxzbPvPCardList;
		public long qxzbStarTime = 0;
		public long qxzbEndTime = 0;
		public long cursysTime = 0;//当前StarTime 和 endTime的系统时间
		public long curCDsysTime = 0;//当前CD的系统时间
		public int  nQxzbFightTime = 0;//挑战次数
		public long coldTime = 0; //
		public int  nQxzbMoney = 0;
		public bool nQxzbIfShowFightReward = false;
		public int  nGetRewardCurStar = 0;  //要领取奖励对应的星级
		public long lQxzbChoosePlayerGUID = 0;
		public int[] heroQxzbPvPRank = new int[5];
		public int[] heroQxzbPvPRewardID = new int[5];
		public int curPvPStar = 3; //记录当前是几星PVP
		public long[] PvPBattleArray ={ -1, -1,-1, -1, -1, -1};	       //--记录新PVP阵形
		public long curPvPLearder = -1;
        public int get_result = -1; //0表示领取成功
//------------用户方法开始------------	
        public static Obj_MyselfPlayer GetMe()
        {
            if (m_Impl == null)
            {
                m_Impl = new Obj_MyselfPlayer();
                m_Impl.InitState();
            }
            return m_Impl;
        }

        public void InitState()
        {
			this.LoadbattleArray();
            itemList = new List<UserItem>();
            activityCopys = new List<MainCopy>();
			normalCopys = new List<SubCopy>();
            cardBagList = new List<UserCardItem>();
			friendsList = new List<UserFriend>();
			AssistanceList = new List<AssistFriend>();
        }
		
		public static void ReleaseMe(){
			if (m_Impl == null){
				return;
            }
//			m_Impl.CleanPlayerPrefs();
//			m_Impl.ClearBattleArraySet();
            m_Impl = null;
        }
		
		public void CleanPlayerPrefs(){
			if(PlayerPrefs.HasKey("fightFriendPos")){
				PlayerPrefs.DeleteKey("fightFriendPos");
			}
			for(int i=0;i<6;i++){
				if(PlayerPrefs.HasKey(i.ToString())){
					PlayerPrefs.DeleteKey(i.ToString());
				}
			}
		}
		
		public UserCardItem GetUserCardByGUID(Int64 id)
		{
			foreach(UserCardItem card in cardBagList)
			{
				if(card.cardID == (long)id)
				{
					return card; 
				}
			}
			return null;
		}
		public UserItem GetUserItemByID(Int32 id)
		{
			foreach(UserItem item in itemList)
			{
				if(item.itemID == id)
				{
					return item;
				}
			}
			return null;
		}
		public int GetItemCountByType(ItemType type)
		{
			foreach(UserItem item in itemList)
			{
				if(item.itemID == (int)type)
				{
					return item.itemCount;
				}
			}
			return 0;
		}
        public void setUserInfo(SCAskUserData msg)
		{
			SetUserBaseData(msg.BaseData);
			SetUserBagData(msg.BagData);
			SetUserCopyData(msg.CopyData);

		}
		public void SetUserBaseData(PBUserBaseData msg , int mode=0)//mode:0-normal    1-afterBattle
		{
			Debug.Log("*********************xlym:serverUserBaseData************");
			Debug.Log("msg.Name="+msg.Name);
			Debug.Log("msg.Mail="+msg.Mail);
			Debug.Log("msg.Level="+msg.Level);
			Debug.Log("msg.Exp="+msg.Exp);
			Debug.Log("msg.Money="+msg.Money);
			Debug.Log("msg.Friendpoint="+msg.Friendpoint);
			Debug.Log("msg.Dollar="+msg.Dollar);
			Debug.Log("msg.Power="+msg.Power);
			Debug.Log("msg.Leadership="+msg.Leadership);
			Debug.Log("msg.NoReadMail="+msg.NoReadMail);
			Debug.Log("msg.BagCapacity="+msg.BagCapacity);
			if (msg.HasFreeCD)
				Debug.Log("msg.FreeCD="+msg.FreeCD);
			if(msg.HasLastBattleData) 
				Debug.Log("msg.lastBattleData="+msg.LastBattleData);
			Debug.Log("*********************xlym:serverUserBaseData************");
			accountName = msg.Name;
			mail = msg.Mail;
			level = msg.Level;
			exp = msg.Exp;
			money = msg.Money;
			fpoint = msg.Friendpoint;
			dollar = msg.Dollar;
			power = msg.Power;
			leadership = msg.Leadership;
			GameManager.Instance.lastPowerTime=DateTime.Now.AddSeconds(-msg.Powertime);
			lotteryLuckyNum = msg.LuckyPoint;
			bagMax = msg.BagCapacity;
			mailListCount=msg.NoReadMail;
			taskListCount=msg.NoGetQuest;
			friendNumMax = msg.FriendNumMax;
			purchaseDollar=msg.PurchaseDollar;
			daysAfterFirstLogin = msg.DaysAfterFirstLogin;
			GGLTimes = msg.GGLTimes;
			buyMoneyTimes = msg.BuyMoneyTimes;
			buyPowerTimes = msg.BuyPowerTimes;
			BGZTimes = msg.BBZTimes;
			Flags = msg.BBZFlag;
            monthCardFlag = msg.MonthCardFlag;
			pataTimes = msg.PataTimes;
			pataNum = msg.PataNum;
			
			if(msg.HasAreaid){
				areaId = msg.Areaid;
			}else{
				areaId = 0;
			}			
			
			curCDsysTime = (long)Time.time;
			Debug.LogWarning("pataNum" + pataNum);
            //战斗中断返回
			if(msg.HasLastBattleData)
			{
				DataBattle db = msg.LastBattleData;
				battleData.SetLastBattleData(db);
				if(db.HasWhichPVE && db.WhichPVE == 1)
				{
					battleType = Games.Battle.BattleType.CHONG_LOU;
					Debug.LogWarning("中断返回 pataNum" + pataNum);
					int _PataNum = pataNum;
					if(db.Win_idx != 0)
					{
						_PataNum++;
					}
					Tab_ChongLou tab_cl = TableManager.GetChongLouByID(_PataNum);
					pataRoundRewardMoney = 0;
					pataRoundRewardYuanbao = 0;
					if(tab_cl != null)
					{
						for(int c = 0; c < 2; c++)
						{
							if(tab_cl.GetCopyDropbyIndex(c) == 1)	//金币用1表示
							{
								pataRoundRewardMoney += tab_cl.GetCopyNumbyIndex(c);
							}
							else if(tab_cl.GetCopyDropbyIndex(c) == 2)	//元宝用2表示
							{
								pataRoundRewardYuanbao += tab_cl.GetCopyNumbyIndex(c);
							}
						}
					}
				}
			}
            for (int i = 0; i < msg.beforeDropsCount; i++)
            {
                LogicObject.DropBag drop_bag = new LogicObject.DropBag();
                drop_bag.type = (DropType)msg.beforeDropsList[i].Type;
                drop_bag.val = msg.beforeDropsList[i].Value;
                lastBattleDrops.Add(drop_bag);
				Debug.Log("drop_bag.type = " + drop_bag.type);
				Debug.Log("drop_bag.val = " + drop_bag.val);
            }
            Debug.Log("lastBattleDrops = " + msg.beforeDropsCount);
            
			//cb:通过服务器记录当前状态
			//Jack Wen 20131010
			if(mode == 1 && (GuideManager.GUIDE_STEP)msg.Finish_step != GuideManager.GUIDE_STEP.END)
			{
				Debug.LogWarning("*** SetUserBaseData ***   "+(GuideManager.GUIDE_STEP)msg.Finish_step);
				switch((GuideManager.GUIDE_STEP)msg.Finish_step){
					
				case GuideManager.GUIDE_STEP.COPY1_1_END:
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_1_END;
					break;
				case GuideManager.GUIDE_STEP.COPY1_2_END:
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_2_END;
					break;
				case GuideManager.GUIDE_STEP.COPY1_3_END:
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_3_END;
					break;
				case GuideManager.GUIDE_STEP.COPY1_4:
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.UPDATE;
					break;
				case GuideManager.GUIDE_STEP.COPY1_5:
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.GIFT;
					break;
				case GuideManager.GUIDE_STEP.COPY2_1:
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.END;
					break;
				case GuideManager.GUIDE_STEP.END:
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.END;
					break;
					
				default:
					break;
				}
			}
			
			Debug.LogWarning("****** Receive a userbasedata and Finish_step : "+GuideManager.Instance.currentStep);
			//王明磊 每日登录赠送抽奖
			if (msg.HasFreeCD)
				freeCD = msg.FreeCD;
			if (msg.HasFengshuiInfo)
				SetFengshiInfo(msg.FengshuiInfo);
		}
		
		public void SetUserBagData(PBUserBagData msg)
		{
            if (msg == null || msg.cardInfoCount == 0)
            {
                Debug.LogError("SetUserBagData(), msg = null !");
                return;
            }
			
			cardBagList.Clear();
			itemList.Clear();
//			bagMax=msg.BagCapacity;
//            //设置背包最大容量
//            bagMax = msg.BagCapacity;
			string[] protectCardIDs = GameManager.getProtectedCardsID();
			
			UserCardItem[] materialList = updateMaterialItems;
			updateMaterialItems = new UserCardItem[6];
			int choosedNum = 0;
			Debug.Log("*******************xlym:severBagData******************************");
			foreach(CardInfo card in msg.cardInfoList)
			{
				LogModule.DebugLog("GET Card->cardID:"+card.CardId+" templateID:"+card.TemplateId);
                
				UserCardItem item = new UserCardItem();
                item.cardID = card.CardId;
                item.templateID = card.TemplateId;
                item.level = card.Level;
				item.fightIndex = card.FightIndex;
				/* //不能这么写,因为PvPBattleArray是各个榜公用的,没有每个储存
				for(int i = 0; i < PvPBattleArray.Length; i++)
				{
					if (PvPBattleArray[i] == item.cardID)
						item.fightIndex = 
				}
                */
				item.qxzbFightIndex = card.QxzbFightIndex;
                item.quality = TableManager.GetCardByID(card.TemplateId).Star;
				item.addQualityHp = card.Add_quality_hp;
				item.addQualityAtt = card.Add_quality_att;
				item.skillLevel = card.Skill_level;
				item.cardExp = (Int32)card.CurLevExp;
				item.memberID=card.MemberID;
                item.skillStudyId = card.StudySkillID;
                item.skillStudyLev = card.StudySkillLev;
                item.skillStudyExp = card.StudySkillcurLevExp;
				LogModule.DebugLog("addQualityHp = " + item.addQualityHp + " addQualityAtt = " + item.addQualityAtt);
				
				//this.ClearBattleArraySet();
				//init card protect state
				for(int i = 0;i<protectCardIDs.Length;i++)
				{
					if(long.Parse(protectCardIDs[i]) == card.CardId)
					{
						item.isProtected = true;
						break;
					}
				}
				
				 if (item.memberID >= 0 && item.memberID < BATTLECARD_MAX)
                {
					LogModule.DebugLog("team membe index = "+item.memberID+",card guid = "+item.cardID);
					teamMemberArray[item.memberID] = item.cardID;
                }
				
//                if (item.fightIndex >= 0 && item.fightIndex < BATTLECARD_MAX)
//                {
//					LogModule.DebugLog("BattleArray->index:"+item.fightIndex+"cardID:"+item.cardID);
//                    battleArray[item.fightIndex] = item.cardID;
//                }
				
                AddCardToBag(item);
				
				if(CheckIsChoosedMaterialCard(item, materialList) && choosedNum < 6)
				{
					updateMaterialItems[choosedNum] = item;
					choosedNum++;
				}
			}
			Debug.Log("msg.cardInfoList.Count="+msg.cardInfoList.Count);
			Debug.Log("*******************xlym:severBagData******************************");
			RefreshBattleArray();
						
			foreach(xjgame.message.ItemInfo item in msg.itemInfoList)
			{
				LogModule.DebugLog("user item id:"+item.ItemId+" count:"+item.Count);
				UserItem user_item = new UserItem(item.ItemId, item.Count);
				itemList.Add(user_item);
			}
		}
		
		
		//检查是否是已经选中的材料卡牌
		private bool  CheckIsChoosedMaterialCard(UserCardItem checkCard, UserCardItem[] materialList)
		{
			if(materialList == null)
			{
				return false;
			}
			for(int i=0; i < materialList.Length; i++)
			{
				if(materialList[i] != null && materialList[i].cardID == checkCard.cardID)
				{
					return true;
				}
			}
			
			return false;
		}
		
		//设置PVP玩家信息
		public void SetPVPPlayerInfoData(SCAskPVPList msg)
		{
			
			nHeroRank = msg.NHeroRank;
			nPvPTimes = msg.NHeroPVPTimes;
			nlTotalScore = msg.NHeroScore;
			
			if(pvpPlayerInfoList == null)
			{
				pvpPlayerInfoList = new List<PVPPlayerInfo>();
			}
			else
			{
				pvpPlayerInfoList.Clear();
			}
			
			
			foreach(PVPUserBaseData PlayerData in msg.userInfoList)
			{
				PVPPlayerInfo playerInfo = new PVPPlayerInfo();
				playerInfo.nlGUID = PlayerData.NGuid;
				playerInfo.strName = PlayerData.Name;
				playerInfo.nRank = PlayerData.NRank;
				playerInfo.nLev = PlayerData.NLevel;
				playerInfo.nTempleID = PlayerData.NTempleID;
				playerInfo.nFight = PlayerData.NFight;
				playerInfo.skill_level = PlayerData.Skill_level;
				playerInfo.add_quality_att = PlayerData.Add_quality_att;
				playerInfo.add_quality_hp = PlayerData.Add_quality_hp;
                playerInfo.studySkillId = PlayerData.StudySkillID;
                playerInfo.studySkillLev = PlayerData.StudySkillLev;
				pvpPlayerInfoList.Add(playerInfo);
			}
		}
		
		public PVPPlayerInfo GetPvPPlayerInfoByGuid(long nGuid)
		{
			foreach(PVPPlayerInfo PlayerData in pvpPlayerInfoList)
			{
				if(nGuid == PlayerData.nlGUID)
				{
					return PlayerData;
				}
				
			}
			
			return null;
		}
		
		//设置pvp商城购买消息返回
		public void SetPvPShopBuyRetData(SCPVPShopRet msg)
		{
			nlTotalScore = msg.NScore;
			this.SetUserBagData(msg.BaseBagData);
		}
		
		//刷新pvp商城积分（和pvp积分一样）
		public void SetPvPShopScore(SCAskScoreShopFresh msg)
		{
			nlTotalScore = msg.NScore;
		}
		
		public void SetUserCopyData(PBUserCopyData msg)
		{
            if (msg == null)
            {
                Debug.LogError("SetUserCopyData(). msg = null!");
                return;
            }

            if (msg.activityCopyCount > 0)
            {
                activityCopys.Clear();
                foreach (CopyInfo copy in msg.activityCopyList)
                {
                    MainCopy mncopy = new MainCopy();
                    mncopy.SetData(copy);
                    activityCopys.Add(mncopy);
                }
            }
            
            if (msg.normalCopyCount > 0)
            {
                normalCopys.Clear();
                foreach (MissionInfo copy in msg.normalCopyList)
                {
                    LogModule.DebugLog("copy id = " + copy.BattleId + ", count = " + copy.Count);
                    SubCopy mncopy = new SubCopy();
                    mncopy.SetData(copy);
                    normalCopys.Add(mncopy);
                }
            }
		}

        //存储scrollbar位置 xlym
        public void SetScrollValue(string key, ScrollData scrValue)
        {
            if (scrollRecord.ContainsKey(key))
            {
                scrollRecord[key] = scrValue;
            }
            else
            {
                scrollRecord.Add(key, scrValue);
            }
        }

		/*
		登陆
		-1：Billing异常
		0：登陆成功
		1：参数不完整
		2：用户不存在
		3：密码错误
		99：账号系统异常
			
		注册
		-1：Billing异常
		0：注册成功
		1：参数不完整
		2：用户名已存在
		3：该ip地址禁止注册（黑名单）
		4：同ip注册间隔必须大于3分钟，3分钟不得超过6个
		5：该ip今天不能注册账号
		6：用户名长度非5-16位
		7：用户名首字母必须为字母或数字
		8：用户名只能使用英文字母或数字以及"_"
		9：禁用词(密码不合法)
		99：账号系统异常
		 */
		public void SetCyouLoginData(SCLoginRet msg)
		{
			cyouType = msg.Type;
			cyouCode = msg.RetCode;
			cyouAccountId = msg.AccountId;
			Debug.Log("SetCyouLoginData(), type = " + cyouType + ", code = " + cyouCode + ", accountId = " + cyouAccountId);
		}
		public void SetCyouBindData(SCBindAccount msg)
		{
			cyouCode = msg.RetCode;
            if (msg.HasAccountid) {
				cyouAccountId = msg.Accountid;
				//清除Guest时保存的信息
				AccountManager.userType = AccountManager.UserType.OldUser;
				if (PlayerPrefs.HasKey(HTTPClientAPI.uri.ToString() + "_Guest"))
				{
					string accountID = PlayerPrefs.GetString(HTTPClientAPI.uri.ToString() + "_Guest");
					if (PlayerPrefs.HasKey(accountID + "_LoginTimes"))
						PlayerPrefs.DeleteKey(accountID + "_LoginTimes");
					PlayerPrefs.DeleteKey(HTTPClientAPI.uri.ToString() + "_Guest");
				}
			}
			
			if(msg.HasUsername){
				userName = msg.Username;
				Debug.Log("userName:"+userName);
			}
			
			if(msg.HasPassward){
				passward = msg.Passward;
				if(passward == null){
					passward = "";
				}
				Debug.Log("passward:"+passward);
			}
			
			Debug.Log("SetCyouBindData(), type = " + cyouType + ", code = " + cyouCode + ", accountId = " + cyouAccountId);
		}
		
		public void SetUCLoginRetData(SCLoginThirdPlatformRet msg)
		{
			cyouCode = msg.RetCode;
            cyouAccountId = msg.AccountId;
			Debug.Log("SetUCLoginRetData(), code = " + cyouCode + ", accountId = " + cyouAccountId);
		}		
        //private List
        public void SetLoginData(SCLoginRet msg)
        {
            cyouCode = msg.RetCode;         // 快速登录加了返回值判断。
            accountID = msg.AccountId;
			Debug.Log("*********    Login receive account id = "+accountID);
			isNewUser = (msg.State == 1);
			if(isNewUser)
			{
				Debug.Log("*** *** ***    Enter NewUser setting!");
				PlayerPrefs.SetString("ACCOUNT_ID",accountID.ToString());
			}
			
			// 存储快速登陆相关信息 ----- Start -----
			if (AccountManager.userType != AccountManager.UserType.OldUser)
			{
				PlayerPrefs.SetString(HTTPClientAPI.uri.ToString() + "_Guest",accountID.ToString());
				string key = accountID.ToString() + "_LoginTimes";
				if (PlayerPrefs.HasKey(key) && PlayerPrefs.GetInt(key) > 0 && !PlayerPrefs.HasKey("InGameBackLogin"))
					PlayerPrefs.SetInt(key,2);
				else
					PlayerPrefs.SetInt(key,1);
			}
			// 存储快速登陆相关信息 ----- The End ----
			if(msg.HasUid && msg.HasUsername){
				AccountInfo ai_t = new AccountInfo();
				ai_t.email = msg.Username;
				ai_t.accountId = msg.AccountId;
				if(AccountManager.Instance.CurAccount != null)
					ai_t.password = AccountManager.Instance.CurAccount.password;
				else
					ai_t.password = "";
				AccountManager.Instance.CurAccount = ai_t;
			}
			
			//cb:通过服务器记录当前状态
			Debug.Log("*** msg.Guide_finished_step ***      ****** "+msg.Guide_finished_step);
			//int guide_int_temp = PlayerPrefs.GetInt("GUIDE_INT_TEMP" , -1);
			if(!GuideManager.Instance.isInSubStep/*guide_int_temp == 1*/){
				switch((GuideManager.GUIDE_STEP)msg.Guide_finished_step){
				
				case GuideManager.GUIDE_STEP.NONE://0
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.NONE;			break;
				case GuideManager.GUIDE_STEP.CARD_CHOOSE://100
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.LEADER;			break;
				case GuideManager.GUIDE_STEP.LEADER://101
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.TEAM_MEMBER;	break;
				case GuideManager.GUIDE_STEP.TEAM_MEMBER://102
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_1;		break;
					
				case GuideManager.GUIDE_STEP.COPY1_1_END://103
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_2;		break;			
				case GuideManager.GUIDE_STEP.COPY1_2_END://104
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_3;		break;				
				case GuideManager.GUIDE_STEP.COPY1_3_END://105
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.LOTTERY;		break;	
					
				case GuideManager.GUIDE_STEP.LOTTERY://106
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.TEAM_MEMBER2;	break;
				case GuideManager.GUIDE_STEP.TEAM_MEMBER2://107
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_4;		break;			
				case GuideManager.GUIDE_STEP.COPY1_4://108
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.UPDATE;			break;	
				case GuideManager.GUIDE_STEP.UPDATE://109
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_5;		break;
				case GuideManager.GUIDE_STEP.COPY1_5://110
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.GIFT;			break;	
				case GuideManager.GUIDE_STEP.GIFT://111
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY2_1;		break;	
				case GuideManager.GUIDE_STEP.COPY2_1://112
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.END;			break;
				case GuideManager.GUIDE_STEP.END://900
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.END;			break;
					
				}
				Debug.LogWarning("****** Receive a SCLoginRet and Finish_step : "+GuideManager.Instance.currentStep);
			}
			/*daixiugai
			*/
			Debug.Log("Login guide state:"+GuideManager.Instance.currentStep);
        }
#if UNITY_ANDROID
        public void SetUCLoginData(SCLoginThirdPlatformRet msg)
        {
            accountID = msg.AccountId;
			Debug.Log("*********    Login receive account id = "+accountID);
			//if(! PlayerPrefs.HasKey("ACCOUNT_ID")) PlayerPrefs.SetString("ACCOUNT_ID",accountID.ToString());
			isNewUser = (msg.State == 1);
			if(isNewUser)
			{
				Debug.Log("*** *** ***    Enter NewUser setting!");
				PlayerPrefs.SetString("ACCOUNT_ID",accountID.ToString());
			}
			if(msg.HasNickName){
				AccountInfo ai_t = new AccountInfo();
				ai_t.email = msg.NickName;
				ai_t.password = "";
				ai_t.accountId = long.Parse(msg.UcAccount);
				AccountManager.Instance.CurAccount = ai_t;
			}
			
			//cb:通过服务器记录当前状态
			Debug.Log("*** msg.Guide_finished_step ***      ****** "+msg.Guide_finished_step);
			int guide_int_temp = PlayerPrefs.GetInt("GUIDE_INT_TEMP" , -1);
			if(!GuideManager.Instance.isInSubStep/*guide_int_temp == 1*/){
				switch((GuideManager.GUIDE_STEP)msg.Guide_finished_step){
				
				case GuideManager.GUIDE_STEP.NONE://0
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.NONE;			break;
				case GuideManager.GUIDE_STEP.CARD_CHOOSE://100
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.LEADER;			break;
				case GuideManager.GUIDE_STEP.LEADER://101
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.TEAM_MEMBER;	break;
				case GuideManager.GUIDE_STEP.TEAM_MEMBER://102
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_1;		break;
					
				case GuideManager.GUIDE_STEP.COPY1_1_END://103
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_2;		break;			
				case GuideManager.GUIDE_STEP.COPY1_2_END://104
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_3;		break;				
				case GuideManager.GUIDE_STEP.COPY1_3_END://105
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.LOTTERY;		break;	
					
				case GuideManager.GUIDE_STEP.LOTTERY://106
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.TEAM_MEMBER2;	break;
				case GuideManager.GUIDE_STEP.TEAM_MEMBER2://107
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_4;		break;			
				case GuideManager.GUIDE_STEP.COPY1_4://108
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.UPDATE;			break;	
				case GuideManager.GUIDE_STEP.UPDATE://109
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_5;		break;
				case GuideManager.GUIDE_STEP.COPY1_5://110
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.GIFT;			break;	
				case GuideManager.GUIDE_STEP.GIFT://111
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY2_1;		break;	
				case GuideManager.GUIDE_STEP.COPY2_1://112
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.END;			break;
				case GuideManager.GUIDE_STEP.END://900
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.END;			break;
					
				}
				Debug.LogWarning("****** Receive a SCLoginRet and Finish_step : "+GuideManager.Instance.currentStep);
			}
			/*daixiugai
			*/
			Debug.Log("Login guide state:"+GuideManager.Instance.currentStep);
        }
		 public void SetSDKLoginRetData(SCSDKLoginThirdPlatformRet msg)
        {
            accountID = msg.AccountId;
			Debug.Log("*********    Login receive account id = "+accountID);
			//if(! PlayerPrefs.HasKey("ACCOUNT_ID")) PlayerPrefs.SetString("ACCOUNT_ID",accountID.ToString());
			isNewUser = (msg.State == 1);
			if(isNewUser)
			{
				Debug.Log("*** *** ***    Enter NewUser setting!");
				PlayerPrefs.SetString("ACCOUNT_ID",accountID.ToString());
			}
			
			/*
			if(msg.HasNickName){
				AccountInfo ai_t = new AccountInfo();
				ai_t.email = msg.NickName;
				ai_t.password = "";
				
				Debug.Log("*********    msg.UcAccount = "+msg.UcAccount);
				ai_t.accountId = long.Parse(msg.UcAccount);
				AccountManager.Instance.CurAccount = ai_t;
			}
			*/
			
			//cb:通过服务器记录当前状态
			Debug.Log("*** msg.Guide_finished_step ***      ****** "+msg.Guide_finished_step);
			int guide_int_temp = PlayerPrefs.GetInt("GUIDE_INT_TEMP" , -1);
			if(!GuideManager.Instance.isInSubStep/*guide_int_temp == 1*/){
				switch((GuideManager.GUIDE_STEP)msg.Guide_finished_step){
				
				case GuideManager.GUIDE_STEP.NONE://0
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.NONE;			break;
				case GuideManager.GUIDE_STEP.CARD_CHOOSE://100
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.LEADER;			break;
				case GuideManager.GUIDE_STEP.LEADER://101
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.TEAM_MEMBER;	break;
				case GuideManager.GUIDE_STEP.TEAM_MEMBER://102
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_1;		break;
					
				case GuideManager.GUIDE_STEP.COPY1_1_END://103
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_2;		break;			
				case GuideManager.GUIDE_STEP.COPY1_2_END://104
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_3;		break;				
				case GuideManager.GUIDE_STEP.COPY1_3_END://105
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.LOTTERY;		break;	
					
				case GuideManager.GUIDE_STEP.LOTTERY://106
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.TEAM_MEMBER2;	break;
				case GuideManager.GUIDE_STEP.TEAM_MEMBER2://107
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_4;		break;			
				case GuideManager.GUIDE_STEP.COPY1_4://108
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.UPDATE;			break;	
				case GuideManager.GUIDE_STEP.UPDATE://109
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY1_5;		break;
				case GuideManager.GUIDE_STEP.COPY1_5://110
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.GIFT;			break;	
				case GuideManager.GUIDE_STEP.GIFT://111
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.COPY2_1;		break;	
				case GuideManager.GUIDE_STEP.COPY2_1://112
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.END;			break;
				case GuideManager.GUIDE_STEP.END://900
					GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.END;			break;
					
				}
				Debug.LogWarning("****** Receive a SCLoginRet and Finish_step : "+GuideManager.Instance.currentStep);
			}
			/*daixiugai
			*/
			Debug.Log("Login guide state:"+GuideManager.Instance.currentStep);
        }
#endif
		//增加新卡牌并按照Quality排序
		//排序优先级Quality->Level
        public void AddCardToBag(UserCardItem newCard)
        {
			//Debug.Log("Start ADD:"+newCard.cardID);
            if (cardBagList.Count == 0)
            {
				//Debug.Log("ADD CARD:"+newCard.cardID);
                cardBagList.Add(newCard);
                return;
            }
			for(int index = 0; index < cardBagList.Count; index++)
			{
				if(newCard.quality >=cardBagList[index].quality /*&& newCard.level >=cardBagList[index].level*/)
				{
					cardBagList.Insert(index,newCard);
					//Debug.Log("ADD CARD:"+newCard.cardID);
					return;
				}
			}
			cardBagList.Add(newCard);
        }
			
//			//质量最佳，直接加到最前面
//            if (cardBagList[0].quality <= newCard.quality)
//            {
//                
//                cardBagList.Insert(0, newCard);
//                return;
//            }
//            if (cardBagList[cardBagList.Count - 1].quality > newCard.quality)
//            {
//                
//                cardBagList.Add(newCard);
//                return;
//            }
//            int insertIndex = 0;
//            bool bCompareQuality = true;
//            for (insertIndex = 0; insertIndex < cardBagList.Count; insertIndex++)
//            {
//                if (bCompareQuality)
//                {
//                    if (cardBagList[insertIndex].quality > newCard.quality)
//                    {
//                        continue;
//                    }
//                    else
//                    {
//                        bCompareQuality = false;
//                        if (newCard.templateID >= cardBagList[insertIndex].templateID)
//                        {
//                            break;
//                        }
//                    }
//                }
//                else
//                {
//                    if (newCard.templateID >= cardBagList[insertIndex].templateID)
//                    {
//                        break;
//                    }
//                }
//            }
//            cardBagList.Insert(insertIndex, newCard);

		
        public void RemoveCardByID(Int32 id)
        {
//            RemoveBattleCardByID(id);
//            UserCardItem itemRemove;
//            if(cardMap.TryGetValue(id, out itemRemove))
//            {
//                cardBagList.Remove(itemRemove);
//                cardMap.Remove(id);
//            }
//            
            
        }
		
		public void UpdateCopyList(CopyType type)
		{
			if(type == CopyType.NORMAL)
			{
				foreach(SubCopy sub_copy in normalCopys)
				{
					if(sub_copy.subCopyID == -1)
					{
						continue;
					}
					foreach(MainCopy main_copy in normalMainCopys)
					{
						bool is_found = false;
						foreach(SubCopy sub_copy1 in main_copy.subCopys)
						{
							if(sub_copy.subCopyID == sub_copy1.subCopyID)
							{
								sub_copy1.count = sub_copy.count;
                                sub_copy1.maxStar = sub_copy.maxStar;
								is_found = true;
								break;
							}
						}
						if(is_found)
						{
							break;
						}
					}
				}
			}
			else
			{
				foreach(MainCopy copy1 in activityCopys)
				{
					foreach(MainCopy copy2 in activityMainCopys)
					{
						if(copy1.copyId == copy2.copyId)
						{
							copy2.startTime = copy1.startTime;
							copy2.clostTime = copy1.clostTime;
							foreach(SubCopy scopy1 in copy1.subCopys)
							{
								foreach(SubCopy scopy2 in copy2.subCopys)
								{
									if(scopy2.subCopyID == scopy1.subCopyID)
									{
										scopy2.count = scopy1.count;
                                        scopy2.maxStar = scopy1.maxStar;
										Debug.Log("scopy2.count~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"+scopy2.count);
										break;
									}
								}
							}
							break;
						}
					}
				}
			}
			UpdateCopyState(type);
		}
		
		private void UpdateCopyState(CopyType type)
		{
			if(type == CopyType.NORMAL)
			{
				normalMainCopys.Sort(SortMainCopy);
			
				foreach(MainCopy copy in normalMainCopys)
				{
					copy.UpdateState();
				}
			}
			else
			{
				activityMainCopys.Sort(SortMainCopy);
				foreach(MainCopy copy in activityMainCopys)
				{
					copy.UpdateState();
				}
			}
		}
		
		private static int SortMainCopy(MainCopy m1, MainCopy m2)
		{
			return m1.tblCopy.CopyOrder.CompareTo(m2.tblCopy.CopyOrder);
		}

//        public UserCardItem BattleCardInfo(Int32 id)
//        {
//            if (!cardMap.ContainsKey(battleArray[id]))
//            {
//                return null;
//            }
//            return cardMap[battleArray[id]];
//        }
		
//------------用户方法结束------------	
		
		
		
//----------------------------------战斗方法开始------------------------------------------
        public void SetBattleCard(int index, Int32 cardID)
        {
            if (index >= 0 && index <= BATTLECARD_MAX)
            {
                for (int i = 0; i < BATTLECARD_MAX; i++)
                {
                    if (cardID == battleArray[i])
                    {
                        battleArray[i] = -1;
                    }
                }

                battleArray[index] = cardID;
            }
        }

        public void RemoveBattleCardByID(Int32 cardID)
        {
            for (int i = 0; i < BATTLECARD_MAX; i++)
            {
                if (cardID == battleArray[i])
                {
                    battleArray[i] = -1;
                }
            }
        }
		
		public void SetBattleData(SCBattleData data)
		{
            GameObject go = GameObject.Find("BattleLogic");
            if (go != null && go.GetComponent<BattleLogic>() != null)
            {
                BattleLogic battle_logic = go.GetComponent<BattleLogic>();
                Games.Battle.BattleProcedureType pro_type = battle_logic.GetBattleCore().GetBattlePlayer().GetBattleStateType();
                if (pro_type != Battle.BattleProcedureType.E_BATTLE_PROCEDURE_WAITING)
                {
                    return;
                }
            }
			battleData.SetBattleData(data);
		}
		
		public void SetBattleData(SCAskWorldBossBattle data)
		{
			GameObject go = GameObject.Find("BattleLogic");
            if (go != null && go.GetComponent<BattleLogic>() != null)
            {
                BattleLogic battle_logic = go.GetComponent<BattleLogic>();
                Games.Battle.BattleProcedureType pro_type = battle_logic.GetBattleCore().GetBattlePlayer().GetBattleStateType();
                if (pro_type != Battle.BattleProcedureType.E_BATTLE_PROCEDURE_WAITING)
                {
                    return;
                }
            }
			worldBossCurrentDamage = data.CurrentDamage;
			worldBossRewardMoney = data.RewardMoney;
			worldBossHpBeginBattle = data.CurrentBossHp;
			battleData.SetBattleData(data);
		}
		
		private Battle.BattleProcedureType GetBattleProcedureType()
		{
			GameObject go = GameObject.Find("BattleLogic");
            if (go != null)
            {
                BattleLogic battle_logic = go.GetComponent<BattleLogic>();
				if(battle_logic != null)
				{
                	return battle_logic.GetBattleCore().GetBattlePlayer().GetBattleStateType();
				}
            }
			return Games.Battle.BattleProcedureType.E_BATTLE_PROCEDURE_NONE;
		}
		
		public void SetBattleData(SCPaiTaBattleData data)
		{
			GameObject go = GameObject.Find("BattleLogic");
            if (go != null && go.GetComponent<BattleLogic>() != null)
            {
                BattleLogic battle_logic = go.GetComponent<BattleLogic>();
                Games.Battle.BattleProcedureType pro_type = battle_logic.GetBattleCore().GetBattlePlayer().GetBattleStateType();
                if (pro_type != Battle.BattleProcedureType.E_BATTLE_PROCEDURE_WAITING)
                {
                    return;
                }
            }
			
			Debug.LogWarning("pataNum" + pataNum);
			SetUserBaseData(data.BaseData);
			battleData.SetBattleData(data);
			pataRoundRewardMoney = data.Moneynum;
			pataRoundRewardYuanbao = data.Rubynum;
		}
		public void SetPVPBattleData(SCPVPBattleData data)
		{
            GameObject go = GameObject.Find("BattleLogic");
            if (go != null && go.GetComponent<BattleLogic>() != null)
            {
                BattleLogic battle_logic = go.GetComponent<BattleLogic>();
                Games.Battle.BattleProcedureType pro_type = battle_logic.GetBattleCore().GetBattlePlayer().GetBattleStateType();
                if (pro_type != Battle.BattleProcedureType.E_BATTLE_PROCEDURE_WAITING)
                {
                    return;
                }
            }
            
			battleData.SetPVPBattleData(data);
		}
		public void SetClearBattleData(SCClearBattleData data)
		{				
			if(0 == data.Result)
			{
				if(data.HasBaseData)
					SetUserBaseData(data.BaseData , 1);
				if(data.HasCopyData)
					SetUserCopyData(data.CopyData);
                currentCopyStar = data.CopyStar;
                currentFragmentList.Clear();
                if (data.suipianList != null)
                {
                    foreach (var suipian in data.suipianList)
                    {
                        FragmentInfo fragment = new FragmentInfo();
                        fragment.id = suipian.Id;
                        fragment.num = suipian.Num;
                        currentFragmentList.Add(fragment);
                    }
                }
			}
			else
			{
				Debug.LogError("SetClearBattleData(), result failed!");
			}
		}
		
		public void SetClearPataBattleData(SCClearPaiTaBattleData data)
		{				
			if(0 == data.Result)
			{
				if(data.HasBaseData)
					SetUserBaseData(data.BaseData , 1);
				pataTotalRewardMoney = data.Moneynum;
				pataTotalRewardYuanbao = data.Rubynum;
				//获取金币，获取元宝数量
                //currentCopyStar = data.CopyStar;
				
			}
			else
			{
				Debug.LogError("SetClearBattleData(), result failed!");
			}
		}
		
		public void RemoveFriendFromBattleArray()
		{
            this.LoadbattleArray();
// 			for(int i = 0; i < battleArray.Length; i++)
// 			{
// 				bool is_friend = true;
// 				for(int j = 0; j < teamMemberArray.Length; j++)
// 				{
// 					if(battleArray[i] == teamMemberArray[j])
// 					{
// 						is_friend = false;
// 						break;
// 					}
// 				}
// 				
// 				if(is_friend)
// 				{
// 					battleArray[i] = -1;
// 				}
// 			}
// 			
// 			for(int i = 0; i < battleArray.Length; i++)
// 			{
// 				for(int j = i + 1; j < battleArray.Length; j++)
// 				{
// 					if(battleArray[i] == battleArray[j])
// 					{
// 						battleArray[j] = -1;
// 					}
// 				}
// 			}
            if (this.CheckSetState() && nfightFriendPos > 0)
            {
                battleArray[nfightFriendPos] = -1;
            }
            this.SavebattleArray();
		}
		
		public void DebugBattleArray()
		{
			//
			Debug.Log("DebugBattleArray");
			 for (int i = 0; i < 6; i++)
                {
					Debug.Log(battleArray[i]);
                }
			
		}


        public void LoadbattleArray()
        {
			string accountIDStr = "battleArrayAccountID" + accountID;
			nGetAccountStrCheck = PlayerPrefs.GetInt(accountIDStr);
            nSetState = PlayerPrefs.GetInt("SetState");
            nfightFriendPos = PlayerPrefs.GetInt("fightFriendPos");
			
            //设置状态大于零的情况，说明之前有保存过
            if (this.CheckSetState())
            {
                for (int i = 0; i < 6; i++)
                {
					string battleArrayKey = "accountID"+accountID.ToString() +":"+ i.ToString();
					long menberID = (long)PlayerPrefs.GetInt(battleArrayKey);
					if(menberID <= 0)
					{
						battleArray[i] = -1;
					}
					else
					{
						battleArray[i] = menberID;
					}
                    
					Debug.Log(battleArray[i]);
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    battleArray[i] = -1;
                }
                nfightFriendPos = -1;
            }

        }
		
		public void ClearBattleArraySet()
		{
			string accountIDStr = "battleArrayAccountID" + accountID;
			PlayerPrefs.SetInt(accountIDStr, -1);
			PlayerPrefs.SetInt("SetState", -1);
            PlayerPrefs.SetInt("fightFriendPos", -1);
			
            for (int i = 0; i < 6; i++)
            {
				string battleArrayKey = "accountID"+accountID.ToString() +":"+ i.ToString();
                PlayerPrefs.SetInt(battleArrayKey, -1);
            }
		}


        //保持战斗队型
        public void SavebattleArray()
        {
			
			string accountIDStr = "battleArrayAccountID" + accountID;
			PlayerPrefs.SetInt(accountIDStr, (int)accountID);
            PlayerPrefs.SetInt("SetState", 1);
            PlayerPrefs.SetInt("fightFriendPos", nfightFriendPos);
			
            for (int i = 0; i < 6; i++)
            {
//				Debug.Log(battleArray[i]);
				string battleArrayKey = "accountID"+accountID.ToString() +":"+ i.ToString();
                PlayerPrefs.SetInt(battleArrayKey, (int)battleArray[i]);
            }
        }


        //检查设置状态，判断之前是否有设置过
        public bool CheckSetState()
        {
            if (nSetState > 0 && nGetAccountStrCheck > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

		//刷新BattleArray,因为队员可能改变--
		public void RefreshBattleArray()
		{
            this.LoadbattleArray();

			//delete that not in teamMemberArray

            //记录当前 teamMemberArray 是否是初始状态，还没有值
            bool bTeamArrayEmpty = true;
            for (int i = 0; i < 5;i++ )
            {
                if (teamMemberArray[i] > 0)
                {
                    bTeamArrayEmpty = false;
                    break;
                }
            }


            //当teamMemberArray 里面有实际值时才处理
            if (!bTeamArrayEmpty)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (battleArray[i] != -1)
                    {
                        bool has = false;
                        for (int j = 0; j < 5; j++)
                        {
                            if (battleArray[i] == teamMemberArray[j])
                            {
                                has = true;
                                break;
                            }
                        }
                        if (!has && i != Obj_MyselfPlayer.GetMe().nfightFriendPos)
                        {
                            battleArray[i] = -1;
                        }
                    }
                }
            }
			
			
			//刷新助战好友
			if((Obj_MyselfPlayer.GetMe().battleType == Games.Battle.BattleType.PVE )&& Obj_MyselfPlayer.GetMe().currentAssistFriend != null 
                   && Obj_MyselfPlayer.GetMe().nfightFriendPos >= 0 )
			{
                battleArray[Obj_MyselfPlayer.GetMe().nfightFriendPos] = Obj_MyselfPlayer.GetMe().currentAssistFriend.cardGuiId;
			}
			else
			{
				if(Obj_MyselfPlayer.GetMe().nfightFriendPos >= 0)
					battleArray[Obj_MyselfPlayer.GetMe().nfightFriendPos] = -1;
			}
			
			for(int i = 0; i < 5; i++)
			{
				if(teamMemberArray[i] != -1)
				{
					bool has = false;
					for(int j = 0; j < 6; j++)
					{
						if(battleArray[j] == teamMemberArray[i])
						{
							has = true;
							break;
						}
					}
					if(!has)
					{
						for(int j = 0; j < 6; j++)
						{
                            //设置队员位置，按顺序排下去，但不能是助战好友位置
							if(battleArray[j] == -1 && j != Obj_MyselfPlayer.GetMe().nfightFriendPos)
							{
								battleArray[j] = teamMemberArray[i];
								break;
							}
						}
					}
				}
			}
            this.SavebattleArray();
		}

		public void GetReviveCostMoney()
		{
			
		}
		public void SetBattleBeforeDate()
		{
			lastLevel=level;
			lastPower=TableManager.GetIdexperienceByID(Obj_MyselfPlayer.GetMe().level).IDPhysicalValue;
			lastLeaderShip=leadership;
			lastHero=bagMax;
			lastFriend=friendNumMax;
			lastExp=exp;
		}
		//战斗结束，重置数据--
		public void OnBattleEnd()
		{
			//battleType = Games.Battle.BattleType.PVE;
			//isPVPBattle = false;
			enemyGUID = -1;
			//enemyName = "";
		}
//------------------------------------------战斗方法结束---------------------------------------------
		
		
		
		//------------养成方法开始------------
		public void SetCardCombingData(SCCardCombiningRet data)
		{
			if(data.Combine_result == 1)
			{
				SetUserBaseData(data.BaseData);
				SetUserBagData(data.BagData);
			}
			else
			{
				LogModule.ErrorLog("setCardCombingData(), card combine failed!");	
			}
		}
		public void ResetCultivateData()
		{
			curCultivateType = CultivateType.UNKNOWN;//当前的养成操作类型--
			isSelectHero = true;//是否要摆选择英雄,选择英雄:true,选择材料:false.
			//updateHeroItem = null;
			//for(int i = 0; i < 6; i++)
			//{
				//updateMaterialItems[i] = null;
			//}
			evolutionHeroItem = null;
			for(int i = 0; i < 5; i++)
			{
				evolutionMaterialItems[i] = null;
			}
			strengthenHeroItem = null;
		}
		public void SetCardEvolutionData(SCCardEvolveRet data)
		{
			if(data.Evolve_result == 1)
			{
				SetUserBagData(data.BagData);
                SetUserBaseData(data.BaseData);
			}
			else
			{
				LogModule.ErrorLog("SetCardEvolutionData(), card evolution failed!");
			}
		}
		public void SetCardStrengthenData(SCCardStrengthenRet data)
		{
			if(data.Streng_result == 1)
			{
				SetUserBaseData(data.BaseData);
				SetUserBagData(data.BagData);
			}
			else
			{
				LogModule.ErrorLog("SetCardStrengthenData(), card strengthen failed!");
			}
		}
		//------------养成方法结束------------
		
		//------------好友方法开始------------
		public void setFriendsData(SCFriendsList data,int friends_num,int friends_max)
		{
			friendNumMax = friends_max;
			friendsList.Clear();
			foreach(PBFriend friend in data.friendsListList)
			{
				UserFriend uf = new UserFriend(friend);
				friendsList.Add(uf);
			}
		}
		public void setSearchResult(SCSearchFriend data)
		{
			friendSearchResult = null;
			friendSearchResult = new UserFriend(data.Friend_info);
		}
		public void giveFriendPower(SCGiveFriendPower data)
		{
			//得到了取得power的好友ID
			foreach(UserFriend friend in Obj_MyselfPlayer.GetMe().friendsList)
			{
				if(data.Friend_guid == friend.guid)
				{
					friend.canGivePower = false;
				}
			}
		}
		public void getFriendPower(SCGetFriendPower data)
		{
            this.receive_power_time = data.Receive_power_time;
			this.power = data.New_power_num;
			foreach(UserFriend friend in Obj_MyselfPlayer.GetMe().friendsList)
			{
				if(data.Friend_guid == friend.guid)
				{
					friend.canGetPower = false;
				}
			}
			GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");//2013-7-29 Jack Wen
		}
		public void addFriend(SCADDFriend data)
		{
			if(data.State==2)
			{
//				BoxManager.showMessage("对方好友申请已满");
//				BoxManager.showMessageByID((int)MessageIdEnum.Msg21);
				BoxManager.showMessageByID((int)MessageIdEnum.Msg147);
			}
			else if(data.State==3)
			{
//				BoxManager.showMessage("对方好友已满");
//				BoxManager.showMessageByID((int)MessageIdEnum.Msg13);
				BoxManager.showMessageByID((int)MessageIdEnum.Msg147);
			}
			//改为只返回成功或者失败,下次从新刷新列表
//			UserFriend newFriend = new UserFriend(data.NewFriend);
//			friendsList.Add(newFriend);
		}
		public void deleteFriend(SCDeleteFriend data)
		{
			UserFriend removeFriend = null;
			foreach(UserFriend uf in friendsList)
			{
				if(uf.guid == data.Friend_guid)
				{
					removeFriend =uf;
					break;
				}
			}
			if(removeFriend!=null)
			{
				friendsList.Remove(removeFriend);
			}
		}
		public void setAssistanceList(SCGetRandomAssistanceList data)
		{
			AssistanceList.Clear();
			Debug.Log("assistanceListListCount="+data.assistanceListList.Count);
			foreach(PBFriend friend in data.assistanceListList)
			{
				AssistFriend uf = new AssistFriend(friend);
				AssistanceList.Add(uf);
			}
		}
		//------------好友方法结束------------
		
		//------------背包方法开始------------
		public void setSellCard(SCSellCard data)
		{
			foreach(long guid in data.card_guidList)
			{
				UserCardItem removeCard = null;
				foreach(UserCardItem uci in cardBagList)
				{
					if(uci.cardID == guid)
					{
						removeCard = uci;
						break;
					}
				}
				cardBagList.Remove(removeCard);
			}
			
			this.money = data.New_money;
			Debug.Log("New Money:"+money);
		}
		//------------背包方法结束------------
		
		//------------任务方法开始------------
		public void setTaskData(SCTaskList data)
		{
			taskList.Clear();
			foreach(PBTask task in data.pbTaskList)
			{
				Tab_Quest questInfo=TableManager.GetQuestByID(task.Templet_id);
				if(questInfo!=null)
				{
					UserTask ut = new UserTask(task);
					ut.taskType=questInfo.QuestType;
					ut.title=questInfo.Name;
					ut.request=questInfo.QuestValue;
					ut.gold=questInfo.RewardGold;
					ut.dollar=questInfo.RewardDollar;
					ut.propID=questInfo.RewardID;
					ut.power = questInfo.RewardPower;
                    taskList.Add(ut);
                    //if(ut.state==1&&taskList.Count>0)
                    //{
                    //    taskList.Insert(0,ut);
                    //}
                    //else
                    //{
                    //    taskList.Add(ut);
                    //}
				}
			}
		}
		
		public void setFinishTask(SCFinishTask data)
		{
			SetUserBaseData(data.BaseData);
			if(data.BagData.cardInfoCount!=0||data.BagData.itemInfoCount!=0)
			{
				SetUserBagData(data.BagData);
			}
//			finishTask=new UserFinishTask(data);
//			money=finishTask.gold;
//			dollar=finishTask.dollar;
//			power=finishTask.power;
			//这块会改为掉落的id和类型，从本地表读取相关物品
//			if(finishTask.card!=null)
//			{
//				cardBagList.Add(finishTask.card);
//			}
		}
		//------------任务方法结束------------
		
		//------------商城方法开始------------
		
		public void setPurchaseInfoData(SCProductList data)
		{
			purchaseInfoList.Clear();
			foreach(ProductInfo product in data.productList)
			{
				PurchaseInfo purchase=new PurchaseInfo(product);
				purchaseInfoList.Add(purchase);
			}
		}
		
		public void setPurchaseInfoData(SC30038 data)
		{
			Debug.Log("********************************************");
			purchaseInfoList.Clear();
			//isFirstPurchase=data.IsFirst;
			foreach(ProductInfo product in data.productList)
			{
				Debug.Log(product.GoodsPrice);
				PurchaseInfo purchase=new PurchaseInfo(product);
				purchaseInfoList.Add(purchase);
			}
		}
		
		public void setPurchaseInfoData(SC30040 data)
		{
			purchaseInfoList.Clear();
			//isFirstPurchase=data.IsFirst;
			Debug.Log("********************************************");
			foreach(ProductInfo product in data.productList)
			{
				Debug.Log(product.GoodsPrice);
				PurchaseInfo purchase=new PurchaseInfo(product);
				purchaseInfoList.Add(purchase);
			}
		}

        public void setPurchaseInfoData(SCPPProductList data)
        {
            purchaseInfoList.Clear();
            //isFirstPurchase=data.IsFirst;
              Debug.Log("********************************************");
            foreach (ProductInfo product in data.productList)
            {
                Debug.Log(product.GoodsPrice);
                PurchaseInfo purchase = new PurchaseInfo(product);
                purchaseInfoList.Add(purchase);
            }
        }
		//------------商城方法结束------------
		
		//------------邮件方法开始------------
		public void setMailList(SCMailList data)
		{
			mailList.Clear();
			foreach(PBMail mail in data.pbMailList)
			{
				MailInfo mailInfo=new MailInfo(mail);
				if(mailInfo.mailType==2&&mailList.Count>0)
				{
					mailList.Insert(0,mailInfo);
				}
				else
				{
					mailList.Add(mailInfo);
				}
			}
			mailIsFull=data.Isfull;
			friendMailIsFull=data.FriendIsfull;
			mailListCount = data.NoReadMail;
			//mailList=mailList.OrderBy(entity=>entity.fromTime).ToList();
			Debug.Log("get mail list end");
		}
		
		public void receiveGoods(SCMailSystem data)
		{
			SetUserBaseData(data.BaseData);
			if(data.BagData.cardInfoCount!=0||data.BagData.itemInfoCount!=0)
			{
				SetUserBagData(data.BagData);
			}
		}
		//------------邮件方法结束------------
		
		//------------图鉴方法开始------------
		public bool ChangeHeroInfoState(int cardTemplateID,int state)
		{
			bool isFirstHave=false;
			for(int i=0;i<heroList.Count;i++)
			{
				if(heroList[i].templateId==cardTemplateID && state>heroList[i].state)
				{
					heroList[i].state=state;
					if(state==2)
					{
						isFirstHave=true;
					}
					break;
				}
			}
			return isFirstHave;
		}
		//------------图鉴方法结束------------
		//------------GM命令开始-------------
		public void GMCommand(SCGMcmds data)
		{
			if (data.Result == 0)
			{
				SetUserBagData(data.BagData);
				SetUserBaseData(data.BaseData);
				SetUserCopyData(data.CopyData);
				GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
			}
			else 
			{
				LogModule.ErrorLog("GMCommand(), gm command failed!");
			}
		}
		//------------GM命令结束-------------
        //------------世界Boss方法开始------------	
        //获取世界boss信息
        public void SetWorldBossInfo(SCAskWorldBossInfo msg)
        {
            hasWorldBossReward = msg.HasReward;
            if (msg.ActiveBoss != null)
            {
                activeBoss = new WorldBossClass(msg.ActiveBoss);
                activeBossCD = DateTime.Now.AddSeconds(activeBoss.remainOpenTime);
            }
            if (msg.LastKillInfo != null)
            {
                lastKillInfo = new WorldBossKillInfoClass(msg.LastKillInfo);
            }
            if (msg.CurrentKillInfo != null)
            {
                currentKillInfo = new WorldBossAttInfoClass(msg.CurrentKillInfo);
                resurgenceCD = DateTime.Now.AddSeconds(currentKillInfo.battleCD);
            }
        }

        //获取人物复活信息
        public void SetWorldBossResurgenceInfo(SCWorldBossResurgence msg)
        {
            if (msg.CurrentAttInfo != null)
            {
                currentKillInfo = new WorldBossAttInfoClass(msg.CurrentAttInfo);
                resurgenceCD = DateTime.Now.AddSeconds(currentKillInfo.battleCD);
            }
            if (msg.BaseData != null)
            {
                SetUserBaseData(msg.BaseData);
            }
        }

        //获取世界boss排行榜
        public void SetWorldBossRank(SCWorldBossWeekRank msg)
        {
            if (msg.PlayerRankInfo != null)
            {
                playerRank = new WorldBossDamageRankInfoClass(msg.PlayerRankInfo);
            }
            worldBossWeekRankList.Clear();
            if (msg.top10RankInfoList != null)
            {
                foreach (WorldBossDamageRankInfo rank in msg.top10RankInfoList)
                {
                    WorldBossDamageRankInfoClass topInfo = new WorldBossDamageRankInfoClass(rank);
                    worldBossWeekRankList.Add(topInfo);
                }
            }
            hasWorldBossReward = msg.HasReward;
        }

        //获取buff信息
        public void SetWorldBossBuffInfo(SCWorldBossAddZhufu msg)
        {
            SetUserBaseData(msg.BaseData);
            currentKillInfo = new WorldBossAttInfoClass(msg.CurrentAttInfo);
        }
        //------------世界Boss方法结束------------	
		//lottery start
		public List<int> lotteryTempletIDs = new List<int>();
		public void setLotteryData(SCGambleRet data)
		{
			SetUserBaseData(data.BaseData);
			SetUserBagData(data.BagData);
			lotteryTempletIDs.Clear();
			foreach(int templetID in data.templateIDList)
			{
				lotteryTempletIDs.Add(templetID);
			}	
		}
		
		public void setFreeLotteryData(SCRandomCardFree data)
		{
			SetUserBaseData(data.BaseData);
			SetUserBagData(data.BagData);
			lotteryTempletIDs.Clear();
			lotteryTempletIDs.Add(data.TemplateID);
		}
		
		//获得队长ID
		public long GetTeamLeaderCardID()
		{
			return teamMemberArray[0];
		}
		
		public int GetTeamFightNum()
		{
			int nLen = teamMemberArray.Length;
			int nNum=0;
			for(int i=0; i<nLen; i++)
			{
				if(teamMemberArray[i] >= 0)
				{
					nNum++;
				}
			}
			
			return nNum;
		}
		
		//判断卡牌在背包中
		public bool IsCardInBagByID(long cardID)
		{
			return GetUserCardByGUID(cardID) == null ? false : true;
		}
		
		//获得战斗力
		public int GetFightValue()
		{	
			int nowstreng=0;
			//计算当前战斗力
			for(int i=0;i<cardBagList.Count;i++)
			{
				for(int j=0;j<teamMemberArray.Length;j++)
				{
					if(cardBagList[i].cardID==teamMemberArray[j])
					{
						nowstreng += ((cardBagList[i].GetAttack()+cardBagList[i].GetHp())/100+cardBagList[i].level*UserCardItem.startFactor[cardBagList[i].quality]);
						break;
					}
				}
			}
			
			return nowstreng;
		}
		//获得PvP战斗力
		public int GetPvPFightValue()
		{
			int nowstreng=0;
			//计算当前战斗力
			for(int i=0;i< cardBagList.Count;i++)
			{
				for(int j=0;j<PvPBattleArray.Length;j++)
				{
					if(cardBagList[i].cardID==PvPBattleArray[j])
					{
						nowstreng += ((cardBagList[i].GetAttack()+cardBagList[i].GetHp())/100+cardBagList[i].level*UserCardItem.startFactor[cardBagList[i].quality]);
						break;
					}
				}
			}
			return nowstreng;
		}
		
        //获得领导力
        public int GetLeaderShipValue()
        {
            int nowLeaderShip = 0;
            List<UserCardItem> cardList = cardBagList;
            for (int i = 0; i < cardList.Count; i++)
            {
                for (int j = 0; j < teamMemberArray.Length; j++)
                {
                    if (cardList[i].cardID == teamMemberArray[j])
                    {
                        nowLeaderShip += cardList[i].GetLeaderShip();
                    }
                }
            }
            return nowLeaderShip;
        }
		//获得PvP领导力
        public int GetPvPLeaderShipValue()
        {
            int nowLeaderShip = 0;
            List<UserCardItem> cardList = cardBagList;
            for (int i = 0; i < cardList.Count; i++)
            {
                for (int j = 0; j < PvPBattleArray.Length; j++)
                {
                    if (cardList[i].cardID == PvPBattleArray[j])
                    {
                        nowLeaderShip += cardList[i].GetLeaderShip();
                    }
                }
            }
            return nowLeaderShip;
        }
		
		public void SetGGL(SCGGL msg)
		{
			//for(int i=0; i<9; i++)
			//{
				//GuaGuaLeCardsIDArray[i] = msg.showCardIDsList[i];
			//}
			//dropBagType = msg.DropBag.Type;
			//dropBagValue = msg.DropBag.Value;
			GGLRewardID = msg.ShowRewardID;
			GGLTimes = msg.NowTimes;
		}
		
		public void SetFengshiInfo(FengshuiInfo infor)
		{
			Dictionary<int,int> wuxing = FengShuiData.Instance().WuxingInfor;
			if (wuxing == null)
				wuxing = new Dictionary<int, int>();
			wuxing.Clear();
			foreach (WuxingInfo info in infor.wuxingInfoList)
			{
//				Debug.LogError("Wuxng: [ " + info.Id + ", " + info.Level + " ]");
				if (wuxing.ContainsKey(info.Id))
					wuxing[info.Id] += info.Level;
				else
					wuxing.Add(info.Id,info.Level);
			}
			FengShuiData.Instance().WuxingInfor = wuxing;
			Dictionary<int,int> suipian = FengShuiData.Instance().SuipianInfor;
			if (suipian == null)
				suipian = new Dictionary<int, int>();
			suipian.Clear();
//			Debug.LogError("Count = " + infor.suipianInfoList.Count);
			foreach (SuipianInfo info in infor.suipianInfoList)
			{
//				Debug.LogError("ShuiPian: [ " + info.Id + ", " + info.Num + " ]");
				if (suipian.ContainsKey(info.Id))
					suipian[info.Id] += info.Num;
				else
					suipian.Add(info.Id,info.Num);
			}
			FengShuiData.Instance().SuipianInfor = suipian;
//			Debug.LogError("Star Count = " + infor.StarNum);
			FengShuiData.Instance().star = infor.StarNum;
			InitFengShuiAdd();
		}
		
		public bool InitFengShuiAdd()
		{
			FengShuiData.Instance().fengshuiAdd = new double[5,3];
			if (FengShuiData.Instance().WuxingInfor == null || FengShuiData.Instance().WuxingInfor.Count == 0)
				return false;
			foreach (KeyValuePair<int,int> item in FengShuiData.Instance().WuxingInfor)
			{
				if (item.Value > 0)
				{
					double attackAdd = 0;
					double hpAdd = 0;
					double hpPercent = 0;
					for (int i = 0; i < 2; i++)
					{
						switch (TableManager.GetFengshuiByID(item.Key).GetEffectTypebyIndex(i))
						{
						case 1: //攻击加成
							attackAdd += TableManager.GetFengshuiByID(item.Key).GetEffectDatabyIndex(i) * item.Value;
							break;
						case 3: //血量加成
							hpAdd += TableManager.GetFengshuiByID(item.Key).GetEffectDatabyIndex(i) * item.Value;
							break;
						case 4: //血量百分比加成
							hpPercent += (TableManager.GetFengshuiByID(item.Key).GetEffectDatabyIndex(i) * item.Value);
							break;
						default:
							break;
						}
					}
				 	//金木水火土 风水加成统计记录
					FengShuiData.Instance().fengshuiAdd[TableManager.GetFengshuiByID(item.Key).EffectArea,0] += attackAdd;
					FengShuiData.Instance().fengshuiAdd[TableManager.GetFengshuiByID(item.Key).EffectArea,1] += hpAdd;
					FengShuiData.Instance().fengshuiAdd[TableManager.GetFengshuiByID(item.Key).EffectArea,2] += hpPercent;
				}
			}
			return true;
		}
		
		public void SetBZT(SCBGZ msg)
		{
			BGZRewardID = msg.ShowRewardID;
			BGZTimes =  msg.BaseData.BBZTimes;
			Flags = msg.BaseData.BBZFlag;
		}

        public void setMonthCardInfo(SCMonthCardInfo msg)
        {
            monthCardInfo = new MonthCardInfoClass();
            monthCardInfo.currentPurchase = msg.YuekaInfo.CurrentPurchase;
            monthCardInfo.monthCardState = msg.YuekaInfo.MonthCardState;
            monthCardInfo.remainTime = msg.YuekaInfo.RemainTime;
            monthCardInfo.rewardRemainDays = msg.YuekaInfo.RewardRemainDays;
            monthCardInfo.totalPurchase = msg.YuekaInfo.TotalPurchase;
            monthCardInfo.totalTime = msg.YuekaInfo.TotalTime;
            Debug.Log("xlym:MonthCardInfo Start");
            Debug.Log("currentPurchase:" + msg.YuekaInfo.CurrentPurchase);
            Debug.Log("monthCardState:" + msg.YuekaInfo.MonthCardState);
            Debug.Log("remainTime:" + msg.YuekaInfo.RemainTime);
            Debug.Log("rewardRemainDays:" + msg.YuekaInfo.RewardRemainDays);
            Debug.Log("totalPurchase:" + msg.YuekaInfo.TotalPurchase);
            Debug.Log("totalTime:" + msg.YuekaInfo.TotalTime);
            Debug.Log("xlym:MonthCardInfo End");
            if (msg.YuekaInfo.rewardsList != null)
            {
                foreach (var data in msg.YuekaInfo.rewardsList)
                {
                    MonthRewardClass reward = new MonthRewardClass();
                    reward.id = data.Id;
                    reward.num = data.Num;
                    reward.type = data.Type;
                    monthCardInfo.rewardsList.Add(reward);
                }
            }
        }
	
		
		public void SetQxzbPvPDataInfo(SCQxzbPVPDataAsk msg)
		{
			if(qxzbPvPCardList == null)
			{
				qxzbPvPCardList = new List<PVPPlayerInfo[]>();
			}
			else
			{
				qxzbPvPCardList.Clear();
			}
			
			
			IList<PVPUserBaseData> PlayerData3 = msg.pvpStar3List;
			PVPPlayerInfo[] pvpStar3 = new PVPPlayerInfo[8];
			int index = 0;
			foreach(PVPUserBaseData pvpBd in PlayerData3)
			{
				PVPPlayerInfo playerInfo = new PVPPlayerInfo();
				SetMsgPvPPlayerDate(pvpBd, ref playerInfo);
				if(index < pvpStar3.Length)
				{
					pvpStar3[index] = playerInfo;
				}
				index++;
			}
			qxzbPvPCardList.Add(pvpStar3);
			
			
			
			IList<PVPUserBaseData> PlayerData4 = msg.pvpStar4List;
			PVPPlayerInfo[] pvpStar4 = new PVPPlayerInfo[8];
			index = 0;
			foreach(PVPUserBaseData pvpBd in PlayerData4)
			{
				PVPPlayerInfo playerInfo = new PVPPlayerInfo();
				SetMsgPvPPlayerDate(pvpBd, ref playerInfo);
				if(index < pvpStar4.Length)
				{
					pvpStar4[index] = playerInfo;
				}
				index++;
			}
			qxzbPvPCardList.Add(pvpStar4);
			
			
			IList<PVPUserBaseData> PlayerData5 = msg.pvpStar5List;
			PVPPlayerInfo[] pvpStar5 = new PVPPlayerInfo[8];
			index = 0;
			foreach(PVPUserBaseData pvpBd in PlayerData5)
			{
				PVPPlayerInfo playerInfo = new PVPPlayerInfo();
				SetMsgPvPPlayerDate(pvpBd, ref playerInfo);
				if(index < pvpStar5.Length)
				{
					pvpStar5[index] = playerInfo;
				}
				index++;
			}
			qxzbPvPCardList.Add(pvpStar5);
			
			
			
			IList<PVPUserBaseData> PlayerData6 = msg.pvpStar6List;
			PVPPlayerInfo[] pvpStar6 = new PVPPlayerInfo[8];
			index = 0;
			foreach(PVPUserBaseData pvpBd in PlayerData6)
			{
				PVPPlayerInfo playerInfo = new PVPPlayerInfo();
				SetMsgPvPPlayerDate(pvpBd, ref playerInfo);
				if(index < pvpStar6.Length)
				{
					pvpStar6[index] = playerInfo;
				}
				index++;
			}
			qxzbPvPCardList.Add(pvpStar6);
			
			
			
			IList<PVPUserBaseData> PlayerData7 = msg.pvpStar7List;
			PVPPlayerInfo[] pvpStar7 = new PVPPlayerInfo[8];
			index = 0;
			foreach(PVPUserBaseData pvpBd in PlayerData7)
			{
				PVPPlayerInfo playerInfo = new PVPPlayerInfo();
				SetMsgPvPPlayerDate(pvpBd, ref playerInfo);
				if(index < pvpStar7.Length)
				{
					pvpStar7[index] = playerInfo;
				}
				index++;
			}
			qxzbPvPCardList.Add(pvpStar7);
			
			
			//设置时间
			qxzbStarTime = msg.StartTime/1000;
			qxzbEndTime = msg.EndTime/1000;
			coldTime = msg.QxzbBeatCdTime/1000;
			cursysTime = (long)Time.time;
			nQxzbFightTime = msg.QxzbRemainingBeatCount;
			Debug.Log("qxzb ***************************** qxzb");
			Debug.Log("coldTime:" + coldTime);
			Debug.Log("qxzb ***************************** qxzb");
			if(msg.ranksList.Count == heroQxzbPvPRank.Length)
			{
				for(int i=0; i<heroQxzbPvPRank.Length; i++)
				{
					heroQxzbPvPRank[i] = msg.ranksList[i];
				}
			}
			
			if(msg.rewardsList.Count == heroQxzbPvPRewardID.Length)
			{
				for(int j=0; j<heroQxzbPvPRewardID.Length; j++)
				{
					heroQxzbPvPRewardID[j] = GetQxzbRewrdIDByRank(msg.rewardsList[j]);
					//Debug.Log("heroQxzbPvPRewardID[j] :" + heroQxzbPvPRewardID[j]);
				}
				
			}
			
			for(int i=0; i<msg.rewardsList.Count; i++)
			{
				Debug.Log("msg.rewardsList : " + msg.rewardsList[i]);
				if(msg.rewardsList[i] > 0)
				{
					
					heroQxzbPvPRank[i] = msg.rewardsList[i] ;
				}
			}
		}
		
		
		private int GetQxzbRewrdIDByRank(int nRank)
		{
			int curRank = nRank;
			int nRewardID = 0;
			int tabNum = TableManager.GetPvpnewReward().Count;
			for(int i = 1; i<= tabNum; i++)
			{
				Tab_PvpnewReward pvpNewRewardTab =  TableManager.GetPvpnewRewardByID(i);
				if(curRank <= pvpNewRewardTab.RankMin
					  && curRank >= pvpNewRewardTab.RankMax)
				{
					nRewardID = i;
					break;
				}
			}
		
			return nRewardID;
		}
		
		public void TestSetPvPPlayerDate()
		{
			if(qxzbPvPCardList == null)
			{
				qxzbPvPCardList = new List<PVPPlayerInfo[]>();
			}
			else
			{
				qxzbPvPCardList.Clear();
			}
			
			
			//IList<PVPUserBaseData> PlayerData3 = msg.pvpStar3List;
			PVPPlayerInfo[] pvpStar3 = new PVPPlayerInfo[8];
			int index = 0;
			for(int i=0; i<8; i++)
			{
				PVPPlayerInfo playerInfo = new PVPPlayerInfo();
				playerInfo.add_quality_att = 100+i;
				playerInfo.add_quality_hp = 200+i;
				playerInfo.nFight = 6000+i;
				playerInfo.nLev = 50+i;
				playerInfo.nRank = 1+i;
				playerInfo.strName="我真的晕了" + i.ToString();
				playerInfo.nTempleID = 1004;
				
				pvpStar3[i] = playerInfo;
				if(i==7)
				{
					playerInfo.nlGUID = 331122;
				}
				
			}
			qxzbPvPCardList.Add(pvpStar3);
			
			
			
			//IList<PVPUserBaseData> PlayerData4 = msg.pvpStar4List;
			PVPPlayerInfo[] pvpStar4 = new PVPPlayerInfo[8];
			index = 0;
			for(int i=0; i <8; i++)
			{
				
				PVPPlayerInfo playerInfo = new PVPPlayerInfo();
				playerInfo.add_quality_att = 100+i;
				playerInfo.add_quality_hp = 200+i;
				playerInfo.nFight = 6000+i;
				playerInfo.nLev = 80+i;
				playerInfo.nRank = 1+i;
				playerInfo.strName="你没晕" + i.ToString();
				playerInfo.nTempleID = 1014;
				
				pvpStar4[i] = playerInfo;
				
				
			}
			qxzbPvPCardList.Add(pvpStar4);
			
			
			//IList<PVPUserBaseData> PlayerData5 = msg.pvpStar5List;
			PVPPlayerInfo[] pvpStar5 = new PVPPlayerInfo[8];
			index = 0;
			for(int i=0; i<8; i++)
			{
				PVPPlayerInfo playerInfo = new PVPPlayerInfo();
				playerInfo.add_quality_att = 100+i;
				playerInfo.add_quality_hp = 200+i;
				playerInfo.nFight = 6000+i;
				playerInfo.nLev = 80+i;
				playerInfo.nRank = 1+i;
				playerInfo.strName="他晕了" + i.ToString();
				playerInfo.nTempleID = 1019;
				
				pvpStar5[i] = playerInfo;
			}
			qxzbPvPCardList.Add(pvpStar5);
			
			
			
			//IList<PVPUserBaseData> PlayerData6 = msg.pvpStar6List;
			PVPPlayerInfo[] pvpStar6 = new PVPPlayerInfo[8];
			index = 0;
			for(int i=0; i<8; i++)
			{
				PVPPlayerInfo playerInfo = new PVPPlayerInfo();
				playerInfo.add_quality_att = 100+i;
				playerInfo.add_quality_hp = 200+i;
				playerInfo.nFight = 6000+i;
				playerInfo.nLev = 80+i;
				playerInfo.nRank = 1+i;
				playerInfo.strName="他们晕了" + i.ToString();
				playerInfo.nTempleID = 1033;
				
				pvpStar6[i] = playerInfo;
			}
			qxzbPvPCardList.Add(pvpStar6);
			
			
			
			//IList<PVPUserBaseData> PlayerData7 = msg.pvpStar7List;
			PVPPlayerInfo[] pvpStar7 = new PVPPlayerInfo[8];
			index = 0;
			for(int i=0; i<8; i++)
			{
				
				PVPPlayerInfo playerInfo = new PVPPlayerInfo();
				playerInfo.add_quality_att = 100+i;
				playerInfo.add_quality_hp = 200+i;
				playerInfo.nFight = 6000+i;
				playerInfo.nLev = 80+i;
				playerInfo.nRank = 1+i;
				playerInfo.strName="他们全部晕了" + i.ToString();
				playerInfo.nTempleID = 1024;
				
				pvpStar7[i] = playerInfo;
				
			}
			qxzbPvPCardList.Add(pvpStar7);
			
			cursysTime = (long)Time.time;
			qxzbStarTime = 0;
			qxzbEndTime = 5;
			dollar = 200000;
			money = 300500;
			accountID = 331122;
			for(int i=0; i<5; i++)
			{
				heroQxzbPvPRank[0] = 0;
			}
			
			for(int j=0; j<5;j++)
			{
				heroQxzbPvPRewardID[j] = 9; 
			}
			//heroQxzbPvPRank[0] = 
					//public int[] heroQxzbPvPRank = new int[5];
		//public int[] heroQxzbPvPRewardID = new int[5];
		}
		
		private void SetMsgPvPPlayerDate(PVPUserBaseData pvpBd, ref PVPPlayerInfo playerInfo)
		{
			if(pvpBd == null)
			{
				return;
			}
			
			
			if(playerInfo == null)
			{
				playerInfo = new PVPPlayerInfo();
			}
			
			
			playerInfo.nlGUID = pvpBd.NGuid;
			playerInfo.strName = pvpBd.Name;
			playerInfo.nRank = pvpBd.NRank;
			playerInfo.nLev = pvpBd.NLevel;
			playerInfo.nTempleID = pvpBd.NTempleID;
			playerInfo.nFight = pvpBd.NFight;
			playerInfo.skill_level = pvpBd.Skill_level;
			playerInfo.add_quality_att = pvpBd.Add_quality_att;
			playerInfo.add_quality_hp = pvpBd.Add_quality_hp;
			
			
		}
		
		//获得星级队长
		public UserCardItem GetQxzbStarLeader(int nStar)
		{
			foreach(UserCardItem card in cardBagList)
			{
				//Debug.Log("card.qxzbFightIndex: " +card.qxzbFightIndex);
				if((card.qxzbFightIndex/100) == 0)
				{
					int nCardStar = (card.qxzbFightIndex%100)/10;
					if(nCardStar == nStar)
					{
						return card;
					}
				}
			}
			return null;
		}
		
		
		//获得背包中同一星级的卡牌
		public List<UserCardItem> GetBagCardListByStar(int nStar)
	    {
		      List<UserCardItem> cardList = new List<UserCardItem>();
		      foreach (UserCardItem item in cardBagList)
		      {
		        if (item.quality != nStar || item.cardType != UserCardItem.CardType.NORMAL_CARD)
		          continue;
		        cardList.Add(item);
		      }
		      
		      return cardList;
	    }
		
		//获得群雄争霸中星级上阵卡牌(队长不一定在 数组下标的第0位)
		public UserCardItem[] GetBagQxzbCardFightArrayByStar(int nStar)
		{
			UserCardItem[] cardArray = new UserCardItem[6];
			int nFindIndex = 0;
			foreach (UserCardItem item in cardBagList)
		    {
			   int starFight = 0;
			   if(item.qxzbFightIndex > 0)
			   {
				  starFight = (item.qxzbFightIndex%100)/10;
			   }
			   
			   if (nFindIndex <= cardArray.Length
					 && item.quality == nStar
					 	&& starFight == item.quality
					  	  && item.cardType == UserCardItem.CardType.NORMAL_CARD)
			   {
					cardArray[nFindIndex] = item;
					nFindIndex++;
			   }
		    } 
			
			return cardArray;
		}
		
		
		//当阵型拖动变化之后 刷新PvPBattleArray 中 第index卡的 PvP index
		public void refrashPvPBattleArray(int index)
		{
			long cardID = PvPBattleArray[index];
			if(cardID <= 0)
				return;
			foreach (UserCardItem item in cardBagList)
			{
				if (item.cardID == cardID)
				{
					item.qxzbFightIndex = (item.qxzbFightIndex / 10) * 10 + index; //只更新个位的数字
					break;
				}
			}
		}
		
		public void SetActivityData(SCAskActivity data)
		{
			if (data.HasActivityData)
			{
				ActivityInfo info = data.ActivityData;
				if (info.HasChangeCardFlag)
				{
					isChangeCardOpen = info.ChangeCardFlag == 0 ? false : true;
				}
                worldBossActiveFlag = info.WorldBossFlag;
			}
		}
		
		public void SetChangeCardListData(SCAskChangeCardList listData)
		{
			changeCardInfo.Clear();
			if (listData.HasEndTime)
				changeCardTimer = listData.EndTime;
			if (listData.infoList != null)
            {
                foreach (ChangeCardInfo info in listData.infoList)
                {
                    changeCardInfo.Add(info);
                }
            }
		}
		
		public void SetChangeCardConfirmData(SCChangeCardConfirm changeConfirm)
		{
			if (changeConfirm.Result == 1)
				LogModule.DebugLog("Change Card Confirm Success!");
			if (changeConfirm.HasTempleID)
				LogModule.DebugLog("TempleID : " + changeConfirm.TempleID);
			if (changeConfirm.HasBaseData)
			{
				LogModule.DebugLog("Set BaseData");
				SetUserBaseData(changeConfirm.BaseData,0);
			}
			if (changeConfirm.HasBagData)
			{
				LogModule.DebugLog("Set BagData");
				SetUserBagData(changeConfirm.BagData);
			}
			if (changeConfirm.HasCopyData)
			{
				LogModule.DebugLog("Set CopyData");
				SetUserCopyData(changeConfirm.CopyData);
			}
			if (changeConfirm.HasActivityData)
			{
				LogModule.DebugLog("Set Activity Data");
				SCAskActivity activity = new SCAskActivity();
				activity.ActivityData = changeConfirm.ActivityData;
				SetActivityData(activity);
			}
		}
		
		
		//lottery end
		
#if UNITY_ANDROID
		public float cyou_pay_time = 0.0f;
		public void setPurchaseInfoData(SCCYouProductList data)
        {
            purchaseInfoList.Clear();            //isFirstPurchase=data.IsFirst;
            foreach (ProductInfo product in data.productList)
            {              
				PurchaseInfo purchase = new PurchaseInfo(product);                
				purchaseInfoList.Add(purchase);
            }
        }
			
		public void setThirdPurchaseInfoData(SCCommonProductListRet data)
        {
			Debug.Log("----setThirdPurchaseInfoData");
            purchaseInfoList.Clear();            //isFirstPurchase=data.IsFirst;
            foreach (ProductInfo product in data.productList)
            {          
				
				PurchaseInfo purchase = new PurchaseInfo(product);    
				Debug.Log("----product "+purchase.goodsId);
				purchaseInfoList.Add(purchase);
            }
        }
#endif		
		
		public string userName ;
		public string passward ;
//------------请求任务数据开始------------by zhouwei of haiwai
		public bool requestTaskSuccess = false;
//------------请求任务数据结束------------by zhouwei of haiwai	
		
//------------请求任务数据开始------------by zhouwei of haiwai
		public List<YunyingHuodong> yunyinHuoDongLists = new List<YunyingHuodong>();
		public bool HasHuodong = false;
		public string HuodongMiaoshu;
//------------请求任务数据结束------------by zhouwei of haiwai		
		public void SetYunyinghuodong(SCYunyingHuodong data){
			
			HuodongMiaoshu = data.HuodongMiaoshu;
			HasHuodong = data.HasHuodong > 0 ?true:false;
			
			foreach(PBYunyingHuodong yunying_temp in data.HuodongArrayList){
				YunyingHuodong yunying = new YunyingHuodong(yunying_temp.ItemType,
															yunying_temp.ItemValue,
															yunying_temp.ItemNumber);
				yunyinHuoDongLists.Add(yunying);
			}
		}		
    }

}
