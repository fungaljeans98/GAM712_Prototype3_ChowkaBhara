using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class gyro : MonoBehaviour
{

#region Initialisations
    public InputSystem_Actions inputSystem;

    private void Awake() {
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
    }

    private void OnEnable() {
    }
    private void OnDisable() {
        inputSystem.Disable();
    }
#endregion
#region Start, Update
    private void Start() {
        
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
#endregion

#region Functions
    void GyroRotate() {
        Vector3 rotation = inputSystem.Player.Gyro.ReadValue<Vector3>();
        transform.Rotate(rotation.x, rotation.y, rotation.z);

        Debug.Log(rotation);
    }
#endregion
}
