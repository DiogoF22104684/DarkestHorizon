using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : Enemy
{
    // Update is called once per frame
    virtual protected void Update()
    {
        UpdateSensors();

        Vector2 currentVelocity = rb.velocity;
        if (onGround)
        {
            if (DetectWallAndNotGround())
            {
                transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
            }
            currentVelocity.x = moveSpeed * transform.right.x;
        }

        rb.velocity = currentVelocity;

        UpdateVisuals();
    }
}
