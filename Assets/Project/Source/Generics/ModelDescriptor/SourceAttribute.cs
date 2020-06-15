using System;

namespace Exa.Generics
{
    public class SourceAttribute : Attribute
    {
        public IDataSourceProvider DataSourceProvider { get; private set; } = null;
        public IOptionCreationListener ViewCreationlistener { get; private set; } = null;

        public SourceAttribute(Type dataSourceProvider)
        {
            AssignDataSourceProvider(dataSourceProvider);
        }

        public SourceAttribute(Type dataSourceProvider, Type viewCreationListener)
        {
            AssignDataSourceProvider(dataSourceProvider);
            AssignViewCreationListener(viewCreationListener);
        }

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

        private void AssignViewCreationListener(Type viewCreationlistener)
        {
            if (typeof(IOptionCreationListener).IsAssignableFrom(viewCreationlistener))
            {
                ViewCreationlistener = (IOptionCreationListener)Activator.CreateInstance(viewCreationlistener);
            }
            else
            {
                throw new ArgumentException($"{viewCreationlistener.Name} should implement the IValuesSourceProvider interface");
            }
        }
    }
}