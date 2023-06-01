using UnityEngine;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
                     private Rigidbody2D    rb;
    [SerializeField] private float          jumpForce = 100f, speed = 10f;
                     private Vector2        movement;
    [SerializeField] private PlayerState    state = PlayerState.Walking;
                     private Vector2        playerScale;
    [SerializeField] public bool            facingRight = true;
    [SerializeField] private GameObject     gunPrefab;

    // Bullet information
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletSpread = 1f;
    [SerializeField] private Transform firePoint;
                     private float nextFireTime;

    //Gun information
    private int counter = 10;
    private Sprite gunSprite;
    private bool hasBullets;
    private float gunSpeed = 5f;

    //UI information
    [SerializeField] private Text ammoText;

    private enum PlayerState
    {
        Jumping,
        Walking
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerScale = transform.localScale;
        ammoText = GameObject.FindGameObjectWithTag("Ammo Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyInput();
    }

    #region Movement

    private void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.T))
            Die();

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
        }

        if(state == PlayerState.Walking)
        {
            movement = new Vector2(Input.GetAxis("Horizontal"), 0);
            rb.velocity = (movement * speed) + (Vector2.down * 9.8f);
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
        state = PlayerState.Jumping;
        Debug.Log("Now jumping");
    }

    #endregion

    private void Shoot()
    {

        AmmoCounterCheck();

        if (hasBullets)
        {
            nextFireTime = Time.time + 1f / fireRate;

            Vector2 bulletDirection = (facingRight ? Vector2.right : Vector2.left);
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, Random.Range(-bulletSpread, bulletSpread));

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

            Destroy(bullet, 2f);
        }
    }

    public void GunTypeReceiver(Sprite sprite, float weaponFireRate)
    {
        fireRate = weaponFireRate;
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprite;
        gunSprite = sprite;
        hasBullets = true;
        counter = 11;
        AmmoCounterCheck();
    }

    private void ThrowGun()
    {
        Sprite gunHeld = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        
        GameObject newGun = Instantiate(gunPrefab, firePoint.position, Quaternion.identity);
        Vector3 gunDirection = (facingRight ? Vector2.right : Vector2.left);
        newGun.GetComponent<Rigidbody2D>().velocity = gunDirection * gunSpeed;

        SpriteRenderer spriteRenderer = gunPrefab.GetComponent<SpriteRenderer>();

        if (gunSprite)
        {
            spriteRenderer.sprite = gunHeld;
        }
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = null;

        Destroy(newGun, 2f);

    }

    private void AmmoCounterCheck()
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

    private void Die()
    {
        counter = 0;
        AmmoCounterCheck();
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") && state == PlayerState.Jumping)
        {
            state = PlayerState.Walking;
            Debug.Log("Now walking");
        }

        if (collision.gameObject.CompareTag("Gun"))
        {
            collision.gameObject.GetComponent<GunPickup>().GunTypeInfo();
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Fall") && collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }
}
