using UnityEngine;

public static class AnimationTrigger
{
    // 攻撃時アニメーション
    public static void AttackAnim(Animator animator)
    {
        animator.SetTrigger("Attack");
    }

    // 敗北時アニメーション
    public static void DeadAnim(Animator animator)
    {
        animator.SetTrigger("Dead");
    }

    // 勝利時アニメーション
    public static void WinAnim(Animator animator)
    {
        animator.SetTrigger("Win");
    }
}
