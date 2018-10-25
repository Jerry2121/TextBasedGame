using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Sing")]
public class Sing : InputAction {

    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {

        int num = Random.Range(1, 5);

        if (separatedInputWords.Length == 4)
        {
            if (separatedInputWords[1] == "to" && separatedInputWords[2] == "the" && separatedInputWords[3] == "birds")
            {
                controller.LogStringWithReturn("You sing to the birds. Why?");
            }
            else
            {
                switch (num)
                {
                    case 1:
                        controller.LogStringWithReturn("What are you doing?");
                        break;
                    case 2:
                        controller.LogStringWithReturn("Don't you have better things to do?");
                        break;
                    case 3:
                        controller.LogStringWithReturn("Why are you singing?");
                        break;
                    case 4:
                        controller.LogStringWithReturn("Survival first, singing later");
                        break;
                }
            }
        }
        else
        {
            switch (num)
            {
                case 1:
                    controller.LogStringWithReturn("What are you doing?");
                    break;
                case 2:
                    controller.LogStringWithReturn("Don't you have better things to do?");
                    break;
                case 3:
                    controller.LogStringWithReturn("Why are you singing?");
                    break;
                case 4:
                    controller.LogStringWithReturn("Survival first, singing later");
                    break;
            }
        }
    }

}
