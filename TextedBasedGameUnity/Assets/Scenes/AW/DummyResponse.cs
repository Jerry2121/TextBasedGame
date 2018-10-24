using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/DummyResponse")]
public class DummyResponse : ActionResponse {

    public Room exitToChangeTo;
    public string exitChangedName;

    public override bool DoActionResponse(GameController controller)
    { 
        Debug.Log("DummyResponse");
        if(controller.roomNavigation.currentRoom.roomName == requiredString)
        {
            for (int i = 0; i < controller.roomNavigation.currentRoom.exits.Length; i++)
            {
                if(controller.roomNavigation.currentRoom.exits[i].valueRoom.roomName == exitChangedName)
                {
                    controller.roomNavigation.currentRoom.exits[i].valueRoom = exitToChangeTo;
                    break;
                }
            }

            return true;
        }
        return false;
    }

}
