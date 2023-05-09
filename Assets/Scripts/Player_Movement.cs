using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
                     private Rigidbody2D    rb;
    [SerializeField] private float          jumpForce = 100f, speed = 10f;
                     private Vector2        movement;
    [SerializeField] private PlayerState    state = PlayerState.walking;
                     private Vector2        playerScale;
    [SerializeField] private bool           facingRight = true;
    [SerializeField] private GameObject     gunPrefab;

    // Bullet information
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletSpread = 1f;
    [SerializeField] private Transform firePoint;
                     private float nextFireTime = 0f;

    //Gun information
    private int counter = 10;
    private Sprite gunSprite;
    private bool hasBullets = false;
    private float gunSpeed = 5f;

    //UI information
    [SerializeField] private Text ammoText;

    private enum PlayerState
    {
        jumping,
        walking
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        KeyInput();
    }

    #region Movement

    private void KeyInput()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
        }

        if(state == PlayerState.walking)
        {
            movement = new Vector2(Input.GetAxis("Horizontal"), 0);
            rb.velocity = movement * speed;
            Flip();


            if (Input.GetAxis("Vertical") > 0)
                Jump();
        }
    }

    private void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0 && !facingRight)
        {
            facingRight = !facingRight;
            playerScale.x = 1;
            transform.localScale = playerScale;
        }
        else if (Input.GetAxis("Horizontal") < 0 && facingRight)
        {
            facingRight = !facingRight;
            playerScale.x = -1;
            transform.localScale = playerScale;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
        state = PlayerState.jumping;
        Debug.Log("Now jumping");
    }

    #endregion

    private void Shoot()
    {

        AmmoCounterCheck();

        if (hasBullets)
        {
            nextFireTime = Time.time + 1f / fireRate;

            Vector3 bulletDirection = (facingRight ? Vector3.right : Vector3.left);
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, Random.Range(-bulletSpread, bulletSpread));

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

            Destroy(bullet, 2f);
        }
    }

    public void GunTypeReciever(Sprite sprite)
    {
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprite;
        hasBullets = true;
        counter = 10;
        AmmoCounterCheck();
    }

    private void ThrowGun()
    {

        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = null;
        
        GameObject newGun = Instantiate(gunPrefab, firePoint.position, Quaternion.identity);
        Vector3 gunDirection = (facingRight ? Vector3.right : Vector3.left);
        newGun.GetComponent<Rigidbody2D>().velocity = gunDirection * gunSpeed;

        SpriteRenderer spriteRenderer = gunPrefab.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && gunSprite != null)
        {
            spriteRenderer.sprite = gunSprite;
        }

        Destroy(newGun, 2f);

    }

    private void AmmoCounterCheck()
    {
        if(counter > 0)
        {
            counter -= 1;
            ammoText.text = counter.ToString();
        }
        else if (counter <= 0 && hasBullets == true)
        {
            hasBullets = false;
            ammoText.text = "0";
            ThrowGun();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && state == PlayerState.jumping)
        {
            state = PlayerState.walking;
            Debug.Log("Now walking");
        }

        if (collision.gameObject.tag == "Gun")
        {
            collision.gameObject.GetComponent<GunPickup>().GunTypeInfo();
            Destroy(collision.gameObject);
        }
    }
}
