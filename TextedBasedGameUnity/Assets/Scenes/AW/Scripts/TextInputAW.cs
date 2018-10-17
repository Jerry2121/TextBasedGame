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

        char[] delimiterCharacters = { ' ' };
        string[] separatedInputWords = _userInput.Split(delimiterCharacters);

        //go through each input the controller knows of and see if they match what the user types
        for (int i = 0; i < controller.inputActions.Length; i++)
        {
            InputActionAW inputAction = controller.inputActions[i];
            if (inputAction.keyword == separatedInputWords[0])
            {
                inputAction.RespondToInput(controller, separatedInputWords);
            }
        }

        InputComplete();
    }

    void InputComplete()
    {
        controller.DisplayLoggedText();
        inputField.ActivateInputField();
        inputField.text = null;
    }

}
