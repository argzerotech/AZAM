using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Manages the gradient of pCO2 and pO2 between the mother and the baby
/// </summary>
public class GradientBehaviour : SystemBehaviour
{
    private SystemSlider matpCO2
    {
        get { return manager.Sliders["Mother pCO2"]; }
        set { manager.Sliders["Mother pCO2"] = value; }
    }

    private SystemSlider matpO2
    {
        get { return manager.Sliders["Mother pO2"]; }
        set { manager.Sliders["Mother pO2"] = value; }
    }

    private SystemSlider fetpCO2
    {
        get { return manager.Sliders["Baby pCO2"]; }
        set { manager.Sliders["Baby pCO2"] = value; }
    }

    private SystemSlider fetpO2
    {
        get { return manager.Sliders["Baby pO2"]; }
        set { manager.Sliders["Baby pO2"] = value; }
    }

    protected override void updateSystem()
    {
		Debug.Log ("grad"); 
        // adjust gradient between mother and child by 30% of the delta pCO2 every tick
        grad(matpCO2, fetpCO2, 0.3f, 0, 1f);

        // adjust gradient between mother and child by 30% of the delta pO2 every tick
        grad(matpO2, fetpO2, 0.3f);
    }

    /// <summary>
    /// Compares the values of two sliders and adjusts levels roughly based on gradient formed
    /// </summary>
    private void grad(SystemSlider mat, SystemSlider fet, float pctChange, float minMatDelta = 1f, float minFetDelta = 0f)
    {
        // compare values
        if(mat.Value - minMatDelta > fet.Value)
        {
            float delta = mat.Value - fet.Value;
            mat.Value -= delta * pctChange;
            fet.Value += delta * pctChange;
        }
        else if(fet.Value - minFetDelta > mat.Value)
        {
            float delta = fet.Value - mat.Value;
            fet.Value -= delta * pctChange;
            mat.Value += delta * pctChange;
        }
    }
}
