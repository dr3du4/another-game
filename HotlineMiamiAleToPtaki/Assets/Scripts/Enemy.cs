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
    [SerializeField] 
    List<Transform> patrolPoints;
    [SerializeField]
    float movementSpeed = 5f;
    [SerializeField]
    float detectionRadius = 5f;
    int currentPatrolIndex = 0;
    

    CircleCollider2D col;
    Transform playerTransform;

    bool canPatrol = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        UpdateDetectionRadius();

        if (patrolPoints.Count > 0) 
        {
            canPatrol = true;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(canPatrol)
        {
            Patrol();   
        }
        else if(playerTransform != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
        }
    }

    private void UpdateDetectionRadius(){
        col.radius = detectionRadius;
    }

    private void Patrol(){
        if(Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, movementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            canPatrol = false;
            playerTransform = other.transform;
        }
    }
}
