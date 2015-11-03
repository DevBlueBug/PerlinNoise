using UnityEngine;
using System.Collections.Generic;

public class RenderTalkingCube : MonoBehaviour
{
	static float SPEED_ROTATION = 200.0f;
	public enum KState{Disabled, ClickMe, Talking,TalkingFinishing};

	public RenderFlash S_Flash;
	public Dels.V_V E_ClickMeNotify = delegate {};
	public List<MeshRenderer> meshes;

	KState meState = KState.ClickMe;
	Rigidbody body;
	Vector3 torqueSpin = Vector3.one;
	float 
		timeelapsed_ClickMeNotify = 0,
		timeElapsed_ClickMeMax = .5f;


	// Use this for initialization
	void Start ()
	{
		body = GetComponent<Rigidbody> ();
		E_ClickMeNotify += delegate {
			S_Flash.On (timeElapsed_ClickMeMax *.8f);
		};
	}
	public void SetState(KState state){
		switch (state) {
		case KState.ClickMe:
			timeelapsed_ClickMeNotify = 0;
			meState = state;
			break;
		case KState.Talking:
		case KState.TalkingFinishing: 
			meState = state;break;
		case KState.Disabled:
			if(meState == KState.Talking){
				meState = KState.TalkingFinishing;
			}
			else meState = state;
			break;
		}

	}
	void Update(){
		switch (meState) {
		case KState.Disabled:
			break;
		case KState.ClickMe:
			timeelapsed_ClickMeNotify+= Time.deltaTime;
			if(timeelapsed_ClickMeNotify > timeElapsed_ClickMeMax ){
				timeelapsed_ClickMeNotify=0;
				E_ClickMeNotify();
			}
			break;
		case KState.Talking:
			this.transform.localRotation = Quaternion.Euler (0, this.transform.localRotation.eulerAngles.y + SPEED_ROTATION * Time.deltaTime, 0);
			break;
		case KState.TalkingFinishing:
			//stopping to pause
			this.transform.localRotation = Quaternion.Euler(
				transform.localRotation.eulerAngles +
				(new Vector3(0,360,0) - transform.localRotation.eulerAngles)* 1f*Time.deltaTime );
			float dis = (new Vector3(0,360,0) - transform.localRotation.eulerAngles).sqrMagnitude;
			if(dis < .01){
				meState = KState.Disabled;
			}
			break;
		}

	}
	// Update is called once per frame
	//

}

