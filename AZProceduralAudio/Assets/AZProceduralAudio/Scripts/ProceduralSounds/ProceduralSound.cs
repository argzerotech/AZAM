using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ProceduralSound : MonoBehaviour {
	public float volume; // 0 <-- x --> 1
	public bool active = false;
	protected float elapsedTime;
	protected PROCEDURAL_SOUND_TYPE Type;
	protected INITIALIZATION_STATE Init = INITIALIZATION_STATE.UNINITIALIZED;

	// Use this for initialization
	void Start () {
		elapsedTime = 0.0f;
		Init = INITIALIZATION_STATE.UNINITIALIZED;
		Source = Utilities.HierarchySearchForAudioSource(gameObject);
		// Playing with the idea of automatic detection of new sounds in the Scene Audio Ecosystem
		//		if (AZProceduralAudioManager.Instance.Sounds[id] == null)
		//			AZProceduralAudioManager.Instance.Sounds.Add(id, this);
	}

	// Determines currently playing sound: Unapplicable for VR and HR. Required for all simple PS subclasses.
	protected abstract AudioClip DetermineSound ();

	protected virtual void Play(){
		Source.PlayOneShot (DetermineSound());
	}

	protected enum PROCEDURAL_SOUND_TYPE{
		TIMER,
		TRIGGER
	}

	protected enum INITIALIZATION_STATE{
		INITIALIZED,
		UNINITIALIZED
	}
}