using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Go")]
public class Go : InputAction
{
    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);
    }
}
