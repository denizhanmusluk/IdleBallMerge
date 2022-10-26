using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/Set_Upgrade")]

public class SetUpgrade : ScriptableObject
{
    [Header("       BALL UPGRADE")]
    [SerializeField] private GameObject[] ballPrefab;
    public GameObject[] _ballPrefab { get { return ballPrefab; } }

    [SerializeField] private float[] radius;
    public float[] _radius { get { return radius; } }

    [SerializeField] private float[] mass;
    public float[] _mass { get { return mass; } }



    [SerializeField] private int coinPerBallMultiple;
    public int _coinPerBallMultiple { get { return coinPerBallMultiple; } }

    [SerializeField] private int[] ballUpgradeCost;
    public int[] _ballUpgradeCost { get { return ballUpgradeCost; } }



    [Header("       MERGE BALL")]

    [SerializeField] private int[] mergeCost;
    public int[] _mergeCost { get { return mergeCost; } }



    [Header("       SPEED UPGRADE")]
    [SerializeField] private float[] clickSpawnSpeed;
    public float[] _clickSpawnSpeed { get { return clickSpawnSpeed; } }

    [SerializeField] private int[] clickSpeedUpgradeCost;
    public int[] _clickSpeedUpgradeCost { get { return clickSpeedUpgradeCost; } }
}
