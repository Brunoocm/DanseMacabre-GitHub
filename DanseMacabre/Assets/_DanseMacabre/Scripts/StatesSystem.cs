using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesSystem : MonoBehaviour
{
    public States.MasterState masterState = States.MasterState.idle;
    public States.CombatState combatState = States.CombatState.inactive;

    public Enemy currentEnemy;
    public bool hasEnemy;

    public MovementHandler movementHandler;
    private CombatSystem combatSystem;

    private void Awake()
    {
        movementHandler = GetComponent<MovementHandler>( );
        combatSystem = GetComponent<CombatSystem>( );
    }

    private void Update()
    {
        UpdateCurrentInfo( );
        UpdateMovementHandler( );
        UpdateCombatSystem( );
    }

    private void UpdateMovementHandler()
    {
            
    }

    private void UpdateCombatSystem()
    {

    }

    private void UpdateCurrentInfo()
    {
        currentEnemy = combatSystem.currentEnemy;
        hasEnemy = combatSystem.hasEnemy;
    }
}
