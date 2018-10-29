using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Take")]
public class Take : InputAction {

    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords[1] != "nap")
        {
            Dictionary<string, string> takeDictionary = controller.interactableItems.Take(separatedInputWords);

            if (takeDictionary != null)
            {
                controller.LogStringWithReturn(controller.TestVerbDictionaryWithNoun(takeDictionary, separatedInputWords[0], separatedInputWords[1]));
            }
        }
        else
            controller.LogStringWithReturn("You take a quick nap");
    }

}
