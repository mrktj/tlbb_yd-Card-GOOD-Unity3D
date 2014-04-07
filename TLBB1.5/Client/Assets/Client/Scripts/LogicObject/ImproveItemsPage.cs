using UnityEngine;
using System.Collections;

public class ImproveItemsPage : MonoBehaviour {
	
	public enum JianTouMode
	{
		PageMini,  //页数最小
		Normal,    //在中间
		PageMax,   //页数最大
	}
	
	//标记向左，向右拖拽 Sprite
    public GameObject JianTouLeft;          //----向左的箭头
    public GameObject JianTouLeftHui;       //----向左的灰态箭头
    public GameObject JianTouRight;         //----向右的箭头
    public GameObject JianTouRightHui;      //----向右的灰态箭头
	
	//----拖拽界面（要拖拽的界面）
    public GameObject goDragPanel;
	
	//当前页面
    private int curPage = 1;
    //最大页
    private int maxPage = 5;
	
	// Use this for initialization
	void Start () {
		if (goDragPanel != null)
	    {
            goDragPanel.GetComponent<UIDraggablePanel>().onDragFinished += OnDragonItemFinished;
	    }
	}
	
	void OnEnable ()
	{
		if (JianTouLeft == null
               || JianTouLeftHui  == null
                   || JianTouRight == null
                       || JianTouRightHui == null)
        {
            return;
        }
		if (curPage <= 1)
		{
			curPage = 1;
			JianTouLeft.SetActive(false);
			JianTouLeftHui.SetActive(true);
			JianTouRight.SetActive(true);
			JianTouRightHui.SetActive(false);
		}
		if (curPage >= maxPage)
		{
			curPage = maxPage;
			JianTouRight.SetActive(false);
			JianTouRightHui.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public void OnDragonItemFinished(UIDraggablePanel.DragonMode dragonMode)
    {
		int[] lastItem = transform.GetComponent<ImproveController>().lastSelected;
		curPage = int.Parse(transform.FindChild("Graph/Grid").gameObject.GetComponent<UICenterOnChild>().centeredObject.name);
        //默认点击的风水阵
		string path;
		switch (lastItem[curPage-1])
		{
			case 2:
				path = "Graph/Grid/" + curPage + "/left_pic";
				transform.GetComponent<ImproveController>().LeftClicked(transform.FindChild(path).gameObject);
				break;
			case 3:
				path = "Graph/Grid/" + curPage + "/right_pic";
				transform.GetComponent<ImproveController>().RightClicked(transform.FindChild(path).gameObject);
				break;
			case 4:
				path = "Graph/Grid/" + curPage + "/bottom_pic";
				transform.GetComponent<ImproveController>().BottomClicked(transform.FindChild(path).gameObject);
				break;
			default :
				path = "Graph/Grid/" + curPage + "/top_pic";
				transform.GetComponent<ImproveController>().TopClicked(transform.FindChild(path).gameObject);
				break;
		}
		if (JianTouLeft == null
               || JianTouLeftHui  == null
                   || JianTouRight == null
                       || JianTouRightHui == null) {
            return;
        }
		if(maxPage == 1){ 
			return;
		}
		
		if (curPage <= 1) {
			JianTouLeft.SetActive(false);
			JianTouLeftHui.SetActive(true);
			JianTouRight.SetActive(true);
			JianTouRightHui.SetActive(false);
		}else if (curPage >= maxPage) {
			JianTouLeft.SetActive(true);
			JianTouLeftHui.SetActive(false);
			JianTouRight.SetActive(false);
			JianTouRightHui.SetActive(true);
		}else {
			JianTouLeft.SetActive(true);
			JianTouLeftHui.SetActive(false);
			JianTouRight.SetActive(true);
			JianTouRightHui.SetActive(false);
		}
	}
	
}
