using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public float jumpHeight = 5f;

    [SerializeField]
    GameObject groundCheck;
    [SerializeField]
    LayerMask Walkable;

    Rigidbody2D rb;
    bool onGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        float movement = InputManager.instance.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movement * speed,Mathf.Clamp(rb.velocity.y,-12,25));
        if (InputManager.instance.GetButtonDown("Jump") && onGround)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            onGround = false;
        }
        if(groundCheck != null)
        {
            if (Physics2D.OverlapCircle(groundCheck.transform.position, 0.1f,Walkable) != null)
            {
                onGround = true;
            }
            else
            {
                onGround = false;
            }
        }
    }
}
