using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public AttackState attackState;
    private int attackDamage;

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<HealthSystem>().TakeDamage(attackState.currentDamage);
        }
    }
}
