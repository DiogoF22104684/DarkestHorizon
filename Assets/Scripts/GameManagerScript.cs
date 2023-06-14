using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManagerScript : MonoBehaviour
{
    private GameObject PlayerPrefab;
    [SerializeField] private GameObject PlayerPrefabGreen, PlayerPrefabRed, PlayerPrefabGrey, PlayerPrefabBlue, PlayerPrefabYellow;
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
            RandomPrefabPicker();
            Vector2 position = gameObject.transform.localPosition;
            GameObject player = Instantiate(PlayerPrefab, position, Quaternion.identity);
            camera.Follow = player.gameObject.transform;
        }
    }

    private void RandomPrefabPicker()
    {
        int i = Random.Range(1, 6);
        
        switch (i)
        {
            case 1:
                PlayerPrefab = PlayerPrefabGreen;
                break;
            case 2:
                PlayerPrefab = PlayerPrefabRed;
                break;
            case 3:
                PlayerPrefab = PlayerPrefabGrey;
                break;
            case 4:
                PlayerPrefab = PlayerPrefabBlue;
                break;
            case 5:
                PlayerPrefab = PlayerPrefabYellow;
                break;
            default:
                PlayerPrefab = PlayerPrefabGreen;
                break;
        }
    }
}
