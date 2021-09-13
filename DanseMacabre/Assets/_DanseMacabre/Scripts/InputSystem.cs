using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }
    public Vector3 MousePosition { get; private set; }

    private MovementHandler movementHandler;
    private CombatSystem combatSystem;
    private CameraHandler cameraHandler;

    [HideInInspector]
    public bool walking;

    private void Awake()
    {
        movementHandler = GetComponent<MovementHandler>();
        combatSystem = GetComponent<CombatSystem>( );
        cameraHandler = GetComponent<CameraHandler>( );
    }

    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        InputVector = new Vector2(h, v);

        MousePosition = Input.mousePosition;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            movementHandler.TryMove(new Vector3(InputVector.x, 0, InputVector.y));
        }   
        
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            combatSystem.TryTarget( );
            movementHandler.RotateTowardTarget( );
            cameraHandler.SwitchTarget( );
        }
    }
}
