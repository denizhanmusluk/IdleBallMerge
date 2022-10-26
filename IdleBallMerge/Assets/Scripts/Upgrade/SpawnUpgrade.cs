using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SpawnUpgrade : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI levelText1, levelText2;
    [SerializeField] public TextMeshProUGUI costText;
    [SerializeField] Image ballImage;
    public void TextInit(int level, int cost, GameObject ball)
    {
        float ballNum;
        if (level < 10)
        {
            ballNum = Mathf.Pow(2, (level + 1));
        }
        else
        {
            ballNum = Mathf.Pow(2, (level - 9)) * 1000;

        }

        if (ballNum < 1000)
        {
            levelText1.text = ((int)ballNum).ToString();
        }
        else if (ballNum < 1000000)
        {
            ballNum /= 1000;
            //ballNum = Mathf.Round(ballNum);
            levelText1.text = ((int)ballNum).ToString() + "k";
        }
        else
        {
            ballNum /= 1000000;
            //ballNum = Mathf.Round(ballNum);
            levelText1.text = ((int)ballNum).ToString() + "m";
        }



        string ballNumString = ((int)ballNum).ToString();

        //levelText1.text = ballNumString;
        if (ballNumString.Length > 2)
        {
            levelText1.fontSize = 50;
        }
        else
        {
            levelText1.fontSize = 70;
        }
        ballImage.color = ball.GetComponent<SpriteRenderer>().color;



        levelText2.text = "Lv. " + (level + 1).ToString();

        //costText.text = "$" + cost.ToString();



        costText.text = Factor(cost);


        //if (cost < 1000)
        //{
        //    costText.text = "$" + cost.ToString();
        //}
        //else if (cost < 1000000)
        //{
        //    costText.text = "$" + (cost / 1000).ToString() + "." + ((cost / 100) % 10).ToString() + "k";
        //}
        //else
        //{
        //    costText.text = "$" + (cost / 1000000).ToString() + "." + ((cost / 100000) % 10).ToString() + "m";
        //}

    }
    public string Factor(int value)
    {
        string txt;
        if (value < 1000)
        {
            txt = (value).ToString();
        }
        else if (value < 1000000)
        {
            txt = (value / 1000).ToString() + "." + ((value / 100) % 10).ToString() + "k";
        }
        else
        {
            txt = (value / 1000000).ToString() + "." + ((value / 100000) % 10).ToString() + "m";

        }
        return txt;
    }
    public void TextInitFull()
    {
        levelText1.text = "Max";
        levelText2.text = "";
        costText.text = "";
    }
    public void TextInitNull()
    {
        levelText1.text = "";
        levelText2.text = "";
        costText.text = "";
    }
}
