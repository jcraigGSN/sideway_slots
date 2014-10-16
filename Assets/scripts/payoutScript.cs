using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class payoutScript
{

	// private static variable
	private static int[] sPayOuts = new int[]{10,8,6,4,2,25};


	//private spinResult sr;
	private spinResult sr;


	// determines total award amount for all outcomes. Note 0 award amount is no win
	public static int GetAwardAmt (ref spinResult sr, int betAmt)
	{
		int rc = 0;
		List<outcomeObj> outcomes;
		int multiplier;
		string symbol;
		int award;
		
		if (sr != null && sr.IsWin () == true) {
			// determine out come	
			// loop through each outcome and calculate the amount
			
			outcomes = sr.GetHighlights ();
			foreach (outcomeObj outcome in outcomes) {
				if (outcome != null) {
					symbol = outcome.GetSymbol ();
					multiplier = outcome.GetMultiplier ();
					
					award = DetermineAmt (symbol, multiplier, betAmt);
					outcome.SetAward (award);
					rc += award;
				}
			}
		} else {
			rc = 0;
			// if you get a 0, deduct the last bet amount from the balance and then reset bet amount to 0 for next spin
		}
		return rc;
	}

	// looks up winning amount for given outcome
	private static int DetermineAmt (string symbol, int multiplier, int betAmt)
	{
		int rc = 0;
		int idx;
		int basePayout;
		
		idx = SymbolToIndex (symbol);
		if (idx != -1) {
			rc = sPayOuts [idx] * multiplier * (betAmt / 25);
		}
		
		return rc;
	}	
	
	// converts symbol into index for payout lookup
	private static int SymbolToIndex (string symbol)
	{
		int rc = -1;
		List<string> symbolList = serverScript.sReelSymbols;
		
		for (int i=0; i<symbolList.Count; i++) {
			if (symbolList [i] == symbol) {
				rc = i;
				break;
			}
		}
		return rc;
	}
	
}
