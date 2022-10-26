using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Rope/Rope_Settings")]
public class RopeSettings : ScriptableObject
{
    [Header("       ROPE HEALTH")]
    [SerializeField] private int[] ropeMaxHealth;
    public int[] _ropeMaxHealth { get { return ropeMaxHealth; } }

    [Header("       ROPE COIN")]
    [SerializeField] private int[] coin;
    public int[] _coin { get { return coin; } }



    [Header("        COIN")]
    //private int[] ropeIDCoin;
    public int[] _ropeIDCoin = new int[6];



    [Header("       Rope COIN")]
    [SerializeField] private int ropeCoin;
    //public int _ropeID { get { return ropeCoin; } }
    


    [Header("       Rope + Box COIN")]
    [SerializeField] private int ropeBoxCoin;


    [Header("       Rope + Key COIN")]
    [SerializeField] private int ropeKeyCoin;


    [Header("       Chain COIN")]
    [SerializeField] private int chainCoin;


    [Header("       Chain + Box COIN")]
    [SerializeField] private int chainBoxCoin;


    [Header("       Chain + Key COIN")]
    [SerializeField] private int chainKeyCoin;

    private void Awake()
    {
        _ropeIDCoin[0] = ropeCoin;
        _ropeIDCoin[1] = ropeBoxCoin;
        _ropeIDCoin[2] = ropeKeyCoin;
        _ropeIDCoin[3] = chainCoin;
        _ropeIDCoin[4] = chainBoxCoin;
        _ropeIDCoin[5] = chainKeyCoin;

    }
}
