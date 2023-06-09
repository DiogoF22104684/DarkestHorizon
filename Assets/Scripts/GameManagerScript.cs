using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private CinemachineVirtualCameraBase camera;

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerCheck();
    }

    private void PlayerCheck()
    {
        bool playerDead = GameObject.FindObjectOfType<Player_Movement>() == null;
        if(playerDead) 
        {
            GameObject player = Instantiate(PlayerPrefab, gameObject.transform.localPosition, Quaternion.identity);
            camera.Follow = player.transform;
        }
    }
}
