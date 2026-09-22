using UnityEngine;
public class DestroyOutOfBounds2D : MonoBehaviour
{
    [Tooltip("Margen extra (en fracción de pantalla) antes de considerar el objeto fuera de cámara.")]
    [SerializeField] private float margin = 0.1f;

    [Tooltip("Actívalo solo en los enemigos: resta una vida si salen por el borde inferior. Déjalo en false para el Projectile.")]
    [SerializeField] private bool reportGameOver = false;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        bool exitedBottom = viewportPos.y < -margin;
        bool outOfBounds = viewportPos.x < -margin
                         || viewportPos.x > 1f + margin
                         || viewportPos.y < -margin
                         || viewportPos.y > 1f + margin;

        if (outOfBounds)
        {
            //resta vida si el objeto salio por abajo asi se distingue de que el Projectile salga por arriba
            if (reportGameOver && exitedBottom && GameManager.Instance != null)
            {
                GameManager.Instance.LoseLife();
            }

            Destroy(gameObject);
        }
    }
}
