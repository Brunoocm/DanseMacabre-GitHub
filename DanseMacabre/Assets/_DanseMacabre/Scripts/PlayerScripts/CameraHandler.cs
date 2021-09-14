using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Transform normalCameraTarget;
    [SerializeField] private Transform combatCameraTarget;
    [SerializeField] private float rotationPower = 3f;
    [SerializeField] private bool isControlling = true;
    [SerializeField] private CinemachineVirtualCamera aimCamera;

    private StatesSystem statesSystem;

    private void Awake()
    {
        statesSystem = GetComponent<StatesSystem>( );
        isControlling = true;
    }

    private void Update()
    {
        if ( isControlling )
            AimControl( );
        else if ( statesSystem.currentEnemy.enemy != null )
                combatCameraTarget.LookAt( statesSystem.currentEnemy.enemy.transform );
    }

    //Alterna entre o estado de foco de combate e livre da camera
    public void SwitchTarget()
    {
        isControlling = isControlling ? false : true;
        aimCamera.enabled = isControlling ? true : false;
    }

    private void AimControl()
    {
        normalCameraTarget.rotation *= Quaternion.AngleAxis( Input.GetAxis( "Mouse X" ) * rotationPower , Vector3.up );
        normalCameraTarget.rotation *= Quaternion.AngleAxis( Input.GetAxis( "Mouse Y" ) * rotationPower , Vector3.left );

        var angles = normalCameraTarget.localEulerAngles;
        var angle = normalCameraTarget.localEulerAngles.x;
        angles.z = 0;

        if ( angle > 180 && angle < 340 )
            angles.x = 340;
        else if ( angle < 180 && angle > 40 )
            angles.x = 40;

        normalCameraTarget.localEulerAngles = angles;
    }
}
