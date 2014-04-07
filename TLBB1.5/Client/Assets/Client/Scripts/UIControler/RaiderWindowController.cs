using UnityEngine;
using System.Collections;

public class RaiderWindowController : MonoBehaviour {
	
	public GameObject mainUILogic;
	
	public GameObject gridItem;
	private string raiderItem = "RaiderItem";
	//可以通过设置数据字典，设置攻略内容
	
    string wuxing = "五行属性\n"+
        "每个侠士都拥有自己的五行属性，五行属性决定卡牌之间的克制关系。\n"+
            "[a5971a]金[000000]克[4d722c]木[000000]、"+
            "[4d722c]木[000000]克[764a1b]土[000000]、"+
            "[764a1b]土[000000]克[195474]水[000000]、"+
            "[195474]水[000000]克[731a14]火[000000]、"+
            "[731a14]火[000000]克[a5971a]金[000000]。"+
            "所谓克敌制胜，使用克制对方的卡牌进行战斗便会产生更大的伤害。\n"+
            "每张卡牌的五行属性在卡牌下方中间通过图表来表示，分别是：\n\n\n\n\n";
    string pinzhi = "侠士品质\n"+
        "侠士品质通过颜色和星级显示，通常可以从卡牌的背景和星级显示中得知，品质从高至低依次是：\n"+
            "[aa9652]金色[000000]、[64352e]朱色[000000]、[794d26]橙色[000000]、[5d5972]紫色[000000]、"+
            "[476568]蓝色[000000]、[647148]绿色[000000]和[6c6c68]灰色[000000]。\n\n";
    string jueji = "独门绝技\n"+
        "每位侠士都有自己的独门绝技。单体攻击、后排群攻、前排群攻、竖排群攻、单体治疗、群体治疗，还有晕眩、施毒等Debuff技能，丰富至极。\n"+
            "玩家可以通过吸收拥有相同独门绝技的侠士卡牌进行独门绝技的升级，大大增加其威力。\n\n";
    string heti="合体技能\n"+
        "某些特别的侠士拥有超强的合体技能，当他们与能触发他们合体技能的侠士同时上阵时就有几率发出绚烂的大招了！\n"+
            "乔峰+阿朱=降龙十八掌\n"+
            "虚竹+银川公主=天山六阳掌\n"+
            "段誉+王语嫣=六脉神剑\n"+
            "游坦之+阿紫=一往情深\n"+
            "萧远山+慕容博=不共戴天\n"+
            "丁春秋+苏星河=逍遥三笑散\n"+
            "刀白凤+段正淳=爱恨交加\n"+
            "谭公+谭婆=逆来顺受\n"+
            "游骥+游驹=兄弟齐心\n\n";
    string duizhangji="队长技能\n"+
        "有些侠士拥有独特的队长技能，当他成为队长时能为团队战斗带来特殊的增益效果。然而如果你的助阵侠士拥有队长技能的话，则会带来额外的增益效果。\n"+
            "以天之名：增加全体暴击几率\n"+
            "逆天而行：减少全体被暴击几率\n"+
            "万箭追心：增加全体命中几率\n"+
            "幻影迷踪：增加全体闪避几率\n"+
            "万力归宗：增加全体攻击力\n"+
            "内功修行：增加全体内功攻击力\n"+
            "外功修行：增加全体外功攻击力\n"+
            "外力抵御：增加全体外功防御\n"+
            "内力化解：增加全体内功防御\n\n";
    string huodong="活动副本\n"+
        "定时开启的活动副本每次开启时限定副本次数，会掉落大量经验卡牌、强化道具和精进道具。\n\n";
	string jiebai="八拜之交\n"+
		"和玩家结成八拜之交，并在每天第一次选择其一同进行战斗时可以获得10点侠义点数。也可以和八拜之交互赠体力，每日一次，同样也是10点侠义点数。侠义点数可以用来参与侠义抽奖，获得更多惊喜卡牌。\n\n";
	
	public static string raiderurl = "about:blank";
	
	void Awake(){
		mainUILogic = GameObject.Find("MainUILogic");
#if UNITY_IPHONE
		
#elif UNITY_ANDROID
		
#else
		GameObject newItem =  ResourceManager.Instance.loadWidget(raiderItem);//(GameObject)Instantiate(friendListItem);
		newItem.transform.parent = gridItem.transform;
		newItem.transform.localPosition = new Vector3(0, 0, -1);
		newItem.transform.localScale = new Vector3(1, 1, 1);	
		newItem.transform.FindChild("Label").GetComponent<UILabel>().text = wuxing + pinzhi + jueji + heti + duizhangji + huodong + jiebai;
		float line_label_1 = newItem.transform.FindChild("Label").GetComponent<UILabel>().effectDistance.y;
				
		gridItem.GetComponent<UIGrid>().repositionNow = true;
#endif
	}
	
	void OnEnable(){
		//KDTLWebView.Instance.SetWebView();
#if UNITY_IPHONE
		KDTLWebView.ShowWebView(raiderurl, KDTLWebView.webViewMode.RAIDER);
		gameObject.SetActive(false);
#elif UNITY_ANDROID	
		KDTLWebView.ShowWebView(raiderurl, KDTLWebView.webViewMode.RAIDER);
		gameObject.SetActive(false);	
#endif
	}
	void OnDisable(){
	}
	
	public void Cancel(){
		mainUILogic.SendMessage("OnHelpWindow");
	}
}
