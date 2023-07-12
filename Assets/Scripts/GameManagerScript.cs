using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManagerScript : MonoBehaviour
{
    private GameObject PlayerPrefab;
    [SerializeField] private GameObject[] PlayerPrefabList;
    [SerializeField] private CinemachineVirtualCameraBase camera;
    [SerializeField] private GameObject spawnLocation;

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerCheck();
    }

    private void PlayerCheck()
    {
        bool playerDead = FindObjectOfType<Player>() == null;
        if(playerDead)
        {
            RandomPrefabPicker();
            Vector2 position = gameObject.transform.localPosition;
            GameObject player = Instantiate(PlayerPrefab, spawnLocation.transform.position, Quaternion.identity);
            camera.Follow = player.gameObject.transform;
        }
    }

    private void RandomPrefabPicker()
    {
        int i = Random.Range(0, 5);
        
        PlayerPrefab = PlayerPrefabList[i];
    }
}
