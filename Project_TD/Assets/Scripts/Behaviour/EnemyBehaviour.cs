using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    protected GameObject Target;
    protected Rigidbody2D rb;
    [SerializeField] protected float walkSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackDamage;

    bool canAttack = true;

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Target = GameObject.Find("Tower");
    }
    
    protected virtual void Update()
    {
        Vector2 dir = (Target.transform.position - transform.position).normalized;
        dir.x *= walkSpeed;
        dir.y = rb.velocity.y;
        rb.velocity = dir;

        if (Vector2.Distance(Target.transform.position, transform.position) <= attackRange)
        {
            rb.velocity = Vector2.zero;
            if (canAttack)
                StartCoroutine(Attack());
        }
    }

    protected virtual IEnumerator Attack()
    {
        canAttack = false;
        yield return new WaitForSeconds(1f);
        if (Vector2.Distance(Target.transform.position, transform.position) <= attackRange)
        {
            Target.GetComponent<CharacterStats>().TakeDamage(attackDamage);
        }
        canAttack = true;
    }
}
