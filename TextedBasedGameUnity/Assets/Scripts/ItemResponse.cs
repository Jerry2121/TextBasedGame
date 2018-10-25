using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/ItemResponse")]
public class ItemResponse : ActionResponse {

    public InteractableObject itemToGive;
    [TextArea]
    public string successTextResponse;
    [TextArea]
    public string failureTextResponse;

    public override bool DoActionResponse(GameController controller)
    {
        if (!controller.interactableItems.nounsInInventory.Contains(itemToGive.noun))
        {
            controller.interactableItems.nounsInInventory.Add(itemToGive.noun);
            controller.LogStringWithReturn(successTextResponse);
        }
        else
        {
            controller.LogStringWithReturn(failureTextResponse);
        }

        return true;
    }
}
