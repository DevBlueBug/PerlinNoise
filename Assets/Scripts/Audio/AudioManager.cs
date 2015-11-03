using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
	public AudioSource SRC_PlayerWalked;
	public List<AudioSource> SRC_TextTyped;
	public AudioSource 
		Src_KeyPressd,
		Src_KeyReleased;

	public PlayerCamera P_PlayerCamera;
	public RenderText_SelfType P_TextSelfType;
	public PatternLogic P_PatternLogic;

	// Use this for initialization
	void Awake(){
		P_PlayerCamera.E_PlayerWalked += delegate {
			SRC_PlayerWalked.Play();

		};
		P_TextSelfType.E_Typed += delegate {
			SRC_TextTyped[(int)Random.Range(0,SRC_TextTyped.Count)].Play();
		};
		if (P_PatternLogic != null) {
			P_PatternLogic.E_BoxClicked += delegate{
				Src_KeyPressd.Play();
			};
			P_PatternLogic.E_BoxReleased += delegate{
				Src_KeyReleased.Play();
			};
		}
	}
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

