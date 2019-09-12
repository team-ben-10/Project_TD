using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float speed;

    public float mouseSensitivity;
    public float clampAngle;


    private float rotY;
    private float rotX;


    // Start is called before the first frame update
    void Start()
    {
        //transform.position = game.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxis("Horizontal"));
        if (Input.GetAxis("Horizontal") > 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity += new Vector3(speed, 0, 0);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity += new Vector3(-speed, 0, 0);
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0, 0, speed);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0, 0, -speed);
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;

        
    }
}
