using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GunInfo : MonoBehaviour
{
    [SerializeField] private GameObject     gunPrefab;

    // Bullet information
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float speed = 10f;
    [SerializeField] public int damage = 10;
    [SerializeField] private Transform firePoint;
                     private float nextFireTime;
                     public bool facingRight = true;

    //Gun information
    private int counter = 10;
    private Sprite gunSprite;
    private bool hasBullets;
    private float gunSpeed = 5f;
    private bool isInfinite = false;
    [SerializeField] private Sprite pistolSprite;

    //UI information
    [SerializeField] private Text ammoText;
    [SerializeField] private Image gunUI;

    private void Awake()
    {
        ammoText = GameObject.FindObjectOfType<AmmoNumber>().GetComponent<Text>();
        gunUI = ammoText.GetComponentInParent<Image>();
        GunTypeReceiver(pistolSprite, 2f, 10f, 1, 10, true);
    }

    // Update is called once per frame
    void Update()
    {
        KeyInput();
    }
    
    private void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.T))
            GetComponent<Animator>().SetTrigger("Death");

        if (Input.GetAxis("Horizontal") > 0 && !facingRight)
        {
            facingRight = !facingRight;
        }
        else if (Input.GetAxis("Horizontal") < 0 && facingRight)
        {
            facingRight = !facingRight;
        }
        
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }
    
#region Shooting
    private void Shoot()
    {
        AmmoCounterCheck();

        if (hasBullets)
        {
            nextFireTime = Time.time + 1f / fireRate;

            Vector2 bulletDirection = (facingRight ? Vector2.right : Vector2.left);
            Quaternion bulletRotation = (facingRight ? Quaternion.Euler(0f, 0f, 0) : Quaternion.Euler(0f, 0f, 180));

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * speed;

            Destroy(bullet, 2f);
        }
    }

    public void GunTypeReceiver(Sprite sprite, float weaponFireRate, float bulletSpeed, int bulletDamage, int bulletCount, bool infiniteAmmo)
    {
        isInfinite = infiniteAmmo;
        fireRate = weaponFireRate;
        damage = bulletDamage;
        speed = bulletSpeed;
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
        gunSprite = sprite;
        gunUI.sprite = sprite;
        gunUI.color = Color.white;  
        hasBullets = true;
        counter = bulletCount;
        AmmoCounterCheck();
    }

    private void ThrowGun()
    {
        Sprite gunHeld = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        gunPrefab.GetComponent<SpriteRenderer>().sprite = gunHeld;
        GameObject newGun = Instantiate(gunPrefab, firePoint.position, Quaternion.identity);
        Vector3 gunDirection = (facingRight ? Vector2.right : Vector2.left);
        newGun.GetComponent<Rigidbody2D>().velocity = gunDirection * gunSpeed;

        SpriteRenderer spriteRenderer = gunPrefab.GetComponent<SpriteRenderer>();

        if (gunSprite)
        {
            spriteRenderer.sprite = gunHeld;
        }
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        gunUI.sprite = null;
        gunUI.color = Color.clear;
        Destroy(newGun, 2f);
        GunTypeReceiver(pistolSprite, 2f, 10f, 1, 10, true);
    }

    private void AmmoCounterCheck()
    {
        if (isInfinite)
        {
            ammoText.text = "∞";
        }
        else
        {
            if(counter > 0)
            {
                counter -= 1;
                ammoText.text = counter.ToString();
            }
            else if (counter <= 0 && hasBullets)
            {
                hasBullets = false;
                ammoText.text = "0";
                ThrowGun();
            }
        }
    }

    #endregion
    
    private void Die()
    {
        counter = 0;
        AmmoCounterCheck();
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {
            collision.gameObject.GetComponent<GunPickup>().GunTypeInfo();
            Destroy(collision.gameObject);
        }
    }
}
