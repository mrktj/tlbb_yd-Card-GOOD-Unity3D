using UnityEngine;
using System.Collections;

public class PortraitAttack : MonoBehaviour {
	
	public GameObject obj;
	
	private int movesteps = 0;
	// Use this for initialization
	void Start () {
		Hashtable args = new Hashtable();
		args.Add("time", 0.4);
		args.Add("x", 0.3);
		args.Add("looptype", iTween.LoopType.pingPong);
		args.Add("easetype", iTween.EaseType.linear);
		args.Add("oncompletetarget", this.gameObject);
		args.Add("oncompleteparams", obj);
		args.Add("oncomplete", "MoveAddOver");
		
		movesteps = 0;
		
		iTween.MoveAdd(obj, args);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void MoveAddOver(Object _obj)
	{
		++movesteps;
		if(movesteps >= 2)
		{
			GameObject stopobj = _obj as GameObject;
			if(stopobj != null)
			{
				iTween.Stop(stopobj);
			}
		}
	}
	
}
