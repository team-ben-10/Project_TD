using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehaviour : EnemyBehaviour
{
    [SerializeField] Vector2 attentionRanges;

    float attentionRange;
    [SerializeField] States state = States.Walking;

    [SerializeField] float shotSpeed;
    [SerializeField] GameObject weapon;
    [SerializeField] LayerMask shootable;
    [SerializeField] GameObject shootParticle = null;
    [SerializeField] Material shootMat;
     bool isLookingLeft = false;

    enum States
    {
        Attacking, Walking
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attentionRanges.x);
        Gizmos.DrawWireSphere(transform.position, attentionRanges.y);
    }

    protected override void Update()
    {
        if (state == States.Walking)
        {
            attentionRange = attentionRanges.y;
            weapon.SetActive(false);
            base.Update();
        }
        if(state == States.Attacking)
        {
            weapon.SetActive(true);
            attentionRange = attentionRanges.y;
            if (GameManager.instance.Player != null)
            {
                Vector2 dir = (GameManager.instance.Player.transform.position - transform.position).normalized;

                Quaternion rotation = Quaternion.Euler(isLookingLeft ? 0 : 180, 0, (Mathf.Atan2(dir.y, dir.x) + Mathf.PI) * Mathf.Rad2Deg * (isLookingLeft ? 1 : -1));
                weapon.transform.rotation = rotation;

                if (!isLookingLeft)
                {
                    if (dir.x > 0)
                    {
                        Flip();
                    }
                }
                else
                {
                    if (dir.x < 0)
                    {
                        Flip();
                    }
                }

                //TODO: Shooting
                if (canShoot)
                    StartCoroutine(Shooting());

                dir.y = rb.velocity.y;
                dir.x = dir.x * -1 * walkSpeed;
                rb.velocity = dir;
            }
        }

        if (GameManager.instance.Player != null)
        {
            if (Vector2.Distance(GameManager.instance.Player.transform.position, transform.position) <= attentionRange)
            {
                state = States.Attacking;
            }
            else
            {
                state = States.Walking;
            }
        }
        else
        {
            state = States.Walking;
        }
    }

    bool canShoot = true;

    IEnumerator Shooting()
    {
        canShoot = false;
        GameObject firePoint = weapon.transform.GetChild(0).gameObject;
        Vector2 dir = -firePoint.transform.right;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, dir, shootable);
        LineRenderer lr = new GameObject("line", typeof(LineRenderer)).GetComponent<LineRenderer>();
        GameObject SP = Instantiate(shootParticle, firePoint.transform.position, firePoint.transform.rotation);
        SP.transform.eulerAngles += shootParticle.transform.eulerAngles;
        Destroy(SP, 1f);
        lr.endWidth = 0.005f;
        lr.startWidth = 0.005f;
        lr.sortingOrder = 1;
        lr.SetPosition(0, firePoint.transform.position);
        if (hit)
        {
            lr.SetPosition(1, hit.point);
            CharacterStats stats = hit.collider.GetComponent<CharacterStats>();
            if (stats != null)
            {
                stats.TakeDamage(attackDamage);
            }
        }
        else
        {
            lr.SetPosition(1, dir * 100);
        }
        lr.material = shootMat;
        Destroy(lr.gameObject, 0.1f);
        yield return new WaitForSeconds(shotSpeed);
        canShoot = true;
    }

    void Flip()
    {
        isLookingLeft = !isLookingLeft;
        transform.eulerAngles = new Vector3(0, isLookingLeft ? 0 : 180, 0);
    }

    protected override void Start()
    {
        base.Start();
    }
}
