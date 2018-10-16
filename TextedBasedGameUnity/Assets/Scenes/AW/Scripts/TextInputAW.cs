using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputAW : MonoBehaviour {

    public InputField inputField;

    private GameControllerAW controller;

    private void Awake()
    {
        controller = GetComponent<GameControllerAW>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    void AcceptStringInput(string _userInput)
    {
        _userInput = _userInput.ToLower();
        controller.LogStringWithReturn(_userInput);
        InputComplete();
    }

    void InputComplete()
    {
        controller.DisplayLoggedText();
        inputField.ActivateInputField();
        inputField.text = null;
    }

}
