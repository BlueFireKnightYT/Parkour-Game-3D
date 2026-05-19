using UnityEngine;
using UnityEngine.InputSystem;

public class WallRun : MonoBehaviour
{
    float xInput;
    float yInput;
    public float rayCastDistance;
    public bool wallRunning;
    float rotationAmt;
    bool speedHasBecome0;
    bool wallRunRightHit;
    bool wallRunLeftHit;

    LayerMask wallRunLayer;
    Rigidbody rb;
    GameObject cam;
    private void Start()
    {
        wallRunLayer = LayerMask.GetMask("wallrun");
        rb = GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        wallRunLeftHit = Physics.Raycast(transform.position, transform.right * -1f, rayCastDistance, wallRunLayer);
        wallRunRightHit = Physics.Raycast(transform.position, transform.right, rayCastDistance, wallRunLayer);

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

        if (yInput == 0)
        {
            rb.useGravity = true;
        }

        rotationAmt = (wallRunLeftHit) ? 45 : -45;
    }

    private void FixedUpdate()
    {
        if (wallRunning)
        {
            rb.AddForce(new Vector3(0, -1, 0), ForceMode.Acceleration);
            cam.transform.eulerAngles = new Vector3(0, 0, rotationAmt);
        }
    }

    public void WallRunning(InputAction.CallbackContext context)
    {
        xInput = context.ReadValue<Vector2>().x;
        yInput = context.ReadValue<Vector2>().y;
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
