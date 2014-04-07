using UnityEngine;
using System.Collections;

public class FlashColor : MonoBehaviour {

	void Update () {
		renderer.material.SetColor("_TintColor",new Color (0.43f,0.37f,0,(Mathf.Sin(Time.time)+ 1)/2*0.1f));
	}
}
