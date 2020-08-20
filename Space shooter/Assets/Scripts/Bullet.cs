using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletData BulletData;

    void FixedUpdate()
    {
        transform.Translate(transform.up * BulletData.Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GetComponent<Damager>()?.Strike(collision.gameObject);
            ObjectsPool.CheckAndDestroy(gameObject);
        }
    }
}
