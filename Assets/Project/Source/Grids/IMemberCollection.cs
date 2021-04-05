using System.Collections.Generic;
using Exa.Grids;

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