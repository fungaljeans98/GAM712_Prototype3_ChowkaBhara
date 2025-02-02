using UnityEngine;

public class ChurchCollisionDetection : MonoBehaviour
{
    public bool collided;
    private BoxCollider2D boxCollider;

    private void Awake() {
        collided = false;
    }

    private void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            collided = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            collided = false;
        }
    }
}
