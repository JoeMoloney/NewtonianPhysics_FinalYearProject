using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    void Start()
    { 
        ObjectManager.instance.earthRB.AddTorque(new Vector3(0, -.5f, 0), ForceMode.VelocityChange);
        ObjectManager.instance.moonRB.AddTorque(new Vector3(0, -0.034f, 0), ForceMode.VelocityChange);
    }
}
