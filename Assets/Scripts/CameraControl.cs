using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;

    GameObject lookAt; //Object we're looking at

    #region Conditions
    public bool lookingAtObject; //Looking at an object
    public bool hasFocus; //Not focused on UI | Focused in 3D environment
    bool autoRotate; //Auto-rotate camera around object 
    #endregion

    #region Mouse Movement Variables
    float distance = 300f;
    float distanceMin = 100f;
    float distanceMax = 300f;
    float x, y = 0f;
    float mouseX, mouseY = 0f;
    float xSpeed, ySpeed = 120f;
    float yMin = -20f;
    float yMax = 80f;
    #endregion

    Vector3 lastMouse;
    float sensitivity = 5f;
    float totalRun = 500f;
    float sprintSpeed = 100f;
    float maxSpeed = 100f;
    float mainSpeed = 50f;


    void Awake()
    {
        instance = (instance != this) ? this : instance;    
    }

    void Start()
    {
        ActiveLookAt(ObjectManager.instance.earth);
        Cursor.lockState = CursorLockMode.Locked;
        hasFocus = true;

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void Update()
    {
        if (hasFocus)
        {
            if (lookingAtObject)
            {
                transform.LookAt(lookAt.transform);

                x += Input.GetAxis("Mouse X") + mouseX * xSpeed * distance * 1f;
                y -= Input.GetAxis("Mouse Y") + mouseY * ySpeed * 1f;

                y = ClampAngle(y, yMin, yMax);

                Quaternion rotation = Quaternion.Euler(y, x, 0);

                distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 25, distanceMin, distanceMax);

                Vector3 negDistance = new Vector3(0, 0, -distance);

                Vector3 position = rotation * negDistance + lookAt.transform.position;

                transform.rotation = rotation;

                transform.position = position;

                if (autoRotate)
                    transform.RotateAround(lookAt.transform.position, Vector3.up, 5 * Time.deltaTime);
            }
            //else
            //    FreeMovement();

            if (Input.GetKeyDown(KeyCode.V))
            {
                Debugger.instance.ToggleVelocityDraw();
            }
        }

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == null)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    hasFocus = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            Cursor.lockState = CursorLockMode.None;
            hasFocus = false;
        }
    }

    //Keyboard Movement
    Vector3 GetBaseInput()
    {
        Vector3 velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
            velocity += new Vector3(0, 0, 1);

        if (Input.GetKey(KeyCode.S))
            velocity += new Vector3(0, 0, -1);
        
        if (Input.GetKey(KeyCode.A))
            velocity += new Vector3(-1, 0, 0);
        
        if (Input.GetKey(KeyCode.D))
            velocity += new Vector3(1, 0, 0);
        
        if (Input.GetKey(KeyCode.Space))
            velocity += new Vector3(0, 1, 0);
        
        if (Input.GetKey(KeyCode.LeftControl))
            velocity += new Vector3(0, -1, 0);

        return velocity;
    }

    public void ActiveLookAt(GameObject go)
    {
        switch (go.tag)
        {
            case "Moon":
                lookAt = ObjectManager.instance.moon;
                break;
            case "Earth":
                lookAt = ObjectManager.instance.earth;
                break;
            case "Sun":
                lookAt = ObjectManager.instance.sun;
                break;
            default:
                lookAt = ObjectManager.instance.earth;
                break;
        }
        lookingAtObject = true;
    }

    void FreeMovement()
    {
        lastMouse = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        lastMouse = new Vector3(-lastMouse.y * sensitivity, lastMouse.x * sensitivity, 0);

        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);

        transform.eulerAngles = lastMouse;

        lastMouse = Input.mousePosition;

        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0) //Move when a key is pressed
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //totalRun += Time.deltaTime;
                totalRun += TimeManager.instance.decoupledDeltaTime;
                p = p * totalRun * sprintSpeed;
                p.x = Mathf.Clamp(p.x, -maxSpeed, maxSpeed);
                p.y = Mathf.Clamp(p.y, -maxSpeed, maxSpeed);
                p.z = Mathf.Clamp(p.z, -maxSpeed, maxSpeed);
            }
            else
            {
                totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, maxSpeed);
                p = p * mainSpeed;
            }

            //p = p * Time.deltaTime;
            p = p * TimeManager.instance.decoupledDeltaTime;
            transform.Translate(p);
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
