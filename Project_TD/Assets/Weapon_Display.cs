using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Display : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI title;
    [SerializeField] TMPro.TextMeshProUGUI desc;
    [SerializeField] Image image;
    [SerializeField] GameObject content;

    public static Weapon_Display instance;

    private void Awake()
    {
        instance = this;
        Hide();
    }

    public void Show(string title, Sprite image, string desc)
    {
        this.title.text = title;
        this.desc.text = desc;
        this.image.sprite = image;
        if(!content.gameObject.activeSelf)
            content.gameObject.SetActive(true);
    }

    public void SetPosition(Vector2 pos)
    {
        content.transform.position = pos;
    }

    public void Hide()
    {
        if(content.gameObject.activeSelf)
            content.gameObject.SetActive(false);
    }


}
