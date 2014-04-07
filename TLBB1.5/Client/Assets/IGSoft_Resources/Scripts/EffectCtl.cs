using UnityEngine;
using System.Collections;

public class EffectCtl : MonoBehaviour
{
	
	public GameObject[] Effects;
	public float[] DelayTime;
	private int number;
	private int Eindex,Tindex;
	
	// Use this for initialization
	void Start ()
	{
		number = Effects.Length;
		Eindex = 0;
		Tindex = 0;
		if (number > 0) {
			for (int count = 0; count < number; count ++) 
			{
				if (Tindex < DelayTime.Length -1)
					Tindex += 1;
				else
					Tindex = 0;
				Invoke ("IniEffect", DelayTime [Tindex]);
			}
		} else {
			Debug.LogError ("没有指定特效预制物体");
		}
		
			
			
	
	}
	
	void IniEffect ()
	{
		GameObject go;
		go = Instantiate (Effects [Eindex])as GameObject;
		go.transform.parent = transform;
		go.transform.localPosition = Vector3.zero;
		if (Eindex < number)
			Eindex += 1;
		else
			Eindex = 0;
	}
}
