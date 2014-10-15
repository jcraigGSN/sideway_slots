
using System.Collections;
using System.Collections.Generic;


public class spinResult
{
    private List<List<string>> mResult = new List<List<string>>();
    private List<outcomeObj> mOutcomes = new List<outcomeObj>();
    
    
    public void AddOutcome(string symbol, int multiplier, List<List<int>> highlights)
    {
        outcomeObj outcome = new outcomeObj();
        outcome.SetSymbol(symbol);
        outcome.SetMultiplier(multiplier);
        outcome.SetHighlights(highlights);
        this.mOutcomes.Add(outcome);
    }

    public void AddResult(List<List<string>> result)
    {
        this.mResult = result;
    }
    
    // returns list of outcome objects containing ways info
    public List<outcomeObj> GetHighlights()
    {
        return this.mOutcomes;
    }
    
    // returns 3x3 array of symbols
    public List<List<string>> GetResult()
    {
        return this.mResult;
    }
    
    // determines if won spin
    public bool IsWin()
    {
        if (this.mOutcomes != null && this.mOutcomes.Count > 0)
        {
            return true;
        }
        return false;
    }
    
    // returns the total amount won for all ways
    public int GetTotalAward()
    {
        int totalAward = 0;
        
        foreach (outcomeObj outcome in this.mOutcomes)
        {
            if (outcome != null)
            {
                totalAward += outcome.GetAward();
            }
        }
        return totalAward;
    }

}
