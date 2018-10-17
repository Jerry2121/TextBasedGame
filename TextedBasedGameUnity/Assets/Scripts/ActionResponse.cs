using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionResponse : ScriptableObject {

    public string requiredString;

    public abstract bool DoActionResponse(GameController controller);

}
