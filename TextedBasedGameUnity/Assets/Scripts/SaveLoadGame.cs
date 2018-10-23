using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadGame : MonoBehaviour {

    GameController controller;

    string gameText;
    Room room;
    List<string> inventory;
    List<string> equipment;

    void Start()
    {
        controller = GetComponent<GameController>();
    }

    public void SaveGame(string fileName)
    {
        gameText = controller.displayText.text; //may not save spacing use log as example to fix
        room = controller.roomNavigation.currentRoom;
        inventory = controller.interactableItems.nounsInInventory;
        equipment = controller.interactableItems.nounsInEquipment;

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + "/GameState" + fileName + ".tbg", FileMode.OpenOrCreate);
        GameStateInfo myInfo = new GameStateInfo();

        //put what ever you're saving as myInfo.whatever
        myInfo.gameText = gameText;
        myInfo.room = room;
        myInfo.inventory = inventory;
        myInfo.equipment = equipment;
        bf.Serialize(file, myInfo);
        file.Close();
        controller.LogStringWithReturn("Game saved as " + fileName);
    }

    public void LoadGame(string fileName)
    {
        if (File.Exists(Application.persistentDataPath + "/GameState" + fileName + ".tbg"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GameState" + fileName + ".tbg", FileMode.Open);
            GameStateInfo myLoadedInfo = (GameStateInfo)bf.Deserialize(file);
            gameText = myLoadedInfo.gameText;
            room = myLoadedInfo.room;
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
        controller.displayText.text = gameText;
        controller.roomNavigation.currentRoom = room;
        controller.interactableItems.nounsInInventory = inventory;
        controller.interactableItems.nounsInEquipment = equipment;
    }

}

public class GameStateInfo
{
    public string gameText;
    public Room room;
    public List<string> inventory;
    public List<string> equipment;
}
