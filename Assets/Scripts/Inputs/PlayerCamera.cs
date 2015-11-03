using UnityEngine;
using System.Collections.Generic;

public class PlayerCamera : MonoBehaviour {

	GameObject cam;
	Rigidbody body;
	public float sensitivity = 0.0f;
	public float velocity = 1.0f;
	float timePlayerWalked = 0;
	public Dels.V_V E_PlayerWalked = delegate {};
	Vector3 rotationLocal = Vector3.zero;



	void Awake(){
		//Cursor.lockState = CursorLockMode.Locked;
		body = GetComponent<Rigidbody> ();
		rotationLocal = new Vector3 (transform.localRotation.eulerAngles.y, -transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.z );
		Debug.Log ("START ROTATION " + this.rotationLocal);
		//this.rotationY = this.transform.localRotation.x;

	}


	bool isAlive= true;

	// Update is called once per frame	
	void Update () {
		if(Input.GetKeyDown(KeyCode.M) ){
			isAlive = !isAlive;
			if(isAlive){
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		if (!isAlive) 
			return;

		Vector3 axis = new Vector3 (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"),0) * sensitivity * Time.deltaTime;
		Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
		if (axis.x != 0 || axis.y != 0) {
			rotationLocal += axis * sensitivity* Time.deltaTime;
			//rotationLocal.x = Mathf.Clamp(rotationLocal.x, -90, 90);
			rotationLocal.y = Mathf.Clamp(rotationLocal.y, -90, 90);

			//transform.localRotation =Quaternion.Euler(transform.localRotation.eulerAngles + new Vector3(-axis.y,axis.x,0) );
			//Debug.Log(axis.y + " " + this.transform.localRotation.eulerAngles.x  );
			this.transform.localRotation = Quaternion.Euler(-rotationLocal.y,rotationLocal.x,0);
			//this.transform.Rotate(new Vector3(0,axis.x,0));
			//transform.localRotation =Quaternion.Euler(new Vector3(-axis.y,axis.x,0) ) * transform.localRotation;

			
		}
		Dictionary<KeyCode, Vector3> dicMove = new Dictionary<KeyCode, Vector3> (){
			{KeyCode.W, Vector3.forward},
			{KeyCode.S, Vector3.back},
			{KeyCode.A, Vector3.left},
			{KeyCode.D, Vector3.right}
		};
		bool isPlayerWalked = false;
		foreach (var key in dicMove) {
			if(Input.GetKey(key.Key) ){
				isPlayerWalked = true;
				body.velocity =
					Quaternion.Euler(0,transform.localRotation.eulerAngles.y,0) 
					//Quaternion.Euler(transform.localRotation.eulerAngles.x,transform.localRotation.eulerAngles.y,transform.localRotation.eulerAngles.z) 
						* key.Value  *velocity ;

			}
		}
		if (isPlayerWalked) {
			timePlayerWalked += Time.deltaTime;
			if (timePlayerWalked > .3f) {
				E_PlayerWalked ();
				timePlayerWalked = 0;
			}
		} else {
			timePlayerWalked =10;
		}

	
	}
}
 