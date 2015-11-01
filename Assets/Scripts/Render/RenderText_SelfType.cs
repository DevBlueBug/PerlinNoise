using UnityEngine;
using System.Collections;

public class RenderText_SelfType : MonoBehaviour
{
	public Dels.V_V E_Typed = delegate {};

	public TextMesh mesh;
	public string content;
	int index = 0;
	float timeWaitPerChar = 1.0f;
	bool isCoroutineContinue = false;
	// Use this for initialization

	void Start ()
	{
		this.enabled = false;	
	}
	public void On(float duration){
		index = 0;
		timeWaitPerChar = duration / content.Length;
		isCoroutineContinue = true;
		StartCoroutine (KCoroutine_Display ());
	}
	public void Off(){
		mesh.text = "";
		isCoroutineContinue = false;
	}

	IEnumerator KCoroutine_Display(){
		while (isCoroutineContinue) {
			var contentNew = content.Substring(0,++index).Replace("\\n", "\n");
			if(contentNew[contentNew.Length-1] == '\\'){
				contentNew = contentNew.Substring(0, contentNew.Length -1);
			}
			mesh.text = contentNew;
			E_Typed();
			if(index >= content.Length) 
				break;
			yield return new WaitForSeconds(timeWaitPerChar);
		}
		yield return null;

	}
}

;