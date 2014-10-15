using UnityEngine;
using System.Collections;

public class ButtonSpin : MonoBehaviour {
	public GameObject M_GameController;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown ()
	{	
		//Spin button 45 degrees.
		iTween.RotateAdd (gameObject, iTween.Hash("z", -45, "time", 1));
		//Tell gameController we hit spin.
		M_GameController.GetComponent<GameController> ().spinButtonHit ();
	}
}
