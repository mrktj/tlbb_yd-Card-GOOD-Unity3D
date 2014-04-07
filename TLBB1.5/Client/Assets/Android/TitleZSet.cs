using UnityEngine;
using System.Collections;

 class TitleZSet : MonoBehaviour
{
#if UNITY_ANDROID
	public float z = -15.0f;
	void Start(){
		float x = transform.localPosition.x;
		float y = transform.localPosition.y;
		transform.localPosition = new Vector3(x,y,z);
	}
	/*void LateUpdate () {
		float x = transform.localPosition.x;
		float y = transform.localPosition.y;
		transform.localPosition = new Vector3(x,y, -15);
		isSet = true;
	}*/
#endif
}
