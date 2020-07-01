using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Exa.Debug.Commands.Parser
{
    public class CommandTypeContext
    {
        // links aliases to the property info of the type
        public Dictionary<string, PropertyInfo> aliasedProperties { get; }

        // links positional index to the property info of the type
        public Dictionary<int, PropertyInfo> positionalProperties { get; }

        // links property to its help text
        public Dictionary<string, string> propertiesHelpText { get; }

        public CommandTypeContext(Type type)
        {
            bool AreElementsContiguous(int[] array)
            {
                // After sorting, check if current element is one more
                for (int i = 1; i < array.Length; i++)
                {
                    if (array[i] - array[i - 1] != 1)
                    {
                        return false;
                    }
                }

                return true;
            }

            var aliasedProperties = new Dictionary<string, PropertyInfo>();
            var positionalProperties = new Dictionary<int, PropertyInfo>();
            var propertiesHelpText = new Dictionary<string, string>();

            var orders = new List<int>();
            var properties = type.GetProperties();
            var aliases = new List<string>();

            foreach (var property in properties)
            {
                ArgumentDefinitionAttribute argument;

                try
                {
                    argument = property.GetProperty<ArgumentDefinitionAttribute>();
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }

                // Handle property types
                if (!(
                    property.PropertyType.IsEnum ||
                    property.PropertyType == typeof(string) ||
                    property.PropertyType == typeof(double) ||
                    property.PropertyType == typeof(float) ||
                    property.PropertyType == typeof(long) ||
                    property.PropertyType == typeof(bool) ||
                    property.PropertyType == typeof(int)))
                {
                    throw new IncorrectModelFormatException($"Unsupported type of {property.PropertyType} present on property {property.Name}");
                }

                if (!(property.CanRead && property.CanWrite))
                {
                    throw new IncorrectModelFormatException($"Property: {property.Name} must be have both a setter and a getter");
                }

                // Index properties to the model context
                if (argument.Positional)
                {
                    var order = argument.ArgumentOrder;
                    orders.Add(order);
                    positionalProperties[order] = property;
                    propertiesHelpText[property.Name] = argument.HelpText;
                }
                else
                {
                    if (argument.aliases == null)
                    {
                        var alias = property.Name;
                        aliases.Add(alias);
                        aliasedProperties[alias] = property;
                        propertiesHelpText[alias] = argument.HelpText;
                    }
                    else
                    {
                        for (int i = 0; i < argument.aliases.Length; i++)
                        {
                            var alias = argument.aliases[i];
                            aliases.Add(alias);
                            aliasedProperties[alias] = property;
                            propertiesHelpText[alias] = argument.HelpText;
                        }
                    }
                }
            };

            // Handle positional arguments
            if (orders.Count != 0)
            {
                var sortedOrders = orders.ToArray();
                Array.Sort(sortedOrders);

                if (sortedOrders[0] != 0)
                {
                    throw new IncorrectModelFormatException($"Order index of the first argument is 0 based");
                }

                if (!AreElementsContiguous(sortedOrders))
                {
                    throw new IncorrectModelFormatException($"Found a incorrect argument value order");
                }
            }

            var duplicateAliases = aliases.GroupBy(x => x)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key);

            if (duplicateAliases.Count() > 0)
            {
                throw new IncorrectModelFormatException($"Duplicate aliases found, aliases are ${string.Join(", ", duplicateAliases)}");
            }

            this.aliasedProperties = aliasedProperties;
            this.positionalProperties = positionalProperties;
            this.propertiesHelpText = propertiesHelpText;
        }
    }
}