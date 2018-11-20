using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Take")]
public class Take : InputAction {

    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords[separatedInputWords.Length - 1] == "nap" && separatedInputWords.Length == 2)
        {
            controller.LogStringWithReturn("You take a quick nap!\n...\nRefreshing!");
        }
        else
        {
            Dictionary<string, string> takeDictionary = controller.interactableItems.Take(separatedInputWords);

            if (takeDictionary != null)
            {
                controller.LogStringWithReturn(controller.TestVerbDictionaryWithNoun(takeDictionary, separatedInputWords[0], separatedInputWords[1]));

            }
        }
    }
}
