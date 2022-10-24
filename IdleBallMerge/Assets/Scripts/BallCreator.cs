using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverSystem;
using DG.Tweening;
public class BallCreator : Observer
{
    //[SerializeField] GameObject[] ballPrefabs;
    [SerializeField] public float totalMass = 0f;
    private static BallCreator _instance = null;
    public static BallCreator Instance => _instance;

    UpgradeSettings upgradeSettings;
    public List<Rope> ropes = new List<Rope>();
    public GameObject ballParent;
    public bool withoutBallSpawn = false;
    float tempSpeed;

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        ObserverManager.Instance.RegisterObserver(this, SubjectType.GameState);//observer register

        tempSpeed = Globals.currentSpawnSpeed;
        Initialize();
    }
    void Initialize()
    {
        upgradeSettings = LevelManager.Instance.upgradeSettings[0];
        //upgradeSettings = LevelManager.Instance.upgradeSettings[PlayerPrefs.GetInt("level")];
    }
    IEnumerator Ballcreation()
    {
        while (Globals.spawnActive)
        {
            ChangeSpeedCheck();
            if (!withoutBallSpawn)
            {
                Spawning();
            }

            UpdateCoin();
            float counter = 0;
            while(counter< 1f / Globals.currentSpawnSpeed)
            {
                counter += Time.deltaTime;
                yield return null;
            }
            //yield return new WaitForSeconds(1f / Globals.currentSpawnSpeed);
        }
    }
    void Spawning()
    {
        GameObject newBall = Instantiate(upgradeSettings._ballPrefab[Globals.ballLevel], transform.position, Quaternion.identity, ballParent.transform);
        //newBall.GetComponent<Rigidbody2D>().mass = upgradeSettings._mass[Globals.ballLevel];
        newBall.GetComponent<Ball>().mass = upgradeSettings._mass[Globals.ballLevel];
        newBall.transform.localScale = Vector3.one * upgradeSettings._radius[Globals.ballLevel];
        BallManager.Instance.ballList.Add(newBall.GetComponent<Ball>());

    

        totalMass += upgradeSettings._mass[Globals.ballLevel];

        foreach(Rope rope in ropes)
        {
            rope.totalMass = totalMass * 1000;
            rope.MassUpdate();
        }
    }
    void ChangeSpeedCheck()
    {
        if (Globals.currentSpawnSpeed != tempSpeed)
        {
            DoGetValueScale(UpgradeManager.Instance.brickMoneyText.transform, true, 0.75f, 1, 0.5f, Ease.OutElastic);
        }
        tempSpeed = Globals.currentSpawnSpeed;
    }
    void UpdateCoin()
    {
        UpgradeManager.Instance.CoinPerSecondView();
        GameManager.Instance.MoneyUpdate(Globals.coinPerBall);
        UpgradeManager.Instance.MergeCheck();
    }
    public override void OnNotify(NotificationType notificationType)
    {
        Globals.spawnActive = true;
        StartCoroutine(Ballcreation());
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
