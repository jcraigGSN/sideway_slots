using UnityEngine;
using System.Collections;

public class ButtonBetMinus : MonoBehaviour {
	public GameObject M_GameController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator OnMouseDown ()
	{	
		iTween.Stop (gameObject);
		yield return new WaitForSeconds(0.01f);
		//Spin button 45 degrees CCW.
		spinButton ();
		//Tell gameController we hit spin.
		M_GameController.GetComponent<GameController> ().onBetDecrease ();
	}

	void spinButton() {
		//Spin button 45 degrees.
		iTween.RotateAdd (gameObject, iTween.Hash ("z", 45, "time", 1));
	}
}
