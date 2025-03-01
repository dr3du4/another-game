using UnityEngine;

public class ChasingPlayerState : IState
{
    Transform playerTransform;
    Transform transform;
    float movementSpeed;

    public ChasingPlayerState(Transform transform, Transform playerTransform, float moveSpeed)
    {
        this.playerTransform = playerTransform;
        this.transform = transform;
        movementSpeed = moveSpeed;
    }
    public void OnEnter()
    {
        Debug.Log("Entering chase state");
    }

    public void updateState()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
    }

    public void OnExit()
    {
        Debug.Log("Leaving chase state");
    }

}
