using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 40)
        {
            transform.position = new Vector3(39.95f, 0, transform.position.z);
        }
        else if (transform.position.x < -40)
        {
            transform.position = new Vector3(-39.95f, 0, transform.position.z);
        }
        else if (transform.position.z > 20)
        {
            transform.position = new Vector3(transform.position.x, 0, 19.95f);
        }
        else if (transform.position.z < -20)
        {
            transform.position = new Vector3(transform.position.x, 0, -19.95f);
        }

    }
}