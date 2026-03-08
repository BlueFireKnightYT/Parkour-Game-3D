using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchAndSlide : MonoBehaviour
{
    Rigidbody rb;
    PlayerMove pM;

    public float rayDistance;

    bool goUpNext;
    bool isCrouched;
    bool blockAbove;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pM = GetComponent<PlayerMove>();
    }
    private void Update()
    {
        blockAbove = Physics.Raycast(transform.position, Vector3.up, rayDistance);
        Debug.DrawRay(transform.position, Vector3.up * rayDistance, Color.red);

        if (isCrouched && goUpNext && !blockAbove)
        {
            UnCrouch();
        }
    }
    public void CrouchSlide(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Crouch();
            goUpNext = false;
        }
        if(context.canceled)
        {
            
            if(!blockAbove)
            {
                UnCrouch();
            }
            else
            {
                goUpNext = true;
            }
        }
    }
    void Crouch()
    {
        transform.localScale = new Vector3(1, 0.5f, 1);
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        pM.speed = 30;
        pM.maxSpeed = 5;
        pM.canSprint = false;
        pM.sprinting = false;
        isCrouched = true;
    }

    void UnCrouch()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        transform.localScale = new Vector3(1, 1, 1);
        pM.speed = pM.startSpeed;
        pM.maxSpeed = pM.startMaxSpeed;
        pM.canSprint = true;
        isCrouched = false;
    }
}

