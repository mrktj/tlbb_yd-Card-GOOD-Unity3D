using UnityEngine;
using System.Collections;

public class HpBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public tk2dSprite backGround;
	public tk2dSprite foreGround;
	
	public void SetHpPercent(float percent)
	{
		if(percent >= 1.0f)
		{
			foreGround.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}
		else if(percent <= 0.0f)
		{
			foreGround.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		}
		else
		{
			foreGround.transform.localScale = new Vector3(1.0f, percent, 1.0f);
		}
	}
}
