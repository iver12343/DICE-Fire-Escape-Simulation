using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GameObject wall;
    public GameObject preview_wall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject object_clone = Instantiate(wall, preview_wall.transform.position, preview_wall.transform.rotation);
        }
    }
}
