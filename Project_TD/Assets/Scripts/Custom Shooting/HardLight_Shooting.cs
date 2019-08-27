using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardLight_Shooting : Custom_Shooting
{
    public override void OnFire(float[] args, GameObject[] objects, GameObject currentGunObject, Weapon currentWeapon, LayerMask shootable, Material shootMat)
    {
        GameObject gunPoint = currentGunObject.transform.GetChild(0).gameObject;
        Vector2 firePoint = gunPoint.transform.position;
        Vector2 dir = -gunPoint.transform.right;
        bool smoking = true;
        for (int i = 0; i < args[0]+1; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint + dir*0.1f, dir, shootable);
            LineRenderer lr = new GameObject("line", typeof(LineRenderer)).GetComponent<LineRenderer>();
            if (smoking)
            {
                GameObject shootParticle = Instantiate(currentWeapon.shootParticle, firePoint, gunPoint.transform.rotation);
                shootParticle.transform.eulerAngles += currentWeapon.shootParticle.transform.eulerAngles;
                Destroy(shootParticle, 1f);
                smoking = false;
            }
            lr.endWidth = 0.005f;
            lr.startWidth = 0.005f;
            lr.sortingOrder = 1;
            lr.SetPosition(0, firePoint);
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
                dir = Vector2.Reflect(dir, hit.normal).normalized;
                firePoint = hit.point;
            }
            else
            {
                lr.SetPosition(1, dir * 100);
                lr.material = shootMat;
                Destroy(lr.gameObject, 0.1f);
                break;
            }
            lr.material = shootMat;
            Destroy(lr.gameObject, 0.1f);
        }
        
    }
}
