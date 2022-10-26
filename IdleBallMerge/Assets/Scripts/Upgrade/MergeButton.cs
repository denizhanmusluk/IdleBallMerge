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
        float ballNum;
        if (level <= 10)
        {
            ballNum = Mathf.Pow(2, (level));
        }
        else
        {
            ballNum = Mathf.Pow(2, (level - 9)) * 1000;

        }
        if (ballNum < 1000)
        {
            levelText.text = ((int)ballNum).ToString() + "   " + ((int)ballNum).ToString();
        }
        else if (ballNum < 1000000)
        {
            ballNum /= 1000;
            //ballNum = Mathf.Round(ballNum);
            levelText.text = ((int)ballNum).ToString() + "k" + "   " + ((int)ballNum).ToString() + "k";
        }
        else
        {
            ballNum /= 1000000;
            //ballNum = Mathf.Round(ballNum);
            levelText.text = ((int)ballNum).ToString() + "m" + "   " + ((int)ballNum).ToString() + "m";
        }
        //levelText.text = ((int)ballNum).ToString() + "   " + ((int)ballNum).ToString();


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
