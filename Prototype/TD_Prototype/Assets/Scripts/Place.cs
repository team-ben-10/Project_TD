using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    public GameObject cube;
    public KeyCode pause;
    public float groundHeight;

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var pos = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(pos, out hit, Mathf.Infinity);
        if (Input.GetMouseButtonDown(0) && Input.GetKey(pause))
        {
            Instantiate(cube, hit.point + new Vector3(0, cube.GetComponent<MeshFilter>().sharedMesh.bounds.size.y +groundHeight/2, 0), new Quaternion(0, 0, 0, 0));
        }
        Debug.Log("First" + pos);
        Debug.Log("Second" + hit.point.y);
        Debug.Log("Check: " + cube.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * transform.localScale.y);
        Debug.Log("Check 2: " + hit.point.y + cube.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * transform.localScale.y);

    }
}
