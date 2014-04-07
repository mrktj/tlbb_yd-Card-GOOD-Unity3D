using UnityEngine;
using System.Collections;

 class TitleDepthSet : MonoBehaviour
{
#if UNITY_ANDROID
	public int Depth = -1000;
	void Start(){
        if (Depth > -1000)
        {
            transform.GetComponent<UISprite>().depth =  Depth;
        }
    }
#endif
}
