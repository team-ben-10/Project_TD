using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon currentWeapon;

    [SerializeField]
    GameObject gunPoint;

    GameObject currentGunObject = null;

    bool canFire = true;
    bool isLookingLeft = true;

    [SerializeField]
    LayerMask shootable;

    [SerializeField]
    Material shootMat;

    [SerializeField]
    GameObject Torch;

    private void Start()
    {
        SetupWeapon();
    }

    public void SetupWeapon()
    {
        if (currentGunObject != null)
            Destroy(currentGunObject);
        foreach (var item in GetComponents<Custom_Shooting>())
        {
            Destroy(item);
        }

        currentWeapon = currentWeapon.Copy();
        currentWeapon.perks.ForEach(x => x.OnEquip(currentWeapon, GetComponent<PlayerStats>()));
        currentGunObject = Instantiate(currentWeapon.weaponOBJ, gunPoint.transform.position, gunPoint.transform.rotation, gunPoint.transform);
        if (currentWeapon.customShootingName != "")
        {
            gameObject.AddComponent(Type.GetType(currentWeapon.customShootingName));
        }
    }

    private void Update()
    {
        //Basic Camera Follow
        Camera.main.transform.position = transform.position + new Vector3(0, 0, -10);
        //EZCameraShake.CameraShaker.Instance.RestPositionOffset = transform.position + new Vector3(0, 0, -10);

        /*if (Input.GetKeyDown(KeyCode.G))
        {
            Vector2 dirTorch = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            var torch = Instantiate(Torch, transform.position, Quaternion.identity);
            torch.GetComponent<Rigidbody2D>().AddForce(dirTorch * 15f, ForceMode2D.Impulse);
        }*/

        if (Input.GetMouseButton(0) && canFire)
        {
            StartCoroutine(Shoot());
        }
        if (!Input.GetMouseButton(0))
        {
            GetComponent<Custom_Shooting>()?.OnFireReleased(currentWeapon.customShootingParams, currentWeapon.customShootingObjects, currentGunObject, currentWeapon, shootable, shootMat);
        }

        Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - currentGunObject.transform.position).normalized;
        Quaternion rotation = Quaternion.Euler(isLookingLeft ? 0 : 180, 0, (Mathf.Atan2(dir.y, dir.x) + Mathf.PI) * Mathf.Rad2Deg * (isLookingLeft ? 1 : -1));
        currentGunObject.transform.rotation = rotation;

        Vector2 dirToPlayer = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        if (isLookingLeft)
        {
            if (dirToPlayer.x > 0)
            {
                Flip();
            }
        }
        else
        {
            if (dirToPlayer.x < 0)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        isLookingLeft = !isLookingLeft;
        transform.eulerAngles = new Vector3(0, isLookingLeft ? 0 : 180, 0);
    }

    IEnumerator Shoot()
    {
        canFire = false;
        //TODO: Play Sound, Particle.

        if(currentWeapon.gunFireSound != "")
            Audio_Manager.instance.Play(currentWeapon.gunFireSound);
        var cs = GetComponent<Custom_Shooting>();
        cs?.OnFire(currentWeapon.customShootingParams, currentWeapon.customShootingObjects, currentGunObject,currentWeapon, shootable, shootMat);
        if (cs == null)
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
                        perk.OnHit(currentWeapon, stats, GetComponent<PlayerStats>());
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
        yield return new WaitForSeconds(currentWeapon.fireSpeed);
        canFire = true;
    }
}
