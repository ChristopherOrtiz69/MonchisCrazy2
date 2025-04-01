using UnityEngine;

public class MouseMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    private Transform spriteTransform; // Referencia al hijo que contiene el sprite

    void Start()
    {
        // Buscar el objeto hijo con el sprite
        spriteTransform = transform.Find("Player");

        if (spriteTransform == null)
        {
            Debug.LogError("No se encontró el objeto hijo con el sprite. Verifica el nombre.");
        }
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = mousePosition - transform.position;

        // Voltear solo el objeto hijo en el eje X según la dirección
        if (spriteTransform != null)
        {
            if (direction.x < 0)
                spriteTransform.localScale = new Vector3(-1, 1, 1); // Mirando a la izquierda
            else
                spriteTransform.localScale = new Vector3(1, 1, 1); // Mirando a la derecha
        }

        // Mover hacia el mouse si hay distancia suficiente
        if (direction.magnitude > 0.1f)
        {
            direction.Normalize();
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        // Limitar el movimiento dentro de los límites
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
        transform.position = new Vector3(clampedX, clampedY, 0);
    }
}
