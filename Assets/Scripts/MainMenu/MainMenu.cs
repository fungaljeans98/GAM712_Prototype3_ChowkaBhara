using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource audioSource;

    [Header ("Mention Which Scene to Load")]
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string openUrl;


    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    public void ChangeScene() {

        Invoke("SceneManagerLoadScene", 2);

        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
    }

    public void GoToDocumentation() {
        Application.OpenURL(openUrl);
    }

    void SceneManagerLoadScene() {
        SceneManager.LoadScene (sceneToLoad);
    }
}
