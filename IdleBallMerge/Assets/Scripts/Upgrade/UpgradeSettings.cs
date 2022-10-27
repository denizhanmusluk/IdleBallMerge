using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/Upgrade_Settings")]

public class UpgradeSettings : ScriptableObject
{


    [Header("       BALL UPGRADE")]
    [SerializeField] private GameObject[] ballPrefab;
    public GameObject[] _ballPrefab { get { return ballPrefab; } }

    [SerializeField] private float[] radius;
    public float[] _radius { get { return radius; } }

    [SerializeField] private float[] mass;
    public float[] _mass { get { return mass; } }
    //[SerializeField] private int[] brickCount;
    //public int[] _brickCount { get { return brickCount; } }

    [SerializeField] private int[] coinPerBall;
    public int[] _coinPerBall { get { return coinPerBall; } }

    [SerializeField] private int[] ballUpgradeCost;
    public int[] _ballUpgradeCost { get { return ballUpgradeCost; } }


    [Header("       MERGE BALL")]


    [SerializeField] private int[] mergeCost;
    public int[] _mergeCost { get { return mergeCost; } }

    //[SerializeField] private int[] healthDownPerSeconds;
    //public int[] _healthDownPerSeconds { get { return healthDownPerSeconds; } }

    //[SerializeField] private int[] coolDownPerSeconds;
    //public int[] _coolDownPerSeconds { get { return coolDownPerSeconds; } }

    //[SerializeField] private int[] staminaUpgradeCost;
    //public int[] _staminaUpgradeCost { get { return staminaUpgradeCost; } }

    [Header("       SPEED UPGRADE")]
    [SerializeField] private float[] clickSpawnSpeed;
    public float[] _clickSpawnSpeed { get { return clickSpawnSpeed; } }

    [SerializeField] private long[] clickSpeedUpgradeCost;
    public long[] _clickSpeedUpgradeCost { get { return clickSpeedUpgradeCost; } }
}
