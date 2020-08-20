using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Data", menuName = "Space Shooter/Bullet Data", order = 2)]
public class BulletData : ScriptableObject
{
    public GameObject BulletPrefab;
    public float Speed = 3;
}
