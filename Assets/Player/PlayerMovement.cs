using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rotationSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private Camera Camera;
    private Animator anim;

    private float horizontal, vertical;

    private CharacterController controller;

    private float attackRate = 1.0f;

    private float nextAttack;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            StartCoroutine(Attack());
        }
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, moveZ);

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0f, vertical);

        moveDirection = Quaternion.AngleAxis(45, Vector3.up) * moveDirection;
        //moveDirection = MoveTowardTarget(targetVector);

        //if (targetVector.magnitude > 0)
        //{
        //    targetVector.Normalize();
        //    targetVector *= _speed * Time.deltaTime;
        //    transform.Translate(targetVector, Space.World);
        //}

        Rotate();

        float velocityX = Vector3.Dot(moveDirection.normalized, transform.right);
        float velocityZ = Vector3.Dot(moveDirection.normalized, transform.forward);

        //_animator.SetFloat("VelocityZ", Mathf.Abs(velocityZ), 0.1f, Time.deltaTime);
        //_animator.SetFloat("VelocityX", Mathf.Abs(velocityZ), 0.1f, Time.deltaTime);


        if (true)//isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                //Walk
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                //Run
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                //Idle
                Idle();
            }

            moveDirection *= moveSpeed;
        }

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;
        // transform.Translate(targetVector * (MovementSpeed * Time.deltaTime)); Demonstrate why this doesn't work
        //transform.Translate(targetVector * (MovementSpeed * Time.deltaTime), Camera.gameObject.transform);

        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;

        //controller.Move(targetVector * speed);

        //var targetPosition = transform.position + targetVector * speed;
        //transform.position = targetPosition;
        return targetVector;
    }

    private void Rotate()
    {

        if (horizontal > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(45, Vector3.up) * Vector3.right), rotationSpeed * Time.deltaTime);
        else if (horizontal < 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(45, Vector3.up) * Vector3.left), rotationSpeed * Time.deltaTime);
        if (vertical > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(45, Vector3.up) * Vector3.forward), 0.67f * rotationSpeed * Time.deltaTime);
        else if (vertical < 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(45, Vector3.up) * Vector3.back), 0.67f * rotationSpeed * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Run Blend", 0.0f, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Run Blend", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Run Blend", 1.0f, 0.1f, Time.deltaTime);
    }

    private IEnumerator Attack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.9f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
    }
}
