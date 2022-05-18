using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public static Debugger instance;

    public delegate void Debugging();
    public static event Debugging debuggingDelegate;

    bool velocityDrawEnabled;

    void Awake()
    {
        instance = (instance != this) ? this : instance;
    }

    #region Velocity Draw
    public void ToggleVelocityDraw()
    {
        if (velocityDrawEnabled)
            UnsubVelocityDraw();
        else if (!velocityDrawEnabled)
            SubVelocityDraw();
    }
    void SubVelocityDraw()
    {
        debuggingDelegate += VelocityTrail;
        velocityDrawEnabled = true;
    }
    void UnsubVelocityDraw()
    {
        debuggingDelegate -= VelocityTrail;
        velocityDrawEnabled = false;
    }
    #endregion

    public void PrintDetails(GameObject go)
    {
        Rigidbody rb = go.GetComponent<Rigidbody>();
        Debug.Log($"Debugger: " +
            $"\nGameObject: {go.name}, Position: {go.transform.position}" +
            $"\nRigidbody: {rb.name}, Position: {rb.position}, Mass: {rb.mass}" +
            $"\nVelocity: {rb.velocity}");
    }

    void FixedUpdate()
    {
        if (debuggingDelegate != null)
            debuggingDelegate();
    }

    public void DrawTrajectory()
    {
        ObjectManager.instance.moonLineRenderer.SetPosition(0, ObjectManager.instance.moon.transform.position);
        ObjectManager.instance.moonLineRenderer.SetPosition(1, ObjectManager.instance.moonRB.transform.position + ObjectManager.instance.moonRB.velocity);
    }

    public void TrajectoryTrail()
    {
        ObjectManager.instance.moonDebugTrailRenderer.transform.position = ObjectManager.instance.moon.transform.position + (ObjectManager.instance.moonGravityVector / 3.5f);
    }

    public void VelocityTrail()
    {
         ObjectManager.instance.moonVelocityTrailRenderer.transform.position = ObjectManager.instance.moon.transform.position + ObjectManager.instance.moonRB.velocity;
    }
}
