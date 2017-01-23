using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour
{
    public static SystemManager systemManagerInstance; // Singleton instance of SystemManager
    [Tooltip("The system's serialized state file.")]
    public TextAsset systemFile;
    public TextAsset metaDataFile;

    // Sliders
    public Dictionary<string, SystemSlider> Sliders;


    [Tooltip("Whether or not the system is currently updating.")]
    public bool IsSystemRunning = true;

    [Tooltip("Scalar value for system run speed (default: 1).")]
    public float SystemUpdateSpeed = 1f;

    void Awake()
    {
		// Full screen for mobile
		Screen.fullScreen = true;

        // Set systemManagerInstance to this class
        if (systemManagerInstance == null)
        {
            systemManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Populate values for Input Reader
        InputReader.LoadFiles(systemFile, metaDataFile);

        // Get slider values from input reader
        SliderValues[] entries = InputReader.GetValues();

        // Get meta data from input reader 
        Dictionary<string, MetaData> metaDict = InputReader.GetMetaData();

        // Get Canvas Game Object 
        GameObject canvas = GameObject.Find("Canvas");

        // Get all the UI Display objects in the scene (for searching later)
        UIDisplay[] uiDisplays = Resources.FindObjectsOfTypeAll<UIDisplay>();

        // Populate dictionary with slider values 
        Sliders = new Dictionary<string, SystemSlider>();
        for (int i = 0; i < entries.Length; i++)
        {
            // Get meta data from the dictionary. If it does not exist in the dictionary, create an empty file. 
            MetaData meta;
            if (metaDict.ContainsKey(entries[i].name))
            {
                meta = metaDict[entries[i].name];
            }
            else
            {
                meta.keyName = "";
                meta.displayName = "";
                meta.description = "";
                meta.tooltip = "";
                meta.units = "";
            }

            // Create a system slider and add it to the dictonary 
            SystemSlider slider = new SystemSlider(entries[i], meta);
            Sliders.Add(entries[i].name, slider);

            // Attempt to find all UI Displays for this slider
            // (collection will be empty if no object is found)
            List<UIDisplay> displays = (from obj in uiDisplays
                                        where obj.name == slider.KeyName
                                        select obj).ToList();

            // add displays to slider
            if (displays != null)
                slider.AddUIDisplays(displays);
        }

    }

    /// <summary>
    /// Starts the system if it was not running
    /// </summary>
    public void StartSystem()
    {
        if (IsSystemRunning)
        {
            Debug.LogWarning("An attempt to start the system was made, but the system is already running.");
            return;
        }

        IsSystemRunning = true;
    }

    /// <summary>
    /// Starts the system with a specific update speed
    /// </summary>
    /// <param name="speed">The update speed of the system</param>
    public void StartSystem(float speed)
    {
        if (IsSystemRunning)
        {
            Debug.LogWarning("An attempt to start the system was made, but the system is already running.");
            return;
        }

        SystemUpdateSpeed = speed;
        StartSystem();
    }

    /// <summary>
    /// Stops the system simulation
    /// </summary>
    public void StopSystem()
    {
        if (!IsSystemRunning)
        {
            Debug.LogWarning("An attempt to stop the system was made, but the system is already stopped.");
            return;
        }

        IsSystemRunning = false;
    }

}
