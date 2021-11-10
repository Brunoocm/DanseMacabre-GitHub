using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorLocked : MonoBehaviour
{
    public string doorName;
    public UnityEvent events;
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
        events.Invoke();
        //Destroy(gameObject);
    }
}
