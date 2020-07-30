using System;

namespace UCommandConsole
{
    public class MethodArgumentContext : IParameterContext
    {
        public Type CustomParser => null;

        public Type PropertyType { get; set; }

        public bool Required => true;

        public string Name { get; set; }
    }
}