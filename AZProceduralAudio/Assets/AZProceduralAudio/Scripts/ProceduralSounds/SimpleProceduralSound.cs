﻿using System;
using UnityEngine;

public abstract class SimpleProceduralSound : ProceduralSound
{
	public AudioSource Source;	
	public float timePlaying = 0;
	// Determines currently playing sound: Unapplicable for VR and HR. Required for all simple PS subclasses.
	protected virtual AudioClip DetermineSound (){
		Debug.LogError ("DetermineSoundNotImplementedException!");
		return null;
	}

	void Start(){
		Source = Utilities.HierarchySearchForAudioSource (gameObject);
	}

	public override void Play(){
		Source.volume = GetVolume ();
		Source.clip = DetermineSound ();
		Source.time = 0;
		Source.Play();
		IsPlaying = true;
			Debug.Log ("Playing SPS");
	}
		
	public override void UpdateSound (){
		Source.volume = GetVolume ();
		if (Source.isPlaying) {
			timePlaying += Time.deltaTime;
			if (timePlaying > Source.clip.length) {
				timePlaying = 0;
				IsPlaying = false;
			}
		}
	}
}
