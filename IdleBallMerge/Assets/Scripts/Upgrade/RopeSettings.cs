using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Rope/Rope_Settings")]
public class RopeSettings : ScriptableObject
{
    [Header("       ROPE HEALTH")]
    [SerializeField] private int[] ropeMaxHealth;
    public int[] _ropeMaxHealth { get { return ropeMaxHealth; } }
}
