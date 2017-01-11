using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BreatheTrack : ProceduralSound {
	[System.Serializable]
	public enum BreatheMode{
		In,
		Out
	}
		
	public BreatheMode Mode = BreatheMode.In;

	[System.Serializable]
	public class BreatheSoundDictionary : SerializableDictionary<BreatheMode,AudioClip>{}

	[UnityEditor.CustomPropertyDrawer(typeof(BreatheSoundDictionary))]
	public class BreatheSoundDictionaryDrawer : SerializableDictionaryDrawer<BreatheMode,AudioClip> { }

	[SerializeField]
	public BreatheSoundDictionary BreatheSounds = new BreatheSoundDictionary ();

	// Use this for initialization
	void Start () {
		Type = PROCEDURAL_SOUND_TYPE.TIMER;
	}

	protected override AudioClip DetermineSound(){
		return BreatheSounds [Mode];
	}

	protected override void PlayOnce(){
		base.PlayOnce ();

		Mode = (Mode == BreatheMode.In)? BreatheMode.Out : BreatheMode.In;
	}
}
