using UnityEngine;
using System;
using System.Collections;

public class BattleFrameUI : MonoBehaviour {
	public GameObject go;
#if UNITY_ANDROID
	//private int count;
	//private float x;
	//private float y;
	//private float z;
	void Start()
	{
		go.SetActive(false);
		StartCoroutine("doit");
		//count = 0;
		//x = go.transform.localPosition.x;
		//y = go.transform.localPosition.y;
		//z = go.transform.localPosition.z;
		//go.transform.localPosition = new Vector3(x, y, 100);
	}
	/*
	void Update ()
	{
		count++;
		if(count == 30){
			go.SetActiveRecursively(true);
			//go.transform.localPosition = new Vector3(x, y, z);
		}
	}
	*/
	IEnumerator doit()
	{
		yield return new WaitForSeconds(0.2f);
		go.SetActive(true);
	}
#endif
}
