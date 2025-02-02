using UnityEngine;
using UnityEngine.InputSystem;

public class CameraGyro : MonoBehaviour
{

    public InputSystem_Actions inputSystem;

    private void Awake() {
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
    }
    private void OnDisable() {
        inputSystem.Disable();
    }


    private void Update() {
        if (UnityEngine.InputSystem.Gyroscope.current != null) {
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
            Debug.LogWarning("Gyroscope has been enabled");
            GyroRotate();
        }
        else {
            Debug.LogWarning("Gyroscope is not supported on this device");
        }
    }

    void GyroRotate() {
        Vector3 rotation = inputSystem.Player.Gyro.ReadValue<Vector3>()/10;
        transform.Rotate(rotation.x, rotation.y, -rotation.z);
    }

    public void CameraEnable() {
        
    }

    public void CameraDisable() {
        Destroy(gameObject);
    }
}
