using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Transform cameraTargetTransform;
    [SerializeField] private float rotationPower = 3f;
    [SerializeField] private bool yInverted = true;
    [SerializeField] private bool isControlling = true;
    [SerializeField] private CinemachineVirtualCamera camera;

    private StatesSystem statesSystem;

    private void Awake()
    {
        statesSystem = GetComponent<StatesSystem>( );
    }

    private void Update()
    {
        if ( statesSystem.hasEnemy )
        {
            isControlling = false;
            camera.LookAt = statesSystem.currentEnemy.enemy.transform;
        }
        else
        {
            isControlling = true;
            camera.LookAt = null;
        }

        if ( !isControlling )
            return;

        if(!yInverted) cameraTargetTransform.rotation *= Quaternion.AngleAxis( Input.GetAxis( "Mouse Y" ) * rotationPower , Vector3.right );
        else cameraTargetTransform.rotation *= Quaternion.AngleAxis( Input.GetAxis( "Mouse Y" ) * rotationPower , Vector3.left );
        cameraTargetTransform.rotation *= Quaternion.AngleAxis( Input.GetAxis( "Mouse X" ) * rotationPower , Vector3.up );

        var angles = cameraTargetTransform.localEulerAngles;
        var angle = cameraTargetTransform.localEulerAngles.x;
        angles.z = 0;

        if ( angle > 180 && angle < 340 ) angles.x = 340;
        else if ( angle < 180 && angle > 40 ) angles.x = 40;

        cameraTargetTransform.localEulerAngles = angles;
    }
}
