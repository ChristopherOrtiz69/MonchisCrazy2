using UnityEngine;

public class EnemyFollower : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float stopDistance = 1.5f; // Distancia a la que el enemigo deja de moverse y comienza a disparar
    public float idleDistance = 5f; // Distancia a la que el enemigo entra en estado idle
    public float shootDistance = 2f; // Distancia a la que el enemigo dispara
    public Transform spriteTransform; // Referencia al sprite del enemigo
    public Animator animator; // Referencia al Animator del enemigo

    void Update()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if (distance > idleDistance)
        {
            animator.Play("idle");
        }
        else if (distance > stopDistance)
        {
            direction.Normalize();
            transform.position += direction * moveSpeed * Time.deltaTime;
            animator.Play("run");
        }
        else if (distance <= shootDistance) // Activar disparo si está dentro de la distancia de disparo
        {
            animator.Play("shoot");
        }

        // Voltear el sprite en la dirección del jugador
        if (spriteTransform != null)
        {
            // Mirar hacia la derecha si el jugador está a la derecha, de lo contrario mirar a la izquierda
            spriteTransform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
        }
    }
}
