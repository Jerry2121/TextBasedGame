using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingComponent : MonoBehaviour {
    public GameController Controller;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Controller.GetComponent<TextInput>().inputField.text = Controller.lastInput;
        }
	}
}
