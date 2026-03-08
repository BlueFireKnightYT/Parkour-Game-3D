using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Speed")]
    public float speed;
    public float maxSpeed;
    public float startSpeed;
    public float startMaxSpeed;
    public bool sprinting;
    public bool canSprint = true;

    [Header("FOV")]
    float baseFOV;
    float maxFOV;
    public float zoomSpeed;

    [Header ("Objects & Components")]
    Rigidbody rb;
    Vector2 moveInput;
    public Vector3 horizontalForce;
    Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        startSpeed = speed;
        startMaxSpeed = maxSpeed;
        baseFOV = 60;
        maxFOV = 80;
    }

    private void Update()
    {
        // Zet de vurrent target van de FOV naar max of base als die sprint of niet.
        float currentTarget = (sprinting) ? maxFOV : baseFOV;

        float newFOV = Mathf.Lerp(cam.fieldOfView, currentTarget, Time.deltaTime * zoomSpeed);

        if (Mathf.Abs(newFOV - currentTarget) < 0.1f)
        {
            cam.fieldOfView = currentTarget;
        }
        else
        {
            cam.fieldOfView = newFOV;
        }
    }
    private void FixedUpdate()
    {
        //player moves
        rb.AddRelativeForce(new Vector3(moveInput.x * speed, 0, moveInput.y * speed), ForceMode.Force);

        horizontalForce = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        //caps player speed
        if (horizontalForce.magnitude > maxSpeed)
        {
            horizontalForce = horizontalForce.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(horizontalForce.x, rb.linearVelocity.y, horizontalForce.z);
        }

        //Debug.Log(rb.linearVelocity);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed && canSprint)
        { 
            sprinting = true;
            speed = startSpeed * 1.5f;
            maxSpeed = startMaxSpeed * 1.5f;
        }
        if (context.canceled)
        {
            sprinting = false;
            speed = startSpeed;
            maxSpeed = startMaxSpeed;
        }
    }
}
