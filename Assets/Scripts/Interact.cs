using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!CameraControl.instance.lookingAtObject) CameraControl.instance.lookingAtObject = true;
            if (!CameraControl.instance.hasFocus) CameraControl.instance.hasFocus = true;
            Cursor.lockState = CursorLockMode.Locked;
            CameraControl.instance.transform.LookAt(gameObject.transform);
            CameraControl.instance.ActiveLookAt(gameObject);    
        } 
    }
}