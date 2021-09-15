using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morte : MonoBehaviour
{
    public float velocity;

    private bool killPlayer;
    void Start()
    {
        
    }

    void Update()
    {
        Run();
    }

    void Run()
    {
        if (!killPlayer)
        {
            transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            killPlayer = true;
        }
    }
}
