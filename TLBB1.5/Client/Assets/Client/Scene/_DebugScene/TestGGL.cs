using UnityEngine;
using System.Collections;

public class TestGGL : MonoBehaviour {
	
	
	public GameObject[] items;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	bool CheckIfItemIOpen(GameObject obj)
	{
		return !obj.transform.FindChild("Mask").gameObject.activeSelf;
	}
	
	void OnGGLClick(GameObject btn){
		if(CheckIfItemIOpen(btn))
		{
			return;
		}
		
		
		btn.transform.FindChild("Mask").gameObject.SetActive(false);
	}
	
	void OnGGLTouchMoveIn(GameObject btn){
		if(CheckIfItemIOpen(btn))
		{
			return;
		}
		
		
		btn.transform.FindChild("Mask").gameObject.SetActive(false);
	}
}
