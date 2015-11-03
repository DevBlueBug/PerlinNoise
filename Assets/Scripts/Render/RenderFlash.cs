using UnityEngine;
using System.Collections.Generic;

public class RenderFlash : MonoBehaviour
{
	public List<MeshRenderer> meshes;
	public Color colorFlash;

	float 
		timeElapsed,
		timeMax;


	List<Color> meshColors ; 


	public void On(float duration){
		this.enabled = true;
		timeElapsed = 0;
		timeMax = duration;
	}
	// Use this for initialization
	void Start ()
	{
		meshColors = new List<Color> ();
		foreach (var m in meshes)
			meshColors.Add (m.material.color);
		this.enabled = false;

	
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeElapsed += Time.deltaTime;
		float ratio = Mathf.Min (1, timeElapsed / timeMax);
		ratio *= ratio;
		for (int i = 0; i < meshes.Count; i++) {
			meshes[i].material.color = new Color(
				colorFlash.r + (meshColors[i].r-colorFlash.r) * ratio,
				colorFlash.g + (meshColors[i].g-colorFlash.g) * ratio,
				colorFlash.b + (meshColors[i].b-colorFlash.b) * ratio);//colorFlash + (meshColors[i]-colorFlash) * ratio;
		}
		if (ratio >= 1) {
			enabled = false;
		}
	}
}

