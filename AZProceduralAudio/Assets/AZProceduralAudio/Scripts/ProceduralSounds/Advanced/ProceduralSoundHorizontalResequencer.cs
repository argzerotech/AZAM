using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows for Resequencing of Individual Tracks
// Trigger / Timer hybrid: On current Sound Finished
public class ProceduralSoundHorizontalResequencer : ProceduralSound {
	[SerializeField]
	public SoundProbabilityDictionary Clips = new SoundProbabilityDictionary ();
	public ProceduralSound Current;

	void Start(){
		Source = null;
	}

	protected override AudioClip DetermineSound(){
		return Current.DetermineSound();
	}

	protected override void Play(){
		return;
	}
		
	protected void Stop(){
		if (Source.isPlaying)
			Source.Pause ();
	}

	[System.Serializable]
	public class SoundProbabilityDictionary : SerializableDictionary<string,SoundProbabilityAssociation>{}

	[UnityEditor.CustomPropertyDrawer(typeof(SoundProbabilityDictionary))]
	public class SoundProbabilityDictionaryDrawer : SerializableDictionaryDrawer<string,SoundProbabilityAssociation> { }
}

[System.Serializable]
public struct SoundProbabilityAssociation{
	// Audio File associated with the probability
	public ProceduralSound Clip;

	// 0 to 1: 1 is maximum probability for Clip
	public float Probability;
}