using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Restart")]
public class Restart : InputAction {

    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.Restart();
    }

}
