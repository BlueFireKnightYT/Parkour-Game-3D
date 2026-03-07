using UnityEngine;
using UnityEngine.InputSystem;

public class Looking : MonoBehaviour
{
    GameObject cam;

    public float sensitivity = 0.1f;
    float xRotation = 0f;

    Vector2 lookInput;
    private void Start()
    {
        //camera becomes invisible and locked
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void Update()
    {
        //rotate player
        transform.Rotate(Vector3.up * lookInput.x * sensitivity);

        //set rotation on x
        xRotation -= lookInput.y * sensitivity;
        //clamp rotation on x
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate camera
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    public void Look(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}
