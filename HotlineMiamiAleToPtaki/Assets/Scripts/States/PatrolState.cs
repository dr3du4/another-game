using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    List<Transform> patrolPoints;
    Transform transform;

    int currentPatrolIndex;
    float movementSpeed;

    public PatrolState(Transform transform, List<Transform> patrolPoints, float moveSpeed)
    {
        this.patrolPoints = patrolPoints;
        currentPatrolIndex = 0;
        movementSpeed = moveSpeed;
        this.transform = transform;
    }
    public void OnEnter()
    {
        Debug.Log("Entering patrol state");
    }

    public void updateState()
    {
        if(Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, movementSpeed * Time.deltaTime);
    }

    public void OnExit()
    {
        Debug.Log("Leaving patrol state");
    }
}
