using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Go")]
public class Go : InputAction
{
    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords.Length > 1)
            controller.roomNavigation.AttemptToChangeRooms(separatedInputWords[1]);
        else
            controller.LogStringWithReturn("go where?");
    }
}
