using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public GunData GunData;
    public Transform ShotPoint;

    public abstract void Fire();
}
