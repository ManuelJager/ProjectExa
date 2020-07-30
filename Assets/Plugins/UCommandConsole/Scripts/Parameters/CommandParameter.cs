using System;

namespace UCommandConsole
{
    internal class CommandParameter : ICommandParameter
    {
        private Func<object, object> getter;
        private Action<object, object> setter;

        public IParameterContext Context { get; }

        public CommandParameter(IParameterContext argumentContext, Func<object, object> getter, Action<object, object> setter)
        {
            this.Context = argumentContext;
            this.getter = getter;
            this.setter = setter;
        }

        public void SetValue(object target, object value)
        {
            setter(target, value);
        }

        public object GetValue(object target)
        {
            return getter(target);
        }
    }
}