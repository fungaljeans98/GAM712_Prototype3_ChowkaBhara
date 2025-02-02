using UnityEngine;

public class CameraSpawner : MonoBehaviour
{
    public GameObject cameraObject;

    public void EnableCamera() {
        Instantiate(cameraObject, transform.position, transform.rotation);
    }

    public void DisableCamera() {
        Destroy(cameraObject);
    }
}
