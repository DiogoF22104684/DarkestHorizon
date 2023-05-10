using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private Player_Movement playerMovement;
    private Vector2 bulletScale;
    private bool directionBool;


    void Awake()
    {
        directionBool = playerMovement.facingRight;
        bulletScale = transform.localScale;

        if (directionBool)
        {
            bulletScale.x = 1f;
        }
        else
        {
            bulletScale.x = -1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
            Destroy(gameObject);

    }
}
