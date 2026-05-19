using System.Collections;
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
            canJump = true;
        }
        else
        {
            
            if (!wallRun.wallRunning)
            {
                canJump = false;
            }
            else
            {
                canJump = true;
            }
        }

        if (wallRun.wallRunning)
        {
            coll.material = noFrictionMat;
        }
        else
        {
            coll.material = floorMat;
        }
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
