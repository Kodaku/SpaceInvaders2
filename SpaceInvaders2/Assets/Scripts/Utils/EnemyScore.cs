using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyScore
{
    private static Dictionary<string, int> enemyScores = new Dictionary<string, int>() { { "Grunt", 10 }, { "Commander", 20 },
        { "Colonel", 30}, { "Boss", 100 } };

    public static int GetScore(string enemyTag)
    {
        if(enemyScores.ContainsKey(enemyTag))
            return enemyScores[enemyTag];
        return -1;
    }
}
