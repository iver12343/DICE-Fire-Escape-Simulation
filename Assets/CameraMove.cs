using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float rotate_speed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, rotate_speed, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, -rotate_speed, 0);
        }
    }
}
