namespace Exa.AI
{
    public abstract class Action : IAction
    {
        public bool IsRunning { get; set; }
        public abstract ActionLane Lanes { get; }
        public abstract float Priority { get; }

        public abstract void Run(ref ActionLane blockedLanes);
    }
}