using UnityEngine.SceneManagement;
using UnityEngine;

public class EndSceneScript : MonoBehaviour
{
    [SerializeField] private bool isThisTheFinalLevel = false;
    private void OnCollisionEnter2D(Collision2D other)
    {
        Player isPlayer = other.gameObject.GetComponent<Player>();
        if(!isThisTheFinalLevel && isPlayer)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else if (isPlayer)
        {
            SceneManager.LoadScene(0);
        }
    }
}
