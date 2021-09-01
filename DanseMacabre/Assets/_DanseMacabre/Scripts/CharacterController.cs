using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class CharacterController : MonoBehaviour
{
    private InputHandler _input;
    public Transform target;

    [SerializeField]
    private bool RotateTowardMouse;    
    [SerializeField]
    private bool RotateTowardEnemy;
    [SerializeField]
    private bool WalkWithCameraPosition;

    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private float RotationSpeed;

    [SerializeField]
    private Camera Camera;

    private Rigidbody rb;
    public Vector3 targetVector;


    private void Awake()
    {
        _input = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        var movementVector = MoveTowardTarget(targetVector);

        if (!RotateTowardMouse) RotateTowardMovementVector(movementVector);
        else RotateTowardMouseVector();

        if (RotateTowardEnemy) transform.LookAt(target);
    }

    private void RotateTowardMouseVector()
    {
        Ray ray = Camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = MovementSpeed * Time.deltaTime;
      
        if(WalkWithCameraPosition)
            targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;

        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if(movementDirection.magnitude == 0) { return; }

        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed);
    }
}
