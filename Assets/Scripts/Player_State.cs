using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private Rigidbody2D playerRigidbody;
    private Collider2D playerCollider;
    private float throwForce = 10f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Die()
    {
        //Generation to the rantom angle
        float randomAngle = Random.Range(0f, 180f);

        // Calculating the direction with a random angle
        Vector2 throwDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));

        // Apply the force
        playerRigidbody.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

        // Remove the collider
        playerCollider.enabled = false;
        StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        return new WaitForSecondsRealtime(3f);
        this.gameObject.SetActive(false);
        
    }

}
