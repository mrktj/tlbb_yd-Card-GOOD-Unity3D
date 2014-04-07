using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour {
	
	private bool isUp = true;
	private bool isShake = false;
	private int mCount = 0;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(!isShake)
		{
			return;
		}
		
		mCount++;
		
		if(mCount == 2)
		{
			mCount = 0;
			if(isUp)
			{
				gameObject.transform.localPosition = new Vector3(0f, 2f, 0f);
			}
			else
			{
				gameObject.transform.localPosition = new Vector3(0f, -2f, 0f);
			}
			isUp = !isUp;
		}
	}
	
	public void StartShake()
	{
		isShake = true;
		isUp = true;
		mCount = 0;
	}
	public void StopShake()
	{
		isShake = false;
	}
}
