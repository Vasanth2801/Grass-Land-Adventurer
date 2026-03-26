using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private float bulletForce = 10f;

    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private ObjectPooler pooler;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = pooler.SpawnFromPools("Bullet",firePoint.position,firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletForce,ForceMode2D.Impulse);
    }
}