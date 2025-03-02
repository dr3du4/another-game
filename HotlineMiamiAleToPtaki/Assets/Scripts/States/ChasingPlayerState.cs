using UnityEngine;

public class ChasingPlayerState : IState
{
    Transform playerTransform;
    Transform transform;
    Enemy enemy;
    float attackRadius = 3f;

    public ChasingPlayerState(Transform transform, Transform playerTransform, Enemy enemy)
    {
        this.playerTransform = playerTransform;
        this.transform = transform;
        this.enemy = enemy;
    }
    public void OnEnter()
    {
        Debug.Log("Entering chase state");
    }

    public void UpdateState()
    {
        if(Vector2.Distance(transform.position, playerTransform.position) <= attackRadius)
        {
            if(playerTransform.GetComponent<playerMovement>().isDead){
                return;
            }
            enemy.TryAttackPlayer(playerTransform);
        }
        else if (!enemy.isAttacking){
            enemy.Move(playerTransform);
        }
    }

    public void OnExit()
    {
        Debug.Log("Leaving chase state");
    }

}
