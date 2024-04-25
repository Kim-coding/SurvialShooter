using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    
    private PlayerInput playerInput;
    private Rigidbody rb;
    private Animator PlayerAnimator;

    private float h = 0;
    private float v = 0;
    private Vector3 dir = Vector3.zero;

    private Ray ray;
    private Plane plane;

    private static readonly int moved = Animator.StringToHash("Move");
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        PlayerAnimator = GetComponent<Animator>();
        plane = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Rotate(hitPoint);
        }
    }

    private void FixedUpdate()
    {
        h = playerInput.hMove;
        v = playerInput.vMove;

        dir = new Vector3(h, 0f, v);
        if (dir.magnitude > 1f)
        {
            dir.Normalize(); 
        }
        
        if(dir.magnitude == 0f) 
        {
            PlayerAnimator.SetBool(moved, false);
        }
        else
        {
            PlayerAnimator.SetBool(moved, true);
        }

        Move();

    }
    private void Move()
    {
        rb.MovePosition(rb.position + dir * moveSpeed * Time.deltaTime);
    }

    private void Rotate(Vector3 hitPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(hitPoint.x, 0, hitPoint.z);
        Quaternion targetRotation = Quaternion.LookRotation(heightCorrectedPoint - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
