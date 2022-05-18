using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    [Header("Objects")]
    public GameObject sun;
    public GameObject earth;
    public GameObject moon;

    [Header("Rigidbodys")]
    public Rigidbody sunRB;
    public Rigidbody earthRB;
    public Rigidbody moonRB;

    [Header("Lights")]
    public Light sunLight;

    [Header("Materials")]
    public Material defaultMat;

    [Header("Renderers")]
    public LineRenderer moonLineRenderer;
    public TrailRenderer moonTrailRenderer;
    public TrailRenderer moonDebugTrailRenderer;
    public TrailRenderer moonVelocityTrailRenderer;

    [Header("Data")]
    public Vector3 moonGravityVector;
    public Vector3 moonGravityDirection;

    void Awake()
    {
        instance = (instance != this) ? this : instance;    
    }

    void Update()
    {
        SunLookAtEarth();
    }

    //Always aim spotlight of sun at earth
    void SunLookAtEarth()
    {
        sunLight.transform.LookAt(earth.transform, Vector3.up);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
