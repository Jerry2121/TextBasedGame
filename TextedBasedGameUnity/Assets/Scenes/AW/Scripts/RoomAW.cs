using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/RoomAW")]
public class RoomAW : ScriptableObject {

    [TextArea]
    public string description;
    public string roomName;
    public ExitAW[] exits;

}
