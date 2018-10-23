using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/GetLastCommand")]
public class GetLastInput : InputAction {

    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.GetComponent<TextInput>().inputField.text = controller.lastInput;
    }

}
