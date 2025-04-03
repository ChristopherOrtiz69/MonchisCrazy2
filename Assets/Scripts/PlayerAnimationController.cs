using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator; 

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError("No se encontró un Animator en los hijos del objeto.");
        }
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool("run", isRunning);
        animator.SetBool("idle", !isRunning);
    }

    public void SetShooting(bool isShooting)
    {
        animator.SetBool("isShooting", isShooting);
        if (isShooting)
        {
            animator.SetTrigger("shoot"); 
        }
    }

    public void SetIdle()
    {
        animator.SetBool("run", false);
        animator.SetBool("idle", true); 
    }

    public void SetHurt()
    {
        animator.SetTrigger("Hurt");
    }
}
