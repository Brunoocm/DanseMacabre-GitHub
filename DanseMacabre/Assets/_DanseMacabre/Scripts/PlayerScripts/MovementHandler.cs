using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputSystem))]
public class MovementHandler : MonoBehaviour
{
    [Header("Movement Controls")]
    public bool canMove = true;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float accelerationSpeed = 0.5f;
    [SerializeField] private float rotationSpeed;

    [Header("Enemy Control")]
    [SerializeField] private bool rotateTowardEnemy;
    public Transform enemyPosition;

    [Header("Objects")]
    [SerializeField] private Transform normalFollowTransform;
    [SerializeField] private Transform combatFollowTransform;
    [SerializeField] private Transform visualsTransform;

    //Private
    private Rigidbody rb;
    private Vector3 lastVelocity;
    [HideInInspector] public bool isWalking;

    private void Awake()
    {
        rotateTowardEnemy = false;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CalculateRotation();

        currentSpeed = 0;
        if (currentSpeed < movementSpeed)
        {
            currentSpeed += accelerationSpeed * Time.deltaTime;
        }
    }

    #region Movimentação
    public void TryMove(Vector3 targetVector)
    {
        if (!canMove)
        {
            isWalking = false;
            return;
        }

        var movementVector = MoveTowardTarget(targetVector.normalized);
        lastVelocity = movementVector;
    }

    //Calcula a direção da movimentação baseada no input e direção da câmera
    //Retorna um vetor para que a função de rotação saiba a direção atual
    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        if (!rotateTowardEnemy)
            targetVector = Quaternion.Euler(0, normalFollowTransform.rotation.eulerAngles.y, 0) * targetVector;
        else
            targetVector = Quaternion.Euler(0, combatFollowTransform.rotation.eulerAngles.y, 0) * targetVector;

        var targetPosition = transform.position + targetVector * currentSpeed;
        transform.position = targetPosition;
        return targetVector;
    }
    #endregion

    #region Rotação do Personagem
    private void CalculateRotation()
    {
        if (rotateTowardEnemy)
            visualsTransform.LookAt(enemyPosition);

        RotateTowardMovementVector(lastVelocity);
    }

    private void RotateTowardMovementVector(Vector3 movementDirection)
    {
        if (movementDirection.magnitude == 0)
        { return; }

        var rotation = Quaternion.LookRotation(movementDirection);
        visualsTransform.rotation = Quaternion.RotateTowards(visualsTransform.transform.rotation, rotation, rotationSpeed);
    }
    #endregion

    #region Funções Públicas
    public void RotateTowardTarget(GameObject enemy, bool _rotateTowardEnemy)
    {
        if (enemy == null && _rotateTowardEnemy) return;

        rotateTowardEnemy = _rotateTowardEnemy;

        if (rotateTowardEnemy && enemy != null)
            enemyPosition = enemy.transform;
        else if (!_rotateTowardEnemy && enemy == null)
            enemyPosition = null;
    }

    public void IsMoving(bool value)
    {
        isWalking = value;
    }

    #endregion
}
