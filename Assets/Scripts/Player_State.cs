using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private Rigidbody2D playerRigidbody;
    [SerializeField] private float throwForce = 10f;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
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
        Vector2 throwDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // Apply the force to the player in the random direction
        playerRigidbody.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

        StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }

}
