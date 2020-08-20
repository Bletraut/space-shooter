using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class ObjectsPool
{
    static public int Size = 20;
    static private Dictionary<GameObject, List<GameObject>> pool = new Dictionary<GameObject, List<GameObject>>();

    static public GameObject Create(GameObject gameObject)
    {
        if (pool.ContainsKey(gameObject))
        {
            var pooled = pool[gameObject].FirstOrDefault(n => !n.activeInHierarchy);
            if (pooled != null)
            {
                pooled.SetActive(true);
                return pooled;
            }
        }
        else pool.Add(gameObject, new List<GameObject>() { });

        var obj = Object.Instantiate(gameObject);
        if (pool[gameObject].Count < Size) pool[gameObject].Add(obj);
        return obj;
    }

    static public bool Contains(GameObject gameObject) => pool.Keys.Any(key => pool[key].Any(obj => obj == gameObject));
    static public void CheckAndDestroy(GameObject gameObject)
    {
        if (Contains(gameObject)) gameObject.SetActive(false);
        else Object.Destroy(gameObject);
    }

    static public void Clear() => pool.Clear();
}
