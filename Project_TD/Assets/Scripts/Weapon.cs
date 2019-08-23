﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/New Weapon")]
public class Weapon : ScriptableObject
{
    public GameObject weaponOBJ;
    public float damage;
    public float fireSpeed;
    
    public string gunFireSound;
}
