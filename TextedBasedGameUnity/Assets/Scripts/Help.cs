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
            if(controller.inputActions[i].keyWord != "help" && controller.inputActions[i].keyWord != "sing" && controller.inputActions[i].keyWord != "credits")
                controller.LogStringWithReturn("- " + controller.inputActions[i].keyWord + " " + controller.inputActions[i].description);
        }

        /*for (int i = 0; i < controller.inputActions.Length; i++)
        {
            string fullLine = "";
            for (int j = 0; j < 2; j++)
            {
                fullLine += controller.inputActions[i + j].keyWord + ", ";
            }
            i++;
            controller.LogStringWithReturn(fullLine += "\n");
        }*/

    }

}
