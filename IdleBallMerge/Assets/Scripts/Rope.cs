using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Rope : MonoBehaviour
{
    bool ropeActive = true;
    [SerializeField] float ropeMaxHealth;
    [SerializeField] public float ropeCurrentHealth;
    [SerializeField] public float totalMass;
    [SerializeField] TextMeshProUGUI ropeStaminaText;
    [SerializeField] RelativeJoint2D rope1, rope2;
    [SerializeField] GameObject locked;
    void Start()
    {
        Debug.Log("rope");
        ropeCurrentHealth = ropeMaxHealth;
        InitText(ropeCurrentHealth);
        BallCreator.Instance.ropes.Add(this);
    }
    void InitText(float health)
    {
        ropeStaminaText.text = ((int)(health)).ToString();
    }
    public void MassUpdate()
    {
        if (ropeActive)
        {
            ropeCurrentHealth = ropeMaxHealth - totalMass;
            if (ropeCurrentHealth > 0)
            {
                InitText(ropeCurrentHealth);
            }
            else
            {
                InitText(0);
                RopeCut();
            }
        }
    }
    void RopeCut()
    {
        ropeActive = false;
        rope1.enabled = false;
        rope2.enabled = false;
        locked.SetActive(false);
        foreach (var rp in GetComponentsInChildren<FixedJoint2D>())
        {
            rp.frequency = 1;
        }
        GameManager.Instance.ui.WinLevel();
    }
}