using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBehaviourScript : SystemBehaviour {

	// Attributes 
	private int week = 0; 
	private int dayOfTheWeek = 1; 
	private SystemSlider timeSlider; 

	private UnityEngine.UI.Button stopButton; 
	private UnityEngine.UI.Button playButton; 
	private UnityEngine.UI.Button fastForwardButton1; 
	private UnityEngine.UI.Button fastForwardButton2; 

	public float playButtonSpeed = 1.0f;
	public float fastForwardButton1Speed = 0.5f; 
	public float fastForwardButton2Speed = 0.0f; 

	// Use this for initialization
	public override void Start(){
		base.Start();
		timeSlider = manager.Sliders ["Simulation Time"]; 
		//Debug.Log ("Week: " + timeSlider.Value + " Day: " + day); 

		// Get references to buttons 
		stopButton = transform.GetChild(0).GetComponent<UnityEngine.UI.Button> (); 
		playButton = transform.GetChild(1).GetComponent<UnityEngine.UI.Button> (); 
		fastForwardButton1 = transform.GetChild(2).GetComponent<UnityEngine.UI.Button> (); 
		fastForwardButton2 = transform.GetChild(3).GetComponent<UnityEngine.UI.Button> (); 

		// Adds listener functionality to each button 
		stopButton.onClick.AddListener(delegate
			{
				StopButtonListener(); 
			});
		
		playButton.onClick.AddListener(delegate
			{ 
				PlayButtonListener(); 
			});

		fastForwardButton1.onClick.AddListener(delegate
			{
				FastForward1Listener(); 
			});

		fastForwardButton2.onClick.AddListener(delegate
			{ 
				FastForward2Listener(); 
			});
	}
	
	// Update is called once per tick
	protected override void updateSystem()
	{
		if (timeSlider.Value >= timeSlider.Max) {
			Debug.Log ("End of simulation"); 
		}

		// Increment day 
		timeSlider.Value += 1.0f; 
		dayOfTheWeek++; 
		week = (int)(timeSlider.Value / 7.0f);
		 
		if ((int)timeSlider.Value % 7 == 0) {
			dayOfTheWeek = 0; 
		}

		//Debug.Log ("Week: " + week + " Day of the week: " + dayOfTheWeek); 
	}

	private void StopButtonListener()
	{
		manager.IsSystemRunning = false;
	}

	private void PlayButtonListener()
	{
		manager.IsSystemRunning = true; 
		manager.SystemUpdateSpeed = playButtonSpeed;
	}

	private void FastForward1Listener()
	{
		manager.IsSystemRunning = true; 
		manager.SystemUpdateSpeed = fastForwardButton1Speed; 
	}

	private void FastForward2Listener()
	{
		manager.IsSystemRunning = true; 
		manager.SystemUpdateSpeed = fastForwardButton2Speed;
	}
}
