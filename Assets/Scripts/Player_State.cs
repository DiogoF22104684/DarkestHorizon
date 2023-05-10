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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            Die();
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    /* for later use with the animation
    
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(3f);
    }
    
     */
    

}
