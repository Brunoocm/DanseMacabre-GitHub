using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputSystem))]
public class MovementHandler : MonoBehaviour
{
    [Header("Movement Controls")]
    public bool canMove = true;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float desaccelerationSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float zerarEm;

    [Header("Valores Atuais")]
    [SerializeField] private Vector3 currentSpeed;
    [SerializeField, Range(-1, 1)] private float horizontalSpeed;
    [SerializeField, Range(-1, 1)] private float verticalSpeed;

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
    }

    #region Movimentação
    public void TryMove(float _horizontalInput, float _verticalInput)
    {
        if (!canMove)
        {
            isWalking = false;
            return;
        }

        if (_horizontalInput > 0 && horizontalSpeed <= movementSpeed) horizontalSpeed += accelerationSpeed * Time.deltaTime;
        else if (_horizontalInput < 0 && horizontalSpeed >= -movementSpeed) horizontalSpeed -= accelerationSpeed * Time.deltaTime;
        else if (_horizontalInput == 0 && horizontalSpeed > 0 && horizontalSpeed > zerarEm) horizontalSpeed -= desaccelerationSpeed * Time.deltaTime;
        else if (_horizontalInput == 0 && horizontalSpeed < 0 && horizontalSpeed < zerarEm) horizontalSpeed += desaccelerationSpeed * Time.deltaTime;
        else if (_horizontalInput == 0 && (horizontalSpeed < zerarEm || horizontalSpeed > zerarEm)) horizontalSpeed = 0;

        if (_verticalInput > 0 && verticalSpeed <= movementSpeed) verticalSpeed += accelerationSpeed * Time.deltaTime;
        else if (_verticalInput < 0 && verticalSpeed >= -movementSpeed) verticalSpeed -= accelerationSpeed * Time.deltaTime;
        else if (_verticalInput == 0 && verticalSpeed > 0 && verticalSpeed > zerarEm) verticalSpeed -= desaccelerationSpeed * Time.deltaTime;
        else if (_verticalInput == 0 && verticalSpeed < 0 && verticalSpeed < zerarEm) verticalSpeed += desaccelerationSpeed * Time.deltaTime;
        else if (_verticalInput == 0 && (verticalSpeed < zerarEm || verticalSpeed > zerarEm)) verticalSpeed = 0;

        if ((_horizontalInput > 0 && horizontalSpeed < 0) || (_horizontalInput < 0 && horizontalSpeed > 0)) horizontalSpeed = 0;
        if ((_verticalInput > 0 && verticalSpeed < 0) || (_verticalInput < 0 && verticalSpeed > 0)) verticalSpeed = 0;

        var movementVector = MoveTowardTarget(new Vector3(horizontalSpeed, 0, verticalSpeed));
        lastVelocity = movementVector;
    }

    //Calcula a direção da movimentação baseada no input e direção da câmera
    //Retorna um vetor para que a função de rotação saiba a direção atual
    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        currentSpeed = targetVector;
        if (!rotateTowardEnemy)
            targetVector = Quaternion.Euler(0, normalFollowTransform.rotation.eulerAngles.y, 0) * targetVector;
        else
            targetVector = Quaternion.Euler(0, combatFollowTransform.rotation.eulerAngles.y, 0) * targetVector;

        var targetPosition = transform.position + targetVector * Time.deltaTime;
        transform.position = targetPosition;
        return targetVector;
    }
    #endregion

    #region Rotação do Personagem
    private void CalculateRotation()
    {
        if (rotateTowardEnemy) //Travei o jogador pra nao olhar para cima
        {
            Vector3 enemyXpos = enemyPosition.position;
            enemyXpos.y = transform.position.y;
            visualsTransform.LookAt(enemyXpos); //Antes visualsTransform.LookAt(enemyPosition);
        }


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
