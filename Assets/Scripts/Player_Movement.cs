using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D     rb;
    [SerializeField]
    private float           jumpForce = 100f, speed = 10f;
    private float           movement;
    private PlayerState     state = PlayerState.walking;
    private Vector2         playerScale;
    [SerializeField]
    private bool            facingRight = true;


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
        Debug.Log(state);
    }

    private void KeyInput()
    {
        //Needs fixing
        //if(state == PlayerState.walking)
        {
            movement = Input.GetAxis("Horizontal") * Time.deltaTime;
            transform.Translate(movement, 0, 0);
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
            state = PlayerState.walking;
    }

}
