using System;
using System.Collections.Generic;
using System.Text;
using Exa.Utils;

namespace Exa.Types {
    public class ContextfulException {
        private readonly Exception innerException;
        private readonly Dictionary<string, object> context;
        
        public ContextfulException(Exception innerException) {
            this.innerException = innerException;
            context = new Dictionary<string, object>();
        }

        public void Add(string key, object value) {
            context.Add(key, value);
        }

        public override string ToString() {
            var sb = new StringBuilder();

            foreach (var (key, value) in context.Unpack()) {
                sb.AppendLine($"{key}={value}");
            }
            
            sb.AppendLine(innerException.ToString());

            return sb.ToString();
        }
    }

    public static class ContextfulExceptionExtensions {
        public static ContextfulException Context(this Exception exception) {
            return new ContextfulException(exception);
        }
    }
}