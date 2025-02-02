using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneFruitSpawner : MonoBehaviour
{
    [Header("Link to Fruit")]
    [SerializeField] private GameObject fruit;
    private Rigidbody fruitRb;

    [Header("Delay")]
    [SerializeField] private float spawnDelay;
    [SerializeField] private float fruitFallDelay;

    [Header("Next Scene")]
    [SerializeField] private string nextScene;


    private void Start() {
        fruitRb = fruit.GetComponent<Rigidbody>();
        fruitRb.useGravity = false;


        StartCoroutine(CutScene());
    }

    private void Update() {
    }

    private IEnumerator CutScene() {
        
        yield return new WaitForSeconds(spawnDelay);
        bool fruitSpawned = false;
        if (!fruitSpawned) {
            SpawnFruit();
            fruitSpawned = true;
        }

        yield return new WaitForSeconds (fruitFallDelay);
        FruitFalls();

        yield return new WaitForSeconds(fruitFallDelay + 1f);
        NextScene();
    }

    void FruitFalls() {
        fruitRb.useGravity = true;
    }

    void SpawnFruit() {
        Instantiate(fruit, gameObject.transform);
        Debug.Log("fruit spawned");
    }

    void NextScene() {
        SceneManager.LoadScene(nextScene);
    }
}
