using UnityEngine;
using System.Collections;
public static class RenderUtilityEasy
{
	
	
	static public RenderUtilityMover GetMover(GameObject me, Vector3 posFrom, Vector3 posTo, float duration){
		var obj = new GameObject ().AddComponent<RenderUtilityMover> ();
		obj.Link(me, posFrom, posTo, duration);
		return obj;
	}
	static public RenderUtilityScaler GetScaler(GameObject me, Vector3 scaleFrom, Vector3 scaleTo, float duration){
		var obj = new GameObject ().AddComponent<RenderUtilityScaler> ();
		obj.Link(me, scaleFrom, scaleTo, duration);
		return obj;
	}
}