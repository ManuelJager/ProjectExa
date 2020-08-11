namespace Exa.AI
{
    public interface IAIAction
    {
        bool IsRunning { get; set; }
        AIActionLane Lanes { get; }
        float Priority { get; }

        void Run(ref AIActionLane blockedLanes);
    }
}