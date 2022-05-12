using UnityEngine;

public delegate void StarsDelegate();
public delegate void DestroyEnemyDelegate(GameObject enemy);

public static class EventHandler
{
    public static event StarsDelegate GenerateStarsEvent;
    public static event DestroyEnemyDelegate DestroyEnemyEvent;

    public static void CallGenerateStarsEvent()
    {
        if(GenerateStarsEvent != null)
        {
            GenerateStarsEvent();
        }
    }

    public static void CallDestroyEnemyEvent(GameObject enemy)
    {
        if(DestroyEnemyEvent != null)
        {
            DestroyEnemyEvent(enemy);
        }
    }
}
