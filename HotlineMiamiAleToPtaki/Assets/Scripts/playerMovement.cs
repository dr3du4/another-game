using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{ 
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    public bool isDead = false;
    private PlayerPickup playerPickup;
    [SerializeField]
    Sprite deadPlayerSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Pobieramy SpriteRenderer
        playerPickup = GetComponent<PlayerPickup>();
    }

    void Update()
    {
        if(isDead)
        {
            return;
        }
        // Pobieranie wejścia
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Sprawdzenie, czy gracz się porusza
        bool isMoving = movement.sqrMagnitude > 0;
        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
        {
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
        }
    }

    void FixedUpdate()
    {
        if(isDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
    public void TakeDamage()
    {
        if(isDead)
        {
            return;
        }
        GetComponent<Collider2D>().enabled = false;
        Debug.Log("Player Hit and died");
        isDead = true;
        playerPickup.setDead();
        Destroy(animator);
        ParticleSystem blood = GetComponentInChildren<ParticleSystem>();
        blood.Play();

        spriteRenderer.sprite = deadPlayerSprite;
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
}
