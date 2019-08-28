using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    TMPro.TextMeshProUGUI waitingForPlayersText;
    [SerializeField]
    GameObject BeforeGameReadyObject;
    

    [Header("Prefabs")]
    [SerializeField]
    GameObject PlayerPrefab;
    [SerializeField] GameObject playerSpawnEffect;
    [SerializeField] GameObject DamageIndecator;

    [Header("Scene Objects")]
    [SerializeField]
    Transform PlayerSpawn;
    [SerializeField]
    TMPro.TextMeshProUGUI EnemyDataText;
    [SerializeField]
    TMPro.TextMeshProUGUI timePastText;

    [HideInInspector] public GameObject Player { get; private set; }
    [HideInInspector]public bool Running { get; private set; }
    public bool TowerAlive { get; set; } = true;
    [HideInInspector] public int EnemyData { get; set; } = 0;
    [HideInInspector] public float TimePast;
    [HideInInspector] public Weapon startWeapon { get; set; }
    [HideInInspector] public bool weaponSelected { get; set; }
    

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    public void ShowDamage(float value, Vector2 pos)
    {
        Instantiate(DamageIndecator, pos, Quaternion.identity).GetComponent<Damage_Display>().value = value;
    }

    

    IEnumerator PlayerDeathCoroutine()
    {
        Destroy(Player);
        if (TowerAlive)
        {
            Camera.main.transform.position = PlayerSpawn.transform.position;
            Destroy(Instantiate(playerSpawnEffect, PlayerSpawn.transform.position, Quaternion.identity), 3);
            yield return new WaitForSeconds(2);
            Player = Instantiate(PlayerPrefab, PlayerSpawn.position, Quaternion.identity);
            Player.GetComponent<WeaponManager>().currentWeapon = startWeapon;
            Player.GetComponent<WeaponManager>().SetupWeapon();
        }
        else
        {
            Debug.Log("Lost!");
            Client.instance.Disconnect();
            SceneManager.LoadScene(0);
        }
    }

    public void PlayerDeath()
    {
        StartCoroutine(PlayerDeathCoroutine());
    }

    public void BeginGame()
    {
        StartCoroutine(BeginGameCoroutine());
    }

    IEnumerator BeginGameCoroutine()
    {
        for (int i = 10; i > 0; i--)
        {
            waitingForPlayersText.text = i + "";
            yield return new WaitForSeconds(1);
        }
        waitingForPlayersText.text = "GO!";
        yield return new WaitForSeconds(1);
        Running = true;
        BeforeGameReadyObject.SetActive(false);
        while (!weaponSelected) { yield return null; }
        Player = Instantiate(PlayerPrefab, PlayerSpawn.position, Quaternion.identity);
        Player.GetComponent<WeaponManager>().currentWeapon = startWeapon;
        Player.GetComponent<WeaponManager>().SetupWeapon();
        Player.GetComponent<PlayerStats>().Setup();
    }

    float alpha = 1;
    int multi = 1;

    void Update()
    {
        if (!Running)
        {
            waitingForPlayersText.color = new Color(255,255,255,alpha);
            alpha -= Time.deltaTime * multi;
            if(alpha <= 0 || alpha >=1)
            {
                multi *= -1;
            }
        }
        EnemyDataText.text = "Enemy Data: " + EnemyData;

        
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if(!Running)
                BeginGame();
        }
        if (Running)
        {
            TimePast += Time.deltaTime;
            timePastText.text = "Time past: " + (int)(TimePast / 60) + ":" + (int)(TimePast % 60);
        }
    }

    public void SendBlocker(int i)
    {
        if (EnemyData >= 5 * i)
        {
            Client.instance.Send("$SendBlocker " + i);
            EnemyData -= 5 * i;
        }
    }

    private void Start()
    {
        Audio_Manager.instance.Play("Main");
        if(Player == null)
        {
            Player = GameObject.Find("Player");
        }
    }
}
