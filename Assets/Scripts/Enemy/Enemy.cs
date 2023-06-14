using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] 
    protected Transform wallDetector;
    [SerializeField] 
    private Transform   groundAheadDetector;

    protected bool DetectWallAndNotGround()
    {
        Collider2D collider = Physics2D.OverlapCircle(wallDetector.position, groundDetectorRadius, groundMask);
        bool wallAhead = (collider != null);

        if (wallAhead)
        {
            return true;
        }
        else
        {
            if (groundAheadDetector != null)
            {
                collider = Physics2D.OverlapCircle(groundAheadDetector.position, groundDetectorRadius, groundMask);

                if (collider == null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
            if (!player.IsOnGround())
            {
                if (playerRB.velocity.y < -1e-3) return;
            }

            player.DealDamage(1, gameObject, collision.gameObject.GetComponent<Animator>());
        }
    }

    override protected void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (wallDetector != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(wallDetector.position, groundDetectorRadius);
        }

        if (groundAheadDetector != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(groundAheadDetector.position, groundDetectorRadius);
        }
    }
}
