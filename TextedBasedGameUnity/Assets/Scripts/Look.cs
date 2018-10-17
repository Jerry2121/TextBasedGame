using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Look")]
public class Look : InputAction {

    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.DisplayRoomText();
    }

}
