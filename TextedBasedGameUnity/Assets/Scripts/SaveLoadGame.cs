using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadGame : MonoBehaviour {

    GameController controller;

    string gameText;
    string roomName;
    List<string> inventory;
    List<string> equipment;

    void Start()
    {
        controller = GetComponent<GameController>();
    }

    public void SaveGame(string fileName)
    {
        gameText = controller.displayText.text; //may not save spacing use log as example to fix
        roomName = controller.roomNavigation.currentRoom.roomName;
        inventory = controller.interactableItems.nounsInInventory;
        equipment = controller.interactableItems.nounsInEquipment;

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + ".tbg", FileMode.OpenOrCreate);
        GameStateInfo myInfo = new GameStateInfo();

        //put what ever you're saving as myInfo.whatever
        myInfo.gameText = gameText;
        myInfo.roomName = roomName;
        myInfo.inventory = inventory;
        myInfo.equipment = equipment;
        bf.Serialize(file, myInfo);
        file.Close();
        controller.LogStringWithReturn("Game saved as " + fileName);
    }

    public void LoadGame(string fileName)
    {
        Debug.Log("LoadGame");
        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".tbg"))
        {
            Debug.Log("LoadGame -- past if");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + ".tbg", FileMode.Open);
            GameStateInfo myLoadedInfo = (GameStateInfo)bf.Deserialize(file);
            gameText = myLoadedInfo.gameText;
            roomName = myLoadedInfo.roomName;
            inventory = myLoadedInfo.inventory;
            equipment = myLoadedInfo.equipment;

            SetUpGame();
        }
        else
        {
            controller.LogStringWithReturn("Could not find saved game: " + fileName);
        }
    }

    void SetUpGame()
    {
        controller.ClearScreen();

        Debug.Log("SetUpGame");
        controller.displayText.text = gameText;
        controller.interactableItems.nounsInInventory = inventory;
        controller.interactableItems.nounsInEquipment = equipment;

        for (int i = 0; i < controller.roomNavigation.allRooms.Length; i++)
        {
            if(controller.roomNavigation.allRooms[i].roomName == roomName)
            {
                controller.roomNavigation.currentRoom = controller.roomNavigation.allRooms[i];
                break;
            }
        }
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
    public string gameText;
    public string roomName;
    public List<string> inventory;
    public List<string> equipment;
}
