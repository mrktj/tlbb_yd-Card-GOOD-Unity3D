using UnityEngine;
using System.Collections;

using Games.LogicObject;
using GCGame.Table;
using card.net;
using Games.CharacterLogic;

#if UNITY_ANDROID
using card;
#endif

public class GuidePlayer : MonoBehaviour {
	
	public GameObject playerBG;
	public GameObject playerZhao;
	public GameObject playerRow;
	public GameObject guideCopyM;
	public UILabel playerZhaoLabel;
	
	public UILabel playerName;
	public UITexture playerPic;
	
	public UITexture ruanxingzhu;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//--------------------------总接口：显示、隐藏当前页--------------------------
	public void SetPlayerActive(bool act){
		if(act){
			SetZhaoActive(false);
			SetRowActive(false);
			gameObject.SetActive(act);
		}else
			gameObject.SetActive(act);
	}
	//--------------------------接口：显示、隐藏赵天师--------------------------
	public void SetZhaoActive(bool act){
		playerZhao.SetActive(act);
		SetRowActive(!act);//赵天师显示时隐藏箭头，反之亦然
	}
	//--------------------------接口：显示、隐藏指引箭头--------------------------
	public void SetRowActive(bool act){
		playerRow.SetActive(act);
	}
	//--------------------------接口：显示、隐藏赵天师和指引箭头--------------------------
	public void DisableRowZhao(){
		SetZhaoActive(false);
		SetRowActive(false);
	}
	//--------------------------接口：阶段性指引结束隐藏全部界面--------------------------
	public void GuideStepEnd(){
		SetZhaoActive(false);
		SetRowActive(false);
		SetPlayerActive(false);
	}
	//--------------------------接口：居中设置箭头位置--------------------------
	public void SetRowPosition(Vector3 v){
		playerRow.transform.localPosition = v;
		playerRow.transform.localScale = Vector3.one;
		this.SetRowActive(true);
	}
	public void SetRowPositionS(GameObject keyObject){//自身
		Vector3 col	=	keyObject.GetComponent<BoxCollider>().size;
		Vector3 coc	=	keyObject.GetComponent<BoxCollider>().center;
		this.SetRowPosition(new Vector3(0 ,col.y/2 ,0) + coc + keyObject.transform.localPosition);
	}
	public void SetRowPositionPS(GameObject keyObject){//父节点坐标相关
		Vector3 col	=	keyObject.GetComponent<BoxCollider>().size;
		Vector3 coc	=	keyObject.GetComponent<BoxCollider>().center;
		Vector3 pa	=	keyObject.transform.parent.localPosition;
		this.SetRowPosition(new Vector3(0 ,col.y/2 ,0) + coc + pa + keyObject.transform.localPosition);
	}
	public void SetRowPositionGS(GameObject keyObject){//祖父节点坐标相关
		Vector3 col	=	keyObject.GetComponent<BoxCollider>().size;
		Vector3 coc	=	keyObject.GetComponent<BoxCollider>().center;
		Vector3 gpa	=	keyObject.transform.parent.parent.localPosition;
		this.SetRowPosition(new Vector3(0 ,col.y/2 ,0) + coc + gpa + keyObject.transform.localPosition);
	}
	public void SetRowPositionGPS(GameObject keyObject){//祖父与父节点相关
		Vector3 col	=	keyObject.GetComponent<BoxCollider>().size;
		Vector3 coc	=	keyObject.GetComponent<BoxCollider>().center;
		Vector3 pa	=	keyObject.transform.parent.localPosition;
		Vector3 gpa	=	keyObject.transform.parent.parent.localPosition;
		this.SetRowPosition(new Vector3(0 ,col.y/2 ,0) + coc + pa + gpa + keyObject.transform.localPosition);
	}
	//----------------------------END------------------------
	
	//----------------------------显示新手提示语------------------------------
	public void ShowLabel(int labelID){
		string str = LanguageManger.GetWords(labelID);
        if (GuideManager.Instance.selectedTempletID != 0)
        {
            Tab_Card card = TableManager.GetCardByID(GuideManager.Instance.selectedTempletID);
            if (card != null)
            {
                if (TableManager.GetAppearanceByID(card.Appearance) != null)
                {
                    str = string.Format(str, LanguageManger.GetWords(TableManager.GetAppearanceByID(card.Appearance).Name));
                }
            }          
        }
        else
        {
            Debug.LogWarning("*** ****** Guide show label warning : selected Templet ID = 0!");
        }
#if UNITY_ANDROID
        //str = str.Replace(" ", "");
        Utils.EraseColorStrSpace(ref str);
#endif
		playerZhaoLabel.text = str;
	}
	public void ShowLabel_temp(string str){
		playerZhaoLabel.text = str;
	}
	//----------------------------END------------------------
	
	public void ButtonNextStep(){
		if(nextStep == null)
			return;
		nextStep();
	}
	
	public delegate void NextStep();	
	public NextStep nextStep;
	
	//战斗引导显示阵型调整接口
	public void PlayCopyGuide(){
		string picName = TableManager.GetAppearanceByID(TableManager.GetCardByID(1190/*阮星竹TemplateID*/).Appearance).BodyIcon;	
		AtlasManager.Instance.SetBodyName(ruanxingzhu , picName);
		guideCopyM.SetActive(true);
	}
	public void StopCopyGuide(){
		guideCopyM.SetActive(false);
	}
	
	public void SetPlayer(int tempID){
		string picName;
//		if(tempID == 1008 || tempID == 1012 || tempID == 1016)
			picName = TableManager.GetAppearanceByID(TableManager.GetCardByID(tempID).Appearance).HetiIcon;
//		else
//			picName = TableManager.GetAppearanceByID(TableManager.GetCardByID(tempID).Appearance).BodyIcon;
		AtlasManager.Instance.SetBodyName(playerPic , picName);
		playerName.text = LanguageManger.GetWords( TableManager.GetAppearanceByID(TableManager.GetCardByID(tempID).Appearance).Name );
	}
	public void SetZhaoPlayer(){
		string picName = "xinshouyindao_zhaotianshi_renwu";
		AtlasManager.Instance.SetBodyName(playerPic , picName);
		playerName.text = "赵天师";
	}
	
	public void SetZhaoPosition1(){
		playerZhao.transform.FindChild("label").localPosition = new Vector3(0, -100, 0);
	}
	public void SetZhaoPosition2(){
		playerZhao.transform.FindChild("label").localPosition = new Vector3(0, 150, 0);
	}
}
