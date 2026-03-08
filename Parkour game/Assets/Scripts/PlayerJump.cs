using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    Rigidbody rb;
    Collider coll;
    public float jumpHeight;
    bool groundCheck;
    bool canJump;

    [Header("Physics Materials")]
    public PhysicsMaterial floorMat;
    public PhysicsMaterial noFrictionMat;

    float rayLength = 1.6f;
    public WallRun wallRun;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    private void Update()
    {
        //draws the raycast for testing
        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red);
        //makes a groundcheck raycast
        groundCheck = Physics.Raycast(transform.position, Vector3.down, rayLength);

        if (groundCheck)
        {
            coll.material = floorMat;
            canJump = true;
        }
        else
        {
            coll.material = noFrictionMat;
            if (!wallRun.wallRunning)
            {
                canJump = false;
            }
            else
            {
                canJump = true;
            }
        }
        Debug.Log(canJump);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            canJump = false;
        }
    }
}
