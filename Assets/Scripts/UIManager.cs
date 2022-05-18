using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Sliders")]
    [SerializeField] Slider gravitySlider;
    [SerializeField] Slider earthMassSlider;
    [SerializeField] Slider moonMassSlider;

    [Header("Text Fields")]
    [SerializeField] TMP_Text gravityValueUI;
    [SerializeField] TMP_Text earthMassValueUI;
    [SerializeField] TMP_Text moonMassValueUI;

    [SerializeField] TMP_Text forceEqValueUI;
    [SerializeField] TMP_Text gravityEqValueUI;
    [SerializeField] TMP_Text mass1EqValueUI;
    [SerializeField] TMP_Text mass2EqValueUI;
    [SerializeField] TMP_Text distanceEqValueUI;
    
    public TMP_Text speedReadoutValueUI;
    public TMP_Text timeScaleValue;
    public TMP_Text frameRateValue;

    [Header("Canvas Groups")]
    [SerializeField] CanvasGroup physicsCanvas;

    int avgFrameRate;

    void Awake()
    {
        instance = (instance != this) ? this : instance;
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;
        frameRateValue.text = $"{avgFrameRate}";
    }

    void LateUpdate()
    {
        UpdateEquationUI();    
    }
    void UpdateEquationUI()
    {
        forceEqValueUI.text = $"{Orbits.instance.gravityMagnitude:0.000}";
        gravityEqValueUI.text = $"{Orbits.instance.gravitationalConstant:0.000}";
        mass1EqValueUI.text = $"{ObjectManager.instance.moonRB.mass}";
        mass2EqValueUI.text = $"{ObjectManager.instance.earthRB.mass}";
        distanceEqValueUI.text = $"{Orbits.instance.distance:0.000}";
    }

    void UpdateUI()
    {
        gravityValueUI.text = $"{gravitySlider.value}";
        earthMassValueUI.text = $"{earthMassSlider.value}";
        moonMassValueUI.text = $"{moonMassSlider.value}";
        timeScaleValue.text = $"{Time.timeScale}";
    }

    public void OpenClose(GameObject go)
    {
        switch (go.GetComponent<CanvasGroup>().interactable)
        {
            case true:
                Hide(go);
                break;
            case false:
                Show(go);
                break;
            default:
                Hide(go);
                break;
        }
    }
    void Show(GameObject go)
    {
        go.GetComponent<CanvasGroup>().alpha = 1f;
        go.GetComponent<CanvasGroup>().blocksRaycasts = true;
        go.GetComponent<CanvasGroup>().interactable = true;
    }
    void Hide(GameObject go)
    {
        go.GetComponent<CanvasGroup>().alpha = 0f;
        go.GetComponent<CanvasGroup>().blocksRaycasts = false;
        go.GetComponent<CanvasGroup>().interactable = false;
    }

    public void OnGravitySliderValueChanged()
    {
        Orbits.instance.gravitationalConstant = gravitySlider.value;
        UpdateUI();
    }
    public void OnEarthMassValueChanged()
    {
        ObjectManager.instance.earthRB.mass = earthMassSlider.value;
        UpdateUI();
    }
    public void OnMoonMassValueChanged()
    {
        ObjectManager.instance.moonRB.mass = moonMassSlider.value;
        UpdateUI();
    }
    public void IncreaseSpeedBtn()
    {
        TimeManager.instance.Increase();
    }
    public void DecreaseSpeedBtn()
    {
        TimeManager.instance.Decrease();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
