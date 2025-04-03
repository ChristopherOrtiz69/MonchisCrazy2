using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100; // Salud del jugador

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Jugador recibió {damage} de daño. Salud restante: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("El jugador ha muerto.");
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletEnemie"))
        {
            TakeDamage(10); 
            Destroy(collision.gameObject); 
        }
    }
}
