using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Exa.Debugging.Commands.Parser
{
    public abstract class ParameterfulCommand : Command
    {
        internal CommandTypeContext Context { get; }

        public ParameterfulCommand()
        {
            Context = new CommandTypeContext(GetType());
        }

        public ParameterfulCommand(CommandTypeContext context)
        {
            Context = context;
        }

        public override void CommandHandle(Console console, Tokenizer tokenizer)
        {
            SetDefaults();

            var count = Context.positionalProperties.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var property = Context.positionalProperties[i];
                    switch (tokenizer.Token)
                    {
                        case Token.Literal:
                            HandlePositionalLiteral(tokenizer, property);
                            break;

                        case Token.String:
                            HandlePositionalString(tokenizer, property);
                            break;

                        case Token.Number:
                            HandlePositionalNumber(tokenizer, property);
                            break;

                        case Token.Flag:
                            throw new IncorrectCommandFormatException($"Flags not supported as positional argument");
                        case Token.Key:
                            throw new IncorrectCommandFormatException($"Keys not supported as positional argument");
                        case Token.EOF:
                            throw new IncorrectCommandFormatException($"Missing value for argument[{i}] of type {property.PropertyType.ToString()}");
                    }
                }

                tokenizer.NextToken();
            }

            // Handle named arguments and flags
            while (tokenizer.Token != Token.EOF)
            {
                switch (tokenizer.Token)
                {
                    case Token.Flag:
                        HandleNamedFlag(tokenizer);
                        break;

                    case Token.Key:
                        HandleNamedKey(tokenizer);
                        break;

                    case Token.String:
                    case Token.Literal:
                    case Token.Number:
                        throw new IncorrectCommandFormatException("A value type should be proceded by a key");
                }

                tokenizer.NextToken();
            }

            CommandAction();
        }

        // Reset values of this object
        public void SetDefaults()
        {
            var props = TypeDescriptor.GetProperties(this);
            for (int i = 0; i < props.Count; i++)
            {
                var prop = props[i];
                if (prop.CanResetValue(this) && prop.PropertyType.IsValueType)
                {
                    prop.ResetValue(this);
                }
            }
        }

        private void HandlePositionalLiteral(Tokenizer tokenizer, PropertyInfo property)
        {
            SetEnumProperty(tokenizer, property);
        }

        private void HandlePositionalString(Tokenizer tokenizer, PropertyInfo property)
        {
            SetStringProperty(tokenizer, property);
        }

        private void HandlePositionalNumber(Tokenizer tokenizer, PropertyInfo property)
        {
            SetNumberProperty(tokenizer, property);
        }

        /// <summary>
        /// Handle a leading flag token
        /// </summary>
        private void HandleNamedFlag(Tokenizer tokenizer)
        {
            var property = Context.aliasedProperties[tokenizer.Value];

            property.SetValue(this, true, null);
        }

        /// <summary>
        /// Handle a leading key token
        /// </summary>
        private void HandleNamedKey(Tokenizer tokenizer)
        {
            // Get property data
            var property = Context.aliasedProperties[tokenizer.Value];

            // Skip to next token, expecting a value type
            tokenizer.NextToken();

            switch (tokenizer.Token)
            {
                case Token.Literal:
                    SetEnumProperty(tokenizer, property);
                    break;

                case Token.Flag:
                    throw new IncorrectCommandFormatException("Cannot provide a flag after a key");
                case Token.Key:
                    throw new IncorrectCommandFormatException("Cannot provide a key after a key");
                case Token.String:
                    SetStringProperty(tokenizer, property);
                    break;

                case Token.Number:
                    SetNumberProperty(tokenizer, property);
                    break;

                case Token.EOF:
                    throw new IncorrectCommandFormatException("Expected a value after a key");
            }
        }

        /// <summary>
        /// Sets the value by string of an enum property
        /// </summary>
        private void SetEnumProperty(Tokenizer tokenizer, PropertyInfo property)
        {
            var propertyType = property.PropertyType;

            if (!propertyType.IsEnum)
            {
                throw new IncorrectCommandFormatException($"Expected a {propertyType.Name}, instead got a literal value");
            }

            var names = Enum.GetNames(propertyType);
            var literalValue = tokenizer.Value;

            if (!names.Contains(literalValue))
            {
                throw new IncorrectCommandFormatException($"Expected one of the following values: {string.Join(", ", names)}");
            }

            property.SetValue(this, Enum.Parse(propertyType, literalValue));
        }

        /// <summary>
        /// Sets the value of a string property
        /// </summary>
        private void SetStringProperty(Tokenizer tokenizer, PropertyInfo property)
        {
            var propertyType = property.PropertyType;

            if (propertyType == typeof(string))
            {
                property.SetValue(this, tokenizer.Value, null);
            }
            else
            {
                throw new IncorrectCommandFormatException($"Expected a number type, found {propertyType} instead");
            }
        }

        /// <summary>
        /// Sets the value of a number (double, float, long or int) property
        /// </summary>
        private void SetNumberProperty(Tokenizer tokenizer, PropertyInfo property)
        {
            var propertyType = property.PropertyType;
            if (propertyType == typeof(double))
            {
                property.SetValue(this, double.Parse(tokenizer.Value), null);
            }
            else if (propertyType == typeof(float))
            {
                property.SetValue(this, float.Parse(tokenizer.Value), null);
            }
            else if (propertyType == typeof(long))
            {
                property.SetValue(this, long.Parse(tokenizer.Value), null);
            }
            else if (propertyType == typeof(int))
            {
                property.SetValue(this, int.Parse(tokenizer.Value), null);
            }
            else
            {
                throw new IncorrectCommandFormatException($"Expected a number type, found {propertyType} instead");
            }
        }
    }
}