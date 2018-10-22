using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Equip")]
public class Equip : InputAction {

    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        Dictionary<string, string> equipDictionary = controller.interactableItems.Take(separatedInputWords);

        if (equipDictionary != null)
        {
            controller.LogStringWithReturn(controller.TestVerbDictionaryWithNoun(equipDictionary, separatedInputWords[0], separatedInputWords[1]));
        }
    }

}
