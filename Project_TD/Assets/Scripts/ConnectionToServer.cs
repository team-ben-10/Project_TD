using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectionToServer : MonoBehaviour
{
    public TMPro.TMP_InputField inputField;

    public void Connect()
    {
        Client.instance.Connect(inputField.text);
    }
}
