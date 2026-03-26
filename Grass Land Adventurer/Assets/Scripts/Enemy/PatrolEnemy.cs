using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 1f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        speed = -speed;
        FlipEnemy();
    }

    void FlipEnemy()
    {
        transform.localScale = new Vector2(-Mathf.Sign(rb.linearVelocity.x), 1f);
    }
}