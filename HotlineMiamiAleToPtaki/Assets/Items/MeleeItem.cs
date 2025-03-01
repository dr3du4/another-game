using System.Collections;
using UnityEngine;

public class MeleeItem : Item
{
    [SerializeField]
    AnimationClip swingAnimation;
    protected bool isSwinging = false;

    Animator animator;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    protected override bool CanThrow()
    {
        return isHeld && !isThrowed && !isSwinging;
    }

    [SerializeField] private float swingDuration = 0.2f; // Czas trwania jednego etapu
    [SerializeField] private Transform pivotPoint; // Punkt obrotu (np. rękojeść)
    

    protected override void itemUse()
    {
        if (isSwinging) return;
        Debug.Log("Swinging sword using Quaternion in multiple steps!");

        isSwinging = true;
        StartCoroutine(SwingSequence());
    }

    private IEnumerator SwingSequence()
    {
        yield return RotateToAngle(-75); // Pierwszy ruch do -45°
        yield return RotateToAngle(90);  // Następnie do 50°
        yield return RotateToAngle(0);   // Powrót do 0°
        
        isSwinging = false; // Koniec zamachu
    }

    private IEnumerator RotateToAngle(float targetAngle)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        while (elapsedTime < swingDuration)
        {
            float t = elapsedTime / swingDuration;
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = targetRotation; // Dokładne ustawienie końcowego kąta
    }
}
