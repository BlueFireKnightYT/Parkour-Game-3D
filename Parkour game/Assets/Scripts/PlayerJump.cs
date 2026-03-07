using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    Rigidbody rb;
    public float jumpHeight;

    float rayLength = 1.6f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //draws the raycast for testing
        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        //makes a groundcheck raycast
        bool groundCheck = Physics.Raycast(transform.position, Vector3.down, rayLength);
        if (context.performed && groundCheck)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
}
