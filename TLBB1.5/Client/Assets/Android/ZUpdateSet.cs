using UnityEngine;
using System.Collections;

 class ZUpdateSet : MonoBehaviour
{
#if UNITY_ANDROID
	void LateUpdate () {
		float x = transform.localPosition.x;
		float y = transform.localPosition.y;
		transform.localPosition = new Vector3(x,y, -15);
	}
#endif
}
