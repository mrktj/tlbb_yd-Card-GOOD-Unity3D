using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using Module.Log;
//using System.Linq;

public class PVEMainController : MonoBehaviour
{

    public GameObject GridNormal;
    public GameObject GridActivity;
    public GameObject dungeonListPanel;
    public UIScrollBar scrollBar;
    public UISprite title;
    public GameObject activeBtn; //TODO 在这里加二级按钮
    public UIDraggablePanel dragPanel;

    private GameObject logicTarget;
    private string mainCopyNormalItem = "CopyMainNormalItem";

    public CopyType curActive;

    private List<MainCopy> normalCopys;
    private List<MainCopy> activityCopys;
    private Vector3 openLevPo = new Vector3(-260, -40, 0);
    private Vector3 closeLevPo = new Vector3(-260, -5, 0);
    private string normalMainCopy_Key = "NormalMainCopy_Key";
    private string activeMainCopy_Key = "ActiveMainCopy_Key";
    private string SubCopy_Key = "SubCopy_Key";
    private int openNormalCount = 0;
    private int openActiveCount = 0;
    void Start()
    {
        logicTarget = GameObject.Find("MainUILogic");
        InitCopys();

        Obj_MyselfPlayer.GetMe().UpdateCopyList(CopyType.NORMAL);
        normalCopys = Obj_MyselfPlayer.GetMe().normalMainCopys;
        CreateCopyList(CopyType.NORMAL);
        Obj_MyselfPlayer.GetMe().UpdateCopyList(CopyType.ACTIVITY);
        activityCopys = Obj_MyselfPlayer.GetMe().activityMainCopys;
        CreateCopyList(CopyType.ACTIVITY);
        EnalbleCopy(Obj_MyselfPlayer.GetMe().copyType);

        if (Obj_MyselfPlayer.GetMe().isLastBattleNotFinish)  //如果是战斗中断返回的那场战斗,需要以下代码处理跳转
        {
            Obj_MyselfPlayer.GetMe().isLastBattleNotFinish = false;
            int curMainCopyID = Obj_MyselfPlayer.GetMe().curSubcopy.tblCopyDetail.Copyfather; //中断的战斗的大副本ID
            curMainCopyID--; //在List里从0开始,因此CopyID需要减1

            int nextSubCopy = Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID + 1;
            Debug.Log("NextCopyID : " + nextSubCopy);
            List<SubCopy> subCopys = Obj_MyselfPlayer.GetMe().normalCopys;
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
                GameObject go = this.transform.FindChild("PanelList/GridNormal/" + curMainCopyID.ToString()).gameObject;
                if (go != null)
                    OnSelectMainCopy(go);
                return;
            }
            Debug.Log("GetMe().curSubcopy.tblCopyDetail.Copyfather = " + Obj_MyselfPlayer.GetMe().curSubcopy.tblCopyDetail.Copyfather);
            if (nextSubDetail.Copyfather == Obj_MyselfPlayer.GetMe().curSubcopy.tblCopyDetail.Copyfather)
                isMainCopyOpen = false;
            if (!isMainCopyOpen) //没有大副本开启
            {
                Debug.Log("No Main Copy New Open");
                GameObject go = this.transform.FindChild("PanelList/GridNormal/" + curMainCopyID.ToString()).gameObject;
                if (go != null)
                    OnSelectMainCopy(go);
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
                                GameObject goCopy = this.transform.FindChild("PanelList/GridNormal/" + curMainCopyID.ToString()).gameObject;
                                if (goCopy != null)
                                    OnSelectMainCopy(goCopy);
                                return;
                            }
                            else if (mainCopy.copyId == 2) //如果是第二个大副本,并且是首次打开
                            {
                                Debug.Log("No.2 Main New Open");
                                if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.GIFT)
                                {
                                    GuideManager.Instance.isInSubStep = false;
                                }
                                logicTarget.SendMessage("ReturnToMainUI"); //首次打开第二大副本 返回主界面
                                return;
                            }
                            else
                                curMainCopyID++;
                            break;
                        }
                    }
                }
                Debug.Log("Level < Request || Other Main Copy New Open");
                Transform go = this.transform.FindChild("PanelList/GridNormal/" + curMainCopyID.ToString());
                if (go != null)
                    OnSelectMainCopy(go.gameObject);
            }
        }
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
    void OnEnable()
    {
        if (logicTarget == null)
        {
            return;
        }
        scrollBar.scrollValue = 0;
        openNormalCount = 0;
        openActiveCount = 0;
        Obj_MyselfPlayer.GetMe().UpdateCopyList(CopyType.NORMAL);
        normalCopys = Obj_MyselfPlayer.GetMe().normalMainCopys;
        CreateCopyList(CopyType.NORMAL);
        Obj_MyselfPlayer.GetMe().UpdateCopyList(CopyType.ACTIVITY);
        activityCopys = Obj_MyselfPlayer.GetMe().activityMainCopys;
        CreateCopyList(CopyType.ACTIVITY);
        EnalbleCopy(Obj_MyselfPlayer.GetMe().copyType);

    }
    void OnDisable()
    {
        RecordScrollValue();
    }
    void InitCopys()
    {
        Obj_MyselfPlayer.GetMe().normalMainCopys.Clear();
        Obj_MyselfPlayer.GetMe().activityMainCopys.Clear();
        Hashtable copy_table = TableManager.GetCopy();
        foreach (DictionaryEntry pair in copy_table)
        {
            MainCopy copy = new MainCopy((int)pair.Key);
            if (copy.tblCopy.GetCopyTypebyIndex(0) == (int)CopyType.NORMAL)
            {
                Obj_MyselfPlayer.GetMe().normalMainCopys.Add(copy);
            }
            else
            {
                Obj_MyselfPlayer.GetMe().activityMainCopys.Add(copy);
            }
        }
    }

    private void CreateCopyList(CopyType type)
    {
        GameObject prompt = activeBtn.transform.FindChild("PromptAni").gameObject;
        prompt.SetActive(false);

        GameObject parent = GridNormal;
        if (type == CopyType.ACTIVITY)
        {
            parent = GridActivity;
        }

        int count = parent.transform.childCount;
        for (int i = count - 1; i >= 0; i--)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
        parent.GetComponent<UIGrid>().repositionNow = true;

        if (type == CopyType.NORMAL)
        {
            for (int i = normalCopys.Count - 1; i >= 0; i--)
            {
                bool canOpen = false;
                if (normalCopys[i].copyState == CopyState.UNOPEN)
                {
                    if (i == 0)
                    {
                        canOpen = true;
                    }
                    else if (normalCopys[i - 1].copyState == CopyState.PASSED)
                    {
                        canOpen = true;
                    }
                    //mainCopy.subCopys[i].copyState = CopyState.OPENED;
                }
                else if (normalCopys[i].copyState == CopyState.PASSED)
                {
                    canOpen = true;
                }
                //				bool isNewOpen = false;
                //				if(normalCopys[i].copyState == CopyState.UNOPEN)
                //				{
                //					isNewOpen = true;
                //					normalCopys[i].copyState = CopyState.OPENED;
                //				}
                normalCopys[i].listOrder = i;
                if (canOpen)
                {
                    GameObject new_item = ResourceManager.Instance.loadWidget(mainCopyNormalItem);
                    new_item.transform.parent = parent.transform;
                    new_item.transform.localPosition = Vector3.zero;
                    new_item.transform.localScale = Vector3.one;
                    new_item.transform.name = normalCopys[i].listOrder.ToString();

                    //副本缩略图
                    UISprite coypIcon = new_item.transform.FindChild("Sprite-bg").GetComponent<UISprite>();
                    string iconName = TableManager.GetCopyByID(normalCopys[i].copyId).Thumb;
                    coypIcon.spriteName = iconName;
                    //掉落描述
                    UILabel dropDesc = new_item.transform.FindChild("Labels/DropDescribe").GetComponent<UILabel>();
                    dropDesc.gameObject.SetActive(false);

                    //name
                    UILabel label_name = new_item.transform.FindChild("Labels/Label-Name-Value").GetComponent<UILabel>();
                    label_name.text = LanguageManger.GetWords(normalCopys[i].tblCopy.Copyname);

                    //设置名字为"无量山遇险"的item为clone obj
                    Debug.Log("name:" + normalCopys[i].tblCopy.Copyname);
                    if (normalCopys[i].tblCopy.Copyname == 11000)
                    {
                        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_1 ||
                                GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_2 ||
                                GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_3 ||
                                GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_4 ||
                                GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_5)
                        {
                            this.guideItem = new_item;
                        }
                    }
                    else if (normalCopys[i].tblCopy.Copyname == 11001)
                    {
                        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY2_1)
                        {
                            this.guideItem = new_item;
                        }
                    }

                    //state
                    UILabel label_state = new_item.transform.FindChild("Labels/Label-State-Value").GetComponent<UILabel>();
                    if (normalCopys[i].copyState == CopyState.PASSED)
                    {
                        label_state.text = "[d4a106]已通关[000000]";
                    }
                    else
                    {
                        label_state.text = "[89ca88]新开启[000000]";
                    }

                    //level
                    GameObject goLev = new_item.transform.FindChild("Level").gameObject;
                    UILabel label_level = goLev.transform.FindChild("Label-Level-Value").GetComponent<UILabel>();
                    GameObject lockTip = new_item.transform.FindChild("Sprite-CanOpen").gameObject;
                    if (normalCopys[i].tblCopy.PlayerLevel > 0)
                    {
                        if (Obj_MyselfPlayer.GetMe().level < normalCopys[i].tblCopy.PlayerLevel)
                        {
                            label_level.text = normalCopys[i].tblCopy.PlayerLevel.ToString();
                            goLev.transform.localPosition = closeLevPo;
                            lockTip.SetActive(true);
                        }
                        else
                        {
                            label_level.text = normalCopys[i].tblCopy.PlayerLevel.ToString();
                            goLev.transform.localPosition = openLevPo;
                            lockTip.SetActive(false);
                        }
                        goLev.SetActive(true);
                    }
                    else
                    {
                        goLev.SetActive(false);
                    }

                    //time
                    UILabel label_time = new_item.transform.FindChild("Labels/Label-Time").GetComponent<UILabel>();
                    label_time.gameObject.SetActive(false);
                    label_time = new_item.transform.FindChild("Labels/Label-Time-Value").GetComponent<UILabel>();
                    label_time.gameObject.SetActive(false);
                    GameObject name_bg = new_item.transform.FindChild("Sprite").gameObject;
                    if (Obj_MyselfPlayer.GetMe().level >= normalCopys[i].tblCopy.PlayerLevel)
                    {
                        label_name.text = "[F1ECCF]" + LanguageManger.GetWords(normalCopys[i].tblCopy.Copyname) + "[000000]";
                        label_state.gameObject.SetActive(true);
                        label_name.transform.localPosition = new Vector3(-160, -15, -20);
                        name_bg.transform.localPosition = new Vector3(-105, -14, -2);
                        UIEventListener.Get(new_item).onClick += OnSelectMainCopy;
                    }
                    else
                    {
                        label_name.text = "[7F7F7F]" + LanguageManger.GetWords(normalCopys[i].tblCopy.Copyname) + "[000000]";
                        label_state.gameObject.SetActive(false);
                        label_name.transform.localPosition = new Vector3(-160, -5, -20);
                        name_bg.transform.localPosition = new Vector3(-105, -5, -2);
                        Destroy(new_item.GetComponent<UIImageButton>());
                    }
                    openNormalCount++;
                }
                //				if(isNewOpen)
                //				{
                //					break;
                //				}
            }
        }
        else
        {
            activityCopys.Sort(ActiveCompareTo);
            for (int i = 0; i < activityCopys.Count; i++)
            {
                if (activityCopys[i].clostTime == 0)
                {
                    continue;
                }
                activityCopys[i].listOrder = i;

                GameObject new_item = ResourceManager.Instance.loadWidget(mainCopyNormalItem);
                new_item.transform.parent = parent.transform;
                new_item.transform.localPosition = Vector3.zero;
                new_item.transform.localScale = Vector3.one;
                new_item.transform.name = activityCopys[i].listOrder.ToString();

                //副本缩略图
                UISprite coypIcon = new_item.transform.FindChild("Sprite-bg").GetComponent<UISprite>();
                string iconName = TableManager.GetCopyByID(activityCopys[i].copyId).Thumb;
                coypIcon.spriteName = iconName;

                //name
                UILabel label_name = new_item.transform.FindChild("Labels/Label-Name-Value").GetComponent<UILabel>();
                //label_name.text = LanguageManger.GetWords(activityCopys[i].tblCopy.Copyname);

                //state
                UILabel label_state = new_item.transform.FindChild("Labels/Label-State-Value").GetComponent<UILabel>();
                if (activityCopys[i].startTime == 0)
                {
                    label_state.text = "[89ca88]开启中[000000]";
                    foreach (SubCopy sub in activityCopys[i].subCopys)
                    {
                        int copyCount = TableManager.GetCopydetailByID(sub.subCopyID).CopyLimit - sub.count;
                        if (copyCount > 0)
                        {
                            prompt.SetActive(true);
                            break;
                        }
                    }
                }
                else if (activityCopys[i].clostTime == 0)
                {
                    label_state.text = "[c83809]未开启[000000]";
                }

                //level
                GameObject goLev = new_item.transform.FindChild("Level").gameObject;
                UILabel label_level = goLev.transform.FindChild("Label-Level-Value").GetComponent<UILabel>();
                GameObject lockTip = new_item.transform.FindChild("Sprite-CanOpen").gameObject;
                if (activityCopys[i].tblCopy.PlayerLevel > 0)
                {
                    if (Obj_MyselfPlayer.GetMe().level < activityCopys[i].tblCopy.PlayerLevel)
                    {
                        label_level.text = activityCopys[i].tblCopy.PlayerLevel.ToString();
                        goLev.transform.localPosition = closeLevPo;
                        lockTip.SetActive(true);
                    }
                    else
                    {
                        label_level.text = activityCopys[i].tblCopy.PlayerLevel.ToString();
                        goLev.transform.localPosition = openLevPo;
                        lockTip.SetActive(false);
                    }
                    goLev.SetActive(true);
                }
                else
                {
                    goLev.SetActive(false);
                }
                //time
                UILabel label_time = new_item.transform.FindChild("Labels/Label-Time").GetComponent<UILabel>();
                UILabel label_time_value = new_item.transform.FindChild("Labels/Label-Time-Value").GetComponent<UILabel>();
                if (activityCopys[i].startTime == 0)
                {
                    label_time.text = "关闭剩余：";
                    label_time_value.text = ConvertSecond(activityCopys[i].clostTime);
                    UIEventListener.Get(new_item).onClick += OnSelectMainCopy;

                }
                else if (activityCopys[i].clostTime == 0)
                {
                    label_time.text = "开启剩余：";
                    label_time_value.text = ConvertSecond(activityCopys[i].startTime);
                    //new_item.GetComponent<UIImageButton>().enabled=false;
                    Destroy(new_item.GetComponent<UIImageButton>());
                }

                //掉落描述
                UILabel dropDesc = new_item.transform.FindChild("Labels/DropDescribe").GetComponent<UILabel>();
                int dropID = activityCopys[i].tblCopy.Drop;
                if (dropID != -1)
                {
                    UILabel level = new_item.transform.FindChild("Labels/DropDescribe").GetComponent<UILabel>();
                    Tab_Language language = TableManager.GetLanguageByID(dropID);
                    level.text = language.Chinese;
                }
                GameObject name_bg = new_item.transform.FindChild("Sprite").gameObject;
                if (Obj_MyselfPlayer.GetMe().level >= activityCopys[i].tblCopy.PlayerLevel)
                {
                    label_name.text = "[F1ECCF]" + LanguageManger.GetWords(activityCopys[i].tblCopy.Copyname) + "[000000]";
                    label_state.gameObject.SetActive(true);
                    label_time.gameObject.SetActive(true);
                    label_time_value.gameObject.SetActive(true);
                    dropDesc.gameObject.SetActive(true);
                    label_name.transform.localPosition = new Vector3(-160, -15, -20);
                    name_bg.transform.localPosition = new Vector3(-105, -14, -2);
                }
                else
                {
                    label_name.text = "[7F7F7F]" + LanguageManger.GetWords(activityCopys[i].tblCopy.Copyname) + "[000000]";
                    label_state.gameObject.SetActive(false);
                    label_time.gameObject.SetActive(false);
                    label_time_value.gameObject.SetActive(false);
                    dropDesc.gameObject.SetActive(false);
                    label_name.transform.localPosition = new Vector3(-160, -5, -20);
                    name_bg.transform.localPosition = new Vector3(-105, -5, -2);
                    if (new_item.GetComponent<UIImageButton>() != null)
                    {
                        Destroy(new_item.GetComponent<UIImageButton>());
                        UIEventListener.Get(new_item).onClick -= OnSelectMainCopy;
                    }
                }
                openActiveCount++;
            }
        }
        parent.GetComponent<UIGrid>().repositionNow = true;
    }

    //排序算法
    static public int CompareTo(MainCopy copyA, MainCopy copyB)
    {
        if (copyB.startTime != copyA.startTime)
        {
            //开始时间
            return (-1 * copyB.startTime.CompareTo(copyA.startTime));
        }
        else if (copyB.tblCopy.PlayerLevel != copyA.tblCopy.PlayerLevel)
        {
            //副本等级
            return (-1 * copyB.tblCopy.PlayerLevel.CompareTo(copyA.tblCopy.PlayerLevel));
        }
        else
        {
            //副本id
            return (-1 * copyB.copyId.CompareTo(copyA.copyId));
        }
    }

    //活动副本排序
    static public int ActiveCompareTo(MainCopy copyA, MainCopy copyB)
    {
        if (copyB.tblCopy.PlayerLevel != copyA.tblCopy.PlayerLevel)
        {
            return (-1 * copyB.tblCopy.PlayerLevel.CompareTo(copyA.tblCopy.PlayerLevel));
        }
        else if (copyB.startTime != copyA.startTime)
        {
            //开始时间
            return (-1 * copyB.startTime.CompareTo(copyA.startTime));
        }
        else if (copyB.tblCopy.PlayerLevel != copyA.tblCopy.PlayerLevel)
        {
            //副本等级
            return (-1 * copyB.tblCopy.PlayerLevel.CompareTo(copyA.tblCopy.PlayerLevel));
        }
        else
        {
            //副本id
            return (-1 * copyB.copyId.CompareTo(copyA.copyId));
        }
    }

    private void EnalbleCopy(CopyType type)
    {
        scrollBar.scrollValue = 0;
        switch (type)
        {
            case CopyType.ACTIVITY:
                title.spriteName = "选择活动";
                title.MakePixelPerfect();
                activeBtn.SetActive(false);
                GridActivity.SetActive(true);
                GridNormal.SetActive(false);
                break;
            case CopyType.NORMAL:
                title.spriteName = "选择副本";
                title.MakePixelPerfect();
                activeBtn.SetActive(true);
                GridNormal.SetActive(true);
                GridActivity.SetActive(false);
                break;
            default:
                break;
        }
        curActive = type;
        StartCoroutine(SetScrollValue());
    }

    public IEnumerator SetScrollValue()
    {
        yield return 0;
        FreshBar();
    }

    public void FreshBar()
    {
        if (curActive == CopyType.NORMAL)
        {
            if (Obj_MyselfPlayer.GetMe().scrollRecord.ContainsKey(normalMainCopy_Key))
            {
                if (openNormalCount > 4)
                {
                    scrollBar.scrollValue = Obj_MyselfPlayer.GetMe().scrollRecord[normalMainCopy_Key].scrollValue;
                }
                else
                {
                    scrollBar.scrollValue = 0;
                }
            }
        }
        else
        {
            if (Obj_MyselfPlayer.GetMe().scrollRecord.ContainsKey(activeMainCopy_Key))
            {
                if (openActiveCount > 4)
                {
                    scrollBar.scrollValue = Obj_MyselfPlayer.GetMe().scrollRecord[activeMainCopy_Key].scrollValue;
                }
                else
                {
                    scrollBar.scrollValue = 0;
                }
            }
        }
    }

    public void OnBtnActivity()
    {
        ScrollData scData = new ScrollData(scrollBar.scrollValue);
        Obj_MyselfPlayer.GetMe().SetScrollValue(normalMainCopy_Key, scData);
        EnalbleCopy(CopyType.ACTIVITY);
    }

    public void OnBtnNormal()
    {
        EnalbleCopy(CopyType.NORMAL);
    }

    public void OnSelectMainCopy(GameObject go)
    {
        switch (GuideManager.Instance.currentStep)
        {
            case GuideManager.GUIDE_STEP.COPY1_1:
                GuideCopy1_1.Instance.NextStep();//新手战斗引导 SELECT_2
                break;
            case GuideManager.GUIDE_STEP.COPY1_2:
                GuideCopy1_2.Instance.NextStep();//新手战斗引导 SELECT_2
                break;
            case GuideManager.GUIDE_STEP.COPY1_3:
                GuideCopy1_3.Instance.NextStep();//新手战斗引导 SELECT_2
                break;
            case GuideManager.GUIDE_STEP.COPY1_4:
                GuideCopy1_4.Instance.NextStep();//新手战斗引导 SELECT_2
                break;
            case GuideManager.GUIDE_STEP.COPY1_5:
                GuideCopy1_5.Instance.NextStep();//新手战斗引导 SELECT_2
                break;
            case GuideManager.GUIDE_STEP.COPY2_1:
                GuideCopy2_1.Instance.NextStep();//新手战斗引导 SELECT_2
                break;
            default:
                break;
        }

        int order = int.Parse(go.name);
        MainCopy mcopy = null;
        if (curActive == CopyType.NORMAL)
        {
            foreach (MainCopy copy in normalCopys)
            {
                if (copy.listOrder == order)
                {
                    mcopy = copy;
                    break;
                }
            }
        }
        else
        {
            foreach (MainCopy copy in activityCopys)
            {
                if (copy.listOrder == order)
                {
                    mcopy = copy;
                    break;
                }
            }
        }
        if (mcopy != null)
        {
            Obj_MyselfPlayer.GetMe().curMainCopy = mcopy;
            logicTarget.SendMessage("LoadPveBossList");
        }
        else
        {
            Debug.LogError("OnSelectMainCopy(), copy is null");
        }
        //点击后清除小副本记录位置
        ScrollData scDatasub1 = new ScrollData(0);
        Obj_MyselfPlayer.GetMe().SetScrollValue(SubCopy_Key, scDatasub1);
    }

    //记录scrollvalue值
    public void RecordScrollValue()
    {
        if (curActive == CopyType.NORMAL)
        {
            ScrollData scData = new ScrollData(scrollBar.scrollValue);
            Obj_MyselfPlayer.GetMe().SetScrollValue(normalMainCopy_Key, scData);
        }
        else
        {
            ScrollData scData = new ScrollData(scrollBar.scrollValue);
            Obj_MyselfPlayer.GetMe().SetScrollValue(activeMainCopy_Key, scData);
        }
    }

    public void OnTopLeftBtn()
    {
        if (curActive == CopyType.NORMAL)
        {
            logicTarget.GetComponent<MainUILogic>().ReturnToMainUI();
        }
        else
        {
            ScrollData scData = new ScrollData(scrollBar.scrollValue);
            Obj_MyselfPlayer.GetMe().SetScrollValue(activeMainCopy_Key, scData);
            EnalbleCopy(CopyType.NORMAL);
        }
    }
    private string ConvertSecond(int second)
    {
        int day = 0;
        int hour = 0;
        int miu = 0;

        string ret = "";

        if (second <= 0)
        {
            return "1分钟";
        }

        day = second / (24 * 60 * 60);
        if (day > 0)
        {
            ret = day.ToString() + "天";
            return ret;
        }

        hour = second / (60 * 60);
        if (hour > 0)
        {
            ret = hour.ToString();
            return ret + "小时";
        }

        miu = second / 60;
        if (miu > 1)
        {
            ret = miu.ToString() + "分钟";
            return ret;
        }
        else
        {
            return "1分钟";
        }
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
