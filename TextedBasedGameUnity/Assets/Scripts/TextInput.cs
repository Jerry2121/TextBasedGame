using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public InputField inputField;

    GameController controller;

    void Awake()
    {
        controller = GetComponent<GameController>();
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    void Update()
    {
        if (inputField.IsActive() == false){
            inputField.ActivateInputField();
            inputField.text = null;
        }
    }

    void AcceptStringInput(string userInput)
    {
        if (inputField.text == string.Empty)
        {
            inputField.ActivateInputField();
            return;
        }

        userInput = userInput.ToLower();
        controller.LogStringWithReturn(userInput);

        char[] delimiterCharacters = { ' ' };
        string[] separatedInputWords = userInput.Split(delimiterCharacters);
        bool matchingInputAction = false;

        for (int i = 0; i < controller.inputActions.Length; i++)
        {
            InputAction inputAction = controller.inputActions[i];
            if (inputAction.keyWord.ToLower() == separatedInputWords[0])
            {
                matchingInputAction = true;
                inputAction.RepsondToInput(controller, separatedInputWords);
            }
        }
        if(matchingInputAction == false)
        {
            controller.LogStringWithReturn(separatedInputWords[0] + " is not a valid command");
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
