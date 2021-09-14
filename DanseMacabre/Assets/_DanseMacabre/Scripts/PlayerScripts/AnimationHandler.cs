using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator anim => GetComponentInChildren<Animator>();

    private void Update()
    {

    }

    public void SetMovementInput(float horizontalInput, float verticalInput)
    {
        anim.SetFloat( "Movement X" , horizontalInput );
        anim.SetFloat( "Movement Y" , verticalInput );

        if ( horizontalInput != 0 || verticalInput != 0 )
            anim.SetBool( "IsWalking" , true );
        else
            anim.SetBool( "IsWalking" , false );
    }

    public void InCombat (bool value)
    {
        anim.SetBool( "InCombat" , value );
    }
}
