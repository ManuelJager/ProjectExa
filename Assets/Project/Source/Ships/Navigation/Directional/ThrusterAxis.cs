using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class ThrusterAxis
    {
        private readonly ThrusterGroup positiveThrusterGroup;
        private readonly ThrusterGroup negativeThrusterGroup;

        public ThrusterAxis(float directionalThrust)
        {
            positiveThrusterGroup = new ThrusterGroup(directionalThrust);
            negativeThrusterGroup = new ThrusterGroup(directionalThrust);
        }

        public void Register(IThruster thruster, bool positiveComponent)
        {
            SelectGroup(positiveComponent).Add(thruster);
        }

        public void Unregister(IThruster thruster, bool positiveComponent)
        {
            SelectGroup(positiveComponent).Remove(thruster);
        }

        public void Fire(float scalar)
        {
            SelectGroup(scalar > 0f).SetFireStrength(Mathf.Abs(scalar));
            SelectGroup(!(scalar > 0f)).SetFireStrength(0);
        }

        private ThrusterGroup SelectGroup(bool positiveComponent)
        {
            return positiveComponent
                ? positiveThrusterGroup
                : negativeThrusterGroup;
        }
    }
}