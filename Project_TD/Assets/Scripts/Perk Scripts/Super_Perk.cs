using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Super Perk", menuName = "Perks/New Super Perk")]
public class Super_Perk : Perk
{
    public List<Perk> perks = new List<Perk>();
    List<Perk> copyPerks = new List<Perk>();

    public override void OnEquip(Weapon weapon, PlayerStats player)
    {
        perks.ForEach(x => copyPerks.Add(x.Copy()));
        copyPerks.ForEach(x => {
            x.OnEquip(weapon, player);
        });
    }



    public Super_Perk Create(List<Perk> perks)
    {
        Super_Perk perk = new Super_Perk();
        List<Perk> ps = new List<Perk>();
        perks.ForEach(x => ps.Add(x.Copy()));
        perk.perks = ps;
        return (Super_Perk)perk.Copy();
    }

    public override void OnHit(Weapon weapon, CharacterStats enemy, PlayerStats player)
    {
        copyPerks.ForEach(x => x.OnHit(weapon, enemy,player));
    }

    public override void OnKill(Weapon weapon, CharacterStats enemy, PlayerStats player)
    {
        copyPerks.ForEach(x => x.OnKill(weapon, enemy,player));
    }

    public override Perk Copy()
    {
        var perk = base.Copy();
        List<Perk> p = new List<Perk>();
        foreach (var item in perks)
        {
            p.Add(item.Copy());
        }
        (perk as Super_Perk).perks = p;
        foreach (var item in perks)
        {
            perk.description += item.description + "\n";
        }
        Texture2D texture = new Texture2D(90, 90);
        for (int x = 0; x < 44; x++)
        {
            for (int y = 0; y < 90; y++)
            {
                texture.SetPixel(x, y, perks[0].icon.texture.GetPixel((int)perks[0].icon.textureRect.x + x, (int)perks[0].icon.textureRect.y + y));
            }
        }
        for (int y = 0; y < 90; y++)
        {
            texture.SetPixel(44, y, Color.black);
        }
        for (int y = 0; y < 90; y++)
        {
            texture.SetPixel(45, y, Color.black);
        }
        for (int x = 46; x < 90; x++) 
        {
            for (int y = 0; y < 90; y++)
            {
                Rect textureRect= perks[1].icon.textureRect;
                texture.SetPixel(x, y, perks[1].icon.texture.GetPixel((int)textureRect.x +  x, (int)textureRect.y + y));
            }
        }
        texture.Apply();
        Vector2 offset = perks[1].icon.textureRectOffset;
        perk.icon = Sprite.Create(texture, new Rect(0,0, 90, 90), new Vector2(offset.x/2, offset.y/2));
        perk.name = perks[0].name.Split(' ')[0] + " " + perks[1].name.Split(' ')[1];
        return perk;
    }
}
