using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Interaction {

    public InputAction inputAction;
    [TextArea]
    public string textResponse;
    public int scoreGiven = 0;
    public ActionResponse actionResponse;

}
