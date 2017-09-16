using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSoundGenerator : MonoBehaviour {
	public GameObject TriggerSoundPrefab;
	public string DefaultName;

	public void GenerateSound(string _name){
		GameObject soundObj = GameObject.Instantiate (TriggerSoundPrefab);
		AZProceduralAudioManager.Instance.Add (_name, TriggerSoundPrefab.GetComponent<ProceduralTriggerSound> ());
	}

	public void GenerateSound(){
		GenerateSound (DefaultName);
	}
}
