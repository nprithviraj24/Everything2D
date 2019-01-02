using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour {
	int max = 1000;
	int min = 1;
	int guess = 500;
	// Use this for initialization
	void Start () {
		Debug.Log ("Welcome to Number Wizard"); 
		Debug.Log ("Select a number between 1 and 1000 and don't Tell me what it is.");
		Debug.Log ("The highest number you can pick now is: "+ max);
		Debug.Log ("The lowest number you can pick now is: "+ min);
		Debug.Log ("Press Up arrow if it is higher than 500, and Down Arrow if it is lesser than 500. Enter if 500");
		max = max + 1;	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			Debug.Log ("Up was pressed");
			min = guess;
			guess = (max + min) / 2;
			Debug.Log (guess);
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Debug.Log ("Down was preseed");
			max = guess;
			guess = (max + min) / 2;
			Debug.Log (guess);
		} else if (Input.GetKeyDown(KeyCode.Return)){
			Debug.Log("Enter was pressed.");
		}
		
	}
}
