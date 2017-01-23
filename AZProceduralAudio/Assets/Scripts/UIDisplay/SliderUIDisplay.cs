using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUIDisplay : UIDisplay
{
    #region Member Level Attributes

    /// <summary>
    /// UI slider game object and local references to its child objects  
    /// </summary>
    private Slider m_UISlider;
    #endregion

    #region Public Accessors
    // Slider
    public Slider UISlider
    {
        get { return m_UISlider; }
        set { m_UISlider = value; }
    }
    #endregion

    public RectTransform DisplayNameText, 
        ToolTipText, 
        ValueText, 
        UnitsText;

    public RectTransform SliderHandle;

    /// <summary>
    /// Initializes the UI Slider when the display's SystemSlider is set
    /// </summary>
    protected override void initSliderUI()
    {
        m_UISlider = GetComponent<Slider>();

        // Asserts prefab is assembled correctly 
        //m_backgroundImage = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        //m_fillArea = transform.GetChild(1).GetComponent<UnityEngine.RectTransform>();
        //m_fillImage = transform.GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>();
        //m_handleArea = transform.GetChild(2).GetComponent<UnityEngine.RectTransform>();
        //m_handleImage = transform.GetChild(2).GetChild(0).GetComponent<UnityEngine.UI.Image>();
        //m_infoButton = transform.GetChild(3).GetComponent<UnityEngine.UI.Button>();
        //m_infoButtonText = m_infoButton.GetComponentInChildren<UnityEngine.UI.Text>();
        //m_displayNameText = transform.GetChild(4).GetComponent<UnityEngine.UI.Text>();
        //m_toolTipText = transform.GetChild(5).GetComponent<UnityEngine.UI.Text>();
        //m_valueText = transform.GetChild(6).GetComponent<UnityEngine.UI.Text>();
        //m_unitsText = transform.GetChild(7).GetComponent<UnityEngine.UI.Text>();

        // Update initial UI display based on slider values 
        DisplayNameText.GetComponent<Text>().text = SystemSlider.MetaData.displayName;
        ToolTipText.GetComponent<Text>().text = SystemSlider.MetaData.tooltip;
        ValueText.GetComponent<Text>().text = SystemSlider.Value.ToString();
        UnitsText.GetComponent<Text>().text = SystemSlider.MetaData.units;

        SetVisible(SystemSlider.IsDisplayed);
        SetInteractive(SystemSlider.IsInteractive);

        // Set minimum and maximum values first and then current value 
        UISlider.minValue = SystemSlider.Min;
        UISlider.maxValue = SystemSlider.Max;
        UISlider.value = SystemSlider.Value;

        // Slider listener that will update UI as values change 
        m_UISlider.onValueChanged.AddListener(delegate
        {
            SliderListener();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (SystemSlider != null && SystemSlider.IsDisplayed)
        {
            // Update minimum and maximum values and then current value 
            UISlider.minValue = SystemSlider.Min;
            UISlider.maxValue = SystemSlider.Max;
            UISlider.value = SystemSlider.Value;

            
        }
    }

    /// <summary>
    /// Called when this slider's values are changed and needs to update UI 
    /// </summary>
    public void SliderListener()
    {
        SystemSlider.Value = UISlider.value;
        ValueText.GetComponent<Text>().text = UISlider.value.ToString();
    }

    /// <summary>
    /// Sets the UI Slider's interactive state
    /// </summary>
    /// <param name="interactive"></param>
    public override void SetInteractive(bool interactive)
    {
        // Set UI slider interactive state
        if (UISlider)
        {
            // Disable interaction 
            UISlider.interactable = interactive;
            // Disable Handle 
            SliderHandle.GetComponent<Image>().enabled = interactive;
        }
    }

    /// <summary>
    /// Sets the UI Slider's visible state
    /// </summary>
    /// <param name="visible"></param>
    public override void SetVisible(bool visible)
    {
        if (UISlider)
        {
            UISlider.enabled = visible;
        }
    }
}
