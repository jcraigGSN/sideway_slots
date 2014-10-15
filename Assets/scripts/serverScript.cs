using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class serverScript : MonoBehaviour
{

    // slot machine setup
    private static int kSYMBOLS_PER_REEL = 3;
    private static int kREELS_COUNT = 3;
    
    // symbols
    private static List<string> sReelSymbols = new List<string>{"A","B","C","D","E","J"};
    
    // symbol's weights
    private static List<int> sReelOneWeights = new List<int>{10,10,10,10,10,10};
    private static List<int> sReelTwoWeights = new List<int>{10,20,30,40,50};
    private static List<int> sReelThreeWeights = new List<int>{10,20,30,40,50}; 
    
    
    // private variables
    private List<List<int>> mReelWeights;
    
    private spinResult mLastSpinResults;
    
    /*************************************
    /* MonoBehaviour Methods
    /************************************/       
    void Start()
    {
        var spinResults = new List<List<string>>();
        spinResult sr;
        
        // spin        
        spinResults = this.Spin();
        
        if (this.mLastSpinResults.IsWin() == true)
        {
            List<outcomeObj> outcomes = mLastSpinResults.GetHighlights();
            foreach (outcomeObj outcome in outcomes)
            {
                Debug.Log("Multiplier: " + outcome.GetMultiplier());
            }
        }
    }
    
    void Update()
    {
        
    }
    
    
    /*************************************
    /* Public Methods
    /************************************/        

    /* Spin - creates a 3x3 array of weighted symbols */
    public List<List<string>> Spin()
    {
        int weight = 0;
        string symbol = "";
        
        var reelResult = new List<string>();
        var spinResult = new List<List<string>>();
        
        // create spin results
        for (int reel = 0; reel < kREELS_COUNT; reel++)
        {
            reelResult = new List<string>();
            for (int index = 0; index < kSYMBOLS_PER_REEL; index++)
            {
                weight = GetRandomWeight(reel);
                symbol = GetSymbolByWeight(reel, weight);
                reelResult.Add(symbol);
            }
            spinResult.Add(reelResult);
        }
        
        // store last spin results
        this.mLastSpinResults = this.CreateWayMatches(spinResult);
        
        return spinResult;
    }   
    
    /* GetSpinResults - returns the results from last spin */
    public spinResult GetSpinResults()
    {
        return this.mLastSpinResults;
    }
       
    
    /*************************************
    /* Private Methods
    /************************************/        
    
    
    // determine way count for given match array
    private int GetWayMultiplier(List<List<int>> matches)
    {
        int count = 0;
        int multiplier = 1;
        
        for (int reel = 0; reel < kREELS_COUNT; reel ++)
        {
            count = GetSymbolMatchCount(matches [reel]);
            if (count == 0)
            {
                multiplier = 0;
                break;
            } else
            {
                multiplier *= count;
            }
        }
        return multiplier;
    }

    // simple method to return the number of matches "1" per reel
    private int GetSymbolMatchCount(List<int> symbolMatch)
    {
        int count = 0;
        for (int i = 0; i < symbolMatch.Count; i++)
        {
            if (symbolMatch [i] == 1)
            {
                count++;
            }
        }
        return count;
    }
    
    
    // bad method name - basically will check all symbols in first reel to make sure we dont count them twice
    private bool IsDuplicate(List<List<string>> spinResults, int row)
    {
        for (int i=0; i<row; i++)
        {
            if (spinResults [0] [i] == spinResults [0] [row])
            {
                return true;
            }
        }
        return false;
    }
    
    // creates all way matches
    private spinResult CreateWayMatches(List<List<string>> spinResults)
    {
      
        spinResult sr; 
        string targetSymbol = "";
        List<int> reelMatches;
        List<List<int>> way = new List<List<int>>(); // contains 3 reelmatches
        
        // create a new spin results object
        sr = new spinResult();
        sr.AddResult(spinResults);
        
        for (int row = 0; row < kREELS_COUNT; row++)
        {
            //if (spinResults [0] [row] != targetSymbol)
            way = null;
            if (IsDuplicate(spinResults, row) == false)
            {
                way = new List<List<int>>();
                targetSymbol = spinResults [0] [row]; // grab symbol from first column
                
                for (int reel = 0; reel < kREELS_COUNT; reel++)
                {
                    reelMatches = CreateReelSymbolMatch(spinResults, reel, targetSymbol);
                    if (GetSymbolMatchCount(reelMatches) > 0)
                    {
                        way.Add(reelMatches);
                    } else
                    {
                        way = null;
                        break;
                    }
                }
            }
            
            if (way != null)
            {
                // add an outcome to the spin results
                sr.AddOutcome(targetSymbol, GetWayMultiplier(way), way);
                
                Debug.Log("Found way for symbol: " + targetSymbol + " M: " + GetWayMultiplier(way));
            }
        }
        return sr;
    }
            
    // builds symbol match by reel        
    private List<int> CreateReelSymbolMatch(List<List<string>> spinResults, int reelidx, string targetSymbol)
    {
        var reelMatch = new List<int>();
        
        for (int row = 0; row < kSYMBOLS_PER_REEL; row++)
        {
            if (spinResults [reelidx] [row] == targetSymbol)
            {
                reelMatch.Add(1);
            } else
            {
                reelMatch.Add(0);
            }
        }
        return reelMatch;
    }
    
    
    
    /* GetRandomWeight - returns random weight for given reel */
    private int GetRandomWeight(int reelIndex)
    {
        int max = CalcMaxReelWeight(reelIndex);
        int weight = Random.Range(1, max);
        return weight;
    }
		
    /* GetSymbolByWeight - returns symbol for given reel / weight */
    private string GetSymbolByWeight(int reelIdx, int targetWeight)
    {
        List<int> weights = GetReelWeights(reelIdx);
        
        string symbol;
        int currentWeight = 0;
		
        int index = 0;
        currentWeight = 0;
        
        while (currentWeight < targetWeight)
        {
            currentWeight += weights [index];
            if (currentWeight < targetWeight)
                index++;
        }        
        symbol = sReelSymbols [index];
		
        return symbol;
    }
	
    /* GetReelWeights - returns reel weights for given reel */
    private List<int> GetReelWeights(int reelIdx)
    {
        var weights = new List<int>();
			
        switch (reelIdx)
        {
            case 0:
                weights = sReelOneWeights;
                break;
            case 1:
                weights = sReelTwoWeights;
                break;
            case 2:
                weights = sReelThreeWeights;
                break;
            default:
                break;
        }
        return weights;
    }	
		
    /*CalcMaxReelWeight - returns the sum of weights for a given reel */
    private int CalcMaxReelWeight(int reelIdx)
    {
        var weights = GetReelWeights(reelIdx);
        int range = 0;
        foreach (int weight in weights)
        {
            range += weight;
        }
        return range;
    }
		

}



