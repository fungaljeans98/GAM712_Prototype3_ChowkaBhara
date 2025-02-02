using UnityEngine;

public class TreeTrigger : MonoBehaviour
{
    public bool collided;

    private void Awake() {
        collided = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            collided = true;
        }
    }
}
