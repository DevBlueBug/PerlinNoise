using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
	public KEntity bttnStart;
	public RenderTalkingCube talkingCube;
	public RenderText_SelfType P_Title;

	public AudioSource 
		dialog00_intro,
		Sound00_Intro;


	// Use this for initialization
	void Start ()
	{
		PrepareScene00 ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	void PrepareScene00(){
		bttnStart.E_Pressed += Scene00_Start;

	}
	void Scene00_Start(){
		bttnStart.E_Pressed -= Scene00_Start;
		StartCoroutine (Scene00_Couroutine ());
	}
	IEnumerator Scene00_Couroutine(){
		talkingCube.On ();
		dialog00_intro.Play ();
		yield return new WaitForSeconds (dialog00_intro.clip.length);
		talkingCube.Off ();

		float timePlayed = Sound00_Intro.clip.length *.8f;
		Sound00_Intro.Play ();
		yield return new WaitForSeconds (.5f);
		P_Title.On (timePlayed);
		yield return new WaitForSeconds (timePlayed);
		//return null;
		//press bttn is pressed


	}
	void UpdateFirst00(){

	}
}

