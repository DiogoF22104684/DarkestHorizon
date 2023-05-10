using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private CinemachineVirtualCameraBase camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localPosition = new Vector3(-8, -3.5f, 0f);
        PlayerCheck();
    }

    private void PlayerCheck()
    {
        bool playerDead = GameObject.FindGameObjectsWithTag("Player").Length == 0;
        if(playerDead)
        {
            GameObject player = Instantiate(PlayerPrefab, gameObject.transform.localPosition, Quaternion.identity);
            camera.Follow = player.transform;
        }
    }
}
