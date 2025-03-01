using Unity.VisualScripting;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public IState currentState;
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = newState;
        currentState.OnEnter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.updateState();
        }
    }
}

public interface IState
{
    public void OnEnter();
    public void updateState();
    public void OnExit();
}
