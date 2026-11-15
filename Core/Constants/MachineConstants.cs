namespace TerraIndustry.Core.Constants
{
    /// <summary>
    /// Constants for the machine system.
    /// </summary>
    public static class MachineConstants
    {
        // Machine types (string identifiers)
        public const string CoalGenerator = "coal_generator";
        public const string ElectricFurnace = "electric_furnace";
        public const string Crusher = "crusher";
        public const string EnrichmentChamber = "enrichment_chamber";
        public const string AlloySmelter = "alloy_smelter";

        // Default inventory sizes
        public const int DefaultInputSlots = 1;
        public const int DefaultOutputSlots = 1;
        public const int GeneratorFuelSlots = 1;

        // Default processing
        public const int DefaultProcessingTime = 200; // ticks (~3.3 seconds)

        // Tile sizes (in tiles)
        public const int StandardMachineWidth = 3;
        public const int StandardMachineHeight = 3;
    }
}
