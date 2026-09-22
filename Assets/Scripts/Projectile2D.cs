using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    [SerializeField] private float speed = 15f;

    private void Update()
    {
        //misil hacia arriba
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}