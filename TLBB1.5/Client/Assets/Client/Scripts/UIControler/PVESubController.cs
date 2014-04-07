using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using Module.Log;
using card.net;
using System;

public class PVESubController : MonoBehaviour
{

    public GameObject grid;
    public UISprite title;
    public UIScrollBar scrollBar;

    private string subCopyItem = "CopySubItem";
    private GameObject logicTarget = null;
    private MainCopy mainCopy = null;
    public UIDraggablePanel dragPanel;
    private string SubCopy_Key = "SubCopy_Key";
    private int subCopyCount = 0;

    // Use this for initialization
    void Awake()
    {
    }

    void Start()
    {
        logicTarget = GameObject.Find("MainUILogic");
        Refresh();

    }

    // Update is called once per frame
    void Update()
    {
        //FreashScroll();
    }

    private void FreashScroll()
    {
        if (scrollBar.scrollValue == 0 || scrollBar.scrollValue == 1)
        {
            dragPanel.scale.y = 0.1f;
        }
        else
        {
            dragPanel.scale.y = 1;
        }
    }

    private void CreateList()
    {
        //title.text = LanguageManger.GetWords( mainCopy.tblCopy.Copyname );

        GameObject parent = grid;
        int count = parent.transform.childCount;
        for (int i = count - 1; i >= 0; i--)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }

        parent.GetComponent<UIGrid>().repositionNow = true;
        parent.SetActive(true);
        if (mainCopy.tblCopy.GetCopyTypebyIndex(0) == (int)CopyType.NORMAL)
        {
            for (int i = mainCopy.subCopys.Count - 1; i >= 0; i--)
            {
                bool canOpen = false;
                if (mainCopy.subCopys[i].copyState == CopyState.UNOPEN)
                {
                    if (i == 0)
                    {
                        canOpen = true;
                    }
                    else if (mainCopy.subCopys[i - 1].copyState == CopyState.PASSED)
                    {
                        canOpen = true;
                    }
                    //mainCopy.subCopys[i].copyState = CopyState.OPENED;
                }
                else if (mainCopy.subCopys[i].copyState == CopyState.PASSED)
                {
                    canOpen = true;
                }
                if (canOpen)
                {
                    GameObject new_item = ResourceManager.Instance.loadWidget(subCopyItem);
                    new_item.transform.parent = parent.transform;
                    new_item.transform.localPosition = Vector3.zero;
                    new_item.transform.localScale = Vector3.one;
                    new_item.transform.name = mainCopy.subCopys[i].subCopyID.ToString();
                    //name
                    UILabel label_name = new_item.transform.FindChild("Labels/Label-Name-Value").GetComponent<UILabel>();
                    label_name.text = LanguageManger.GetWords(mainCopy.subCopys[i].tblCopyDetail.Copyname);


                    //设置名字为"无量剑西宗"的item为clone obj
                    if (mainCopy.subCopys[i].tblCopyDetail.Copyname == TableManager.GetCopydetailByID(TableManager.GetCopyByID(1).GetSubcopybyIndex(0)).Copyname &&
                            GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_1)
                    {
                        this.guideItem = new_item;
                    }
                    else if (mainCopy.subCopys[i].tblCopyDetail.Copyname == TableManager.GetCopydetailByID(TableManager.GetCopyByID(1).GetSubcopybyIndex(1)).Copyname &&
                           GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_2)
                    {
                        this.guideItem = new_item;
                    }
                    else if (mainCopy.subCopys[i].tblCopyDetail.Copyname == TableManager.GetCopydetailByID(TableManager.GetCopyByID(1).GetSubcopybyIndex(2)).Copyname &&
                           GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_3)
                    {
                        this.guideItem = new_item;
                    }
                    else if (mainCopy.subCopys[i].tblCopyDetail.Copyname == TableManager.GetCopydetailByID(TableManager.GetCopyByID(1).GetSubcopybyIndex(3)).Copyname &&
                           GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_4)
                    {
                        this.guideItem = new_item;
                    }
                    else if (mainCopy.subCopys[i].tblCopyDetail.Copyname == TableManager.GetCopydetailByID(TableManager.GetCopyByID(1).GetSubcopybyIndex(4)).Copyname &&
                           GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_5)
                    {
                        this.guideItem = new_item;
                    }
                    else if (mainCopy.subCopys[i].tblCopyDetail.Copyname == TableManager.GetCopydetailByID(TableManager.GetCopyByID(2).GetSubcopybyIndex(0)).Copyname &&
                           GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY2_1)
                    {
                        this.guideItem = new_item;
                    }

                    //state
                    UISprite sprite_state = new_item.transform.FindChild("Sprites/Sprite-State").GetComponent<UISprite>();
                    //UILabel label_state = new_item.transform.FindChild("Labels/Label-State-Value").GetComponent<UILabel>();
                    if (mainCopy.subCopys[i].copyState == CopyState.PASSED)
                    {
                        sprite_state.spriteName = "yitongguan";
                        //label_state.text = "[d4a106]已通关[000000]";
                    }
                    else
                    {
                        sprite_state.spriteName = "xinkaiqi";
                        //label_state.text = "[89ca88]新开启[000000]";
                    }

                    //power
                    UILabel label_power = new_item.transform.FindChild("Labels/Label-Power-Value").GetComponent<UILabel>();
                    label_power.text = mainCopy.subCopys[i].tblCopyDetail.CostValue.ToString();

                    //Count
                    UILabel label_count = new_item.transform.FindChild("Labels/Label-Count-Value").GetComponent<UILabel>();
                    label_count.gameObject.SetActive(false);
                    label_count = new_item.transform.FindChild("Labels/Label-Count").GetComponent<UILabel>();
                    label_count.gameObject.SetActive(false);

                    //Star
                    GameObject go_star = new_item.transform.FindChild("Star").gameObject;
                    for (int j = 0; j < 3; j++)
                    {
                        if (j < mainCopy.subCopys[i].maxStar)
                        {
                            go_star.transform.GetChild(j).gameObject.SetActive(true);
                        }
                        else
                        {
                            go_star.transform.GetChild(j).gameObject.SetActive(false);
                        }
                    }

                    UIEventListener.Get(new_item).onClick += OnSelectSubCopy;
                    subCopyCount++;
                }
            }
            if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_2 ||
                GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_3)
            {
                GuideManager.Instance.checkGuideState(0);
            }
        }
        else
        {
            for (int i = 0; i < mainCopy.subCopys.Count; i++)
            {
                GameObject new_item = ResourceManager.Instance.loadWidget(subCopyItem);//(GameObject)Instantiate(subCopyItem);
                new_item.transform.parent = parent.transform;
                new_item.transform.localPosition = Vector3.zero;
                new_item.transform.localScale = Vector3.one;
                new_item.transform.name = mainCopy.subCopys[i].subCopyID.ToString();

                //name
                UILabel label_name = new_item.transform.FindChild("Labels/Label-Name-Value").GetComponent<UILabel>();
                label_name.text = LanguageManger.GetWords(mainCopy.subCopys[i].tblCopyDetail.Copyname);

                //state
                //UILabel label_state = new_item.transform.FindChild("Labels/Label-State-Value").GetComponent<UILabel>();
                UISprite sprite_state = new_item.transform.FindChild("Sprites/Sprite-State").GetComponent<UISprite>();
                sprite_state.spriteName = "xinkaiqi";
                //power
                UILabel label_power = new_item.transform.FindChild("Labels/Label-Power-Value").GetComponent<UILabel>();
                label_power.text = mainCopy.subCopys[i].tblCopyDetail.CostValue.ToString();

                //count
                UILabel label_count = new_item.transform.FindChild("Labels/Label-Count-Value").GetComponent<UILabel>();
                UILabel label_countTitle = new_item.transform.FindChild("Labels/Label-Count").GetComponent<UILabel>();
                int copyCount = TableManager.GetCopydetailByID(mainCopy.subCopys[i].subCopyID).CopyLimit - mainCopy.subCopys[i].count;
                //				if(copyCount<0)
                //				{
                //					label_count.text="无限次";
                //				}
                //				else
                //				{
                //					label_count.text = copyCount+"次";
                //				}
                if (copyCount <= 0)//JackWen 加入容错，避免服务器错误数据导致玩家无限刷bug
                {
                    //new_item.GetComponent<UIImageButton>().isEnabled=false;
                    Destroy(new_item.GetComponent<UIImageButton>());
                    label_countTitle.text = "[ff231a]今日剩余:[000000]";
                    label_count.text = "[ff231a]" + copyCount + "次[000000]";
                }
                else
                {
                    UIEventListener.Get(new_item).onClick += OnSelectSubCopy;
                    label_countTitle.text = "[f1eccf]今日剩余:[000000]";
                    label_count.text = "[f1eccf]" + copyCount + "次[000000]";
                }
                //Star
                GameObject go_star = new_item.transform.FindChild("Star").gameObject;
                for (int j = 0; j < 3; j++)
                {
                    if (j < mainCopy.subCopys[i].maxStar)
                    {
                        go_star.transform.GetChild(j).gameObject.SetActive(true);
                    }
                    else
                    {
                        go_star.transform.GetChild(j).gameObject.SetActive(false);
                    }
                }
                subCopyCount++;
            }
        }

        parent.GetComponent<UIGrid>().repositionNow = true;
        scrollBar.scrollValue = 0;
    }

    public void OnEnable()
    {
        Refresh();
    }

    void OnDisable()
    {
        ScrollData scData = new ScrollData(scrollBar.scrollValue);
        Obj_MyselfPlayer.GetMe().SetScrollValue(SubCopy_Key, scData);
    }

    void Refresh()
    {
        subCopyCount = 0;
        Obj_MyselfPlayer.GetMe().UpdateCopyList(CopyType.NORMAL);
        Obj_MyselfPlayer.GetMe().UpdateCopyList(CopyType.ACTIVITY);
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().normalMainCopys.Count; i++)
        {
            if (Obj_MyselfPlayer.GetMe().curMainCopy.copyId == Obj_MyselfPlayer.GetMe().normalMainCopys[i].copyId)
            {
                Obj_MyselfPlayer.GetMe().curMainCopy = Obj_MyselfPlayer.GetMe().normalMainCopys[i];
                //title.spriteName = "选择副本";
                //title.MakePixelPerfect();
                break;
            }
        }
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().activityMainCopys.Count; i++)
        {
            if (Obj_MyselfPlayer.GetMe().curMainCopy.copyId == Obj_MyselfPlayer.GetMe().activityMainCopys[i].copyId)
            {
                Obj_MyselfPlayer.GetMe().curMainCopy = Obj_MyselfPlayer.GetMe().activityMainCopys[i];
                //title.spriteName = "选择活动";
                //title.MakePixelPerfect();
                break;
            }
        }

        mainCopy = Obj_MyselfPlayer.GetMe().curMainCopy;
        if (mainCopy != null)
        {
            CreateList();
            StartCoroutine(SetScrollValue());
        }
        else
        {
            Debug.LogError("main copy is null !");
        }
    }

    public IEnumerator SetScrollValue()
    {
        yield return 0;
        FreshBar();
    }

    public void FreshBar()
    {
        if (Obj_MyselfPlayer.GetMe().scrollRecord.ContainsKey(SubCopy_Key))
        {
            if (subCopyCount > 4)
            {
                scrollBar.scrollValue = Obj_MyselfPlayer.GetMe().scrollRecord[SubCopy_Key].scrollValue;
            }
            else
            {
                scrollBar.scrollValue = 0;
            }
        }
    }

    public void OnSelectSubCopy(GameObject go)
    {
        switch (GuideManager.Instance.currentStep)
        {
            case GuideManager.GUIDE_STEP.COPY1_1:
                GuideCopy1_1.Instance.NextStep();//新手战斗引导 SELECT_3
                break;
            case GuideManager.GUIDE_STEP.COPY1_2:
                GuideCopy1_2.Instance.NextStep();//新手战斗引导 SELECT_3
                break;
            case GuideManager.GUIDE_STEP.COPY1_3:
                GuideCopy1_3.Instance.NextStep();//新手战斗引导 SELECT_3
                break;
            case GuideManager.GUIDE_STEP.COPY1_4:
                GuideCopy1_4.Instance.NextStep();//新手战斗引导 SELECT_3
                break;
            case GuideManager.GUIDE_STEP.COPY1_5:
                GuideCopy1_5.Instance.NextStep();//新手战斗引导 SELECT_3
                break;
            case GuideManager.GUIDE_STEP.COPY2_1:
                GuideCopy2_1.Instance.NextStep();//新手战斗引导 SELECT_3
                break;
            default:
                break;
        }
        Obj_MyselfPlayer.GetMe().isNextMainOpened = false; //初始化下一大副本是否开启的标志 by 王明磊
        if (Obj_MyselfPlayer.GetMe().cardBagList.Count >= Obj_MyselfPlayer.GetMe().bagMax)
        {
            //			BoxManager.showBagFullBox("您携带的侠士已经达到上限可以将侠士吸收、出售或者扩充您的背包.");
            BoxManager.showMessageByID((int)MessageIdEnum.Msg74);
            return;
        }
        //领导力
        int nowLeaderShip = Obj_MyselfPlayer.GetMe().GetLeaderShipValue();
        if (nowLeaderShip > Obj_MyselfPlayer.GetMe().leadership)
        {
            //			BoxManager.showMessage("当前领导力不足");
            BoxManager.showMessageByID((int)MessageIdEnum.Msg55);
            return;
        }
        SubCopy sub_copy = null;
        int id = int.Parse(go.name);
        foreach (SubCopy copy in mainCopy.subCopys)
        {
            if (copy.subCopyID == id)
            {
                sub_copy = copy;
                break;
            }
        }
        if (sub_copy != null)
        {
            if (sub_copy.tblCopyDetail.CostValue > TableManager.GetIdexperienceByID(Obj_MyselfPlayer.GetMe().level).IDPhysicalValue)
            {
                //上限判断
                BoxManager.showMessageByID((int)MessageIdEnum.Msg134);
                return;
            }
            if (sub_copy.tblCopyDetail.CostValue > Obj_MyselfPlayer.GetMe().power)
            {
                //				BuySth((int)MessageIdEnum.Msg72,2); //WML MARK
                NetworkSender.Instance().buyPower(BuySthDone);
                return;
            }
            Obj_MyselfPlayer.GetMe().curSubcopy = sub_copy;
            Debug.Log("curSubcopyID:" + sub_copy.subCopyID);
            //cb:因引导需要，必须在已经获得数据之后，才切换的助战好友界面，避免中断bug
            //			logicTarget.SendMessage("OnSelectAssistWindow",gameObject,SendMessageOptions.DontRequireReceiver);

            //记录当前小副本的大副本是否已经开启 by 王明磊
            foreach (MainCopy main in Obj_MyselfPlayer.GetMe().normalMainCopys)
            {
                if (main.copyId == sub_copy.tblCopyDetail.Copyfather && main.copyState == CopyState.PASSED)
                {
                    Debug.Log("Next Main Copy Already Opened");
                    Obj_MyselfPlayer.GetMe().isNextMainOpened = true;
                    break;
                }
            }
            OnSelectAssistWindow();
        }
        else
        {
            Debug.LogError("cur selected subcopy is null !");
        }
    }
    void OnSelectAssistWindow()
    {
        NetworkSender.Instance().askRandomAssistanceList(OnSelectAssistWindowDone);
    }
    void OnSelectAssistWindowDone(bool bSuccess)
    {
        logicTarget.SendMessage("OnSelectAssistWindowDone", true);

    }

    public void BuySthDone(bool isSuccess)
    {
        if (isSuccess)
        {
            logicTarget.SendMessage("refreshTopBar");
            //			BoxManager.showMessage("购买成功!");
            BoxManager.showMessageByID((int)MessageIdEnum.Msg59);
        }
        else
        {
            //在BoxManager.showErrorMsg方法里统一弹窗报错.

            Debug.LogError("购买失败");
        }
    }

    void OnTopLeftBtn()
    {
        Obj_MyselfPlayer.GetMe().copyType = (CopyType)mainCopy.tblCopy.GetCopyTypebyIndex(0);
        logicTarget.SendMessage("LoadPveSceneList");
    }
    //for guide
    private GameObject guideItem = null;
    public GameObject getGuideItem()
    {
        return guideItem;
    }
    public void SetPanelListEnable(bool bEnable)
    {
        dragPanel.enabled = bEnable;
    }
}
