using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocked : MonoBehaviour
{
    public string doorName;

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void OpenTheDoor()
    {
        print("open");
        anim.SetTrigger("Open");
        //Destroy(gameObject);
    }
}
