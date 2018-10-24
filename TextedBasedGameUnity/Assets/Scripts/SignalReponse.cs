using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponses/SignalResponse")]
public class SignalReponse : ActionResponse {

    public override bool DoActionResponse(GameController controller)
    {

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
