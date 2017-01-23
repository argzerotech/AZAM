using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SystemSlider
{
    #region Member Level Attributes
    /// <summary>
    /// Slider values from scenario file
    /// </summary>
    private SliderValues m_values;

    /// <summary>
    /// Metadata information from metadata file
    /// </summary>
    private MetaData m_metaData;

    /// <summary>
    /// Script managing display of this slider
    /// </summary>
    private List<UIDisplay> m_uiDisplays;
    #endregion

    #region Public Accessors
    public string KeyName
    {
        get { return m_values.name; }
    }

    public float Value
    {
        get { return m_values.value; }
        set { m_values.value = value; }
    }

    public float Min
    {
        get { return m_values.min; }
        set { m_values.min = value; }
    }

    public float MinWarning
    {
        get { return m_values.minWarning; }
        set { m_values.minWarning = value; }
    }

    public float MinCritical
    {
        get { return m_values.minCritical; }
        set { m_values.minCritical = value; }
    }

    public float MinFailure
    {
        get { return m_values.minFailure; }
        set { m_values.minFailure = value; }
    }

    public float Max
    {
        get { return m_values.max; }
        set { m_values.max = value; }
    }

    public float MaxWarning
    {
        get { return m_values.maxWarning; }
        set { m_values.maxWarning = value; }
    }

    public float MaxCritical
    {
        get { return m_values.maxCritical; }
        set { m_values.maxCritical = value; }
    }

    public float MaxFailure
    {
        get { return m_values.maxFailure; }
        set { m_values.maxFailure = value; }
    }

    public float NormalizedValue
    {
        get { return (Value - Min) / (Max - Min); }
    }

    public float InvNormalizedValue
    {
        get { return 1 - NormalizedValue; }
    }

    public bool IsDisplayed
    {
        get { return m_values.isDisplayed; }
        set { m_values.isDisplayed = value; }
    }

    public bool IsInteractive
    {
        get { return m_values.isInteractive; }
        set { m_values.isInteractive = value; }
    }
    // Metadata
    public MetaData MetaData
    {
        get { return m_metaData; }
    }
    
    public List<UIDisplay> UIDisplays
    {
        get { return m_uiDisplays; }
        set { m_uiDisplays = value; }
    }
    #endregion

    /// <summary>
    /// Initializes a new slider with specified values and metadata
    /// </summary>
    /// <param name="values">Slider values from scenario file</param>
    /// <param name="metaData">Metadata information for the slider</param>
	public SystemSlider(SliderValues values, MetaData metaData)
    {
        // Initialize member variables
        m_values = values;
        m_metaData = metaData;
    }

    
    public void SetVisible(bool visible)
    {
        IsDisplayed = visible;

        // Update visible state in UI
        if (UIDisplays != null)
        {
            UIDisplays.ForEach(d => d.SetVisible(visible));
        }
    }

    public void SetInteractive(bool interactive)
    {
        IsInteractive = interactive;

        // Update interactive state in UI
        if(UIDisplays != null)
        {
            UIDisplays.ForEach(d => d.SetInteractive(interactive));
        }
    }

    /// <summary>
    /// Sets the slider's UI display, and registers the slider to the display
    /// </summary>
    /// <param name="disp"></param>
    public void AddUIDisplay(UIDisplay disp)
    {
        if (UIDisplays == null)
            UIDisplays = new List<UIDisplay>();

        UIDisplays.Add(disp);

        // register self to display
        disp.SystemSlider = this;
    }

    public void AddUIDisplays(List<UIDisplay> disp)
    {
        if(UIDisplays == null)
        {
            UIDisplays = disp;
        }
        else
        {
            UIDisplays.AddRange(disp);
        }

        // register self to each display
        disp.ForEach(d => d.SystemSlider = this);
    }
}
