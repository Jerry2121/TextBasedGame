using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/CryochamberResponse")]
public class CryochamberResponse : ActionResponse
{

    public InteractableObject[] itemsRequired;
    [TextArea]
    public string outcomeAText;
    [TextArea]
    public string outcomeBText;
    [TextArea]
    public string failedInteractionText;

    public int scoreGivenA;
    public int scoreGivenB;

    public Room winRoom;
    public Room loseRoom;

    public override bool DoActionResponse(GameController controller)
    {
        for (int i = 0; i < itemsRequired.Length; i++)
        {
            if (!controller.interactableItems.nounsInInventory.Contains(itemsRequired[i].noun))
            {
                controller.LogStringWithReturn(failedInteractionText);
                return false;
            }
        }

        if (controller.interactableItems.nounsInInventory.Contains("vitamins"))
        {
            OutcomeA(controller);
        }

        else
        {
            OutcomeB(controller);
        }

        return true;
    }

    void OutcomeA(GameController controller)
    {
        controller.LogStringWithReturn(outcomeAText);
        controller.roomNavigation.currentRoom = winRoom;
        controller.LogStringWithReturn("\n score: " + controller.score);
        controller.LogStringWithReturn("\n moves: " + controller.moves);
        controller.DisplayRoomText();
        controller.IncreaseScore(scoreGivenA);
    }

    void OutcomeB(GameController controller)
    {
        controller.LogStringWithReturn(outcomeBText);
        controller.roomNavigation.currentRoom = loseRoom;
        controller.LogStringWithReturn("\n score: " + controller.score);
        controller.LogStringWithReturn("\n moves: " + controller.moves);
        controller.DisplayRoomText();
        controller.IncreaseScore(scoreGivenB);
    }
}
