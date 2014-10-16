using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class bankerScript : MonoBehaviour
{



	/*************************************
	/* Private Constants
	/*
	/*************************************/
	private const int kPLAYER_STARTING_BALANCE = 10000;
	private static int[] kBET_AMT = new int[] {25, 50, 75, 100, 125, 150};


	/*************************************
	/* Private Class Memebers
	/*
	/*************************************/

	private int mBalance = kPLAYER_STARTING_BALANCE;
	private int mBetAmt = 25;
	private int mAwardAmt = 0;
	private int mBetIdx = 0;

	// Use this for initialization
	void Start()
	{
		string str = GetBalanceString();
		Debug.Log("Balance: " + str);
	}
	
	
	/*************************************
	/* Public Class Methods
	/*
	/*************************************/

	// returns current balance	
	public int GetBalance()
	{
		return this.mBalance;
	}
	
	// returns a formmated string for balance amount used to display on the screen
	public string GetBalanceString()
	{
		return FormatCurrency(this.mBalance);
	}

	// returns the last spin award amount
	public int GetAwardAmt()
	{
		return this.mAwardAmt;
	}
	
	// returns a formmated string for award amount used to display on the screen
	public string GetAwardString()
	{
		return FormatCurrency(this.mAwardAmt);
	}

	// returns the current bet amount				
	public int GetBetAmt()
	{
		return this.mBetAmt;
	}
	
	// increments the bet index into the valid bet amount array and returns the current bet amount
	public int IncBetAmt()
	{
		// increment bet index by 1
		this.mBetIdx++;
		
		// check valid bet amount range
		this.mBetIdx = (this.mBetIdx >= kBET_AMT.Length) ? this.mBetIdx - 1 : this.mBetIdx;
		
		// set new bet amount total
		this.mBetAmt = kBET_AMT [this.mBetIdx];
		
		// if the new bet amount is greater than our balance, reduce bet amount
		if (this.mBetAmt > this.mBalance)
		{
			this.mBetIdx -= 1;
			this.mBetAmt = kBET_AMT [this.mBetAmt];
		}						
		
		// return new bet amount value
		return this.mBetAmt;
	}

	// decrements the bet index and returns the current bet amount
	public int DecBetAmt()
	{
		this.mBetIdx--;
		this.mBetIdx = (this.mBetIdx < 0) ? 0 : this.mBetIdx;
		this.mBetAmt = kBET_AMT [this.mBetIdx];
		return this.mBetAmt;
	}
	
	// resets the bet
	public void ClearBetAmt()
	{
		this.mBetAmt = 0;
		this.mBetIdx = 0;
	}
	
	// resets the award amount
	public void ClearAwardAmt()
	{
		this.mAwardAmt = 0;
	}

	// adds funds to the balance.
	public void IncBalance(int val)
	{
		this.mBalance += val;
	}

	// sets the last spin award, this value should come from our paytable manager
	public void SetAwardAmt(int val)
	{
		this.mAwardAmt = val;
	}


	/*************************************
	/* Private Class Methods
	/*
	/*************************************/

	// helper method to add "," to currency string amount
	private string FormatCurrency(int val)
	{
		char[] formmatedVal = val.ToString().ToCharArray();
		string rc = "";
		
		int j = 0;
		for (int i = formmatedVal.Length-1; i >= 0; i--)
		{
			rc += formmatedVal [i];
			j++;
			if (j % 3 == 0)
			{
				rc += ",";
			}
		}
		
		char[] arr = rc.ToCharArray();
		Array.Reverse(arr);
		return new string(arr);
	}
	

}
