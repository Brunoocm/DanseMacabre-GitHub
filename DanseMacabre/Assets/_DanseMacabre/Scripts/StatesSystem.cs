using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesSystem : MonoBehaviour
{
    public States.MasterState masterState = States.MasterState.idle;
    public States.CombatState combatState = States.CombatState.inactive;

    public Enemy currentEnemy;
    public bool hasEnemy;

    private MovementHandler movementHandler;
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
        if ( hasEnemy )
            movementHandler.enemyPosition = currentEnemy.enemy.transform;
        else
            movementHandler.enemyPosition = this.transform;
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
