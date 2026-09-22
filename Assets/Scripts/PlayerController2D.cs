using UnityEngine;
using UnityEngine.InputSystem;

//controla a la Nave defensora
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float horizontalPadding = 0.5f;

    [Header("Disparo")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform launchPoint;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector2 moveInput;
    private float minX;
    private float maxX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Start()
    {
        CalculateHorizontalBounds();
    }

    private void CalculateHorizontalBounds()
    {
        Vector3 leftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 rightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f));

        minX = leftEdge.x + horizontalPadding;
        maxX = rightEdge.x - horizontalPadding;
    }

    //se llama a este metodo automaticamente cuando se activa la acción Move
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    //se llama a este metodo automaticamente cuando se activa la acción Fire
    public void OnFire(InputValue value)
    {
        if (!value.isPressed) return;
        Shoot();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            return;
        }

        //movimiento horizontal
        float newX = rb.position.x + moveInput.x * moveSpeed * Time.fixedDeltaTime;
        newX = Mathf.Clamp(newX, minX, maxX);
        rb.MovePosition(new Vector2(newX, rb.position.y));
    }

    private void Shoot()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            return;
        }

        if (projectilePrefab == null || launchPoint == null)
        {
            Debug.LogWarning("Asigna projectilePrefab y launchPoint en el Inspector.");
            return;
        }

        Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
    }
}
