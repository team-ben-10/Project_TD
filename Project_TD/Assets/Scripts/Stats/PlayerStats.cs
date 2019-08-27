using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    [SerializeField] Image HealthBarFill;
    [SerializeField] Image HealthBar;
    [HideInInspector] public float TimeOutSideLight;
    [SerializeField] float MaxTimeOutSideLight;
    [SerializeField] Light2D light;

    private void Update()
    {
        TimeOutSideLight = Mathf.Clamp(TimeOutSideLight, 0, MaxTimeOutSideLight);
        HealthBarFill.rectTransform.localScale = new Vector3(Mathf.Max(0, Health / MaxHealth * HealthBar.rectTransform.localScale.x), 1, 0);
        light.pointLightOuterRadius = TimeOutSideLight / MaxTimeOutSideLight * 6;
        TimeOutSideLight -= Time.deltaTime;
        if(TimeOutSideLight <= 0)
        {
            TakeDamage(Time.deltaTime*10);
        }
    }

    protected override void Start()
    {
        base.Start();
        TimeOutSideLight = MaxTimeOutSideLight;
    }

    public override void Die()
    {
        GameManager.instance.PlayerDeath();
    }

}
