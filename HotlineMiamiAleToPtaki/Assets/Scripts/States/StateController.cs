using Unity.VisualScripting;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public IState currentState;
    Enemy enemy;
    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    public void ChangeState(IState newState)
    {
        if(enemy.isDead) return;
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
            currentState.UpdateState();
        }
    }
}

public interface IState
{
    public void OnEnter();
    public void UpdateState();
    public void OnExit();
}
