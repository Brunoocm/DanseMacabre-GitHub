using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public EnemyLocomotionManager enemyLocomotionManager;
    EnemyAnimatorManager enemyAnimationManager;
    EnemyStats enemyStats;

    public NavMeshAgent navmeshAgent;
    public State currentState;
    public HealthSystem currentTarget;
    public Vector3 currentRotation;
    public Rigidbody enemyRigidBody;

    //public EnemyAttack[] enemyAttacks; //EnemyAttackActions;
    //public EnemyAttack currentAttack;

    public bool isPreformingAction;
    public float rotationSpeed = 15f;
    public float maximumAttackRange = 1.5f;
    //public float minimumAttackRange = 1.5f;

    [Header("AI")]
    public float detectionRadius = 20;

    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;

    public float currentRecoveryTime = 0;
    private void Awake()
    {
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        enemyAnimationManager = GetComponentInChildren<EnemyAnimatorManager>();
        enemyStats = GetComponent<EnemyStats>();
        enemyRigidBody = GetComponent<Rigidbody>();
        navmeshAgent = GetComponentInChildren<NavMeshAgent>();
        navmeshAgent.enabled = false;

    }
    private void Start()
    {
        enemyRigidBody.isKinematic = false;

    }
    private void Update()
    {
        HandleRecoveryTimer();
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
        navmeshAgent.transform.localPosition = Vector3.zero;
        navmeshAgent.transform.localRotation = Quaternion.identity;

    }

    private void HandleStateMachine()
    {
        if(currentState != null)
        {
            State nextState = currentState.Tick(this, enemyStats, enemyAnimationManager);

            if(nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(State state)
    {
        currentState = state;
    }

    private void HandleRecoveryTimer()
    {
        if(currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if(isPreformingAction)
        {
            if(currentRecoveryTime <= 0)
            {
                isPreformingAction = false;
            }
        }
    }


}
