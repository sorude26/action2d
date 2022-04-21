using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    None,
}
public class DamageBundle : ScriptableObject
{
    [SerializeField]
    public int damage = 1;
    [SerializeField]
    public DamageType type = DamageType.None;
    [SerializeField]
    public float effect = 100;

    public Transform attackerTrans = null;
}
