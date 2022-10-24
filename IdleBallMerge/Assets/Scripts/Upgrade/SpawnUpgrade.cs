using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpawnUpgrade : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI levelText1, levelText2;
    [SerializeField] public TextMeshProUGUI costText;
    public void TextInit(int level, int cost)
    {
        float ballNum = Mathf.Pow(2, (level + 1));


        levelText1.text = ((int)ballNum).ToString();


        levelText2.text = "Lv. " + (level + 1).ToString();

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
        levelText1.text = "Full";
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
