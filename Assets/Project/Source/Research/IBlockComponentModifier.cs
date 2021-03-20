﻿using System;
using System.Collections.Generic;
using Exa.Grids.Blocks;

namespace Exa.Research
{
    public interface IBlockComponentModifier
    {
        public bool AffectsTemplate(BlockTemplate blockTemplate);
        public abstract IEnumerable<ResearchStep> GetResearchSteps();
    }
}