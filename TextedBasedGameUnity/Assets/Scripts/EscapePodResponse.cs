using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/EscapePodResponse")]
public class EscapePodResponse : ActionResponse
{
    [TextArea]
    public string outcomeAText;
    [TextArea]
    public string outcomeBText;

    public Room winRoom;
    public Room loseRoom;

    public int scoreGivenA;

    public override bool DoActionResponse(GameController controller)
    {
        OutcomeA(controller);
        return true;
    }

    void OutcomeA(GameController controller)
    {
        controller.LogStringWithReturn(outcomeAText);
        controller.roomNavigation.currentRoom = loseRoom;
        controller.LogStringWithReturn("\n score: " + controller.Score);
        controller.LogStringWithReturn("\n moves: " + controller.Moves);
        controller.DisplayRoomText();
        controller.IncreaseScore(scoreGivenA);
    }
}
