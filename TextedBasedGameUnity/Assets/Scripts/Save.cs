using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Save")]
public class Save : InputAction {

    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.SaveGame();
    }

}
