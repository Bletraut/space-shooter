using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 3;
    public Bounds Bounds;

    public VariableJoystick Joystick;

    // Update is called once per frame
    void FixedUpdate()
    {
        var movX = Input.GetAxis("Horizontal");
        var movY = Input.GetAxis("Vertical");
        var movement = new Vector3(movX, movY);

        if (movement == Vector3.zero && Joystick != null) movement = new Vector3(Joystick.Horizontal, Joystick.Vertical);

        transform.Translate(movement * Speed * Time.fixedDeltaTime);

        var clampPosition = transform.position;
        clampPosition.x = Mathf.Clamp(clampPosition.x, Bounds.xMin, Bounds.xMax);
        clampPosition.y = Mathf.Clamp(clampPosition.y, Bounds.yMin, Bounds.yMax);
        transform.position = clampPosition;
    }
}
