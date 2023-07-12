using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
                     private GameObject PlayerPrefab;
    [SerializeField] private GameObject[] PlayerPrefabList;
    [SerializeField] private CinemachineVirtualCameraBase camera;
    [SerializeField] private GameObject spawnLocation;
                     private int deathText = 0;

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
            deathText++;
            GameObject.Find("Death Number").GetComponent<Text>().text = deathText.ToString();
        }
    }

    private void RandomPrefabPicker()
    {
        int i = Random.Range(0, 5);
        
        PlayerPrefab = PlayerPrefabList[i];
    }
}
