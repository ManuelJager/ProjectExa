using System;
using UCommandConsole.Attributes;

namespace UCommandConsole
{
    [IgnoreHistory]
    internal class GetCommand : Command
    {
        public override string GetName() => "get";
        
        public override void Execute(Console host)
        {
            throw new NotImplementedException();
        }
    }
}