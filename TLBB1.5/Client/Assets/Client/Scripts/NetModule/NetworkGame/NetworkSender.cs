using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xjgame.message;
using System.Collections;
using Games.CharacterLogic;
using Games.LogicObject;
using UnityEngine;
using GCGame.Table;
using Module.Log;
using Games.Battle;

namespace card.net
{
    class NetworkSender
    {
        private UIListener.OnReceive buyGoldFun = null; //仅用于缓存购买金币的回调函数
        private UIListener.OnReceive buyPowerFun = null; //仅用于缓存购买体力的回调函数
        private int buyGoldCost = 0;
        private int buyPowerCost = 0;

        //王明磊 - 发送锁,为true时禁止发送,所有请求都丢弃
        private bool sendLock = false;

        private static NetworkSender m_instance = null;
        public static NetworkSender Instance()
        {
            if (null == m_instance)
            {
                m_instance = new NetworkSender();
                NetManager.registierPacketHandler(new NetworkReceiver());
            }
            return m_instance;
        }

        //发送完毕,解除发送锁
        public void sendFinish(bool isSuccess)
        {
            if (!isSuccess)
                Debug.LogError("Last Packet Send Failure");
            Debug.Log("Send Lock Cancel");
            sendLock = false;
        }

        public void send(UIListener.OnReceive funReceive, PacketDistributed packet, bool isblockScreen)
        {
            if (sendLock)
            {
                Debug.LogError("Send Lock, Request Refuse : " + packet.getMessageID());
                Debug.Log("xlym:loginFailFlag:6");
                return;
            }
            if (packet == null)
            {
                Debug.Log("xlym:loginFailFlag:7");
                return;
            }
			//Android接入渠道SDK登录判断，解决不断发送登录消息问题  2014-01-10
            if (packet.getMessageID() != MessageID.CSLogin && packet.getMessageID() != MessageID.CSSDKLoginThirdPlatform)
            {
                NetManager.lastFunReceive = funReceive;
                NetManager.lastPacket = packet;
                NetManager.lastIsblockScreen = isblockScreen;
            }
            Debug.Log("Send Lock By : " + packet.getMessageID());
            sendLock = true;

            if (isblockScreen)
            {
                if (BoxManager.topFrame == null)
                {
                    BoxManager.showMessageByID((int)MessageIdEnum.Msg104);
                }
            }
            UIListener.Instance().AddReciever(funReceive);
            UIListener.Instance().isBlock = isblockScreen;
            HTTPClientAPI.SendByProtoBuf(packet);
        }
        public void CyouLogin(UIListener.OnReceive funReceive, int type, string email, string password, string visitor_id)
        {
			Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~1");
#if UNITY_ANDROID
			if(channelLogin(funReceive, visitor_id, type))
			{
            Debug.Log("xlym:loginFailFlag:5");
				return;
			}
#endif
            CSLogin msg = (CSLogin)PacketDistributed.CreatePacket(MessageID.CSLogin);
			Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~2");
#if UNITY_IPHONE
            msg.Mid = DeviceHelper.GetMacAddress();
#else
			msg.Mid = GameManager.getMacAddress(); 
#endif
			Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~3");
            msg.Version = DeviceHelper.GetVersionNumber();//ClientConfigure.GetClientVersion();
			Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~4");
            msg.Type = type;
            if (string.IsNullOrEmpty(email))
            {
                // use token login
                msg.Username = "";
                msg.Password = "";
                msg.Token = password;
            }
            else
            {
                msg.Username = email;
                msg.Password = password;
				msg.Token = password;
            }

            if (!string.IsNullOrEmpty(visitor_id)) msg.AccountId = visitor_id;
			Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~5");
#if UNITY_IPHONE
            msg.Area = DeviceHelper.GetUserLocale();
            msg.Country = DeviceHelper.GetUserContry();
            msg.Device = GetiOSDeviceType();
            msg.DeviceSystem = DeviceHelper.GetSystemName();
            msg.NetworkType = DeviceHelper.GetNetworkState();
            msg.DownloadType = DeviceHelper.GetChannelID();
            msg.PrisonBreak = DeviceHelper.IsJailbroken() ? "1" : "0";
            //			msg.PrisonBreak = DeviceHelper.IsJailbroken().ToString();
            msg.Operator = DeviceHelper.GetNetWorkProvider();
#elif UNITY_ANDROID
			msg.Area = DeviceHelper.GetUserLocale();
			Debug.Log("Area is : " + msg.Area);
			msg.Country = DeviceHelper.GetUserContry();
			Debug.Log("Country is : " + msg.Country);
			msg.Device = WebMediator.GetModel();
			Debug.Log("Device is : " + msg.Device);
			msg.DeviceSystem = DeviceHelper.GetSystemName();
			Debug.Log("DeviceSystem is : " + msg.DeviceSystem);
			msg.NetworkType = DeviceHelper.GetNetworkState();
			Debug.Log("NetworkType is : " + msg.NetworkType);
			msg.DownloadType = DeviceHelper.GetChannelID();
			Debug.Log("DownloadType is : " + msg.DownloadType);
			msg.PrisonBreak = DeviceHelper.IsJailbroken() ? "1" : "0";
			Debug.Log("PrisonBreak is : " + msg.PrisonBreak);
			msg.Operator = DeviceHelper.GetNetWorkProvider();
			Debug.Log("Operator is : " + msg.Operator);
#else
			msg.Mid = "2c:27:d7:46:50:22";
			msg.Area = "Beijing";
			msg.Country = "China";
			msg.Device = "Samsung";
			msg.DeviceSystem = "Android 4.0";
			msg.DownloadType = "win32";
			msg.NetworkType = "WiFi";
			msg.PrisonBreak = "0";
			msg.Operator = "China Unicom";
#endif

			Debug.Log("Mid is : " + msg.Mid);
			
            xjgame.message.ButtonInfo buttonInfo;
            for (int btnNo = 1; btnNo <= 58; btnNo++)
            {
                string key = "Btn" + btnNo.ToString();
                if (PlayerPrefs.HasKey(key))
                {
                    buttonInfo = new xjgame.message.ButtonInfo();
                    buttonInfo.Id = btnNo;
                    buttonInfo.Value = PlayerPrefs.GetInt(key);
                    msg.AddInfo(buttonInfo);
                }
            }
            if (PlayerPrefs.HasKey("LastAccountId"))
                msg.LastAccountId = PlayerPrefs.GetString("LastAccountId");

            send(funReceive, msg, true);
        }
        public void Login(UIListener.OnReceive funReceive, string account_id, int type)
        {
#if UNITY_ANDROID
			if(channelLogin(funReceive, account_id,type))
			{
				return;
			}
#endif
            CSLogin msg = (CSLogin)PacketDistributed.CreatePacket(MessageID.CSLogin);//new CSLogin();
#if UNITY_IPHONE
            msg.Mid = DeviceHelper.GetMacAddress();
#else
			msg.Mid = GameManager.getMacAddress(); 
#endif
            msg.Version = DeviceHelper.GetVersionNumber();// ClientConfigure.GetClientVersion();
            msg.AccountId = account_id;
            msg.LoginType = type;
            if (AccountManager.userType == AccountManager.UserType.OldUser)
                msg.Uid = PlayerPrefs.GetString("PLAYER_UID");
            if (AccountManager.IsUseAccount && AccountManager.userType == AccountManager.UserType.OldUser)
                msg.Username = AccountManager.Instance.CurAccount.email;

            Debug.Log("*** Jack Wen ***");
            Debug.Log("Login accoutn ID = " + msg.AccountId);
            Debug.Log("Uid: " + msg.Uid);
            Debug.Log("Username: " + msg.Username);
            Debug.Log("***    End   ***");

            //			PlayerPrefs.DeleteKey("ACCOUNT_ID");
            //			msg.AccountId =  PlayerPrefs.GetString("ACCOUNT_ID");

#if UNITY_IPHONE
            msg.Area = DeviceHelper.GetUserLocale();
            msg.Country = DeviceHelper.GetUserContry();
            msg.Device = GetiOSDeviceType();
            msg.DeviceSystem = DeviceHelper.GetSystemName();
            msg.NetworkType = DeviceHelper.GetNetworkState();
            msg.DownloadType = DeviceHelper.GetChannelID();
            //			msg.PrisonBreak = DeviceHelper.IsJailbroken().ToString();
            msg.PrisonBreak = DeviceHelper.IsJailbroken() ? "1" : "0";
            msg.Operator = DeviceHelper.GetNetWorkProvider();
#elif UNITY_ANDROID
			msg.Area = DeviceHelper.GetUserLocale();
			Debug.Log("Area is : " + msg.Area);
			msg.Country = DeviceHelper.GetUserContry();
			Debug.Log("Country is : " + msg.Country);
			msg.Device = WebMediator.GetModel();
			Debug.Log("Device is : " + msg.Device);
			msg.DeviceSystem = DeviceHelper.GetSystemName();
			Debug.Log("DeviceSystem is : " + msg.DeviceSystem);
			msg.NetworkType = DeviceHelper.GetNetworkState();
			Debug.Log("NetworkType is : " + msg.NetworkType);
			msg.DownloadType = DeviceHelper.GetChannelID();
			Debug.Log("DownloadType is : " + msg.DownloadType);
			msg.PrisonBreak = DeviceHelper.IsJailbroken() ? "1" : "0";
			Debug.Log("PrisonBreak is : " + msg.PrisonBreak);
			msg.Operator = DeviceHelper.GetNetWorkProvider();
			Debug.Log("Operator is : " + msg.Operator);
#else
			msg.Mid = "2c:27:d7:46:50:22";
			msg.Area = "Beijing";
			msg.Country = "China";
			msg.Device = "Samsung";
			msg.DeviceSystem = "Android 4.0";
			msg.DownloadType = "win32";
			msg.NetworkType = "WiFi";
			msg.PrisonBreak = "0";
			msg.Operator = "China Unicom";
#endif

			Debug.Log("Mid is : " + msg.Mid);
			
			
            xjgame.message.ButtonInfo buttonInfo;
            for (int btnNo = 1; btnNo <= 58; btnNo++)
            {
                string key = "Btn" + btnNo.ToString();
                if (PlayerPrefs.HasKey(key))
                {
                    buttonInfo = new xjgame.message.ButtonInfo();
                    buttonInfo.Id = btnNo;
                    buttonInfo.Value = PlayerPrefs.GetInt(key);
                    msg.AddInfo(buttonInfo);
                }
            }
            if (PlayerPrefs.HasKey("LastAccountId"))
                msg.LastAccountId = PlayerPrefs.GetString("LastAccountId");
            send(funReceive, msg, true);
        }

#if UNITY_IPHONE
        public string GetiOSDeviceType()
        {
            string deviceType;
            switch (iPhone.generation)
            {
                case iPhoneGeneration.iPad4Gen: deviceType = "iPad4Gen"; break;
                case iPhoneGeneration.iPad3Gen: deviceType = "iPad3Gen"; break;
                case iPhoneGeneration.iPadMini1Gen: deviceType = "iPadMini1Gen"; break;
                case iPhoneGeneration.iPhone5: deviceType = "iPhone5"; break;
                case iPhoneGeneration.iPhone4S: deviceType = "iPhone4S"; break;
                case iPhoneGeneration.iPhone4: deviceType = "iPhone4"; break;
                case iPhoneGeneration.iPodTouch5Gen: deviceType = "iPodTouch5Gen"; break;
                case iPhoneGeneration.iPodTouch4Gen: deviceType = "iPodTouch4Gen"; break;
                //Old Device
                case iPhoneGeneration.iPad2Gen: deviceType = "iPad2Gen"; break;
                case iPhoneGeneration.iPad1Gen: deviceType = "iPad1Gen"; break;
                case iPhoneGeneration.iPhone3G: deviceType = "iPhone3G"; break;
                case iPhoneGeneration.iPhone3GS: deviceType = "iPhone3GS"; break;
                case iPhoneGeneration.iPhone: deviceType = "iPhone"; break;
                case iPhoneGeneration.iPodTouch1Gen: deviceType = "iPodTouch1Gen"; break;
                case iPhoneGeneration.iPodTouch2Gen: deviceType = "iPodTouch2Gen"; break;
                case iPhoneGeneration.iPodTouch3Gen: deviceType = "iPodTouch3Gen"; break;
                case iPhoneGeneration.iPadUnknown: deviceType = "iPadUnknown"; break;
                case iPhoneGeneration.iPhoneUnknown: deviceType = "iPhoneUnknown"; break;
                case iPhoneGeneration.iPodTouchUnknown: deviceType = "iPodTouchUnknown"; break;
                default: deviceType = "Unknown"; break;
            }
            return deviceType;
        }
#endif

        public void createNewUser(UIListener.OnReceive funReceive, string name, int cardTempletId)
        {
            CSAskUserData msg = (CSAskUserData)PacketDistributed.CreatePacket(MessageID.CSAskUserData);//new CSAskUserData();
            msg.IsNewUser = 1;//true;
            msg.UserName = name;
            msg.CardTempletId = cardTempletId;
            send(funReceive, msg, true);
        }
        public void RecieveUserInfo(UIListener.OnReceive funReceive)
        {
            CSAskUserData msg = (CSAskUserData)PacketDistributed.CreatePacket(MessageID.CSAskUserData);//new CSAskUserData();
            msg.IsNewUser = 2;//false;
            send(funReceive, msg, false);
        }
        public void GetUserInfo(UIListener.OnReceive funReceive)
        {
            //			Debug.LogWarning("CSAskUserData()");

            CSAskUserData msg = (CSAskUserData)PacketDistributed.CreatePacket(MessageID.CSAskUserData);//new CSAskUserData();
            msg.IsNewUser = 2;//false;
            send(funReceive, msg, true);
        }
        //战斗请求--
        public void AskBattleData(UIListener.OnReceive funReceive, int round, int curSubcopyID)
        {
            Obj_MyselfPlayer.GetMe().SetBattleBeforeDate();
            Debug.Log("*********************beforeBattleUserBaseData************");
            Debug.Log("Name=" + Obj_MyselfPlayer.GetMe().accountName);
            Debug.Log("Mail=" + Obj_MyselfPlayer.GetMe().mail);
            Debug.Log("Level=" + Obj_MyselfPlayer.GetMe().level);
            Debug.Log("Exp=" + Obj_MyselfPlayer.GetMe().exp);
            Debug.Log("Money=" + Obj_MyselfPlayer.GetMe().money);
            Debug.Log("Friendpoint=" + Obj_MyselfPlayer.GetMe().fpoint);
            Debug.Log("Dollar=" + Obj_MyselfPlayer.GetMe().dollar);
            Debug.Log("Power=" + Obj_MyselfPlayer.GetMe().power);
            Debug.Log("Leadership=" + Obj_MyselfPlayer.GetMe().leadership);
            Debug.Log("NoReadMail=" + Obj_MyselfPlayer.GetMe().mailListCount);
            Debug.Log("Obj_MyselfPlayer.GetMe().battleData.isPlayed=" + Obj_MyselfPlayer.GetMe().battleData.isPlayed);
            Debug.Log("*********************beforeBattleUserBaseData************");

            CSBattleData msg = (CSBattleData)PacketDistributed.CreatePacket(MessageID.CSBattleData);//new CSBattleData();//(CSBattleData)PacketDistributed.CreatePacket(MessageID.CSBattleData);
            msg.Friendguid = (ulong)Obj_MyselfPlayer.GetMe().currentAssistFriend.guid;
            Debug.Log(Obj_MyselfPlayer.GetMe().currentAssistFriend.guid + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            for (int i = 0; i < 6; i++)
            {
                long cID = Obj_MyselfPlayer.GetMe().battleArray[i];
                if (cID == -1)
                {
                    continue;
                }
                if (cID == Obj_MyselfPlayer.GetMe().currentAssistFriend.cardGuiId)
                {
                    xjgame.message.BattleCard battlecard = new xjgame.message.BattleCard();
                    battlecard.Cardguid = (ulong)Obj_MyselfPlayer.GetMe().currentAssistFriend.cardGuiId;
                    battlecard.Isfriend = 1;
                    battlecard.CardID = Obj_MyselfPlayer.GetMe().currentAssistFriend.cardTempleId;
                    battlecard.Place_idx = i;
                    if (round == 1)
                    {
                        battlecard.State = 0;
                    }
                    else
                    {
                        if (BattleCardManager.Instance.FindCardByGUID(cID) != null)
                        {
                            battlecard.State = BattleCardManager.Instance.FindCardByGUID(cID).IsDead == true ? 1 : 0;
                        }
                        else
                        {
                            battlecard.State = 0;
                            Debug.LogError("AskBattleData(), can't find Battlecard!");
                        }
                    }
                    msg.AddInfo(battlecard);
                }
                else
                {
                    UserCardItem card = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(cID);
                    if (card != null)
                    {
                        xjgame.message.BattleCard battlecard = new xjgame.message.BattleCard();
                        battlecard.Cardguid = (ulong)card.cardID;
                        battlecard.Isfriend = 0;
                        battlecard.CardID = card.templateID;
                        battlecard.Place_idx = i;
                        if (round == 1)
                        {
                            battlecard.State = 0;
                        }
                        else
                        {
                            if (BattleCardManager.Instance.FindCardByGUID(cID) != null)
                            {
                                battlecard.State = BattleCardManager.Instance.FindCardByGUID(cID).IsDead == true ? 1 : 0;
                            }
                            else
                            {
                                battlecard.State = 0;
                                Debug.LogError("AskBattleData(), can't find Battlecard! guid = " + cID);
                            }
                        }
                        msg.AddInfo(battlecard);
                    }
                    else
                    {
                        Debug.LogError("AskBattleData(), can't find UserCardItem! guid = " + cID);
                    }
                }
            }
            msg.MissionID = curSubcopyID;
            msg.Round = round;
            if (round == 1)
            {
                msg.Cost_money = 0;
            }
            else
            {
                int money = 0;
                for (int i = 0; i < Obj_MyselfPlayer.GetMe().reviveCount; i++)
                {
                    money += (i + 1) * 1000;
                }
                msg.Cost_money = money;
            }
            string log = "AskBattleData(), missionID = " + msg.MissionID;
            log += "\t\t, round = " + msg.Round;
            log += "\t\t, friendguid = " + msg.Friendguid;
            log += "\t\t, cost_money = " + msg.Cost_money;
            Debug.LogWarning(log);
            foreach (xjgame.message.BattleCard card in msg.infoList)
            {
                log = "BattleCard: cardguid = " + card.Cardguid;
                log += "\t\t, cardID = " + card.CardID;
                log += "\t\t, place_idx = " + card.Place_idx;
                log += "\t\t, state = " + card.State;
                log += "\t\t, isfriend = " + card.Isfriend;
                Debug.LogWarning(log);
            }
            send(funReceive, msg, true);
        }
        //PVP战斗请求--
        public void AskPVPBattleData(UIListener.OnReceive funReceive, long enemy_guid, string enemy_name)
        {
            Obj_MyselfPlayer.GetMe().SetBattleBeforeDate();
            Debug.Log("*********************beforeBattleUserBaseData PVP************");
            Debug.Log("Name=" + Obj_MyselfPlayer.GetMe().accountName);
            Debug.Log("Mail=" + Obj_MyselfPlayer.GetMe().mail);
            Debug.Log("Level=" + Obj_MyselfPlayer.GetMe().level);
            Debug.Log("Exp=" + Obj_MyselfPlayer.GetMe().exp);
            Debug.Log("Money=" + Obj_MyselfPlayer.GetMe().money);
            Debug.Log("Friendpoint=" + Obj_MyselfPlayer.GetMe().fpoint);
            Debug.Log("Dollar=" + Obj_MyselfPlayer.GetMe().dollar);
            Debug.Log("Power=" + Obj_MyselfPlayer.GetMe().power);
            Debug.Log("Leadership=" + Obj_MyselfPlayer.GetMe().leadership);
            Debug.Log("NoReadMail=" + Obj_MyselfPlayer.GetMe().mailListCount);
            Debug.Log("Obj_MyselfPlayer.GetMe().battleData.isPlayed=" + Obj_MyselfPlayer.GetMe().battleData.isPlayed);
            Debug.Log("*********************beforeBattleUserBaseData PVP************");

            //Obj_MyselfPlayer.GetMe().isPVPBattle = true;
            Obj_MyselfPlayer.GetMe().enemyGUID = enemy_guid;
            Obj_MyselfPlayer.GetMe().enemyName = enemy_name;
            CSPVPBattleData msg = (CSPVPBattleData)PacketDistributed.CreatePacket(MessageID.CSPVPBattleData);
            for (int i = 0; i < 6; i++)
            {
                long cID = Obj_MyselfPlayer.GetMe().battleArray[i];
                if (cID == -1)
                {
                    continue;
                }

                UserCardItem card = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(cID);
                if (card != null)
                {
                    xjgame.message.BattleCard battlecard = new xjgame.message.BattleCard();
                    battlecard.Cardguid = (ulong)card.cardID;
                    battlecard.Isfriend = 0;
                    battlecard.CardID = card.templateID;
                    battlecard.Place_idx = i;
                    battlecard.State = 0;
                    msg.AddInfo(battlecard);
                }
                else
                {
                    Debug.LogError("AskBattleData(), can't find UserCardItem! guid = " + cID);
                }
            }
            msg.Guid = enemy_guid;
            msg.Round = 3;

            string log = "AskBattleData(), missionID = " + msg.Guid;
            log += "\t\t, round = " + msg.Round;
            Debug.LogWarning(log);
            foreach (xjgame.message.BattleCard card in msg.infoList)
            {
                log = "BattleCard: cardguid = " + card.Cardguid;
                log += "\t\t, cardID = " + card.CardID;
                log += "\t\t, place_idx = " + card.Place_idx;
                log += "\t\t, state = " + card.State;
                log += "\t\t, isfriend = " + card.Isfriend;
                Debug.LogWarning(log);
            }
            send(funReceive, msg, true);
        }
		
		public xjgame.message.BattleCard UserCardItemToBattle(UserCardItem card,int index)		//非好友卡牌类的转换
		{
			if (card != null)
            {
                xjgame.message.BattleCard battlecard = new xjgame.message.BattleCard();
                battlecard.Cardguid = (ulong)card.cardID;
                battlecard.Isfriend = 0;
                battlecard.CardID = card.templateID;
                battlecard.Place_idx = index;
                battlecard.State = 0;
				return battlecard;
            }
			else
			{
				return null;
			}
		}
		
        //重楼战斗请求--
        public void AskChonglouBattleData(UIListener.OnReceive funReceive)
        {
            Obj_MyselfPlayer.GetMe().SetBattleBeforeDate();
            Debug.Log("*********************beforeBattleUserBaseData************");
            Debug.Log("Name=" + Obj_MyselfPlayer.GetMe().accountName);
            Debug.Log("Mail=" + Obj_MyselfPlayer.GetMe().mail);
            Debug.Log("Level=" + Obj_MyselfPlayer.GetMe().level);
            Debug.Log("Exp=" + Obj_MyselfPlayer.GetMe().exp);
            Debug.Log("Money=" + Obj_MyselfPlayer.GetMe().money);
            Debug.Log("Friendpoint=" + Obj_MyselfPlayer.GetMe().fpoint);
            Debug.Log("Dollar=" + Obj_MyselfPlayer.GetMe().dollar);
            Debug.Log("Power=" + Obj_MyselfPlayer.GetMe().power);
            Debug.Log("Leadership=" + Obj_MyselfPlayer.GetMe().leadership);
            Debug.Log("NoReadMail=" + Obj_MyselfPlayer.GetMe().mailListCount);
            Debug.Log("Obj_MyselfPlayer.GetMe().battleData.isPlayed=" + Obj_MyselfPlayer.GetMe().battleData.isPlayed);
            Debug.Log("*********************beforeBattleUserBaseData************");

            CSPaiTaBattleData msg = (CSPaiTaBattleData)PacketDistributed.CreatePacket(MessageID.CSPaiTaBattleData);
            msg.Guid = Obj_MyselfPlayer.GetMe().accountID;
            msg.Round = 0;
            msg.Num = Obj_MyselfPlayer.GetMe().pataNum;
            for (int i = 0; i < 6; i++)
            {
                long cID = Obj_MyselfPlayer.GetMe().battleArray[i];
                if (cID == -1)
                {
                    continue;
                }

                UserCardItem card = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(cID);
                if (card != null)
                {
                    xjgame.message.BattleCard battlecard = new xjgame.message.BattleCard();
                    battlecard.Cardguid = (ulong)card.cardID;
                    battlecard.Isfriend = 0;
                    battlecard.CardID = card.templateID;
                    battlecard.Place_idx = i;
                    battlecard.State = 0;
                    msg.AddInfo(battlecard);
                }
                else
                {
                    Debug.LogError("AskBattleData(), can't find UserCardItem! guid = " + cID);
                }
            }
            string log = "AskBattleData(), guid = " + msg.Guid;
            log += "\t\t, round = " + msg.Round;
            log += "\t\t, num = " + msg.Num;
            Debug.LogWarning(log);
            foreach (xjgame.message.BattleCard card in msg.infoList)
            {
                log = "BattleCard: cardguid = " + card.Cardguid;
                log += "\t\t, cardID = " + card.CardID;
                log += "\t\t, place_idx = " + card.Place_idx;
                log += "\t\t, state = " + card.State;
                log += "\t\t, isfriend = " + card.Isfriend;
                Debug.LogWarning(log);
            }
            send(funReceive, msg, true);
        }
		
		//世界boss战斗请求
		public void AskWorldBossBata(UIListener.OnReceive funReceive)
		{
			Obj_MyselfPlayer.GetMe().SetBattleBeforeDate();
            Debug.Log("*********************beforeBattleUserBaseData************");
            Debug.Log("Name=" + Obj_MyselfPlayer.GetMe().accountName);
            Debug.Log("Mail=" + Obj_MyselfPlayer.GetMe().mail);
            Debug.Log("Level=" + Obj_MyselfPlayer.GetMe().level);
            Debug.Log("Exp=" + Obj_MyselfPlayer.GetMe().exp);
            Debug.Log("Money=" + Obj_MyselfPlayer.GetMe().money);
            Debug.Log("Friendpoint=" + Obj_MyselfPlayer.GetMe().fpoint);
            Debug.Log("Dollar=" + Obj_MyselfPlayer.GetMe().dollar);
            Debug.Log("Power=" + Obj_MyselfPlayer.GetMe().power);
            Debug.Log("Leadership=" + Obj_MyselfPlayer.GetMe().leadership);
            Debug.Log("NoReadMail=" + Obj_MyselfPlayer.GetMe().mailListCount);
            Debug.Log("Obj_MyselfPlayer.GetMe().battleData.isPlayed=" + Obj_MyselfPlayer.GetMe().battleData.isPlayed);
			CSAskWorldBossBattle msg = (CSAskWorldBossBattle)PacketDistributed.CreatePacket(MessageID.CSAskWorldBossBattle);
            msg.Bossid = Obj_MyselfPlayer.GetMe().activeBoss.id;
            msg.Round = 3;
			for(int i = 0; i < 6; i++)
			{
				long cID = Obj_MyselfPlayer.GetMe().battleArray[i];
                if (cID == -1)
                {
                    continue;
                }
                UserCardItem card = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(cID);
				xjgame.message.BattleCard bc = UserCardItemToBattle(card,i);
				if(bc != null)
				{
					msg.AddInto(bc);
				}
			}
            send(funReceive, msg, true);
		}
		
        //战斗结束确认--
        public void ClearBattleData(UIListener.OnReceive funReceive, int mission_id)
        {
            CSClearBattleData msg = (CSClearBattleData)PacketDistributed.CreatePacket(MessageID.CSClearBattleData);
            msg.CopyDetial = mission_id;
            Debug.Log("ClearBattleData(), mission_id = " + mission_id);
            send(funReceive, msg, true);
        }

        //爬塔战斗结束
        public void ClearPataBattleData(UIListener.OnReceive funReceive)
        {
			Obj_MyselfPlayer.GetMe().lastPataNum = Obj_MyselfPlayer.GetMe().pataNum;
            CSClearPaiTaBattleData msg = (CSClearPaiTaBattleData)PacketDistributed.CreatePacket(MessageID.CSClearPaiTaBattleData);
            msg.Guid = Obj_MyselfPlayer.GetMe().accountID;
			msg.Clearflag = Obj_MyselfPlayer.GetMe().pataClearFlag;
            //Debug.Log("ClearBattleData(), mission_id = " + mission_id);
            send(funReceive, msg, true);
        }
        //合成
        public void CardUpdate(UIListener.OnReceive funReceive)
        {
            CSCardCombining msg = (CSCardCombining)PacketDistributed.CreatePacket(MessageID.CSCardCombining);//new CSCardCombining();
            msg.Hero_cardguid = Obj_MyselfPlayer.GetMe().updateHeroItem.cardID;
            UserCardItem[] materials = Obj_MyselfPlayer.GetMe().updateMaterialItems;
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] != null && materials[i].cardID != -1)
                {
                    msg.AddSwallowed_cardguid(materials[i].cardID);
                }
            }
            //log
            string log = msg.Hero_cardguid.ToString();
            for (int i = 0; i < msg.swallowed_cardguidCount; i++)
            {
                log += " " + msg.swallowed_cardguidList[i].ToString();
            }
            LogModule.WarningLog("CardUpdate(), " + log);
            send(funReceive, msg, true);
        }
        //进化
        public void CardEvolution(UIListener.OnReceive funReceive)
        {
            CSCardEvolve msg = (CSCardEvolve)PacketDistributed.CreatePacket(MessageID.CSCardEvolve);
            msg.Evolve_cardguid = Obj_MyselfPlayer.GetMe().evolutionHeroItem.cardID;
            UserCardItem[] materials = Obj_MyselfPlayer.GetMe().evolutionMaterialItems;
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] != null && materials[i].cardID != -1)
                {
                    msg.AddMaterial_cardguid(materials[i].cardID);
                }
            }
            //log
            string log = msg.Evolve_cardguid.ToString();
            for (int i = 0; i < msg.material_cardguidCount; i++)
            {
                log += " " + msg.material_cardguidList[i].ToString();
            }
            LogModule.WarningLog("CardEvolution(), " + log);
            send(funReceive, msg, true);
        }
        //强化
        public void CardStrengthen(UIListener.OnReceive funReceive, UserCardItem card, int type)
        {
            CSCardStrengthen msg = (CSCardStrengthen)PacketDistributed.CreatePacket(MessageID.CSCardStrengthen);
            msg.Streng_cardguid = card.cardID;
            msg.Streng_type = type;
            LogModule.WarningLog("CardStrengthen(), card guid = " + card.cardID + " type = " + type);
            send(funReceive, msg, true);
        }
        //取得好友列表
        public void getFriendsList(UIListener.OnReceive funReceive)
        {
            CSFriendsList msg = (CSFriendsList)PacketDistributed.CreatePacket(MessageID.CSFriendsList);//new CSFriendsList();
            msg.User_guid = Obj_MyselfPlayer.GetMe().accountID;
            send(funReceive, msg, true);

        }
        //搜索好友
        public void SearchFriendsList(UIListener.OnReceive funReceive, long friend_guid)
        {
            CSSearchFriend msg = (CSSearchFriend)PacketDistributed.CreatePacket(MessageID.CSSearchFriend);//new CSSearchFriend();
            msg.Friend_guid = friend_guid;
            send(funReceive, msg, true);

        }
        //GM命令
        public void GMCommand(UIListener.OnReceive funReceive, String cmd)
        {
            CSGMcmds msg = (CSGMcmds)PacketDistributed.CreatePacket(MessageID.CSGMcmds);
            msg.Cmds = cmd;
            send(funReceive, msg, true);
        }
        //添加好友
        public void ADDFriend(UIListener.OnReceive funReceive, long friend_guid)
        {
            CSADDFriend msg = (CSADDFriend)PacketDistributed.CreatePacket(MessageID.CSADDFriend);//new CSADDFriend();
            msg.Friend_guid = friend_guid;
            send(funReceive, msg, true);
        }
        //删除好友
        public void deleteFriend(UIListener.OnReceive funReceive, long friend_guid)
        {
            CSDeleteFriend msg = (CSDeleteFriend)PacketDistributed.CreatePacket(MessageID.CSDeleteFriend);//new CSDeleteFriend();
            msg.Friend_guid = friend_guid;
            send(funReceive, msg, true);
        }
        //赠送体力
        public void giveFriendPower(UIListener.OnReceive funReceive, long friend_guid)
        {
            CSGiveFriendPower msg = (CSGiveFriendPower)PacketDistributed.CreatePacket(MessageID.CSGiveFriendPower);//new CSGiveFriendPower();
            msg.Friend_guid = friend_guid;
            send(funReceive, msg, true);
        }
        //接受体力
        public void getFriendPower(UIListener.OnReceive funReceive, long friend_guid)
        {
            CSGetFriendPower msg = (CSGetFriendPower)PacketDistributed.CreatePacket(MessageID.CSGetFriendPower);//new CSGetFriendPower();
            msg.Friend_guid = friend_guid;
            send(funReceive, msg, true);
        }
        //请求助战好友
        public void askRandomAssistanceList(UIListener.OnReceive funReceive)
        {
            CSGetRandomAssistanceList msg = (CSGetRandomAssistanceList)PacketDistributed.CreatePacket(MessageID.CSGetRandomAssistanceList);//new CSGetRandomAssistanceList();
            msg.User_guid = Obj_MyselfPlayer.GetMe().accountID;
            send(funReceive, msg, true);
        }
        //卖卡
        public void sellCard(UIListener.OnReceive funReceive, List<UserCardItem> selectedCards)
        {
            CSSellCard msg = (CSSellCard)PacketDistributed.CreatePacket(MessageID.CSSellCard);//new CSSellCard();
            //			msg.card_guidList.AddRange(keys);
            foreach (UserCardItem card in selectedCards)
            {
                msg.AddCard_guid(card.cardID);//.card_guidList.Add(key);
            }
            send(funReceive, msg, true);
        }
        //发送聊天内容
        public void sendChatContent(UIListener.OnReceive funReceive, string content, long friend_guid)
        {

        }
        //获取任务列表
        public void getTaskList(UIListener.OnReceive funReceive)
        {
            CSTaskList msg = (CSTaskList)PacketDistributed.CreatePacket(MessageID.CSTaskList);//new CSTaskList();
            msg.Guid = Obj_MyselfPlayer.GetMe().accountID;
            Debug.Log("------------- gettask: curAccountID:" + msg.Guid.ToString());
            send(funReceive, msg, true);
        }
        //发送战斗结束消息
        public void sendFinishBattle(UIListener.OnReceive funReceive)
        {
            CSClearBattleData msg = (CSClearBattleData)PacketDistributed.CreatePacket(MessageID.CSClearBattleData);
            send(funReceive, msg, true);
        }
        //完成任务
        public void sendFinishTask(UIListener.OnReceive funReceive, int templet_id)
        {
            CSFinishTask msg = (CSFinishTask)PacketDistributed.CreatePacket(MessageID.CSFinishTask);//new CSFinishTask();
            msg.Player_guid = Obj_MyselfPlayer.GetMe().accountID;
            msg.Templet_id = templet_id;
            send(funReceive, msg, true);
        }
        //获取商品信息
        public void getPurchase(UIListener.OnReceive funReceive)
        {
            CSProductList msg = (CSProductList)PacketDistributed.CreatePacket(MessageID.CSProductList);
            msg.Guid = Obj_MyselfPlayer.GetMe().accountID;
            send(funReceive, msg, true);
        }
        //更换队长队员
        public void sendChangeMember(UIListener.OnReceive funReceive, long[] templet_id_list)
        {
            CSChangeMember msg = (CSChangeMember)PacketDistributed.CreatePacket(MessageID.CSChangeMember);//new CSChangeMember();
            for (int i = 0; i < templet_id_list.Length; i++)
            {
                //				msg.cardguidListList.Add(templet_id_list[i]);
                msg.AddCardguidList(templet_id_list[i]);
            }
            send(funReceive, msg, true);
        }
        //获取邮件列表
        public void getMailList(UIListener.OnReceive funReceive)
        {
            CSMailList msg = (CSMailList)PacketDistributed.CreatePacket(MessageID.CSMailList);
            msg.Player_guid = Obj_MyselfPlayer.GetMe().accountID;
            send(funReceive, msg, true);
            Debug.Log("get mail list start");
        }
        //发送邮件
        public void sendMail(UIListener.OnReceive funReceive, long friend_guid, string content)
        {
            CSMailSend msg = (CSMailSend)PacketDistributed.CreatePacket(MessageID.CSMailSend);
            msg.Friend_id = friend_guid;
            msg.Content = content;
            send(funReceive, msg, true);
        }
        //收取系统邮件附件
        public void receiveGoods(UIListener.OnReceive funReceive, long mail_guid)
        {
            CSMailSystem msg = (CSMailSystem)PacketDistributed.CreatePacket(MessageID.CSMailSystem);
            msg.Mail_id = mail_guid;
            send(funReceive, msg, true);
        }
        //好友申请处理
        public void sendFriendApplyResult(UIListener.OnReceive funReceive, long mail_guid, int result)
        {
            CSMailFriend msg = (CSMailFriend)PacketDistributed.CreatePacket(MessageID.CSMailFriend);
            msg.Mail_id = mail_guid;
            msg.State = result;
            send(funReceive, msg, true);
        }
        //删除邮件
        public void deleteMail(UIListener.OnReceive funReceive, long mail_guid)
        {
            CSMailDelete msg = (CSMailDelete)PacketDistributed.CreatePacket(MessageID.CSMailDelete);
            msg.Mail_id = mail_guid;
            send(funReceive, msg, true);
        }
        //读邮件
        public void readMail(UIListener.OnReceive funReceive, long mail_guid)
        {
            CSMailRead msg = (CSMailRead)PacketDistributed.CreatePacket(MessageID.CSMailRead);
            msg.Mail_id = mail_guid;
            send(funReceive, msg, true);
        }
        //一键拒绝
        public void refuseAllApply(UIListener.OnReceive funReceive)
        {
            CSFriendMailDelete msg = (CSFriendMailDelete)PacketDistributed.CreatePacket(MessageID.CSFriendMailDelete);
            send(funReceive, msg, true);
        }
        //抽卡牌
        public void lotteryCard(UIListener.OnReceive funReceive, LotteryController.LOTTERY_TYPE type, int times)
        {
            CSGamble msg = (CSGamble)PacketDistributed.CreatePacket(MessageID.CSGamble);
            msg.SetType((int)type);
            msg.SetTimes(times);
            send(funReceive, msg, true);
        }
        //扩充好友 扩充背包 补充体力 购买PVP WML
        public void buySth(UIListener.OnReceive funReceive, int type)
        {
            CSShop msg = (CSShop)PacketDistributed.CreatePacket(MessageID.CSShop);
            msg.SetPlayer_guid((int)Obj_MyselfPlayer.GetMe().accountID);
            msg.SetType(type);
            //			Debug.Log(msg.Player_guid + " " + msg.Type);
            send(funReceive, msg, true);
        }

        //获得PVP对手玩家信息
        public void GetPvPPlayerInfoList(UIListener.OnReceive funReceive)
        {
            CSAskPVPList msg = (CSAskPVPList)PacketDistributed.CreatePacket(MessageID.CSAskPVPList);
            msg.SetHeroGuid((int)(Obj_MyselfPlayer.GetMe().accountID));
            send(funReceive, msg, true);
        }
        //王明磊 : 统计模块代码 -> Statistics
        //public void uploadStatistics(UIListener.OnReceive funReceive,Dictionary<int,int> info)
        //{
        //	CSStatistics msg = (CSStatistics)PacketDistributed.CreatePacket(MessageID.CSStatistics);
        //	msg.Mid = GameManager.getMacAddress();	
        //	msg.Count = info.Count;
        //	if (msg.Count <= 0)
        //		return ;
        //	xjgame.message.ButtonInfo buttonInfo;
        //	Statistics.StatisticsLogic.Instance().backupInfo();
        //	foreach (KeyValuePair<int,int> entry in info)
        //	{
        //		buttonInfo = new xjgame.message.ButtonInfo();
        //		buttonInfo.Id = entry.Key;
        //		buttonInfo.Value = entry.Value;
        //		msg.AddInfo(buttonInfo);
        //	}
        //	info.Clear();
        //	send(funReceive, msg, false);
        //}

        //完成引导
        //完成某一步均改为服务器记录和判断
        public void guideFinishStep(UIListener.OnReceive funReceive, GuideManager.GUIDE_STEP step)
        {
            Debug.Log("Send Finish Step:" + step);
            CSGuide msg = (CSGuide)PacketDistributed.CreatePacket(MessageID.CSGuide);
            msg.Player_guid = (int)Obj_MyselfPlayer.GetMe().accountID;
            msg.Finish_step = (int)step;
            bool isNeedLockScreen = true;
            send(funReceive, msg, isNeedLockScreen);
        }

        //更改名称
        public void sendChangeName(UIListener.OnReceive funReceive, string new_name)
        {
            CSChangeName msg = (CSChangeName)PacketDistributed.CreatePacket(MessageID.CSChangeName);
            msg.SetPlayer_guid((int)Obj_MyselfPlayer.GetMe().accountID);
            msg.Name = new_name;
            send(funReceive, msg, true);
        }
        //绑定--
        public void BindCyouAccount(UIListener.OnReceive funReceive, int type, string username, string password, string accountid)
        {
            CSBindAccount msg = (CSBindAccount)PacketDistributed.CreatePacket(MessageID.CSBindAccount);
			msg.Accountid = int.Parse(accountid);
            msg.Username = username;
            msg.Password = password;
            msg.Type = type;
#if UNITY_IPHONE	
			msg.DeviceSystem = DeviceHelper.GetSystemName();
			msg.DownloadType = DeviceHelper.GetChannelID();
#elif UNITY_ANDROID
			msg.DeviceSystem = DeviceHelper.GetSystemName();
			msg.DownloadType = DeviceHelper.GetChannelID();
#else
			msg.DeviceSystem = "Android 4.0";
			msg.DownloadType = "win32";
#endif
            send(funReceive, msg, true);
        }

        //pvpSop 玩家刷新信息
        public void FreshPvPShopInfo(UIListener.OnReceive funReceive)
        {
            CSAskScoreShopFresh msg = (CSAskScoreShopFresh)PacketDistributed.CreatePacket(MessageID.CSAskScoreShopFresh);
            msg.Guid = (int)Obj_MyselfPlayer.GetMe().accountID;
            send(funReceive, msg, true);
        }

        //pvpShop 选择兑换
        public void ChoosePvPItem(UIListener.OnReceive funReceive, int itemID)
        {
            CSPVPShop msg = (CSPVPShop)PacketDistributed.CreatePacket(MessageID.CSPVPShop);
            msg.Player_guid = (int)Obj_MyselfPlayer.GetMe().accountID;
            msg.Type = itemID;
            send(funReceive, msg, true);
        }

        //每24小时免费抽奖一次
        public void freeLotteryOnce(UIListener.OnReceive funReceive)
        {
            //			Obj_MyselfPlayer.GetMe().freeCD = 60 * 2;
            //			return ;
            CSRandomCardFree msg = (CSRandomCardFree)PacketDistributed.CreatePacket(MessageID.CSRandomCardFree);
            send(funReceive, msg, true);
        }

        public void VarifyActiveCode(UIListener.OnReceive funReceive, string strCode)
        {
            CSscode packet = (CSscode)PacketDistributed.CreatePacket(MessageID.CSscode);
            packet.Scode = strCode;
            send(funReceive, packet, true);
        }

        public void VarifyAppstoreOrder(UIListener.OnReceive funReceive, int goodID, string productId, string receipt, string tid)
        {
            CS20041 packet = (CS20041)PacketDistributed.CreatePacket(MessageID.CS20041);
            packet.GoodsId = goodID;
            packet.ProductId = productId;
            packet.Receipt = receipt;
            packet.TransactionId = tid;
            send(funReceive, packet, true);
        }

        public void VarifyIAppPayOrder(UIListener.OnReceive funReceive, int goodID, string productID, string order)
        {
            CS20039 packet = (CS20039)PacketDistributed.CreatePacket(MessageID.CS20039);
            packet.GoodsId = goodID;
            packet.OrderId = order;
            packet.WaresId = productID;
            send(funReceive, packet, true);
        }

        public void VarifyPPPayOrder(UIListener.OnReceive funReceive, int goodID, string productID, string orderID, int price, string userID, int zoneID)
        {
            CSPPVerifyCharge packet = (CSPPVerifyCharge)PacketDistributed.CreatePacket(MessageID.CSPPVerifyCharge);
            packet.GoodsId = goodID;
            packet.ProductId = productID;
            packet.OrderID = orderID;
            packet.Price = price;
            packet.UserID = userID;
            packet.ZoneID = zoneID;
            send(funReceive, packet, true);
        }
        public void RequestAppStoreProductList(UIListener.OnReceive funReceive)
        {
            send(funReceive, PacketDistributed.CreatePacket(MessageID.CS20040), true);
        }

        public void RequestIAppPayProductList(UIListener.OnReceive funReceive)
        {
            send(funReceive, PacketDistributed.CreatePacket(MessageID.CS20038), true);
        }

        public void RequestPPProductList(UIListener.OnReceive funReceive)
        {
            send(funReceive, PacketDistributed.CreatePacket(MessageID.CSPPProductList), true);
        }

        //刮刮乐请求
        public void RequestGuaGuaLe(UIListener.OnReceive funReceive)
        {
            CSGGL msg = (CSGGL)PacketDistributed.CreatePacket(MessageID.CSGGL);
            msg.Times = Obj_MyselfPlayer.GetMe().GGLTimes;
            msg.Player_guid = (int)Obj_MyselfPlayer.GetMe().accountID;
            send(funReceive, msg, true);
        }

        public void buyGold(UIListener.OnReceive funReceive, int channel) //channel = 0 -> 商铺点购买 1 -> 其他地方不足点购买 2 -> 战斗复活金币不足点购买
        {
            buyGoldFun = funReceive;
            int times = Obj_MyselfPlayer.GetMe().buyMoneyTimes;
            if (times < 0)
                Debug.LogError("Error BuyMoneyTimes < 0 ");
            if (times >= TableManager.GetGoldPrice().Count)
                times = TableManager.GetGoldPrice().Count - 1;
            Tab_GoldPrice gold = TableManager.GetGoldPriceByID(times + 1);
            buyGoldCost = gold.PowerPrice;
            string label = "";
            if (channel != 0)
                label = "您的金币不足,";
            label = label + "是否消耗" + gold.PowerPrice + "元宝\n购买" + gold.GoldValue + "金币?";
            if (channel != 2 || (channel == 2 && Obj_MyselfPlayer.GetMe().dollar > buyGoldCost))
            {
                BoxManager.showConfirmMessage(label, ClientConfigure.title); //WML MARK
                UIEventListener.Get(BoxManager.buttonYes).onClick += sendBuyGoldPB;
            }
            else
            {
                BoxManager.showConfirmMessage(label, ClientConfigure.title); //WML MARK
                UIEventListener.Get(BoxManager.buttonYes).onClick += BattleNoMoney;
            }

        }

        private void BattleNoMoney(GameObject button)
        {
            BoxManager.showMessage("您的元宝不足", ClientConfigure.title);
        }

        private void sendBuyGoldPB(GameObject button)
        {
            if (!checkDoller(buyGoldCost))
                return;
            CSBuyMoney packet = (CSBuyMoney)PacketDistributed.CreatePacket(MessageID.CSBuyMoney);
            packet.Player_guid = (int)Obj_MyselfPlayer.GetMe().accountID;
            packet.Times = Obj_MyselfPlayer.GetMe().buyMoneyTimes;
            send(buyGoldFun, packet, true);
        }

        public void buyPower(UIListener.OnReceive funReceive)
        {
            buyPowerFun = funReceive;
            int power = Obj_MyselfPlayer.GetMe().power;
            if (power < 0 || power > TableManager.GetIdexperienceByID(Obj_MyselfPlayer.GetMe().level).IDPhysicalValue)
                Debug.LogError("Power value Error !");
            if (power >= TableManager.GetIdexperienceByID(Obj_MyselfPlayer.GetMe().level).IDPhysicalValue)
            {
                //			BoxManager.showMessage("您的体力已满,无需购买");
                BoxManager.showMessageByID((int)MessageIdEnum.Msg2);
                return;
            }
            int buyPowerTimes = Obj_MyselfPlayer.GetMe().buyPowerTimes;
            if (buyPowerTimes >= TableManager.GetPowerPrice().Count)
            {
                BoxManager.showMessageByID((int)MessageIdEnum.Msg10);
                return;
            }
            int powerPrice = TableManager.GetPowerPriceByID(buyPowerTimes + 1).PowerPrice;

            string label = "您的体力不足\n是否使用" + powerPrice + "元宝恢复体力至满？";
            BoxManager.showConfirmMessage(label, ClientConfigure.title); //WML MARK
            UIEventListener.Get(BoxManager.buttonYes).onClick += sendBuyPowerPB;
        }

        private void sendBuyPowerPB(GameObject button)
        {
            if (!checkDoller(buyPowerCost))
                return;
            CSBuyPower packet = (CSBuyPower)PacketDistributed.CreatePacket(MessageID.CSBuyPower);
            packet.Player_guid = (int)Obj_MyselfPlayer.GetMe().accountID;
            packet.Times = Obj_MyselfPlayer.GetMe().buyPowerTimes;
            send(buyPowerFun, packet, true);
        }

        private bool checkDoller(int cost)
        {
            if (Obj_MyselfPlayer.GetMe().dollar < cost)
            {
                //				BoxManager.showMessage("当前元宝不足");
                BoxManager.showMessageByID((int)MessageIdEnum.Msg73);
                UIEventListener.Get(BoxManager.getYesButton()).onClick += GoRecharge;
                return false;
            }
            return true;
        }

        //风水相关PB
        //风水激活
        public void activeFengshui(UIListener.OnReceive funReceive, int id)
        {
            CSWuxingActivation packet = (CSWuxingActivation)PacketDistributed.CreatePacket(MessageID.CSWuxingActivation);
            packet.Id = id;
            send(funReceive, packet, true);
        }
        //风水升级
        public void updateFengshui(UIListener.OnReceive funReceive, int id)
        {
            CSWuxingLevelup packet = (CSWuxingLevelup)PacketDistributed.CreatePacket(MessageID.CSWuxingLevelup);
            packet.Id = id;
            send(funReceive, packet, true);
        }
        //风水重置
        public void resetFengshui(UIListener.OnReceive funReceive, int id)
        {
            CSWuxingReset packet = (CSWuxingReset)PacketDistributed.CreatePacket(MessageID.CSWuxingReset);
            packet.Id = id;
            send(funReceive, packet, true);
        }

        //月卡信息
        public void RequestMonthCardInfo(UIListener.OnReceive funReceive)
        {
            CSMonthCardInfo packet = (CSMonthCardInfo)PacketDistributed.CreatePacket(MessageID.CSMonthCardInfo);
            send(funReceive, packet, true);
        }

        //领取月卡奖励
        public void RequestGetMonthCardDollar(UIListener.OnReceive funReceive)
        {
            CSMonthCardGetDollar packet = (CSMonthCardGetDollar)PacketDistributed.CreatePacket(MessageID.CSMonthCardGetDollar);
            send(funReceive, packet, true);
        }

        //学习技能
        public void RequestLearnSkill(UIListener.OnReceive funReceive)
        {
            CSStudySkill packet = (CSStudySkill)PacketDistributed.CreatePacket(MessageID.CSStudySkill);
            packet.Hero_cardguid = Obj_MyselfPlayer.GetMe().learnMainHeroItem.cardID;
            packet.Swallowed_cardguid = Obj_MyselfPlayer.GetMe().learnChildHeroItem.cardID;
            send(funReceive, packet, true);
        }

        //升级技能
        public void RequestUpdateSkill(UIListener.OnReceive funReceive)
        {
            CSStudySkillUpdate packet = (CSStudySkillUpdate)PacketDistributed.CreatePacket(MessageID.CSStudySkillUpdate);
            packet.Hero_cardguid = Obj_MyselfPlayer.GetMe().updateMainHeroItem.cardID;
            foreach (UserCardItem card in Obj_MyselfPlayer.GetMe().updateChildHeroItems)
            {
                packet.AddSwallowed_cardguid(card.cardID);
            }
            send(funReceive, packet, true);
        }

        //世界boss
        public void RequestWorldBossInfo(UIListener.OnReceive funReceive)
        {
            CSAskWorldBossInfo packet = (CSAskWorldBossInfo)PacketDistributed.CreatePacket(MessageID.CSAskWorldBossInfo);
            packet.Guid = Obj_MyselfPlayer.GetMe().accountID;
            send(funReceive, packet, true);
        }

        //祝福
        public void RequestWorldBossBuff(UIListener.OnReceive funReceive)
        {
            CSWorldBossAddZhufu packet = (CSWorldBossAddZhufu)PacketDistributed.CreatePacket(MessageID.CSWorldBossAddZhufu);
            send(funReceive, packet, true);
        }

        //复活
        public void RequestWorldBossResurgence(UIListener.OnReceive funReceive)
        {
            CSWorldBossResurgence packet = (CSWorldBossResurgence)PacketDistributed.CreatePacket(MessageID.CSWorldBossResurgence);
            send(funReceive, packet, true);
        }

        //周排行
        public void RequeseWorldBossWeekRank(UIListener.OnReceive funReceive)
        {
            CSWorldBossWeekRank packet = (CSWorldBossWeekRank)PacketDistributed.CreatePacket(MessageID.CSWorldBossWeekRank);
            send(funReceive, packet, true);
        }

        //获取世界boss周排名奖励
        public void RequestGetWorldBossReward(UIListener.OnReceive funReceive)
        {
            CSWorldBossWeekReward packet = (CSWorldBossWeekReward)PacketDistributed.CreatePacket(MessageID.CSWorldBossWeekReward);
            send(funReceive, packet, true);
        }

        public void SendBGZPB(UIListener.OnReceive funReceive)
        {
            CSBGZ packet = (CSBGZ)PacketDistributed.CreatePacket(MessageID.CSBGZ);
            packet.Player_guid = (int)Obj_MyselfPlayer.GetMe().accountID;
            packet.Times = Obj_MyselfPlayer.GetMe().BGZTimes;

            send(funReceive, packet, true);
        }

        public void GoRecharge(GameObject button)
        {
            //王明磊 : 统计模块代码 -> Statistics
            PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn58).ToString());
            //		BoxManager.showMessage("功能暂未开放");  
            //BoxManager.showMessageByID((int)MessageIdEnum.Msg18);
            MainUILogic mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
            mainLogic.OnPurchaseWindow();
        }
		
		public void SendClearQxzbPvPCD(UIListener.OnReceive funReceive)
		{
			CSQxzbPVPClearCD packet = (CSQxzbPVPClearCD)PacketDistributed.CreatePacket(MessageID.CSQxzbPVPClearCD);
            packet.PlayerId = Obj_MyselfPlayer.GetMe().accountID;

            send(funReceive, packet, true);
		}
		
		public void GetQxzbPvPPlayerInfoList(UIListener.OnReceive funReceive)
		{
			CSQxzbPVPDataAsk packet = (CSQxzbPVPDataAsk)PacketDistributed.CreatePacket(MessageID.CSQxzbPVPDataAsk);
            packet.PlayerId = Obj_MyselfPlayer.GetMe().accountID;

            send(funReceive, packet, true);
		}
		
		
		public void GetQxzbReward(UIListener.OnReceive funReceive)
		{
			CSQxzbGetReward packet = (CSQxzbGetReward)PacketDistributed.CreatePacket(MessageID.CSQxzbGetReward);
            packet.PlayerId = (int)Obj_MyselfPlayer.GetMe().accountID;
			packet.Star = Obj_MyselfPlayer.GetMe().nGetRewardCurStar;
            send(funReceive, packet, true);
		}
		
		public void QxzbBattle(UIListener.OnReceive funReceive)
		{
			Obj_MyselfPlayer.GetMe().SetBattleBeforeDate();
			CSQxzbBattle msg = (CSQxzbBattle)PacketDistributed.CreatePacket(MessageID.CSQxzbBattle);
            msg.PlayerId =(int)Obj_MyselfPlayer.GetMe().accountID;
			msg.DefierId = (int)Obj_MyselfPlayer.GetMe().enemyGUID;
			msg.Star = Obj_MyselfPlayer.GetMe().curPvPStar;
			msg.LeaderID = Obj_MyselfPlayer.GetMe().curPvPLearder;
			Obj_MyselfPlayer.GetMe().battleType = BattleType.QxzbPvP;
			
			for (int i = 0; i < Obj_MyselfPlayer.GetMe().PvPBattleArray.Length; i++)
			{
				long cardID = Obj_MyselfPlayer.GetMe().PvPBattleArray[i];
				if (cardID <= 0)
					continue;
				UserCardItem card = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(cardID);
				if (card == null)
					continue;
				xjgame.message.BattleCard battleCard = new xjgame.message.BattleCard();
                battleCard.Cardguid = (ulong)card.cardID;
                battleCard.Isfriend = 0;
                battleCard.CardID = card.templateID;
				
                if(Obj_MyselfPlayer.GetMe().curPvPLearder == cardID)
				{
					battleCard.Place_idx = i + Obj_MyselfPlayer.GetMe().curPvPStar*10 ;
				}
				else
				{
					battleCard.Place_idx = i + Obj_MyselfPlayer.GetMe().curPvPStar*10 + 100;
				}
				
				battleCard.State = 0;
				msg.AddInfo(battleCard);
			}
            send(funReceive, msg, true);
		}
		
		public void AskActivity(UIListener.OnReceive funReceive)
		{
			CSAskActivity msg = (CSAskActivity)PacketDistributed.CreatePacket(MessageID.CSAskActivity);
			send(funReceive,msg,true);
		}
		
		public void AskChangeCardList(UIListener.OnReceive funReceive)
		{
			CSAskChangeCardList packet = (CSAskChangeCardList)PacketDistributed.CreatePacket(MessageID.CSAskChangeCardList);
            packet.PlayerGuid = Obj_MyselfPlayer.GetMe().accountID;
            send(funReceive, packet, true);
		}
		
		public void RequestChangeCard(UIListener.OnReceive funReceive, int infoID, bool hasCard, int[,] type)
		{
//				required int32 changeCardInfoID = 1;//本条换卡ID
//	required int32 HaveCardflag = 2;//换卡条件是否有卡牌 0 = 没有 1 =有
//	repeated int64 info_1 = 3; // 条件1
//	repeated int64 info_2 = 4; // 条件2
//	repeated int64 info_3 = 5; // 条件3
			CSChangeCardConfirm packet = (CSChangeCardConfirm)PacketDistributed.CreatePacket(MessageID.CSChangeCardConfirm);
           	packet.ChangeCardInfoID = infoID;
			packet.HaveCardflag = hasCard ? 1 : 0;
			int i = 0;
			while (type[0,i] > 0)
			{
				packet.AddInfo_1(type[0,i]);
				i++;
			}
			i = 0;
			while (type[1,i] > 0)
			{
				packet.AddInfo_2(type[1,i]);
				i++;
			}
			i = 0;
			while (type[2,i] > 0)
			{
				packet.AddInfo_3(type[2,i]);
				i++;
			}
            send(funReceive, packet, true);
		}
		
#if UNITY_ANDROID
        public void RequestCYouPayProductList(UIListener.OnReceive funReceive, bool bNeedMsgBox)
        {
			if(AndroidConfig.is360Channel()){
				CSCommonProductList msg = (CSCommonProductList)PacketDistributed.CreatePacket(MessageID.CSCommonProductList);
				msg.Channelid = 3005;//360
				send(funReceive,msg,bNeedMsgBox);
			}
			else if (AndroidConfig.isXiaoMiChannel()){
				CSCommonProductList msg = (CSCommonProductList)PacketDistributed.CreatePacket(MessageID.CSCommonProductList);
				msg.Channelid = 3004;
				send(funReceive,msg, bNeedMsgBox);
			}
			else if (AndroidConfig.is91Channel()){
				CSCommonProductList msg = (CSCommonProductList)PacketDistributed.CreatePacket(MessageID.CSCommonProductList);
				msg.Channelid = 3006;
				send(funReceive,msg, bNeedMsgBox);
			}
			else if (AndroidConfig.isDuokuChannel()){
				CSCommonProductList msg = (CSCommonProductList)PacketDistributed.CreatePacket(MessageID.CSCommonProductList);
				msg.Channelid = 3007;
				send(funReceive,msg, bNeedMsgBox);
			}
			else if (AndroidConfig.isWanDouJiaChannel()){
				CSCommonProductList msg = (CSCommonProductList)PacketDistributed.CreatePacket(MessageID.CSCommonProductList);
				msg.Channelid = 3008;//豌豆荚的是3008//
				send(funReceive,msg, bNeedMsgBox);
			}
			//整合UC渠道代码 lihao_yd 2013-11-25
			else if (AndroidConfig.isUCChannel()){
				CSCommonProductList msg = (CSCommonProductList)PacketDistributed.CreatePacket(MessageID.CSCommonProductList);
				msg.Channelid = 3002;
				send(funReceive,msg, bNeedMsgBox);
			}
			
			else if (AndroidConfig.isOPPOChannel()){
				CSCommonProductList msg = (CSCommonProductList)PacketDistributed.CreatePacket(MessageID.CSCommonProductList);
				msg.Channelid = 3011;
				send(funReceive,msg, bNeedMsgBox);
			}
			else if (AndroidConfig.isAnZhiChannel()){
				CSCommonProductList msg = (CSCommonProductList)PacketDistributed.CreatePacket(MessageID.CSCommonProductList);
				msg.Channelid = 3010;
				send(funReceive,msg, bNeedMsgBox);
			}
			else if (AndroidConfig.isSogouChannel()){
				CSCommonProductList msg = (CSCommonProductList)PacketDistributed.CreatePacket(MessageID.CSCommonProductList);
				msg.Channelid = 3013;
				send(funReceive,msg, bNeedMsgBox);
			}else if (AndroidConfig.isHuaweiChannel()){
				CSCommonProductList msg = (CSCommonProductList)PacketDistributed.CreatePacket(MessageID.CSCommonProductList);
				msg.Channelid = 3014;
				send(funReceive,msg, bNeedMsgBox);
			}
			
			else{
				//畅游渠道
				CSCommonProductList msg = (CSCommonProductList)PacketDistributed.CreatePacket(MessageID.CSCommonProductList);
				msg.Channelid = 6001;
				send(funReceive,msg, bNeedMsgBox);
				//send(funReceive, PacketDistributed.CreatePacket(MessageID.CSCYouProductList), true);
			}
        }
		
		public void GooglePayVerifyCYouPurchase(UIListener.OnReceive funReceive, string  orderId,
			string signature, string purchaseData,bool needBlockMsg){
			CSGooglePayVerifyCharge msg = (CSGooglePayVerifyCharge) PacketDistributed.CreatePacket(MessageID.CSGooglePayVerifyCharge);
			msg.ChannelId = 6001;
			msg.OrderId = orderId;
			msg.Signature = signature;
			msg.PurchaseData = purchaseData;
			Debug.Log("msg.ChannelId:"+msg.ChannelId
						+"msg.OrderId:"+msg.OrderId
						+"msg.Signature"+msg.Signature
						+"msg.PurchaseData"+msg.PurchaseData);
			send(funReceive, msg, needBlockMsg);
		}
		
        //购买验证//
        public void VerifyCYouPurchase(UIListener.OnReceive funReceive, int goodID, string productID, string order, string price, int payway, bool needBlockMsg)
        {
			if(AndroidConfig.isUCChannel()||AndroidConfig.isThirdSDKPlatform()){
				CSCYouPayVerifyCharge msg = (CSCYouPayVerifyCharge)PacketDistributed.CreatePacket(MessageID.CSCYouPayVerifyCharge);
				msg.OrderId = order;
				msg.GoodsId = goodID;
				msg.GoodPrice = (int)(Convert.ToDouble(price));
				msg.ProductId = productID;
				int channel = 0;
				if(AndroidConfig.isUCChannel()){
					channel = 3002;
				}else if(AndroidConfig.isXiaoMiChannel()){
					channel = 3004;
				}else if(AndroidConfig.is360Channel()){
					channel = 3005;
				}else if(AndroidConfig.is91Channel()){
					channel = 3006;
				}else if(AndroidConfig.isDuokuChannel()){
					channel = 3007;
				}
				else if(AndroidConfig.isWanDouJiaChannel()){
					channel = 3008;
				}else if(AndroidConfig.isAnZhiChannel()){
					channel = 3010;
				}else if(AndroidConfig.isOPPOChannel()){
					channel = 3011;
				}else if(AndroidConfig.isSogouChannel()){
					channel = 3013;
				}else if(AndroidConfig.isHuaweiChannel()){
					channel = 3014;
				}
				msg.ChannelId = channel;
				msg.PayWay = "0";
				send(funReceive, msg, needBlockMsg);
			}else{
				CSCYouPayVerifyCharge msg = (CSCYouPayVerifyCharge)PacketDistributed.CreatePacket(MessageID.CSCYouPayVerifyCharge);
				msg.GoodsId = goodID;
				msg.ProductId = productID;
				msg.OrderId = order;
				msg.GoodPrice = (int)(Convert.ToDouble(price));
				msg.PayWay = payway.ToString();
				msg.ChannelId = 3001;
				send(funReceive, msg, needBlockMsg);
            }
        }
		
		// 只用于第三方
		private bool channelLogin(UIListener.OnReceive funReceive, string account_id,int type)
        {
			bool isReturn = false;
			if(AndroidConfig.isUCChannel()||AndroidConfig.isThirdSDKPlatform())
			{
				CSSDKLoginThirdPlatform msguc = (CSSDKLoginThirdPlatform)PacketDistributed.CreatePacket(MessageID.CSSDKLoginThirdPlatform);
				//CSLoginThirdPlatform msguc = (CSLoginThirdPlatform)PacketDistributed.CreatePacket(MessageID.CSLoginThirdPlatform);
				msguc.Mid = GameManager.getMacAddress(); 
				msguc.Version =DeviceHelper.GetVersionNumber();// ClientConfigure.GetClientVersion();
				Debug.Log("----send Version ----"+msguc.Version);
				if(AndroidConfig.isUCChannel()){
					msguc.Platform = AndroidConfig.PLATFORM_UC;// 第三方平台类型 5是UC//
				}else if(AndroidConfig.is360Channel()){
					msguc.Platform = 2;// 第三方平台类型 
					Debug.Log("----channelLogin1 ----"+msguc.Platform);
				}else if(AndroidConfig.is91Channel()){
					msguc.Platform = 1;// 第三方平台类型 
					Debug.Log("----channelLogin1 ----"+msguc.Platform);
				}else if(AndroidConfig.isXiaoMiChannel()){
					msguc.Platform = 3;// 第三方平台类型 
					Debug.Log("----channelLogin1 ----"+msguc.Platform);
				}else if(AndroidConfig.isDuokuChannel()){
					msguc.Platform = 4;// 第三方平台类型 
					Debug.Log("----channelLogin1 ----"+msguc.Platform);
				}else if(AndroidConfig.isWanDouJiaChannel()){
					msguc.Platform = 6;// 第三方平台类型   ======豌豆荚为6,5预留给UC=====//
					Debug.Log("----lee-channelLogin1 ----"+msguc.Platform);
				}else if(AndroidConfig.isAnZhiChannel()){
					msguc.Platform = 7;// 第三方平台类型   ======豌豆荚为6,5预留给UC=====//
					Debug.Log("----lee-channelLogin1 ----"+msguc.Platform);
				}else if(AndroidConfig.isOPPOChannel()){
					msguc.Platform = 8;// 第三方平台类型   ======豌豆荚为6,5预留给UC=====//
					Debug.Log("----lee-channelLogin1 ----"+msguc.Platform);
				}else if(AndroidConfig.isHuaweiChannel()){
					msguc.Platform = 9;// 第三方平台类型   ======sogou=====//
					Debug.Log("----lee-channelLogin1 ----"+msguc.Platform);
				}else if(AndroidConfig.isSogouChannel()){
					msguc.Platform = 10;// 第三方平台类型   ======sogou=====//
					Debug.Log("----lee-channelLogin1 ----"+msguc.Platform);
				}
				
				msguc.Sid = account_id;// UC登陆会话id//
				
				Debug.Log("----channelLogin2 ----"+msguc.Sid);
				if(!string.IsNullOrEmpty(account_id)) 
					msguc.Sid = account_id;
				else if(string.IsNullOrEmpty(msguc.Sid))
					msguc.Sid = "";

                if(AndroidConfig.is360Channel() == true)
                {
                    msguc.Sid = PlayerPrefs.GetString("LastAccountId");
					Debug.Log("----msguc.Sid ----"+msguc.Sid);
                }

				msguc.Area  = DeviceHelper.GetUserLocale(); //	登录地区名称//
				msguc.Country = DeviceHelper.GetUserContry();//	登录国家名称//
				msguc.Device = WebMediator.GetModel();//	登录设备名称//
				msguc.DeviceSystem = DeviceHelper.GetSystemName();//	登录系统名称//
				msguc.NetworkType = DeviceHelper.GetNetworkState();//	联网类型名称//
				msguc.Operator = DeviceHelper.GetNetWorkProvider();//	运营商名称//
				msguc.LoginType = type; //登陆还是终端恢复 0 ：中断恢复 1：正常登陆
				// 先去掉
				msguc.DownloadType = AndroidConfig.GetChannelID();
				msguc.JsonData = account_id;
				xjgame.message.ButtonInfo buttonInfouc;
				for (int btnNo = 1; btnNo <= 58; btnNo++)
				{
					string key = "Btn" + btnNo.ToString();
					if (PlayerPrefs.HasKey(key))
					{
						buttonInfouc = new xjgame.message.ButtonInfo();
						buttonInfouc.Id = btnNo;
						buttonInfouc.Value = PlayerPrefs.GetInt(key);
						msguc.AddInfo(buttonInfouc);
					}
				}
				if (PlayerPrefs.HasKey("LastAccountId"))
					msguc.LastAccountId = PlayerPrefs.GetString("LastAccountId");
				Debug.Log("----channelLogin3 ----");
				send(funReceive,msguc,true);
				isReturn = true;
				Debug.Log("----channelLogin4 ----");
			}
			//整合UC渠道代码 lihao_yd 2013-11-25
			/*  
			else if(AndroidConfig.isUCChannel()){
				CSLoginThirdPlatform msguc = (CSLoginThirdPlatform)PacketDistributed.CreatePacket(MessageID.CSLoginThirdPlatform);
				msguc.Mid = GameManager.getMacAddress(); 
				msguc.Version =DeviceHelper.GetVersionNumber();// ClientConfigure.GetClientVersion();
				Debug.Log("----send Version ----"+msguc.Version);
				if(AndroidConfig.GetChannelID().Equals("android_uc")){
					msguc.Platform = AndroidConfig.PLATFORM_UC;// 第三方平台类型 0 是UC//
				}else if(AndroidConfig.is360Channel()){
					msguc.Platform = 2;// 第三方平台类型 0 是UC  //
					Debug.Log("----channelLogin1 ----"+msguc.Platform);
				}else if(AndroidConfig.is91Channel()){
					msguc.Platform = 1;// 第三方平台类型 0 是UC  //
					Debug.Log("----channelLogin1 ----"+msguc.Platform);
				}else if(AndroidConfig.isXiaoMiChannel()){
					msguc.Platform = 3;// 第三方平台类型 0 是UC  //
					Debug.Log("----channelLogin1 ----"+msguc.Platform);
				}else if(AndroidConfig.isDuokuChannel()){
					msguc.Platform = 4;// 第三方平台类型 0 是UC  //
					Debug.Log("----channelLogin1 ----"+msguc.Platform);
				}
				
				msguc.Sid = account_id;// UC登陆会话id//
				
				Debug.Log("----channelLogin2 ----"+msguc.Sid);
				if(!string.IsNullOrEmpty(account_id)) 
					msguc.Sid = account_id;
				else if(string.IsNullOrEmpty(msguc.Sid))
					msguc.Sid = "";
				msguc.Area  = DeviceHelper.GetUserLocale(); //	登录地区名称//
				msguc.Country = DeviceHelper.GetUserContry();//	登录国家名称//
				msguc.Device = WebMediator.GetModel();//	登录设备名称//
				msguc.DeviceSystem = DeviceHelper.GetSystemName();//	登录系统名称//
				msguc.NetworkType = DeviceHelper.GetNetworkState();//	联网类型名称//
				msguc.Operator = DeviceHelper.GetNetWorkProvider();//	运营商名称//
				msguc.Serverid = WebMediator.GetServerID();	//服务器号
				//msguc.Info = 16; //统计 - Button 
				//msguc.LastAccountId = 17; //统计信息所属的用户ID
				msguc.LoginType = 1; //登陆还是终端恢复 0 ：中断恢复 1：正常登陆
				//msguc.DownloadType = AndroidConfig.GetChannelID();
				//msguc.JsonData = account_id;
				xjgame.message.ButtonInfo buttonInfouc;
				for (int btnNo = 1; btnNo <= 58; btnNo++)
				{
					string key = "Btn" + btnNo.ToString();
					if (PlayerPrefs.HasKey(key))
					{
						buttonInfouc = new xjgame.message.ButtonInfo();
						buttonInfouc.Id = btnNo;
						buttonInfouc.Value = PlayerPrefs.GetInt(key);
						msguc.AddInfo(buttonInfouc);
					}
				}
				if (PlayerPrefs.HasKey("LastAccountId"))
					msguc.LastAccountId = PlayerPrefs.GetString("LastAccountId");
				send(funReceive,msguc,true);
				isReturn = true;
			}
			*/
			return isReturn;
		}
#endif
		
		//facebook 点赞等
		public void RequestTaskOver(UIListener.OnReceive funReceive ,int taskid){
			CSTaskOver msg = (CSTaskOver)PacketDistributed.CreatePacket(MessageID.CSTaskOver);
			msg.TaskId = taskid;
			send(funReceive, msg, true);
		}
		
		//活动送礼
		public void RequestHuodongGift(UIListener.OnReceive funReceive){
			CSYunyingHuodong msg = (CSYunyingHuodong)PacketDistributed.CreatePacket(MessageID.CSYunyingHuodong);
			msg.AccountId = Obj_MyselfPlayer.GetMe().accountID;
			send(funReceive, msg, true);
		}		
    }
	
	
	
}


