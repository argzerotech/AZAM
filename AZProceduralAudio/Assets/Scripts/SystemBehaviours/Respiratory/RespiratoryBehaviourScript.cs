using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simulates ventilation in respiratory system
/// </summary>
public class RespiratoryBehaviourScript : SystemBehaviour
{
	private int eventCounter;  
	public float tvEventStartDay; 
	public float tvEventEndDay;
	public float tvEventStartValue;
	public float tvEventEndValue; 
	public float pCO2RateOfChange = 0.3f; 


	private GradientBehaviour gradient; 

	// Getters and Setters for sliders of this system -------------

	// Inputs -----------------
	// Respiratory Rate 
	#region Public Accessors
	private SystemSlider rr
	{
		get{ return manager.Sliders ["Mother RR"]; }
		set { manager.Sliders ["Mother RR"] = value; }
	}
	// Tidal Volume 
	private SystemSlider tv 
	{
		get{ return manager.Sliders ["Mother TV"]; }
		set { manager.Sliders ["Mother TV"] = value; }
	}
	// Expiratory Reserve Volume 
	private SystemSlider erv 
	{
		get{ return manager.Sliders ["Mother ERV"]; }
		set { manager.Sliders ["Mother ERV"] = value; }
	}
	// Residual Volume 
	private SystemSlider rv 
	{
		get{ return manager.Sliders ["Mother RV"]; }
		set { manager.Sliders ["Mother RV"] = value; }
	}

	// Outputs 
	// Ventilation 
	private SystemSlider vm 
	{
		get{ return manager.Sliders ["Mother Vm"]; }
		set { manager.Sliders ["Mother Vm"] = value; }
	}
	// Aerial blood gas pH 
	private SystemSlider pH 
	{
		get{ return manager.Sliders ["Mother pH"]; }
		set { manager.Sliders ["Mother pH"] = value; }
	}
	// Partial pressure of CO2 
	private SystemSlider pCO2 
	{
		get{ return manager.Sliders ["Mother pCO2"]; }
		set { manager.Sliders ["Mother pCO2"] = value; }
	}
	// Partial pressure of O2 
	private SystemSlider pO2 
	{
		get{ return manager.Sliders ["Mother pO2"]; }
		set { manager.Sliders ["Mother pO2"] = value; }
	}
	#endregion

    // Use this for initialization
	public override void Start()
    {
        base.Start();
		gradient = GetComponent<GradientBehaviour> (); 
		//Debug.Log (tv.Value); 
    } 

    // Update is called once per frame
    protected override void updateSystem()
	{
        // Events that occur in this system --------------
        // Event Description: In the given range, TV will approach the end value for TV. Formula should be an increase by 45% 
        if (timeSlider.Value >= tvEventStartDay && timeSlider.Value < tvEventEndDay)
        {
			tv.Value = LerpValueByTime (tvEventStartDay, tvEventEndDay, tvEventStartValue, tvEventEndValue, 2.0f); 
        }



        // RR, TV, ERV and RV influences Va
        // RR == + ==> Vm

        // Vm
        vm.Value = (rr.Value * tv.Value) / 1000;

        //        vm.Value = rr.NormalizedValue * 0.20f;
        //
        //        // TV == + ==> Vm
        //        vm.Value += tv.NormalizedValue * 0.20f;
        //
        //        // ERV == - ==> Vm
        //        vm.Value +=  erv.InvNormalizedValue * 0.30f;
        //
        //        // RV == - ==> Vm
        //        vm.Value += rv.InvNormalizedValue * 0.30f;

        // Vm influences pH, pCO2 and pO2

        // Vm == - ==> pCO2
		pCO2.Value = LerpPercent(pCO2.Value, pCO2.Min + vm.InvNormalizedValue * (pCO2.Max - pCO2.Min), pCO2RateOfChange); 

        // Vm == ++ ==> pO2
		pO2.Value = pO2.Min + pCO2.InvNormalizedValue * (pO2.Max - pO2.Min);

		// Vm == + ==> pH
		pH.Value = pH.Min + pCO2.InvNormalizedValue * (pH.Max - pH.Min);
    }
}
