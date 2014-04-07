using UnityEngine;
using System.Collections;

public class UIRootAdjust : MonoBehaviour {
	
	//UIRoot uiRoot;
	
	void Awake () {
		//uiRoot = this.gameObject.GetComponent<UIRoot>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnEnable () {
		ResizeActiveHeight();
	}
	
	void ResizeActiveHeight () {
#if UNITY_IPHONE
//		if (iPhone.generation == iPhoneGeneration.iPhone5) {
//			uiRoot.manualHeight = 1136;
//		}
#endif
	}
}
