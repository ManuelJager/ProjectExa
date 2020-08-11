namespace Exa.AI
{
    public abstract class AIAction : IAIAction
    {
        public bool IsRunning { get; set; }
        public abstract AIActionLane Lanes { get; }
        public abstract float Priority { get; }

        public abstract void Run(ref AIActionLane blockedLanes);
    }
}