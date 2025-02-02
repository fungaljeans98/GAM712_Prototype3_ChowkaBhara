using UnityEngine;

public class EnemyFruit : MonoBehaviour
{

    #region Initialisations

    [Header("Link to player")]
    public GameObject player;
    public GameObject playerSpawner;
    public PlayerSpawnPoint playerSpawnerScript;

    public LayerMask playerLayer;

    private AudioSource audioSource;

    [Header("Gates To Destroy")]
    [SerializeField] private string GatesToDestroy = "Lv3Gates";

    private Transform objectA; // The object you want to check from
    private Transform objectB; // The object you want to check against


    #endregion
    

    #region Start, update
    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update() {
        Debug.DrawRay(transform.position*500f, Vector3.forward, Color.red);
    }
    

    private void OnCollisionEnter(Collision other) {
        
        if (other.gameObject.tag == "Player") {

            if (PlayerIsAhead()) {
                Destroy(other.gameObject);
                Invoke("Spawn", 1);
                Debug.Log("Player was destroyed");

                    if (!audioSource.isPlaying) {
                        audioSource.Play();
                }
            }
            else {
                Debug.Log("Enemy destroyed");
                DestroyGates();
                Destroy(gameObject);
            }
        }
    }
    #endregion

    #region functions

    private void Spawn() {
        playerSpawnerScript.Spawn();
    }

    private void OpenGates_1() {
    }

    void DestroyGates() {
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(GatesToDestroy);
        foreach (GameObject obj in objectsToDestroy) {
            Destroy(obj);
        }
        Debug.Log("Objects with tag "+GatesToDestroy+" were destroyed");
    }

    bool PlayerIsAhead() {
        objectA = gameObject.transform;
        objectB = player.transform;

        // Calculate the direction from objectA to objectB
        Vector3 directionToObjectB = (objectB.position - objectA.position).normalized;

        // Get the forward direction of objectA
        Vector3 forwardA = objectA.forward;

        // Calculate the dot product
        float dotProduct = Vector3.Dot(forwardA, directionToObjectB);

        // Check if objectB is in front of objectA
        if (dotProduct > 0)
        {
            Debug.Log("Object B is in front of Object A");
            return true;
        }
        else
        {
            Debug.Log("Object B is behind Object A");
            return false;
        }
    }
    #endregion
}
