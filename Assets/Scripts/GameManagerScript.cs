using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerCheck();
    }

    private void PlayerCheck()
    {
        bool playerDead = GameObject.FindGameObjectsWithTag("Player").Length == 0;
        if(playerDead)
        {
        Debug.Log("Player Dead");
        }
    }
}
