using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f; // Velocidad de la bala
    public int damage = 10; // Da�o que causar� al jugador
    public float lifeTime = 3f; // Tiempo antes de destruirse

    private Vector2 direction; // Direcci�n de movimiento de la bala

    // M�todo para inicializar la bala con la direcci�n
    public void Initialize(Vector2 dir)
    {
        direction = dir.normalized; // Normalizar la direcci�n para mantener la velocidad constante
    }

    private void Start()
    {
        // Destruir la bala despu�s de un tiempo
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Mover la bala en la direcci�n establecida
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Infligir da�o al jugador
            }
            Destroy(gameObject); // Destruir la bala al impactar en el jugador
        }

        // Destruir la bala si colisiona con cualquier cosa que no sea otra bala
      
    }
}
