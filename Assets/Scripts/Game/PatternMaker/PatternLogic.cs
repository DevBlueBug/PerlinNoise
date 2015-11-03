using UnityEngine;
using System.Collections.Generic;

public class PatternLogic : MonoBehaviour
{
	public static float POSITION_Z = 8;
	public static float BEACON_ACTIVE_DISTANCE_SQRT = 1.0f;
	
	public enum KState{WaitingForInput, Processing, Locked};
	public Dels.V_V E_BoxClicked = delegate {},
					E_BoxReleased = delegate {},
					E_StateLocked = delegate {	};


	public PatternBox P_Box;
	public PatternBeacon P_Beacon;
	
	public List<PatternBox> boxs;
	public List<PatternBeacon> beacons;
	KState meState = KState.WaitingForInput;

	PatternBox boxSelected = null;

	void Start(){
		//Init00 ();
	}

	public void Init00(){
		boxs = new List<PatternBox> ();
		//boxsPosInit = new List<Vector3> ();
		Color[] colors = new Color[] {Color.red,new Color(1,.5f,0),Color.yellow, Color.green, Color.blue,new Color(1,0,.5f), new Color(1,0,1)};
		float size = 1.5f;
		for (int i = 0; i < 7; i++) {
			var boxNew = Instantiate(P_Box);
			boxNew.transform.parent = this.transform;
			boxNew.Init( 1+i,colors[i], new Vector3(-size * (7/2) + size*i, 5,POSITION_Z));
			//boxsPosInit.Add(boxNew.transform.position);
			boxs.Add(boxNew);
		}
		float SIZE_BEACON = 2.0f;
		for (int i = 0; i < 7; i++) {
			var beaconNew = Instantiate(P_Beacon);
			beaconNew.transform.parent= this.transform;
			beaconNew.transform.position = new Vector3(-SIZE_BEACON * (7/2) + SIZE_BEACON * i, 0, POSITION_Z);
			beacons.Add(beaconNew);
		}
	}
	PatternBox GetCollidedBox(){
		RaycastHit hit;
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (!Physics.Raycast (ray, out hit, 100.0f, ~(1 << 1)) )
			return null;
		for (int i = 0; i < boxs.Count; i++) {
			if(hit.collider == boxs[i].C_Collider ){
				return boxs[i];
			}
		}
		return null;

	}
	bool IsAllBeaconReady(){
		for (int i =0; i < beacons.Count; i++) {
			if(!beacons[i].isOccupied) return false;
		}
		return true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (meState) {			
		case KState.WaitingForInput:
			if(UpdateWaitingForInput() )
				E_BoxClicked();
			break;
		case KState.Processing:
			if(! UpdateProcessing())
				E_BoxReleased();
			if(IsAllBeaconReady()){
				meState = KState.Locked;
				E_StateLocked();
			}
			break;
		case KState.Locked:
			break;
		}
		//if clicked see if any one of the boxs I have are clicked.
		//if mouse down then put the state to dragging
		//check if the dragged box is closer the the point I am planning to have it.
		//if it is close enough then release the box then set it to that position.
	
	}
	bool UpdateWaitingForInput(){
		if(!Input.GetMouseButtonDown(0) ) return false;
		boxSelected = GetCollidedBox();
		//boxSelectedPositionInit = boxSelected.transform.position;
		if(boxSelected == null) return false;
		meState = KState.Processing;
		return true;
	}
	int GetTouchedBeacon(PatternBox box){
		for (int i = 0;i< this.beacons.Count; i++) {
			var dis = beacons[i].transform.position - box.transform.position;
			if(dis.sqrMagnitude < BEACON_ACTIVE_DISTANCE_SQRT ){
				return i;
			}
		}
		return -1;
	}
	bool UpdateProcessing(){
		if (Input.GetAxis ("Mouse X") != 0 || Input.GetAxis ("Mouse Y") != 0) {
			var pos0 = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,0) );
			var pos1 = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,1) );
			var pos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x ,Input.mousePosition.y,POSITION_Z/ (pos1.z-pos0.z)) );
			boxSelected.Move (boxSelected.transform.position,
			                  new Vector3(Mathf.Clamp(pos.x,-10,10),Mathf.Clamp(pos.y,-1,10), pos.z), .1f);
			//boxSelected.Move (boxSelected.transform.position, new Vector3(pos.x,pom.y, boxSelected.transform.position.z), .3f);
		}
		if (Input.GetMouseButtonUp (0)) {
			meState = KState.WaitingForInput;
			int n = GetTouchedBeacon(boxSelected);
			BoxTouchedBeacon(boxSelected,(n== -1)? null: beacons[n] );
			return false;
		}
		return true;
	}
	void BoxTouchedBeacon(PatternBox box, PatternBeacon beacon){
		if (beacon == null) {
			box.SetFree ();
		} else {
			if(box.State == PatternBox.KState.LOCKED)
				box.beaconLockedAt.Clear();
			beacon.Clear();
			box.SetLock(beacon);
			beacon.Lock(box);

		}
	}
}

