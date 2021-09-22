using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [Header("Combat Info")]
    public bool inCombat;
    public bool attacking;
    public int attackType;
    public int totalAttacks;
    public States.CombatState combatState;

    [Header("Current Enemy Info")]
    public Enemy currentEnemy;
    public bool hasEnemy;

    private bool blocking;
    private float currentAtackDuration;
    private float timer;


    private void Update()
    {
        hasEnemy = currentEnemy.enemy != null ? true : false;
        currentEnemy.UpdateEnemy(currentEnemy.enemy);
        CombatState();
    }

    private void CombatState()
    {

        if (hasEnemy || attacking || blocking)
        {
            inCombat = true;
        }
        else
            inCombat = false;

        if (!inCombat && !attacking && !blocking)
        {
            combatState = States.CombatState.inactive;
            attackType = 0;
            return;
        }

        combatState = States.CombatState.idle;

        if (attacking)
        {
            combatState = States.CombatState.attacking;
        }
        if (blocking)
        {
            combatState = States.CombatState.defending;
        }
    }

    //Futuramente ir� fazer as verifica��es necess�rias para escolher o alvo de combate
    public void TryTarget()
    {
        if (currentEnemy != null)
            currentEnemy.UpdateEnemy(GameObject.FindGameObjectWithTag("Enemy"));

        if (hasEnemy)
            UnlockTarget();
    }

    //Desseleciona o inimigo focado 
    public void UnlockTarget()
    {
        currentEnemy.enemy = null;
        currentEnemy.UpdateEnemy(null);
    }

    public void TryAttack()
    {
        if (attacking)
        {
            if ((attackType + 1) <= totalAttacks) attackType++;
        }
        else
        {
            attackType++;
            attacking = true;
        }

        if (attackType > totalAttacks) attackType = totalAttacks;
    }

    public void Block(bool value)
    {
        blocking = value;
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
        //Seta valores padr�o se n�o tiver inimigo
        if (_enemy == null)
        {
            enemyStates = null;
            enemyCombatSystem = null;
            enemyCombatState = States.CombatState.inactive;
            enemyMasterState = States.MasterState.idle;
            return;
        }

        enemy = _enemy;
        enemyStates = enemy.GetComponent<StatesSystem>();
        enemyCombatSystem = enemy.GetComponent<CombatSystem>();
        enemyMasterState = enemyStates.masterState;
        enemyCombatState = enemyStates.combatState;
    }
}
