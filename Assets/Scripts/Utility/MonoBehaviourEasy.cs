using UnityEngine;
using System.Collections;

public class MonoBehaviourEasy : MonoBehaviour
{
	RenderUtilityMover mover;

	public virtual void Awake ()
	{
		mover = this.gameObject.AddComponent<RenderUtilityMover>();
		mover.isSelfDestructive = false;
	
	}
	public void Move(Vector3 posFrom, Vector3 posTo, float duration){
		mover.Link (this.gameObject, posFrom, posTo, duration);

	}
}

