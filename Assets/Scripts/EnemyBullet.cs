using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f; // Velocidad de la bala
    public int damage = 10; // Daño que causará al jugador
    public float lifeTime = 3f; // Tiempo antes de destruirse

    private Vector2 direction; // Dirección de movimiento de la bala

    // Método para inicializar la bala con la dirección
    public void Initialize(Vector2 dir)
    {
        direction = dir.normalized; // Normalizar la dirección para mantener la velocidad constante
    }

    private void Start()
    {
        // Destruir la bala después de un tiempo
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Mover la bala en la dirección establecida
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Infligir daño al jugador
            }
            Destroy(gameObject); // Destruir la bala al impactar en el jugador
        }

        // Destruir la bala si colisiona con cualquier cosa que no sea otra bala
      
    }
}
