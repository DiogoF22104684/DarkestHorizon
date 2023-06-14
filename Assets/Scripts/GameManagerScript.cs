using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
        if(playerDead) 
        {
            Vector2 position = gameObject.transform.localPosition;
            GameObject player = Instantiate(PlayerPrefab, position, Quaternion.identity);
            camera.Follow = player.transform;
        }
    }
}
