using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    List<Transform> patrolPoints;
    Transform transform;

    int currentPatrolIndex;

    Enemy enemy;

    public PatrolState(Transform transform, List<Transform> patrolPoints)
    {
        this.patrolPoints = patrolPoints;
        currentPatrolIndex = 0;
        this.transform = transform;
        enemy = transform.GetComponent<Enemy>();
    }
    public void OnEnter()
    {
        Debug.Log("Entering patrol state");
    }

    public void UpdateState()
    {
        if(Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        enemy.Move(patrolPoints[currentPatrolIndex]);
    }

    public void OnExit()
    {
        Debug.Log("Leaving patrol state");
    }
}
