using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour {

    public List<InteractableObject> usableItemList;
    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> equipDictionary = new Dictionary<string, string>();
    public Dictionary<string, ActionResponse> usableInRoomDictionary = new Dictionary<string, ActionResponse>();
    public Dictionary<string, string> pickNounToRealNounDictionary = new Dictionary<string, string>();

    [HideInInspector]
    public List<string> nounsInRoom = new List<string>();

    private Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();
    private GameController controller;
    public List<string> nounsInInventory = new List<string>();
    public List<string> nounsInInventoryHistory = new List<string>();
    public List<string> nounsInEquipment = new List<string>();

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public string GetObjectsNotInInventory(Room currentRoom, int i)
    {
        InteractableObject interactableInRoom = currentRoom.InteractableObjectsInRoom[i];

        if (nounsInInventoryHistory.Contains(interactableInRoom.noun) == false)
        {
            pickNounToRealNounDictionary.Add(interactableInRoom.pickupNoun, interactableInRoom.noun);
            nounsInRoom.Add(interactableInRoom.pickupNoun);
            return interactableInRoom.description;
        }

        return null;
    }

    public string GetObjectsNotInEquipment(Room currentRoom, int i)
    {
        InteractableObject interactableInRoom = currentRoom.InteractableObjectsInRoom[i];

        if (nounsInEquipment.Contains(interactableInRoom.noun) == false)
        {
            nounsInRoom.Add(interactableInRoom.pickupNoun);
            return interactableInRoom.description;
        }

        return null;
    }

    public void AddActionResponsesToUseDictionary()
    {
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            string noun = nounsInInventory[i];

            InteractableObject interactableObjectInInventory = GetInteractableObjectFromUsableList(noun);

            if (interactableObjectInInventory == null)
                continue;

            for (int j = 0; j < interactableObjectInInventory.interactions.Length; j++)
            {
                Interaction interaction = interactableObjectInInventory.interactions[j];

                if (interaction.actionResponse == null)
                    continue;

                if (useDictionary.ContainsKey(noun) == false)
                {
                    useDictionary.Add(noun, interaction.actionResponse);
                }
            }
        }
    }

    public void AddActionResponsesToUsableInRoomDictionary()
    {
        for (int i = 0; i < nounsInRoom.Count; i++)
        {
            string noun = nounsInRoom[i];
            InteractableObject interactableObjectInRoom = GetInteractableObjectFromUsableList(noun);

            if (interactableObjectInRoom == null)
                continue;

            for (int j = 0; j < interactableObjectInRoom.interactions.Length; j++)
            {
                Interaction interaction = interactableObjectInRoom.interactions[j];

                if (interaction.actionResponse == null || interaction.inputAction.keyWord != "use")
                    continue;

                if (usableInRoomDictionary.ContainsKey(noun) == false)
                {
                    usableInRoomDictionary.Add(noun, interaction.actionResponse);
                }
            }
        }
    }

    private InteractableObject GetInteractableObjectFromUsableList(string noun)
    {
        for (int i = 0; i < usableItemList.Count; i++)
        {
            if (usableItemList[i].noun == noun)
                return usableItemList[i];
        }
        return null;
    }

    public void DisplayInventory()
    {
        if (nounsInInventory.Count >= 1)
        {
            controller.LogStringWithReturn("You look in your backpack, inside you have: ");

            int nounsPerLine = 1;

            if (nounsInInventory.Count > 5)
            {
                int value = nounsInInventory.Count / 5;
                nounsPerLine = value + 1;


                for (int i = 0; i < nounsInInventory.Count; i++)
                {
                    string foo = "";
                    for (int j = 0; j < nounsPerLine; j++)
                    {
                        foo += nounsInInventory[i + j] + ", ";
                    }
                    i++;
                    controller.LogStringWithReturn(foo += "\n");
                }
            }
            else
            {
                for (int i = 0; i < nounsInInventory.Count; i++)
                {
                    controller.LogStringWithReturn(nounsInInventory[i]);
                }
            }
        }
        else
        {
            controller.LogStringWithReturn("You look in your backpack, and find nothing");
        }
    }

    public void ClearCollections()
    {
        equipDictionary.Clear();
        takeDictionary.Clear();
        usableInRoomDictionary.Clear();
        examineDictionary.Clear();
        nounsInRoom.Clear();
        pickNounToRealNounDictionary.Clear();
    }

    public Dictionary<string, string> Take (string[] separatedInputWords)
    {
        if (separatedInputWords.Length > 1)
        {
            string noun = separatedInputWords[1];

            if (nounsInRoom.Contains(noun))
            {
                /*nounsInInventory.Add(noun);
                nounsInRoom.Remove(noun);
                AddActionResponsesToUseDictionary();*/
                string realNoun = pickNounToRealNounDictionary[noun];
                Interaction[] itemInteractions = GetInteractableObjectFromUsableList(realNoun).interactions;
                for (int i = 0; i < itemInteractions.Length; i++)
                {

                    if (itemInteractions[i].inputAction.keyWord == "take")
                    {
                        nounsInInventory.Add(realNoun);
                        nounsInInventoryHistory.Add(realNoun);
                        nounsInRoom.Remove(noun);
                        AddActionResponsesToUseDictionary();

                        controller.IncreaseScore(itemInteractions[i].scoreGiven);
                    }
                }
                controller.IncreaseMoves();

                return takeDictionary;
            }
            else
            {
                controller.LogStringWithReturn("There is no " + noun + " here to take.");
                return null;
            }
        }
        else
        {
            controller.LogStringWithReturn("take what?");
            return null;
        }
    }

    public void DisplayEquipment()
    {
        if (nounsInEquipment.Count >= 1)
        {
            controller.LogStringWithReturn("You have: ");

            int nounsPerLine = 1;

            if (nounsInEquipment.Count > 5)
            {
                int value = nounsInEquipment.Count / 5;
                nounsPerLine = value + 1;


                for (int i = 0; i < nounsInEquipment.Count; i++)
                {
                    string foo = "";
                    for (int j = 0; j < nounsPerLine; j++)
                    {
                        foo += nounsInEquipment[i + j] + ", ";
                    }
                    i++;
                    controller.LogStringWithReturn(foo += "\n");
                }
            }
            else
            {
                for (int i = 0; i < nounsInEquipment.Count; i++)
                {
                    controller.LogStringWithReturn(nounsInEquipment[i]);
                }
            }
            controller.LogStringWithReturn("equipped");
        }
        else
        {
            controller.LogStringWithReturn("You have nothing equipped");
        }
    }

    public Dictionary<string, string> Equip(string[] separatedInputWords)
    {
        if (separatedInputWords.Length > 1)
        {
            string noun = separatedInputWords[1];

            if (nounsInRoom.Contains(noun))
            {
                nounsInEquipment.Add(noun);
                nounsInRoom.Remove(noun);
                //AddActionResponsesToUseDictionary();

                Interaction[] itemInteractions = GetInteractableObjectFromUsableList(noun).interactions;
                for (int i = 0; i < itemInteractions.Length; i++)
                {

                    if (itemInteractions[i].inputAction.keyWord == "equip")
                    {
                        controller.IncreaseScore(itemInteractions[i].scoreGiven);
                    }
                }
                controller.IncreaseMoves();

                return equipDictionary;
            }
            else
            {
                controller.LogStringWithReturn("There is no " + noun + " here to equip.");
                return null;
            }
        }
        else
        {
            controller.LogStringWithReturn("equip what?");
            return null;
        }
    }

    public void UseItem(string[] separatedInputWords)
    {
        if (separatedInputWords.Length > 1)
        {
            string nounToUse = separatedInputWords[1];
            if (useDictionary.ContainsKey(nounToUse) || usableInRoomDictionary.ContainsKey(nounToUse))
            {
                if (nounsInInventory.Contains(nounToUse) || nounsInRoom.Contains(nounToUse))
                {
                    bool actionResult;
                    if (useDictionary.ContainsKey(nounToUse))
                        actionResult = useDictionary[nounToUse].DoActionResponse(controller);
                    else
                        actionResult = usableInRoomDictionary[nounToUse].DoActionResponse(controller);

                    if (actionResult == false)
                    {
                        controller.LogStringWithReturn("Hmm. Nothing happens.");
                    }
                    else
                    {
                        controller.IncreaseMoves();

                        Interaction[] itemInteractions = GetInteractableObjectFromUsableList(nounToUse).interactions;
                        for (int i = 0; i < itemInteractions.Length; i++)
                        {

                            if (itemInteractions[i].inputAction.keyWord == "use")
                            {
                                controller.IncreaseScore(itemInteractions[i].scoreGiven);
                            }
                        }
                    }
                }
                else
                {
                    controller.LogStringWithReturn("There is no " + nounToUse);                    
                }
            }
            else
            {
                controller.LogStringWithReturn("You can't use the " + nounToUse);
            }
        }
        else
            controller.LogStringWithReturn("use what?");
    }

    public bool CheckInventoryOrEquiptment(string nounToCheck)
    {
        if (nounsInInventory.Contains(nounToCheck) || nounsInEquipment.Contains(nounToCheck))
            return true;
        else return false;
    }

}
