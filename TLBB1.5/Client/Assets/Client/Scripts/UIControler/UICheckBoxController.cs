using UnityEngine;
using System.Collections;
//cb: use this to send box self
public class UICheckBoxController : MonoBehaviour {
	public GameObject target;
	public string activeFuncName = "checkboxActive";
	public string inactiveFuncName = "checkboxInactive";
	bool mUsingDelegates = false;

	void Start ()
	{
		UICheckbox chk = GetComponent<UICheckbox>();

		if (chk != null)
		{
			mUsingDelegates = true;
			chk.onStateChange += OnActivateDelegate;
		}
	}

	void OnActivateDelegate (bool isActive)
	{
		if (enabled && target != null) //target.enabled = inverse ? !isActive : isActive;
		{
			if(isActive)
			{
				target.SendMessage(activeFuncName,gameObject,SendMessageOptions.DontRequireReceiver);
			}else
			{
				target.SendMessage(inactiveFuncName,gameObject,SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	/// <summary>
	/// Legacy functionality -- keeping it for backwards compatibility.
	/// </summary>

	void OnActivate (bool isActive) { if (!mUsingDelegates) OnActivateDelegate(isActive); }
}
