using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float speed;
    public KeyCode pause;
    public float mouseSensitivity;
    public float clampAngle;
    public float scrollSpeed;


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
            gameObject.GetComponent<Rigidbody>().velocity += transform.right*speed*Time.deltaTime;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity += -transform.right * speed * Time.deltaTime;
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity += (transform.forward+transform.up)*speed*Time.deltaTime;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity += (-transform.forward + -transform.up) * speed * Time.deltaTime;
        }
        if (Input.mouseScrollDelta.y !=0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y+Input.mouseScrollDelta.y * -scrollSpeed * Time.deltaTime, transform.position.z);
        }
        Debug.Log("--------------------------------"+Input.mouseScrollDelta);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle / 2, clampAngle);

        if (!Input.GetKey(pause))
        {
            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
            Cursor.lockState = CursorLockMode.Locked;
            
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Cursor.lockState = CursorLockMode.None;
        }




    }
}
