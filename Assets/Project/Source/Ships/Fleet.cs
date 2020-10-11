using System;
using System.Collections.Generic;
using Exa.Grids.Blueprints;
#pragma warning disable CS0649

namespace Exa.Ships
{
    public class Fleet
    {
        public BlueprintContainer mothership;
        public List<BlueprintContainer> units;

        public Fleet(int unitCapacity)
        {
            mothership = null;
            units = new List<BlueprintContainer>(unitCapacity);
        }
    }
}