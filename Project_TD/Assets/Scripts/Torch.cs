using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Torch : MonoBehaviour
{
    Light2D light;
    public float timeTillFlameOut = 3f;

    private void Start()
    {
        light = GetComponent<Light2D>();
    }

    private void Update()
    {
        light.intensity -= Time.deltaTime / timeTillFlameOut;
        if(light.intensity <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
