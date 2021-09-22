using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPrototipo : MonoBehaviour
{
    private Animator animator => GetComponent<Animator>();

    public void PlayAnimation(int value)
    {
        Debug.Log("Play damage");
        animator.SetInteger("Animation", value);
    }

    public void ResetAnimation()
    {
        animator.SetInteger("Animation", 0);
    }
}
