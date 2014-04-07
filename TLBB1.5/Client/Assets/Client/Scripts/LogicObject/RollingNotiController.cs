using UnityEngine;
using System.Collections;

public class RollingNotiController : MonoBehaviour {
	
	private bool b_position = false;
	private bool b_alpha = false;
	
	private float duration;
	
	private Vector3 v_from;
	private Vector3 v_to;
	
	private float f_from;
	private float f_to;
	
	private Vector3 p_delta;
	private float a_delta;
	
	private float startTime = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(b_position)
		{
			if(startTime <= duration)
			{
				gameObject.transform.localPosition += p_delta;
			}
			else
			{
				b_position = false;
			}
		}
		else if(b_alpha)
		{
			if(startTime <= duration)
			{
				gameObject.GetComponent<UISprite>().alpha += a_delta;
			}
			else
			{
				b_alpha = false;
			}
		}
		
		startTime += Time.deltaTime;
	}
	
	public void PlayPosition(Vector3 f, Vector3 t, float dut){
		//gameObject.transform.localPosition = f;
		v_to = t;
		v_from = f;
		duration = dut;
		
		startTime = 0;
		b_position = true;
		p_delta = new Vector3(((v_to.x - v_from.x)/(duration/Time.deltaTime)), ((v_to.y - v_from.y)/(duration/Time.deltaTime)), ((v_to.z - v_from.z)/(duration/Time.deltaTime)));
		Debug.Log("x:"+p_delta.x+";y:"+p_delta.y+";z:"+p_delta.z);
		//p_delta = (v_to - v_from) / (duration * Time.deltaTime);
	}
	
	public void PlayAlpha(float f, float t, float dut){
		//gameObject.GetComponent<UISprite>().alpha = f;
		f_to = t;
		f_from = f;
		duration = dut;
		
		startTime = 0;
		b_alpha = true;
		a_delta = (f_to - f_from) / (duration / Time.deltaTime);
		Debug.Log("alpha:"+a_delta);
	}
}
