using UnityEngine;

public class ChasingPlayerState : IState
{
    Transform playerTransform;
    Transform transform;
    float movementSpeed;
    Enemy enemy;
    float attackRadius;

    public ChasingPlayerState(Transform transform, Transform playerTransform, float moveSpeed, Enemy enemy)
    {
        this.playerTransform = playerTransform;
        this.transform = transform;
        this.enemy = enemy;
        movementSpeed = moveSpeed;
    }
    public void OnEnter()
    {
        Debug.Log("Entering chase state");
    }

    public void UpdateState()
    {
        if(Vector2.Distance(transform.position, playerTransform.position) <= attackRadius)
        {
            enemy.TryAttackPlayer(playerTransform);
        }
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
    }

    public void OnExit()
    {
        Debug.Log("Leaving chase state");
    }

}
