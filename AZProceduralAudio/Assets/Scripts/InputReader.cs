using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputReader {

	// Attributes --- Public 

	// Attributes --- Private 
	static private ValueContainer jsonValueContainer; 
	static private MetaDataContainer jsonMetaDataContainer;  
	static private SliderValues[] values; 
	static private Dictionary<string, MetaData> metaData; 

	// Loads the textFile for slider values. Attempts to load the dataFile to populate metadata. 
	public static void LoadFiles(TextAsset textFile, TextAsset dataFile)
	{
		if (textFile != null) {
			// Parse slider values JSON into container 
			jsonValueContainer = JsonUtility.FromJson<ValueContainer> (textFile.text); 
			values = jsonValueContainer.values; 
		} else {
			Debug.Log ("Invalid Text File passed to Input Reader."); 
		}
		if (dataFile == null) {
			return; 
		} else {
			LoadMetaData (dataFile); 
		}
	}

	public static void LoadMetaData(TextAsset dataFile)
	{
		// Parse JSON into metadata container
		jsonMetaDataContainer = JsonUtility.FromJson<MetaDataContainer> (dataFile.text); 
		MetaData[] metaDataArr = jsonMetaDataContainer.data; 

		// Populate dictionary to organize metadata
		metaData = new Dictionary<string, MetaData>();
		for (int i = 0; i < metaDataArr.Length; i++)
		{
			metaData.Add(metaDataArr[i].keyName, metaDataArr[i]);
		}
	}


	// Getters
	static public SliderValues[] GetValues()
	{
		return values; 
	}

	static public Dictionary<string, MetaData> GetMetaData()
	{
		return metaData; 
	}
}
