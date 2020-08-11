namespace Exa.AI
{
    public interface IAction
    {
        bool IsRunning { get; set; }
        ActionLane Lanes { get; }
        float Priority { get; }

        void Run(ref ActionLane blockedLanes);
    }
}