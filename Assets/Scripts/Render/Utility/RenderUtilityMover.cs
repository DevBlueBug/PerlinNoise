using UnityEngine;
using System.Collections;

public class RenderUtilityMover : RenderUtility
{
	Vector3 posFrom,posTo;

	public void Link(GameObject gObject,Vector3 positionFrom, Vector3 position, float duration){
		this.gObject = gObject;
		posFrom = positionFrom;
		posTo = position;
		On (duration);

	}

	public override void TimerFinished ()
	{
		base.TimerFinished ();
		gObject.transform.position = posTo;
	}
	public override void TimerTick (float ratio)
	{
		base.TimerTick (ratio);
		ratio = (ratio*ratio + (1- ratio*ratio) *1*ratio );
		gObject.transform.position = posFrom + (posTo- posFrom) * ratio;

	}
}

