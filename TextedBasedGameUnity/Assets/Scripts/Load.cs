using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Load")]
public class Load : InputAction{

    public override void RepsondToInput(GameController controller, string[] separatedInputWords)
    {
        if (separatedInputWords.Length > 1)
        {

        }
        else
        {
            //display saves
        }

    }

}
