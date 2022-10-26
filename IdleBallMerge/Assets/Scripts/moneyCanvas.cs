using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyCanvas : MonoBehaviour
{
    public static moneyCanvas Instance;

    [SerializeField] RectTransform target;
    [SerializeField] GameObject panelPrefab;
    [SerializeField] Image money;
    Vector3 screenPos;
    bool panelPosActive = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void moneySpawn(Vector3 troublePos, int _moneyIconAmount,int moneyAmount)
    {
        GameObject pnl = Instantiate(panelPrefab, this.transform);
        //panel.SetActive(true);
        moneyPanel _moneyPanel = pnl.GetComponent<moneyPanel>();
        _moneyPanel.troublePos = troublePos;
        _moneyPanel.moneyIconAmount = _moneyIconAmount;
        _moneyPanel.target = target;
        _moneyPanel.EarnMoneyAmount = moneyAmount;
        _moneyPanel.earnMonetText.text = FactorCalculator.TextConverter(moneyAmount, 1);

        StartCoroutine(ButtonAutoClick(_moneyPanel));

    }
    IEnumerator ButtonAutoClick(moneyPanel pnl)
    {
        yield return new WaitForSeconds(0.3f);
        pnl.button();
    }
}
