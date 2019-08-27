using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_Shooting : MonoBehaviour
{
    public virtual void OnFire(float[] args, GameObject[] objects, GameObject currentGunObject, Weapon currentWeapon, LayerMask shootable, Material shootMat)
    {
        GameObject firePoint = currentGunObject.transform.GetChild(0).gameObject;
        Vector2 dir = -firePoint.transform.right;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, dir, shootable);
        LineRenderer lr = new GameObject("line", typeof(LineRenderer)).GetComponent<LineRenderer>();
        GameObject shootParticle = Instantiate(currentWeapon.shootParticle, firePoint.transform.position, firePoint.transform.rotation);
        shootParticle.transform.eulerAngles += currentWeapon.shootParticle.transform.eulerAngles;
        Destroy(shootParticle, 1f);
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
                GameManager.instance.ShowDamage((int)currentWeapon.damage, hit.point);
                stats.TakeDamage(currentWeapon.damage);
                foreach (var perk in currentWeapon.perks)
                {
                    perk.OnHit(currentWeapon, stats, GameManager.instance.Player.GetComponent<PlayerStats>());
                }
            }
        }
        else
        {
            lr.SetPosition(1, dir * 100);
        }
        lr.material = shootMat;
        Destroy(lr.gameObject, 0.1f);
    }

    public virtual void OnFireReleased(float[] args, GameObject[] objects, GameObject currentGunObject, Weapon currentWeapon, LayerMask shootable, Material shootMat)
    {

    }
}
