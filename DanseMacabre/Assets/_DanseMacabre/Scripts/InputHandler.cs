using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }
    public Vector3 MousePosition { get; private set; }
    [HideInInspector]
    public bool walking;

    void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        InputVector = new Vector2(h, v);

        MousePosition = Input.mousePosition;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            walking = true;
        }   
        else walking = false;
    }
}
