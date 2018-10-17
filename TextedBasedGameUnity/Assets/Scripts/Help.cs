using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Help")]
public class Help : InputAction {

    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        controller.LogStringWithReturn("These are the commands you can use:");

        for (int i = 0; i < controller.inputActions.Length; i++)
        {
            if(controller.inputActions[i].keyWord != "help")
                controller.LogStringWithReturn("\n" + controller.inputActions[i].keyWord);
        }
    }

}
