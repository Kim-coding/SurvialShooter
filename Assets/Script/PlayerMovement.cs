using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CinemachineVirtualCamera TPSCamera;
    public CinemachineVirtualCamera FPSCamera;
    public GameObject MiniMap;

    private bool isTpsCamera = true;

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

        if (Input.GetKeyDown(KeyCode.V))
        {
            CameraChange();
        }
    }

    private void CameraChange()
    {
        isTpsCamera = !isTpsCamera;
        TPSCamera.gameObject.SetActive(isTpsCamera);
        FPSCamera.gameObject.SetActive(!isTpsCamera);
        MiniMap.SetActive(!isTpsCamera);
    }

    private void FixedUpdate()
    {
        if (isTpsCamera)
        {
            TpsCameraFixedUpdate();
        }
        else
        {
            FpsCameraFixedUpdate();
        }
    }

    private void TpsCameraFixedUpdate()
    {
        h = playerInput.hMove;
        v = playerInput.vMove;

        dir = new Vector3(h, 0f, v);
        if (dir.magnitude > 1f)
        {
            dir.Normalize();
        }
        AnimationPlay(dir);

        Move();

    }
    private void FpsCameraFixedUpdate()
    {
        h = playerInput.hMove;
        v = playerInput.vMove;

        Vector3 dir = v * transform.forward + h * transform.right;
        dir.Normalize();
        AnimationPlay(dir);

        Move(dir);
    }

    private void AnimationPlay(Vector3 dir)
    {
        if (dir.magnitude == 0f)
        {
            PlayerAnimator.SetBool(moved, false);
        }
        else
        {
            PlayerAnimator.SetBool(moved, true);
        }
    }

    private void Move()
    {
        rb.MovePosition(rb.position + dir * moveSpeed * Time.deltaTime);
    }

    private void Move(Vector3 direction)
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    private void Rotate(Vector3 hitPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(hitPoint.x, 0, hitPoint.z);
        Quaternion targetRotation = Quaternion.LookRotation(heightCorrectedPoint - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
