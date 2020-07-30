using System;
using UCommandConsole.Attributes;

namespace UCommandConsole
{
    [IgnoreHistory]
    internal class UndoCommand : Command
    {
        public override string GetName() => "undo";

        public override void Execute(Console host)
        {
            host.container.Undo();
        }

        public override void Undo(Console host)
        {
            throw new Exception("An undo command cannot be undone");
        }
    }
}