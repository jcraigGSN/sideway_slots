using UnityEngine;
using System.Collections;

public class ButtonBetPlus : MonoBehaviour {
	public GameObject M_GameController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator OnMouseDown ()
	{	
		iTween.StopByName ("BetPlusAnim");
		yield return new WaitForSeconds(0.01f);
		spinButton ();
		//Tell gameController we hit spin.
		M_GameController.GetComponent<GameController> ().onBetIncrease ();
	}

	void spinButton() {
		//Spin button 45 degrees.
		iTween.RotateAdd (gameObject, iTween.Hash ("name", "BetPlusAnim", "z", -45, "time", 1));
	}

}
