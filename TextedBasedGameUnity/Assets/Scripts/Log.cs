using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Log")]
public class Log : InputAction {

    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.LogToTextFile(separatedInputWords);
    }

}
