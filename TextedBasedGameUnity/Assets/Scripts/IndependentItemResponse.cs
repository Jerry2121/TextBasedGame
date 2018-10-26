using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/IndependentItemResponse")]
public class IndependentItemResponse : ActionResponse
{
    [TextArea]
    public string textResponse;

    public override bool DoActionResponse(GameController controller)
    {
        controller.LogStringWithReturn(textResponse);
        return true;
    }
}
