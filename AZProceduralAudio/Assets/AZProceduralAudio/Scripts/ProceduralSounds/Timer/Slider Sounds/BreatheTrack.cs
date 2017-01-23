using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BreatheTrack : ProceduralSliderTimerSound {
	public BreatheMode Mode = BreatheMode.In;

	[SerializeField]
	public BreatheSoundDictionary BreatheSounds = new BreatheSoundDictionary ();

	protected override AudioClip DetermineSound(){
		return BreatheSounds [Mode];
	}

	public override void Play(){
		if (Init == INITIALIZATION_STATE.UNINITIALIZED)
			return;
		// Debug.Log ("Breathing: " + Mode.ToString ());
		base.Play();
		Mode = (Mode == BreatheMode.In)? BreatheMode.Out : BreatheMode.In;
	}

	[System.Serializable]
	public enum BreatheMode{
		In,
		Out
	}

	[System.Serializable]
	public class BreatheSoundDictionary : SerializableDictionary<BreatheMode,AudioClip>{}

	[UnityEditor.CustomPropertyDrawer(typeof(BreatheSoundDictionary))]
	public class BreatheSoundDictionaryDrawer : SerializableDictionaryDrawer<BreatheMode,AudioClip> { }
}

// IDEAS: Expand this. Subclass it under a class handling Reordering of tracks and alternation / checking of alternate states. 
// 		  ADVANTAGE: More generic class; wider use case scenario range.
