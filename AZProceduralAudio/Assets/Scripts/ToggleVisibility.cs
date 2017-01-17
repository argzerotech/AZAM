using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ToggleVisibility : MonoBehaviour {

	public Button button; 
	private Text text; 

	void Awake()
	{
		text = GetComponent<Text> (); 
	}

	void OnEnable()
	{
		button.onClick.AddListener(ToggleEnable);//adds a listener for when you click the button

	}
	void ToggleEnable()// your listener calls this function
	{
		text.enabled = !text.enabled; 
	}
}
