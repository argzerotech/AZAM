using System;
using UnityEngine;

public class SimpleProceduralSound : ProceduralSound
{
	public AudioSource Source;	
	void Start(){
		Source = Utilities.HierarchySearchForAudioSource (gameObject);
	}
}

