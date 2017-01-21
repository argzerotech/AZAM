#define DEBUG
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSliderTimerSound : SimpleProceduralSound, Timed {
	
	public float DEBUG_OVERRIDE_SPEED = 1.0f;

	// Slider Object
	public SystemSlider SliderInstance;
	public float MinimumOffset = 1f;
	public float Multiplier = 2f;
	private float delay;
	private float elapsedTime;

	#if DEBUG  
	void Start(){
		Init = INITIALIZATION_STATE.INITIALIZED;
	}
	#endif  
	public float Delay{
		get{ return delay; }
		set{ delay = value; }
	}

	public float ElapsedTime{
		get{ return elapsedTime; }
	}

	public bool Repeating{
		get{ return true; }
	}

	protected float DetermineWaitTime(){
		if (Init != INITIALIZATION_STATE.INITIALIZED) {
			if (SliderInstance != null)
				Init = INITIALIZATION_STATE.INITIALIZED;
			Debug.LogError("ERROR!: SliderSound's Slider must be initialized!");
		}
		// NORMAL WITH SLIDERS
		//Delay = Utilities.MapFloat (SliderInstance.Value, SliderInstance.Min, SliderInstance.Max, 0f, 1f) * Multiplier + MinimumOffset;

		// DEBUG WITHOUT SLIDER
		Delay = DEBUG_OVERRIDE_SPEED * Multiplier + MinimumOffset;

		if (Delay <= 0)
			Delay = 1000.0f;
		return Delay;
	}

	public override void UpdateSound(){
		elapsedTime += Time.deltaTime;
		if (Init == INITIALIZATION_STATE.INITIALIZED && Active) {
			if (elapsedTime > DetermineWaitTime ()) {
				Play ();
				elapsedTime = 0.0f;
			}
		}
		base.UpdateSound ();
	}

	public void SetSlider(SystemSlider sliderInstance){
		Init = INITIALIZATION_STATE.INITIALIZED;
		SliderInstance = sliderInstance;
	}
}
