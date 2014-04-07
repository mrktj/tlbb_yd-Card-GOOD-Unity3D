using UnityEngine;
using System.Collections;

public class ObjHeightScale : MonoBehaviour
{
#if UNITY_ANDROID
	public const int RES_SCALE_Y = 1136;
	public const int RES_SCALE_Y1 = 1152;
	public const int RES_SCALE_Y2 = 1024;
	public const int DES_SCALE_Y = 1280;
	public const int DES_COLL_SCALE_Y = 1680;
	private float scaleX;
	private float scaleZ;
	// Update is called once per frame
	void Update() {
		// GameObject 1136 to 1280
        Object[] objs = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objs)
        { 
			if(go.transform.localScale.y >= RES_SCALE_Y2 && go.transform.localScale.y < DES_SCALE_Y)
			{
				scaleX = go.transform.localScale.x;
				scaleZ = go.transform.localScale.z;
				go.transform.localScale = new Vector3(scaleX, DES_SCALE_Y, scaleZ);
				//Debug.Log("objname: "+go.name);
			}
        }

		// BoxCollider 1136 to 1280
		/*Object[] colls = FindObjectsOfType(typeof(BoxCollider));
        foreach (BoxCollider coll in colls)
        { 
			if(coll.size.y >= RES_SCALE_Y2 && coll.size.y <= RES_SCALE_Y1)
			{
				scaleX = coll.size.x;
				scaleZ = coll.size.z;
				coll.size = new Vector3(scaleX, DES_COLL_SCALE_Y, scaleZ);
			}
        }*/
	}
#endif
}
