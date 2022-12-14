using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpgradeButton : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI levelText;
    [SerializeField] public TextMeshProUGUI costText;
    public void TextInit(int level, long cost)
    {
        levelText.text = "Lv. " + (1 + level).ToString();
        //costText.text = "$" + cost.ToString();
        costText.text = FactorCalculator.TextConverter(cost, 2);

        //if (cost < 1000)
        //{
        //    costText.text = "$" + cost.ToString();
        //}
        //else if(cost <1000000)
        //{
        //    costText.text = "$" + (cost / 1000).ToString() + "." + ((cost / 100) % 10).ToString() + "k";
        //}
        //else
        //{
        //    costText.text = "$" + (cost / 1000000).ToString() + "." + ((cost / 100000) % 10).ToString() + "m";
        //}

    }
    public void TextInitFull()
    {
        levelText.text = "Max";
        costText.text = "";
    }
    public void TextInitNull()
    {
        levelText.text = "";
        costText.text = "";
    }
}
