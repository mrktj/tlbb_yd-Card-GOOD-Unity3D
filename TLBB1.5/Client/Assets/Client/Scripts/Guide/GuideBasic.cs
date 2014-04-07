using UnityEngine;
using System.Collections;
using Games.CharacterLogic;

public class GuideBasic : MonoBehaviour {

	public GuidePlayer player;	
	public GameObject keyObject = null;//关键对象，指某步骤突出显示部分
	public MainUILogic mainLogic;
	
	public int curstep;
	public int labelIndex;
	
	public GameObject tempDraggableGO = null;
	
	public float waitSecond = 0.02f;
	
	//初始化guide基类基本数据
	public void InitGuide(GuidePlayer gp, MainUILogic mul){
		player = gp;
		mainLogic = mul;
		labelIndex = 0;
		curstep = 0;
	}
	public void SetKeyObj(GameObject go){
		keyObject = go;
	}
	
	
	private UIPanel up;
	private bool newpanel;

	//关键对象使用前更新
	public void UpdateKeyObject(){
		
		keyObject.transform.localPosition += new Vector3(0,0,-300);//坐标前移
		
		up = keyObject.GetComponent<UIPanel>();
		newpanel = up==null ? true : false;
		if(up == null)
			up = keyObject.AddComponent<UIPanel>();//获取panel
		
		UIDragPanelContents uidpc = keyObject.GetComponent<UIDragPanelContents>();
		if(uidpc != null)
			uidpc.enabled = false;//关闭dragpanel
		
		UIButtonOffset ubo = keyObject.GetComponent<UIButtonOffset>();
		if(ubo != null)
			Destroy(ubo);
		
		keyObject.SetActive(false);
		keyObject.SetActive(true);
	}
	//关键对象使用后更新
	public void RecoverKeyObject(){
		
		if(keyObject == null)
			return;
		
		keyObject.transform.localPosition += new Vector3(0,0,300);//坐标还原
		
		if(newpanel && up != null)
			Destroy(keyObject.GetComponent<UIPanel>());//销毁panel

		UIDragPanelContents uidpc = keyObject.GetComponent<UIDragPanelContents>();
		if(uidpc != null)
			uidpc.enabled = true;//重启dragpanel
		
		keyObject = null;
	}
	
	//提升指定对象Z坐标
	public void UpdateGameObject(GameObject go){
		go.transform.localPosition += new Vector3(0,0,-300);//坐标前移
		UIPanel up = go.GetComponent<UIPanel>();
		if(up == null)
			up = go.AddComponent<UIPanel>();//获取panel
		go.SetActive(false);
		go.SetActive(true);
	}
	public void RecoverGameObject(GameObject go){
		go.transform.localPosition += new Vector3(0,0,300);//坐标前移
		UIPanel up = go.GetComponent<UIPanel>();
		if(up != null)
			Destroy(up);//获取panel
		go.SetActive(false);
		go.SetActive(true);
	}
	//设置对话框
	public void ShowLabel(int teamNum , string sLabel){
		int templateID = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(Obj_MyselfPlayer.GetMe().teamMemberArray[teamNum]).templateID;
		if(Obj_MyselfPlayer.GetMe().teamMemberArray[teamNum] == null)
			Debug.LogWarning("*** GUIDE team "+teamNum+" is null");
		player.SetPlayer(templateID);		//设置对话者
		player.ShowLabel_temp(sLabel);		//设置对话内容
		player.SetZhaoActive(true);			//显示对话
	}
	public void ShowLabel(int teamNum , int iLabel){
		int templateID = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(Obj_MyselfPlayer.GetMe().teamMemberArray[teamNum]).templateID;
		player.SetPlayer(templateID);
		player.ShowLabel(iLabel);
		player.SetZhaoActive(true);
	}
}