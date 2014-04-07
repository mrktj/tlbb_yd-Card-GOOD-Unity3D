using UnityEngine;
using System.Collections;

public class iTweenHandler : MonoBehaviour {

    public delegate void ProjectileFlyComplete(GameObject go);

    public ProjectileFlyComplete ProjectileFlyCompleteHandler;

	// Use this for initialization
	void Start () {
	
	}
	

    public void OnProjectileFlyComplete()
    {
        if (ProjectileFlyCompleteHandler != null)
        {
            ProjectileFlyCompleteHandler(gameObject);
        }

        // EventManager.Instance.Fire(EventDefine.SKILL_PROJECTILE_FLY_COMPLETE, gameObject);
    }
}
