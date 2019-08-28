using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Display : MonoBehaviour
{
    public float value;
    [SerializeField] TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }
    
    void Update()
    {
        text.text = value + "";
        transform.position += Vector3.up*0.015f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime);
    }
}
