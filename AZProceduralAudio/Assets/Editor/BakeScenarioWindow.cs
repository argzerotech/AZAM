using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BakeScenarioWindow : EditorWindow
{
    private string m_filepath;
    public ValueContainer SliderValues;

    private Vector2 m_scrollPos;

    [MenuItem("MedGame/Bake Scenario")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<BakeScenarioWindow>();
    }

    private void OnGUI()
    {
        // Get serialized properties from this script
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        //SerializedProperty serializedFilename = so.FindProperty("Filename");
        SerializedProperty serializedSliderValues = so.FindProperty("SliderValues");

        m_scrollPos = EditorGUILayout.BeginScrollView(m_scrollPos, false, true);

        // Render gui
        //EditorGUILayout.PropertyField(serializedFilename);

        // load file button
        if (GUILayout.Button("Load File"))
        {
            // get path from load file diag
            m_filepath = EditorUtility.OpenFilePanel("Load Scenario File", "Assets/", "json");
            if (!string.IsNullOrEmpty(m_filepath))
            {
                m_filepath = m_filepath.Replace(Application.dataPath, "Assets");
                Debug.Log(m_filepath);
                TextAsset jsonAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(m_filepath);

                if (jsonAsset)
                {
                    SliderValues = JsonUtility.FromJson<ValueContainer>(jsonAsset.text);
                }
                else
                {
                    Debug.LogError("Could not find asset at: " + m_filepath);
                }
            }
        }

        // save file button
        bool save = GUILayout.Button("Save File");
        if (save)
        {
            string directory = string.Empty, 
                filename = string.Empty;
            if (!string.IsNullOrEmpty(m_filepath))
            {
                FileInfo pathinfo = new FileInfo(m_filepath);
                directory = pathinfo.DirectoryName;
                filename = pathinfo.Name;
            }

            m_filepath = EditorUtility.SaveFilePanel("Save Scenario File", directory, filename, "json");

            if (!string.IsNullOrEmpty(m_filepath))
            {
                // generate json data from array
                string jsonData = JsonUtility.ToJson(SliderValues);

                // create the asset
                File.WriteAllText(m_filepath, jsonData);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                Debug.Log("File successfully saved!");
            }

        }

        EditorGUILayout.PropertyField(serializedSliderValues, true);

        EditorGUILayout.EndScrollView();

        // Apply modified properties
        so.ApplyModifiedProperties();
    }
}
