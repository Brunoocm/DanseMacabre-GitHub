﻿using System;
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

    [Header("Enemy Control")]
    [SerializeField] private bool rotateTowardEnemy;
    public Transform enemyPosition;

    [Header( "Objects" )]
    [SerializeField] private Transform normalFollowTransform;
    [SerializeField] private Transform combatFollowTransform;
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
    }

    #region Movimentação
    public void TryMove( Vector3 targetVector )
    {
        if ( !canMove )
            return;

        var movementVector = MoveTowardTarget( targetVector.normalized );
        lastVelocity = movementVector;
    }

    //Calcula a direção da movimentação baseada no input e direção da câmera
    //Retorna um vetor para que a função de rotação saiba a direção atual
    private Vector3 MoveTowardTarget( Vector3 targetVector )
    {
        var speed = MovementSpeed * Time.deltaTime;

        if ( !rotateTowardEnemy )
            targetVector = Quaternion.Euler( 0 , normalFollowTransform.rotation.eulerAngles.y , 0 ) * targetVector;
        else
            targetVector = Quaternion.Euler( 0 , combatFollowTransform.rotation.eulerAngles.y , 0 ) * targetVector;

        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }
    #endregion

    #region Rotação do Personagem
    private void CalculateRotation()
    {
        if ( rotateTowardEnemy )
            visualsTransform.LookAt( enemyPosition );

        RotateTowardMovementVector( lastVelocity );
    }

    private void RotateTowardMovementVector( Vector3 movementDirection )
    {
        if ( movementDirection.magnitude == 0 )
        { return; }

        var rotation = Quaternion.LookRotation( movementDirection );
        visualsTransform.rotation = Quaternion.RotateTowards( visualsTransform.transform.rotation , rotation , RotationSpeed );
    }
    #endregion

    #region Funções Públicas
    public void RotateTowardTarget()
    {
        rotateTowardEnemy = rotateTowardEnemy ? false : true;
    }

    #endregion
}
