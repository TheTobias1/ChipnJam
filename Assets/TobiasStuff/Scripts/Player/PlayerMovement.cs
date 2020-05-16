using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components
    public Transform playerModel;

    private CharacterController controller;
    public CharacterController Controller { get => controller; }

    //Movement attributes
    public float speed;
    public float acceleration;
    public float deceleration;
    public float gravity;
    public float jumpForce;
    public LayerMask groundMask;

    //Movement State
    private Vector3 velocity;
    private float nextJump;
    private Vector3 forwardDirection;
    public Vector3 Velocity { get { return velocity; } set { velocity = value; } }

    private bool stunned;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        PlayerInput inputBuffer = InputManager.GetPlayerInput();

        Move(inputBuffer);
        RotateModel();
    }

    protected virtual void Move(PlayerInput input)
    {
        Vector2 planearVelocity = CondenseVector3(velocity);
        Vector2 moveInput = (!stunned)? input.moveInput : Vector2.zero;

        if(moveInput.magnitude > 0.25f && !input.aim)
        {
            planearVelocity += moveInput * acceleration * Time.deltaTime;
            planearVelocity = Vector2.ClampMagnitude(planearVelocity, speed);

            if(planearVelocity.magnitude > 5)
                forwardDirection = new Vector3(planearVelocity.x, 0, planearVelocity.y);
            else
                forwardDirection = new Vector3(moveInput.x, 0, moveInput.y);
        }
        else if(!stunned)
        {
            float curSpeed = planearVelocity.magnitude;
            curSpeed = Mathf.Max(0, curSpeed - deceleration * Time.deltaTime);
            planearVelocity = planearVelocity.normalized * curSpeed;

            if(input.aim)
            {
                forwardDirection = new Vector3(moveInput.x, 0, moveInput.y);
            }
        }

        velocity = new Vector3(planearVelocity.x, velocity.y, planearVelocity.y);

        if(controller.isGrounded)
        {


            velocity.y = Mathf.Max(velocity.y, -0.1f);

            if (input.jump && nextJump < Time.time)
            {
                Jump();
            }
            else
            {
                velocity.y -= gravity * Time.deltaTime;

                if (input.jump && nextJump < Time.time)
                {
                    //Jump ability
                }
            }

        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;

            if (input.jump && nextJump < Time.time)
            {
                //Jump ability
            }
        }

        controller.Move(velocity * Time.deltaTime);
    }

    protected virtual void RotateModel()
    {
        if(playerModel != null)
            playerModel.transform.rotation = Quaternion.Lerp(playerModel.rotation, Quaternion.LookRotation(forwardDirection), 15 * Time.deltaTime);
    }

    protected virtual void Jump()
    {
        nextJump = Time.time + 0.2f;

        velocity.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        bool flatSurface = false;
        Vector3 detectionPos = new Vector3(transform.position.x, controller.bounds.max.y + 0.3f, transform.position.z);

        RaycastHit[] raycastHit = Physics.SphereCastAll(detectionPos, controller.radius, Vector3.down, controller.bounds.size.y + 0.4f, groundMask);
        foreach(RaycastHit h in raycastHit)
        {
            if (Vector3.Angle(Vector3.up, h.normal) < 10)
            {
                Debug.Log("Ground: " + h.collider.name);
                flatSurface = true;
                break;
            }
        }

        if(!flatSurface)
        {
            Stun(Vector3.Reflect(Velocity, hit.normal).normalized * 6.7f, 0.2f);
        }
    }

    //Utility Functions
    public Vector2 CondenseVector3(Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }

    public void Stun(Vector3 knockBackForce, float stunTime = 0.4f)
    {
        CancelInvoke("UnStun");
        stunned = true;
        velocity = knockBackForce;
        Invoke("UnStun", stunTime);
    }

    public void Stun(float stunTime = 0.4f)
    {
        CancelInvoke("UnStun");
        stunned = true;
        Invoke("UnStun", stunTime);
    }

    public void UnStun()
    {
        stunned = false;
    }
}

