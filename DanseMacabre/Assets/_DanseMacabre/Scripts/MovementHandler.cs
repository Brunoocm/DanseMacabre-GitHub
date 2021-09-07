using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( InputSystem ) )]
public class MovementHandler : MonoBehaviour
{
    [Header( "Movement Controls" )]
    public bool canMove = true;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float RotationSpeed;

    [SerializeField] private RotationType rotationType;
    /*[SerializeField] private bool WalkWithCameraPosition;*/

    [Header( "Objects" )]
    [SerializeField] private Camera Camera;
    public Transform target;

    //Private
    private Rigidbody rb;
    private Vector3 lastVelocity;

    private enum RotationType
    {
        RotateTowardEnemy,
        RotateWithCamera,
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>( );
    }

    private void Update()
    {
        CalculateRotation( );
    }

    public void TryMove( Vector3 targetVector )
    {
        if ( !canMove )
            return;
        Debug.Log( "TryMove" );

        var movementVector = MoveTowardTarget( targetVector.normalized );
        lastVelocity = targetVector;
    }

    private void CalculateRotation()
    {
        RotateTowardMovementVector( lastVelocity );

        if ( rotationType == RotationType.RotateTowardEnemy )
            transform.LookAt( target );

        /*        if (rotationType == RotationType.RotateTowardMouse) RotateTowardMouseVector();
        else RotateTowardMovementVector(lastVelocity);*/
    }

    private void RotateTowardMouseVector()
    {
        Ray ray = Camera.ScreenPointToRay( Input.mousePosition );

        if ( Physics.Raycast( ray , out RaycastHit hitInfo , maxDistance: 300f ) )
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt( target );
        }
    }

    private Vector3 MoveTowardTarget( Vector3 targetVector )
    {
        var speed = MovementSpeed * Time.deltaTime;

        if ( rotationType == RotationType.RotateWithCamera )
            targetVector = Quaternion.Euler( 0 , Camera.gameObject.transform.rotation.eulerAngles.y , 0 ) * targetVector;

        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector( Vector3 movementDirection )
    {
        if ( movementDirection.magnitude == 0 )
        { return; }

        var rotation = Quaternion.LookRotation( movementDirection );
        transform.rotation = Quaternion.RotateTowards( transform.rotation , rotation , RotationSpeed );
    }
}
