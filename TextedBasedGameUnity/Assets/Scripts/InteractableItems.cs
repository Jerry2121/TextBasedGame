﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour {

    public List<InteractableObject> usableItemList;
    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> equipDictionary = new Dictionary<string, string>();


    [HideInInspector]
    public List<string> nounsInRoom = new List<string>();

    private Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();
    private GameController controller;
    private List<string> nounsInInventory = new List<string>();
    private List<string> nounsInEquipment = new List<string>();

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public string GetObjectsNotInInventory(Room currentRoom, int i)
    {
        InteractableObject interactableInRoom = currentRoom.InteractableObjectsInRoom[i];

        if (nounsInInventory.Contains(interactableInRoom.noun) == false)
        {
            nounsInRoom.Add(interactableInRoom.noun);
            return interactableInRoom.description;
        }

        return null;
    }

    public string GetObjectsNotInEquipment(Room currentRoom, int i)
    {
        InteractableObject interactableInRoom = currentRoom.InteractableObjectsInRoom[i];

        if (nounsInEquipment.Contains(interactableInRoom.noun) == false)
        {
            nounsInRoom.Add(interactableInRoom.noun);
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
        examineDictionary.Clear();
        nounsInRoom.Clear();
    }

    public Dictionary<string, string> Take (string[] separatedInputWords)
    {
        if (separatedInputWords.Length > 1)
        {
            string noun = separatedInputWords[1];

            if (nounsInRoom.Contains(noun))
            {
                nounsInInventory.Add(noun);
                nounsInRoom.Remove(noun);
                AddActionResponsesToUseDictionary();

                Interaction[] itemInteractions = GetInteractableObjectFromUsableList(noun).interactions;
                for (int i = 0; i < itemInteractions.Length; i++)
                {

                    if (itemInteractions[i].inputAction.keyWord == "take")
                    {
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

            if (nounsInInventory.Contains(nounToUse))
            {
                if (useDictionary.ContainsKey(nounToUse))
                {
                    bool actionResult = useDictionary[nounToUse].DoActionResponse(controller);

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
                    controller.LogStringWithReturn("You can't use the " + nounToUse);
                }
            }
            else
            {
                controller.LogStringWithReturn("There is no " + nounToUse + " in your inventory");
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
