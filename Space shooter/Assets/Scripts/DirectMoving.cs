using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectMoving : MonoBehaviour
{
    public float Speed = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        transform.Translate(transform.up * Speed * Time.fixedDeltaTime);
    }
}
