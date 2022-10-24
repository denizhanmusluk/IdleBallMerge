using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpawnUpgrade : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI levelText;
    [SerializeField] public TextMeshProUGUI costText;
    public void TextInit(int level, int cost)
    {
        float ballNum = Mathf.Pow(2, (level + 2));


        levelText.text = ((int)ballNum).ToString();

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
        levelText.text = "Full";
        costText.text = "";
    }
    public void TextInitNull()
    {
        levelText.text = "";
        costText.text = "";
    }
}
