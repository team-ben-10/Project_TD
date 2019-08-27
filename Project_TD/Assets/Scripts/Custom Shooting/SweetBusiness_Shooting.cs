using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetBusiness_Shooting : Custom_Shooting
{
    public override void OnFire(float[] args, GameObject[] objects, GameObject currentGunObject, Weapon currentWeapon, LayerMask shootable, Material shootMat)
    {
        base.OnFire(args, objects, currentGunObject, currentWeapon, shootable, shootMat);
        if(currentWeapon.fireSpeed > args[1])
        {
            currentWeapon.fireSpeed -= args[2];
        }
    }

    public override void OnFireReleased(float[] args, GameObject[] objects, GameObject currentGunObject, Weapon currentWeapon, LayerMask shootable, Material shootMat)
    {
        currentWeapon.fireSpeed = args[0];
    }
}
