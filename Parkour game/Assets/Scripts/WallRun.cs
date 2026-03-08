using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallRun : MonoBehaviour
{
    float xInput;
    public float rayCastDistance;
    public bool wallRunning;
    bool speedHasBecome0;

    LayerMask wallRunLayer;
    Rigidbody rb;
    private void Start()
    {
        wallRunLayer = LayerMask.GetMask("wallrun");
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        bool wallRunLeftHit = Physics.Raycast(transform.position, transform.right * -1f, rayCastDistance, wallRunLayer);
        bool wallRunRightHit = Physics.Raycast(transform.position, transform.right, rayCastDistance, wallRunLayer);

        Debug.DrawRay(transform.position, (transform.right * -1f) * rayCastDistance, Color.red);
        Debug.DrawRay(transform.position, transform.right * rayCastDistance, Color.red);

        if ((xInput > 0 && wallRunRightHit) || (xInput < 0 && wallRunLeftHit))
        {
            StartWallrun();
        }
        else
        {
            StopWallrun();
        }
    }

    private void FixedUpdate()
    {
        if (wallRunning)
        {
            rb.AddForce(new Vector3(0, -1, 0), ForceMode.Acceleration);
        }
    }

    public void WallRunning(InputAction.CallbackContext context)
    {
        xInput = context.ReadValue<Vector2>().x;
    }

    void StartWallrun()
    {
        wallRunning = true;
        rb.useGravity = false;

        if (!speedHasBecome0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            speedHasBecome0 = true;
        }
    }
    void StopWallrun()
    {
        speedHasBecome0 = false;
        wallRunning = false;
        rb.useGravity = true;
    }
}
