using System.Collections;
using UnityEngine;

public class door : MonoBehaviour
{
    public Transform doorTransform; // Obiekt drzwi, który będzie się obracał
    public float openAngle = -90f; // Kąt otwarcia drzwi
    public float openSpeed = 2f; // Prędkość otwierania/zamykania
    private bool isOpen = false; // Czy drzwi są otwarte?
    private bool isPlayerNearby = false; // Czy gracz jest w pobliżu?
    private Quaternion closedRotation; // Początkowa rotacja drzwi
    private Quaternion openRotation; // Docelowa rotacja otwartych drzwi

    void Start()
    {
        
        closedRotation = doorTransform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0,0 ,openAngle);
    }

    void Update()
    {
       
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Open");
            StopAllCoroutines(); 
            StartCoroutine(ToggleDoor());
        }
    }

    IEnumerator ToggleDoor()
    {
        Quaternion targetRotation = isOpen ? closedRotation : openRotation;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            doorTransform.rotation = Quaternion.Slerp(doorTransform.rotation, targetRotation, elapsedTime * openSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        doorTransform.rotation = targetRotation;
        isOpen = !isOpen; // Zmieniamy stan drzwi
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Sprawdzamy, czy to gracz
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}