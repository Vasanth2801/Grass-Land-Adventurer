using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isChasing;
    [SerializeField] private Animator animator;

    [Header("Enemy Settings")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private int facingDirection = 1;

    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 2;
    [SerializeField] private LayerMask playerLayer;

    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private bool isAttacking = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(player == null)
        {
            return;
        }

        float distance = Vector2.Distance(player.position,transform.position);

        if(distance <= attackRange)
        {
            isChasing = false;
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isRunning", false);

            if(!isAttacking)
            {
                StartCoroutine(AttackCoroutine());
            }

            return;
        }

        if(isChasing == true)
        {
            if (player.position.x < transform.position.x && facingDirection == -1 || player.position.x > transform.position.x && facingDirection == 1)
            {
                Flip();
            }
        }

        if(isChasing == true)
        {
            Chase();
        }
    }

    void Chase()
    {
        animator.SetBool("isRunning", true);
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;

        while(true)
        {
            if(player == null)
            {
                break;
            }

            float distance = Vector2.Distance(player.position, transform.position);
            if(distance >  attackRange)
            {
                break;
            }

            Attack();

            yield return new WaitForSeconds(attackCooldown);

            isAttacking = false;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach(Collider2D hit in hitEnemies)
        {
           if(player == null)
            {
                continue;
            }

            PlayerHealth ph = hit.GetComponent<PlayerHealth>();
            if(ph != null)
            {
                ph.TakeDamage(15);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isChasing = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animator.SetBool("isRunning", false);
            isChasing = false;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}