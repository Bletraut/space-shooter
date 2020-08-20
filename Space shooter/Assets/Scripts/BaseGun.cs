using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : Gun
{
    private void Awake()
    {
        nextFire = Time.time + GunData.FireRate;
    }

    private float nextFire = 0;
    public override void Fire()
    {
        if (Time.time > nextFire)
        {
            var bullet = ObjectsPool.Create(GunData.BulletData.BulletPrefab);
            bullet.transform.position = ShotPoint.transform.position;

            nextFire = Time.time + GunData.FireRate;
        }
    }
}
