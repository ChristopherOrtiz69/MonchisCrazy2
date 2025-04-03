using UnityEngine;
using System.Collections; 

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public PlayerAnimationController animationController; 
    public float shootInterval = 0.2f; 

    private Coroutine shootingCoroutine;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
        {
           
            if (shootingCoroutine == null)
            {
                shootingCoroutine = StartCoroutine(ShootContinuously());
            }
        }

        if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2"))
        {
          
            if (shootingCoroutine != null)
            {
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
                animationController.SetShooting(false);
            }
        }
    }

    private IEnumerator ShootContinuously()
    {
        animationController.SetShooting(true);

        while (true) 
        {
            Shoot(); 
            yield return new WaitForSeconds(shootInterval); 
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
