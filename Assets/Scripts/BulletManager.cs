using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletManager : MonoBehaviour
{
    [FormerlySerializedAs("playerMovement")] [SerializeField] private GunInfo gunInfo;
    private Vector2 bulletScale;
    private bool directionBool;


    void Awake()
    {
        directionBool = gunInfo.facingRight;
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
