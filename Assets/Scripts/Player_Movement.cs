using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
                     private Rigidbody2D    rb;
    [SerializeField] private float          jumpForce = 100f, speed = 10f;
                     private Vector2        movement;
    [SerializeField] private PlayerState    state = PlayerState.walking;
                     private Vector2        playerScale;
    [SerializeField] private bool           facingRight = true;
    [SerializeField] private GameObject     gunPrefab;


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
        //Needs fixing
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

    public void GunTypeReciever(Sprite sprite)
    {
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprite;
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
