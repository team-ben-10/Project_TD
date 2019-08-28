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
    public float MaxTimeOutSideLight;
    [SerializeField] Light2D light;
    float lightRange;

    private void Update()
    {
        TimeOutSideLight = Mathf.Clamp(TimeOutSideLight, 0, MaxTimeOutSideLight);
        HealthBarFill.rectTransform.localScale = new Vector3(Mathf.Max(0, Health / MaxHealth * HealthBar.rectTransform.localScale.x), 1, 0);
        light.pointLightOuterRadius = TimeOutSideLight / MaxTimeOutSideLight * lightRange;
        TimeOutSideLight -= Time.deltaTime;
        if(TimeOutSideLight <= 0)
        {
            TakeDamage(Time.deltaTime*10);
        }
    }

    protected override void Start()
    {

    }

    public void Setup()
    {
        base.Start();
        TimeOutSideLight = MaxTimeOutSideLight;
        lightRange = light.pointLightOuterRadius;
    }

    public override void Die()
    {
        GameManager.instance.PlayerDeath();
    }

}
