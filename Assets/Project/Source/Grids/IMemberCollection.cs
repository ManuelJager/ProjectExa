using System;
using System.Collections;
using System.Collections.Generic;
using Exa;
using Exa.Grids;
using Exa.Grids.Blocks;

namespace Project.Source.Grids
{
    public interface IMemberCollection
    {
        public delegate void MemberChange(IGridMember member);

        public event MemberChange MemberAdded;
        public event MemberChange MemberRemoved;

        public IEnumerable<IGridMember> GetMembers();
    }
}