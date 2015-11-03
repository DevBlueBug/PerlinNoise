using UnityEngine;
using System.Collections;

public class PatternBox : MonoBehaviourEasy
{
	public enum KState {FREE, LOCKED};
	public delegate void D_V_Me(PatternBox me);

	public D_V_Me E_GotHit = delegate {};
	public Collider C_Collider;
	public MeshRenderer C_MeshRenderer;
	public TextMesh C_Text;
	public PatternBeacon beaconLockedAt;
	
	KState meState = KState.FREE;

	internal int 
		id = -1;
	internal Vector3 
		posInit = Vector3.zero;

	public KState State{
		get{ return meState;}
	}

	public void Init(int id,Color color, Vector3 posInit){
		this.C_MeshRenderer.material.color = color;
		C_Text.text = ""+id;
		this.id = id;
		this.posInit = posInit;
		transform.localPosition = posInit;
	}
	public void SetFree(){
		meState = KState.FREE;
		Move (transform.position, posInit, .5f);
	}
	public void SetLock(PatternBeacon beacon){
		meState = KState.LOCKED;
		beaconLockedAt = beacon;
		Move (this.transform.position, beacon.transform.position, .5f);
	}

	public override void Awake ()
	{
		base.Awake ();
		//RenderUtilityEasy.GetScaler (this.gameObject, Vector3.zero, Vector3.one, .3f);

	}


}

