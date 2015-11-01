using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
	public AudioSource SRC_PlayerWalked;
	public List<AudioSource> SRC_TextTyped;
	public AudioSource SRC_Intro;

	public PlayerCamera P_PlayerCamera;
	public RenderText_SelfType P_TextSelfType;

	// Use this for initialization
	void Awake(){
		P_PlayerCamera.E_PlayerWalked += delegate {
			SRC_PlayerWalked.Play();

		};
		P_TextSelfType.E_Typed += delegate {
			SRC_TextTyped[(int)Random.Range(0,SRC_TextTyped.Count)].Play();
		};
	}
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

