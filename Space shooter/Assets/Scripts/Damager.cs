using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField]
    private List<DamageData> damagesTypes = new List<DamageData>();

    public void Strike(GameObject collision)
    {
        collision.gameObject.BroadcastMessage("ApplyDamage", damagesTypes, SendMessageOptions.DontRequireReceiver);
    }

    public void AddDamageType(DamageData damageData) => damagesTypes.Add(damageData);
    public void RemoveDamageType(DamageData damageData) => damagesTypes.Remove(damageData);
}
