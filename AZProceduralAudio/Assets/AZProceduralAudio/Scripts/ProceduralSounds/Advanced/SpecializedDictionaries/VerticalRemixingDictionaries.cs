using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializeableStateVolumeDictionary : SerializableDictionary<string, GameObject> {

}

[System.Serializable]
public class SerializableProceduralSoundVolumeDictionary : SerializableDictionary<ProceduralSound,float>{}