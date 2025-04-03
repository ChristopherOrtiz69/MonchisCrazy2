using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint; // Punto desde donde se dispara
    public float shootingInterval = 2f; // Intervalo entre disparos
    public float bulletSpeed = 10f; // Velocidad de la bala

    private void Start()
    {
        // Busca al jugador por etiqueta al inicio
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("No se encontró un GameObject con la etiqueta 'Player'.");
        }

        // Comenzar la corutina de disparo
        StartCoroutine(ShootAtPlayer());
    }

    private IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            if (player != null)
            {
                // Disparar solo si el jugador está dentro de un rango
                Vector3 direction = player.position - transform.position;
                float distance = direction.magnitude;

                if (distance <= 10f) // Ajusta el rango de disparo según sea necesario
                {
                    Shoot();
                }
            }
            yield return new WaitForSeconds(shootingInterval);
        }
    }

    private void Shoot()
    {
        if (firePoint == null) return;

        // Crear la bala del enemigo
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
        if (enemyBullet != null)
        {
            // Calcular la dirección hacia el jugador
            Vector2 direction = (player.position - firePoint.position).normalized;
            enemyBullet.Initialize(direction); // Inicializar la bala con la dirección hacia el jugador
        }
    }
}
