using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProceduralSliderTimerSound : ProceduralTimerSound {
	// Slider Object
	public SystemSlider SliderInstance;
	public float Offset = 1f;
	public float Multiplier = 2f;

	//void Start(){
    //	Assign Slider!
	//}

	protected override float DetermineWaitTime(){
		if (Init != INITIALIZATION_STATE.INITIALIZED)
			return -1f;
		TimeToNext = Utilities.MapFloat (SliderInstance.Value, SliderInstance.Min, SliderInstance.Max, 0f, 1f) * Multiplier + Offset;
		return TimeToNext;
	}

	public void SetSlider(SystemSlider sliderInstance){
		Init = INITIALIZATION_STATE.INITIALIZED;
	}
}
