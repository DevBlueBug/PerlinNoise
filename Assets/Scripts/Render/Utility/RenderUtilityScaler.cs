using UnityEngine;
using System.Collections;

public class RenderUtilityScaler : RenderUtility
{
	Vector3 scaleFrom, scaleTo;
	public void Link(GameObject gObject, Vector3 scale, Vector3 scaleTo, float duration){
		this.gObject = gObject;
		scaleFrom = scale;
		this.scaleTo = scaleTo;
		On (duration);
	}
	public override void TimerFinished ()
	{
		base.TimerFinished ();
		gObject.transform.localScale = scaleTo;
	}
	public override void TimerTick (float ratio)
	{
		base.TimerTick (ratio);
		ratio = (ratio*ratio + (1- ratio*ratio) *1.5f*ratio );
		gObject.transform.localScale = scaleFrom + (scaleTo - scaleFrom )*ratio;
	}
}

