using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerStats : CharacterStats
{
    [SerializeField] Image HealthBarFill;
    [SerializeField] Image HealthBar;
    [SerializeField] ParticleSystem Fire;

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        HealthBarFill.rectTransform.localScale = new Vector3(Mathf.Max(0, Health / MaxHealth * HealthBar.rectTransform.localScale.x), 1, 0);
    }

    public override void Die()
    {
        Fire.loop = false;
        GameManager.instance.TowerAlive = false;
        Invoke("LightGoesOut", 1f);
    }

    void LightGoesOut()
    {
        Fire.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) TakeDamage(10);
    }

}
