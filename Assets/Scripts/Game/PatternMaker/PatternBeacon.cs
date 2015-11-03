using UnityEngine;
using System.Collections;

public class PatternBeacon : MonoBehaviour
{

	public bool isOccupied = false;
	public int idStored = -1;
	public PatternBox boxLockedAt;
	public void Lock(PatternBox box){
		boxLockedAt = box;
		isOccupied = true;
		idStored = box.id;
	}
	public void Clear(){ 
		isOccupied = false;
		idStored = -1;
		if (boxLockedAt != null) {
			boxLockedAt.SetFree();
			boxLockedAt =null;
		}
	}


}

