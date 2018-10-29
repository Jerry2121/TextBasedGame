using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class SaveLoadGame : MonoBehaviour {

    GameController controller;
    
    List<string> actionLog;
    string roomName;
    List<string> inventory;
    List<string> equipment;
    int score;
    int moves;

    void Start()
    {
        controller = GetComponent<GameController>();
    }

    public void SaveGame(string fileName)
    {
        actionLog = controller.actionLog;
        roomName = controller.roomNavigation.currentRoom.roomName;
        inventory = controller.interactableItems.nounsInInventory;
        equipment = controller.interactableItems.nounsInEquipment;
        score = controller.score;
        moves = controller.moves;

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + ".tbg", FileMode.OpenOrCreate);
        GameStateInfo myInfo = new GameStateInfo();

        //put what ever you're saving as myInfo.whatever
        //myInfo.gameText = gameText;
        myInfo.actionLog = actionLog;
        myInfo.roomName = roomName;
        myInfo.inventory = inventory;
        myInfo.equipment = equipment;
        myInfo.score = score;
        myInfo.moves = moves;
        bf.Serialize(file, myInfo);
        file.Close();
        controller.LogStringWithReturn("Game saved as " + fileName);
    }

    public void LoadGame(string fileName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".tbg"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + ".tbg", FileMode.Open);
            GameStateInfo myLoadedInfo = (GameStateInfo)bf.Deserialize(file);
            //gameText = myLoadedInfo.gameText;
            actionLog = myLoadedInfo.actionLog;
            roomName = myLoadedInfo.roomName;
            inventory = myLoadedInfo.inventory;
            equipment = myLoadedInfo.equipment;
            score = myLoadedInfo.score;
            moves = myLoadedInfo.moves;

            SetUpGame(fileName);
        }
        else
        {
            controller.LogStringWithReturn("Could not find saved game: " + fileName);
        }
    }

    void SetUpGame(string fileName)
    {
        controller.ResetScoreAndMoves();
        controller.ClearScreen();

        controller.IncreaseScore(score);
        controller.IncreaseMoves(moves);
        //controller.displayText.text = gameText;
        controller.interactableItems.nounsInInventory = inventory;
        controller.interactableItems.nounsInEquipment = equipment;

        actionLog.Reverse<string>();

        foreach(string s in actionLog)
        {
            controller.LogStringWithReturn(s);
        }

        for (int i = 0; i < controller.roomNavigation.allRooms.Length; i++)
        {
            if(controller.roomNavigation.allRooms[i].roomName == roomName)
            {
                controller.roomNavigation.currentRoom = controller.roomNavigation.allRooms[i];
                break;
            }
        }
        controller.LogStringWithReturn("Game " + fileName + " loaded");
        controller.DisplayRoomText();
    }

    public void GetSavedGames()
    {
        controller.LogStringWithReturn("Saved Games:");

        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dir.GetFiles("*.*");

        string tempString = "";
        string[] tempStringSplit;

        foreach (FileInfo f in info)
        {
            tempString = f.ToString();
            tempStringSplit = tempString.Split('\\');
            tempStringSplit = tempStringSplit[tempStringSplit.Length - 1].Split('.');
            Debug.Log(tempStringSplit[0].ToString());
            controller.LogStringWithReturn("- " + tempStringSplit[0].ToString());
        }
    }

}

[System.Serializable]
public class GameStateInfo
{
    //public string gameText;
    public List<string> actionLog;
    public string roomName;
    public List<string> inventory;
    public List<string> equipment;
    public int score;
    public int moves;
}
