namespace Exa.Grids.Blueprints
{
    public class BlueprintCreationOptions
    {
        public string name;
        public string shipClass;

        public BlueprintCreationOptions(string name, string shipClass)
        {
            this.name = name;
            this.shipClass = shipClass;
        }
    }
}