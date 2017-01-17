using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BakeMetadataWindow : EditorWindow
{
    //public string Filename;
    public MetaDataContainer Metadata;

    private Vector2 m_scrollPos;
    private string m_filepath;

    [MenuItem("MedGame/Bake Metadata")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<BakeMetadataWindow>();
    }

    private void OnGUI()
    {
        // Get serialized properties from this script
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        //SerializedProperty serializedFilename = so.FindProperty("Filename");
        SerializedProperty serializedMetadata = so.FindProperty("Metadata");

        m_scrollPos = EditorGUILayout.BeginScrollView(m_scrollPos, false, true);

        // Render gui
        //EditorGUILayout.PropertyField(serializedFilename);

        // load file button
        if(GUILayout.Button("Load File"))
        {
            m_filepath = EditorUtility.OpenFilePanel("Load Metadata File", "Assets/", "json");
            if (!string.IsNullOrEmpty(m_filepath))
            {
                m_filepath = m_filepath.Replace(Application.dataPath, "Assets");
                Debug.Log(m_filepath);
                TextAsset jsonAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(m_filepath);

                if (jsonAsset)
                {
                    Metadata = JsonUtility.FromJson<MetaDataContainer>(jsonAsset.text);
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
                string jsonData = JsonUtility.ToJson(Metadata);

                // create the asset
                File.WriteAllText(m_filepath, jsonData);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                Debug.Log("File successfully saved!");
            }
        }

        EditorGUILayout.PropertyField(serializedMetadata, true);

        EditorGUILayout.EndScrollView();

        // Apply modified properties
        so.ApplyModifiedProperties();
    }
}
