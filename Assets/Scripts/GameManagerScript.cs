using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localPosition = new Vector2(-8, -3.5f);
        PlayerCheck();
    }

    private void PlayerCheck()
    {
        bool playerDead = GameObject.FindGameObjectsWithTag("Player").Length == 0;
        if(playerDead)
        {
            Instantiate(PlayerPrefab, gameObject.transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
    }
}
