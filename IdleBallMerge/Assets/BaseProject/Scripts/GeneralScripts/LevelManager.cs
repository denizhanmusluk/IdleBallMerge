using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

[System.Serializable]
public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance = null;
    public static LevelManager Instance => _instance;

    public GameObject loadedLevel;
    public GameObject oldLevel;

    //[SerializeField] private List<LevelScriptable> level_set;
    //[SerializeField] public List<BuildSettings> levelBuildSettings;
    [SerializeField] public List<UpgradeSettings> upgradeSettings;

    [SerializeField] List<GameObject> levels;
    //[SerializeField] public int LevelCount;
    public TextMeshProUGUI levelText;
    [SerializeField] GameObject roof;
    [SerializeField] GameObject clickPanel;
    [SerializeField] Button mergeButton;
    private void Awake()
    {
        _instance = this;
    }
    public void levelInit()
    {
        Globals.LevelCount = levels.Count;
        if (levels.Count > 0)
        {
            levelLoad();
        }
    }
    void levelLoad()
    {
        if (PlayerPrefs.GetInt("levelIndex") != 0)
        {
            Globals.currentLevel = PlayerPrefs.GetInt("levelIndex");
            levelText.text = Globals.currentLevel.ToString();
        }
        else
        {
            PlayerPrefs.SetInt("levelIndex", Globals.currentLevel);

        }
        //PlayerPrefs.SetInt("level", 0);
        if (PlayerPrefs.GetInt("level") != 0)
        {
            //Debug.Log(PlayerPrefs.GetInt("level"));
            Globals.currentLevelIndex = PlayerPrefs.GetInt("level");

        }

        ////////LevelsPrefab = (GameObject)Instantiate(Resources.Load("Level" + Globals.currentLevelIndex.ToString()));
        ///
        //loadedLevel = Instantiate(level_set[Globals.currentLevelIndex]._levelPrefab, transform.position, Quaternion.identity);


        oldLevel = loadedLevel;
        loadedLevel = levels[Globals.currentLevelIndex];
        loadedLevel.SetActive(true);
        if (oldLevel != null)
        {
            StartCoroutine(OldLevelClosed());
            CameraManager.Instance.CameraPositionSet();

        }

    }
    IEnumerator OldLevelClosed()
    {
        mergeButton.interactable = false;
        roof.SetActive(false);
        clickPanel.SetActive(false);
        yield return new WaitForSeconds(4f);
        oldLevel.SetActive(false);
        roof.SetActive(true);
        DoGetValuePos(roof.transform, true, 6, 4.33f, 1f, Ease.OutElastic);

        clickPanel.SetActive(true);
    }

    public Tween DoGetValuePos(Transform tr, bool active, float value, float lastValue, float duration, DG.Tweening.Ease type)
    {
        Vector3 firstPos = tr.localPosition;
        Tween tween = DOTween.To
            (() => value, x => value = x, lastValue, duration).SetEase(type).OnUpdate(delegate ()
            {
                tr.localPosition = new Vector3(firstPos.x, value, firstPos.z);
            });
        return tween;
    }
}
