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

    protected override void itemUse()
    {
        if (isSwinging) return;
        animator.SetTrigger("triggerSwing");
        Debug.Log("trying to play animation");
        isSwinging = true;
        Invoke("resetSwing", swingAnimation.length);
    }

    private void resetSwing()
    {
        isSwinging = false;
    }
}
