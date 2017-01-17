using UnityEngine;

public class ProceduralTriggerSound : SimpleProceduralSound {
	// Use this for initialization
	void Start () {
		Type = PROCEDURAL_SOUND_TYPE.TRIGGER;
	}

	protected override void Play(){
		if (active) {
			base.Play();
		}
	}
}
