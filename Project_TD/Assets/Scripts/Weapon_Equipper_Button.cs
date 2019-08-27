using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon_Equipper_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Weapon weapon;
    public Perk perk;
    
    public void Equip()
    {
        if (weapon != null)
        {
            GameManager.instance.startWeapon = weapon;
            Weapon_Equipper.instance.weaponSlot.sprite = GameManager.instance.startWeapon.icon;
            Weapon_Equipper.instance.SetupPerks();
            Weapon_Display.instance.Hide();
        }
        if (perk != null)
        {
            GameManager.instance.startWeapon.perks.Add(perk);
            Weapon_Equipper.instance.perkSlots[GameManager.instance.startWeapon.perks.Count - 1].sprite = GameManager.instance.startWeapon.perks[GameManager.instance.startWeapon.perks.Count - 1].icon;
        }


        if (GameManager.instance.startWeapon != null && GameManager.instance.startWeapon.perks.Count >= 3)
        {
            GameManager.instance.weaponSelected = true;
            Weapon_Equipper.instance.gameObject.SetActive(false);
            Weapon_Display.instance.Hide();
        }
        
    }

    bool isOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (weapon != null)
            Weapon_Display.instance.Show(weapon.name, weapon.icon, weapon.description);
        if (perk != null)
            Weapon_Display.instance.Show(perk.name, perk.icon, perk.description);
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Weapon_Display.instance.Hide();
        isOver = false;
    }

    private void Update()
    {
        if(isOver)
            Weapon_Display.instance.SetPosition(Input.mousePosition);
    }
}
