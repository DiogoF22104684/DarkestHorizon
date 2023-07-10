using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFollow : Alien
{
    [SerializeField]
    private float sightRadius = 200.0f;
    Player player;

    override protected void Update()
    {
        UpdateSensors();

        Vector2 currentVelocity = rb.velocity;
        if (onGround)
        {
            if (player == null)
            {
                player = FindObjectOfType<Player>();
            }
            bool playerDetected = false;
            if (player != null)
            {
                float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
                if ((distance > sightRadius * 0.2f) && (distance < sightRadius))
                {
                    Vector3 raycastPosition = transform.position;
                    raycastPosition.y += 10;

                    Vector3 toPlayer = (player.transform.position + Vector3.up * 10) - raycastPosition;
                    distance = toPlayer.magnitude;
                    toPlayer.Normalize();

                    RaycastHit2D hit = Physics2D.Raycast(raycastPosition, toPlayer, distance, groundMask);
                    if (hit.collider == null)
                    {
                        if (Vector3.Dot(toPlayer, transform.right) < 0)
                        {
                            transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
                        }
                        playerDetected = true;
                    }
                }
            }
            if (!playerDetected)
            {
                if (DetectWallAndNotGround())
                {
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
                }
            }
            currentVelocity.x = moveSpeed * transform.right.x;
        }

        rb.velocity = currentVelocity;

        UpdateVisuals();
    }

    override protected void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, sightRadius * 0.2f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);

        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        if (player != null)
        {
            float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
            if ((distance > sightRadius * 0.2f) && (distance < sightRadius))
            {
                Vector3 raycastPosition = transform.position;
                raycastPosition.y += 10;

                Vector3 toPlayer = (player.transform.position + Vector3.up * 10) - raycastPosition;
                distance = toPlayer.magnitude;
                toPlayer.Normalize();

                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(raycastPosition, raycastPosition + toPlayer * distance);
            }
        }
    }
}
