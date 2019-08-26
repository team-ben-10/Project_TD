using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [System.Serializable]
    public class KeyPair
    {
        public string name;
        public KeyCode key;

        public KeyPair(string name, KeyCode key)
        {
            this.name = name;
            this.key = key;
        }
    }

    [System.Serializable]
    public class Axis
    {
        public string name;
        public KeyCode Poskey;
        public KeyCode Negkey;

        public Axis(string name, KeyCode poskey, KeyCode negkey)
        {
            this.name = name;
            Poskey = poskey;
            Negkey = negkey;
        }
    }

    [SerializeField]
    List<KeyPair> keyPairs = new List<KeyPair>();
    [SerializeField]
    List<Axis> axises = new List<Axis>();

    public void SetKey(string name, KeyCode key)
    {
        keyPairs.Find(x => x.name == name).key = key;
    }

    public void SetAxis(string name, KeyCode Poskey, KeyCode Negkey)
    {
        axises.Find(x => x.name == name).Poskey = Poskey;
        axises.Find(x => x.name == name).Negkey = Negkey;
    }

    public bool GetButtonDown(string name)
    {
        return Input.GetKeyDown(keyPairs.Find(x => x.name == name).key);
    }

    public bool GetButtonUp(string name)
    {
        return Input.GetKeyUp(keyPairs.Find(x => x.name == name).key);
    }

    public bool GetButton(string name)
    {
        return Input.GetKey(keyPairs.Find(x => x.name == name).key);
    }

    public int GetAxisRaw(string name)
    {
        if (Input.GetKey(axises.Find(x => x.name == name).Poskey))
        {
            return 1;
        }
        if (Input.GetKey(axises.Find(x => x.name == name).Negkey))
        {
            return -1;
        }
        return 0;
    }

    public bool GetMouseButton(int number)
    {
        return Input.GetMouseButton(number);
    }

    public bool GetMouseButtonDown(int number)
    {
        return Input.GetMouseButtonDown(number);
    }

    public bool GetMouseButtonUp(int number)
    {
        return Input.GetMouseButtonUp(number);
    }


    public static InputManager instance;

    private void Start()
    {
        DontDestroyOnLoad(this);
        
        
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
    }
}
