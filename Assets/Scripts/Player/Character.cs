using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected int         maxHealth = 3;
    [SerializeField]
    protected float       invulnerabilityDuration = 2;
    [SerializeField]
    protected float       blinkDuration = 0.1f;
    [SerializeField]
    protected float       moveSpeed = 1.0f;
    [SerializeField]
    protected Transform   groundDetector;
    [SerializeField]
    protected float       groundDetectorRadius = 2;
    [SerializeField]
    protected float       groundDetectorExtraRadius = 6.0f;
    [SerializeField]
    protected LayerMask   groundMask;

    protected Rigidbody2D     rb;
    protected Animator        animator;
    protected SpriteRenderer  spriteRenderer;
    protected bool            onGround = false;
    protected float           lastGroundTime;
    protected int             health;
    protected float           invulnerabilityTimer = 0;
    protected float           blinkTimer = 0;
    private Vector2           characterScale;

    virtual protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterScale = transform.localScale;
        health = maxHealth;
    }

    protected void UpdateSensors()
    {
        DetectGround();

        if (onGround)
        {
            lastGroundTime = Time.time;
        }
    }

    protected void UpdateVisuals()
    {
        Vector2 currentVelocity = rb.velocity;

        // Change visuals
        if (animator != null)
        {
            animator.SetFloat("AbsVelocityX", Mathf.Abs(currentVelocity.x));
            animator.SetFloat("VelocityY", currentVelocity.y);
            animator.SetBool("OnGround", onGround);
        }

        TurnCharacter();

        if (invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if (invulnerabilityTimer <= 0)
            {
                spriteRenderer.enabled = true;
            }
            else
            {
                blinkTimer -= Time.deltaTime;
                if (blinkTimer <= 0)
                {
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                    blinkTimer = blinkDuration;
                }
            }
        }
    }

    private void Update()
    {
        UpdateSensors();

        UpdateVisuals();
    }

    void TurnCharacter()
    {
        float speedX = rb.velocity.x;

        if (speedX < 0)
        {
            if (transform.right.x > 0)
            {
                characterScale.x = -1;
                transform.localScale = characterScale;
            }
        }
        else if (speedX > 0)
        {
            if (transform.right.x < 0)
            {
                characterScale.x = 1;
                transform.localScale = characterScale;
            }
        }
    }

    virtual protected bool ValidateGround(Collider2D collider)
    {
        return true;
    }

    void DetectGround()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundDetector.position, groundDetectorRadius, groundMask);
        if (collider != null)
        {
            if (ValidateGround(collider))
            {
                onGround = true;
            }
        }
        else
        {
            collider = Physics2D.OverlapCircle(groundDetector.position - Vector3.right * groundDetectorExtraRadius, groundDetectorRadius, groundMask);
            if (collider != null) onGround = true;
            else
            {
                collider = Physics2D.OverlapCircle(groundDetector.position + Vector3.right * groundDetectorExtraRadius, groundDetectorRadius, groundMask);
                if (collider != null) onGround = true;
                else onGround = false;
            }
        }
    }

    public int GetHealth()
    {
        return health;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public bool IsOnGround()
    {
        return onGround;
    }

    virtual public void DealDamage(int damage, GameObject damageDealer, Animator animator)
    {
        if (invulnerabilityTimer > 0) return;
        
        health = health - damage;
        if (health <= 0)
        {
            animator.SetTrigger("Death");
        }

        invulnerabilityTimer = invulnerabilityDuration;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
    
    virtual protected void OnDrawGizmos()
    {
        if (groundDetector == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundDetector.position, groundDetectorRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundDetector.position - Vector3.right * groundDetectorExtraRadius, groundDetectorRadius);
        Gizmos.DrawSphere(groundDetector.position + Vector3.right * groundDetectorExtraRadius, groundDetectorRadius);
    }
}
