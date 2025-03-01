using UnityEngine;

public class Groats : MonoBehaviour
{
    [SerializeField]
    float lifespan = 3f;

    bool alreadyCountingDown = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !alreadyCountingDown && other.gameObject.GetComponent<Enemy>().GetCurrentState() is not ChasingPlayerState)
        {
            Debug.Log("Groats collided with enemy");
            alreadyCountingDown = true;
            Invoke("DestroyGroats", lifespan);
        }
    }

    private void DestroyGroats()
    {
        Debug.Log("Destroying Groats");
        Destroy(gameObject);
    }

}
