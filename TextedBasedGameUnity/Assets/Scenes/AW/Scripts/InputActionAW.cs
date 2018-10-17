using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputActionAW : ScriptableObject {

    public string keyword;

    public abstract void RespondToInput(GameControllerAW _controller, string[] _separatedInputWords);

}
