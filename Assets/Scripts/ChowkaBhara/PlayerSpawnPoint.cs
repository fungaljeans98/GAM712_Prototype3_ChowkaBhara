using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public GameObject Fruit;
    [SerializeField] private float spawnDelay;

    private void Awake() {
        Invoke("Spawn", spawnDelay);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "FallDetect") {
            Spawn();
        }
    }

    public void Spawn() {
        Instantiate(Fruit);
    }
}
