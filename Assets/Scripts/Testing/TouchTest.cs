using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class TouchTest : MonoBehaviour
{

     public TextMeshProUGUI textMeshPro;
    private AudioSource audioSource;

[Header ("Mention Which Scene to Load")]
    public string sceneToLoad = "ChowkaBhara1";

    public void ChangeScene() {

        Invoke("SceneManagerLoadScene", 2);

        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
    }

    void SceneManagerLoadScene() {
        SceneManager.LoadScene (sceneToLoad);
    }
}
