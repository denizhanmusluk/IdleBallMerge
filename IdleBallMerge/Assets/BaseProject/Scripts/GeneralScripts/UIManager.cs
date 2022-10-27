using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using DG.Tweening;
using ObserverSystem;
public class UIManager : Subject
{
    public event Action OnLevelStart, OnNextLevel, OnLevelRestart, OnGamePaused, OnGameResume;
    
    [Header("Screens")]
    public GameObject startCanvas;
    public GameObject ingameCanvas;
    public GameObject finishCanvas;
    public GameObject failCanvas;
    public GameObject pauseMenu;


    //[Header("In Game")]
    //public LevelBarDisplay levelBarDisplay;
    public TextMeshProUGUI inGameScoreText;
    public Image moneyIcon;
    //[Header("Finish Screen")]
    //public ScoreTextManager scoreText;
    
    
    void Awake()
    {
        CheckAndDisplayStartScreen();
    }
    public override void SpesificStart()
    {

    }
    void CheckAndDisplayStartScreen()
    {
        startCanvas.SetActive(true);
        //int displayStart = PlayerPrefs.GetInt("displayStart");
        //if(displayStart > 0)
        //{
        //    startCanvas.SetActive(true);
        //}
        //else
        //{ 
        //    StartLevel();
        //    Invoke(nameof(StartLevelButton), DEFAULT_START_DELAY);
        //    PlayerPrefs.SetInt("displayStart", 1);
        //}
    }

    #region Handler Functions

    public void StartLevelButton()
    {
        OnLevelStart?.Invoke();
        Notify(NotificationType.Start);
        UpgradeManager.Instance.upgradePanel.SetActive(true);
    }

    public void NextLevelButton()
    {
        OnNextLevel?.Invoke();
    }
    public void pauseLevelButton()
    {
        OnGamePaused?.Invoke();
    }
    public void resumeLevelButton()
    {
        OnGameResume?.Invoke();
        pauseMenu.SetActive(false);
        //Globals.isGameActive = true;
    }
    public void RestartLevelButton()
    {
        OnLevelRestart?.Invoke();
    }

    #endregion

    public void StartLevel()
    {
        startCanvas.SetActive(false);
        ingameCanvas.SetActive(true);
    }

    public void FinishLevel()
    {
        ingameCanvas.SetActive(false);
        finishCanvas.SetActive(true);
    }

    public void FailLevel()
    {
        ingameCanvas.SetActive(false);
        failCanvas.SetActive(true);
    }
    // Money Update with LeanTween
    //public void MoneyUpdate(int miktar)
    //{
    //    int moneyOld = Globals.moneyAmount;
    //    Globals.moneyAmount = Globals.moneyAmount + miktar;
    //    LeanTween.value(moneyOld, Globals.moneyAmount, 0.2f).setOnUpdate((float val) =>
    //    {
    //        inGameScoreText.text = ((int)val).ToString();
    //    });
    //    PlayerPrefs.SetInt("money", Globals.moneyAmount);
    //}

    // Money Update with Coroutine
    public void MoneyUpdate(long miktar)
    {
        float moneyOld = (float)Globals.moneyAmount;
        Globals.moneyAmount = Globals.moneyAmount + miktar;
        StartCoroutine(setVal(miktar, moneyOld));

        PlayerPrefs.SetInt("money", (int)Globals.moneyAmount);

        UpgradeManager.Instance.isEnoughMoney();
    }

    IEnumerator setVal(long amount, float oldAmount)
    {
        if (amount > 0)
        {
            DoGetValueScale(moneyIcon.transform, true, 0.75f, 1, 0.5f, Ease.OutElastic);
        }
        float counter = 0f;
        while (counter < 1f)
        {
            counter += 8 * Time.deltaTime;
            float money = Mathf.Lerp(oldAmount, (float)Globals.moneyAmount, counter);
            inGameScoreText.text = FactorCalculator.TextConverter((long)money, 1);
            //Factor((int)money);
            yield return null;
        }
        //Factor(Globals.moneyAmount);
        inGameScoreText.text = FactorCalculator.TextConverter((long)Globals.moneyAmount, 1);

    }
    //public void Factor(int value)
    //{
    //    if (value < 1000)
    //    {
    //        inGameScoreText.text = (value).ToString();
    //    }
    //    else if (value < 1000000)

    //    {
    //        inGameScoreText.text = (value / 1000).ToString() + "." + ((value / 100) % 10).ToString() + "k";
    //    }
    //    else
    //    {
    //        inGameScoreText.text = (value / 1000000).ToString() + "." + ((value / 100000) % 10).ToString() + "m";

    //    }
    //}
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
    public void WinLevel()
    {
        Notify(NotificationType.Win);
    }
}
