using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

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
        bool playerDead = GameObject.FindObjectOfType<Player>() == null;
        if(playerDead && SceneManager.GetActiveScene().buildIndex != 0) 
        {
            GameObject player = Instantiate(PlayerPrefab, gameObject.transform.localPosition, Quaternion.identity);
            camera.Follow = player.transform;
        }
    }
}
