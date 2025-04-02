using UnityEngine;
using System.Collections; // Agregar esta línea

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public PlayerAnimationController animationController; // Nueva referencia
    public float shootInterval = 0.2f; // Tiempo entre disparos

    private Coroutine shootingCoroutine; // Para manejar la corutina de disparo

    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
        {
            // Iniciar la corutina para disparar
            if (shootingCoroutine == null)
            {
                shootingCoroutine = StartCoroutine(ShootContinuously());
            }
        }

        if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2"))
        {
            // Detener la corutina de disparo
            if (shootingCoroutine != null)
            {
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
                animationController.SetShooting(false); // Desactivar animación de disparo
            }
        }
    }

    private IEnumerator ShootContinuously()
    {
        animationController.SetShooting(true); // Activar animación de disparo

        while (true) // Bucle infinito hasta que se detenga la corutina
        {
            Shoot(); // Disparar
            yield return new WaitForSeconds(shootInterval); // Esperar el intervalo antes de disparar de nuevo
        }
    }

    void Shoot()
    {
        if (firePoint == null)
        {
            Debug.LogError("No se asignó un firePoint en el Inspector.");
            return;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector2 shootDirection = (mousePosition - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDirection(shootDirection);
        }

        FlipPlayer(mousePosition);
    }

    void FlipPlayer(Vector3 targetPosition)
    {
        if (targetPosition.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
}
