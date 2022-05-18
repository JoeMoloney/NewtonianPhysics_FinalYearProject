using UnityEngine;

public class Orbits : MonoBehaviour
{
    public static Orbits instance;

    //Mass Kg: 1989000000000000000000000000000
    static readonly float sunMassKg = Mathf.Pow(1.989f, 30);
    static readonly float sunDist = 0f;

    //Mass Kg: 5972000000000000000000000
    static readonly float earthMassKg = Mathf.Pow(5.972f, 24);
    static readonly float earthToSunDistKM = 147170000f;

    //Mass Kg: 73480000000000000000000
    static readonly float moonMassKg = Mathf.Pow(7.348f, 22);
    static readonly float moonToEarthDistKM = 384400f;

    //6.67408 × 10-11 m3 kg-1 s-2
    //Force of attraction between particals of 1Kg @ 1M apart
    const float G = 6.6740f;

    //public float gravitationalConstant = 9.81f;
    public float gravitationalConstant = 6.67408f;
    //0.0000000000667408f;
    public float gravityMagnitude;
    public float distance;

    void Awake()
    {
        instance = (instance != this) ? this : instance;
    }

    //=====Rough Work=====//
    //Sun Mass in Kg = 1.989e+30
    //Log base 1.989 of 30

    //Moon Mass in Kg = 7.348e+22
    //Log base 7.348 of 22

    //Math.Log(base, value);

    //Sun : Math.Log(1.989, 22)
    //Moon : Math.Log(7.348, 22)

    //Gravity : 9.81m^2
    //Gravity : Math.Log(9.81, 22)
    //====================//


    //==========Notes==========//
    //
    //Force Towards Sun = Gravity(Sun Mass * Object Mass) / Distance Squared
    //
    //Force = Mass * Acceleration
    //
    //=========================//
    void Start()
    {
        //Debugger.instance.PrintDetails(ObjectManager.instance.moon);
        //Debugger.instance.PrintDetails(ObjectManager.instance.earth);
        //Debugger.instance.PrintDetails(ObjectManager.instance.sun);

        SetupTrailFX(ObjectManager.instance.moon);

        float moonEarthDist = ((ObjectManager.instance.moon.transform.position.x * -1) - (ObjectManager.instance.earth.transform.position.x * -1)) / 1.5f;
        ObjectManager.instance.moonRB.AddForce(new Vector3(moonEarthDist, 0, moonEarthDist + 125), ForceMode.Impulse);
        //float moonEarthDist = ((ObjectManager.instance.moon.transform.position.x * -1) - (ObjectManager.instance.earth.transform.position.x * -1)) / 1.5f;
        //ObjectManager.instance.moonRB.AddRelativeForce(new Vector3(moonEarthDist + 750, 0, moonEarthDist + 1250), ForceMode.Impulse);

        //float earthSunDist = ((ObjectManager.instance.earth.transform.position.x * -1) - (ObjectManager.instance.sun.transform.position.x * -1)) * 20;
        //ObjectManager.instance.earthRB.AddForce(new Vector3(earthSunDist, 0, earthSunDist), ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        MoonEarthOrbit();
        //EarthSunOrbit();
    }

    void MoonEarthOrbit()
    {
        //Global
        ObjectManager.instance.moonRB.AddForce(UG_Type2(ObjectManager.instance.moonRB, ObjectManager.instance.earthRB), ForceMode.Force);

        //Local
        //ObjectManager.instance.moonRB.AddForce(UGLocal(ObjectManager.instance.moonRB, ObjectManager.instance.earthRB), ForceMode.Force);
    }
    void EarthSunOrbit()
    {
        //Global
        ObjectManager.instance.earthRB.AddForce(UG_Type1(ObjectManager.instance.earthRB, ObjectManager.instance.sunRB), ForceMode.Force);

        //Local
        //ObjectManager.instance.earthRB.AddRelativeForce(UGLocal(ObjectManager.instance.earthRB, ObjectManager.instance.sunRB), ForceMode.Force);
    }

    #region Newtons Law of Universal Gravitation
    //Global
    Vector3 UG_Type1(Rigidbody movingObject, Rigidbody staticObject)
    {
        //Type 1:
        //Direction between objects
        Vector3 direction = staticObject.position - movingObject.position;
        
        //Distance
        float distance = direction.magnitude;

        //Gravity Strength
        float forceMagnitude = G * (staticObject.mass * movingObject.mass) / Mathf.Pow(distance, 2);

        //Gravity Direction
        Vector3 force = direction.normalized * forceMagnitude;
        return force;
    }

    //Local
    Vector3 UG_Type2(Rigidbody movingObject, Rigidbody staticObject)
    {
        //Type 2:
        //Difference between 2 objects positions
        Vector3 difference = staticObject.transform.position - movingObject.transform.position;

        //Length of Distance
        distance = difference.magnitude;

        //Direction of travel normalised to a magnitude of 1
        Vector3 gravityDirection = difference.normalized;

        //Newtonian Physics Equation
        gravityMagnitude = gravitationalConstant * ((staticObject.mass * movingObject.mass) / distance * distance);

        //Calculate direction of travel Vector
        Vector3 gravityVector = gravityDirection * gravityMagnitude;

        //Apply coordinates to show direction of travel
        ObjectManager.instance.moonGravityVector = gravityVector;
        ObjectManager.instance.moonGravityDirection = gravityDirection;

        return gravityVector;
    }
    #endregion


    public void SetupTrailFX(GameObject obj)
    {
        TrailRenderer tr = obj.GetComponentInChildren<TrailRenderer>();

        //tr.material = ObjectManager.instance.defaultMat;

        //tr.time = 4f;
        tr.time = 20;
        //tr.startWidth = obj.GetComponent<SphereCollider>().radius * 2;
        tr.startWidth = obj.GetComponent<SphereCollider>().radius / 5;
        tr.endWidth = tr.startWidth;
    }
}