using UnityEngine;
using System.Collections;

public class RenderTalkingCube : MonoBehaviour
{
	static float SPEED_ROTATION = 200.0f;
	Rigidbody body;
	Vector3 torqueSpin = Vector3.one;
	int state = 0;

	// Use this for initialization
	void Start ()
	{
		body = GetComponent<Rigidbody> ();

	
	}
	public void On(){
		state = 1;
	}
	public void Off(){
		state = 2;
	}
	void Update(){
		if (state == 1) {
			this.transform.localRotation = Quaternion.Euler (0, this.transform.localRotation.eulerAngles.y + SPEED_ROTATION * Time.deltaTime, 0);
		} else if(state==2){
			//this.transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles * (1- 1f*Time.deltaTime) );
			this.transform.localRotation = Quaternion.Euler(
				transform.localRotation.eulerAngles +
				(new Vector3(0,360,0) - transform.localRotation.eulerAngles)* 1f*Time.deltaTime );
			float dis = (new Vector3(0,360,0) - transform.localRotation.eulerAngles).sqrMagnitude;
			if(dis < .01){
				state = 0;
			}
		}

	}
	// Update is called once per frame
	//

}

