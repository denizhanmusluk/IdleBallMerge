using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class moneyPanel : MonoBehaviour
{
    public RectTransform target;

    public Vector3 troublePos;
    public int moneyIconAmount;
    [SerializeField] Image money;

    bool panelPosActive = false;
    Vector3 screenPos;
    bool moneyActive = true;
    bool clickActive = true;
    public TextMeshProUGUI earnMonetText;
    public List<GameObject> icons = new List<GameObject>();
    public int EarnMoneyAmount;
    void Start()
    {
        StartCoroutine(panelPos());
        StartCoroutine(panelScaling());
    }
    IEnumerator panelScaling()
    {
        float counter = 0f;
        float scale = 0f;

        while (moneyActive)
        {
            counter +=5* Time.deltaTime;
            scale = 50 * Mathf.Abs(Mathf.Sin(counter));
            GetComponent<RectTransform>().sizeDelta = new Vector2(150 + scale, 200);
            yield return null;
        }
        GetComponent<RectTransform>().sizeDelta = new Vector2(150, 200);

    }
    IEnumerator panelClosing()
    {
        float counter = 0f;
        float scale = 0f;

        while (counter< Mathf.PI/2)
        {
            counter += 2 * Time.deltaTime;
            scale = 50 * Mathf.Abs(Mathf.Sin(counter));
            GetComponent<RectTransform>().sizeDelta = new Vector2(150 + scale, 200 - scale);
            yield return null;
        }
        counter = 0f;
        while (counter < Mathf.PI / 2)
        {
            counter += 2 * Time.deltaTime;
            scale = 50 * Mathf.Abs(Mathf.Sin(counter));
            GetComponent<RectTransform>().sizeDelta = new Vector2(200 - 4 * scale, 150 + 2 * scale);
            yield return null;
        }

    }
    //IEnumerator panelRotate()
    //{
    //    float counter = 0f;
    //    while (counter < 2)
    //    {
    //        counter += Time.deltaTime;
    //        GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 500 * counter * counter, 0);
    //        yield return null;
    //    }

    //}
    IEnumerator panelPos()
    {
        panelPosActive = false;
        yield return null;
        panelPosActive = true;

        while (panelPosActive)
        {
            GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(troublePos);
            yield return null;
        }
    }
    public void button()
    {
        if (clickActive)
        {
            clickActive = false;
            //GetComponent<Image>().enabled = false;
            moneyActive = false;
            StartCoroutine(panelClosing());
            //StartCoroutine(panelRotate());
            earnMonetText.text = FactorCalculator.TextConverter(EarnMoneyAmount, 1);
            screenPos = Camera.main.WorldToScreenPoint(troublePos);
            for (int i = 0; i < moneyIconAmount; i++)
            {
                GameObject mnyIcon = Instantiate(money, new Vector3(screenPos.x, screenPos.y, 0), Quaternion.identity, this.transform).gameObject;
                icons.Add(mnyIcon);
            }

            for (int i = 0; i < moneyIconAmount; i++)
            {
                StartCoroutine(moneyScattering(icons[i].GetComponent<RectTransform>()));
            }
            StartCoroutine(collecting());
            panelPosActive = false;
        }
        //panel.SetActive(false);
    }
    IEnumerator moneyScattering(RectTransform mny)
    {
        float speed = 1000;
        Vector3 targetScat = new Vector3(Random.Range(-Screen.width / 4, Screen.width / 4) + Screen.width / 2, Random.Range(-Screen.height / 4, Screen.height / 4) + Screen.height / 2, 0);
        int screenPosDistance = Random.Range(50, 600);
        while (Vector3.Distance(targetScat, mny.position) > 10f && Vector3.Distance(screenPos, mny.position) < screenPosDistance)
        {
            speed -= 500 * Time.deltaTime;
            mny.position = Vector3.MoveTowards(mny.position, targetScat, speed * Time.deltaTime);
            yield return null;
        }
        //mny.position = targetScat;
    }
    IEnumerator collecting()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(setVal(0, EarnMoneyAmount));
        for (int i = 0; i < moneyIconAmount; i++)
        {
            StartCoroutine(moneyCollect(icons[i].GetComponent<RectTransform>()));
        }
    }
    IEnumerator moneyCollect(RectTransform mny)
    {
        float speed = 1000f;
        while (Vector3.Distance(target.position, mny.position) > 10f)
        {

            speed += 300 * Time.deltaTime;
            mny.position = Vector3.MoveTowards(mny.position, target.position, speed * Time.deltaTime);
            yield return null;
        }
        mny.position = target.position;
        yield return null;
        icons.Remove(mny.gameObject);
        Destroy(mny.gameObject);
        moneyIconAmount--;
        if(moneyIconAmount == 0)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator setVal(int amount, float oldAmount)
    {
        Vector3 firstPos = earnMonetText.transform.position;
        float counter = 0f;
        while (counter < 1f)
        {
            counter += Time.deltaTime;
            float money = Mathf.Lerp(oldAmount, amount, counter);

            earnMonetText.color = new Color(earnMonetText.color.r, earnMonetText.color.g, earnMonetText.color.b, 1 - counter);

            earnMonetText.text = FactorCalculator.TextConverter((long)money, 1);
            earnMonetText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 3, counter);
            earnMonetText.transform.position = Vector3.Lerp(firstPos, firstPos + new Vector3(0,500,0), counter);
            yield return null;
        }
        earnMonetText.text = "";

    }
}
