using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Data", menuName = "Space Shooter/Gun Data", order = 3)]
public class GunData: ScriptableObject
{
    public string Name;
    public BulletData BulletData;
    public float FireRate;
}
