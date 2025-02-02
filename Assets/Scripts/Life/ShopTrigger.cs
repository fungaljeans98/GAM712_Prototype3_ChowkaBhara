using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private GameObject saleBoard;

    private void Awake() {
        saleBoard.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            saleBoard.SetActive(true);
        }
    }
}
