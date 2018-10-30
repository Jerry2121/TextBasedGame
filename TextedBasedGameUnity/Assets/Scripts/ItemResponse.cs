using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/ItemResponse")]
public class ItemResponse : ActionResponse {

    public InteractableObject[] otherItemsRequired;
    public InteractableObject[] itemsToBeRemovedFromInventory;
    public InteractableObject itemToGive;
    [TextArea]
    public string successTextResponse;
    public int scoreGiven;

    public override bool DoActionResponse(GameController controller)
    {
        if (otherItemsRequired.Length > 0)
        {
            for (int i = 0; i < otherItemsRequired.Length; i++)
            {
                if (!controller.interactableItems.nounsInInventory.Contains(otherItemsRequired[i].noun))
                {
                    controller.LogStringWithReturn("You don't have the items necessary to do that!");
                    return false;
                }
            }
        }

        if (itemToGive != null && !controller.interactableItems.nounsInInventory.Contains(itemToGive.noun))
        {
            controller.interactableItems.nounsInInventory.Add(itemToGive.noun);
            controller.LogStringWithReturn(successTextResponse);
        }

        for (int i = 0; i < itemsToBeRemovedFromInventory.Length; i++)
        {
            controller.interactableItems.nounsInInventory.Remove(itemsToBeRemovedFromInventory[i].noun);
        }

        controller.IncreaseScore(scoreGiven);

        return true;
    }
}
