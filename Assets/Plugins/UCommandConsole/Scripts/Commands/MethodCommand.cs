using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UCommandConsole.Attributes;

namespace UCommandConsole
{
    [IgnoreHistory]
    public class MethodCommand : Command
    {
        private MethodInfo methodInfo;
        private object target;

        public override string GetName() => methodInfo.Name;

        public Dictionary<string, object> Arguments { get; private set; }

        public MethodCommand(object target, MethodInfo methodInfo)
        {
            this.target = target;
            this.methodInfo = methodInfo;
            this.Arguments = new Dictionary<string, object>();
        }

        public MethodCommand(object target, string methodName)
            : this(target, target.GetType().GetMethod(methodName))
        {
        }

        public override void Execute(Console host)
        {
            var values = Context.arguments.Values
                .Select((prop) => prop.GetValue(this))
                .ToArray();

            methodInfo.Invoke(target, values);
        }

        public override void BuildParameters()
        {
            foreach (var parameterInfo in methodInfo.GetParameters())
            {
                var parameterName = parameterInfo.Name;
                var argumentContext = new MethodArgumentContext
                {
                    PropertyType = parameterInfo.ParameterType,
                    Name = parameterInfo.Name
                };
                Func<object, object> getter = (target) =>
                {
                    return (target as MethodCommand).Arguments[parameterName];
                };
                Action<object, object> setter = (target, value) =>
                {
                    (target as MethodCommand).Arguments[parameterName] = value;
                };
                Context.arguments[parameterInfo.Name] = new CommandParameter(argumentContext, getter, setter);
            }
        }
    }
}