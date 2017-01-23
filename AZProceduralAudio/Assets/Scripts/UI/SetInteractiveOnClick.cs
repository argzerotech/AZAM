using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(Button))]
public class SetInteractiveOnClick : MonoBehaviour
{
    [Tooltip("Components to be set as interactable on click")]
    public Selectable[] Interactable;
    [Tooltip("Components to be set as not interactable on click")]
    public Selectable[] NonInteractable;

    private Button m_button;

    private void Awake()
    {
        m_button = GetComponent<Button>();

        // add onclick listener
        m_button.onClick.AddListener(() => OnClick());
    }

    public void OnClick()
    {
        for (int i = 0; i < Interactable.Length; i++)
        {
            Interactable[i].interactable = true;
        }

        for (int i = 0; i < NonInteractable.Length; i++)
        {
            NonInteractable[i].interactable = false;
        }
    }
}
