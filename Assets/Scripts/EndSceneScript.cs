using UnityEngine.SceneManagement;
using UnityEngine;

public class EndSceneScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
