//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// All children added to the game object with this script will be repositioned to be on a grid of specified dimensions.
/// If you want the cells to automatically set their scale based on the dimensions of their content, take a look at UITable.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Grid")]
public class UIGrid : MonoBehaviour
{
	public enum Arrangement
	{
		Horizontal,
		Vertical,
	}

	public Arrangement arrangement = Arrangement.Horizontal;
	public int maxPerLine = 0;
	public float cellWidth = 200f;
	public float cellHeight = 200f;
	public bool repositionNow = false;
	public bool sorted = false;
	public bool hideInactive = true;

    //判断是否用缓动效果
    public bool bUseTween = false;

    //用到缓动移动时 的间距
    public int nTweenWidth = 80;
	
	//Item缓动用的时间
	public float fTweenTime = 0.3f;

    //缓动的个数(默认为5个)
    public int nTweenItemNum = 5;

	bool mStarted = false;

	void Start ()
	{
		mStarted = true;
		Reposition();
	}

	void Update ()
	{
		if (repositionNow)
		{
			repositionNow = false;
			Reposition();
		}
	}

	static public int SortByName (Transform a, Transform b) { return string.Compare(a.name, b.name); }

	/// <summary>
	/// Recalculate the position of all elements within the grid, sorting them alphabetically if necessary.
	/// </summary>

	public void Reposition ()
	{
		if (!mStarted)
		{
			repositionNow = true;
			return;
		}

		Transform myTrans = transform;

		int x = 0;
		int y = 0;

		if (sorted)
		{
			List<Transform> list = new List<Transform>();

			for (int i = 0; i < myTrans.childCount; ++i)
			{
				Transform t = myTrans.GetChild(i);
				if (t && (!hideInactive || NGUITools.GetActive(t.gameObject))) list.Add(t);
			}
			list.Sort(SortByName);

			for (int i = 0, imax = list.Count; i < imax; ++i)
			{
				Transform t = list[i];

				if (!NGUITools.GetActive(t.gameObject) && hideInactive) continue;

				float depth = t.localPosition.z;
				t.localPosition = (arrangement == Arrangement.Horizontal) ?
					new Vector3(cellWidth * x, -cellHeight * y, depth) :
					new Vector3(cellWidth * y, -cellHeight * x, depth);

				if (++x >= maxPerLine && maxPerLine > 0)
				{
					x = 0;
					++y;
				}
			}
		}
		else
		{
			for (int i = 0; i < myTrans.childCount; ++i)
			{
				Transform t = myTrans.GetChild(i);
				if(!t.gameObject.activeSelf)
				{
					continue;
				}

				if (!NGUITools.GetActive(t.gameObject) && hideInactive) continue;


                //根据业务需求，暂时只做
				/*
                if (bUseTween && i < nTweenItemNum && arrangement == Arrangement.Vertical)
                {
                    float depth = t.localPosition.z;
                    int nItemWidth = nTweenWidth * (i + 1);
// 					if(t.transform.GetComponent<UITweener>() != null)
// 					{
// 						Destroy(t.transform.GetComponent<UITweener>())
// 					}

//                     UITweener uiTweener = t.transform.gameObject.AddComponent<UITweener>();
//                     uiTweener.style = UITweener.Style.Once;
//                     uiTweener
                    t.localPosition = new Vector3(cellWidth * y, -cellHeight * x, depth);
                    Vector3 vec3Postion = new Vector3(t.position.x, t.position.y, t.position.z);
					//Vector3 vec3Postion = new Vector3(0, 0, t.position.z);
                    t.localPosition = new Vector3(cellWidth * y + nItemWidth, -cellHeight * x, depth);

                    Hashtable hash = new Hashtable();
                    hash.Add("time", fTweenTime);
                    hash.Add("position", vec3Postion);
                    hash.Add("easeType", iTween.EaseType.linear);


                    //iTween.MoveTo(t.gameObject, vec3Postion, 0.5f*(i+1));
                    iTween.MoveTo(t.gameObject, hash);
//                     iTween itween = t.GetComponent<iTween>();
//                     if (itween != null)
//                     {
//                         itween.easeType = iTween.EaseType.linear;
//                     }
                    //Debug.Log(-cellHeight * x);
                    //t.gameObject.AddComponent<iTween>();
                }
                else
                {
                */
                    float depth = t.localPosition.z;
                    t.localPosition = (arrangement == Arrangement.Horizontal) ?
                        new Vector3(cellWidth * x, -cellHeight * y, depth) :
                        new Vector3(cellWidth * y, -cellHeight * x, depth);

//                     int nItemWidth = nTweenWidth * (i + 1);
//                     Debug.Log(cellWidth * y + nItemWidth);
               //}
				

				if (++x >= maxPerLine && maxPerLine > 0)
				{
					x = 0;
					++y;
				}
			}
		}

		UIDraggablePanel drag = NGUITools.FindInParents<UIDraggablePanel>(gameObject);
		if (drag != null) drag.UpdateScrollbars(true);
	}

    //移除所有子obj
	/*
    public void RemoveAllChildObj()
    {
        Transform myTrans = transform;
        int nCount = myTrans.GetChildCount();
        for (int i=0; i<nCount; i++)
        {
            Transform childTrans = myTrans.GetChild(i);
            childTrans.parent = null;
            Destroy(childTrans.gameObject);
        }
       
    }
    */
}