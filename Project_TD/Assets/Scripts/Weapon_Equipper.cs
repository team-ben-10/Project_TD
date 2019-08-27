using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Equipper : MonoBehaviour
{
    [SerializeField] GameObject Content;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] List<Weapon> allWeapons = new List<Weapon>();
    [SerializeField] List<Perk> allPerks = new List<Perk>();
    public Image weaponSlot;
    public Image[] perkSlots;

    public static Weapon_Equipper instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        foreach (var item in allWeapons)
        {
            var button = Instantiate(buttonPrefab, Content.transform);
            button.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
            button.GetComponent<Weapon_Equipper_Button>().weapon = item.Copy();
        }
    }

    public void SetupPerks()
    {
        text.text = "Select your Perks";
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            Destroy(Content.transform.GetChild(i).gameObject);
        }
        foreach (var item in allPerks)
        {
            var button = Instantiate(buttonPrefab, Content.transform);
            button.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
            button.transform.GetChild(0).eulerAngles = new Vector3(0, 0, 0);
            button.GetComponent<Weapon_Equipper_Button>().perk = item.Copy();
        }
    }
    
    void Update()
    {
        
    }
}
