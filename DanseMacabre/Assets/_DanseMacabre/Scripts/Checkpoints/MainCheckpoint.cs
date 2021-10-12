using UnityEngine;
using UnityEngine.SceneManagement;
public class MainCheckpoint : MonoBehaviour
{
    [HideInInspector] public GameObject playerObj;
    [HideInInspector] public Transform[] checkpoints;

    public Vector3 spawn;
    RespawnSystem respawnSystem;
    private void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
        respawnSystem = FindObjectOfType<RespawnSystem>();

        if (respawnSystem.isDead)
        {
            respawnSystem.Respawn();
        }
    }
    void Start()
    {
        GetObjects();


    }

    void Update()
    {
        CheckIsActive();
    }

    void CheckIsActive()
    {
        
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = playerObj.transform.position;

        foreach (Transform pointsTarget in checkpoints)
        {
            if (pointsTarget.GetComponent<Points>().isActive)
            {
                Vector3 directionToTarget = pointsTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    spawn = new Vector3 (pointsTarget.position.x, pointsTarget.position.y, pointsTarget.position.z);

                }
                else
                {
                  
                }
            }
        }

    }

    void GetObjects()
    {
        checkpoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            checkpoints[i] = transform.GetChild(i).transform;

        }

    }

 
}
