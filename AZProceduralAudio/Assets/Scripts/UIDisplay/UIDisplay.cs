using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIDisplay : MonoBehaviour
{
    private SystemSlider m_systemSlider;
    public SystemSlider SystemSlider
    {
        get { return m_systemSlider; }
        set
        {
            m_systemSlider = value;

            if (m_systemSlider != null)
                initSliderUI();
        }
    }

    protected abstract void initSliderUI();

    public abstract void SetInteractive(bool interactive);

    public abstract void SetVisible(bool visible);
}
