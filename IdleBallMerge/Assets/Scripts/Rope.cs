using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Rope : MonoBehaviour
{
    bool ropeActive = true;
    [SerializeField] float ropeMaxHealth;
    [SerializeField] public float ropeCurrentHealth;
    [SerializeField] public float totalMass;
    [SerializeField] TextMeshProUGUI ropeStaminaText;
    [SerializeField] RelativeJoint2D rope1, rope2;
    [SerializeField] GameObject locked;
    [SerializeField] RopeSettings ropeSettings;
    bool startActive = true;
    private void Start()
    {
        Init();
        startActive = false;
    }
    void OnEnable()
    {
        if (!startActive)
        {
            Init();
        }
    }
    void Init()
    {
        RopeHealthInit();
        ropeCurrentHealth = ropeMaxHealth;
        //InitText(ropeCurrentHealth);
        StartCoroutine(setVal(ropeCurrentHealth, ropeCurrentHealth));
        BallCreator.Instance.ropes.Add(this);
    }
    void InitText(float health)
    {
        DoGetValueScale(ropeStaminaText.transform, true, 0.75f, 1, 0.5f, Ease.OutElastic);

        if (health < 1000)
        {
            ropeStaminaText.text = ((int)health).ToString();
        }
        else if (health < 1000000)

        {
            ropeStaminaText.text = ((int)health / 1000).ToString() + "." + (((int)health / 100) % 100).ToString() + "k";
        }
        else
        {
            ropeStaminaText.text = ((int)health / 1000000).ToString() + "." + (((int)health / 100000) % 100).ToString() + "M";

        }
    }
    IEnumerator setVal(float newAmount, float oldAmount)
    {
        DoGetValueScale(ropeStaminaText.transform, true, 0.75f, 1, 0.5f, Ease.OutElastic);

        float counter = 0f;
        while (counter < 1f)
        {
            counter += 8 * Time.deltaTime;
            float newHealth = Mathf.Lerp(oldAmount, (float)newAmount, counter);
            if (newHealth < 1000)
            {
                ropeStaminaText.text = ((int)newHealth).ToString();
            }
            else if (newHealth < 1000000)
            {
                ropeStaminaText.text = ((int)newHealth / 1000).ToString() + "." + (((int)newHealth / 10) % 100).ToString() + "k";
            }
            else
            {
                ropeStaminaText.text = ((int)newHealth / 1000000).ToString() + "." + (((int)newHealth / 10000) % 100).ToString() + "m";
            }
            yield return null;
        }
        if (newAmount < 1000)
        {
            ropeStaminaText.text = ((int)newAmount).ToString();
        }
        else if (newAmount < 1000000)

        {
            ropeStaminaText.text = ((int)newAmount / 1000).ToString() + "." + (((int)newAmount / 10) % 100).ToString() + "k";
        }
        else
        {
            ropeStaminaText.text = ((int)newAmount / 1000000).ToString() + "." + (((int)newAmount / 10000) % 100).ToString() + "M";

        }
    }
    public void MassUpdate()
    {
        if (ropeActive)
        {
            float oldHealth = ropeCurrentHealth;
            ropeCurrentHealth = ropeMaxHealth - totalMass;
            if (ropeCurrentHealth > 0)
            {
                //InitText(ropeCurrentHealth);
                StartCoroutine(setVal(ropeCurrentHealth, oldHealth));
            }
            else
            {
                StartCoroutine(setVal(0, oldHealth));
                RopeCut();
            }
        }
    }
    void RopeCut()
    {
        VibratoManager.Instance.HeavyViration();
        ropeActive = false;
        rope1.enabled = false;
        rope2.enabled = false;
        locked.SetActive(false);
        foreach (var rp in GetComponentsInChildren<FixedJoint2D>())
        {
            rp.frequency = 1;
        }
        GameManager.Instance.ui.WinLevel();
        BallManager.Instance.DestroyListClear();
    }
    void RopeHealthInit()
    {
        ropeMaxHealth= ropeSettings._ropeMaxHealth[PlayerPrefs.GetInt("level")];
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