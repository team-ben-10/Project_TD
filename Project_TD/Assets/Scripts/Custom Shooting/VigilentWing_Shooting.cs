using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VigilentWing_Shooting : Custom_Shooting
{
    public override void OnFire(float[] args, GameObject[] objects, GameObject currentGunObject, Weapon currentWeapon, LayerMask shootable, Material shootMat)
    {
        GameManager.instance.StartCoroutine(Burst(args, objects, currentGunObject, currentWeapon, shootable, shootMat));
    }

    IEnumerator Burst(float[] args, GameObject[] objects, GameObject currentGunObject, Weapon currentWeapon, LayerMask shootable, Material shootMat)
    {
        for (int i = 0; i < args[0]; i++)
        {
            base.OnFire(args, objects, currentGunObject, currentWeapon, shootable, shootMat);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
