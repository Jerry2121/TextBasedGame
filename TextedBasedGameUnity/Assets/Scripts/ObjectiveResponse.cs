using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/ObjectiveResponse")]
public class ObjectiveResponse : ActionResponse
{

    public InteractableObject[] otherItemsRequired;
    [TextArea]
    public string winText;
    [TextArea]
    public string loseText;
    [TextArea]
    public string failedInteractionText;

    public Room winRoom;
    public Room loseRoom;

    public override bool DoActionResponse(GameController controller)
    {
        for (int i = 0; i < otherItemsRequired.Length; i++)
        {
            if (!controller.interactableItems.nounsInInventory.Contains(otherItemsRequired[i].noun))
            {
                controller.LogStringWithReturn(failedInteractionText);
                return false;
            }
        }

        return true;
    }

    void GameLose(GameController controller)
    {
        controller.LogStringWithReturn(loseText);
        controller.roomNavigation.currentRoom = loseRoom;
        controller.LogStringWithReturn("\n score: " + controller.score);
        controller.LogStringWithReturn("\n moves: " + controller.moves);
        controller.DisplayRoomText();
    }

    void GameWin(GameController controller)
    {
        controller.LogStringWithReturn(winText);
        controller.roomNavigation.currentRoom = winRoom;
        controller.LogStringWithReturn("\n score: " + controller.score);
        controller.LogStringWithReturn("\n moves: " + controller.moves);
        controller.DisplayRoomText();
    }
}
