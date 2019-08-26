using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] GameObject Shop;
    private void Update()
    {
        if (GameManager.instance.Running)
        {
            if (InputManager.instance.GetButtonDown("Shop"))
            {
                Shop.gameObject.SetActive(!Shop.gameObject.activeSelf);
                GameManager.instance.Player.GetComponent<PlayerController>().enabled = !Shop.gameObject.activeSelf;
                GameManager.instance.Player.GetComponent<WeaponManager>().enabled = !Shop.gameObject.activeSelf;
                GameManager.instance.Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
}
