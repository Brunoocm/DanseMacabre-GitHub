using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrigger : MonoBehaviour
{
    public SwordController swordController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) swordController.CanAttack();
    }
}
