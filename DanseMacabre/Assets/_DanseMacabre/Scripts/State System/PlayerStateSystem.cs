using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateSystem : StatesSystem
{
    public MovementHandler movementHandler => GetComponent<MovementHandler>();
    private CombatSystem combatSystem => GetComponent<CombatSystem>();
    private AnimationHandler animationHandler => GetComponent<AnimationHandler>();

    [HideInInspector] public bool canMove;
    private bool isWalking;

    private void Update()
    {
        inCombat = combatSystem.hasEnemy;

        UpdateStates();
        UpdateCurrentInfo();
        UpdateMovementHandler();
        UpdateAnimationHandler();
    }

    private void UpdateStates()
    {
        if (isWalking) masterState = MasterState.walking;

        combatState = combatSystem.combatState;

        if (combatState != States.CombatState.inactive)
            masterState = States.MasterState.combat;
        else if (!isWalking)
            masterState = States.MasterState.idle;
    }

    private void UpdateCurrentInfo()
    {
        //Combate
        inCombat = combatSystem.inCombat;
        currentEnemy = combatSystem.currentEnemy;
        canMove = !combatSystem.attacking;

        //Movement
        isWalking = movementHandler.isWalking;
    }

    private void UpdateMovementHandler()
    {
        movementHandler.RotateTowardTarget(currentEnemy.enemy, inCombat);
        movementHandler.canMove = canMove;
    }

    private void UpdateAnimationHandler()
    {
        animationHandler.InCombat(inCombat);
        animationHandler.GetCombatState(combatState);
    }
}
