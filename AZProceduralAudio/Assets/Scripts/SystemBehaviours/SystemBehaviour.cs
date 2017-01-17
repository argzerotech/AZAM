using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for all scripts that modify and/or
/// recieve values in the simulated system
/// </summary>
public abstract class SystemBehaviour : MonoBehaviour
{
	private float elapsedTime; 

    // Protected references to system manager and simulation related values
    protected SystemManager manager
    {
        get { return SystemManager.systemManagerInstance; }
        set { SystemManager.systemManagerInstance = value; }
    }

	protected SystemSlider timeSlider {
		get { return manager.Sliders ["Simulation Time"]; }
		set { manager.Sliders ["Simulation Time"] = value; }
	}

    protected float updateSpeed
    {
        get { return manager.SystemUpdateSpeed; }
    }

    protected bool isSystemRunning
    {
        get { return manager.IsSystemRunning; }
    }

	protected int currentWeek
	{
		get { return (int)(timeSlider.Value / 7.0f); }
	}

    public virtual void Start()
    {
		// Initialize 
		elapsedTime = 0.0f; 
        // check the system manager instance exists
        if (SystemManager.systemManagerInstance == null)
            Debug.LogError("Missing required SystemManager.");
    }

	public void Update()
	{
		if (isSystemRunning) {
			elapsedTime += Time.deltaTime; 
			if (elapsedTime > updateSpeed) { 
				// Update system
				updateSystem ();
				// Update values 
				elapsedTime = 0.0f; 
			} 
		} else {
			return; 
		}
	}

	protected abstract void updateSystem ();

	protected float LerpValueByTime(float startDay, float endDay, float minValue, float maxValue, float rateOfIncrease = 0.0f) 
	{
		float t = (timeSlider.Value - startDay) / (endDay); 
		if (rateOfIncrease != 0.0f) {
			t *= rateOfIncrease; 
		}
		return Mathf.Lerp (minValue, maxValue, t); 
		//Debug.Log("Target: " + target);
	}

	protected float LerpPercent(float startValue, float endValue, float rateOfChange = 0.0f) 
	{
		return Mathf.Lerp (startValue, endValue, rateOfChange); 
	}
}
