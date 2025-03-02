using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using System.Linq;

[RequireComponent(typeof(StateController))]
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
    
    [Header("Detections")]
    [SerializeField]
    float detectionRadius = 5f;

    [Header("Attacks")]
    [SerializeField]
    float attackCooldown = 3f;
    bool isAttacking = false;

    [SerializeField]
    Sprite deadSprite;

    [Header("Sounds")]
    [SerializeField]
    AudioClip deathSound;
    
    Transform playerTransform;

    bool canPatrol = false;

    IState defaultState;

    StateController stateController;

    private Animator animator;

    private PlayerDamagingArea playerDamagingArea;

    public bool isDead { get; private set; }
    void Start()
    {
        UpdateDetectionRadius();
        stateController = GetComponent<StateController>();
        animator = GetComponent<Animator>();
        playerDamagingArea = GetComponentInChildren<PlayerDamagingArea>();

        if (patrolPoints.Count > 0) 
        {
            PatrolState patrolState = new PatrolState(transform, patrolPoints);
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
        stateController.ChangeState(new ChasingPlayerState(transform, playerTransform));
    }

    public IState GetCurrentState(){
        return stateController.currentState;
    }

    protected virtual void Attack(Transform playerTransform)
    {
        
        StartCoroutine(LockMovementForAttack(GetAttackAnimationLength()));
        
    }

    private float GetAttackAnimationLength(){
        AnimationClip clip = animator.runtimeAnimatorController.animationClips.Where(anim => anim.name == "Attack").FirstOrDefault();
        return clip.length;
    }

    protected IEnumerator LockMovementForAttack(float attackTime){
        animator.SetBool("IsAttacking", true);
        isAttacking = true;    
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
    }

    public void RegisterHit()
    {
        Die();
    }

    public void TryAttackPlayer(Transform playerTransform)
    {
        if(!isAttacking)
        {
            Debug.Log("Attacking player");
            Attack(playerTransform);
        }

    }

    protected virtual void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        ParticleSystem blood = GetComponentInChildren<ParticleSystem>();
        blood.Play();
        Debug.Log("Enemy died");
        stateController.ChangeState(new DeadState(this));
        isDead = true;
        SoundManager.Instance.PlaySound(deathSound, transform);
        // disable animations
        BerserkManager.Instance.RegisterKill();
        if(deadSprite != null){
            GetComponentInChildren<SpriteRenderer>().sprite = deadSprite;
        }
        else{
            Debug.LogError("Dead sprite not set - not game breaking but please set it");
        }
        StartCoroutine(StopParticleEmission(blood, 0.07f));
    }

    IEnumerator StopParticleEmission(ParticleSystem particleSystem, float time)
    {
        yield return new WaitForSeconds(time);
        particleSystem.Stop();

        // Stop particle movement by setting velocity to zero
        var particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        int particleCount = particleSystem.GetParticles(particles);

        for (int i = 0; i < particleCount; i++)
        {
            particles[i].velocity = Vector3.zero;
        }

        particleSystem.SetParticles(particles, particleCount);
    }

    public void Move(Transform target){
        if (transform.position.x - target.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        animator.SetBool("IsMoving", true);
        transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
    }
}
