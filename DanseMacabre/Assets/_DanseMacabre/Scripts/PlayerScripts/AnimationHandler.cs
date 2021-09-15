using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator anim => GetComponentInChildren<Animator>( );
    public States.CombatState combatState;

    private float horizontalInput;
    private float verticalInput;
    private bool inCombat;

    private void Update()
    {
        SetCombatState( );
    }

    public void SetMovementInput( float _horizontalInput , float _verticalInput )
    {
        anim.SetFloat( "Movement X" , _horizontalInput );
        anim.SetFloat( "Movement Y" , _verticalInput );

        if ( _horizontalInput != 0 || _verticalInput != 0 )
            anim.SetBool( "IsWalking" , true );
        else
            anim.SetBool( "IsWalking" , false );
    }

    public void InCombat( bool value )
    {
        anim.SetBool( "InCombat" , value );
        inCombat = value;
    }

    public void GetCombatState( StatesSystem.CombatState _combatState )
    {
        combatState = _combatState;
    }
    
    private void SetCombatState()
    {
        if ( combatState == States.CombatState.attacking || combatState == States.CombatState.defending )
        {
            anim.SetLayerWeight( 1 , 1 );
        }
        else
            anim.SetLayerWeight( 1 , 0 );

        if ( combatState == States.CombatState.attacking )
            anim.SetBool( "Attacking" , true );
        else
            anim.SetBool( "Attacking" , false );

        if ( combatState == States.CombatState.defending )
            anim.SetBool( "Blocking" , true );
        else
            anim.SetBool( "Blocking" , false );
    }
}
