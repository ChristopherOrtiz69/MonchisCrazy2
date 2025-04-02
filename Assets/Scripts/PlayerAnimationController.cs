using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator; // Hacer que el Animator sea público

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
        animator.SetBool("idle", !isRunning); // Cambiar a idle si no está corriendo
    }

    public void SetShooting(bool isShooting)
    {
        animator.SetBool("isShooting", isShooting);
        if (isShooting)
        {
            animator.SetTrigger("shoot"); // Activar la animación de disparo
        }
    }

    public void SetIdle()
    {
        animator.SetBool("run", false);
        animator.SetBool("idle", true); // Activar idle
    }

    public void SetHurt()
    {
        animator.SetTrigger("Hurt");
    }
}
