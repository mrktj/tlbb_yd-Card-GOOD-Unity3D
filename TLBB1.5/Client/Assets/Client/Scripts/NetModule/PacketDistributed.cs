//Auto Generate File, Do NOT Modify!!!!!!!!!!!!!!!

using System.IO;
using System;
using System.Net.Sockets;
using Google.ProtocolBuffers;
using xjgame.message; 
using card.net;

    public abstract class PacketDistributed

    {

        public static PacketDistributed CreatePacket(MessageID packetID)
        {
            PacketDistributed packet = null;
            switch (packetID)
            {
			case MessageID.CSLogin: { packet = new CSLogin();}break;
			case MessageID.SCLoginRet: { packet = new SCLoginRet();}break;
			case MessageID.CSLoginThirdPlatform: { packet = new CSLoginThirdPlatform();}break;
			case MessageID.SCLoginThirdPlatformRet: { packet = new SCLoginThirdPlatformRet();}break;
			case MessageID.CSThirdPlatformVerifyCharge: { packet = new CSThirdPlatformVerifyCharge();}break;
			case MessageID.SCThirdPlatformVerifyCharge: { packet = new SCThirdPlatformVerifyCharge();}break;
			case MessageID.CSAskUserData: { packet = new CSAskUserData();}break;
			case MessageID.PBUserBaseData: { packet = new PBUserBaseData();}break;
			case MessageID.PBUserBagData: { packet = new PBUserBagData();}break;
			case MessageID.PBUserCopyData: { packet = new PBUserCopyData();}break;
			case MessageID.SCAskUserData: { packet = new SCAskUserData();}break;
			case MessageID.MissionInfo: { packet = new MissionInfo();}break;
			case MessageID.CopyInfo: { packet = new CopyInfo();}break;
			case MessageID.ItemInfo: { packet = new ItemInfo();}break;
			case MessageID.CardInfo: { packet = new CardInfo();}break;
			case MessageID.DropBag: { packet = new DropBag();}break;
			case MessageID.BattleCard: { packet = new BattleCard();}break;
			case MessageID.CSBattleData: { packet = new CSBattleData();}break;
			case MessageID.DataBuffInfo: { packet = new DataBuffInfo();}break;
			case MessageID.DataSingleAction: { packet = new DataSingleAction();}break;
			case MessageID.DataAction: { packet = new DataAction();}break;
			case MessageID.DataRound: { packet = new DataRound();}break;
			case MessageID.DataBattle: { packet = new DataBattle();}break;
			case MessageID.SCBattleData: { packet = new SCBattleData();}break;
			case MessageID.CSPVPBattleData: { packet = new CSPVPBattleData();}break;
			case MessageID.SCPVPBattleData: { packet = new SCPVPBattleData();}break;
			case MessageID.CSChangeMember: { packet = new CSChangeMember();}break;
			case MessageID.SCChangeMember: { packet = new SCChangeMember();}break;
			case MessageID.CSCardCombining: { packet = new CSCardCombining();}break;
			case MessageID.SCCardCombiningRet: { packet = new SCCardCombiningRet();}break;
			case MessageID.CSCardEvolve: { packet = new CSCardEvolve();}break;
			case MessageID.SCCardEvolveRet: { packet = new SCCardEvolveRet();}break;
			case MessageID.CSCardStrengthen: { packet = new CSCardStrengthen();}break;
			case MessageID.SCCardStrengthenRet: { packet = new SCCardStrengthenRet();}break;
			case MessageID.CSFriendsList: { packet = new CSFriendsList();}break;
			case MessageID.SCFriendsList: { packet = new SCFriendsList();}break;
			case MessageID.CSSearchFriend: { packet = new CSSearchFriend();}break;
			case MessageID.SCSearchFriend: { packet = new SCSearchFriend();}break;
			case MessageID.CSGiveFriendPower: { packet = new CSGiveFriendPower();}break;
			case MessageID.SCGiveFriendPower: { packet = new SCGiveFriendPower();}break;
			case MessageID.CSGetFriendPower: { packet = new CSGetFriendPower();}break;
			case MessageID.SCGetFriendPower: { packet = new SCGetFriendPower();}break;
			case MessageID.CSADDFriend: { packet = new CSADDFriend();}break;
			case MessageID.SCADDFriend: { packet = new SCADDFriend();}break;
			case MessageID.CSDeleteFriend: { packet = new CSDeleteFriend();}break;
			case MessageID.SCDeleteFriend: { packet = new SCDeleteFriend();}break;
			case MessageID.CSGetRandomAssistanceList: { packet = new CSGetRandomAssistanceList();}break;
			case MessageID.SCGetRandomAssistanceList: { packet = new SCGetRandomAssistanceList();}break;
			case MessageID.PBFriend: { packet = new PBFriend();}break;
			case MessageID.CSSellCard: { packet = new CSSellCard();}break;
			case MessageID.SCSellCard: { packet = new SCSellCard();}break;
			case MessageID.SCErrorMsg: { packet = new SCErrorMsg();}break;
			case MessageID.CSMailList: { packet = new CSMailList();}break;
			case MessageID.SCMailList: { packet = new SCMailList();}break;
			case MessageID.PBMail: { packet = new PBMail();}break;
			case MessageID.CSMailDelete: { packet = new CSMailDelete();}break;
			case MessageID.SCMailDelete: { packet = new SCMailDelete();}break;
			case MessageID.CSMailSend: { packet = new CSMailSend();}break;
			case MessageID.SCMailSend: { packet = new SCMailSend();}break;
			case MessageID.CSMailFriend: { packet = new CSMailFriend();}break;
			case MessageID.SCMailFriend: { packet = new SCMailFriend();}break;
			case MessageID.CSMailSystem: { packet = new CSMailSystem();}break;
			case MessageID.SCMailSystem: { packet = new SCMailSystem();}break;
			case MessageID.CSMailRead: { packet = new CSMailRead();}break;
			case MessageID.SCMailRead: { packet = new SCMailRead();}break;
			case MessageID.CSGMcmds: { packet = new CSGMcmds();}break;
			case MessageID.SCGMcmds: { packet = new SCGMcmds();}break;
			case MessageID.CSTaskList: { packet = new CSTaskList();}break;
			case MessageID.SCTaskList: { packet = new SCTaskList();}break;
			case MessageID.PBTask: { packet = new PBTask();}break;
			case MessageID.CSFinishTask: { packet = new CSFinishTask();}break;
			case MessageID.SCFinishTask: { packet = new SCFinishTask();}break;
			case MessageID.CSShop: { packet = new CSShop();}break;
			case MessageID.SCShopRet: { packet = new SCShopRet();}break;
			case MessageID.CSGamble: { packet = new CSGamble();}break;
			case MessageID.SCGambleRet: { packet = new SCGambleRet();}break;
			case MessageID.CSGuide: { packet = new CSGuide();}break;
			case MessageID.SCGuide: { packet = new SCGuide();}break;
			case MessageID.ButtonInfo: { packet = new ButtonInfo();}break;
			case MessageID.CSChangeName: { packet = new CSChangeName();}break;
			case MessageID.SCChangeName: { packet = new SCChangeName();}break;
			case MessageID.CSBindAccount: { packet = new CSBindAccount();}break;
			case MessageID.SCBindAccount: { packet = new SCBindAccount();}break;
			case MessageID.CSAskPVPList: { packet = new CSAskPVPList();}break;
			case MessageID.SCAskPVPList: { packet = new SCAskPVPList();}break;
			case MessageID.PVPUserBaseData: { packet = new PVPUserBaseData();}break;
			case MessageID.CSAskScoreShopFresh: { packet = new CSAskScoreShopFresh();}break;
			case MessageID.SCAskScoreShopFresh: { packet = new SCAskScoreShopFresh();}break;
			case MessageID.CSPVPShop: { packet = new CSPVPShop();}break;
			case MessageID.SCPVPShopRet: { packet = new SCPVPShopRet();}break;
			case MessageID.ProductInfo: { packet = new ProductInfo();}break;
			case MessageID.CSProductList: { packet = new CSProductList();}break;
			case MessageID.SCProductList: { packet = new SCProductList();}break;
			case MessageID.CSClearBattleData: { packet = new CSClearBattleData();}break;
			case MessageID.SCClearBattleData: { packet = new SCClearBattleData();}break;
			case MessageID.CSRandomCardFree: { packet = new CSRandomCardFree();}break;
			case MessageID.SCRandomCardFree: { packet = new SCRandomCardFree();}break;
			case MessageID.CS20038: { packet = new CS20038();}break;
			case MessageID.SC30038: { packet = new SC30038();}break;
			case MessageID.CS20039: { packet = new CS20039();}break;
			case MessageID.SC30039: { packet = new SC30039();}break;
			case MessageID.CS20040: { packet = new CS20040();}break;
			case MessageID.SC30040: { packet = new SC30040();}break;
			case MessageID.CS20041: { packet = new CS20041();}break;
			case MessageID.SC30041: { packet = new SC30041();}break;
			case MessageID.CSscode: { packet = new CSscode();}break;
			case MessageID.SCscode: { packet = new SCscode();}break;
			case MessageID.CSCYouProductList: { packet = new CSCYouProductList();}break;
			case MessageID.SCCYouProductList: { packet = new SCCYouProductList();}break;
			case MessageID.CSCYouVerifyCharge: { packet = new CSCYouVerifyCharge();}break;
			case MessageID.SCCYouVerifyCharge: { packet = new SCCYouVerifyCharge();}break;
			case MessageID.CSPPProductList: { packet = new CSPPProductList();}break;
			case MessageID.SCPPProductList: { packet = new SCPPProductList();}break;
			case MessageID.CSPPVerifyCharge: { packet = new CSPPVerifyCharge();}break;
			case MessageID.SCPPVerifyCharge: { packet = new SCPPVerifyCharge();}break;
			case MessageID.CSBuyMoney: { packet = new CSBuyMoney();}break;
			case MessageID.SCBuyMoney: { packet = new SCBuyMoney();}break;
			case MessageID.CSBuyPower: { packet = new CSBuyPower();}break;
			case MessageID.SCBuyPower: { packet = new SCBuyPower();}break;
			case MessageID.CSSDKLoginThirdPlatform: { packet = new CSSDKLoginThirdPlatform();}break;
			case MessageID.SCSDKLoginThirdPlatformRet: { packet = new SCSDKLoginThirdPlatformRet();}break;
			case MessageID.CSSDKRefresh: { packet = new CSSDKRefresh();}break;
			case MessageID.SCSDKRefreshRet: { packet = new SCSDKRefreshRet();}break;
			case MessageID.CSCommonProductList: { packet = new CSCommonProductList();}break;
			case MessageID.SCCommonProductListRet: { packet = new SCCommonProductListRet();}break;
			case MessageID.CSCYouPayVerifyCharge: { packet = new CSCYouPayVerifyCharge();}break;
			case MessageID.SCCYouPayVerifyChargeRet: { packet = new SCCYouPayVerifyChargeRet();}break;
			case MessageID.CSGGL: { packet = new CSGGL();}break;
			case MessageID.SCGGL: { packet = new SCGGL();}break;
			case MessageID.CSWuxingActivation: { packet = new CSWuxingActivation();}break;
			case MessageID.SCWuxingActivation: { packet = new SCWuxingActivation();}break;
			case MessageID.CSWuxingLevelup: { packet = new CSWuxingLevelup();}break;
			case MessageID.SCWuxingLevelup: { packet = new SCWuxingLevelup();}break;
			case MessageID.CSWuxingReset: { packet = new CSWuxingReset();}break;
			case MessageID.SCWuxingReset: { packet = new SCWuxingReset();}break;
			case MessageID.WuxingInfo: { packet = new WuxingInfo();}break;
			case MessageID.SuipianInfo: { packet = new SuipianInfo();}break;
			case MessageID.FengshuiInfo: { packet = new FengshuiInfo();}break;
			case MessageID.CSBGZ: { packet = new CSBGZ();}break;
			case MessageID.SCBGZ: { packet = new SCBGZ();}break;
			case MessageID.MonthCardInfo: { packet = new MonthCardInfo();}break;
			case MessageID.CSMonthCardGetDollar: { packet = new CSMonthCardGetDollar();}break;
			case MessageID.SCMonthCardGetDollar: { packet = new SCMonthCardGetDollar();}break;
			case MessageID.MonthReward: { packet = new MonthReward();}break;
			case MessageID.CSMonthCardInfo: { packet = new CSMonthCardInfo();}break;
			case MessageID.SCMonthCardInfo: { packet = new SCMonthCardInfo();}break;
			case MessageID.CSFriendMailDelete: { packet = new CSFriendMailDelete();}break;
			case MessageID.SCFriendMailDelete: { packet = new SCFriendMailDelete();}break;
			case MessageID.CSStudySkill: { packet = new CSStudySkill();}break;
			case MessageID.SCStudySkill: { packet = new SCStudySkill();}break;
			case MessageID.CSStudySkillUpdate: { packet = new CSStudySkillUpdate();}break;
			case MessageID.SCStudySkillUpdate: { packet = new SCStudySkillUpdate();}break;
			case MessageID.CSQxzbPVPDataAsk: { packet = new CSQxzbPVPDataAsk();}break;
			case MessageID.SCQxzbPVPDataAsk: { packet = new SCQxzbPVPDataAsk();}break;
			case MessageID.CSQxzbBattle: { packet = new CSQxzbBattle();}break;
			case MessageID.SCQxzbBattle: { packet = new SCQxzbBattle();}break;
			case MessageID.CSQxzbPVPClearCD: { packet = new CSQxzbPVPClearCD();}break;
			case MessageID.SCQxzbPVPClearCD: { packet = new SCQxzbPVPClearCD();}break;
			case MessageID.CSQxzbGetReward: { packet = new CSQxzbGetReward();}break;
			case MessageID.SCQxzbGetReward: { packet = new SCQxzbGetReward();}break;
			case MessageID.CSPaiTaBattleData: { packet = new CSPaiTaBattleData();}break;
			case MessageID.SCPaiTaBattleData: { packet = new SCPaiTaBattleData();}break;
			case MessageID.CSClearPaiTaBattleData: { packet = new CSClearPaiTaBattleData();}break;
			case MessageID.SCClearPaiTaBattleData: { packet = new SCClearPaiTaBattleData();}break;
			case MessageID.CSAskWorldBossInfo: { packet = new CSAskWorldBossInfo();}break;
			case MessageID.SCAskWorldBossInfo: { packet = new SCAskWorldBossInfo();}break;
			case MessageID.WorldBoss: { packet = new WorldBoss();}break;
			case MessageID.WorldBossKillInfo: { packet = new WorldBossKillInfo();}break;
			case MessageID.WorldBossAttInfo: { packet = new WorldBossAttInfo();}break;
			case MessageID.WorldBossDamageRankInfo: { packet = new WorldBossDamageRankInfo();}break;
			case MessageID.CSAskWorldBossBattle: { packet = new CSAskWorldBossBattle();}break;
			case MessageID.SCAskWorldBossBattle: { packet = new SCAskWorldBossBattle();}break;
			case MessageID.CSWorldBossAddZhufu: { packet = new CSWorldBossAddZhufu();}break;
			case MessageID.SCWorldBossAddZhufu: { packet = new SCWorldBossAddZhufu();}break;
			case MessageID.CSWorldBossResurgence: { packet = new CSWorldBossResurgence();}break;
			case MessageID.SCWorldBossResurgence: { packet = new SCWorldBossResurgence();}break;
			case MessageID.CSAskActivity: { packet = new CSAskActivity();}break;
			case MessageID.SCAskActivity: { packet = new SCAskActivity();}break;
			case MessageID.ActivityInfo: { packet = new ActivityInfo();}break;
			case MessageID.ChangeCardInfo: { packet = new ChangeCardInfo();}break;
			case MessageID.cardGuidAndTempleID: { packet = new cardGuidAndTempleID();}break;
			case MessageID.CSAskChangeCardList: { packet = new CSAskChangeCardList();}break;
			case MessageID.SCAskChangeCardList: { packet = new SCAskChangeCardList();}break;
			case MessageID.CSChangeCardConfirm: { packet = new CSChangeCardConfirm();}break;
			case MessageID.SCChangeCardConfirm: { packet = new SCChangeCardConfirm();}break;
			case MessageID.CSWorldBossWeekRank: { packet = new CSWorldBossWeekRank();}break;
			case MessageID.SCWorldBossWeekRank: { packet = new SCWorldBossWeekRank();}break;
			case MessageID.CSWorldBossWeekReward: { packet = new CSWorldBossWeekReward();}break;
			case MessageID.SCWorldBossWeekReward: { packet = new SCWorldBossWeekReward();}break;
			case MessageID.CSTaskOver: { packet = new CSTaskOver();}break;
			case MessageID.SCTaskOver: { packet = new SCTaskOver();}break;
			case MessageID.PBYunyingHuodong: { packet = new PBYunyingHuodong();}break;
			case MessageID.CSYunyingHuodong: { packet = new CSYunyingHuodong();}break;
			case MessageID.SCYunyingHuodong: { packet = new SCYunyingHuodong();}break;
			case MessageID.CSGooglePayVerifyCharge: { packet = new CSGooglePayVerifyCharge();}break;
			case MessageID.SCGooglePayVerifyChargeRet: { packet = new SCGooglePayVerifyChargeRet();}break;

            }
            if (null != packet)
            {
                packet.packetID = packetID;
            }
            //netActionTime = DateTime.Now.ToFileTimeUtc();
            return packet;
        }
       
        public byte[] ToByteArray()
        {
            //Check must init
            if (!IsInitialized())
            {
                throw InvalidProtocolBufferException.ErrorMsg("Request data have not set");
            }
            byte[] data = new byte[SerializedSize()];
            CodedOutputStream output = CodedOutputStream.CreateInstance(data);
            WriteTo(output);
            output.CheckNoSpaceLeft();
            return data;
        }
        public PacketDistributed ParseFrom(byte[] data)
        {
            CodedInputStream input = CodedInputStream.CreateInstance(data);
            PacketDistributed inst = MergeFrom(input,this);
            input.CheckLastTagWas(0);
            return inst;
        }

        public abstract int SerializedSize();
        public abstract void WriteTo(CodedOutputStream data);
        public abstract PacketDistributed MergeFrom(CodedInputStream input,PacketDistributed _Inst);
        public abstract bool IsInitialized();

        protected MessageID packetID;
	
	 	public MessageID getMessageID()
        { 
            return packetID;
        }
    }
