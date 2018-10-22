using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Examine")]
public class Examine : InputAction {

    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords.Length > 1)
            controller.LogStringWithReturn(controller.TestVerbDictionaryWithNoun(controller.interactableItems.examineDictionary, separatedInputWords[0], separatedInputWords[1]));
        else
            controller.LogStringWithReturn("examine what?");
    }

}
