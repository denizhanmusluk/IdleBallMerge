using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MergeButton : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI levelText, textAdd;
    [SerializeField] public TextMeshProUGUI costText;
    public void TextInit(int level, int cost)
    {
        float ballNum = Mathf.Pow(2, (level));
        levelText.text = ((int)ballNum).ToString() + "   " + ((int)ballNum).ToString();
        textAdd.text = "+";
        //costText.text = "$" + cost.ToString();


        if (cost < 1000)
        {
            costText.text = "$" + cost.ToString();
        }
        else if (cost < 1000000)
        {
            costText.text = "$" + (cost / 1000).ToString() + "." + ((cost / 100) % 10).ToString() + "k";
        }
        else
        {
            costText.text = "$" + (cost / 1000000).ToString() + "." + ((cost / 100000) % 10).ToString() + "m";
        }

    }
    public void TextInitFull()
    {
        levelText.text = "Max";
        costText.text = "";
        textAdd.text = "";
    }
    public void TextInitNull()
    {
        levelText.text = "";
        costText.text = "";
        textAdd.text = "";
    }
}
