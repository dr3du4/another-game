using UnityEngine;

public class DeadState : IState
{
    Enemy enemy;
    public DeadState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void OnEnter()
    {
        Debug.Log("Enemy is dead");
    }

    public void UpdateState()
    {
        return;
    }

    public void OnExit()
    {
        Debug.Log("Enemy is no longer dead");
    }
}
