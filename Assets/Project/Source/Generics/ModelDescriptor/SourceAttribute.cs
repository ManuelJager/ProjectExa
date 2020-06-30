using System;

namespace Exa.Generics
{
    /// <summary>
    /// Provides a range of values for a model descriptor property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class SourceAttribute : Attribute
    {
        // Range provider
        public IDataSourceProvider DataSourceProvider { get; private set; } = null;

        public SourceAttribute(Type dataSourceProvider)
        {
            AssignDataSourceProvider(dataSourceProvider);
        }

        /// <summary>
        /// Test the data source type, create instance and assign to the DataSourceProvider property
        /// </summary>
        /// <param name="dataSourceProvider"></param>
        private void AssignDataSourceProvider(Type dataSourceProvider)
        {
            if (typeof(IDataSourceProvider).IsAssignableFrom(dataSourceProvider))
            {
                DataSourceProvider = (IDataSourceProvider)Activator.CreateInstance(dataSourceProvider);
            }
            else
            {
                throw new ArgumentException($"{dataSourceProvider.Name} should implement the IValuesSourceProvider interface");
            }
        }
    }
}