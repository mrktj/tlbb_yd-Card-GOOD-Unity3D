using UnityEngine;
using System.Collections;

public class BattleResult : MonoBehaviour {
	
	public GameObject defeat;
	public GameObject victory;
	
	// Use this for initialization
	void Start () {
	
	}
	
	
	public void ShowResult(bool is_win)
	{
		if(is_win)
		{
			victory.transform.localPosition = new Vector3(21f, 0f, 0f);
			defeat.transform.localPosition = new Vector3(25f, 0f, 0f);
			
			victory.SetActive(true);
			defeat.SetActive(false);
		}
		else
		{
			victory.transform.localPosition = new Vector3(21f, 0f, 0f);
			defeat.transform.localPosition = new Vector3(25f, 0f, 0f);
			
			defeat.SetActive(true);
			victory.SetActive(false);
		}
	}
}
