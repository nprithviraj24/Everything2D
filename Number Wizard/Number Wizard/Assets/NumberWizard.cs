using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("Welcome to Number Wizard"); 
		Debug.Log ("Select a number between 1 and 1000 and don't Tell me what it is.");

		Debug.Log ("Press Up arrow if it is higher than 500, and Down Arrow if it is lesser than 500. Enter if 500");
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Debug.Log ("Up was pressed");	
		}
		
	}
}
