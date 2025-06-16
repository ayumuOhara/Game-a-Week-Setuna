using UnityEngine;

public static class AnimationTrigger
{
    // �U�����A�j���[�V����
    public static void AttackAnim(Animator animator)
    {
        animator.SetTrigger("Attack");
    }

    // �s�k���A�j���[�V����
    public static void DeadAnim(Animator animator)
    {
        animator.SetTrigger("Dead");
    }

    // �������A�j���[�V����
    public static void WinAnim(Animator animator)
    {
        animator.SetTrigger("Win");
    }
}
