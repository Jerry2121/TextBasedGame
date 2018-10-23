using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Quit")]
public class QuitGame : InputAction {

    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        Application.Quit();
    }

}
