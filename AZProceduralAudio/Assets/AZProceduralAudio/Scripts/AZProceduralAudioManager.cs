using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class AZProceduralAudioManager : MonoBehaviour, IAudioEventHandler {	
	public static AZProceduralAudioManager Instance;

	[System.Serializable]
	public class NameIndexedProceduralSoundDictionary : SerializableDictionary<string,ProceduralSound>{}

	[UnityEditor.CustomPropertyDrawer(typeof(NameIndexedProceduralSoundDictionary))]
	public class NameIndexedProceduralSoundDictionaryDrawer : SerializableDictionaryDrawer<string,ProceduralSound> { }

	[SerializeField]
	public NameIndexedProceduralSoundDictionary Sounds = new NameIndexedProceduralSoundDictionary ();

	private bool active;
	public bool Active {
		get { return active; }
		set { 
			active = value; 
			foreach (string _key in Sounds.Keys) {
				ProceduralSound _sound = Sounds[_key];
				if(_sound != null)
					_sound.active = value;
			}
		}
	}

	public void Start(){
		if (Instance != null)
			Destroy (Instance.gameObject);
		Instance = this;

		if(Sounds == null)
			Sounds = new NameIndexedProceduralSoundDictionary ();
	}

	public void Add(string _key, ProceduralSound _newSound) {
		Sounds.Add(_key, _newSound);
	}
		
	public void Remove(string _key) {
		Sounds.Remove(_key);
	}

	public void Destroy(){
		foreach (string _key in Sounds.Keys) {
			Sounds.Remove(_key);
		}
	}
}

[CustomEditor(typeof(AZProceduralAudioManager))]
public class AZProceduralAudioManagerEditor : Editor 
{
	bool defaultGUI = false;
	public override void OnInspectorGUI() {
		defaultGUI = (GUILayout.Button ("Default Inspector")) ? !defaultGUI : defaultGUI;
		if (defaultGUI == true)
			base.OnInspectorGUI();
		
		bool active = (GUILayout.Button ("Toggle Active")) ? !((AZProceduralAudioManager)target).Active : ((AZProceduralAudioManager)target).Active;
		((AZProceduralAudioManager)target).Active = active;

		GUIStyle ActiveTextStyle = new GUIStyle(EditorStyles.label);
		ActiveTextStyle.normal.textColor = (active) ? Color.green : Color.red;
		GUILayout.Label(((active)? "ACTIVE" : "INACTIVE"),ActiveTextStyle);
	}
}