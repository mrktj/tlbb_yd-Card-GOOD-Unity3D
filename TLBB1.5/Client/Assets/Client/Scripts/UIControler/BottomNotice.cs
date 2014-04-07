using UnityEngine;
using System.Collections;

public class BottomNotice : MonoBehaviour {
	
	
	public GameObject mainController;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnEnable(){
		mainController.transform.GetComponent<RollingNotice>().Init();
	}
	
	public void StepStay(){
		mainController.transform.GetComponent<RollingNotice>().StepStay();
	}
	public void StepNone(){
		mainController.transform.GetComponent<RollingNotice>().StepNone();;
	}
}
