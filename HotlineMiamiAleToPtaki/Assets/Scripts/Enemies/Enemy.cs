using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using System.Collections;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    // detection radius
    // patrol path
    // attack range
    [Header("Movement and patrol")]
    [SerializeField] 
    List<Transform> patrolPoints;
    [SerializeField]
    float movementSpeed = 5f;

    int currentPatrolIndex = 0;
    
    [Header("Detections")]
    [SerializeField]
    float detectionRadius = 5f;

    [Header("Attacks")]
    [SerializeField]
    float attackCooldown = 3f;
    bool attackOnCooldown = false;
    bool isAttacking = false;
    
    Transform playerTransform;

    bool canPatrol = false;

    IState defaultState;

    StateController stateController;
    void Start()
    {
        UpdateDetectionRadius();
        stateController = GetComponent<StateController>();

        if (patrolPoints.Count > 0) 
        {
            PatrolState patrolState = new PatrolState(transform, patrolPoints, movementSpeed);
            stateController.ChangeState(patrolState);
            defaultState = patrolState;
        } 
        else{
            defaultState = new IdleState();
        }
    }

    void Update()
    {
        if (!isAttacking){
            MovementUpdate();
        }
    }

    private void MovementUpdate(){
        if(canPatrol)
        {
            //Patrol();   
        }
        else if(playerTransform != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
        }
    }

    private void UpdateDetectionRadius(){
        GetComponentInChildren<PlayerRadiusDetection>().UpdateDetectionRadius(detectionRadius);
    }

    private void Patrol(){
        if(Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, movementSpeed * Time.deltaTime);
    }

    public void OnEnteredGroatsDetectionRadius(Transform groatsTransform)
    {
        if(stateController.currentState is ChasingPlayerState)
        {
            return;
        }
        Debug.Log("Entered groats detection radius");
        EatingState eatingState = new EatingState(transform, groatsTransform, movementSpeed);
        stateController.ChangeState(eatingState);
        detectionRadius -= 1f;
        UpdateDetectionRadius();
    }

    public void OnExitedGroatsDetectionRadius()
    {
        if(stateController.currentState is ChasingPlayerState)
        {
            return;
        }
        stateController.ChangeState(defaultState);
        detectionRadius += 1f;
        UpdateDetectionRadius();
    }

    public void OnPlayerEnteredDetectionRadius(Transform playerTransform)
    {
        stateController.ChangeState(new ChasingPlayerState(transform, playerTransform, movementSpeed));
    }

    public void OnPlayerEnteredAttackRadius(Transform playerTransform)
    {
        if(!attackOnCooldown)
        {
            StartCoroutine(AttackCooldown());
            Attack(playerTransform);
        }
    }

    public IState GetCurrentState(){
        return stateController.currentState;
    }

    private IEnumerator AttackCooldown()
    {
        attackOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        attackOnCooldown = false;
    }

    protected virtual void Attack(Transform playerTransform)
    {
        Debug.Log("Attacking player");
    }

    protected IEnumerable LockMovementForAttack(float attackTime){
        isAttacking = true;
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }

    public void RegisterHit()
    {
        Debug.Log("Enemy hit");
    }
}
