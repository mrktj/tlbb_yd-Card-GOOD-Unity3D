using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using card.net;
using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using System;


public class MonthCardController : MonoBehaviour
{
    public UILabel m_LabelRemainDay;
    public UILabel m_LabelPurchaseInfo;
    public UISprite m_SpriteTitle;
    public UISprite m_SpriteCongratulation;
    public UISprite m_SpriteMonthCardState;
    public UISprite m_SpriteCloseDay;
    public UISlider m_PurchaseProgress;
    public UIImageButton m_ToPurchaseBtn;
    public UIImageButton m_GetDollarBtn;
    public GameObject m_ShowUI;
    private GameObject mainLogic;
    private MainController mainController;
    private MonthCardInfoClass monthCardInfo = new MonthCardInfoClass();
    private MainController m_MainController;
    private bool finishGet = false;

    // Use this for initialization
    void Start()
    {
        mainLogic = GameObject.Find("MainUILogic");
    }

    void OnEnable()
    {
        if (mainController == null)
        {
            mainController = GameObject.Find("MainController").GetComponent<MainController>();
        }
        finishGet = false;
        mainController.ShowActivityTopUI(ActivityType.E_ACTIVITY_TYPE_MONTH_CARD);
        m_ShowUI.SetActive(false);
        RefreashUI(null);
    }

    void OnDisable()
    {
        if (mainController != null)
        {
            mainController.ShowtopInfo();
        }
    }

    public void RefreashUI(GameObject go)
    {
        NetworkSender.Instance().RequestMonthCardInfo(UpdateMonthCardInfoDone);
    }

    public void UpdateMonthCardInfoDone(bool bSuccess)
    {
        monthCardInfo = Obj_MyselfPlayer.GetMe().monthCardInfo;
        if (Obj_MyselfPlayer.GetMe().monthCardInfo.monthCardState == 0)
        {
            Obj_MyselfPlayer.GetMe().monthCardFlag = 0;
            mainController.MonthCardBtn.SetActive(false);
            mainController.ChangeBtnUI();
            BoxManager.showMessageByID((int)MessageIdEnum.Msg234);
            mainController.OpenGGLWindow();
        }
        if (monthCardInfo.monthCardState == 1)
        {
            //开启
            m_GetDollarBtn.gameObject.SetActive(false);
            m_ToPurchaseBtn.gameObject.SetActive(true);
            m_PurchaseProgress.gameObject.SetActive(true);
            m_SpriteCongratulation.gameObject.SetActive(false);
            m_PurchaseProgress.sliderValue = (float)monthCardInfo.currentPurchase / (float)monthCardInfo.totalPurchase;
            m_LabelPurchaseInfo.text = monthCardInfo.currentPurchase + "/" + monthCardInfo.totalPurchase;
            m_SpriteTitle.spriteName = "zi_1";
            m_SpriteTitle.MakePixelPerfect();
            m_SpriteMonthCardState.spriteName = "jubaopen_neiqian_2";
            m_SpriteCloseDay.spriteName = "5";
            m_SpriteCloseDay.MakePixelPerfect();
            m_LabelRemainDay.text = monthCardInfo.remainTime.ToString();
            m_LabelRemainDay.transform.localPosition = new Vector3(0, -190, -10);
        }
        else if (monthCardInfo.monthCardState == 2)
        {
            //完成
            m_GetDollarBtn.gameObject.SetActive(true);
            m_ToPurchaseBtn.gameObject.SetActive(false);
            m_GetDollarBtn.isEnabled = true;
            m_GetDollarBtn.transform.FindChild("Label").GetComponent<UISprite>().spriteName = "lingquyuanbao_2";
            m_GetDollarBtn.transform.FindChild("Label").GetComponent<UISprite>().MakePixelPerfect();
            m_PurchaseProgress.gameObject.SetActive(false);
            m_SpriteCongratulation.gameObject.SetActive(true);
            m_SpriteTitle.spriteName = "zi_2";
            m_SpriteTitle.MakePixelPerfect();
            m_SpriteMonthCardState.spriteName = "jubaopen_neiqian_1";
            m_SpriteCloseDay.spriteName = "3";
            m_SpriteCloseDay.MakePixelPerfect();

#if UNITY_ANDROID
            {
                m_SpriteCloseDay.transform.localScale = new Vector3(288, 24, 1);
            }
#endif

            m_LabelRemainDay.text = monthCardInfo.rewardRemainDays.ToString();
            m_LabelRemainDay.transform.localPosition = new Vector3(40, -190, -10);
        }
        else if (monthCardInfo.monthCardState == 3)
        {
            //已领取
            m_GetDollarBtn.gameObject.SetActive(true);
            m_ToPurchaseBtn.gameObject.SetActive(false);
            m_GetDollarBtn.isEnabled = false;
            m_GetDollarBtn.transform.FindChild("Label").GetComponent<UISprite>().spriteName = "jinriyilingqu_2";
            m_GetDollarBtn.transform.FindChild("Label").GetComponent<UISprite>().MakePixelPerfect();
            m_PurchaseProgress.gameObject.SetActive(false);
            m_SpriteCongratulation.gameObject.SetActive(true);
            m_SpriteTitle.spriteName = "zi_2";
            m_SpriteTitle.MakePixelPerfect();
            m_SpriteMonthCardState.spriteName = "jubaopen_neiqian_2";
            m_SpriteCloseDay.spriteName = "3";
            m_SpriteCloseDay.MakePixelPerfect();

#if UNITY_ANDROID
            {
                m_SpriteCloseDay.transform.localScale = new Vector3(288, 24, 1);
            }
#endif


            m_LabelRemainDay.text = monthCardInfo.rewardRemainDays.ToString();
            m_LabelRemainDay.transform.localPosition = new Vector3(40, -190, -10);
        }
        m_ShowUI.SetActive(true);
        if (finishGet)
        {
            finishGet = false;
            BoxManager.showMessageByID((int)MessageIdEnum.Msg235);
        }
    }

    void OnGetDollarBtn()
    {
        if (Obj_MyselfPlayer.GetMe().cardBagList.Count >= Obj_MyselfPlayer.GetMe().bagMax)
        {
            //			BoxManager.showBagFullBox("您携带的侠士已经达到上限可以将侠士吸收、出售或者扩充您的背包.");
            BoxManager.showMessageByID((int)MessageIdEnum.Msg74);
            return;
        }
        NetworkSender.Instance().RequestGetMonthCardDollar(GetMonthCardDollarDone);
    }

    public void GetMonthCardDollarDone(bool isSuccessed)
    {
        //BoxManager.showMessageByID((int)MessageIdEnum.Msg235);
        GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
        finishGet = true;
        NetworkSender.Instance().RequestMonthCardInfo(UpdateMonthCardInfoDone);
        //UIEventListener.Get(BoxManager.getYesButton()).onClick += RefreashUI;
    }

    void OnToPurchaseBtn()
    {
        mainLogic.SendMessage("OnPurchaseWindow");
    }
}
