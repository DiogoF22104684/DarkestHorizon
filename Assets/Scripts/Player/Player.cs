using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    [SerializeField]
    private float       jumpVelocity = 200.0f;
    [SerializeField]
    private float       maxJumpTime = 0.1f;
    [SerializeField]
    private float       jumpGravity = 1.0f;
    [SerializeField]
    private int         maxJumps = 1;
    [SerializeField]
    private float       coyoteTime = 0.1f;
    [SerializeField]
    private Collider2D  groundCollider;
    [SerializeField]
    private Collider2D  airCollider;

    private float           lastJumpTime;
    private float           initialGravity;
    private int             nJumps = 0;
    private Vector2         trueCharacterScale;
    private bool            facingRight = true;

    private Dictionary<GameObject, float>   hitTime;

    override protected void Awake()
    {
        base.Awake();

        initialGravity = rb.gravityScale;
        trueCharacterScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
    
    
        UpdateSensors();

        Vector2 currentVelocity = rb.velocity;
        float speedX = Input.GetAxis("Horizontal");

        if ((Time.time - lastGroundTime) <= coyoteTime)
        {
            if (currentVelocity.y <= 0)
            {
                nJumps = maxJumps;
            }
        }
        else
        {
            if (nJumps == maxJumps)
            {
                nJumps = 0;
            }
        }

        if (Input.GetAxis("Horizontal") > 0 && !facingRight)
        {
            trueCharacterScale.x = 1;
            transform.localScale = trueCharacterScale;
            facingRight = !facingRight;
        }
        else if (Input.GetAxis("Horizontal") < 0 && facingRight)
        {
            trueCharacterScale.x = -1;
            transform.localScale = trueCharacterScale;
            facingRight = !facingRight;
        }

        groundCollider.enabled = onGround;
        airCollider.enabled = !onGround;

        currentVelocity.x = speedX * moveSpeed;

        if ((Input.GetButtonDown("Jump")) && (nJumps > 0))
        {
            currentVelocity.y = jumpVelocity;
            lastJumpTime = Time.time;
            lastGroundTime = 0;
            rb.gravityScale = jumpGravity;
            nJumps--;
        }
        else if ((Input.GetButton("Jump")) && ((Time.time - lastJumpTime) < maxJumpTime) && (currentVelocity.y > 0))
        {
            rb.gravityScale = jumpGravity;
        }
        else
        {
            rb.gravityScale = initialGravity;
            lastJumpTime = 0;
        }

        rb.velocity = currentVelocity;

        UpdateVisuals();
    }

    override protected bool ValidateGround(Collider2D collider)
    {
        Ant ant = collider.GetComponent<Ant>();
        if ((ant != null) && (rb.velocity.y < -1e-3))
        {
            ant.DealDamage(1, gameObject, collider.gameObject.GetComponent<Animator>());

            Vector2 currentVelocity = rb.velocity;
            currentVelocity.y = jumpVelocity;
            rb.velocity = currentVelocity;

            invulnerabilityTimer = Mathf.Max(invulnerabilityTimer, Time.deltaTime + 0.01f);

            return false;
        }

        return true;
    }

    public void SetMaxJumps(int n)
    {
        maxJumps = n;
    }

    override public void DealDamage(int damage, GameObject damageDealer, Animator animator)
    {
        if (invulnerabilityTimer > 0) return;

        if (hitTime != null)
        {
            float t;
            if (hitTime.TryGetValue(damageDealer, out t))
            {
                if ((Time.time - t) < 1.0f)
                {
                    return;
                }
            }
        }

        health = health - damage;
        if (health == 0)
        {
            animator.SetTrigger("Death");
        }

        if (hitTime == null) hitTime = new Dictionary<GameObject, float>();
        hitTime[damageDealer] = Time.time;

        invulnerabilityTimer = invulnerabilityDuration;
    }
}
