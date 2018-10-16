using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavagationAW : MonoBehaviour {

    public RoomAW currentRoomAW;

    Dictionary<string, RoomAW> exitDictionary = new Dictionary<string, RoomAW>();

    private GameControllerAW controller;

    private void Awake()
    {
        controller = GetComponent<GameControllerAW>();
    }

    public void UnpackExitsInRoom()
    {
        for (int i = 0; i < currentRoomAW.exits.Length; i++)
        {
            exitDictionary.Add(currentRoomAW.exits[i].keyString, currentRoomAW.exits[i].valueRoom);
            controller.interactionDescriptionsInRoom.Add(currentRoomAW.exits[i].exitDescription);

        }
    }

    void AttemptToChangeRooms(string _directionNoun)
    {
        if (exitDictionary.ContainsKey(_directionNoun))
        {
            currentRoomAW = exitDictionary[_directionNoun];
            controller.LogStringWithReturn("You head of to the " + _directionNoun);
            controller.DisplayRoomText();
        }
        else
        {
            controller.LogStringWithReturn("there is no path to the " + _directionNoun);
        }
    }

    public void ClearExits()
    {
        exitDictionary.Clear();
    }

}
