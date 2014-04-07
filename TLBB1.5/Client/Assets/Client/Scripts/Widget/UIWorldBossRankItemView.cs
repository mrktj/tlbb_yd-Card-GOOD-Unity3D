using UnityEngine;
using System.Collections;

public class UIWorldBossRankItemView : MonoBehaviour
{
    public UISprite topPrompt;
    public UILabel rank;
    public UILabel name;
    public UILabel damage;

    public void InitItem(WorldBossDamageRankInfoClass rankInfo)
    {
        switch (rankInfo.rank)
        {
            case 1:
                topPrompt.spriteName = "金箭头";
                topPrompt.gameObject.SetActive(true);
                break;
            case 2:
                topPrompt.spriteName = "银箭头";
                topPrompt.gameObject.SetActive(true);
                break;
            case 3:
                topPrompt.spriteName = "铜箭头";
                topPrompt.gameObject.SetActive(true);
                break;
            default:
                topPrompt.gameObject.SetActive(false);
                break;
        }
        transform.localPosition = new Vector3(0, 0, -1);
        transform.localScale = new Vector3(1, 1, 1);
        rank.text = rankInfo.rank.ToString();
        name.text = rankInfo.name;
        damage.text = rankInfo.totalDamage.ToString();
    }
}
