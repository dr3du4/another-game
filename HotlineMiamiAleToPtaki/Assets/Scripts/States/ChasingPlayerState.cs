using UnityEngine;

public class ChasingPlayerState : IState
{
    Transform playerTransform;
    Transform transform;
    Enemy enemy;
    float attackRadius = 3f;

    public ChasingPlayerState(Transform transform, Transform playerTransform)
    {
        this.playerTransform = playerTransform;
        this.transform = transform;
        enemy = transform.GetComponent<Enemy>();
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
        else{
            enemy.Move(playerTransform);
        }
    }

    public void OnExit()
    {
        Debug.Log("Leaving chase state");
    }

}
