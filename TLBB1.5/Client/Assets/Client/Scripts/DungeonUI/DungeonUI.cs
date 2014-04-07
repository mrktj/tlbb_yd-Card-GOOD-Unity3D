using UnityEngine;
using System.Collections;

public class DungeonUI : MonoBehaviour {
	
	public GameObject dungeonListItem;
	public GameObject GridNormal;
	public GameObject GridActivity;
	public GameObject GridElite;
	public GameObject dungeonListPanel;
    public GameObject loadtarget;
	//活动
	private const int GRID_ACTIVITY = 1;
	//普通
	private const int GRID_NORMAL = 0;
	//精英
	private const int GRID_ELITE = 2;
	
	private int curActive = -1;
    private string[] temporaryscenename;

    // Use this for initialization
	void Awake()
	{
        temporaryscenename = new string[10];
        temporaryscenename[0] = "火眼骷髅";
        temporaryscenename[1] = "骷髅舞会";
        temporaryscenename[2] = "蚂蚁王";
        temporaryscenename[3] = "蚁巢";
        temporaryscenename[4] = "狗男女";
        temporaryscenename[5] = "血色守卫者";
        temporaryscenename[6] = "旋风男";
        temporaryscenename[7] = "血色军部队";
        temporaryscenename[8] = "血法师";
        temporaryscenename[9] = "元素之怒";

        CreateList(GRID_ELITE);
        CreateList(GRID_NORMAL);
        CreateList(GRID_ACTIVITY);
	}

	void Start () 
    {
		EnalbleGrid(GRID_ELITE);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
	
	public void OnBtnActivity()
	{
		EnalbleGrid(GRID_ACTIVITY);
	}
	
	public void OnBtnElite()
	{
		EnalbleGrid(GRID_ELITE);
	}
	
	public void OnBtnNormal()
	{
		EnalbleGrid(GRID_NORMAL);
	}
	
	private void CreateList(int type)
	{
		GameObject parent = GridNormal;
        if (type == GRID_ACTIVITY)
        {
            parent = GridActivity;
        }
        else if (type == GRID_ELITE)
        {
            parent = GridElite;
        }

		for(int i=0; i<10; i++)
		{
			GameObject newItem =  (GameObject)Instantiate(dungeonListItem);
			newItem.transform.parent = parent.transform;
			newItem.transform.position = new Vector3(0, 0, 0);
			newItem.transform.localScale = new Vector3(1, 1, 1);
			UILabel label = newItem.transform.FindChild("Label").GetComponent<UILabel>();
            label.text = temporaryscenename[i];
            UIButtonMessage loadmsg = newItem.GetComponent<UIButtonMessage>();
            loadmsg.target = loadtarget;
            loadmsg.functionName = "LoadBattleUIScene";
		}
		parent.GetComponent<UIGrid>().repositionNow = true;
	}
	
	private void EnalbleGrid(int grid)
	{
		if(curActive == grid)
		{
			return;
		}
		switch(grid)
		{
		case GRID_ACTIVITY:
			GridActivity.SetActive(true);
			GridNormal.SetActive(false);
			GridElite.SetActive(false);
			break;
			
		case GRID_ELITE:
			GridElite.SetActive(true);
			GridNormal.SetActive(false);
			GridActivity.SetActive(false);
			break;
			
		case GRID_NORMAL:
			GridNormal.SetActive(true);
			GridElite.SetActive(false);
			GridActivity.SetActive(false);
			break;
		}
		curActive = grid;
	}
}
