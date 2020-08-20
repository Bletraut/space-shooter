using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryOut : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ObjectsPool.Contains(collision.gameObject)) collision.gameObject.SetActive(false);
        else Destroy(collision.gameObject);
    }
}
