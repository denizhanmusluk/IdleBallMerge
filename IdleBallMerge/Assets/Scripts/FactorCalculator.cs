using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FactorCalculator
{
    public static string TextConverter(long amount, int stepCount)
    {
        string txt = "";

        long stepMod = (long)Mathf.Pow(10, stepCount);
        
        if (amount < (long)Mathf.Pow(10,3))
        {
            txt = (amount).ToString();
        }
        else if (amount < (long)Mathf.Pow(10, 6))
        {
            txt = (amount / (long)Mathf.Pow(10, 3)).ToString() + "." + ((amount / ((long)Mathf.Pow(10, 3) / stepMod)) % stepMod).ToString() + "K";
            //txt = (amount / 1000).ToString() + "." + ((amount / 10) % 100).ToString() + "k";
        }
        else if (amount < (long)Mathf.Pow(10, 9))
        {
            txt = (amount / (long)Mathf.Pow(10, 6)).ToString() + "." + ((amount / (long)Mathf.Pow(10, 6) / stepMod) % stepMod).ToString() + "M";
        }
        else if (amount < (long)Mathf.Pow(10, 12))
        {
            txt = (amount / (long)Mathf.Pow(10, 9)).ToString() + "." + ((amount / (long)Mathf.Pow(10, 9) / stepMod) % stepMod).ToString() + "B";
        }
        else if (amount < (long)Mathf.Pow(10, 15))
        {
            txt = (amount / (long)Mathf.Pow(10, 12)).ToString() + "." + ((amount / (long)Mathf.Pow(10, 12) / stepMod) % stepMod).ToString() + "T";
        }
        else if (amount < (long)Mathf.Pow(10, 18))
        {
            txt = (amount / (long)Mathf.Pow(10, 15)).ToString() + "." + ((amount / (long)Mathf.Pow(10, 15) / stepMod) % stepMod).ToString() + "AA";
        }
        else if (amount < (long)Mathf.Pow(10, 21))
        {
            txt = (amount / (long)Mathf.Pow(10, 18)).ToString() + "." + ((amount / (long)Mathf.Pow(10, 18) / stepMod) % stepMod).ToString() + "BB";
        }
        else if (amount < (long)Mathf.Pow(10, 24))
        {
            txt = (amount / (long)Mathf.Pow(10, 21)).ToString() + "." + ((amount / (long)Mathf.Pow(10, 21) / stepMod) % stepMod).ToString() + "CC";
        }
        else if (amount < (long)Mathf.Pow(10, 27))
        {
            txt = (amount / (long)Mathf.Pow(10, 24)).ToString() + "." + ((amount / (long)Mathf.Pow(10, 24) / stepMod) % stepMod).ToString() + "DD";
        }
        return txt;
    }
}
