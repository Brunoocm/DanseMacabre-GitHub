using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [Header( "Current Enemy Info" )]
    public Enemy currentEnemy;
    public bool hasEnemy;

    private void Update()
    {
        hasEnemy = currentEnemy.enemy != null ? true : false;

        currentEnemy.UpdateEnemy( currentEnemy.enemy );
    }

    public void TryTarget()
    {
        if(currentEnemy != null)
        {
            currentEnemy.UpdateEnemy( GameObject.FindGameObjectWithTag( "Enemy" ) );
            GetComponent<MovementHandler>().RotateTowardTarget( currentEnemy.enemy, true );
        }

        if ( hasEnemy )
            UnlockTarget( );
    }

    public void UnlockTarget()
    {
        currentEnemy.enemy = null;
        currentEnemy.UpdateEnemy( null );
        GetComponent<MovementHandler>( ).RotateTowardTarget( null, false );
    }
}


[System.Serializable]
public class Enemy
{
    public GameObject enemy;
    [HideInInspector] public StatesSystem enemyStates;
    [HideInInspector] public CombatSystem enemyCombatSystem;
    public States.MasterState enemyMasterState;
    public States.CombatState enemyCombatState;

    public void UpdateEnemy(GameObject _enemy)
    {
        if ( _enemy == null )
        {
            enemyStates = null;
            enemyCombatSystem = null;
            enemyCombatState = States.CombatState.inactive;
            enemyMasterState = States.MasterState.idle;
            return;
        }  

        enemy = _enemy;
        enemyStates = enemy.GetComponent<StatesSystem>( );
        enemyCombatSystem = enemy.GetComponent<CombatSystem>( );
        enemyMasterState = enemyStates.masterState;
        enemyCombatState = enemyStates.combatState;
    }
}