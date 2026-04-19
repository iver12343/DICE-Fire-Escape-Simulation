using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Ray casepoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(casepoint, out hit, Mathf.Infinity))
        {
            transform.position = new Vector3(hit.point.x, hit.point.y + transform.localScale.y / 2, hit.point.z);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.Rotate(0, 90, 0);
        }
    }
}
