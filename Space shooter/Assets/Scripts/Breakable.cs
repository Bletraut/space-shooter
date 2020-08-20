using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Breakable : MonoBehaviour
{
    public float Health;
    public float maxHealth;

    public bool Undead = false;
    //public void SetUndead(bool flag) { Undead = flag; }
    public void SetUndead(bool flag) => Undead = flag;

    [SerializeField]
    private List<ArmorData> armors = new List<ArmorData>();

    public GameObject DeadFX;

    public UnityEvent OnDestroy;
    public UnityEvent OnDamage;

    private void OnEnable()
    {
        Health = maxHealth;
    }

    public void ApplyDamage(List<DamageData> damages)
    {
        if (Undead) return;

        float resultDamage = 0;
        foreach (var damageData in damages)
        {
            var damage = damageData.Damage;
            foreach (var armor in armors) damage *= armor.GetDefense(damageData);

            resultDamage += damage;
        }
        Health -= resultDamage;
        OnDamage?.Invoke();

        if (Health <= 0) DestroyObject();
    }

    public void AddArmor(ArmorData armor) => armors.Add(armor);
    public void RemoveArmor(ArmorData armor) => armors.Remove(armor);

    public void DestroyObject()
    {
        OnDestroy?.Invoke();
        var fx = ObjectsPool.Create(DeadFX);
        fx.transform.position = transform.position;

        ObjectsPool.CheckAndDestroy(gameObject);
    }
}
