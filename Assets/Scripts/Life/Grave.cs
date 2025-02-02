using UnityEngine;
using UnityEngine.SceneManagement;

public class Grave : MonoBehaviour
{

    [SerializeField] string SceneChange;

    private void OnCollisionEnter2D(Collision2D other) {
        Handheld.Vibrate();
        Invoke("ChangeScene", 1f);
    }

    void ChangeScene() {
        SceneManager.LoadScene(SceneChange);
    }
}
