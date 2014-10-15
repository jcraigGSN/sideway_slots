using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public GameObject Reel_1;
	public GameObject Reel_2;
	public GameObject Reel_3;
	public GameObject SpinButton;
	public GameObject TextField_Balance;
	public GameObject TextField_Bet;
	public GameObject TextField_Win;

	protected int BalanceAmount = 10000;

	// Use this for initialization
	void Start () {
		TextField_Balance.GetComponent<TextFieldScript>().setValue (BalanceAmount);
		TextField_Bet.GetComponent<TextFieldScript>().setValue (5);
		TextField_Win.GetComponent<TextFieldScript>().setValue (0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void spinButtonHit() {
		//Deduct bet.
		AdjustBalance (-5);

		//Tell reels to spin.
		Reel_1.GetComponent<ReelScript> ().OnSpin ();
		Reel_2.GetComponent<ReelScript> ().OnSpin ();
		Reel_3.GetComponent<ReelScript> ().OnSpin ();
		//Stop each reel, with a delay.
		StartCoroutine (StopReel1 ());
		StartCoroutine (StopReel2 ());
		StartCoroutine (StopReel3 ());
	}

	public IEnumerator StopReel1() {
		yield return new WaitForSeconds(1);
		Reel_1.GetComponent<ReelScript> ().OnStopReel(2);
	}
	
	public IEnumerator StopReel2() {
		yield return new WaitForSeconds(1.25f);
		Reel_2.GetComponent<ReelScript> ().OnStopReel(6);
	}
	
	public IEnumerator StopReel3() {
		yield return new WaitForSeconds(1.5f);
		Reel_3.GetComponent<ReelScript> ().OnStopReel(8);
	}

	void AdjustBalance(int value) {
		BalanceAmount += value;
		TextField_Balance.GetComponent<TextFieldScript>().setValue (BalanceAmount);
	}
}
