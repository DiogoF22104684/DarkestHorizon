using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private Rigidbody2D playerRigidbody;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private Player_Movement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerMovement = gameObject.GetComponent<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Die();
        }
    }

    private void Die()
    {
        playerMovement.enabled = false;

        // Vector2 throwDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Vector2 throwDirection = Vector2.left + Vector2.up;
        // Apply the force to the player in the random direction
        playerRigidbody.velocity = throwDirection * throwForce;

        StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);


    }

}
