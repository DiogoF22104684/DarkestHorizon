using UnityEngine;
using UnityEngine.Serialization;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GunInfo gunInfo;
    private Vector2 bulletScale;
    private bool directionBool;


    void Awake()
    {
        gunInfo = FindObjectOfType<GunInfo>().GetComponent<GunInfo>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Alien alien = collision.gameObject.GetComponent<Alien>();
            if(alien != null)
            {
                alien.DealDamage(gunInfo.damage, gameObject, collision.gameObject.GetComponent<Animator>());
            }
            Destroy(gameObject);

    }
}
