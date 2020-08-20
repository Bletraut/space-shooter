using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Data", menuName = "Space Shooter/Damage Data", order = 2)]
public class DamageData : ScriptableObject
{
    public string Name;
    public float Damage;
}
