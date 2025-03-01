using UnityEngine;

public class EatingState : IState
{
    Transform groatsTransform;
    Transform transform;
    float movementSpeed;

    public EatingState(Transform transform, Transform groatsTransform, float moveSpeed)
    {
        this.groatsTransform = groatsTransform;
        this.transform = transform;
        this.movementSpeed = moveSpeed;
    }
     
    public void OnEnter()
    {
        Debug.Log("Entering eating state");
    }

    public void UpdateState()
    {
        if(Vector2.Distance(transform.position, groatsTransform.position) > 1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, groatsTransform.position, movementSpeed * Time.deltaTime);
        }
    }

    public void OnExit()
    {
        Debug.Log("Leaving eating state");
    }
}
