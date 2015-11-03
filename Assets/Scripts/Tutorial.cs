using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tutorial : MonoBehaviour
{
	public KEntity bttnStart;
	public RenderTalkingCube talkingCube;
	public RenderText_SelfType P_Title;
	public PatternLogic P_PatternLogic;

	public AudioSource 
		dialog00_intro,
		dailog01,
		Sound00_PhaseReady,
		Sound00_Intro,
		Sound01_BeaconsAppear,
		Sound02_BeaconsReady;
	public List<AudioSource>
		Sounds01_BoxsAppear;


	// Use this for initialization
	void Start ()
	{
		PrepareScene00 ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	void TalkingCube_ClickMe(){
		
		talkingCube.SetState (RenderTalkingCube.KState.ClickMe);
		Sound00_PhaseReady.Play ();
	}
	void PrepareScene00(){
		//TalkingCube_ClickMe ();
		bttnStart.E_Pressed += Scene00_Start;
	}
	void PrepareScene01(){
		bttnStart.E_Pressed += Scene01_Start;
	}
	void PrepareScene02(){
		P_PatternLogic.E_StateLocked -= PrepareScene02;
		StartCoroutine (PrepareScene02_Coroutine ());
	}
	IEnumerator PrepareScene02_Coroutine(){
		yield return new WaitForSeconds (1.0f);
		Sound02_BeaconsReady.Play ();
		yield return new WaitForSeconds (1.0f+Sound02_BeaconsReady.clip.length);
		TalkingCube_ClickMe ();
		talkingCube.E_ClickMeNotify += Scene02_Start;
	}
	void Scene00_Start(){
		bttnStart.E_Pressed -= Scene00_Start;
		StartCoroutine (Scene00_Couroutine ());
	}
	void Scene01_Start(){
		bttnStart.E_Pressed -= Scene01_Start;
		StartCoroutine (Scene01_Couroutine ());
	}
	void Scene02_Start(){
	}
	IEnumerator Scene00_Couroutine(){
		talkingCube.SetState ( RenderTalkingCube.KState.Talking);
		dialog00_intro.Play ();
		yield return new WaitForSeconds (dialog00_intro.clip.length);
		talkingCube.SetState (RenderTalkingCube.KState.Disabled);

		float timePlayed = Sound00_Intro.clip.length *.8f;
		Sound00_Intro.Play ();
		yield return new WaitForSeconds (.5f);
		P_Title.On (timePlayed);
		yield return new WaitForSeconds (timePlayed + 1.0f);
		TalkingCube_ClickMe ();
		PrepareScene01 ();
	}
	IEnumerator Scene01_Couroutine(){
		RenderUtilityEasy.GetMover (talkingCube.gameObject, talkingCube.transform.position, new Vector3 (5.2f, -2, 8), .5f);
		talkingCube.SetState (RenderTalkingCube.KState.Talking);
		yield return new WaitForSeconds (.5f);

		RenderUtilityEasy.GetMover (P_Title.gameObject, P_Title.transform.position, new Vector3(0,8,8), .2f);
		dailog01.Play ();
		yield return new WaitForSeconds (dailog01.clip.length);
		talkingCube.SetState (RenderTalkingCube.KState.Disabled);
		//now bring out the example game
		P_PatternLogic.Init00 ();

		for (int i = 0; i< P_PatternLogic.boxs.Count; i++) {
			P_PatternLogic.boxs[i].transform.localScale = Vector3.zero;
		}
		for (int i = 0; i < P_PatternLogic.beacons.Count; i++) {
			//P_PatternLogic.beacons[i].gameObject.SetActive(false);
			P_PatternLogic.beacons[i].transform.localScale = Vector3.zero;
		}

		for (int i = 0; i< P_PatternLogic.boxs.Count; i++) {
			RenderUtilityEasy.GetScaler(P_PatternLogic.boxs[i].gameObject, Vector3.zero,Vector3.one, .75f);
			Sounds01_BoxsAppear[i%Sounds01_BoxsAppear.Count].Play();
			yield return new WaitForSeconds (.5f);
		}
		yield return new WaitForSeconds (.5f);
		Sound01_BeaconsAppear.Play();
		for (int i = 0; i < P_PatternLogic.beacons.Count; i++) {
			//P_PatternLogic.beacons[i].gameObject.SetActive(true);
			RenderUtilityEasy.GetScaler(P_PatternLogic.beacons[i].gameObject,Vector3.zero,new Vector3(2,2,2),1);
		}
		P_PatternLogic.E_StateLocked += PrepareScene02;

		yield return null;
	}
}

