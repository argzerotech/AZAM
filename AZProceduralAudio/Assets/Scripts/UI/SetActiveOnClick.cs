using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SetActiveOnClick : MonoBehaviour
{
    [Tooltip("Objects to set active when attached button is clicked")]
    public GameObject[] Active;

    [Tooltip("Objects to be set inactive when attached button is clicked")]
    public GameObject[] Inactive;

    private Button m_button;

    private void Awake()
    {
        // get attached button
        m_button = GetComponent<Button>();

        // add OnClick as a listener
        m_button.onClick.AddListener(() => OnClick());
    }

    public void OnClick()
    {
        for (int i = 0; i < Active.Length; i++)
        {
            Active[i].SetActive(true);
        }

        for (int i = 0; i < Inactive.Length; i++)
        {
            Inactive[i].SetActive(false);
        }
    }
}
