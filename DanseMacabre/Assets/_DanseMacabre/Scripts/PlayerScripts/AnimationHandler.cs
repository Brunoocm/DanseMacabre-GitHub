using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator anim => GetComponentInChildren<Animator>();
    public States.CombatState combatState;
    public float aceleracao;
    public float desaceleracao;
    public float zerarEm;

    [Range(-1, 1)] public float horizontalInput;
    [Range(-1, 1)] public float verticalInput;

    [HideInInspector] public bool canMove;


    private bool inCombat;

    private void Update()
    {
        SetCombatState();
    }

    public void GetMovementInput(float _horizontalInput, float _verticalInput)
    {
        if (_horizontalInput > 0 && horizontalInput <= 1) horizontalInput += aceleracao * Time.deltaTime;
        else if (_horizontalInput < 0 && horizontalInput >= -1) horizontalInput -= aceleracao * Time.deltaTime;
        else if (_horizontalInput == 0 && horizontalInput > 0 && horizontalInput > zerarEm) horizontalInput -= desaceleracao * Time.deltaTime;
        else if (_horizontalInput == 0 && horizontalInput < 0 && horizontalInput < zerarEm) horizontalInput += desaceleracao * Time.deltaTime;
        else if (_horizontalInput == 0 && (horizontalInput < zerarEm || horizontalInput > zerarEm)) horizontalInput = 0;

        if (_verticalInput > 0 && verticalInput <= 1) verticalInput += aceleracao * Time.deltaTime;
        else if (_verticalInput < 0 && verticalInput >= -1) verticalInput -= aceleracao * Time.deltaTime;
        else if (_verticalInput == 0 && verticalInput > 0 && verticalInput > zerarEm) verticalInput -= desaceleracao * Time.deltaTime;
        else if (_verticalInput == 0 && verticalInput < 0 && verticalInput < zerarEm) verticalInput += desaceleracao * Time.deltaTime;
        else if (_verticalInput == 0 && (verticalInput < zerarEm || verticalInput > zerarEm)) verticalInput = 0;

        SetMovementInput(horizontalInput, verticalInput);
    }

    private void SetMovementInput(float _horizontalInput, float _verticalInput)
    {
        if (combatState == States.CombatState.attacking)
        {
            anim.SetBool("IsWalking", false);
            anim.SetLayerWeight(0, 0);
        }

        anim.SetFloat("Movement X", _horizontalInput);
        anim.SetFloat("Movement Y", _verticalInput);

        if (_horizontalInput != 0 || _verticalInput != 0)
            anim.SetBool("IsWalking", true);
        else
            anim.SetBool("IsWalking", false);
    }

    public void InCombat(bool value)
    {
        anim.SetBool("InCombat", value);
        inCombat = value;
    }

    public void GetCombatState(StatesSystem.CombatState _combatState)
    {
        combatState = _combatState;
    }

    private void SetCombatState()
    {
        if (combatState == States.CombatState.attacking || combatState == States.CombatState.defending)
        {
            anim.SetLayerWeight(1, 1);
        }
        else
            anim.SetLayerWeight(1, 0);

        if (combatState == States.CombatState.attacking)
            anim.SetBool("Attacking", true);
        else
            anim.SetBool("Attacking", false);

        if (combatState == States.CombatState.defending)
            anim.SetBool("Blocking", true);
        else
            anim.SetBool("Blocking", false);
    }
}
