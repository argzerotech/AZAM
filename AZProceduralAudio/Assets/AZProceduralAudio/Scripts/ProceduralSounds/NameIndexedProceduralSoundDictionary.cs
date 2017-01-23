using UnityEngine;

[System.Serializable]
public class NameIndexedProceduralSoundDictionary : SerializableDictionary<string,ProceduralSound>{}

[UnityEditor.CustomPropertyDrawer(typeof(NameIndexedProceduralSoundDictionary))]
public class NameIndexedProceduralSoundDictionaryDrawer : SerializableDictionaryDrawer<string,ProceduralSound> { }

