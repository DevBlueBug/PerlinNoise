using UnityEngine;
using System.Collections;

public class AniRandomSpin : MonoBehaviour
{
	Vector3 rotationVelocity;
	float speed = 0;
	// Use this for initialization
	void Awake ()
	{
		rotationVelocity = new Vector3 (Random.Range (-1, 1.0f), Random.Range (-1, 1.0f), Random.Range (-1, 1.0f));
		rotationVelocity.Normalize ();
		speed = Random.Range (10, 50);
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.transform.Rotate (rotationVelocity *speed* Time.deltaTime);
	
	}
}

