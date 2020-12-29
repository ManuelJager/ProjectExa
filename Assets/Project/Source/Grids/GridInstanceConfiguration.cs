namespace Exa.Grids
{
    public struct GridInstanceConfiguration
    {
        public bool Invulnerable { get; set; }

        public static GridInstanceConfiguration InvulnerableConfig => new GridInstanceConfiguration {
            Invulnerable = true
        };
    }
}