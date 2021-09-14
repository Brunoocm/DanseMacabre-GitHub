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

    //Futuramente irá fazer as verificações necessárias para escolher o alvo de combate
    public void TryTarget()
    {
        if(currentEnemy != null)
            currentEnemy.UpdateEnemy( GameObject.FindGameObjectWithTag( "Enemy" ) );

        if ( hasEnemy )
            UnlockTarget( );
    }

    //Desseleciona o inimigo focado 
    public void UnlockTarget()
    {
        currentEnemy.enemy = null;
        currentEnemy.UpdateEnemy( null );
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
        //Seta valores padrão se não tiver inimigo
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
