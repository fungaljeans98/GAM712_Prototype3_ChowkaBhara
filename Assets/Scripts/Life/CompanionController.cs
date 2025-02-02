using UnityEngine;

public class CompanionController : MonoBehaviour
{

    [Header("Link Child GameObjects")]
    [SerializeField] private GameObject companion;
    [SerializeField] private GameObject heartIcon;

    [Header("Link Church")]
    public GameObject church;
    public ChurchCollisionDetection churchScript;

    [Header("Link GraveYard Tree")]
    public GameObject tree;
    public TreeTrigger treeScript;

    private void Awake() {
        companion.SetActive(false);
        heartIcon.SetActive(false);
    }

    private void Update() {
        if (churchScript.collided) {
            Debug.Log("companion script called");
            companion.SetActive(true);
            heartIcon.SetActive(true);
        }

        else if (!churchScript.collided) {
            Invoke("FadeOutHeart", 2.5f);
        }

        if (treeScript.collided) {
            companion.SetActive(false);
        }
    }

    void FadeOutHeart() {
        heartIcon.SetActive(false);
    }
}
