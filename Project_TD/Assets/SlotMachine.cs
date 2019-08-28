using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] List<Perk> allPerks;
    [SerializeField] Image img;
    Perk lastRolled;

    public void Roll()
    {
        Super_Perk perk = new Super_Perk();
        List<Perk> ps = new List<Perk>();
        do
        {
            ps.Clear();
            ps.Add(allPerks[Random.Range(0, allPerks.Count)].Copy());
            ps[0].value *= 2;
            ps.Add(allPerks[Random.Range(0, allPerks.Count)].Copy());
            ps[1].value *= -1;
        } while (ps[0] == ps[1]);
        perk = perk.Create(ps);
        Client.instance.personalPerks.Add(perk);
        lastRolled = perk;
        img.sprite = perk.icon;
    }
}
