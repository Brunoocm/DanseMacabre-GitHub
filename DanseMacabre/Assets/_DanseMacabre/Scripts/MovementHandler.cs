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
    [SerializeField] private bool WalkWithCameraPosition;

    [Header("Enemy Control")]
    [SerializeField] private bool rotateTowardEnemy;
    public Transform enemyPosition;

    [Header( "Objects" )]
    [SerializeField] private Transform cameraFollowTransform;
    [SerializeField] private Transform visualsTransform;

    //Private
    private Rigidbody rb;
    private Vector3 lastVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>( );
    }

    private void Update()
    {
        CalculateRotation( );
        rotateTowardEnemy = enemyPosition != null ? true : false;
    }

    private void CalculateRotation()
    {
        if ( rotateTowardEnemy )
            visualsTransform.LookAt( enemyPosition );

       RotateTowardMovementVector( lastVelocity );
    }

    public void TryMove( Vector3 targetVector )
    {
        if ( !canMove )
            return;

        var movementVector = MoveTowardTarget( targetVector.normalized );
        lastVelocity = movementVector;
    }

    private Vector3 MoveTowardTarget( Vector3 targetVector )
    {
        var speed = MovementSpeed * Time.deltaTime;

        if ( WalkWithCameraPosition )
            targetVector = Quaternion.Euler( 0 , cameraFollowTransform.rotation.eulerAngles.y , 0 ) * targetVector;

        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }

    private void RotateTowardMovementVector( Vector3 movementDirection )
    {
        if ( movementDirection.magnitude == 0 )
        { return; }

        var rotation = Quaternion.LookRotation( movementDirection );
        visualsTransform.rotation = Quaternion.RotateTowards( visualsTransform.transform.rotation , rotation , RotationSpeed );
    }
}
