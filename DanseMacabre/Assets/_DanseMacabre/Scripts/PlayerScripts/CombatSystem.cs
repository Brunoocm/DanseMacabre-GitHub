using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [Header("Combat Info")]
    public bool canAttack;
    public bool inCombat;
    public bool attacking;
    public bool blocking;
    public int attackType;
    public int totalAttacks;
    public States.CombatState combatState;

    [Header("Current Enemy Info")]
    public Enemy currentEnemy;
    public bool hasEnemy;


    private HealthSystem healthSystem => GetComponent<HealthSystem>();
    private void Update()
    {
        hasEnemy = currentEnemy.enemy != null ? true : false;
        currentEnemy.UpdateEnemy(currentEnemy.enemy);
        canAttack = GetComponent<SwordController>().canAttack;
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

        if (!attacking && !blocking) //tirei !inCombat && 
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
        else if (blocking)
        {
            combatState = States.CombatState.defending;
        }
    }

    //Futuramente ir� fazer as verifica��es necess�rias para escolher o alvo de combate
    public void TryTarget()
    {
        // if (currentEnemy != null || !GameObject.FindGameObjectWithTag("LockEnemy")) return;

        Debug.Log("TryTarget");
        List<GameObject> inimigos = new List<GameObject>();
        inimigos.AddRange(GameObject.FindGameObjectsWithTag("LockEnemy"));

        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject inimigo in inimigos)
        {
            Vector3 directionToTarget = inimigo.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                currentEnemy.UpdateEnemy(inimigo);
                Debug.Log("Update Enemy");
            }
        }


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
        if (!blocking && canAttack) //coloquei if(!blocking) pra nao ativar no meio da espadada
        {
            if (attacking && attackType < 2)
            {

                if ((attackType + 1) <= totalAttacks)
                {

                    attackType++;
                    attacking = true;

                }
            }
            else if (attackType < 2)
            {

                attackType++;
                attacking = true;

            }
            //else if(attackType == 2)
            //{
            //    attacking = false;

            //}

            if (attackType > totalAttacks) attackType = 0;
        }
        else
        {
            //zera os valores se não trava o personagem no estado atacando
            attackType = 0;
            attacking = false;
        }
    }

    public void Block(bool value)
    {
        if (!canAttack && value) return;

        blocking = value;
    }

    public void PerdeStamina()
    {
        healthSystem.m_stamina = healthSystem.stamina;
        healthSystem.stamina -= healthSystem.attackStamina; //diminui Stamina
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
        enemy = _enemy;

        ////Seta valores padr�o se n�o tiver inimigo
        //if (_enemy == null)
        //{
        //    enemyStates = null;
        //    enemyCombatSystem = null;
        //    enemyCombatState = States.CombatState.inactive;
        //    enemyMasterState = States.MasterState.idle;
        //    return;
        //}

        //enemyStates = enemy.GetComponent<StatesSystem>();
        //enemyCombatSystem = enemy.GetComponent<CombatSystem>();
        //enemyMasterState = enemyStates.masterState;
        //enemyCombatState = enemyStates.combatState;
    }
}
