using UnityEngine;
using System.Collections;

public class ButtonSpin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown ()
	{		
		Debug.Log("   SPIN");
		//transform.Rotate (Vector3.forward * -10);
		iTween.RotateAdd (gameObject, iTween.Hash("z", -30, "time", 1)); 
	}
}
