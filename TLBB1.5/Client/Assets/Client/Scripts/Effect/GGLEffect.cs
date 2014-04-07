using UnityEngine;
using System.Collections;

public class GGLEffect : MonoBehaviour {
	
	
	public 	Animation objEffect02;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ShowNexteffect()
	{
		if(objEffect02 != null)
		{
			objEffect02.gameObject.SetActive(true);
			objEffect02.Play("gglEffect02");
		}
		
		gameObject.transform.parent.SendMessage("EffectCom");
		gameObject.SetActive(false);
	}
}
