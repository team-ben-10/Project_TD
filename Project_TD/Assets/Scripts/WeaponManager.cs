﻿using System.Collections;
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

    private void Start()
    {
        SetupWeapon();
    }

    void SetupWeapon()
    {
        currentGunObject = Instantiate(currentWeapon.weaponOBJ, gunPoint.transform.position, gunPoint.transform.rotation, gunPoint.transform);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && canFire)
        {
            StartCoroutine(Shoot());
        }
        
        Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - currentGunObject.transform.position).normalized;
        Quaternion rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(dir.y, dir.x)+Mathf.PI) * Mathf.Rad2Deg);
        currentGunObject.transform.rotation = rotation;


        if (isLookingLeft)
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
    }

    void Flip()
    {
        isLookingLeft = !isLookingLeft;
        transform.eulerAngles = new Vector3(0, isLookingLeft ? 0 : 180, 0);
        currentGunObject.GetComponent<SpriteRenderer>().flipY = !isLookingLeft;
    }

    IEnumerator Shoot()
    {
        canFire = false;
        //TODO: Play Sound, Particle.
        GameObject firePoint = currentGunObject.transform.GetChild(0).gameObject;
        Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, dir,shootable);
        LineRenderer lr = new GameObject("line", typeof(LineRenderer)).GetComponent<LineRenderer>();
        lr.endWidth = 0.005f;
        lr.startWidth = 0.005f;
        lr.SetPosition(0, firePoint.transform.position);
        if (hit)
        {
            lr.SetPosition(1, hit.point);
        }
        else
        {
            lr.SetPosition(1, dir*100);
        }
        Destroy(lr.gameObject, 0.1f);
        yield return new WaitForSeconds(currentWeapon.fireSpeed);
        canFire = true;
    }
}
