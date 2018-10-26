using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/SignalResponse")]
public class SignalReponse : ActionResponse {

    public InteractableObject[] otherItemsRequired;

    public override bool DoActionResponse(GameController controller)
    {
        for (int i = 0; i < otherItemsRequired.Length; i++)
        {
            if (!controller.interactableItems.nounsInInventory.Contains(otherItemsRequired[i].noun))
            {
                controller.LogStringWithReturn("The terminal is dead. Maybe you could jumpstart it somehow.");
                return false;
            }
        }

        int chanceForEnemies = 99;

        if (Random.Range(1,101) <= chanceForEnemies)
            EnemyFindsYou();
        else
            FriendlyFindsYou();

        return true;
    }

    void EnemyFindsYou()
    {
        throw new System.NotImplementedException();
    }
    void FriendlyFindsYou()
    {
        throw new System.NotImplementedException();
    }
}
