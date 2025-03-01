using UnityEngine;

public class IdleState : IState
{
    public void OnEnter()
    {
        Debug.Log("Entering idle state");
    }

    public void updateState()
    {
        Debug.Log("Idle state");
    }

    public void OnExit()
    {
        Debug.Log("Leaving idle state");
    }
}
