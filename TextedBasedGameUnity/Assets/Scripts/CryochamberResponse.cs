using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/CryochamberResponse")]
public class CryochamberResponse : ActionResponse
{

    public InteractableObject[] itemsRequired;
    [TextArea]
    public string noteAText;
    [TextArea]
    public string noteBText;
    [TextArea]
    public string failedInteractionText;

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

        if (controller.interactableItems.nounsInInventory.Contains("note-a"))
        {
            NoteAEnd(controller);
        }

        if (controller.interactableItems.nounsInInventory.Contains("note-b"))
        {
            NoteBEnd(controller);
        }

        return true;
    }

    void NoteAEnd(GameController controller)
    {
        controller.LogStringWithReturn(noteAText);
        controller.roomNavigation.currentRoom = winRoom;
        controller.LogStringWithReturn("\n score: " + controller.score);
        controller.LogStringWithReturn("\n moves: " + controller.moves);
        controller.DisplayRoomText();
    }

    void NoteBEnd(GameController controller)
    {
        controller.LogStringWithReturn(noteBText);
        controller.roomNavigation.currentRoom = loseRoom;
        controller.LogStringWithReturn("\n score: " + controller.score);
        controller.LogStringWithReturn("\n moves: " + controller.moves);
        controller.DisplayRoomText();
    }
}
