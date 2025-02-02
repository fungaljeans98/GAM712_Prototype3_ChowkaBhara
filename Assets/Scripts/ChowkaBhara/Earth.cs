using UnityEngine;
using UnityEngine.SceneManagement;

public class Earth : MonoBehaviour
{

    public string nextScene;
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            Handheld.Vibrate();
            Invoke("ChangeScene", 1f);
        }
    }

    void ChangeScene() {
        SceneManager.LoadScene(nextScene);
    }
}
