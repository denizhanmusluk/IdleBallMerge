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
    [SerializeField] float oscilPeriod;
    [SerializeField] float oscilForce;
    [SerializeField] Transform box;
    [SerializeField] Transform moneyIconPos;

    [SerializeField] int moneyIconCount;
    [SerializeField] int ropeID;
    private void Start()
    {
        Init();
        startActive = false;
        StartCoroutine(Oscillation());
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

            ropeStaminaText.text = FactorCalculator.TextConverter((int)newHealth, 2);
            //if (newHealth < 1000)
            //{
            //    ropeStaminaText.text = ((int)newHealth).ToString();
            //}
            //else if (newHealth < 1000000)
            //{
            //    ropeStaminaText.text = ((int)newHealth / 1000).ToString() + "." + (((int)newHealth / 10) % 100).ToString() + "k";
            //}
            //else
            //{
            //    ropeStaminaText.text = ((int)newHealth / 1000000).ToString() + "." + (((int)newHealth / 10000) % 100).ToString() + "m";
            //}
            yield return null;
        }

        ropeStaminaText.text = FactorCalculator.TextConverter((int)newAmount, 2);

        //if (newAmount < 1000)
        //{
        //    ropeStaminaText.text = ((int)newAmount).ToString();
        //}
        //else if (newAmount < 1000000)

        //{
        //    ropeStaminaText.text = ((int)newAmount / 1000).ToString() + "." + (((int)newAmount / 10) % 100).ToString() + "k";
        //}
        //else
        //{
        //    ropeStaminaText.text = ((int)newAmount / 1000000).ToString() + "." + (((int)newAmount / 10000) % 100).ToString() + "M";

        //}
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
                StartCoroutine(RopeCut());
            }
            if (box != null)
            {
                BoxBlob();
            }
        }
    }
    IEnumerator RopeCut()
    {
        ropeActive = false;
        locked.SetActive(false);
        StartCoroutine(MoneyCanvasSpawn());
        if (box != null)
        {
            BoxOpenAnims();
            yield return new WaitForSeconds(2f);
            StartCoroutine(BoxClosed());
        }
        else
        {
            yield return null;
        }
        VibratoManager.Instance.HeavyViration();
        rope1.enabled = false;
        rope2.enabled = false;
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
    IEnumerator MoneyCanvasSpawn()
    {
        yield return new WaitForSeconds(0.5f);
        //moneyCanvas.Instance.moneySpawn(moneyIconPos.position, moneyIconCount, ropeSettings._coin[PlayerPrefs.GetInt("level")]);
        moneyCanvas.Instance.moneySpawn(moneyIconPos.position, moneyIconCount, ropeSettings._ropeIDCoin[ropeID] * (int)Globals.moneyAmount / 100);
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.MoneyUpdate(ropeSettings._ropeIDCoin[ropeID] * (int)Globals.moneyAmount / 100);

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

    public Tween DoGetValuePos(Transform tr, bool active, float value, float lastValue, float duration, DG.Tweening.Ease type)
    {
        Vector3 firstPos = tr.position;
        Tween tween = DOTween.To
            (() => value, x => value = x, lastValue, duration).SetEase(type).OnUpdate(delegate ()
            {
                tr.position = new Vector3(firstPos.x, firstPos.y - value, 1.1f * value);
            });
        return tween;
    }
    IEnumerator Oscillation()
    {
        while (true)
        {
            float counter = 0;
            while (counter < oscilPeriod)
            {
                counter += Time.deltaTime;
                rope1.GetComponent<Rigidbody2D>().AddForce(Vector2.up * oscilForce);
                rope2.GetComponent<Rigidbody2D>().AddForce(Vector2.up * oscilForce);
                yield return null;
            }
            yield return new WaitForSeconds(oscilPeriod);
        }
    }
    void BoxOpenAnims()
    {
        box.GetComponent<Animator>().SetBool("open", true);
        DoGetValuePos(box.transform, true, 0, -1.2f, 2f, Ease.OutElastic);

        foreach (ParticleSystem glowParticle in GetComponentsInChildren<ParticleSystem>())
        {
            glowParticle.Play();
        }
        box.transform.parent = null;
        Destroy(box.gameObject, 5f);
    }
    IEnumerator BoxClosed()
    {
        float counter = 0;
        float rotateSpeed = 0;
        Vector3 firstScale = box.localScale;
        while(counter < 1f)
        {
            counter += Time.deltaTime;
            box.localScale = Vector3.Lerp(firstScale, Vector3.zero, counter * counter);
            rotateSpeed = Mathf.Lerp(0, 500, counter * counter);
            box.transform.Rotate(0, rotateSpeed, 0);
            yield return null;
        }
    }
    void BoxBlob()
    {
        box.GetComponent<Animator>().SetFloat("blobspeed", 0.2f + (1 - ropeCurrentHealth / ropeMaxHealth));
    }
}