using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputAction : ScriptableObject
{
    public string keyWord;
    public string description;

    public abstract void RepsondToInput(GameController controller, string[] separatedInputWords);
}
