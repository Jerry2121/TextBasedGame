using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour {

    public Room currentRoom;


    Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();
    GameController controller;

    void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public void UnpackExitsInRoom()
    {
        for (int i = 0; i < currentRoom.exits.Length; i++)
        {
            exitDictionary.Add(currentRoom.exits[i].keyString, currentRoom.exits[i].valueRoom);
            controller.interactionsDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
        }
    }

    public void AttemptToChangeRooms(string directionNoun)
    {
        Exit attemptedExit = null;
        directionNoun.ToLower();
        if (exitDictionary.ContainsKey(directionNoun))
        {
            for (int i = 0; i < currentRoom.exits.Length; i++)
            {
                if (currentRoom.exits[i].keyString == directionNoun)
                    attemptedExit = currentRoom.exits[i];
            }

            if (attemptedExit.requiredObjects.Length > 0){
                for (int i = 0; i < attemptedExit.requiredObjects.Length; i++)
                {
                    
                    if (!controller.interactableItems.CheckInventoryOrEquiptment(attemptedExit.requiredObjects[i].noun))
                    {
                        controller.LogStringWithReturn("You missing some items or equipment you need to use that exit");
                        return;
                    }
                }
                currentRoom = exitDictionary[directionNoun];
                controller.LogStringWithReturn("You head off to the " + directionNoun);
                controller.DisplayRoomText();
                controller.IncreaseMoves();
            }
            else{
                currentRoom = exitDictionary[directionNoun];
                controller.LogStringWithReturn("You head off to the " + directionNoun);
                controller.DisplayRoomText();
                controller.IncreaseMoves();
            }
        }
        else
        {
            controller.LogStringWithReturn("There is no path to the " + directionNoun);
        }
    }

    public void ClearExits()
    {
        exitDictionary.Clear();
    }
}
