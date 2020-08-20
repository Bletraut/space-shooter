using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Gun[] Guns = new Gun[0];
    public bool IsSolidFire = true;
    public bool IsCanFire = true;

    private void Update()
    {
        if (!IsCanFire) return;

        if (IsSolidFire 
            || (!IsSolidFire && Input.GetButton("Fire1"))) FireFromAllGuns();
    }

    private void FireFromAllGuns()
    {
        foreach (var gun in Guns) gun.Fire();
    }
}
