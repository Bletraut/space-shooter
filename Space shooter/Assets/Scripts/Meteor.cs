using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var damager = GetComponent<Damager>();
            damager?.Strike(collision.gameObject);
            damager?.Strike(gameObject);
        }
    }
}
