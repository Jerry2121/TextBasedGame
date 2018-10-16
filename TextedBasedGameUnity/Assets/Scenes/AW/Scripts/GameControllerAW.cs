using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerAW : MonoBehaviour {

    public Text displayText;

    [HideInInspector]
    public RoomNavagationAW roomNavagationAW;
    [HideInInspector]
    public List<string> interactionDescriptionsInRoom = new List<string>();

    List<string> actionLog = new List<string>();

	// Use this for initialization
	void Awake () {
        roomNavagationAW = GetComponent<RoomNavagationAW>();
	}

    void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    public void DisplayLoggedText()
    {
        string logAsTest = string.Join("\n", actionLog.ToArray());

        displayText.text = logAsTest;
    }

    public void DisplayRoomText()
    {
        ClearCollectionsForNewRoom();

        UnpackRoom();

        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());

        string combinedText = roomNavagationAW.currentRoomAW.description + "\n" + joinedInteractionDescriptions;

        LogStringWithReturn(combinedText);
    }

    private void UnpackRoom()
    {
        roomNavagationAW.UnpackExitsInRoom();
    }

    void ClearCollectionsForNewRoom()
    {
        interactionDescriptionsInRoom.Clear();
        roomNavagationAW.ClearExits();
    }

    public void LogStringWithReturn(string _stringToAdd)
    {
        actionLog.Add(_stringToAdd + "\n");

    }

	// Update is called once per frame
	void Update () {
		
	}
}
