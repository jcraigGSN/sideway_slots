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

	public GameObject BankerObject;
	private bankerScript banker;
	private TextFieldScript textBalance;
	private TextFieldScript textBet;
	private TextFieldScript textWin;

	//Spin outcome
	public GameObject ServerObject;
	private serverScript mServerScript;
	private List<List<string>> mResult;
	private spinResult mSpinResult;
	public GameObject mSymbolHighlight;
	private List<GameObject> mWinHighlights;

	//Current state of game. 1=betting, 2 = spinning.
	private int mGameState = 1;

	void Awake () {
		banker = BankerObject.GetComponent<bankerScript> ();
		textBalance = TextField_Balance.GetComponent<TextFieldScript> ();
		textBet = TextField_Bet.GetComponent<TextFieldScript> ();
		textWin = TextField_Win.GetComponent<TextFieldScript> ();
		mServerScript = ServerObject.GetComponent<serverScript> ();
		mWinHighlights = new List<GameObject> ();
	}
	// Use this for initialization
	void Start () {
		textBalance.setValue (banker.GetBalance ());
		textBet.setValue (banker.GetBetAmt());
		textWin.setValue (0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void spinButtonHit() {
		if (mGameState == 1) {
			mGameState = 2;
			clearHighlights ();
			//Deduct bet on display.
			int tempBal = banker.GetBalance () - banker.GetBetAmt ();
			textBalance.setValue (tempBal);
			//Clear win amount
			textWin.setValue (0);

			//Tell reels to spin.
			Reel_1.GetComponent<ReelScript> ().OnSpin ();
			Reel_2.GetComponent<ReelScript> ().OnSpin ();
			Reel_3.GetComponent<ReelScript> ().OnSpin ();
			//Stop each reel, with a delay.
			StartCoroutine (StopReel1 ());
			StartCoroutine (StopReel2 ());
			StartCoroutine (StopReel3 ());
			StartCoroutine (OnReelsStopped ());

			//Get outcome.
			mSpinResult = mServerScript.Spin (banker.GetBetAmt ());
			mResult = mSpinResult.GetResult ();  //Get symbols on reels.
		}
	}

	public IEnumerator StopReel1() {
		yield return new WaitForSeconds(1);
		List<string> reelResult = new List<string> ();
		reelResult.Add (mResult [0] [0]);
		reelResult.Add (mResult [1] [0]);
		reelResult.Add (mResult [2] [0]);
		Reel_1.GetComponent<ReelScript> ().OnStopReel(reelResult);
	}
	
	public IEnumerator StopReel2() {
		yield return new WaitForSeconds(1.25f);
		List<string> reelResult = new List<string> ();
		reelResult.Add (mResult [0] [1]);
		reelResult.Add (mResult [1] [1]);
		reelResult.Add (mResult [2] [1]);
		Reel_2.GetComponent<ReelScript> ().OnStopReel(reelResult);
	}
	
	public IEnumerator StopReel3() {
		yield return new WaitForSeconds (1.5f);
		List<string> reelResult = new List<string> ();
		reelResult.Add (mResult [0] [2]);
		reelResult.Add (mResult [1] [2]);
		reelResult.Add (mResult [2] [2]);
		Reel_3.GetComponent<ReelScript> ().OnStopReel(reelResult);
	}

	public IEnumerator OnReelsStopped() {
		//Adjust balance in banker.
		banker.IncBalance (banker.GetBetAmt () * -1);
		//Get outcomes.
		yield return new WaitForSeconds (1.5f);
		//Highlight wins.

		//Show win amount.
		int win = mSpinResult.GetTotalAward ();
		textWin.setValue (win);
		//Increment balance.
		banker.IncBalance (win);
		textBalance.setValue (banker.GetBalance ());
		//Cycle wins.
		if (mSpinResult.GetHighlights().Count > 0) {
			CycleWins (mSpinResult);
		}
		mGameState = 1;
	}

	protected void setBetAmt() {
		textBet.setValue (banker.GetBetAmt ());
		//Clear win amount
		textWin.setValue (0);
	}
	/**
	 * Incereases bet amount. Called from ButtonBetPlus.
	 */
	public void onBetIncrease() {
		if (mGameState == 1) {
			banker.IncBetAmt ();
			setBetAmt ();
			clearHighlights ();
		}
	}
	public void onBetDecrease() {
		if (mGameState == 1) {
			banker.DecBetAmt ();
			setBetAmt ();
			clearHighlights ();
		}
	}

	protected void CycleWins(spinResult inSpinResult) {
		//Get first award
		List<outcomeObj> mOutcomeObj = inSpinResult.GetHighlights ();
		List<List<int>> mHighlights = mOutcomeObj[0].GetHighlights ();

		for(int i=0; i <3; ++i) {
			Debug.Log(" highlight line " + i + " = " + mHighlights[i][0] + mHighlights[i][1] + mHighlights[i][2]);
			for(int j = 0; j < 3; ++j) {
				Debug.Log ("check " + i + "," + j);
				if (mHighlights[i][j] == 1) {
					//Add highlight   
					GameObject reelTemp = GameObject.Find("Reel_" + (j+1)) as GameObject;
					float posX = reelTemp.transform.localPosition.x - ((i-1 )*-2);
					float posY = reelTemp.transform.localPosition.y;
					Debug.Log ("   highlight " + i + "," + j + ", xy = " + posX + "," + posY);
					GameObject highlightSymbol = GameObject.Instantiate(mSymbolHighlight, new Vector3 (posX, posY, -4), Quaternion.identity) as GameObject;
					mWinHighlights.Add(highlightSymbol);
				}
			}
		}
	}

	protected void clearHighlights() {
		foreach (GameObject obj in mWinHighlights) {
			GameObject.DestroyObject (obj);
		}
		mWinHighlights = new List<GameObject> ();
	}
}
