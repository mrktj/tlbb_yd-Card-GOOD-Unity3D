using UnityEngine;
using System.Collections;

public class SkillController : MonoBehaviour
{
    private GameObject mainLogic;
    public UITexture card1;
    public UITexture card2;
    // Use this for initialization
    void Start()
    {
        mainLogic = GameObject.Find("MainUILogic");
        AtlasManager.Instance.SetBodyName(card1, "heti_duanyu_5");
        AtlasManager.Instance.SetBodyName(card2, "heti_qiaofeng_5");
    }

    /// <summary>
    /// 回主界面
    /// </summary>
    private void ReturnToMainUI()
    {
        mainLogic.SendMessage("ReturnToMainUI");
    }

    /// <summary>
    /// 切换到技能学习窗口
    /// </summary>
    private void ToSkillLearnWindow()
    {
        mainLogic.SendMessage("OnSkillLearnWindow");
    }

    /// <summary>
    /// 切换到技能升级窗口
    /// </summary>
    private void ToSkillUpdateWindow()
    {
        mainLogic.SendMessage("OnSkillUpdateWindow");
    }
}
