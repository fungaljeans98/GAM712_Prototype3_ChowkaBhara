using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class GyroController : MonoBehaviour
{
    #region Initialisations

    //INPUT SYSTEM VARIABLES
    InputSystem_Actions inputSystem;
    InputAction tapScreen;
    InputAction gyroscope;

    private void Awake() {
        inputSystem = new InputSystem_Actions();
    }

    private void OnEnable() {
        gyroscope = inputSystem.Player.Gyro;
        gyroscope.Enable();

        tapScreen = inputSystem.Player.TouchTap;
        tapScreen.Enable();
        tapScreen.performed += OnTap;
    }
    private void OnDisable() {
        gyroscope.Disable();

        tapScreen.performed -= OnTap;
        tapScreen.Disable();
    }
#endregion
#region Update
    private void Start() {
        ResetGyro();
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

    void OnTap(InputAction.CallbackContext context) {
        ResetGyro();
    }
#endregion

#region Functions
    void GyroRotate() {
        Vector3 rotation = gyroscope.ReadValue<Vector3>();
        transform.Rotate(rotation.y, rotation.z, -rotation.x);

        Debug.Log(rotation);
    }

    void ResetGyro() {
        transform.rotation = Quaternion.identity;
    }
#endregion
}
