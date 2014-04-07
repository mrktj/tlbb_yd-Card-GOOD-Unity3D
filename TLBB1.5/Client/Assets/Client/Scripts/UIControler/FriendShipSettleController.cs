using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using card.net;
public class FriendShipSettleController : MonoBehaviour
{

    private GameObject mainLogic;
    public UILabel username;
    public UITexture cardBigIcon;
    public UISprite cardFrame;
    public UILabel cardLevel;
    public UILabel newPoint;
    public UILabel myPoint;
    public UISprite iconCategory;
    public GameObject notMyFriend;
    public GameObject isMyFriend;
    public AssistFriend myAssistFriend;
    public UISprite cardBgFrame;
    // Use this for initialization
    void Start()
    {

    }

    void OnEnable()
    {
        mainLogic = GameObject.Find("MainUILogic");
        Debug.Log("xlym: OnEnable FriendShipSettleController");
        myAssistFriend = Obj_MyselfPlayer.GetMe().currentAssistFriend;
        if (myAssistFriend != null)
        {
            if (myAssistFriend.isMyFriend)
            {
                notMyFriend.SetActive(false);
                isMyFriend.SetActive(true);
            }
            else
            {

                notMyFriend.SetActive(true);
                isMyFriend.SetActive(false);
            }
            username.text = myAssistFriend.name;
            if (myAssistFriend.cardTempleId > 0)
            {
                iconCategory.spriteName = UserCardItem.elementTypeName[TableManager.GetCardByID(myAssistFriend.cardTempleId).Element];
                iconCategory.MakePixelPerfect();
                cardBgFrame.spriteName = UserCardItem.largeCardName[TableManager.GetCardByID(myAssistFriend.cardTempleId).Star];
                cardFrame.spriteName = UserCardItem.largeCardBorderName[TableManager.GetCardByID(myAssistFriend.cardTempleId).Star];
                cardFrame.MakePixelPerfect();
                //cardBigIcon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(myAssistFriend.cardTempleId).Appearance).BodyIcon;// 
                AtlasManager.Instance.setBodyByTempletID(cardBigIcon, myAssistFriend.cardTempleId);
            }
            cardLevel.text = myAssistFriend.cardLevel.ToString();
            if (Obj_MyselfPlayer.GetMe().battleData.isWin)
            {
                newPoint.text = myAssistFriend.friendShipNum.ToString();
            }
            else
            {
                newPoint.text = "0";
            }
            myPoint.text = Obj_MyselfPlayer.GetMe().fpoint.ToString();
        }
        //daixiugai
        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_2_END)
        {
            GuideManager.Instance.checkGuideState();
        }
    }

    //Call Back
    public void AddFriend()
    {
        //B12
        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_2_END)
            GuideCopy1_2_End.Instance.NextStep();//战斗结束引导 SELECT_1

        Debug.Log("ADDFriend");
        NetworkSender.Instance().ADDFriend(addFriendDone, myAssistFriend.guid);
    }
    public void addFriendDone(bool isSuccess)
    {
        //这里新手引导中必须返回主界面
        ReturnToPVEBossList();
    }
    public void ReturnToPVEBossList()
    {
        Debug.Log("xlym: FriendShipSettleController return to ReturnToPVEBossList");
        //这里新手引导中必须返回主界面
        if (!GuideManager.Instance.isEnd())
        {
            if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_3_END)
            {
                GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.COPY1_3_END);
                ReturnToMainUI();
                return;
            }
            else if (GuideManager.Instance.currentStep != GuideManager.GUIDE_STEP.COPY1_3)
            {
                ReturnToMainUI();
                return;
            }
            else if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_3)
            {
                //不做处理，跳转至pve列表
            }
        }
        if (Obj_MyselfPlayer.GetMe().battleData.isWin)
        {
            GameObject.FindWithTag("main_controller").SendMessage("showBottomBar");

            if (Obj_MyselfPlayer.GetMe().isLastBattleNotFinish)
            {
                mainLogic.SendMessage("LoadPveSceneList");
                return;
            }
            int nextSubCopy = Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID + 1;
            Debug.Log("NextCopyID : " + nextSubCopy);
            List<SubCopy> subCopys = Obj_MyselfPlayer.GetMe().normalCopys;
            Debug.Log("GetMe().normalCopys.Count = " + subCopys.Count);
            bool isMainCopyOpen = true;
            foreach (SubCopy sub in subCopys)
            {
                if (sub.subCopyID == nextSubCopy)
                    isMainCopyOpen = false;
            }
            Tab_Copydetail nextSubDetail = TableManager.GetCopydetailByID(nextSubCopy);
            if (nextSubDetail == null) //如果获取下一副本不存在,要返回当前大副本小副本列表 可能发生在最后一个小副本
            {
                Debug.Log("No Next Copy [NextCopyDetail is Null]");
                mainLogic.SendMessage("LoadPveBossList");
                return;
            }
            Debug.Log("GetMe().curSubcopy.tblCopyDetail.Copyfather = " + Obj_MyselfPlayer.GetMe().curSubcopy.tblCopyDetail.Copyfather);
            if (nextSubDetail.Copyfather == Obj_MyselfPlayer.GetMe().curSubcopy.tblCopyDetail.Copyfather)
                isMainCopyOpen = false;
            if (!isMainCopyOpen) //没有大副本开启
            {
                Debug.Log("No Main Copy New Open");
                mainLogic.SendMessage("LoadPveBossList");
            }
            else
            {
                Tab_Copy nextMainCopy = TableManager.GetCopyByID(nextSubDetail.Copyfather);
                if (Obj_MyselfPlayer.GetMe().level >= nextMainCopy.PlayerLevel) //等级足够进入新大副本
                {
                    Debug.Log("Level is OK For New Main Copy -> UserLevel : " + Obj_MyselfPlayer.GetMe().level + " Need : " + nextMainCopy.PlayerLevel);
                    Debug.Log("GetMe().normalMainCopys.Count = " + Obj_MyselfPlayer.GetMe().normalMainCopys.Count);
                    foreach (MainCopy mainCopy in Obj_MyselfPlayer.GetMe().normalMainCopys)
                    {
                        if (mainCopy.copyId == nextSubDetail.Copyfather)
                        {
                            if (Obj_MyselfPlayer.GetMe().isNextMainOpened)
                            {
                                Debug.Log("NextMainOpened");
                                mainLogic.SendMessage("LoadPveBossList");//非首次打开 返回当前大副本小副本列表
                                return;
                            }
                            else if (mainCopy.copyId == 2) //如果是第二个大副本,并且是首次打开
                            {
                                Debug.Log("No.2 Main New Open");
                                if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.GIFT)
                                {
                                    GuideManager.Instance.isInSubStep = false;
                                }
                                mainLogic.SendMessage("ReturnToMainUI"); //首次打开第二大副本 返回主界面
                                return;
                            }
                            else
                                Obj_MyselfPlayer.GetMe().curMainCopy = mainCopy; //首次打开其他大副本-跳转到新开启大副本的小副本列表
                            break;
                        }
                    }
                }
                Debug.Log("Level < Request || Other Main Copy New Open");
                mainLogic.SendMessage("LoadPveBossList");
            }
        }
        else
        {
            mainLogic.SendMessage("onBattleFailWindow");
        }
        //		GameObject.FindWithTag("main_controller").SendMessage("showBottomBar");
        //		mainLogic.SendMessage("LoadPveBossList");		
    }
    public void ReturnToMainUI()
    {
        Debug.Log("xlym: FriendShipSettleController return to TeamController");
        mainLogic.SendMessage("ReturnToMainUI");
        GameObject.FindWithTag("main_controller").SendMessage("showBottomBar");
    }

    public GameObject getGuideItem()
    {
        return transform.FindChild("Not-My-Friend/Button-Add-Friend").gameObject;
    }
}
