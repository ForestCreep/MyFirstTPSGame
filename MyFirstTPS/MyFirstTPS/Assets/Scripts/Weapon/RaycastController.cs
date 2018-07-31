using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        // 锁定光标
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Camera.main)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction);
        
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
        }
        //Debug.DrawRay(Camera.main.transform.position,Screen.);
    }
}
