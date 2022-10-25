using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager _instance = null;
    public static UpgradeManager Instance => _instance;
    [SerializeField] Transform brickParent;
    [SerializeField] public GameObject upgradePanel;
    UpgradeSettings upgradeSettings;
    [SerializeField] SpawnUpgrade ballUpgradeButton;
    [SerializeField] MergeButton mergeButton;
    [SerializeField] UpgradeButton clickAnimUpgradeButton;
    [SerializeField] public TextMeshProUGUI brickMoneyText;
    [SerializeField] Transform brickUpgradeImageParent;
    //int brickUpgrade_Level;
    //int staminaUpgrade_Level;
    private void Awake()
    {
        _instance = this;

        upgradePanel.SetActive(false);
        Globals.ballLevel = PlayerPrefs.GetInt("ballLevel");
        Globals.staminaLevel = PlayerPrefs.GetInt("StaminaUpgradeLevel");
        Globals.clickSpeedLevel = PlayerPrefs.GetInt("clickSpeedLevel");
        
    }
    private void Start()
    {

        Init();
        isEnoughMoney();
    }
    void Init()
    {

        //upgradeSettings = LevelManager.Instance.upgradeSettings[PlayerPrefs.GetInt("level")];
        upgradeSettings = LevelManager.Instance.upgradeSettings[0];


        //Globals.brickPerHit = upgradeSettings._brickCount[Globals.brickLevel];
        Globals.coinPerBall = upgradeSettings._coinPerBall[Globals.ballLevel];
        //Globals.healthDownSpeed = upgradeSettings._healthDownPerSeconds[Globals.staminaLevel];
        //Globals.coolDownSpeed = upgradeSettings._coolDownPerSeconds[Globals.staminaLevel];
        Globals.clickSpawnSpeed = upgradeSettings._clickSpawnSpeed[Globals.clickSpeedLevel];
        ModelCreat();
        if (Globals.ballLevel < upgradeSettings._ballUpgradeCost.Length - 1)
        {
            ballUpgradeButton.TextInit(Globals.ballLevel, upgradeSettings._ballUpgradeCost[Globals.ballLevel + 1]);
        }
        else
        {
            ballUpgradeButton.TextInitFull();
        }

        //if (Globals.staminaLevel < upgradeSettings._staminaUpgradeCost.Length - 1)
        //{
        //    staminaUpgradeButton.TextInit(Globals.staminaLevel, upgradeSettings._staminaUpgradeCost[Globals.staminaLevel + 1]);
        //}
        //else
        //{
        //    staminaUpgradeButton.TextInitFull();
        //}

        if (Globals.clickSpeedLevel < upgradeSettings._clickSpeedUpgradeCost.Length - 1)
        {
            clickAnimUpgradeButton.TextInit(Globals.clickSpeedLevel, upgradeSettings._clickSpeedUpgradeCost[Globals.clickSpeedLevel + 1]);
        }
        else
        {
            clickAnimUpgradeButton.TextInitFull();
        }
        //brickMoneyText.text = "$" + (Globals.brickPerHit * Globals.coinPerBrick).ToString() + " / hit";

        MergeCheck();

        CoinPerSecondView();

        MultiplierImageSet();
    }
    public void CoinPerSecondView()
    {
        float totalCoin = ((float)Globals.coinPerBall * (float)Globals.currentSpawnSpeed);
        Debug.Log("totalCoin  " + totalCoin);
        if (((int)totalCoin) < 1000)
        {
            brickMoneyText.text = "$" + ((int)totalCoin).ToString() + " / sec";
        }
        else if (((int)totalCoin) < 1000000)
        {
            brickMoneyText.text = "$" + (((int)totalCoin) / 1000).ToString() + "." + ((((int)totalCoin) / 100) % 10).ToString() + "k" + " / sec";
        }
        else
        {
            brickMoneyText.text = "$" + (((int)totalCoin) / 1000000).ToString() + "." + ((((int)totalCoin) / 100000) % 10).ToString() + "m" + " / sec";
        }
    }
    public void BallUpgradeButton()
    {
        if (Globals.ballLevel < upgradeSettings._ballUpgradeCost.Length - 1)
        {
            if (Globals.moneyAmount >= upgradeSettings._ballUpgradeCost[Globals.ballLevel + 1])
            {
                GameManager.Instance.MoneyUpdate(-upgradeSettings._ballUpgradeCost[Globals.ballLevel + 1]);
                Globals.ballLevel++;
                PlayerPrefs.SetInt("ballLevel", Globals.ballLevel);
                Init();
            }
        }
    }
    public void MergeCheck()
    {
        int searchLevel = 0;
        Ball firstBall = new Ball();
        Ball secondBall = new Ball();
        bool mergeAvtive = false;
        if (BallManager.Instance.ballList.Count > 1)
        {
            for (int level = 0; level < upgradeSettings._ballUpgradeCost.Length - 1; level++)
            {
                for (int i = 0; i < BallManager.Instance.ballList.Count; i++)
                {
                    if (BallManager.Instance.ballList[i].ballLevel == level)
                    {
                        firstBall = BallManager.Instance.ballList[i];
                        break;
                    }
                }

                for (int i = 0; i < BallManager.Instance.ballList.Count; i++)
                {
                    if (level == BallManager.Instance.ballList[i].ballLevel && firstBall.transform.position != BallManager.Instance.ballList[i].transform.position)
                    {
                        secondBall = BallManager.Instance.ballList[i];
                        break;
                    }
                }
                if (secondBall != null && firstBall != null)
                {
                    mergeButton.TextInit(level + 1, upgradeSettings._mergeCost[level + 1]);
                    Globals.currentMergeCost = upgradeSettings._mergeCost[level + 1];
                    Globals.currentMergeLevel = level + 1;
                    mergeAvtive = true;
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        if (!mergeAvtive )
        {
            Globals.nowMergeable = false;
            //mergeButton.TextInitNull();

            mergeButton.TextInit(Globals.ballLevel + 1, upgradeSettings._mergeCost[Globals.ballLevel + 1]);
            mergeButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            Globals.nowMergeable = true;
        }
    }
    void SingleBallCheck()
    {

    }
    public void MergeButton()
    {
        int searchLevel = 0;
        Ball firstBall = new Ball();
        Ball secondBall = new Ball();
    
        if (BallManager.Instance.ballList.Count > 1)
        {
            for (int level = 0; level < upgradeSettings._ballUpgradeCost.Length - 1; level++)
            {
                for (int i = 0; i < BallManager.Instance.ballList.Count; i++)
                {
                    if (BallManager.Instance.ballList[i].ballLevel == level)
                    {
                        firstBall = BallManager.Instance.ballList[i];
                        break;
                    }
                }

                for (int i = 0; i < BallManager.Instance.ballList.Count; i++)
                {
                    if (level == BallManager.Instance.ballList[i].ballLevel && firstBall.transform.position != BallManager.Instance.ballList[i].transform.position)
                    {
                        secondBall = BallManager.Instance.ballList[i];
                        break;
                    }
                }
                if (secondBall == null && firstBall != null)
                {
                    if(firstBall.ballLevel < Globals.ballLevel)
                    {
                        BallManager.Instance.destroyBallList.Add(firstBall);

                        BallManager.Instance.ballList.Remove(firstBall);
                        //BallCreator.Instance.totalMass -= firstBall.GetComponent<Ball>().mass;

                        //Destroy(firstBall.gameObject);
                    }
                }
                if (secondBall != null && firstBall != null)
                {
                    if (Globals.moneyAmount >= upgradeSettings._mergeCost[level + 1])
                    {
                        MergeFunc(firstBall, secondBall, level + 1);
                        GameManager.Instance.MoneyUpdate(-upgradeSettings._mergeCost[level + 1]);
                    }
                    break;
                }
                else
                {
                    continue;
                }
            }
 

            //firstBall = BallManager.Instance.ballList[0];
            //searchLevel = firstBall.ballLevel;

                //for (int l = searchLevel; l < upgradeSettings._ballUpgradeCost.Length; l++)
                //{

                //    for (int i = 1; i < BallManager.Instance.ballList.Count; i++)
                //    {
                //        if (firstBall.ballLevel == BallManager.Instance.ballList[i].ballLevel)
                //        {
                //            secondBall = BallManager.Instance.ballList[i];
                //            break;
                //        }
                //    }
                //}
        }
        //if (Globals.staminaLevel < upgradeSettings._staminaUpgradeCost.Length - 1)
        //{
        //    if (Globals.moneyAmount >= upgradeSettings._staminaUpgradeCost[Globals.staminaLevel + 1])
        //    {
        //        GameManager.Instance.MoneyUpdate(-upgradeSettings._staminaUpgradeCost[Globals.staminaLevel + 1]);
        //        Globals.staminaLevel++;
        //        PlayerPrefs.SetInt("StaminaUpgradeLevel", Globals.staminaLevel);
        //        Init();
        //    }
        //}
    }
    void MergeFunc(Ball firstBall, Ball secondBall,int targetLevel)
    {
        StartCoroutine(Merging(firstBall, secondBall, targetLevel));
        MergeCheck();
        //firstBall.gameObject.SetActive(false);
        //secondBall.gameObject.SetActive(false);

    }
    IEnumerator Merging(Ball firstBall, Ball secondBall, int targetLevel)
    {
        float mergeSpeed = 3f;
        BallManager.Instance.ballList.Remove(firstBall);
        BallManager.Instance.ballList.Remove(secondBall);

        Vector3 firstPos = firstBall.transform.position;
        Vector3 secondPos = secondBall.transform.position;
        Vector3 targetPosition = new Vector3((firstPos.x + secondPos.x) / 2, (firstPos.y + secondPos.y) / 2 + 1, firstPos.z);
        float counter = 0;
        while(counter < 1f)
        {
            counter += mergeSpeed * Time.deltaTime;
            firstBall.transform.position = Vector3.Lerp(firstPos, targetPosition, counter);
            secondBall.transform.position = Vector3.Lerp(secondPos, targetPosition, counter);
            yield return null;
        }
        //firstBall.transform.position = secondBall.transform.position;
        MergeNewBall(targetLevel, targetPosition, upgradeSettings._radius[targetLevel - 1] / 2);


        BallCreator.Instance.totalMass += upgradeSettings._mass[targetLevel];
        //BallCreator.Instance.totalMass -= firstBall.GetComponent<Rigidbody2D>().mass;
        //BallCreator.Instance.totalMass -= secondBall.GetComponent<Rigidbody2D>().mass; 
        BallCreator.Instance.totalMass -= firstBall.GetComponent<Ball>().mass;

        BallCreator.Instance.totalMass -= secondBall.GetComponent<Ball>().mass;

        Destroy(firstBall.gameObject);
        Destroy(secondBall.gameObject);
        VibratoManager.Instance.MediumViration();

    }
    void MergeNewBall(int targetLevel, Vector3 newPos,float oldRadius)
    {
        GameObject newBall = Instantiate(upgradeSettings._ballPrefab[targetLevel], newPos, Quaternion.identity, BallCreator.Instance.ballParent.transform);
        //newBall.GetComponent<Rigidbody2D>().mass = upgradeSettings._mass[targetLevel];
        newBall.GetComponent<Ball>().mass = upgradeSettings._mass[targetLevel];
        newBall.transform.localScale = Vector3.one * upgradeSettings._radius[targetLevel];
        newBall.GetComponent<Ball>().trailRender.startWidth = 3 * upgradeSettings._radius[targetLevel];

        BallManager.Instance.ballList.Add(newBall.GetComponent<Ball>());
        DoGetValueScale(newBall.transform, true, oldRadius, upgradeSettings._radius[targetLevel], 0.5f, Ease.OutElastic);
    }
    public void ClickSpeedUpgradeButton()
    {
        if (Globals.clickSpeedLevel < upgradeSettings._clickSpeedUpgradeCost.Length - 1)
        {
            if (Globals.moneyAmount >= upgradeSettings._clickSpeedUpgradeCost[Globals.clickSpeedLevel + 1])
            {
                GameManager.Instance.MoneyUpdate(-upgradeSettings._clickSpeedUpgradeCost[Globals.clickSpeedLevel + 1]);
                Globals.clickSpeedLevel++;
                PlayerPrefs.SetInt("clickSpeedLevel", Globals.clickSpeedLevel);
                Init();
            }
        }
    }
    public void isEnoughMoney()
    {
        MergeCheck();

        if (Globals.ballLevel < upgradeSettings._ballUpgradeCost.Length - 1 && Globals.moneyAmount >= upgradeSettings._ballUpgradeCost[Globals.ballLevel + 1])
        {
            ballUpgradeButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            ballUpgradeButton.GetComponent<Button>().interactable = false;
        }


        if (Globals.currentMergeLevel <= upgradeSettings._mergeCost.Length && Globals.moneyAmount >= Globals.currentMergeCost && Globals.nowMergeable)
        {
            mergeButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            mergeButton.GetComponent<Button>().interactable = false;
        }


        if (Globals.clickSpeedLevel < upgradeSettings._clickSpeedUpgradeCost.Length - 1 && Globals.moneyAmount >= upgradeSettings._clickSpeedUpgradeCost[Globals.clickSpeedLevel + 1])
        {
            clickAnimUpgradeButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            clickAnimUpgradeButton.GetComponent<Button>().interactable = false;
        }
    }
    void ModelCreat()
    {
        //if(brickParent.childCount > 0)
        //{
        //    Destroy(brickParent.GetChild(0).gameObject);
        //}
        //GameObject _brick = Instantiate(upgradeSettings._brickBoxPrefab[Globals.brickLevel], transform.position, Quaternion.identity, brickParent);
        //_brick.transform.localPosition = new Vector3(0, 10, 0);
        //_brick.transform.localScale = new Vector3(2000, 2000, 2000);
    }
    void MultiplierImageSet()
    {
        for(int i = 0; i < brickUpgradeImageParent.childCount; i++)
        {
            brickUpgradeImageParent.GetChild(i).gameObject.SetActive(false);
        }
        if (Globals.ballLevel < brickUpgradeImageParent.childCount)
        {
            brickUpgradeImageParent.GetChild(Globals.ballLevel).gameObject.SetActive(true);
        }
        else
        {
            brickUpgradeImageParent.GetChild(brickUpgradeImageParent.childCount - 1).gameObject.SetActive(true);
        }
    }

    public Tween DoGetValueScale(Transform tr, bool active, float value, float lastValue, float duration, DG.Tweening.Ease type)
    {
        //Vector3 firstScale = tr.localScale;
        Tween tween = DOTween.To
            (() => value, x => value = x, lastValue, duration).SetEase(type).OnUpdate(delegate ()
            {
                tr.localScale = Vector3.one * value;
            });
        return tween;
    }
}
