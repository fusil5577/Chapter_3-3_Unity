using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void PlayMoveAnimation(Vector3 direction)
    {
        if (direction == Vector3.left)
        {
            animator.SetTrigger("MoveForward");
        }
        else if (direction == Vector3.right)
        {
            animator.SetTrigger("MoveBack");
        }
        else if (direction == Vector3.forward)
        {
            animator.SetTrigger("MoveRight");
        }
        else if (direction == Vector3.back)
        {
            animator.SetTrigger("MoveLeft");
        }
    }
}
