using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public struct ValueContainer
{
	public SliderValues[] values; 
}

[Serializable]
public struct SliderValues
{
	public string name; 
	public float value;
	public float min; 
	public float minWarning; 
	public float minCritical;
    public float minFailure;
	public float max; 
	public float maxWarning; 
	public float maxCritical;
    public float maxFailure;

    public bool isDisplayed;
    public bool isInteractive;
}

[Serializable]
public struct MetaDataContainer
{
	public MetaData[] data; 
}

[Serializable] 
public struct MetaData
{
	public string keyName; 
	public string displayName; 
	public string tooltip; 
	public string description; 
	public string units; 
}