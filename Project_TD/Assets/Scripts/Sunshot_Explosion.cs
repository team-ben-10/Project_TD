using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.Experimental.Rendering.LWRP;

public class Sunshot_Explosion : MonoBehaviour
{
    public float value;
    Light2D light;

    private void Start()
    {
        light = transform.GetChild(0).GetComponent<Light2D>();
    }

    private void Update()
    {
        if (light.pointLightOuterRadius > 0)
        {
            light.pointLightOuterRadius -= Time.deltaTime * 10;
        }
    }


    private void OnParticleCollision(GameObject other)
    {
        var stats = other.GetComponent<CharacterStats>();
        if(stats != null && !(stats is PlayerStats))
        {
            GameManager.instance.ShowDamage(value, other.transform.position);
            if(stats.TakeDamage(value))
            {
                Audio_Manager.instance.Play("SunShot Explosion");
                //CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, .1f);
                Instantiate(gameObject, transform.position, Quaternion.identity).GetComponent<Sunshot_Explosion>().value = value;
            }
        }
    }
    
}
