using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMorte : MonoBehaviour
{

    Animator anim;
    private bool oneTime;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Abrir");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!oneTime)
            {
                FindObjectOfType<RespawnSystem>().LoadSceneInicial();
                oneTime = true;
            }
        }

    }
}
