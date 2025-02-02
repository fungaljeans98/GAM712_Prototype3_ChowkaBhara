using UnityEngine;

public class CityTrigger : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (audioSource.isPlaying) {
                audioSource.Pause();
            }
        }
    }
}
