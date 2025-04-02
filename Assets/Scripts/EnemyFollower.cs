using UnityEngine;

public class EnemyFollower : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float stopDistance = 1.5f;
    public float idleDistance = 5f;
    public float shootDistance = 2f;
    public float separationDistance = 1f; // Distancia mínima entre enemigos

    public Transform spriteTransform;
    public Animator animator;

    public int health = 50; // Vida del enemigo

    void Update()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        // Separación de enemigos
        SeparateEnemies();

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
        else if (distance <= shootDistance)
        {
            animator.Play("shoot");
        }

        // Voltear el sprite según la posición del jugador
        if (spriteTransform != null)
        {
            spriteTransform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
        }
    }

    private void SeparateEnemies()
    {
        // Obtener todos los enemigos en la escena
        EnemyFollower[] enemies = FindObjectsOfType<EnemyFollower>();

        foreach (EnemyFollower enemy in enemies)
        {
            if (enemy != this) // No comparar consigo mismo
            {
                float distanceToOther = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToOther < separationDistance)
                {
                    // Calcular dirección de separación
                    Vector3 separationDirection = (transform.position - enemy.transform.position).normalized;
                    // Mover al enemigo actual en la dirección de separación
                    transform.position += separationDirection * (separationDistance - distanceToOther);
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Enemigo recibió {damage} de daño. Vida restante: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemigo eliminado");
        animator.Play("die"); // Asumiendo que hay una animación de muerte
        Destroy(gameObject, 0.5f); // Esperar un poco antes de destruir
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(10); // Recibe daño cuando una bala lo toca
            Destroy(collision.gameObject); // Destruir la bala
        }
    }
}
