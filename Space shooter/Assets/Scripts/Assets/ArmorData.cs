using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Protection
{
    public DamageData DamageData;
    [Range(0, 1)]
    public float Defense;
}

[CreateAssetMenu(fileName = "Armor Data", menuName = "Space Shooter/Armor Data", order = 2)]
public class ArmorData : ScriptableObject
{
    public Protection[] Protections = new Protection[0];

    public virtual float GetDefense(DamageData damageData)
    {
        float defense = 1;
        foreach (var p in Protections) if (p.DamageData == damageData) defense *= 1 - p.Defense;

        return defense;
    }
}
