using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpValueOverTime : SystemBehaviour
{
    [Tooltip("The key name of the affected slider")]
    public string SliderKey;

    private SystemSlider slider
    {
        get { return manager.Sliders[SliderKey]; }
        set { manager.Sliders[SliderKey] = value; }
    }

    public DayValuePair[] LerpTimes;
    private int m_currentInd;

    protected override void updateSystem()
    {
        if(LerpTimes != null 
            && LerpTimes.Length > 0
            && m_currentInd + 1 < LerpTimes.Length)
        {
            // increment current index if last lerp is finished
            if(LerpTimes[m_currentInd+1].Day == timeSlider.Value)
            {
                m_currentInd++;
            }

            if(m_currentInd < LerpTimes.Length)
            {
                // lerp from current to next
                slider.Value = LerpValueByTime(LerpTimes[m_currentInd].Day,
                    LerpTimes[m_currentInd + 1].Day,
                    LerpTimes[m_currentInd].Value,
                    LerpTimes[m_currentInd + 1].Value);
            }
        }
    }
}

[Serializable]
public struct DayValuePair
{
    public int Day;
    public float Value;
}