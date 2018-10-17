using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/GoAW")]
public class GoAW : InputActionAW {

    public override void RespondToInput(GameControllerAW _controller, string[] _separatedInputWords)
    {
        _controller.roomNavagationAW.AttemptToChangeRooms(_separatedInputWords[1]);
    }
}
