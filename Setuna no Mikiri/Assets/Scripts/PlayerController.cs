using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                AnimationTrigger.AttackAnim(animator);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                AnimationTrigger.WinAnim(animator);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                AnimationTrigger.DeadAnim(animator);
            }
        }
    }
}
