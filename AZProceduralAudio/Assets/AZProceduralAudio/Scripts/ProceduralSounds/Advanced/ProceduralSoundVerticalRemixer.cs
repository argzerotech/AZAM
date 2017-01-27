using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows for Resequencing of Individual Tracks
// Trigger / Timer hybrid: On current Sound Finished
public class ProceduralSoundVerticalRemixer : ProceduralSound, FadableSound {
	// ALL NAMES MUST BE THE SAME AS THE NAME IN THE AZ AUDIO MANAGER

	[SerializeField]
	protected List<SoundVolumeAssociation> TrackVolumes = new List<SoundVolumeAssociation>();
	[SerializeField]
	protected List<VolumeStateManager> TrackVolumeStates = new List<VolumeStateManager>();

	//[SerializeField]
	//public SerializeableNameIndexedSoundToVolumeDictionary VolumeStates;

	public bool Fadable {
		get;
		set;
	}

	void Start(){
		Type = PROCEDURAL_SOUND_TYPE.ADVANCED;
		Volume = 1.0f;
		foreach (SoundVolumeAssociation association in TrackVolumes) {
			association.Sound.Parent = this;
		}
	}

	public override void UpdateSound(){
		if (IsPlaying) {
			foreach (SoundVolumeAssociation association in TrackVolumes) {
				if (Fadable) {
					float fadeSpeed = (association.Sound.Volume - (association.Volume)>0)? FadeInSpeed
									: (association.Sound.Volume - (association.Volume)<0)? FadeOutSpeed 
								    : 0f;
					association.Sound.Volume = Mathf.Lerp(association.Sound.Volume, association.Volume, fadeSpeed);
				} else {
					association.Sound.Volume = association.Volume;
				}
			}
		}
	}

	public override void Play (){
		IsPlaying = true;
		foreach (SoundVolumeAssociation association in TrackVolumes) {
			association.Sound.Volume = association.Volume;
			association.Sound.Play ();
			ProceduralSound sound = association.Sound;
			Debug.Log ("VRSound Playing: " + sound.name);
		}
	}

	public override void Stop (){
		IsPlaying = false;
	}

	public void Destroy(){
		while(TrackVolumes.Count>0) {
			TrackVolumes.RemoveAt(0);
		}
		TrackVolumes = null;
	}
}