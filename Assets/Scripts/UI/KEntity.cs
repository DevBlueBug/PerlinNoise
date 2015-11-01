using UnityEngine;
using System.Collections.Generic;

public class KEntity : MonoBehaviour
{
	public List<Collider> colliders;
	public Dels.V_V E_Pressed = delegate {	};

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	public virtual void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log("HI");
			RaycastHit hit;
			var ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit, 100.0f, ~(1<<1) )){
				for(int i = 0 ; i < colliders.Count;i++){
					if(hit.collider == colliders[i]){
						E_Pressed();
						return;
					}
				}
			}
			
			//Debug.Log("" + hit + " " + hit.collider);
		}
	
	}
}

