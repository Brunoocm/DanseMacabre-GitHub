using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }
    public Vector3 MousePosition { get; private set; }
    public float horizontalInput;
    public float verticalInput;

    private MovementHandler movementHandler => GetComponent<MovementHandler>();
    private CombatSystem combatSystem => GetComponent<CombatSystem>();
    private CameraHandler cameraHandler => GetComponent<CameraHandler>();
    private AnimationHandler animationHandler => GetComponent<AnimationHandler>();

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        InputVector = new Vector2(horizontalInput, verticalInput);

        MousePosition = Input.mousePosition;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            movementHandler.TryMove(new Vector3(InputVector.x, 0, InputVector.y));
            movementHandler.IsMoving(true);
            animationHandler.SetMovementInput(horizontalInput, verticalInput);
        }
        else
        {
            animationHandler.SetMovementInput(0f, 0f);
            movementHandler.IsMoving(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            combatSystem.TryTarget();
            cameraHandler.SwitchTarget();
        }


        if (Input.GetMouseButtonDown(0))
        {
            combatSystem.TryAttack();
        }

        if (Input.GetMouseButton(1))
        {
            combatSystem.Block(true);
        }
        else
            combatSystem.Block(false);
    }
}
