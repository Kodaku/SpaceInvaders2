public delegate void StarsDelegate();

public static class EventHandler
{
    public static event StarsDelegate GenerateStarsEvent;

    public static void CallGenerateStarsEvent()
    {
        if(GenerateStarsEvent != null)
        {
            GenerateStarsEvent();
        }
    }
}
