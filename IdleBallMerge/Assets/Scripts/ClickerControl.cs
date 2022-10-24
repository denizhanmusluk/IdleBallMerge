using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ObserverSystem;

public class ClickerControl : Observer
{
    private static ClickerControl _instance = null;
    public static ClickerControl Instance => _instance;


    bool click = false;
   [SerializeField] float defaultSpawnSpeed = 1f;
    //float clickAnimSpeed = 2f;
    float currentSpawnSpeed;
    bool speedyActive = false;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {

        //staminaSlider
        currentSpawnSpeed = defaultSpawnSpeed;

        Globals.currentSpawnSpeed = currentSpawnSpeed;
        ObserverManager.Instance.RegisterObserver(this, SubjectType.GameState);
    }

    public override void OnNotify(NotificationType notificationType) 
    {
        switch (notificationType)
        {
            case NotificationType.Start:
                {
                    StartState();
                }
                break;
            case NotificationType.End:
                {

                }
                break;
            case NotificationType.Win:
                {
                    WinState();
                }
                break;
            case NotificationType.Fail:
                {
              
                }
                break;

        }
    }
    void StartState()
    {
    }

    void WinState()
    {
        //VibratoManager.Instance.LongHeavyViration();
        PlayerPrefs.SetInt("BrickUpgradeLevel", 0);
        PlayerPrefs.SetInt("StaminaUpgradeLevel", 0);
        PlayerPrefs.SetInt("ClickAnimLevel", 0);
    }
    public void ClickButton()
    {
        StartCoroutine(Accelerator());
    }
    IEnumerator Accelerator()
    {
        speedyActive = true;
        click = false;
        yield return null;
        currentSpawnSpeed = Globals.clickSpawnSpeed;

        Globals.currentSpawnSpeed = currentSpawnSpeed;


        click = true;
        float counter = 0;
        while (counter < 0.3f && click)
        {
            counter += Time.deltaTime;

            yield return null;
        }
        if (click)
        {
            speedyActive = false;
            currentSpawnSpeed = defaultSpawnSpeed;

            Globals.currentSpawnSpeed = currentSpawnSpeed;
        }
    }

    public void HitBrick()
    {
        //UpgradeManager.Instance.CoinPerSecondView();
        if (speedyActive)
        {
            VibratoManager.Instance.MediumViration();
        }
        else
        {
            VibratoManager.Instance.LightViration();
        }
        //StartCoroutine(MultiHit());
        //brick.HitBrick();
        GameManager.Instance.MoneyUpdate(Globals.coinPerBall);

    }
    //IEnumerator MultiHit()
    //{
    //    GameManager.Instance.MoneyUpdate(Globals.brickPerHit * Globals.coinPerBrick);

    //    int counter = 0;
    //    while (counter<Globals.brickPerHit)
    //    {
    //        counter++;
    //        Globals.buildActive = true;
    //        yield return new WaitForSeconds((float)currentAnimSpeed / (5 * Globals.brickPerHit));
    //    }
    //}
}
