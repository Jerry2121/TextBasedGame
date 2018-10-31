using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text displayText;
    public Text scoreText;
    public Text moveText;

    public Room startingRoom;
    public InputAction[] inputActions;

    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public List<string> interactionsDescriptionsInRoom = new List<string>();
    [HideInInspector] public InteractableItems interactableItems;
    [HideInInspector] public SaveLoadGame saveLoadGame;

    [HideInInspector]public List<string> actionLog = new List<string>();

    public string lastInput;

    public int score { get; protected set; }
    public int moves { get; protected set; }

	// Use this for initialization
	void Awake ()
    {
        roomNavigation = GetComponent<RoomNavigation>();
        interactableItems = GetComponent<InteractableItems>();
        saveLoadGame = GetComponent<SaveLoadGame>();
        Cursor.lockState = CursorLockMode.Locked;
        score = moves = 0;
	}

    void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    public void DisplayLoggedText()
    {
        List<string> logAsTextList = actionLog;

        int num = logAsTextList.Count;
        for (int i = 0; i < num - 24; i++)
        {
            logAsTextList.Remove(logAsTextList[i]);
        }

        string logAsText = string.Join("\n", logAsTextList.ToArray());

        displayText.text = logAsText;
    }

    public void DisplayRoomText()
    {
        ClearCollectionsForNewRoom();

        UnpackRoom();

        string joinedInteractionsDescriptions = string.Join("\n", interactionsDescriptionsInRoom.ToArray());

        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionsDescriptions;

        LogStringWithReturn(combinedText);
    }

    void UnpackRoom()
    {
        roomNavigation.UnpackExitsInRoom();
        PrepareObjectsToTakeOrExamine(roomNavigation.currentRoom);
        interactableItems.AddActionResponsesToUsableInRoomDictionary();
        AddInventoryItemsToExamineDictionary();
    }

    void PrepareObjectsToTakeOrExamine(Room currentRoom)
    {
        for (int i = 0; i < currentRoom.InteractableObjectsInRoom.Length; i++)
        {
            string descriptionNotInInventory = interactableItems.GetObjectsNotInInventory(currentRoom, i);
            if(descriptionNotInInventory != null)
            {
                interactionsDescriptionsInRoom.Add(descriptionNotInInventory);
            }

            InteractableObject interactableInRoom = currentRoom.InteractableObjectsInRoom[i];

            for (int j = 0; j < interactableInRoom.interactions.Length; j++)
            {
                Interaction interaction = interactableInRoom.interactions[j];
                if(interaction.inputAction.keyWord == "examine")
                {
                    interactableItems.examineDictionary.Add(interactableInRoom.noun, interaction.textResponse);
                }
                if (interaction.inputAction.keyWord == "take")
                {
                    interactableItems.takeDictionary.Add(interactableInRoom.pickupNoun, interaction.textResponse);
                }
                if (interaction.inputAction.keyWord == "equip")
                {
                    interactableItems.equipDictionary.Add(interactableInRoom.noun, interaction.textResponse);
                }
            }
        }
    }

    public void AddInventoryItemsToExamineDictionary()
    {
        for (int i = 0; i < interactableItems.nounsInInventory.Count; i++)
        {
            for (int j = 0; j < interactableItems.usableItemList.Count; j++)
            {
                if(interactableItems.usableItemList[j].noun == interactableItems.nounsInInventory[i])
                {
                    for (int n = 0; n < interactableItems.usableItemList[j].interactions.Length; n++)
                    {
                        if (interactableItems.usableItemList[j].interactions[n].inputAction.keyWord == "examine")
                        {
                            if(interactableItems.examineDictionary.ContainsKey(interactableItems.nounsInInventory[i]) == false)
                                interactableItems.examineDictionary.Add(interactableItems.nounsInInventory[i], interactableItems.usableItemList[j].interactions[n].textResponse);
                        }
                    }
                    
                }


            }
        }
    }

    public string TestVerbDictionaryWithNoun(Dictionary<string,string> verbDictionary, string verb, string noun)
    {
        if (verbDictionary.ContainsKey(noun))
        {
            return verbDictionary[noun];
        }

        IncreaseMoves();
        return "You can't " + verb + " " + noun;
    }

    void ClearCollectionsForNewRoom()
    {
        interactableItems.ClearCollections();
        interactionsDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    public void LogToTextFile(string[] separatedInputWords)
    {
        string path = separatedInputWords[1]+ ".txt";

        List<string> loggedText = actionLog;

        loggedText.Add("Score:" + score + "\n");
        loggedText.Add("Moves:" + moves + "\n");

        string[] loggedTextArray = loggedText.ToArray();

        string[] createText = (loggedTextArray);
        File.WriteAllLines(path, createText);

        LogStringWithReturn("Game history logged to " + path);
    }

    public void IncreaseScore(int num)
    {
        if (num < 0)
        {
            Debug.LogError("You should not be reducing the score value");
            return;
        }

        score += num;
        scoreText.text = "Score: " + score.ToString();
    }
    public void IncreaseMoves()
    {
        moves ++;
        moveText.text = "Moves: " + moves.ToString();
    }

    public void ClearScreen()
    {
        actionLog.Clear();
        displayText.text = " ";
    }

    public void ResetScoreAndMoves()
    {
        score = moves = 0;
        moveText.text = "Moves: 000";
        scoreText.text = "Score: 000000";
    }

    public void Restart()
    {
        roomNavigation.currentRoom = startingRoom;
        interactableItems.nounsInInventory.Clear();
        interactableItems.nounsInInventoryHistory.Clear();
        interactableItems.nounsInEquipment.Clear();
        ResetScoreAndMoves();
        ClearScreen();
        DisplayRoomText();
    }
    public void IncreaseMoves(int num)
    {
        if (num < 0)
        {
            Debug.LogError("You should not be reducing the move value");
            return;
        }
        moves += num;
        moveText.text = "Moves: " + moves.ToString();
    }
}


