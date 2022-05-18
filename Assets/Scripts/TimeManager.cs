using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float decoupledDeltaTime;
    public float timeScale;
    float timeDifference;

    void Awake()
    {
        instance = (instance != this) ? this : instance;        
    }

    void Start()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
        timeScale = 0;
        ChangeTimeScale();
    }

    void Update()
    {
        decoupledDeltaTime = (Time.timeScale + timeDifference) / 100;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 0;
            timeScale = 0;
            ChangeTimeScale();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        { 
            Time.timeScale = .05f;
            timeScale = .05f;
            ChangeTimeScale();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        { 
            Time.timeScale = .5f;
            timeScale = .5f;
            ChangeTimeScale();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Time.timeScale = 1;
            timeScale = 1;
            ChangeTimeScale();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Time.timeScale = 2;
            timeScale = 2;
            ChangeTimeScale();
        }
    }

    void ChangeTimeScale()
    {
        timeDifference = (1 - Time.timeScale);
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        UIManager.instance.timeScaleValue.text = Time.timeScale.ToString("0.0");
        UIManager.instance.speedReadoutValueUI.text = timeScale.ToString("0.0");
    }

    public void Increase()
    {
        switch (timeScale)
        {
            case 0:
                timeScale = 0.05f;
                Time.timeScale = 0.05f;
                ChangeTimeScale();
            break;
            case 0.05f:
                timeScale = .5f;
                Time.timeScale = .5f;
                ChangeTimeScale();
            break;
            case .5f:
                timeScale = 1;
                Time.timeScale = 1;
                ChangeTimeScale();
            break;
            case 1:
                timeScale = 2;
                Time.timeScale = 2;
                ChangeTimeScale();
            break;
        }
    }
    public void Decrease()
    {
        switch (timeScale)
        {
            case 2:
                timeScale = 1;
                Time.timeScale = 1;
                ChangeTimeScale();
            break;
            case 1:
                timeScale = .5f;
                Time.timeScale = .5f;
                ChangeTimeScale();
            break;
            case .5f:
                timeScale = .05f;
                Time.timeScale = .05f;
                ChangeTimeScale();
            break;
            case .05f:
                timeScale = 0;
                Time.timeScale = 0;
                ChangeTimeScale();
            break;
        }
    }
}