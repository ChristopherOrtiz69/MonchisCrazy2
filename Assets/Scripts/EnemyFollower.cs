using UnityEngine;

public class EnemyFollower : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float stopDistance = 1.5f;
    public float shootDistance = 2f;
    public float separationDistance = 1f;
    public float separationForce = 2f;

    public Transform spriteTransform;
    public Animator animator;

    public int health = 50;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogError("No se encontró un GameObject con la etiqueta 'Player'.");
            }
        }

        FlipSpriteTowardsPlayer();
    }

    void Update()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            direction.Normalize();
            transform.position += direction * moveSpeed * Time.deltaTime;
            animator.Play("run");
        }
        else if (distance <= shootDistance)
        {
            animator.Play("shoot");
        }

        FlipSpriteTowardsPlayer();
    }

    private void FlipSpriteTowardsPlayer()
    {
        if (spriteTransform != null)
        {
            if (player != null)
            {
                if (player.position.x > transform.position.x)
                {
                    spriteTransform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    spriteTransform.localScale = new Vector3(-1, 1, 1);
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
        animator.Play("die");
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(10);
            Destroy(collision.gameObject);
        }
    }
}
