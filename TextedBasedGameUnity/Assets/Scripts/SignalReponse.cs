using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/SignalResponse")]
public class SignalReponse : ActionResponse {

    public InteractableObject[] otherItemsRequired;
    [TextArea]
    public string winText;
    [TextArea]
    public string loseText;

    public Room winRoom;
    public Room loseRoom;

    public override bool DoActionResponse(GameController controller)
    {
        for (int i = 0; i < otherItemsRequired.Length; i++)
        {
            if (!controller.interactableItems.nounsInInventory.Contains(otherItemsRequired[i].noun))
            {
                controller.LogStringWithReturn("The terminal is dead. Maybe you could jumpstart it somehow.");
                return false;
            }
        }

        int chanceForEnemies = 99;
        // Check for items in inventory, decrease chanceForEnemies accordingly

        if (Random.Range(1,101) <= chanceForEnemies)
            EnemyFindsYou(controller);
        else
            FriendlyFindsYou(controller);

        return true;
    }

    void EnemyFindsYou(GameController controller)
    {
        controller.LogStringWithReturn(loseText);
        controller.roomNavigation.currentRoom = loseRoom;
        controller.LogStringWithReturn("\n score: " + controller.score);
        controller.LogStringWithReturn("\n moves: " + controller.moves);
        controller.DisplayRoomText();
    }
    void FriendlyFindsYou(GameController controller)
    {
        controller.LogStringWithReturn(winText);
        controller.roomNavigation.currentRoom = winRoom;
        controller.LogStringWithReturn("\n score: " + controller.score);
        controller.LogStringWithReturn("\n moves: " + controller.moves);
        controller.DisplayRoomText();
    }
}
