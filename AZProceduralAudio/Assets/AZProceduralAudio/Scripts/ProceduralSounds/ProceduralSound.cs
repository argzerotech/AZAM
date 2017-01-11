using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ProceduralSound : MonoBehaviour {

	public bool active = false;
	protected float elapsedTime;
	protected PROCEDURAL_SOUND_TYPE Type;

	// Use this for initialization
	void Start () {
		elapsedTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			if (Type == PROCEDURAL_SOUND_TYPE.TIMER) {
				elapsedTime += Time.deltaTime;
				if (elapsedTime >= DetermineWaitTime ()) {
					PlayOnce ();
					elapsedTime = 0;
				}
			}
		}
	}

	protected virtual float DetermineWaitTime(){
		return 1f;
	}

	protected abstract AudioClip DetermineSound ();

	protected virtual void PlayOnce(){
		GetComponent<AudioSource>().PlayOneShot (DetermineSound());
	}

	protected enum PROCEDURAL_SOUND_TYPE{
		TIMER,
		TRIGGER
	}
}