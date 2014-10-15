
using System.Collections;
using System.Collections.Generic;

public class outcomeObj
{

    private string mSymbol = "";
    private List<List<int>> mHighlights = new List<List<int>>();
    private int mMultiplier = 0;
    private int mAward = 0;


    public void SetSymbol(string symbol)
    {
        this.mSymbol = symbol;
    }
    
    public void SetMultiplier(int val)
    {
        this.mMultiplier = val;
    }
    
    public void SetAward(int val)
    {
        this.mAward = val;
    }
    
    public void SetHighlights(List<List<int>> highlights)
    {
        this.mHighlights = highlights;
    }
        
    public int GetMultiplier()
    {
        return this.mMultiplier;
    }
    
    public int GetAward()
    {
        return this.mAward;
    }

    public List<List<int>>  GetHighlights()
    {
        return this.mHighlights;
    }

    public string GetSymbol()
    {
        return this.mSymbol;
    }



}
