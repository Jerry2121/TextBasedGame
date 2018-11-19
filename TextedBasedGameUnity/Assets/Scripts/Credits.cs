using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Credits")]
public class Credits : InputAction {

    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.LogStringWithReturn(" - Jeremiah Templeton");
        controller.LogStringWithReturn(" - Nathan Weis");
        controller.LogStringWithReturn(" - Aaron Wiens");
    }

}
