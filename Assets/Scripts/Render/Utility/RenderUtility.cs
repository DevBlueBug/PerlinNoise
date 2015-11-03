using UnityEngine;
using System.Collections;

public class RenderUtility : MonoBehaviour
{
	internal bool 
		isSelfDestructive =true,
		isLoop = false;
	Utility.EasyTimer timer;
	internal GameObject gObject;

	// Use this for initialization
	void Awake ()
	{
		enabled = false;
	
	}
	internal void On(float duration){
		timer = new Utility.EasyTimer (0, duration);
		enabled = true;

	}
	public virtual void TimerFinished(){
	}
	public virtual void TimerTick(float timerRatio){
	}
	// Update is called once per frame
	void Update ()
	{
		if (timer.Tick (Time.deltaTime)) {
			TimerFinished();
			if(isLoop){
			}
			else {
				enabled = false;
				if(isSelfDestructive && this.gameObject != null){
					Destroy(this.gameObject);
				}
			}
			
		} else {
			TimerTick(timer.Ratio);
			
		}
	
	}
}

