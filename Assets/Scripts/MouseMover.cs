using UnityEngine;

public class MouseMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Transform spriteTransform;
    private Rigidbody2D rb;
    private bool isHurt = false;
    public PlayerAnimationController animationController;

    void Start()
    {
        spriteTransform = transform.Find("Player");
        rb = GetComponent<Rigidbody2D>();

        if (spriteTransform == null)
        {
            Debug.LogError("No se encontró el objeto hijo con el sprite. Verifica el nombre.");
        }

        if (rb == null)
        {
            Debug.LogError("No se encontró un Rigidbody2D en el objeto.");
        }
    }

    void Update()
    {
        if (isHurt) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = mousePosition - transform.position;

        if (spriteTransform != null)
        {
            if (direction.x < 0)
                spriteTransform.localScale = new Vector3(-1, 1, 1);
            else
                spriteTransform.localScale = new Vector3(1, 1, 1);
        }

        // Movimiento del jugador
        if (direction.magnitude > 0.1f && !animationController.animator.GetBool("isShooting"))
        {
            direction.Normalize();
            rb.velocity = direction * moveSpeed;
            animationController.SetRunning(true);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animationController.SetIdle(); // Cambiar a idle
        }

        // Disparo
        if (Input.GetButtonDown("Fire1"))
        {
            animationController.SetShooting(true); // Activar disparo
            rb.velocity = Vector2.zero; // Detener el movimiento al disparar
        }

        // Al soltar el botón, volver a permitir el movimiento
        if (Input.GetButtonUp("Fire1"))
        {
            animationController.SetShooting(false); // Desactivar el estado de disparo
        }

        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
        transform.position = new Vector3(clampedX, clampedY, 0);
    }

    public void TakeDamage()
    {
        if (animationController != null)
        {
            isHurt = true;
            animationController.SetHurt();
            Invoke(nameof(ResetHurtState), 0.5f);
        }
    }

    private void ResetHurtState()
    {
        isHurt = false;
    }
}
