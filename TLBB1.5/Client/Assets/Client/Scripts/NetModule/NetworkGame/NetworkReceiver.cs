using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xjgame.message;
using Games.CharacterLogic;
using GCGame.Table;
using Module.Log;
using UnityEngine;
namespace card.net
{
    class NetworkReceiver : HTTPPacketHandler
    {
        public bool handle(MessageID opcode, byte[] data)
        {
            //			Debug.Log("Receive message:"+ opcode);
            //			MemoryStream msData = new MemoryStream(data);

            bool handled = true;
            switch (opcode)
            {
                case MessageID.SCErrorMsg:
                    SCErrorMsg errorMsg = new SCErrorMsg();//Serializer.Deserialize<SCErrorMsg>(msData);
                    errorMsg.ParseFrom(data);
                    //cb: a example PBMessage.SCErrorMsg.ErrorType.SELL_CARD
                    Debug.LogError("Error Message:" + (ErrorType)errorMsg.Type);
                    //				NGUIDebug.Log("Error Message:"+(ErrorType)errorMsg.Type);
                    //if((ErrorType)errorMsg.Type != ErrorType.CARD_COMBINE ||
                    //	(ErrorType)errorMsg.Type != ErrorType.CARD_EVOLVE ||
                    //	 (ErrorType)errorMsg.Type != ErrorType.CARD_STRENGTHEN)
                    if ((ErrorType)errorMsg.Type != ErrorType.CARD_COMBINE)      //卡牌升级特殊处理//
                    {
                        UIListener.Instance().OnReceiveMsg(false);
                        //解除发送锁, 发送成功,接受到错误信息
                        NetworkSender.Instance().sendFinish(true);
                    }
                    else
                    {
                    }
                    //				if(UIListener.Instance().isBlock)
                    //				{
                    //					BoxManager.removeMessage();
                    //					UIListener.Instance().isBlock = false;
                    //				}
#if UNITY_ANDROID
				if(((ErrorType)errorMsg.Type) == ErrorType.BAG_FULL){
					if(GameManager.Instance.sceneName.Equals(Utils.UI_NAME_Battle)){
						// 战斗场景不发送 //
						return handled;
					}
				}
				if(((ErrorType)errorMsg.Type) == ErrorType.LOGIN_VERSION_WRONG){
					if(AndroidConfig.versionWrong(errorMsg.Version)){
						return handled;
					}
				}
#endif
                    BoxManager.showErrorMessage(errorMsg.Type);
					if((ErrorType)errorMsg.Type == ErrorType.POWER_SEND_ALREADY)
						UIEventListener.Get(BoxManager.buttonYes).onClick+= On_PowerSendAlready_ErrorMsgButtonClick;
                    Obj_MyselfPlayer.GetMe().battel_sign = 1;
                    return handled;
                case MessageID.SCLoginRet:
                    //登录返回消息
                    SCLoginRet loginRet = new SCLoginRet();//Serializer.Deserialize<SCLoginRet>(msData);
                    loginRet.ParseFrom(data);

                    //王明磊 - 保存本次登录的AccountID 表示本次登录的统计信息所属
                    if (loginRet.AccountId != null && loginRet.AccountId.ToString() != "")
                    {
                        PlayerPrefs.SetString("LastAccountId", loginRet.AccountId.ToString());
                    }
                    if (loginRet.HasUid)
                    {
                        PlayerPrefs.SetString("PLAYER_UID", loginRet.Uid);//记录用户uid
                        Debug.LogWarning("PLAYER_UID	|	" + loginRet.Uid);
                        Obj_MyselfPlayer.GetMe().uid = loginRet.Uid;
                    }
                    Obj_MyselfPlayer.GetMe().giftison = loginRet.Giftison;
                    //如果是畅游账号登录或注册--
                    if (loginRet.HasType)
                    {
                        Obj_MyselfPlayer.GetMe().SetCyouLoginData(loginRet);
                    }
                    else
                    {
                        //检查登陆状态
                        //1.新玩家 2.老玩家
                        Debug.Log("登录成功");
                        //设置用户已登陆标志
                        GameManager.userLoginDone();
                        Obj_MyselfPlayer.GetMe().SetLoginData(loginRet);
                        //打印返回信息
                        Debug.Log("state:" + loginRet.State);
                        Debug.Log("AccountId:" + loginRet.AccountId);
                        loginRet = null;
                    }
                    //王明磊 - 清理过期统计数据
                    for (int btnNo = 1; btnNo <= 58; btnNo++)
                    {
                        string key = "Btn" + btnNo.ToString();
                        if (PlayerPrefs.HasKey(key))
                        {
                            PlayerPrefs.DeleteKey(key);
                        }
                    }
                    //PlayerPrefs.DeleteKey("LastAccountId");
                    PlayerPrefs.SetInt("Btn1", PlayerPrefs.GetInt("Btn-1"));
                    PlayerPrefs.SetInt("Btn2", PlayerPrefs.GetInt("Btn-2"));
                    break;
#if UNITY_ANDROID
				// UC 登录验证后
			case MessageID.SCLoginThirdPlatformRet:
				SCLogin(data);
				break;
			case MessageID.SCSDKLoginThirdPlatformRet:
				SCLogin(data);
				break;
#endif
                case MessageID.SCAskUserData:
                    SCAskUserData msgData = new SCAskUserData();//Serializer.Deserialize<SCAskUserData>(msData);
                    msgData.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setUserInfo(msgData);
                    msgData = null;
                    break;
                case MessageID.SCPVPBattleData:
                    Debug.LogWarning("SCPVPBattleData");
                    SCPVPBattleData pvpData = new SCPVPBattleData();
                    pvpData.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetPVPBattleData(pvpData);
                    Obj_MyselfPlayer.GetMe().battleType = Games.Battle.BattleType.PVP;
                    Obj_MyselfPlayer.GetMe().battel_sign = 2;  //进入加载场景
                    break;

                case MessageID.SCBattleData:
                    Debug.LogWarning("SCBattleData");
                    SCBattleData battleData = new SCBattleData();//Serializer.Deserialize<SCBattleData>(msData);
                    battleData.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetBattleData(battleData);
					Obj_MyselfPlayer.GetMe().battleType = Games.Battle.BattleType.PVE;
                    Obj_MyselfPlayer.GetMe().battel_sign = 2;  //进入加载场景
                    Debug.Log("battle data");
                    break;
				case MessageID.SCAskWorldBossBattle:
					Debug.Log("SCAskWorldBossBattle");
					SCAskWorldBossBattle scawbb = new SCAskWorldBossBattle();
					scawbb.ParseFrom(data);
					Obj_MyselfPlayer.GetMe().SetBattleData(scawbb);
					Obj_MyselfPlayer.GetMe().battleType = Games.Battle.BattleType.WORLD_BOSS;
                    Obj_MyselfPlayer.GetMe().battel_sign = 2;  //进入加载场景
					Debug.Log("world boss battle data");
					break;
				case MessageID.SCPaiTaBattleData:
					Debug.LogWarning("SCPaiTaBattleData");
					SCPaiTaBattleData pataBattleData = new SCPaiTaBattleData();
					pataBattleData.ParseFrom(data);
					Obj_MyselfPlayer.GetMe().SetBattleData(pataBattleData);
					Obj_MyselfPlayer.GetMe().battleType = Games.Battle.BattleType.CHONG_LOU;
                    Obj_MyselfPlayer.GetMe().battel_sign = 2;  //进入加载场景
					//Obj_MyselfPlayer.GetMe().pataNum = pataBattleData.Num;
					Debug.Log("chonglou battle data");
					break;
                case MessageID.SCClearBattleData:
                    SCClearBattleData clearData = new SCClearBattleData();
                    clearData.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetClearBattleData(clearData);
                    break;
				case MessageID.SCClearPaiTaBattleData:
					SCClearPaiTaBattleData clearPataData = new SCClearPaiTaBattleData();
					clearPataData.ParseFrom(data);
					Obj_MyselfPlayer.GetMe().SetClearPataBattleData(clearPataData);
					break;
                case MessageID.SCCardCombiningRet:
                    SCCardCombiningRet combineData = new SCCardCombiningRet();
                    combineData.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetCardCombingData(combineData);
                    break;
                case MessageID.SCCardEvolveRet:
                    SCCardEvolveRet evolveData = new SCCardEvolveRet();
                    evolveData.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetCardEvolutionData(evolveData);
                    break;
                case MessageID.SCCardStrengthenRet:
                    SCCardStrengthenRet sthData = new SCCardStrengthenRet();
                    sthData.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetCardStrengthenData(sthData);
                    break;
                case MessageID.SCFriendsList:
                    SCFriendsList friendList = new SCFriendsList();//Serializer.Deserialize<SCFriendsList>(msData);
                    friendList.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setFriendsData(friendList, friendList.Friends_num, friendList.Friends_max);
                    friendList = null;
                    break;
                case MessageID.SCSearchFriend:
                    SCSearchFriend sf = new SCSearchFriend();//Serializer.Deserialize<SCSearchFriend>(msData);
                    sf.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setSearchResult(sf);
                    break;
                case MessageID.SCGiveFriendPower:
                    SCGiveFriendPower gsf = new SCGiveFriendPower();//Serializer.Deserialize<SCGiveFriendPower>(msData);
                    gsf.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().giveFriendPower(gsf);
				/*
					if(gsf.State == 1)
						BoxManager.showMessageByID((int)MessageIdEnum.Msg190);
					else if( gsf.State == 2)
						BoxManager.showMessageByID((int)MessageIdEnum.Msg17);
						*/
                    break;
                case MessageID.SCGetFriendPower:
                    SCGetFriendPower gfp = new SCGetFriendPower();//Serializer.Deserialize<SCGetFriendPower>(msData);
                    gfp.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().getFriendPower(gfp);
                    break;
                case MessageID.SCADDFriend:
                    SCADDFriend addf = new SCADDFriend();//Serializer.Deserialize<SCADDFriend>(msData);
                    addf.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().addFriend(addf);
                    break;
                case MessageID.SCDeleteFriend:
                    SCDeleteFriend df = new SCDeleteFriend();//Serializer.Deserialize<SCDeleteFriend>(msData);
                    df.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().deleteFriend(df);
                    break;
                case MessageID.SCGetRandomAssistanceList:
                    SCGetRandomAssistanceList gral = new SCGetRandomAssistanceList();//Serializer.Deserialize<SCGetRandomAssistanceList>(msData);
                    gral.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setAssistanceList(gral);
                    break;
                case MessageID.SCSellCard:
                    SCSellCard sellc = new SCSellCard();//Serializer.Deserialize<SCSellCard>(msData);
                    sellc.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setSellCard(sellc);
                    break;
                case MessageID.SCTaskList:
                    SCTaskList tasklist = new SCTaskList();//Serializer.Deserialize<SCTaskList>(msData);
                    tasklist.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setTaskData(tasklist);
                    break;
                case MessageID.SCFinishTask:
                    SCFinishTask finishTask = new SCFinishTask();//Serializer.Deserialize<SCFinishTask>(msData);
                    finishTask.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setFinishTask(finishTask);
                    break;
                case MessageID.SCChangeMember:
                    SCChangeMember changeMember = new SCChangeMember();//Serializer.Deserialize<SCChangeMember>(msData);
                    changeMember.ParseFrom(data);
                    if (changeMember.HasBagData)
                    {
                        Obj_MyselfPlayer.GetMe().SetUserBagData(changeMember.BagData);
                    }
                    break;
                case MessageID.SCMailList:
                    SCMailList mailList = new SCMailList();
                    mailList.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setMailList(mailList);
                    break;
                case MessageID.SCMailSystem:
                    SCMailSystem mailSystem = new SCMailSystem();
                    mailSystem.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().receiveGoods(mailSystem);
                    break;
                case MessageID.SCMailFriend:
                    SCMailFriend mailFriend = new SCMailFriend();
                    mailFriend.ParseFrom(data);
                    break;
                case MessageID.SCMailDelete:
                    SCMailDelete mailDelete = new SCMailDelete();
                    mailDelete.ParseFrom(data);
                    break;
                case MessageID.SCMailRead:
                    SCMailRead mailRead = new SCMailRead();
                    mailRead.ParseFrom(data);
                    break;
                case MessageID.SCMailSend:
                    SCMailSend mailSend = new SCMailSend();
                    mailSend.ParseFrom(data);
                    //Obj_MyselfPlayer.GetMe().mailState=mailSend.State;
                    break;
                case MessageID.SCGMcmds:
                    //GM工具返回消息
                    SCGMcmds gmCmd = new SCGMcmds();
                    gmCmd.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().GMCommand(gmCmd);
                    gmCmd = null;
                    break;
                case MessageID.SCGambleRet:
                    SCGambleRet lotteryRet = new SCGambleRet();
                    lotteryRet.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setLotteryData(lotteryRet);
                    break;
                case MessageID.SCGuide:

                    SCGuide guideRet = new SCGuide();
                    guideRet.ParseFrom(data);
                    //				Debug.Log("Receive Finish Step num:"+guideRet.Finish_step);
                    //				Debug.Log("Receive Finish Step:"+(GuideManager.GUIDE_STEP)guideRet.Finish_step);
                    //				GuideManager.Instance.FinishedStep((GuideManager.GUIDE_STEP)guideRet.Finish_step);
                    GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.END);
                    if (guideRet.HasBaseData)
                    {
                        Obj_MyselfPlayer.GetMe().SetUserBaseData(guideRet.BaseData);
                    }
                    if (guideRet.HasBagData)
                    {
                        Obj_MyselfPlayer.GetMe().SetUserBagData(guideRet.BagData);
                    }
                    break;
                case MessageID.SCShopRet:

                    SCShopRet shopRet = new SCShopRet();
                    shopRet.ParseFrom(data);
                    Debug.Log("Receive SCShopRet: Result is : " + shopRet.Result);
                    if (shopRet.HasBaseData)
                        Obj_MyselfPlayer.GetMe().SetUserBaseData(shopRet.BaseData);
                    break;

                case MessageID.SCPVPShopRet:
                    SCPVPShopRet pvpShopRet = new SCPVPShopRet();
                    pvpShopRet.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetPvPShopBuyRetData(pvpShopRet);

                    break;

                case MessageID.SCAskScoreShopFresh:
                    SCAskScoreShopFresh pvpShopScore = new SCAskScoreShopFresh();
                    pvpShopScore.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetPvPShopScore(pvpShopScore);
                    break;
                //王明磊 : 统计模块代码 -> Statistics
                //case MessageID.SCStatistics:
                //	SCStatistics stat = new SCStatistics();
                //	stat.ParseFrom(data);
                //	if(stat.Issuccess == 1)
                //		Debug.Log("upload Data Sucess!!!!");
                //	else if (stat.Issuccess == 2)
                //		Debug.Log("upload Data Error!!!");
                //	else
                //		Debug.Log(stat.Issuccess + " Unknow ERROR!!!!!!");
                //	break;
                case MessageID.SCChangeName:

                    SCChangeName changeName = new SCChangeName();
                    changeName.ParseFrom(data);
                    Debug.Log("Receive SCChangeName: Result is : " + changeName.Type);
                    if (changeName.HasType)
                        Obj_MyselfPlayer.GetMe().changeNameType = changeName.Type;
                    if (changeName.HasBaseData)
                        Obj_MyselfPlayer.GetMe().SetUserBaseData(changeName.BaseData);
                    break;
                case MessageID.SCBindAccount:
                    SCBindAccount account = new SCBindAccount();
                    account.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetCyouBindData(account);
                    break;

                case MessageID.SCAskPVPList:
                    SCAskPVPList pvpMsg = new SCAskPVPList();
                    pvpMsg.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetPVPPlayerInfoData(pvpMsg);
                    break;
                case MessageID.SCProductList:
                    SCProductList product = new SCProductList();
                    product.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setPurchaseInfoData(product);
                    break;

                case MessageID.SCRandomCardFree:
                    SCRandomCardFree cardFree = new SCRandomCardFree();
                    cardFree.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setFreeLotteryData(cardFree);
                    break;

                case MessageID.SCscode:
                    SCscode retcode = new SCscode();
                    retcode.ParseFrom(data);
                    bool bGet = false;
                    if (retcode.HasBagData)
                    {
                        Obj_MyselfPlayer.GetMe().SetUserBagData(retcode.BagData);
                        bGet = true;
                    }

                    if (retcode.HasBaseData)
                    {
                        Obj_MyselfPlayer.GetMe().SetUserBaseData(retcode.BaseData);
                        bGet = true;
                    }

                    GiftWindow.bSuccessGet = bGet;

                    break;

                case MessageID.SC30039: // 爱贝验证结果
                    SC30039 iappResult = new SC30039();
                    iappResult.ParseFrom(data);
                    PurchaseHelper.PayVarifyResult = iappResult.Result;
                    Obj_MyselfPlayer.GetMe().dollar = iappResult.PlayerDollar;
                    break;
                case MessageID.SC30041:     // APPSTROE验证结果
                    SC30041 appStroeResult = new SC30041();
                    appStroeResult.ParseFrom(data);
                    PurchaseHelper.PayVarifyResult = appStroeResult.Result;
                    PurchaseHelper.PayVarifyResultOrderID = appStroeResult.OrderId;
                    Obj_MyselfPlayer.GetMe().dollar = appStroeResult.PlayerDollar;
                    break;

                case MessageID.SCPPVerifyCharge: // pp付费验证
                    SCPPVerifyCharge ppResult = new SCPPVerifyCharge();
                    ppResult.ParseFrom(data);
                    PurchaseHelper.PayVarifyResult = ppResult.Result;
                    Obj_MyselfPlayer.GetMe().dollar = ppResult.PlayerDollar;
                    break;
                case MessageID.SC30038:     // 爱贝商品列表
                    SC30038 productIAppPay = new SC30038();
                    productIAppPay.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setPurchaseInfoData(productIAppPay);
                    break;

                case MessageID.SC30040:     // appstore商品列表
                    SC30040 productAppStore = new SC30040();
                    productAppStore.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setPurchaseInfoData(productAppStore);
                    break;
                case MessageID.SCBuyMoney:
                    SCBuyMoney buyMoney = new SCBuyMoney();
                    buyMoney.ParseFrom(data);
                    if (buyMoney.HasBaseData)
                        Obj_MyselfPlayer.GetMe().SetUserBaseData(buyMoney.BaseData);
                    break;
                case MessageID.SCBuyPower:
                    SCBuyPower buyPower = new SCBuyPower();
                    buyPower.ParseFrom(data);
                    if (buyPower.HasBaseData)
                        Obj_MyselfPlayer.GetMe().SetUserBaseData(buyPower.BaseData);
                    break;
                case MessageID.SCPPProductList: // pp 商品列表
                    SCPPProductList productPP = new SCPPProductList();
                    productPP.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setPurchaseInfoData(productPP);
                    break;
                case MessageID.SCGGL:  //刮刮乐
                    SCGGL msg = new SCGGL();
                    msg.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetGGL(msg);
                    break;
                case MessageID.SCWuxingActivation:
                    SCWuxingActivation wuxingAct = new SCWuxingActivation();
                    wuxingAct.ParseFrom(data);
                    if (wuxingAct.HasFengshui)
                        Obj_MyselfPlayer.GetMe().SetFengshiInfo(wuxingAct.Fengshui);
                    break;
                case MessageID.SCWuxingLevelup:
                    SCWuxingLevelup wuxingLev = new SCWuxingLevelup();
                    wuxingLev.ParseFrom(data);
                    if (wuxingLev.HasFengshui)
                        Obj_MyselfPlayer.GetMe().SetFengshiInfo(wuxingLev.Fengshui);
                    break;
                case MessageID.SCWuxingReset:
                    SCWuxingReset wuxingReset = new SCWuxingReset();
                    wuxingReset.ParseFrom(data);
                    if (wuxingReset.HasFengshui)
                        Obj_MyselfPlayer.GetMe().SetFengshiInfo(wuxingReset.Fengshui);
                    break;

                case MessageID.SCBGZ:  //八卦阵
                    SCBGZ BGZmsg = new SCBGZ();
                    BGZmsg.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetBZT(BGZmsg);
                    break;
                case MessageID.SCMonthCardInfo:
                    SCMonthCardInfo cardInfo = new SCMonthCardInfo();
                    cardInfo.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().setMonthCardInfo(cardInfo);
                    break;
                case MessageID.SCMonthCardGetDollar:
                    SCMonthCardGetDollar monthCardReceive = new SCMonthCardGetDollar();
                    monthCardReceive.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetUserBaseData(monthCardReceive.BaseData);
                    Obj_MyselfPlayer.GetMe().SetUserBagData(monthCardReceive.BagData);
                    break;
                case MessageID.SCFriendMailDelete:
                    SCFriendMailDelete friendMailDelete = new SCFriendMailDelete();
                    friendMailDelete.ParseFrom(data);
                    break;
                case MessageID.SCStudySkill:
                    SCStudySkill stydySkill = new SCStudySkill();
                    stydySkill.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetUserBaseData(stydySkill.BaseData);
                    Obj_MyselfPlayer.GetMe().SetUserBagData(stydySkill.BagData);
                    break;
                case MessageID.SCStudySkillUpdate:
                    SCStudySkillUpdate skillUpdate = new SCStudySkillUpdate();
                    skillUpdate.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetUserBaseData(skillUpdate.BaseData);
                    Obj_MyselfPlayer.GetMe().SetUserBagData(skillUpdate.BagData);
                    break;
				
				case MessageID.SCQxzbPVPDataAsk:
				    SCQxzbPVPDataAsk qxzbMsg = new SCQxzbPVPDataAsk();
					qxzbMsg.ParseFrom(data);
					Obj_MyselfPlayer.GetMe().SetQxzbPvPDataInfo(qxzbMsg);
				    break;
				case MessageID.SCQxzbBattle:
					SCQxzbBattle qxzbBattle = new SCQxzbBattle();
					qxzbBattle.ParseFrom(data);
				
					SCPVPBattleData qxzbBattleData = new SCPVPBattleData();
					qxzbBattleData.CopyData = qxzbBattle.CopyData;
					qxzbBattleData.BagData = qxzbBattle.BagData;
					qxzbBattleData.BaseData = qxzbBattle.BaseData;
					qxzbBattleData.Battle = qxzbBattle.Battle;
					
				 	Obj_MyselfPlayer.GetMe().nQxzbMoney = qxzbBattle.Money;
                    Obj_MyselfPlayer.GetMe().SetPVPBattleData(qxzbBattleData);
                    Obj_MyselfPlayer.GetMe().battleType = Games.Battle.BattleType.QxzbPvP;
                    Obj_MyselfPlayer.GetMe().battel_sign = 2;  //进入加载场景
					break;
                case MessageID.SCAskWorldBossInfo:
                    SCAskWorldBossInfo askWorldBossInfo = new SCAskWorldBossInfo();
                    askWorldBossInfo.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetWorldBossInfo(askWorldBossInfo);
                    break;
                case MessageID.SCWorldBossAddZhufu:
                    SCWorldBossAddZhufu worldBossAddZhufu = new SCWorldBossAddZhufu();
                    worldBossAddZhufu.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetWorldBossBuffInfo(worldBossAddZhufu);
                    break;
                case MessageID.SCWorldBossResurgence:
                    SCWorldBossResurgence worldBossResurgence = new SCWorldBossResurgence();
                    worldBossResurgence.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetWorldBossResurgenceInfo(worldBossResurgence);
                    break;
				
				case MessageID.SCQxzbPVPClearCD:
					SCQxzbPVPClearCD pvpClearCD = new SCQxzbPVPClearCD();
				    pvpClearCD.ParseFrom(data);
				    break;
                case MessageID.SCWorldBossWeekRank:
                    SCWorldBossWeekRank worldBossWeedRank = new SCWorldBossWeekRank();
                    worldBossWeedRank.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().SetWorldBossRank(worldBossWeedRank);
                    break;
				case MessageID.SCAskActivity:
					SCAskActivity askActivity = new SCAskActivity();
					askActivity.ParseFrom(data);
					Obj_MyselfPlayer.GetMe().SetActivityData(askActivity);
					break;
				case MessageID.SCAskChangeCardList:
					SCAskChangeCardList changeCardList = new SCAskChangeCardList();
					changeCardList.ParseFrom(data);
					Obj_MyselfPlayer.GetMe().SetChangeCardListData(changeCardList);
					break;
				case MessageID.SCChangeCardConfirm:
					SCChangeCardConfirm changeCardConfirm = new SCChangeCardConfirm();
					changeCardConfirm.ParseFrom(data);
					Obj_MyselfPlayer.GetMe().SetChangeCardConfirmData(changeCardConfirm);
					break;
                case MessageID.SCWorldBossWeekReward:
                    SCWorldBossWeekReward worldBossWeedReward = new SCWorldBossWeekReward();
                    worldBossWeedReward.ParseFrom(data);
                    Obj_MyselfPlayer.GetMe().rewardLev = worldBossWeedReward.RewardLev;
                    Obj_MyselfPlayer.GetMe().SetUserBaseData(worldBossWeedReward.BaseData);
                    Obj_MyselfPlayer.GetMe().hasWorldBossReward = worldBossWeedReward.HasReward;
                    break;
				case MessageID.SCQxzbGetReward:
                    SCQxzbGetReward scqxzbgetreward = new SCQxzbGetReward();
                    Obj_MyselfPlayer.GetMe().get_result = scqxzbgetreward.Result;
					break;
#if	UNITY_ANDROID
            case MessageID.SCCYouProductList:                
			{                    
				SCCYouProductList productCYou = new SCCYouProductList();                    
				productCYou.ParseFrom(data);                    
				Obj_MyselfPlayer.GetMe().setPurchaseInfoData(productCYou);                
			}
			break;
			case MessageID.SCCommonProductListRet:
				SCCommonProductListRet productCom = new SCCommonProductListRet();
				Debug.Log("----data==="+data.Length);
				productCom.ParseFrom(data);
				Obj_MyselfPlayer.GetMe().setThirdPurchaseInfoData(productCom);     
				break;
			case MessageID.SCCYouVerifyCharge:
			{                
				AndroidConfig.SCVerifyCharge(data);
			}
			break;
			case MessageID.SCThirdPlatformVerifyCharge:
			{
				AndroidConfig.SCVerifyCharge(data);
			}
			break;
			case MessageID.SCCYouPayVerifyChargeRet:
				AndroidConfig.SCVerifyCharge(data);
				break;
			case MessageID.SCGooglePayVerifyChargeRet:
				AndroidConfig.SCVerifyCharge(data);
				break;
#endif
			case MessageID.SCTaskOver:
				Debug.Log("NetworkReceiver SCTaskOver");
				SCTaskOver scTaskOver = new SCTaskOver ();
				scTaskOver.ParseFrom(data);
				Obj_MyselfPlayer.GetMe().requestTaskSuccess = scTaskOver.TaskOver > 0?true:false;
				break;
			case MessageID.SCYunyingHuodong:
				Debug.Log("NetworkReceiver SCYunyingHuodong");
				SCYunyingHuodong scYunyingHuodong = new SCYunyingHuodong();
				scYunyingHuodong.ParseFrom(data);

				Obj_MyselfPlayer.GetMe().SetYunyinghuodong(scYunyingHuodong);

				break;				
                default:
                    handled = false;
                    break;
            }
            NetworkSender.Instance().sendFinish(true);
            UIListener.Instance().OnReceiveMsg(handled);
            //			UIListener.Instance().resetListener();
            return handled;
        }

        public bool handleError(HttpErrorID errorId)
        {
            bool ret = false;
            switch (errorId)
            {
                case HttpErrorID.VERSION_ERROR:
                    {
                        Utils.ShowTip("VERSION_ERROR error iis in hand : " + errorId);
                        ret = true;
                    }
                    break;
                case HttpErrorID.PLAYER_ID_ERROR:
                    {
                        Utils.ShowTip("Login error is in hand: ID error " + errorId);
                        ret = true;
                    }
                    break;
                case HttpErrorID.PLAYER_ACCOUNT_EXCEPTION:
                    {//账号异常
                        Utils.ShowTip("ACCOUNT_EXCEPTION error is in hand: " + errorId);
                        ret = true;
                    }
                    break;
                case HttpErrorID.ERROR_PLAYER_UPGREAD_LEVEL_ERROR:
                    {//升级异常
                        Utils.ShowTip("PLAYER_UPGREAD_LEVEL_ERROR: " + errorId);
                        ret = true;
                    }
                    break;

            }
            Utils.ShowTip("ERROR: " + errorId);

            UIListener.Instance().OnReceiveMsg(false);


            return ret;
        }
#if UNITY_ANDROID
		// 只用于第三方，所以没有cyou
		private void SCLogin(byte[]data){
			Debug.Log("SCLogin  "+data);
			
			if(AndroidConfig.isUCChannel()||AndroidConfig.isThirdSDKPlatform()){
				//登录返回消息
				SCSDKLoginThirdPlatformRet loginUCRet = new SCSDKLoginThirdPlatformRet();
				//SCLoginThirdPlatformRet loginUCRet = new SCLoginThirdPlatformRet();//Serializer.Deserialize<SCLoginRet>(msData);
				loginUCRet.ParseFrom(data);
				
				Obj_MyselfPlayer.GetMe().giftison = loginUCRet.Giftison;

				Obj_MyselfPlayer.GetMe().SetSDKLoginRetData(loginUCRet);

				//王明磊 - 保存本次登录的AccountID 表示本次登录的统计信息所属
				if (loginUCRet.UcAccount != null && loginUCRet.UcAccount != "")
				{
					PlayerPrefs.SetString("LastAccountId",loginUCRet.UcAccount);
				}
				// 礼包开关设置 //
				//Obj_MyselfPlayer.GetMe().giftison = loginUCRet.Giftison;
				//如果是畅游账号登录或注册--
				if(loginUCRet.RetCode == 0)
				{
					//检查登陆状态
					//1.新玩家 2.老玩家
					if(AndroidConfig.isSogouChannel()||(AndroidConfig.isAnZhiChannel())){
			           AndroidConfig.hideFloatButton();
		            }
					Debug.Log("---登录成功");
					PaySystemInterface.doSdk("onLoginVerify",loginUCRet.JsonData);	
					Debug.Log("onLoginVerify  "+loginUCRet.JsonData+"  RetCode =  "+loginUCRet.RetCode+"  UcAccount = "+loginUCRet.UcAccount);
					// 获取从服务器下发的关键字 //
					
					//UC渠道代码重构 lihao_yd 2013-12-10
					/*
					AndroidConfig.SetApiKey(loginUCRet.ApiKey);
					AndroidConfig.SetAppKey(loginUCRet.Appkey);
					AndroidConfig.SetServerID(loginUCRet.ServerId);
					AndroidConfig.SetGameID(loginUCRet.GameId);
					AndroidConfig.SetAppSecret(loginUCRet.Appsecret);
					*/
					
					AndroidConfig.SetUcAccount(loginUCRet.UcAccount);
					//设置用户已登陆标志
					GameManager.userLoginDone();
					Obj_MyselfPlayer.GetMe().SetSDKLoginRetData(loginUCRet);
					//打印返回信息
					Debug.Log("state:"+loginUCRet.State);
					Debug.Log("AccountId:"+loginUCRet.UcAccount);
					loginUCRet = null;
				}
			}
			//王明磊 - 清理过期统计数据
			for (int btnNo = 1; btnNo <= 58; btnNo++)
			{
				string key = "Btn" + btnNo.ToString();
				if (PlayerPrefs.HasKey(key))
				{
					PlayerPrefs.DeleteKey(key);
				}
			}
			//PlayerPrefs.DeleteKey("LastAccountId");
			PlayerPrefs.SetInt("Btn1",PlayerPrefs.GetInt("Btn-1"));
			PlayerPrefs.SetInt("Btn2",PlayerPrefs.GetInt("Btn-2"));
		}
#endif
		void On_PowerSendAlready_ErrorMsgButtonClick(GameObject obj){
			GameObject.Find("MainUILogic").SendMessage("OnFriendWindow");
		}
    }
	
	
}
